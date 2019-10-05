using UnityEngine;
using System.Collections;

public class SteeringArrive : MonoBehaviour {

	public float min_distance = 0.1f;
	public float slow_distance = 5.0f;
	public float time_to_accel = 0.1f;

	Move move;

	// Use this for initialization
	void Start () { 
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
		    Steer(move.target.transform.position);
	}

	public void Steer(Vector3 target)
	{
		if(!move)
			move = GetComponent<Move>();

        // TODO 3: Find the acceleration to achieve the desired velocity
        // If we are close enough to the target just stop moving and do nothing else
        // Calculate the desired acceleration using the velocity we want to achieve and the one we already have
        // Use time_to_target as the time to transition from the current velocity to the desired velocity
        // Clamp the desired acceleration and call move.AccelerateMovement()

        Vector3 distance = move.target.transform.position - move.transform.position;

        if (distance.magnitude < min_distance && move.movement.magnitude > Vector3.zero.magnitude)
        {
            move.SetMovementVelocity(Vector3.zero);
        }
        else
        {

            Vector3 velocity = ((move.target.transform.position - transform.position));
            velocity.Normalize();
            velocity *= move.max_mov_speed;

            if (distance.magnitude < slow_distance)
            {
                Vector3 unitary_dist = distance;
  
                float factor = unitary_dist.magnitude / slow_distance;
                

                velocity *= factor;
            }

            Vector3 actual = move.movement;

            Vector3 acceler = (velocity - actual) / time_to_accel;

            if(acceler.magnitude > move.max_mov_acceleration)
            {
                 acceler.Normalize();
                 acceler *= move.max_mov_acceleration;
            }


            move.AccelerateMovement(acceler);

        }

        //TODO 4: Add a slow factor to reach the target
        // Start slowing down when we get closer to the target
        // Calculate a slow factor (0 to 1 multiplier to desired velocity)
        // Once inside the slow radius, the further we are from it, the slower we go

    }

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, slow_distance);
	}
}
