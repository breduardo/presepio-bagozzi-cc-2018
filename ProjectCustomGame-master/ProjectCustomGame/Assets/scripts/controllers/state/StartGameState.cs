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
using UnityEngine;
using NatanielSoaresRodrigues.ProjectCustomGame.Models.Manager;
using NatanielSoaresRodrigues.ProjectCustomGame.Utils;
using System.Diagnostics;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Controllers.State
{
	public class StartGameState : StateGame
	{

		bool playAnimationChar = false;
		bool playAnimationScene = false;
		bool lastFinsih = false;

		public override void execute(MainController main){
			UnityEngine.Debug.Log ("Started the state");
			var d1 = Stopwatch.StartNew ();


		
			//disable dialog
			if(main.dialogManager.IsDialog)
				main.typewriterScript.cleanText();

			//disable buttons
			main.canSelect = false;
			main.buttons.SetActive (false);
			foreach (var btn in main.optionButtons) {
				btn.gameObject.SetActive(false);
			}

			//disable scene
			playAnimationScene = playAnimation(main.sceneManager);

			if (playAnimationScene) {
				main.backgroundImageAnimator.enabled = true;
				main.backgroundImageAnimatorEventTrigger.finishAnim += finishSceneAnimation;
				main.backgroundImageAnimator.Play ("sceneAnimationExit");
			} else {
				lastFinsih = true;
			}


			//disable character
			playAnimationChar = playAnimation(main.characterManager, main.characterNameText);

			if (playAnimationChar) {
				main.characterImageAnimator.enabled = true;
				main.characterImageAnimationEventTrigger.finishAnim += finishCharacterAnimation;
				main.characterImageAnimator.Play ("characterAnimationExit");
			} else {
				lastFinsih = true;
			}

			if(!playAnimationChar && !playAnimationScene)
				OnFinishState (new EventArgs());
			
			UnityEngine.Debug.Log("Tempo: "+d1.ElapsedMilliseconds.ToString("0 ms"));
		}

		bool playAnimation(MyObjectManager manager)
		{
			return playAnimation (manager, null);
		}

		bool playAnimation(MyObjectManager manager, GameObject obj){

			//disable obj
			if (!manager.IsSameObject) {
				if(obj != null)
					obj.SetActive (false); //disable the object

				if (manager.HasPreviousObject) {
					return true; //play the animation
				} else
					return false;
			} else
				return false;
		}

		void finishCharacterAnimation(object sender, EventArgs e){
			//the char animation finish
			if (lastFinsih)
				OnFinishState (new EventArgs ());
			else
				lastFinsih = true;
		}

		void finishSceneAnimation(object sender, EventArgs e){
			//the scene animation finish
			if (lastFinsih)
				OnFinishState (new EventArgs ());
			else
				lastFinsih = true;
		}

		public override void nextState (MainController main){
			main.characterImageAnimationEventTrigger.finishAnim -= finishCharacterAnimation; //you do not want to remove this line, trust me or errors will pop up
			main.backgroundImageAnimatorEventTrigger.finishAnim -= finishSceneAnimation; //you do not want to remove this line, trust me or errors will pop up
			main.currentState = new ShowSceneState ();
		}

	}
}

