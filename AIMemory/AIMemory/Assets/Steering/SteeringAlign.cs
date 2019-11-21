using UnityEngine;
using System.Collections;

public class SteeringAlign : SteeringAbstract {

	public float min_angle = 0.01f;
	public float slow_angle = 0.1f;
	public float time_to_target = 0.1f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
		// Orientation we are trying to match
		float my_orientation = Mathf.Rad2Deg * Mathf.Atan2(transform.forward.x, transform.forward.z);
		float target_orientation = Mathf.Rad2Deg * Mathf.Atan2(move.target.transform.forward.x, move.target.transform.forward.z);
		float diff = Mathf.DeltaAngle(my_orientation, target_orientation); // wrap around PI

		float diff_absolute = Mathf.Abs(diff);

		if(diff_absolute < min_angle)
		{
			move.SetRotationVelocity(0.0f, priority);
			return;
		}

		float ideal_rotation = 0.0f;

		if(diff_absolute > slow_angle)
			ideal_rotation = move.max_rot_velocity;
		else
			ideal_rotation = move.max_rot_velocity * diff_absolute / slow_angle;

		float angular_acceleration = ideal_rotation / time_to_target;

		if(diff < 0)
			angular_acceleration = -angular_acceleration;

		move.AccelerateRotation(Mathf.Clamp(angular_acceleration, -move.max_rot_acceleration, move.max_rot_acceleration), priority);
	}
}
