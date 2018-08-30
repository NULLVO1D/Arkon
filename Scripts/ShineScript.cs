using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine (Timer ());
	}
	IEnumerator Timer(){
		yield return new WaitForSeconds (0.2f);
		Destroy (gameObject);
	}
}
