using System;

namespace parser
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			AgentMonitor monitor =  AgentFactory.parseAgentMonitorFromXML("agents.xml");
			Agent agent = monitor.getAgentWithId("agent1");
			
			Console.WriteLine (agent.PathSense);
		}
	}
}
