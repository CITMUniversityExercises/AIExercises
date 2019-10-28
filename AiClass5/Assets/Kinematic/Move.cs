using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_speed = 5.0f;
	public float max_mov_acceleration = 0.1f;
	public float max_rot_speed = 10.0f; // in degrees / second
	public float max_rot_acceleration = 0.1f; // in degrees

	[Header("-------- Read Only --------")]
	public Vector3 movement_vel = Vector3.zero;
	public float rotation = 0.0f; // degrees

	// Methods for behaviours to set / add velocities
	public void SetMovementVelocity (Vector3 velocity) 
	{
        movement_vel = velocity;
	}

	public void AccelerateMovement (Vector3 acceleration) 
	{
        movement_vel += acceleration;
	}

	public void SetRotationVelocity (float rotation_velocity) 
	{
		rotation = rotation_velocity;
	}

	public void AccelerateRotation (float rotation_acceleration) 
	{
		rotation += rotation_acceleration;
	}

	
	// Update is called once per frame
	void Update () 
	{
		// cap velocity
		if(movement_vel.magnitude > max_mov_speed)
		{
            movement_vel.Normalize();
            movement_vel *= max_mov_speed;
		}

		// cap rotation
		Mathf.Clamp(rotation, -max_rot_speed, max_rot_speed);

		// rotate the arrow
		float angle = Mathf.Atan2(movement_vel.x, movement_vel.z);
		aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

		// strech it
		arrow.value = movement_vel.magnitude * 4;

		// final rotate
		transform.rotation *= Quaternion.AngleAxis(rotation * Time.deltaTime, Vector3.up);

		// finally move
		transform.position += movement_vel * Time.deltaTime;
	}
}
