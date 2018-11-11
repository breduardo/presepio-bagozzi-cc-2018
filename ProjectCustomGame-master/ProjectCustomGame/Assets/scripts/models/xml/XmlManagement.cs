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
using System;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using NatanielSoaresRodrigues.ProjectCustomGame.Objs;

//utilizar hashset, para não repetir objetos, para identificar se é igual é preciso implementar o metodo Equals da classe

namespace NatanielSoaresRodrigues.ProjectCustomGame.Xml
{
	public class XmlManagement {

		Type type;
		string fileName;

		public XmlManagement(string fileName, Type type){
			this.fileName = fileName;
			this.type = type;

		}

		public object openFile(){
			TextAsset xml = (TextAsset) Resources.Load("xml/"+fileName);

			object container;

			using(MemoryStream ms = new MemoryStream(xml.bytes)){
				var serializer = new XmlSerializer (type);
				container = serializer.Deserialize (ms);
			}

			return container;
		}

		void testDialogues(List<Dialog> listD)
		{

			for (int i = 0; i < listD.Count; i++) {
				for (int j = i+1; j < listD.Count; j++) {
					if (listD [i].Id == listD [j].Id)
						throw new Exception ("There is Duplicate id's in your Dialogues XML file");

					if(listD[i].NextDialog != null && 
						listD[i].NextDialog.Length > 5)
						throw new Exception ("The Maximum number of options is 5");
				}
			}
		}

	}

}