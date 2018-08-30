using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	public Transform ball;
	public Transform reciever;

	
    void teleport()
    {
        float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
        ball.Rotate(Vector3.forward,rotationDiff);
        ball.position = reciever.position;
    }
	
	void OnTriggerEnter2D(Collider2D other){

       
        StartCoroutine(pause());

    }

	
    public IEnumerator pause()
    {
        GameObject or = GameObject.FindGameObjectWithTag("OrangeportalEntrance");
        GameObject bl= GameObject.FindGameObjectWithTag("BlueportalEntrance");
        or.GetComponent<EdgeCollider2D>().enabled = false;
        bl.GetComponent<EdgeCollider2D>().enabled = false;
        teleport();
        yield return new WaitForSecondsRealtime(0.3f);
        or.GetComponent<EdgeCollider2D>().enabled = true;
        bl.GetComponent<EdgeCollider2D>().enabled = true;
    }
}
