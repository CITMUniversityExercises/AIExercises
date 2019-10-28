using UnityEngine;
using System.Collections;

public class SteeringSeparation : MonoBehaviour {

	public LayerMask mask;
	public float search_radius = 5.0f;
	public AnimationCurve strength;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 1: Agents much separate from each other:
        // 1- Find other agents in the vicinity (use a layer for all agents)
        Collider[] Colliders = Physics.OverlapSphere(transform.position, search_radius, mask);
        // 2- For each of them calculate a escape vector using the AnimationCurve

        Vector3 Desired_acceleration = Vector3.zero;

        for(int i =0 ; i < Colliders.Length; ++i)
        {
            GameObject GO = Colliders[i].gameObject;

            // --- Do not evaluate ourselves ---
            if (GO == gameObject)
                continue;

            // --- Distance between us and another object ---
            Vector3 distance = transform.position - GO.transform.position;

            // --- Find Separation Acceleration strength using the curve ---
            float Acceleration = (1.0f - strength.Evaluate(distance.magnitude / search_radius)) * move.max_mov_acceleration;
           
            // 3- Sum up all vectors and trim down to maximum acceleration
            Desired_acceleration += distance.normalized * Acceleration;
        }
        // 3- Trim down to maximum acceleration
        if (Desired_acceleration.magnitude > 0.0f)
        {
            if (Desired_acceleration.magnitude > move.max_mov_acceleration)
                Desired_acceleration = Desired_acceleration.normalized * move.max_mov_acceleration;

            move.AccelerateMovement(Desired_acceleration);
        }
    }

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, search_radius);
	}
}
