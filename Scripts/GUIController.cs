using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GUIController : MonoBehaviour
{
	public Text score;
	public Text hiScore;
	public Text lives;
	public Text level;
	public Text gameOver;
	private int iScore;
	private int iHiScore;
	private int iLives;
	private int iLevel;
	private float redBlockStep;
	public GameObject redBlock;
	private GameObject[] redBar = new GameObject[32];
	private float excessTime;
	public GameObject blueBlock;
	private GameObject[] blueBar = new GameObject[32];
	private int ammo;
	private GameObject[] yellowBar = new GameObject[32];
	public GameObject yellowBlock;
	public AudioClip pointAwarded;
	public AudioClip blockTooHard;
	public float time;
	private bool stop;
    private bool isBlackHole =false;
    private int blackHoleScore;

	void Start ()
	{
		//Declare starting values
		if (!File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			resetData ();
		}

		loadData ();
		stop = false;
		//Timer Functions
		redBlockStep = time / 32;
		drawTimer ();
		drawBlueBar ();
		InvokeRepeating ("Timer", 0.05f, 0.05f);
		InvokeRepeating ("refreshAmmo", 0.05f, 0.05f);
		//draws scores on start
		drawLevel ();
		string numberPadding = iHiScore.ToString ().PadLeft (6, '0');
		hiScore.text = "HiScore\n\n" + numberPadding;
		numberPadding = iScore.ToString ().PadLeft (6, '0');
		score.text = "Score\n\n" + numberPadding;
        //checks for blackholes
        StartCoroutine(displayWinCondition());
	}

	void Update ()
	{
		
		checkWin ();

		if (iScore > iHiScore) {
			setHiScore (iScore);
		}


		if (!stop) {
			if (time < 0 || iLives < 1) {
				gameEnd ();
			}

		}

	}

	//Mutators for onscreen effects

	public void addScore (int pScore)
	{
		iScore += pScore;
		string numberPadding = iScore.ToString ().PadLeft (6, '0');
		score.text = "Score\n\n" + numberPadding;
	}

	public void addLives (int pLives)
	{
		if (iLives < 5 && pLives == 1) {
			iLives++;
		}
		if (iLives >= 5 && pLives == 1) {
			addScore(200);
		}
		if (iLives <= 0 && pLives == -1) {
			iLives = 0;
		}
		if (iLives > 0 && pLives == -1) {
			iLives--;
		}

		lives.text = "Lives\n\n" + iLives;
	}
		

	//Audio Clips
	void playPointSound ()
	{
		GetComponent<AudioSource> ().clip = pointAwarded;
		GetComponent<AudioSource> ().Play ();
	}

	void playTooHard ()
	{
		GetComponent<AudioSource> ().clip = blockTooHard;
		GetComponent<AudioSource> ().Play ();
	}

	//converts remaining time into bonus time

	void convertRedToBlue ()
	{
		for (int i = (int)((time / redBlockStep)); i > -1; i--) {
			if (excessTime < 32) {
				excessTime += 0.25f;
			}
			if (i < 32) {
				Destroy (redBar [i]);
				drawBlueBar ();
			}
		}
	}
	
	//increments time from start
	void Timer ()
	{
		if (!stop) {
			time -= 0.05f;
			updateTimer ();
		}
	}
	//Draws the bonus time bar
	void drawBlueBar ()
	{
		for (int i = 0; i < (excessTime); i++) {
			blueBar [i] = Instantiate (blueBlock, transform, true);
			blueBar [i].transform.position = new Vector3 (-5.04f, -2.8f + (0.16f * i), -1.2f);


		}
	
	}
	//Draws the red time bar
	void drawTimer ()
	{
		
		for (int i = 0; i < ((time / redBlockStep)); i++) {
			redBar [i] = Instantiate (redBlock, transform, true);
			redBar [i].transform.position = new Vector3 (-4.24f, -2.8f + (0.16f * i), -1.2f);


		}
	}
	//updates the state of the time bar
	void updateTimer ()
	{
		int checkDigit = (int)((time / redBlockStep) + 1);
		if (checkDigit < redBar.Length && checkDigit >= 0) {
			
			Destroy (redBar [checkDigit]);
		}
	}

	//Pauses  the game when it ends due to gameOver
	public void gameEnd ()
	{
		
		GameObject player = GameObject.FindGameObjectsWithTag ("Player") [0];
		player.SendMessage ("setGameOver", true);
		GameObject ball = GameObject.FindGameObjectsWithTag ("Ball") [0];
		ball.SendMessage ("setGameOver", true);
		if (GameObject.FindGameObjectsWithTag ("Ball").Length == 2) {
			GameObject ball1 = GameObject.FindGameObjectsWithTag ("Ball") [1];
			ball1.SendMessage ("setGameOver", true);
			if (GameObject.FindGameObjectsWithTag ("Ball").Length == 3) {
				GameObject ball2 = GameObject.FindGameObjectsWithTag ("Ball") [2];

				ball2.SendMessage ("setGameOver", true);
			}
		}
		gameOver.text = "Game Over";
		stop = true;

		StartCoroutine (goToHiScores());
	}

	public void addAmmo (int ammu)
	{
		
		ammo += ammu;
		if (ammo > 32) {
			ammo = 32;
		}
		if (ammu > 0) {
			for (int i = 0; i < 32; i++) {
				Destroy (yellowBar [i]);
			}
			for (int i = 0; i < ammo; i++) {
				yellowBar [i] = Instantiate (yellowBlock, transform, true);
				yellowBar [i].transform.position = new Vector3 (-5.84f, -2.8f + (0.16f * i), -1.2f);


			}
		}
		if (ammu < 0) {
			
			Destroy (yellowBar [ammo]);
			
		}

	}

	void refreshAmmo ()
	{
		for (int i = 0; i < 32; i++) {
			Destroy (yellowBar [i]);
		}
		for (int i = 0; i < ammo; i++) {
			yellowBar [i] = Instantiate (yellowBlock, transform, true);
			yellowBar [i].transform.position = new Vector3 (-5.84f, -2.8f + (0.16f * i), -1.2f);

		}
	}

	public void setTime (float f)
	{
		time = f;
	}


	void setHiScore (int i)
	{
		iHiScore = i;
		string numberPadding = iHiScore.ToString ().PadLeft (6, '0');
		hiScore.text = "HiScore\n\n" + numberPadding;
	}

	public void drawLevel ()
	{
		string numberPadding = iLevel.ToString ().PadLeft (2, '0');
		level.text = "Level\n\n" + numberPadding;
	}

	public void checkWin ()
	{
        //Goes to next Scene
		if (GameObject.FindGameObjectsWithTag ("Brick").Length < 1) {
			if (SceneManager.sceneCount != SceneManager.GetActiveScene ().buildIndex+1) {
				iLevel++;
				convertRedToBlue ();
				saveData ();
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
			} else if (SceneManager.sceneCount == SceneManager.GetActiveScene ().buildIndex+1){
				saveData ();
				goToHiScores ();

			}

		}
        if (isBlackHole && iScore >= blackHoleScore)
        {
            if (SceneManager.sceneCount != SceneManager.GetActiveScene().buildIndex + 1)
            {
                iLevel++;
                convertRedToBlue();
                saveData();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if (SceneManager.sceneCount == SceneManager.GetActiveScene().buildIndex + 1)
            {
                saveData();
                goToHiScores();

            }
        }
	}
  
    
	//Saves all data
	public void saveData ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();

		data.score = iScore;
		data.hiScore = iHiScore;
		data.lives = iLives;
		data.level = iLevel;
		data.excessTime = excessTime;

		bf.Serialize (file, data);
		file.Close ();
	}
	//resets Data
	public void resetData ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();

		data.score = 0;
		data.hiScore = iHiScore;
		data.lives = 3;
		data.level = 1;
		data.excessTime = 0;

		bf.Serialize (file, data);
		file.Close ();
	}
	//Loads all data
	public void loadData ()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			iScore = data.score;
			iHiScore = data.hiScore;
			iLives = data.lives;
			iLevel = data.level;
			excessTime = data.excessTime;

		}
	}

	void OnApplicationQuit ()
	{
		resetData ();
	}
	IEnumerator goToHiScores(){
		yield return new WaitForSecondsRealtime (4f);
		saveData ();
		SceneManager.LoadScene (1);

	}
    IEnumerator displayWinCondition()
    {
        //checks for black hole
        
                    
            if (GameObject.FindGameObjectWithTag("BlackHole") != null)
            {
                gameOver.text = "Score 400 points to win";
                 yield return new WaitForSecondsRealtime(4f);
                     gameOver.text = "";
                         isBlackHole = true;
                            blackHoleScore = iScore + 400;
        }
        

        
    }
}

	

//container class for saveData;
[Serializable]
class PlayerData
{
	
	public int score;
	public int hiScore;
	public int lives;
	public int level;
	public float excessTime;

}