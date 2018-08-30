using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissle : MonoBehaviour {
	public Transform target;
	private GameObject objTarget;
	public GameObject Explosion;
	private Rigidbody2D rb;
	public float speed =5f;
	public float rotateSpeed = 10f;
	private int ran;
	public GameObject lockOn;
	private GameObject obj;

	// Use this for initialization
	void Start () {
		
		ran=(int)Random.Range (0, GameObject.FindGameObjectsWithTag ("Brick").Length);
		objTarget = GameObject.FindGameObjectsWithTag ("Brick") [ran];
		target = objTarget.transform;
		obj = Instantiate (lockOn, objTarget.gameObject.transform.position, transform.rotation);



		rb = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (objTarget == null) {
			Destroy (obj);
		}
		if (objTarget != null) {
			Vector2 direction = (Vector2)target.position - rb.position;

			direction.Normalize ();

			float rotateamount = Vector3.Cross (direction, transform.up).z;
			rb.angularVelocity = -rotateamount * rotateSpeed;
			rb.velocity = transform.up * speed;
		} else {
			ran=(int)Random.Range (0, GameObject.FindGameObjectsWithTag ("Brick").Length);
			objTarget = GameObject.FindGameObjectsWithTag ("Brick") [ran];
			target = objTarget.transform;
			obj = Instantiate (lockOn, objTarget.gameObject.transform.position, transform.rotation);
		}


	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject == objTarget) {
			Instantiate (Explosion, col.gameObject.transform.position, transform.rotation);
			GameObject controller = GameObject.FindGameObjectsWithTag ("GameController") [0];
			controller.SendMessage ("addScore",15);
			controller.SendMessage ("playPointSound");
			DestroyObject (obj);
			Destroy (col.gameObject);
			Destroy (gameObject);

		}
	}
}
