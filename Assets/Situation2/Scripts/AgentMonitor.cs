using System;
using System.Collections.Generic;

namespace parser
{
	public class AgentMonitor
	{
		private IDictionary<String, Agent> dictionaryAgents = new Dictionary<String, Agent>();
		
		public void addAgent(String id, Agent agent){
			this.dictionaryAgents.Add(id, agent);
		}
		
		public Agent getAgentWithId(String id){
			return this.dictionaryAgents[id];	
		}

		public List<Agent> getAgents () {
			List<Agent> agents = new List<Agent>(dictionaryAgents.Count);
			agents.AddRange(dictionaryAgents.Values);
			return agents;
		}

	}
}

