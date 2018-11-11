/*
   The MIT License (MIT)

Copyright (c) 2017 Nataniel Soares Rodrigues

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using System.Collections;
using System.Collections.Generic;
using System;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using NatanielSoaresRodrigues.ProjectCustomGame.Xml;


namespace NatanielSoaresRodrigues.ProjectCustomGame.Models.Manager
{
	public delegate void ShowDialogResponseHandler(object source, DialogEventArgs e);
	public class DialogManager : MyObjectManager
	{

		public Dialog CurrentDialog {
			get{ 
				return current as Dialog;
			}
			private set{ }
		}

		public bool IsDialog {
			get;
			private set;
		}

		string impact;


		event ShowDialogResponseHandler showDialogResponse;

		public Dialog[] DialogOptions { get; private set;}


		public DialogManager(ShowDialogResponseHandler showDialogResponse) : base()
		{

			DialogContainer container = new XmlManagement("dialogues", typeof(DialogContainer)).openFile () as DialogContainer; //read the xml
			mainList = container.dialogues.ToArray(); 

			this.showDialogResponse = showDialogResponse; //set the event listner


		}


		public void checkDialog()
		{
			Dialog currentDialog = current as Dialog;

			if (currentDialog.NextDialog == null) //next dialog is not found
				return;
			
			int numbersOfD = currentDialog.NextDialog.Length; //number of options for the player choose
			if (numbersOfD > 0) {
				// found more than zero option

				if (numbersOfD > 1) {
					//found more than one option

					DialogOptions = new Dialog[numbersOfD];

					int i = 0;
					foreach (string dialogId in currentDialog.NextDialog) {
						Dialog nxtDialog = findMyObject (dialogId) as Dialog;

						if (nxtDialog.Id == impact) {
							//select impact
							DialogOptions = null;
							impact = null;
							selectCurrent (nxtDialog.Id);
							return;
						}	

						DialogOptions [i] = nxtDialog;

						i++;
					}

					OnShowDialogResponse (new DialogEventArgs (DialogOptions)); //call the event to show the message

				} else { 
					//found only one option then move on

					selectCurrent(currentDialog.NextDialog[0]);
				}

			} else 	{
				//did not found any option
				throw new Exception ("The dialogue cycle has been broken : Current Dialog id = '"+current.Id+"'");
			}
		}

		public void selectOption(int opt){
			//select a option that the player choose

			if (DialogOptions == null) {
				//if have no options move on

				checkDialog ();
			}
			else if((opt+1) > DialogOptions.Length)	{
				//if selected a option that doesn't exist

				return;
			}
			else {
				//selected a valid option

				int nOfresponseOpt = DialogOptions [opt].NextDialog.Length; //number of option redirects

				if (nOfresponseOpt > 1 || nOfresponseOpt == 0) {
					// the option must be have just a one option to redirect
					throw new Exception ("A dialog option should have only one dialog redirect : Option Dialog id = '" + DialogOptions [opt].Id + "'");

				}

				if (!String.IsNullOrEmpty (DialogOptions [opt].Impact))
					impact = DialogOptions [opt].Impact;

				selectCurrent (DialogOptions [opt].NextDialog[0]);
			}
		}
			
		public override void selectCurrent (string id)
		{
			base.selectCurrent (id);
			DialogOptions = null;

			if (CurrentDialog != null && !String.IsNullOrEmpty (CurrentDialog.Impact))
				impact = CurrentDialog.Impact;
			
			OnShowDialogResponse (new DialogEventArgs (CurrentDialog)); //call the event to show the message
		}


		protected override void mainListOk ()
		{
			OnShowDialogResponse (new DialogEventArgs (current as Dialog)); //call the event to show the message
		}

		protected virtual void OnShowDialogResponse(DialogEventArgs e)
		{
			if (e.Dialogues.Length == 1)
				IsDialog = true;
			else if (e.Dialogues.Length > 1)
				IsDialog = false;
				
			//show the message to the view

			if (showDialogResponse != null)
				showDialogResponse (this, e);
		}

	}
}