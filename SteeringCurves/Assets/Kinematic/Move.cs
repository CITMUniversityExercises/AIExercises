using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour
{

	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_speed = 5.0f;
	public float max_mov_acceleration = 0.1f;
	public float max_rot_speed = 10.0f; // in degrees / second
	public float max_rot_acceleration = 0.1f; // in degrees

	[Header("-------- Read Only --------")]
	public Vector3 current_velocity = Vector3.zero;
	public float current_rotation_speed = 0.0f; // degrees

    Vector3[] movement_velocity;
    float[] rotation_speed;

    private void Start()
    {
        movement_velocity = new Vector3[5];
        movement_velocity.Initialize();

        rotation_speed = new float[5];
        rotation_speed.Initialize();
    }

    // Methods for behaviours to set / add velocities
    public void SetMovementVelocity (Vector3 velocity) 
	{
        velocity.y = 0.0f;

        for (int i = 0; i < movement_velocity.Length; ++i)
        {
            movement_velocity[i] = velocity;
        }
    }

	public void AccelerateMovement (Vector3 acceleration, int priority) 
	{
        acceleration.y = 0.0f;

        movement_velocity[priority] += acceleration;
    }

	public void SetRotationVelocity (float _rotation_speed) 
	{
        for (int i = 0; i < rotation_speed.Length; ++i)
        {
                rotation_speed[i] = _rotation_speed;          
        }
    }

	public void AccelerateRotation (float rotation_acceleration, int priority) 
	{
        rotation_speed[priority] += rotation_acceleration; 
    }
	
	// Update is called once per frame
	void Update () 
	{

        // --- Assign Velocity and Rotation according to priority lists ---
        for (int i = 0; i < movement_velocity.Length; ++i)
        {
            if (!Mathf.Approximately(movement_velocity[i].x, Vector3.zero.x)
                || !Mathf.Approximately(movement_velocity[i].y, Vector3.zero.y)
                || !Mathf.Approximately(movement_velocity[i].z, Vector3.zero.z))
            {
                current_velocity = movement_velocity[i];
                break;
            }
        }
        for (int i = 0; i < rotation_speed.Length; ++i)
        {
            if (!Mathf.Approximately(rotation_speed[i], 0.0f))
            {
                current_rotation_speed = rotation_speed[i];
                break;
            }
        }

        // cap velocity
  //      if (current_velocity.magnitude > max_mov_speed)
		//{
  //          current_velocity = current_velocity.normalized * max_mov_speed;
		//}

        // cap rotation
        current_rotation_speed = Mathf.Clamp(current_rotation_speed, -max_rot_speed, max_rot_speed);

		// rotate the arrow
		float angle = Mathf.Atan2(current_velocity.x, current_velocity.z);
		aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

		// strech it
		arrow.value = current_velocity.magnitude * 4;

		// final rotate
		transform.rotation *= Quaternion.AngleAxis(current_rotation_speed * Time.deltaTime, Vector3.up);

		// finally move
		transform.position += current_velocity * Time.deltaTime;

        // --- Reset velocities ---
        for (int i = 0; i < movement_velocity.Length; ++i)
        {
            movement_velocity[i] = Vector3.zero;
            rotation_speed[i] = 0.0f;
        }
	}
}
