using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Flash", 0.01f, 0.01f);
	}
	void Flash(){
		GetComponent<Light> ().intensity = Random.Range (1,3);
	}
}
