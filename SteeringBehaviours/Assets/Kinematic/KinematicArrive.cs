using UnityEngine;
using System.Collections;

public class KinematicArrive : MonoBehaviour {

	public float min_distance = 0.1f;
	public float time_to_target = 0.75f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
        // TODO 8: calculate the distance. If we are in min_distance radius, we stop moving
        // Otherwise devide the result by time_to_target (0.25 feels good)
        // Then call move.SetMovementVelocity()

        float distance = move.target.transform.position.magnitude - move.transform.position.magnitude;

        if (distance < min_distance && move.mov_velocity.magnitude > Vector3.zero.magnitude)
        {
            move.SetMovementVelocity(Vector3.zero);
        }
        else
        {
            move.SetMovementVelocity((move.target.transform.position - transform.position) * move.max_mov_velocity * time_to_target);
        }

    }

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);
	}
}
