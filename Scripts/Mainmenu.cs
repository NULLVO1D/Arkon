using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mainmenu : MonoBehaviour {


	public void startGame () {
		SceneManager.LoadScene (2);
	}
	
	public void startScores () {
		SceneManager.LoadScene (1);
	}
	public void quitGame () {
		Application.Quit ();
	}
}
