using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using parser;

public class AgentSimulation : MonoBehaviour {

	public string XMLPath;
	public float visitDuration;
	public Camera initialCamera;

	public bool withIntro;

	private InputManager input_manager;
	private CameraManager camera_manager;
	private List<Agent> agents;
	private Agent current_agent;

	// Set default value to XML/agents.xml
	private void OnValidate()
	{
		XMLPath = "Assets/Situation2/XML/agents.xml";
		visitDuration = 80;
	}

	// Use this for initialization
	void Start () {

		// Get agents and assign to each a GameObject
		this.ParseAgents (XMLPath);

		// Retrieve managers
		this.input_manager = GetComponent<InputManager> ();
		this.camera_manager = GetComponent<CameraManager> ();

		// Initialize the camera with the first agent one
		this.camera_manager.setMainCamera(initialCamera);

		//this.withIntro = this.GetComponent<changementSceneScript> ().withIntro;

//		GameObject.Find ("Main Camera

//		// Initialize the camera with the main painting one
//		this.camera_manager.setMainCamera(GameObject.Find ("Master").GetComponentInChildren<Camera>());
	}

	// Called each frame
    void Update() {

		// If you want to move an agent it is here

//		agents[0].Body.GetComponent<MovementManager>().Move(
//			this.input_manager.getControlType(),
//			this.input_manager.getDirection(),
//			this.input_manager.getOrientation()
//		);



		// We can orient the camera
		this.camera_manager.Orient(input_manager.getOrientation ());

	}

	private void ParseAgents (string xml_path) {
		// Parse agents
		this.agents = AgentFactory.parseAgentMonitorFromXML(xml_path).getAgents();

		float late = 0;

		foreach (Agent a in this.agents) {

			// Instantiate the agent body from resources
			GameObject body = Instantiate(Resources.Load(a.BodyFileName)) as GameObject;
			a.AssignBody (body);

			GameObject path_go = UniqueInstantiate(a.PathFileName);

			// Create and add the SplineFollow to the agent body
			// TODO : add the sense to permits to the agent begin from either the right or the left
			SplineFollow.CreateSplineFollow(
				body, path_go.GetComponent<BezierSpline>(), visitDuration, SplineWalkerMode.Once, late += 5.0f
			);
		}
	}



	private string getLastName(string path) {

		string[] separators = new string[1]{"/"};
		string[] pathFileNameEntries = path.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
		
		return pathFileNameEntries [pathFileNameEntries.Length - 1];
	}

	private GameObject UniqueInstantiate(string resource_path) {

		// Intantiate the path's game object, if it is not already
		string pathName = getLastName(resource_path);
		GameObject path_go = GameObject.Find (pathName);
		path_go = (path_go == null) ? Instantiate(Resources.Load(resource_path)) as GameObject : path_go;
		path_go.name = path_go.name.Replace("(Clone)", "");

		return path_go;
	}



}
