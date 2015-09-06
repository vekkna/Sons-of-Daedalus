using UnityEngine;
using System.Collections;

public class GenerateManaScript : MonoBehaviour
{

		public GameObject[] mana;
		private int counter;
		private int nextRelease;
		public int minimumWait;
		public int maximumWait;
		private Vector3 pos;
		private Quaternion ori;

		IEnumerator Start ()
		{
				pos = transform.position;
				ori = transform.rotation;
				nextRelease = Random.Range (minimumWait, maximumWait);
				while (true) {
						counter++;
						if (counter >= nextRelease) {
								counter = 0;
								nextRelease = Random.Range (minimumWait, maximumWait);
								int rand = Random.Range (0, 6);
								if (rand == 0 || rand == 1 || rand == 2)
										Instantiate (mana [0], pos, ori);
								else if (rand == 3 || rand == 4)
										Instantiate (mana [1], pos, ori);
								else
										Instantiate (mana [2], pos, ori);
			
						}
						yield return new WaitForSeconds (1f);
				}
		}


}
