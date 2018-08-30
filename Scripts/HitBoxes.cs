using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxes : MonoBehaviour {
	public AudioClip wallHit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D col){
		GetComponent<AudioSource> ().clip = wallHit;
		GetComponent<AudioSource> ().Play ();
	}
}
