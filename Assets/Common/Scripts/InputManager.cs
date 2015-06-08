using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	// ENUM
	public enum ControlType {
		FIRST_PERSON,
		THIRD_PERSON
	}
	public enum BodyControl {
		XBOX,
		KEYS,
		XML,
		INFRARED
	}
	public enum HeadControl {
		XBOX,
		MOUSE,
		OCULUS
	}

	// PUBLIC MEMBERS
	public ControlType control_type = ControlType.THIRD_PERSON;
	public BodyControl body_controls = BodyControl.XBOX;
	public HeadControl head_controls = HeadControl.XBOX;
	
	// PRIVATE MEMBERS
	private bool sneak = false;
	private Vector2 direction; // no jump
	private Vector2 rotation; 
	private bool oculus_use;
	private bool infrared_use;
//	private MovementManager mover;

	// STATIC
	private static float XBOX_SENSITIVITY = 0.6f;
	
	void Start () {
//		mover = GetComponent<MovementManager> ();
	}
	
	void FixedUpdate () {

		oculus_use = false;
		infrared_use = false;

		// Update directions of the body
		switch (body_controls) {
			case BodyControl.XBOX :
				UpdateXboxDirection ();
			break;
			case BodyControl.KEYS : 
				UpdateKeysDirection ();
			break;
			case BodyControl.XML :
				UpdateXmlDirection ();
			break;
			case BodyControl.INFRARED :
				UpdateInfraredDirection ();
			break;
			default :
				Debug.LogError("You have to select a body control in the input manager");
			break;
		}

		// Update rotations in case of fps controls
		if (control_type == ControlType.FIRST_PERSON) {
			switch (head_controls) {
				case HeadControl.XBOX:
					UpdateXboxRotation ();
				break;
				case HeadControl.MOUSE: 
					UpdateMouseRotation ();
				break;
				case HeadControl.OCULUS:
					UpdateOculusRotation ();
				break;
				default :
					Debug.LogError ("You have to select a head control in the input manager");
				break;
			}
		}

//		mover.Move (control_type, direction, rotation);
	}

	public ControlType getControlType () {
		return this.control_type;
	}

	public Vector2 getDirection () {
		return this.direction;
	}

	public Vector2 getOrientation () {
		return this.rotation;
	}
	
	void UpdateXboxDirection() {

		// Record Inputs
		float h =  Input.GetAxis("XboxHorizontal1");
		float v = -Input.GetAxis("XboxVertical1");

		if (Mathf.Abs (h) < XBOX_SENSITIVITY) {
			h = 0;
		}
		if (Mathf.Abs (v) < XBOX_SENSITIVITY) {
			v = 0;
		}

		// Compute for the mover
		direction = new Vector2 (h, v);
	}

	void UpdateKeysDirection() {

		// Record Inputs
		float h = Input.GetAxis("KeyHorizontal");
		float v = Input.GetAxis("KeyVertical");

		// Compute for the mover
		direction = new Vector2 (h, v);
	}

	void UpdateXmlDirection() {
		infrared_use = true;
	}

	void UpdateInfraredDirection() {
		Debug.LogError("Infrared in construction");
		// TO DO : the character follow our infrared movements
	}

	void UpdateXboxRotation() {
		
		// Record Inputs
		float x =  Input.GetAxis("XboxHorizontal2");
		float y = -Input.GetAxis("XboxVertical2");
		
		if (Mathf.Abs (x) < XBOX_SENSITIVITY) {
			x = 0;
		}
		if (Mathf.Abs (y) < XBOX_SENSITIVITY) {
			y = 0;
		}
		
		// Compute for the mover
		rotation = new Vector2 (x, y);
	}
	
	void UpdateMouseRotation() {
		
		// Record Inputs
		float x = Input.GetAxis("Mouse X");
		float y = -Input.GetAxis("Mouse Y");
		//		sneak = Input.GetButton("Sneak");
		
		// Compute for the mover
		rotation = new Vector2 (x, y);
	}
	
	void UpdateOculusRotation() {
		oculus_use = true;
	}



}
