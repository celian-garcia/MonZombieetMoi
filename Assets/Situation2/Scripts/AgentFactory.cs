using System;
using System.Xml;
using System.IO;
using System.Text;

namespace parser
{
	public class AgentFactory
	{
		private static String AGENTS = "agents"; 
		//private static String AGENT = "agent";
		private static String ATT_ID = "id";
		
		private static String CHARACTERISTICS = "characteristics"; 
		private static String AGE = "age"; 
		private static String STRESS = "stress_level"; 
		private static String INTEREST = "interest_level"; 
		
		private static String BODY = "body"; 
		private static String ATT_NAME = "name"; 
		private static String PATH = "path"; 
		private static String ATT_SENSE = "sense"; 
		
		private static String NORMAL = "normal"; 
		private static String YOUNG = "young"; 
		private static String OLD = "old"; 
		
		public static AgentMonitor parseAgentMonitorFromXML(String pathXML){
			XmlDocument doc = new XmlDocument();
			doc.PreserveWhitespace = true;

			try {
				doc.Load(pathXML);
				AgentMonitor monitor = new AgentMonitor();
				XmlNode root = doc[AGENTS];
				
				if (root.HasChildNodes){
			    	for (int i=0; i<root.ChildNodes.Count; i++){
			    		XmlNode node = root.ChildNodes[i];
						if(node.NodeType == XmlNodeType.Element){
							XmlElement nodeAgent = (XmlElement)node;
							monitor.addAgent(nodeAgent.GetAttribute(ATT_ID), parseAgentFromXML(nodeAgent));
						}
			    	}
			    }
				
				return monitor;
			}
			catch (System.IO.FileNotFoundException){
				Console.WriteLine ("le fichier " + pathXML + " est introuvable");
			}
			return null;
		}
		
		public static Agent parseAgentFromXML(XmlElement elem){
			String id = elem.GetAttribute(ATT_ID);
			
			XmlElement charaNode = (XmlElement) elem.GetElementsByTagName(CHARACTERISTICS).Item(0);
			String ageString = charaNode[AGE].InnerText;
			Age age = Age.NORMAL;
			if( ageString.Equals(NORMAL)){
				age = Age.NORMAL;
			}else if( ageString.Equals(YOUNG)){
				age = Age.YOUNG;
			}else if( ageString.Equals(OLD)){
				age = Age.OLD;
			}
			
			int stress = Convert.ToInt32(charaNode[STRESS].InnerText);
			int interest = Convert.ToInt32(charaNode[INTEREST].InnerText);
			
			String body = elem[BODY].InnerText;
			String nameBody = elem[BODY].GetAttribute(ATT_NAME);
			
			String path = elem[PATH].InnerText;
			int sensePath = Convert.ToInt32(elem[PATH].GetAttribute(ATT_SENSE));
			
			return new Agent(id, age, stress, interest, body, nameBody, sensePath, path);
		}
	}
}

