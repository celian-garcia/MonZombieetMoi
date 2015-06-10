using UnityEngine;
using System.Collections;
using System.Linq;

public class CameraManager : MonoBehaviour {

	private Camera transition_camera;
	private Camera current_camera;
	private Camera targetCamera;

	private OVRCameraRig transition_oculus;
	private OVRCameraRig current_oculus;

	private GameObject item;
	private GameObject ethanBody;
	private bool itemEnable = true;

	private FaderScript faderScript;
	private Quaternion transformStock;

	void Start(){
		faderScript = this.gameObject.GetComponent<FaderScript>();
		targetCamera = null;
	}

	void Update(){
		rayCast();
		toSwhichOrNotToSwhich();
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

	public void rayCast(){

		Ray ray = current_camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {

			if (item != null && item != hit.transform.gameObject){
				if (item.name == "char_ethan_museum(Clone)")
					ethanBody.GetComponent<SkinnedMeshRenderer>().enabled = true;
				else
					item.GetComponent<MeshRenderer>().enabled = true;
			}
			if (hit.transform.name == "char_ethan_museum(Clone)" ||  hit.transform.tag == "Painting")// Est-ce Un char Ethan ou un tableau ? 
			{
				item = hit.transform.gameObject; // On le stock

				foreach(Transform child in item.transform){
					if (child.name == "char_ethan_body"){
						ethanBody = child.gameObject; // on stock son body
						ethanBody.GetComponent<SkinnedMeshRenderer>().enabled = itemEnable;
					}else if(child.gameObject.GetComponent<Camera>() != null){
						targetCamera = child.gameObject.GetComponent<Camera>();
					}
				}
				if(hit.transform.tag == "Painting")
					item.GetComponent<MeshRenderer>().enabled = itemEnable;
				itemEnable = !itemEnable; 
			}
		}
	}

	private void toSwhichOrNotToSwhich(){

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if (this.current_camera != targetCamera) {
				faderScript.BeginFade (1, 100);
				current_camera.transform.rotation = transformStock;
				setMainCamera (targetCamera);
				transformStock = current_camera.transform.rotation;
				faderScript.BeginFade (-1, 2, 0.5f);
			}
		
		} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if (this.current_camera != targetCamera) {
				current_camera.transform.rotation = transformStock;
				setMainCamera (targetCamera);
				transformStock = current_camera.transform.rotation;
			}

		}else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			if (this.current_camera != targetCamera) {
				CameraMouvementsScript cms = targetCamera.GetComponent<CameraMouvementsScript>();
//
//				current_camera.transform = transformStock;
//				setMainCamera (targetCamera);
//				transformStock = current_camera.transform;
			}
			
		}

	}

}
