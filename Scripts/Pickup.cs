using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2(0,-1);

	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
