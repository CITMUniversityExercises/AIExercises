using UnityEngine;
using System.Collections;

public class KinematicFaceMovement : SteeringAbstract {

	public float min_angle = 1.0f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float target_degrees = Mathf.Atan2(move.movement.x, move.movement.z) * Mathf.Rad2Deg;
		transform.localRotation = Quaternion.AngleAxis(target_degrees, Vector3.up);

		/* A more refined solution that uses velocity instead

		float current_degrees = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
		float delta = Mathf.DeltaAngle(target_degrees, current_degrees);

		if(Mathf.Abs(delta) < min_angle)
			move.SetRotationVelocity(0.0f);
		else
			move.SetRotationVelocity(-delta);
		*/
	}
}
