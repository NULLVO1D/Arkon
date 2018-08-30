using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;

public class HiscoreTable : MonoBehaviour {

	private String[] names = new String[10];
	private int[] scores= new int[10];
	private bool winner =false;
	private int pos;
	public Text Board;
	private int loadedScore;

	void Start () {
		//if file Doesnt't Exist creates a fresh one
		if (!File.Exists (Application.persistentDataPath + "/biff.dat")) {
			resetBiggie ();

            Cursor.visible = true;
        }

		loadBiggie ();
		loadGamedata ();

		for (int i=0; i<10; i++) {
			
			if (loadedScore >=scores[i]) {
				
				if (loadedScore > 0) {
					winner = true;
					if (loadedScore == scores [i]) {
						pos = i + 1;
						break;
					} else {
						pos = i;
						break;
					}
				}
			}
		}
		if (!winner) {
			Destroy (GameObject.FindGameObjectsWithTag("Input")[0]);
			displayScores ();
            resetData();
            
		}
	}

	public void clicked(String s){
		
		addPlayerScore (s,loadedScore,pos);
		displayScores ();
		Destroy (GameObject.FindGameObjectsWithTag("Input")[0]);
		saveBiggie ();
        resetData();
    }
	public void addPlayerScore(String s, int i, int position){
		if (scores [position] == 0) {
			names [position] = s;
			scores [position] = i;
		} else {
			for (int j = 8; j > position - 1; j--) {
				names [j + 1] = names [j];
			}
			for (int j = 8; j > position - 1; j--) {
				scores [j + 1] = scores [j];
			}

			names [position] = s;
			scores [position] = i;
		}

	}


	//this is the data from the last playthru
	private void loadGamedata ()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			loadedScore = data.score;

		}
	}
	//resets the playthru's Data
	public void resetData ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();

		data.score = 0;
		data.hiScore = scores[0];
		data.lives = 3;
		data.level = 1;
		data.excessTime = 0;

		bf.Serialize (file, data);
		file.Close ();
	}
	//Biggie is the filename that saves the highscores
	private void saveBiggie ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/biff.dat");
		biggieData data = new biggieData();

		data.setPlayerScore (scores)  ;
		data.setPlayerName (names)  ;

		bf.Serialize (file, data);
		file.Close ();
	}
	//Biggie is the filename that saves the highscores
	private void resetBiggie ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/biff.dat");
		biggieData data = new biggieData();
		String[] n=new string[10];
		int[] s= new int[10];
		for(int i=0;i<10;i++){
			n [i] = " ";
			s[i]=0;
		}
		data.setPlayerScore (s)  ;
		data.setPlayerName (n)  ;
		bf.Serialize (file, data);
		file.Close ();
	}
	//Biggie is the filename that saves the highscores
	private void loadBiggie ()
	{
		if (File.Exists (Application.persistentDataPath + "/biff.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/biff.dat", FileMode.Open);
			biggieData data = (biggieData)bf.Deserialize (file);
			file.Close ();

			data.getPlayerScore().CopyTo(scores,0);
			data.getPlayerName().CopyTo(names,0);

		}
	}
	private void displayScores(){
		Board.text =
			"\n01- " + names [0].ToString ().PadRight (16, '.') + ".............." + scores [0].ToString ().PadLeft (7, '0') +
			"\n02- " + names [1].ToString ().PadRight (16, '.') + ".............." + scores [1].ToString ().PadLeft (7, '0') +
			"\n03- " + names [2].ToString ().PadRight (16, '.') + ".............." + scores [2].ToString ().PadLeft (7, '0') +
			"\n04- " + names [3].ToString ().PadRight (16, '.') + ".............." + scores [3].ToString ().PadLeft (7, '0') +
			"\n05- " + names [4].ToString ().PadRight (16, '.') + ".............." + scores [4].ToString ().PadLeft (7, '0') +
			"\n06- " + names [5].ToString ().PadRight (16, '.') + ".............." + scores [5].ToString ().PadLeft (7, '0') +
			"\n07- " + names [6].ToString ().PadRight (16, '.') + ".............." + scores [6].ToString ().PadLeft (7, '0') +
			"\n08- " + names [7].ToString ().PadRight (16, '.') + ".............." + scores [7].ToString ().PadLeft (7, '0') +
			"\n09- " + names [8].ToString ().PadRight (16, '.') + ".............." + scores [8].ToString ().PadLeft (7, '0') +
			"\n10- " + names [9].ToString ().PadRight (16, '.') + ".............." + scores [9].ToString ().PadLeft (7, '0');
	}
	public void restart(){
		resetData ();
		SceneManager.LoadScene (0);

	}
	void OnApplicationQuit ()
	{
		resetData ();
	}
}
[Serializable]
class biggieData{
	private String[] name= new string[10];
	private int[] score= new  int[10];

	public int[] getPlayerScore(){
		return score;
	}
	public String[] getPlayerName(){
		return name;
	}
	public void setPlayerScore(int[] p){
		p.CopyTo(score,0);
	}
	public void setPlayerName(String[] p){
		p.CopyTo(name,0);
	}
}
