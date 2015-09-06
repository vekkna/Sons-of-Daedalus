using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{

		public daedalusScript daedalus;
		private float x, y, h, w;
		private Camera cam;

		void Start ()
		{
				x = transform.position.x;
				y = transform.position.y;
				h = transform.localScale.y;
				w = transform.localScale.x;
				cam = Camera.main;
		}

		void Update ()
		{
				if (Input.touchCount > 0) {

						for (int i = 0; i<Input.touchCount; i++) {

								Vector3 wp = cam.ScreenToWorldPoint (Input.GetTouch (i).position);
								Vector2 touchPos = new Vector2 (wp.x, wp.y);
	
								if (touchPos.x > x - w / 2 && touchPos.x < x + w / 2
				    &&
										touchPos.y > y - h / 2 && touchPos.y < y + h / 2) {
                    
										if (Input.GetTouch (i).phase == TouchPhase.Began) {
												if (daedalus.canLift)
														daedalus.animator.SetInteger ("AnimState", 1);
										} else if (Input.GetTouch (i).phase == TouchPhase.Ended) {									
												daedalus.animator.SetInteger ("AnimState", 3);												
												daedalus.isLifting = false;
										} else {
												daedalus.isLifting = true;
										}
								}
						}
				}

				if (Input.GetKey (KeyCode.Space)) {
						daedalus.isLifting = true;
				}
		}
}
