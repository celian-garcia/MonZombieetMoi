using UnityEngine;
using System.Collections;
using System.Linq;

public class CameraManager : MonoBehaviour {

	private Camera transition_camera;
	private Camera current_camera;

	private OVRCameraRig transition_oculus;
	private OVRCameraRig current_oculus;
	
	public void Awake() {
	}

	public GameObject[] getAllCameras () {
		GameObject[] fp = GameObject.FindGameObjectsWithTag ("FPCamera");
		GameObject[] tp = GameObject.FindGameObjectsWithTag ("TPCamera");
		return fp.Concat(tp).ToArray();
	}

	public GameObject[] getAllOculuses () {
		return GameObject.FindGameObjectsWithTag("Oculus");
	}

	public void DisableAll () {
		DisableAllCameras();
		DisableAllOculuses();
	}

	public void DisableAllCameras() {
		GameObject[] cameras = getAllCameras ();

		foreach (GameObject c in cameras) {
			c.GetComponent<Camera>().enabled = false;
		}
	}

	public void DisableAllOculuses() {
		GameObject[] oculuses = getAllOculuses();
		
		foreach (GameObject oc in oculuses) {
			oc.GetComponent<OVRCameraRig>().enabled = false;
		}
	}

	public void setMainCamera (string camera) {
		// Set the current camera
		this.setMainCamera(GameObject.Find(camera).GetComponent<Camera>());
	}

	public void setMainCamera (Camera camera) {
		// Disable all cameras
		DisableAll ();
		
		// Set the current camera
		this.current_camera = camera;
		// and enable it
		this.current_camera.enabled = true;
	}

	public void setOculusCamera(OVRCameraRig camera) {
		// Disable all cameras
		DisableAll();

		// Set the current camera
		this.current_oculus = camera;
		// and enable it
		this.current_oculus.enabled = true;
	}

	public void Orient(Vector2 orientation) {
		this.current_camera.gameObject.transform.Rotate(
			orientation.y, orientation.x, 0
		);
	}
	public void changeCamera (Camera camera) {
		// TODO : switch of camera using differents transitions
		// You can use currentCamera and/or transitionCamera 
	}

	public void changeOculus (OVRCameraRig camera) {
		// TODO : switch of oculus using differents transitions
		// You can use currentOculus and/or transitionOculus 
	}
}
