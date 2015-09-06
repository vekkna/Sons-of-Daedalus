using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scoreTextScript : MonoBehaviour
{
		internal bool isWhite;
		public float timer;
		private float counter;
		private Color originalColor;
		public Color scoreColor;
		private Text text;

		void Start ()
		{
				isWhite = true;
				text = GetComponent<Text> ();
				originalColor = text.color;
				counter = timer;
		}

		void Update ()
		{
				if (!isWhite)
						counter -= Time.deltaTime;
				if (counter <= 0f)
						text.color = originalColor;
				counter = timer; 
				isWhite = true;
			
		}

		public void ChangeColor ()
		{
				text.color = scoreColor;
				isWhite = false;
		}
}