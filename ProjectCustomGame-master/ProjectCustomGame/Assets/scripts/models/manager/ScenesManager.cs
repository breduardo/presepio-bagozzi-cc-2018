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

using NatanielSoaresRodrigues.ProjectCustomGame.Xml;
using UnityEngine;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Models.Manager
{
	public class ScenesManager : MyObjectManager
	{
		public Scene CurrentScene {
			get{ 
				return current as Scene;
			}
			private set{ }
		}


		public ScenesManager() : base()
		{
			SceneContainer container =  new XmlManagement ("scenes", typeof(SceneContainer)).openFile () as SceneContainer;
			mainList = container.scenes.ToArray ();
		}

		public Texture loadTexture(){
			if (CurrentScene == null)
				return null;
			if (CurrentScene.SceneImage == null)
				return null;

			Texture textureImage = Resources.Load<Texture> ("images/" + CurrentScene.SceneImage);

			return textureImage;

		}

		protected override void mainListOk ()
		{
			throw new System.NotImplementedException ();
		}
	}
}

