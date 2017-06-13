using UnityEngine;
using System.Collections;

public class WallSlide : StickToWall {

	public float slideVelocity = -5f;
	public float slideMultiplier = 5f;
	public GameObject dustPrefab;
	public float dustSpawnDelay = .5f;

	private float timeElapsed = 0f;

	// Update is called once per frame
	override protected void Update () {
		base.Update ();

		if (onWallDetected && !collisionState.standing) {
			var velY = slideVelocity;

			if(inputState.GetButtonValue(inputButtons[0]))
				velY *= slideMultiplier;

			body2d.velocity = new Vector2(body2d.velocity.x, velY);

			if(timeElapsed > dustSpawnDelay){

				var dust = Instantiate(dustPrefab);
				var pos = transform.position;
				pos.y += 2;
				dust.transform.position = pos;
				dust.transform.localScale = transform.localScale;
				timeElapsed = 0;

			}

			timeElapsed += Time.deltaTime;
		}
	}

	override protected void OnStick(){
		body2d.velocity = Vector2.zero;
	}

	override protected void OffWall(){
		timeElapsed = 0;
	}
}
