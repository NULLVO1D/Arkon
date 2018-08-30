using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
	public float bSpeed;
	private float lockX;
	private Vector3 position;
	// Use this for initialization

	void Start () {
		position = gameObject.transform.position;

		GetComponent<Rigidbody2D> ().velocity = new Vector2(0,bSpeed);
		gameObject.transform.SetParent (GameObject.FindGameObjectsWithTag ("GameController") [0].transform);
	}

		
	void OnCollisionEnter2D(Collision2D col){
		Destroy (gameObject);
	}
}

