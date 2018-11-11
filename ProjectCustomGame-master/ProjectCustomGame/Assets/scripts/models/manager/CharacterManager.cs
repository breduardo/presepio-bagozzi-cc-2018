﻿/*
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

using System.Collections.Generic;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using NatanielSoaresRodrigues.ProjectCustomGame.Xml;
using System.IO;
using UnityEngine;
using System;


namespace NatanielSoaresRodrigues.ProjectCustomGame.Models.Manager
{
	public class CharacterManager : MyObjectManager
	{
		public Character CurrentCharacter {
			get{ 
				return current as Character;
			}
			private set{ }
		}


		public CharacterManager() : base()
		{
			CharacterContainer container =  new XmlManagement ("characters",typeof(CharacterContainer)).openFile () as CharacterContainer;
			mainList = container.characters.ToArray ();

		}

		protected override void mainListOk ()
		{
			throw new System.NotImplementedException ();
		}

		public Sprite loadImage(){
			if (CurrentCharacter.CharacterImage == null)
				return null;

			Sprite spriteImage = Resources.Load<Sprite> ("images/" + CurrentCharacter.CharacterImage);

			return spriteImage;

		}


	}
}
