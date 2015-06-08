using System;
using UnityEngine;

namespace parser
{
	public class Agent
	{
		private String id;
		
		private Age age;
			
		private int stress;
		
		private int interest;
		
		private String bodyFileName;
		private String bodyName;
		private GameObject body;
		
		private int pathSense;
		private String pathFileName;
		
		public Agent (String id, Age age, int stress, int interest, String bodyFileName, String bodyName, int pathSense, String pathFileName)
		{
			this.id = id;
			this.age = age;
			this.stress = stress;
			this.interest = interest;
			this.bodyFileName = bodyFileName;
			this.bodyName = bodyName;
			this.pathSense = pathSense;
			this.pathFileName = pathFileName;
		}

		public Age Age {
			get {
				return this.age;
			}
		}

		public String BodyFileName {
			get {
				return this.bodyFileName;
			}
		}

		public String BodyName {
			get {
				return this.bodyName;
			}
		}

		public GameObject Body {
			get {
				return this.body;
			}
		}

		public int Interest {
			get {
				return this.interest;
			}
		}

		public String PathFileName {
			get {
				return this.pathFileName;
			}
		}

		public int PathSense {
			get {
				return this.pathSense;
			}
		}

		public int Stress {
			get {
				return this.stress;
			}
		}

		public void AssignBody (GameObject b){
			this.body = b;
		}
	}
}

