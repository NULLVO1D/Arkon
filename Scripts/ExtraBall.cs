using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBall : MonoBehaviour {

	private Vector2 ballInitialForce;
	public float speed;
	private bool stop;

	// Initialization
	void Start ()
	{
		
		//velocity calibration invoked to reduce lag
		InvokeRepeating ("calibrateVelocity", 0.04f, 0.04f);
		//Set end of game to flase
		stop = false;
		// create the init force
		ballInitialForce = new Vector2 (Random.Range(1,5)*speed/2,Random.Range(1,5)* speed/2) ;
        //apply the force
        GetComponent<Rigidbody2D>().velocity = ballInitialForce;
	    

	}

	// Update is called once per frame
	void Update ()
	{
		if (!stop) {

			// check if the ball has been lost and if so reset 
			checkBounds ();

		} else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
			GetComponent<Rigidbody2D> ().isKinematic = true;
		}

	}

	void checkBounds ()
	{
		if (transform.position.y < -4f) {
			
			Destroy (this.gameObject);
					
		}
	}




	void OnCollisionEnter2D (Collision2D col)
	{
		//this chunck is the logic that controls the balls's bounce vector from the paddle
		if (col.gameObject.tag == "Player") {
			float alpha = ((transform.position.x) - (col.transform.position.x)) / (col.collider.bounds.size.x - 0.3f);
			Vector2 dir = new Vector2 (alpha, 1).normalized;
			GetComponent<Rigidbody2D> ().velocity = dir * ((speed/2)+1);
		} 


	}

	void calibrateVelocity ()
	{

		if (GetComponent<Rigidbody2D> ().velocity.x>speed*0.7&&GetComponent<Rigidbody2D> ().velocity.y>0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed*0.7f,speed*0.3f);
		}
		if (GetComponent<Rigidbody2D> ().velocity.x>speed*0.7&&GetComponent<Rigidbody2D> ().velocity.y<0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed*0.7f,-speed*0.3f);
		}
		if (GetComponent<Rigidbody2D> ().velocity.x<speed*-0.7&&GetComponent<Rigidbody2D> ().velocity.y>0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed*0.7f,speed*0.3f);
		}
		if (GetComponent<Rigidbody2D> ().velocity.x<speed*-0.7&&GetComponent<Rigidbody2D> ().velocity.y<0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed*0.7f,-speed*0.3f);
		}

		if (GetComponent<Rigidbody2D> ().velocity.y>speed*0.7&&GetComponent<Rigidbody2D> ().velocity.x>0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed*0.3f,speed*0.7f);
		}
		if (GetComponent<Rigidbody2D> ().velocity.y>speed*0.7&&GetComponent<Rigidbody2D> ().velocity.x<0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed*0.3f,speed*0.7f);
		}
		if (GetComponent<Rigidbody2D> ().velocity.y<speed*-0.7&&GetComponent<Rigidbody2D> ().velocity.x>0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed*0.3f,-speed*0.7f);
		}
		if (GetComponent<Rigidbody2D> ().velocity.y<speed*-0.7&&GetComponent<Rigidbody2D> ().velocity.x<0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed*0.3f,-speed*0.7f);
		}

		if(GetComponent<Rigidbody2D> ().velocity.x==0&&GetComponent<Rigidbody2D> ().velocity.y<0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed/2,-speed/2);
		}
		if(GetComponent<Rigidbody2D> ().velocity.x==0&&GetComponent<Rigidbody2D> ().velocity.y>0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed/2,speed/2);
		}
		if(GetComponent<Rigidbody2D> ().velocity.y==0&&GetComponent<Rigidbody2D> ().velocity.x>0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed/2,speed/2);
		}
		if(GetComponent<Rigidbody2D> ().velocity.y==0&&GetComponent<Rigidbody2D> ().velocity.x<0){
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-speed/2,speed/2);
		}

	}

	public void setGameOver ()
	{
		stop = true;
	}

}
