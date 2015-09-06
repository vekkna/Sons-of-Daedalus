using UnityEngine;
using System.Collections;

public class manaScript : MonoBehaviour
{
		public int value;
		public float fadeRate = 1f;

		public void Fade ()
		{
				collider2D.enabled = false;
				StartCoroutine (FadeCR ());
		}

		private IEnumerator FadeCR ()
		{
				rigidbody2D.gravityScale = 0f;
				SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
				Color color = sprite.color;
				float red = color.r;
				float green = color.g;
				float blue = color.b;
				float alpha = color.a;
				while (alpha >= 0f) {
						alpha -= fadeRate * Time.deltaTime;
						sprite.color = new Color (red, green, blue, alpha);
						yield return null;
				}
				Destroy (gameObject);
		}
}