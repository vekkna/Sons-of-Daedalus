using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class daedalusScript : MonoBehaviour
{
		public Text scoreText;
		public float liftSpeed;
		private float startingHeight;
		internal int score;
		internal bool canLift;
		private float startingGravity;
		public float gravityWhenBurned = 2f;
		public GameObject hitTheSun;
		private AudioSource[] audio;
		internal Animator animator;
		internal bool isLifting;
		GameObject g;
		GMScript gm;
		private int winningScore;
		internal bool hasWon;
		public GameObject collectCloud;
		private GameObject heaven;
		private GameObject[] players;
		private bool canCollect;
		private Transform tr;
		private float scrHeight;
		private Rigidbody2D rb2D;
		private sunScript sunText;

		void Start ()
		{
				sunText = GameObject.Find ("Sun").GetComponent<sunScript> ();
				rb2D = GetComponent<Rigidbody2D> ();
				scrHeight = Screen.height;
				tr = GetComponent<Transform> ();
				canCollect = true;
				players = GameObject.FindGameObjectsWithTag ("Player"); 
				heaven = GameObject.Find ("Heaven");
				startingHeight = transform.position.y;
				startingGravity = rigidbody2D.gravityScale;
				canLift = true;
				audio = GetComponents<AudioSource> ();
				animator = GetComponent<Animator> ();
	
				g = GameObject.Find ("GameManager");
				if (g) {
						gm = g.GetComponent<GMScript> ();
						winningScore = gm.winningScore;
				}
		}

		void FixedUpdate ()
		{
				if (isLifting && canLift && tr.position.y < scrHeight) {
						rb2D.AddForce (new Vector2 (0f, liftSpeed * Time.deltaTime));
						
				}	
				if (hasWon) {
						rb2D.gravityScale = 0f;
						tr.Translate ((heaven.transform.position - tr.position).normalized * 0.5f * Time.deltaTime);
						tr.localScale = Vector3.Lerp (tr.localScale, new Vector3 (3f, 3f, 1f), Time.deltaTime);
				}


		}

		void LateUpdate ()
		{
				if (!canCollect) {
						animator.SetInteger ("AnimState", 2);
				}
		}
    
		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "Mana") {
						if (canCollect) {
								manaScript m = other.GetComponent<manaScript> ();
								m.Fade ();
								PlayAudioClip (audio [0]);
								MakeSparkles ();
								StopAllCoroutines ();
								StartCoroutine (Score (m.value));
						}

				} else if (other.tag == "Sun") {
						if (canCollect) {
								//	sunText.activatePenaltyText ();
								canCollect = false;
								PlayAudioClip (audio [1]);
								StopAllCoroutines ();
								StartCoroutine (Score (0 - score));
								//	scoreText.text = score.ToString ();
								canLift = false;
								isLifting = false;
								rb2D.velocity = new Vector2 (0f, 0f);
								rb2D.gravityScale = gravityWhenBurned;
								Instantiate (hitTheSun, tr.position, tr.rotation);
								animator.SetInteger ("AnimState", 2);
						}

				} else if (other.tag == "Ground") {
	
						canCollect = true;
						canLift = true;
						rb2D.gravityScale = startingGravity;
						animator.SetInteger ("AnimState", 0);
				}
		}

		private void PlayAudioClip (AudioSource clip)
		{
				clip.Stop ();
				clip.Play ();
		}

		private IEnumerator Score (int points)
		{
				int oldScore = score;
				score += points;
				
				while (oldScore != score) {

						if (oldScore < score)
								oldScore++;
						else if (oldScore > score)
								oldScore--;

						scoreText.text = oldScore.ToString ();
						yield return new WaitForSeconds (0.2f);
				}

				if (score >= winningScore) {
						gm.GameOver ();
						Win ();
				}
		}

		public void MakeSparkles ()
		{
				Instantiate (collectCloud, tr.position, tr.rotation);
		}
    
		public void Win ()
		{
				hasWon = true;
				scoreText.color = new Color32 (243, 129, 69, 255);
				//	PlayAudioClip (audio [3]);
		}
    
}