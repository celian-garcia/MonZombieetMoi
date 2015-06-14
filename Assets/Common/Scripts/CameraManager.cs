using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour {

	private Camera transition_camera;
	private Camera current_camera;
	private Camera targetCamera;
	private Camera previousTargetCamera;

	private OVRCameraRig oculus;

	private GameObject item;
	private GameObject ethanBody;
	private GameObject ethanHead;
	private bool itemEnable = true;
	private bool noTarget;
	private bool moving;

	public Slider slider;

	private FaderScript faderScript;
	private Quaternion transformStock;

	public bool clignotement = false;
	public bool occulusYOrN = true;
	public bool autoSwich = true;	
	public float waitTime = 3;
//	public float bride = 40;

	void Start(){
		faderScript = this.gameObject.GetComponent<FaderScript>();
		targetCamera = null;
		transformStock = oculus.transform.rotation;
		ethanHead = null;
		autoSwich = GameObject.FindObjectOfType<Canvas> ().enabled;
		previousTargetCamera = null;

		noTarget = true;

	}

	void Update(){
		rayCast();
		if(targetCamera != null)
			toSwhichOrNotToSwhich();

		if (targetCamera != null && autoSwich)
			autoSwichFunction();

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

	public void setFollowedCamera (string camera) {
		// Set the current camera
		this.setFollowedCamera(GameObject.Find(camera).GetComponent<Camera>());
	}

	public void setFollowedCamera (Camera camera) {

		this.current_camera = camera;
	}

	public void setOculusCamera(OVRCameraRig camera) {
		// Disable all cameras
		DisableAll();

		// Set the oculus
		this.oculus = camera;
		// and enable it
		this.oculus.enabled = true;
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

		Transform oculus_center = oculus.transform.GetChild(0).GetChild(1);

		Ray ray = new Ray(oculus_center.transform.position, oculus_center.transform.forward);

		Debug.DrawRay(ray.origin, ray.direction);
//		Ray ray = oculus.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
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
				noTarget = false;
				item = hit.transform.gameObject; // On le stock

				foreach(Transform child in item.transform){
					if (child.name == "char_ethan_body"){
						ethanBody = child.gameObject; // on stock son body
						onFocus(1);

					}else if(child.gameObject.GetComponent<Camera>() != null){
						targetCamera = child.gameObject.GetComponent<Camera>();
					}
				}
				if(hit.transform.tag == "Painting")
					onFocus(2);
				itemEnable = clignotement ? !itemEnable : true;
			}else 
				noTarget = true;
		}
	}

	private void onFocus(int choix){
		switch (choix)
		{
		case 1:
			ethanBody.GetComponent<SkinnedMeshRenderer>().enabled = itemEnable;
			break;
		case 2:
			item.GetComponent<MeshRenderer>().enabled = itemEnable;
			break;
		default:
			print("BUG");
			break;
		}

	}

	private void onFocus (GameObject gameObject){


	}

	private void toSwhichOrNotToSwhich(){

		if (Input.GetKeyDown (KeyCode.Alpha2) && this.current_camera != targetCamera) {
			faderScript.BeginFade (1, 100f);
			if (!occulusYOrN) {
				oculus.transform.rotation = transformStock;
				setFollowedCamera (targetCamera);
				transformStock = oculus.transform.rotation;	
			} else {
				oculus.transform.position = targetCamera.transform.position;
				oculus.transform.rotation = targetCamera.transform.rotation;
			}
			faderScript.BeginFade (-1, 2, 0.5f);
			oculus.transform.parent = item.transform;

		} else if (Input.GetKeyDown (KeyCode.Alpha1) && this.current_camera != targetCamera) {
			if (!occulusYOrN) {
				oculus.transform.rotation = transformStock;
				setFollowedCamera (targetCamera);
				transformStock = oculus.transform.rotation;
			} else {
				oculus.transform.position = targetCamera.transform.position;
				oculus.transform.rotation = targetCamera.transform.rotation;				
			}
			oculus.transform.parent = item.transform;
		} else if (Input.GetKeyDown (KeyCode.Alpha3) && this.current_camera != targetCamera) {

			StartCoroutine (fadeBeforeSwich ());
			oculus.transform.parent = item.transform;

		} else if (Input.GetKeyDown (KeyCode.Alpha4) && this.current_camera != targetCamera) {
			moveCamera();
		} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
			autoSwich = !autoSwich;
		}
	}

	private void moveCamera(){

		if (!occulusYOrN) {
			CameraMouvementsScript cms = targetCamera.transform.gameObject.AddComponent<CameraMouvementsScript> ();
			oculus.transform.rotation = transformStock;
			
			// Stockage des différents position des caméra pour intervertir.
			Quaternion targetInitialCamRot = targetCamera.transform.rotation; 
			Vector3 targetInitialCamPos = targetCamera.transform.position;
			
			cms.resetLook = targetInitialCamPos + targetCamera.transform.forward * 3;
			cms.pos = new Vector3[] {oculus.transform.position,targetInitialCamPos};
			cms.fadeOutEnable = false;
			
			targetCamera.transform.rotation = oculus.transform.rotation;
			targetCamera.transform.position = oculus.transform.position;
			
			setFollowedCamera (targetCamera);
			transformStock = targetInitialCamRot;
			cms.Start ();
			
			StartCoroutine (checkCameraWellArrived (cms));
		} else {
			CameraMouvementsScript cms = oculus.transform.gameObject.AddComponent<CameraMouvementsScript> ();
			
			Quaternion targetInitialCamRot = targetCamera.transform.rotation; 
			Vector3 targetInitialCamPos = targetCamera.transform.position;
			
			cms.resetLook = targetInitialCamPos + targetCamera.transform.forward * 3;
			cms.pos = new Vector3[] {oculus.transform.position,targetInitialCamPos};
			cms.fadeOutEnable = false;
			cms.Start ();
			
			StartCoroutine (checkCameraWellArrived (cms));
			oculus.transform.parent = item.transform;
		}


	}

	private void autoSwichFunction(){

		if (!moving) {

			if (slider.value >= slider.maxValue) {
				if ((targetCamera.transform.position - oculus.transform.position).magnitude > 1)
				{
					moveCamera ();
					moving = true;

				}
				else
				{
					oculus.transform.position = targetCamera.transform.position;
					oculus.transform.rotation = targetCamera.transform.rotation;
				}
				slider.value = 0;

			} else {
				if (targetCamera == previousTargetCamera && targetCamera != null) {
				
					slider.value += item.name == "char_ethan_museum(Clone)" ? Time.deltaTime*1.5f:Time.deltaTime;
				} else {
					slider.value -= Time.deltaTime*2;
				}
			}
		}
		previousTargetCamera = noTarget? null:targetCamera;
	}



	private IEnumerator fadeBeforeSwich(){// Fonction qui lance le changement de caméra à la fin du fade.
		faderScript.BeginFade (1, 2);
		yield return new WaitForSeconds (0.3f);
		if (!occulusYOrN){
			oculus.transform.rotation = transformStock;
			setFollowedCamera (targetCamera);
			transformStock = oculus.transform.rotation;

		}else{
			oculus.transform.position = targetCamera.transform.position;
			oculus.transform.rotation = targetCamera.transform.rotation;
		}
		faderScript.BeginFade (-1, 1, 0.3f);
	}

	private IEnumerator checkCameraWellArrived(CameraMouvementsScript cms){
		while (!cms.getCameraMvtEnd()) {
			if (item.name == "char_ethan_museum(Clone)"){
				ethanHead = item.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
				cms.pos[cms.pos.Length - 1] = ethanHead.transform.position;
			}
			yield return new WaitForSeconds (0.01f);
		}
		//			yield return new WaitForSeconds (0.2f);
		faderScript.BeginFade (-1, 1);
		Destroy (cms);
		StartCoroutine(waitBeforeAutoSwichAgain());
	}

	private IEnumerator waitBeforeAutoSwichAgain(){
		yield return new WaitForSeconds (waitTime);
		moving = false;
	}


//	private void bridageCamera(){
//
//		if (item.name == "char_ethan_museum(Clone)" && current_camera.GetComponent<CameraMouvementsScript>() == null)
//		{
//			print ("Dans la tete");
//			ethanHead = item.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
//
//			if (Mathf.Abs(ethanHead.transform.localEulerAngles.x - current_camera.transform.localEulerAngles.x) > bride)
//			{
//				float tmp = current_camera.transform.localEulerAngles.x;
//				tmp = ethanHead.transform.localEulerAngles.x;
//				print ("stooooopt");
//			}
//
//		}
//	}



}
