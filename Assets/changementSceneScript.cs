using UnityEngine;
using System.Collections;

public class changementSceneScript : MonoBehaviour {

	private FaderScript faderScript;
	public bool withIntro;
	// Use this for initialization

	void Awake(){
		DontDestroyOnLoad (this);
	}
	void Start () {
		faderScript = this.gameObject.GetComponent<FaderScript>();
		withIntro = GameObject.Find ("Main Camera").GetComponent<CameraMouvementsScript> ().withIntro;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.F1)) {
			withIntro = true;
			StartCoroutine (ChangementScene ("MonZombieEtMoi"));
		} else if (Input.GetKeyDown (KeyCode.F2)) {
			withIntro = true;
			StartCoroutine (ChangementScene ("MonZombieEtMoi_NoTextures"));
		} else if (Input.GetKeyDown (KeyCode.F3)) {
			withIntro = false;
			StartCoroutine (ChangementScene ("Musee"));
		} else if (Input.GetKeyDown (KeyCode.F4)) {
			withIntro = false;
			StartCoroutine (ChangementScene ("MonZombieEtMoi"));
		} else if (Input.GetKeyDown (KeyCode.F5)) {
			withIntro = false;
			StartCoroutine (ChangementScene ("MonZombieEtMoi_NoTextures"));
		}
	}

	private IEnumerator ChangementScene(string newScene){
		faderScript.BeginFade(1);
		while (!faderScript.isFaded()) {
			yield return new WaitForSeconds (0.5f);
		}
			Application.LoadLevel (newScene);
	}
}