using UnityEngine;
using System.Collections;

public class WelcomeLevelScript : MonoBehaviour
{

		void Update ()
		{
				if (Input.GetKey (KeyCode.Escape))
						Application.Quit ();
		}

	
		public void StartGame (int numPlayers)
		{
				GameManagerScript.numberOfPlayers = numPlayers;
				Application.LoadLevel (1);
//				GameObject angel = GameObject.Find ("Angel1");
//				daedalusScript d = angel.GetComponent<daedalusScript> ();
//				d.animator.SetInteger ("AnimState", 1);
		}
}
