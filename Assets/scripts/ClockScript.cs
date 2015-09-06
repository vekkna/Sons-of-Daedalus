using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ClockScript : MonoBehaviour
{
	
		public float interval = 1f;
		private Text text;
		public int startTime, endTime, increment = 1;
		public enum dir
		{
				ascending,
				descending}
		;
		public dir direction;
		public Color nearEndColor = Color.red;
		public bool warnOfEnding = true;
		public int finalSeconds = 10;
		public float flickerRate = 0.5f;
	
		void Start ()
		{
				text = GetComponent<Text> ();
				text.text = FormatTime (startTime);
				StartCoroutine (Timer (startTime));
		}
	
		private IEnumerator Timer (int startTime)
		{
				while (startTime != endTime) {
						yield return new WaitForSeconds (interval);
						text.text = FormatTime (startTime);
						startTime = (direction == dir.ascending) ? startTime + increment : startTime - increment;
						if (warnOfEnding && Mathf.Abs (startTime - endTime) <= finalSeconds) {
								text.color = nearEndColor;
								StartCoroutine (Flicker (flickerRate));
						}
				}
				EndTimer ();
		}
	
		private string FormatTime (int secs)
		{
				TimeSpan t = TimeSpan.FromSeconds (secs);
				return string.Format ("{0:D2}:{1:D2}",
		                     t.Minutes, 
		                     t.Seconds);
		}
	
		private IEnumerator Flicker (float rate)
		{
				while (startTime != endTime) {
						yield return new WaitForSeconds (rate);
						text.color = new Color (nearEndColor.r, nearEndColor.g, nearEndColor.b, 0f);
						yield return new WaitForSeconds (rate);
						text.color = new Color (nearEndColor.r, nearEndColor.g, nearEndColor.b, 1f);
				}
		}
	
		private void EndTimer ()
		{
				GameObject.Find ("GameManager").GetComponent<GMScript> ().GameOver ();
				text.text = "";
	
		}
}