using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
	private bool ballActivation;
	private Vector3 ballPosition;
	private Vector2 ballInitialForce;
	public GameObject playerObject;
	public float speed;
	private bool stop;



	// Initialization
	void Start ()
	{
		//if player is null
		if (playerObject == null) {
			playerObject = GameObject.FindGameObjectsWithTag ("Player") [0];
		}
		//velocity calibration invoked to reduce lag
		InvokeRepeating ("calibrateVelocity", 0.04f, 0.04f);
		//Set end of game to flase
		stop = false;
		// create the init force
		ballInitialForce = new Vector2 (speed/2, speed/2) ;
		// set the ball's activation sate to inactive
		ballActivation = false;
		// set the ball's position
		ballPosition = transform.position;

	}

	// Update is called once per frame
	void Update ()
	{
		if (!stop) {

			// check for gameStart
			checkStart ();
			// if game has not started follow the paddle
			checkPaddle ();
			// check if the ball has been lost and if so reset 
			checkBounds ();
            // keep minimum angle
            calibrateVelocity();
            


        } else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
			GetComponent<Rigidbody2D> ().isKinematic = true;
		}

	}

	void checkBounds ()
	{
		if (ballActivation && transform.position.y < -4f) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
			GetComponent<Rigidbody2D> ().isKinematic = true;
			ballActivation = false;
			GameObject o = GameObject.FindGameObjectsWithTag ("Player") [0];
			o.SendMessage ("setBall",false);
			ballPosition.x = playerObject.transform.position.x;
			ballPosition.y = -2.895f;
			transform.position = ballPosition;
			GetComponent<Rigidbody2D> ().isKinematic = true;
			{
				GameObject o2 = GameObject.FindGameObjectsWithTag ("GameController") [0];
				o2.SendMessage ("addLives", -1);

			}

		}
	}

	void checkPaddle ()
	{
		if (!ballActivation && playerObject != null) {
			ballPosition.x = playerObject.transform.position.x;
			transform.position = ballPosition;
		}
	}

	void checkStart ()
	{
		if (Input.GetButtonDown ("Jump")) {
			if (!ballActivation) {
				// add a force

				GetComponent<Rigidbody2D> ().velocity = ballInitialForce;
				ballActivation = true;
				GetComponent<Rigidbody2D> ().isKinematic = false;

				StartCoroutine (delayOrder());
			}


		}
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		//this chunck is the logic that controls the balls's bounce vector from the paddle
		if (col.gameObject.name == playerObject.name) {
			float alpha = ((transform.position.x) - (col.transform.position.x)) / (col.collider.bounds.size.x - 0.3f);
			Vector2 dir = new Vector2 (alpha, 1).normalized;
			GetComponent<Rigidbody2D> ().velocity = dir * ((speed/2)+1);
		} 
	    if (col.gameObject.tag == "BlackHole")
        {
            transform.position = new Vector3(0,-5,-1.1f);
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
	IEnumerator delayOrder(){
		yield return new WaitForSecondsRealtime (0.25f);
		GameObject o = GameObject.FindGameObjectsWithTag ("Player") [0];
		o.SendMessage ("setBall",true);
	}

}
