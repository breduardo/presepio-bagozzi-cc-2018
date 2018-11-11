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

using NatanielSoaresRodrigues.ProjectCustomGame.Objs;
using System.Collections.Generic;
using System;
using NatanielSoaresRodrigues.ProjectCustomGame.Xml;

namespace NatanielSoaresRodrigues.ProjectCustomGame.Models.Manager
{
	public abstract class MyObjectManager
	{
		protected MyObject current;
		protected MyObject[] mainList;
		public bool HasPreviousObject {
			get;
			private set;
		}
		public bool IsSameObject {
			get;
			private set;
		}

		public MyObjectManager ()
		{
			HasPreviousObject = false;
			IsSameObject = false;
		}

		public virtual void selectCurrent(string id){

			if (current != null)
				HasPreviousObject = true;
			else
				HasPreviousObject = false;

			if (id == null) {
				clearCurrent ();
				IsSameObject = false;
				return;
			} else if (current != null &&
				id == current.Id) {
				IsSameObject = true;
				return;
			}

			IsSameObject = false;
			current = findMyObject (id);
		}

		public virtual void clearCurrent(){
			current = null;
		}

		protected MyObject findMyObject(string id)
		{
			if (id == null)
				return null;

			//find a object in a list
			List<MyObject> aux =  new List<MyObject>(mainList);

			MyObject o = aux.Find (x => x.Id == id);
			if(o == null)
				throw new Exception("The Object with id : '" + id + "' is not found ");
			return o;
		}

		public void initialize(){
			//initialize  with the first item of the mainlist

			if (mainList != null && mainList.Length > 0) {
				current = mainList [0];

				mainListOk ();

			} else {
				throw new Exception ("The List is Empty");
			}
		}

		protected abstract void mainListOk ();

	}
}

