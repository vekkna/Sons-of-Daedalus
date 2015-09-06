using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sunScript : MonoBehaviour
{
		private Vector3 originalSize, startSize, nextSize;
		public float minimumSize, maximumSize, minRate, maxRate, neededDifference;
		private float rate;
		internal bool gameOver;
		private Vector3[] sizes;
		private float[] rates;
		private int currentStep;
		int stepsInPattern;
		public Text penaltyText;
		private bool penaltyTextOn;
		public float penaltyTextDuration = 2f;
		private float penaltyTextCounter;
		public string penaltyTextString;
	
		void Start ()
		{
				currentStep = 0;
				originalSize = transform.localScale;
				stepsInPattern = Random.Range (10, 13);
				sizes = new Vector3[stepsInPattern];
				rates = new float[stepsInPattern];

				//sizes [0] = originalSize;
				for (int index = 0; index < stepsInPattern; index++) {
						//	while (sizes[index ].x - sizes[index - 1].x < neededDifference) {
						sizes [index] = FindNewSize ();
						//	}
						rates [index] = FindNextRate ();
				}
		}

//		void Update ()
//		{
//				if (penaltyTextOn) {
//						penaltyTextCounter += Time.deltaTime;
//						if (penaltyTextCounter >= penaltyTextDuration) {
//								penaltyText.text = "";
//								penaltyTextCounter = 0f;
//								penaltyTextOn = false;
//						}
//				}
//		}

		void FixedUpdate ()
		{
				if (Mathf.Abs (transform.localScale.x - sizes [currentStep].x) >= 0.1f) {
						{
								transform.localScale = Vector3.Lerp (transform.localScale, sizes [currentStep], rates [currentStep] * Time.deltaTime);
						}
				} else {
						currentStep++;
						if (currentStep >= stepsInPattern) {
								currentStep = 0;
						}
				}
				if (gameOver) {
						transform.Translate (new Vector3 (0f, 1f * Time.deltaTime, 0f));
						collider2D.enabled = false;
				}
		}
		int count = 0;
		private Vector3 FindNewSize ()
		{

				float previous = transform.localScale.x / originalSize.x;
				float next = previous;
				while (Mathf.Abs(next - previous) <= neededDifference) {
						count++;
						next = Random.Range (minimumSize, maximumSize);
						//Debug.Log (next + ";" + previous);
				}
				return originalSize * next;
				//	return originalSize * Random.Range (minimumSize, maximumSize);
		}

		private float FindNextRate ()
		{
				return Random.Range (minRate, maxRate);
		}

		public void activatePenaltyText ()
		{
				penaltyTextOn = true;
				penaltyText.text = penaltyTextString;
		}
}