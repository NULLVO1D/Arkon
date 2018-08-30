using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
	public GameObject shine;
	public int hitsToKill;
	public int points;
	private int hitCounter;
	public GameObject blasterBox;
	public GameObject rocketBox;
	public GameObject extendBox;
	public GameObject ballBox;
	public GameObject oneUp;

	// Use this for initialization
	void Start ()
	{
		hitCounter = 0;
	}


	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Ball" || col.gameObject.tag == "bullet") {
			GameObject controller = GameObject.FindGameObjectsWithTag ("GameController") [0];
			hitCounter++;
			if (hitCounter >= hitsToKill) {
				//tells 'controller' to update the score
				controller.SendMessage ("addScore", points);
				//'controller' plays the deathSound 
				controller.SendMessage ("playPointSound");
				//chance for pickup
				int rar = (int)Random.Range (-1, 100);
				if (rar > 85) {
					int drop = (int)Random.Range (0, 6);	
					switch (drop) {
					case 1:
						{
							GameObject box = Instantiate (blasterBox, transform, true);
							box.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
							box.transform.SetParent (GameObject.FindGameObjectsWithTag ("GameController") [0].transform);
							break;

						}
					case 2:
						{
							GameObject box = Instantiate (rocketBox, transform, true);
							box.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
							box.transform.SetParent (GameObject.FindGameObjectsWithTag ("GameController") [0].transform);
							break;
						}
					case 3:
						{
							GameObject box = Instantiate (extendBox, transform, true);
							box.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
							box.transform.SetParent (GameObject.FindGameObjectsWithTag ("GameController") [0].transform);
							break;
						}
					case 4:
						{
							GameObject box = Instantiate (ballBox, transform, true);
							box.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
							box.transform.SetParent (GameObject.FindGameObjectsWithTag ("GameController") [0].transform);
							break;
						}
					case 5:
						{
							GameObject box = Instantiate (oneUp, transform, true);
							box.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
							box.transform.SetParent (GameObject.FindGameObjectsWithTag ("GameController") [0].transform);
							break;
						}

					} 
				}
				Destroy (this.gameObject);
			} else {
				GameObject ts = Instantiate (shine, transform.position, transform.rotation);
				//'controller' plays the hardblock Sound
				controller.SendMessage ("playTooHard");
			}
		}

	}

}
