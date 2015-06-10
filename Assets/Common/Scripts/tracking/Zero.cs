using UnityEngine;
using System.Collections;

public class Zero : MonoBehaviour {

	public Quaternion offset;
	public GameObject refBone;

	// Use this for initialization
	void Start () {
		refBone = GameObject.Find("ZERO_" + this.name);
		//zeroQuat = this.gameObject.transform.rotation;
		offset = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			//zeroQuat = Quaternion.identity;
			//zeroQuat = zeroBone.transform.rotation;
			//zeroQuat = zeroBone.transform.rotation) * this.gameObject.transform.rotation;
				
			//StartCoroutine(ABC());

		}
	}

	IEnumerator ABC()
	{
		
		//returning 0 will make it wait 1 frame
		yield return new WaitForSeconds(0.5f);
		
		//code goes here
		offset = this.gameObject.transform.rotation;
		
	}
}
