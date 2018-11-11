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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Utils
{
	public delegate void finishTypewriterHandler(object source, EventArgs e);
	public class TypewriterScript : MonoBehaviour {

		public event finishTypewriterHandler finishTypewriter;

		private float delay = 0.025f;
		private string fullText;
		private string currentText = "";
		Text textBox;


		public void showMessage(string message)
		{
			
			this.fullText = message;
			StartCoroutine (ShowText());
		}

		public void showAllMessage()
		{
			StopCoroutine (ShowText ());
			textBox.text = fullText;

		}

		public void cleanText(){
			textBox = this.GetComponent<Text> ();
			textBox.text = "";
		}

		IEnumerator ShowText(){
			for (int i = 0; i < fullText.Length+1; i++) {
				currentText = fullText.Substring (0, i);

				textBox.text = currentText;

				yield return new WaitForSeconds (delay);
			}
			OnFinishTypewriter (new EventArgs());

		}

		protected virtual void OnFinishTypewriter(EventArgs e)
		{
			//show the message to the view

			if (finishTypewriter != null)
				finishTypewriter (this, e);
		}
	}

}