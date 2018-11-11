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

using System;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using UnityEngine;
using UnityEngine.UI;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Controllers.State
{
	public class ShowMessageState : StateGame
	{

		public override void execute(MainController main){
			if (main.dialogManager.IsDialog) {
				//single dialog message

				main.typewriterScript.finishTypewriter += finishTypewriter;

				Dialog dialog = main.dialogManager.CurrentDialog;

				string message = dialog.Message;

				message = "\t" + dialog.Message;

				main.typewriterScript.showMessage(message);

				Debug.Log (dialog.Message);
			} else {

				//option dialog

				main.buttons.SetActive (true);
				int i = 0;
				Dialog[] dialogues = main.dialogManager.DialogOptions;


				foreach (var d in dialogues) {
					Button btn = main.optionButtons [i];
					btn.gameObject.SetActive (true);

					main.textButtons[i].text = "\t"+d.Message;

					Debug.Log (d.Message);
					i++;
				}
				OnFinishState (new EventArgs());
			}
		
		}
		
		void finishTypewriter(object sender, EventArgs e){
			//the typewriter finish

			Debug.Log ("Finish the typewrite");
			OnFinishState (new EventArgs ());
		}

		public override void nextState(MainController main){
			main.typewriterScript.finishTypewriter -= finishTypewriter; //you do not want to remove this line, trust me or errors will pop up
			main.currentState = new SelectActionState ();
		}

	}
}

