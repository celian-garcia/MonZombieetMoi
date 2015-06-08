using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using parser;

public class AgentSimulation : MonoBehaviour {

	public string XMLPath;
	private InputManager input_manager;
	private CameraManager camera_manager;
	private List<Agent> agents;
	private Agent current_agent;

	// Use this for initialization
	void Start () {

		// Get agents and assign to each a GameObject
		this.ParseAgents (XMLPath);

		// Retrieve managers
		this.input_manager = GetComponent<InputManager> ();
		this.camera_manager = GetComponent<CameraManager> ();
		
		// Initialize the main camera
		camera_manager.setCamera ("Painting Camera");
	}

    void Update() {

		// Move each according to the input manager
//		foreach (Agent a in agents) {
//			a.Body.GetComponent<MovementManager>().Move(
//				this.input_manager.getControlType(),
//				this.input_manager.getDirection(),
//				this.input_manager.getOrientation()
//			);
//		}

		// Or move only one, we decide
		agents[0].Body.GetComponent<MovementManager>().Move(
			this.input_manager.getControlType(),
			this.input_manager.getDirection(),
			this.input_manager.getOrientation()
		);

	}

	private void ParseAgents (string xml_path) {
		AgentMonitor monitor =  AgentFactory.parseAgentMonitorFromXML(xml_path);
		// Retrieve all agents from monitor
		this.agents = monitor.getAgents ();

		int i = 0;

		// We instantiate a gameobject for each agent and give it as body
		foreach (Agent a in agents) {
			GameObject body = Instantiate(Resources.Load(a.BodyFileName)) as GameObject;
			body.transform.position = new Vector3(++i, -2.32f, 0);

			a.AssignBody (body);
		}
	}

	// Set default value to XML/agents.xml
	private void OnValidate()
	{
		XMLPath = "Assets/Situation2/XML/agents.xml";
	}



}
