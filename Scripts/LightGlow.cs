using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGlow : MonoBehaviour {
	int degree;

	void Start () {
		InvokeRepeating ("Glow", 0.05f, 0.05f);
	}
	
	void Glow(){
		degree ++;
		GetComponent<Light> ().intensity = 4*Mathf.Sin(degree);
		if (degree == 360)
			degree = 0;
	}
}
