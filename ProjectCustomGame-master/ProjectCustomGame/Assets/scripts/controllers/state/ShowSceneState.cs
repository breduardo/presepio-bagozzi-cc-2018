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
using NatanielSoaresRodrigues.ProjectCustomGame.Utils;


namespace NatanielSoaresRodrigues.ProjectCustomGame.Controllers.State
{
	public class ShowSceneState : StateGame
	{
		public override void execute (MainController main)
		{
			var scene = main.sceneManager.CurrentScene;

			if (scene == null) {
				//none character selected
				OnFinishState (new EventArgs());
				return;			
			}

			if (main.sceneManager.IsSameObject) {
				OnFinishState (new EventArgs());
				return;	
			}

			Texture texture = main.sceneManager.loadTexture();
				
			if (texture != null) {
				//	main.backgroundImage.SetActive(true);
				main.backgroundImageRenderer.material.mainTexture = texture;
				main.backgroundImageAnimatorEventTrigger.finishAnim += finishSceneAnimation;
				main.backgroundImageAnimator.enabled = true;
				main.backgroundImageAnimator.Play ("sceneAnimationIn");
			} else {
				OnFinishState (new EventArgs ());
			}
		

		}

		void finishSceneAnimation(object sender, EventArgs e){
			//the animation finish
			OnFinishState (new EventArgs ());
		}

		public override void nextState (MainController main)
		{
			main.backgroundImageAnimatorEventTrigger.finishAnim -= finishSceneAnimation; //you do not want to remove this line, trust me or errors will pop up
			main.currentState = new CharacterEnteredState ();
		}
	}
}

