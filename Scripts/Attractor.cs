using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

	public Rigidbody2D rb;
	const float G = 6.674f;
	void FixedUpdate(){
		
		Attractor[] attractors = FindObjectsOfType<Attractor>();
		foreach (Attractor attractor in attractors) {
			if(attractor != this)
			Attract (attractor);
		}
	}
	void Attract(Attractor objOther){

		Rigidbody2D rbOther = objOther.rb;
		Vector2 direction = rb.position - rbOther.position;
		float distance = direction.magnitude;
		float forceMagnitude = G*(rb.mass*rbOther.mass)/Mathf.Pow(distance,2);
		Vector2 force = direction.normalized * forceMagnitude;
		rbOther.AddForce (force);
	}

}
