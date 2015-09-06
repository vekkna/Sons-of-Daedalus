using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;

public class GMScript : MonoBehaviour
{
		public int winningScore;
		private GameObject buttons;
		private GameObject cloudGenerator;
		public Text playerTwoScore;
		public Text victoryText;
		private AudioSource audio;
	
		void Awake ()
		{
				audio = GetComponent<AudioSource> ();
				buttons = GameObject.Find ("Buttons");
				cloudGenerator = GameObject.Find ("Cloud Generator");
				if (GameManagerScript.numberOfPlayers == 2) {
						GameObject.Find ("Angel2").SetActive (false);
						GameObject.Find ("button2").SetActive (false);
						playerTwoScore.text = "";
				}
		}

		void Start ()
		{
				GC.Collect ();
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						Application.LoadLevel (0); 
				}
		}
    
		public void GameOver ()
		{
				victoryText.text = "Freedom!";
				GameObject music = GameObject.Find ("Music Player");
				if (music)
						music.GetComponent<AudioSource> ().Play ();
				buttons.SetActive (false); 
				foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
						daedalusScript d = player.GetComponent<daedalusScript> ();
						d.canLift = false;
						d.isLifting = false;
				}
				GameObject.Find ("Cloud Generator").SetActive (false);
				GameObject sun = GameObject.Find ("Sun");
				sun.GetComponent<sunScript> ().gameOver = true;
				foreach (GameObject cloud in GameObject.FindGameObjectsWithTag ("Mana")) {
						cloud.SetActive (false);
				}

//				GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
//				int[] scores = (from player in players select player.GetComponent<daedalusScript> ().score).ToArray ();
//				bool scoresAreEqual = Array.TrueForAll (scores, s => s == scores [0]);
//				if (!scoresAreEqual) {
//						int winnerIndex = (from score in scores orderby score select Array.IndexOf (scores, score)).Last ();
//						GameObject winner = players [winnerIndex];
//						winner.GetComponent<daedalusScript> ().Win ();
//						victoryText.text = "Victory!";
//				} else {
//						victoryText.text = "Draw!";
//						audio.Play ();
//				}

						

				
		}

}
