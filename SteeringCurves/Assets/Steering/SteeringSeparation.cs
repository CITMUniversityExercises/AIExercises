using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SteeringSeparation : SteeringAbstract
{

    public LayerMask mask;
    public float search_radius = 5.0f;
    public AnimationCurve strength;
    private List<Collider> Colliders;

    Move move;

    // Use this for initialization
    void Start()
    {
        Colliders = new List<Collider>();
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        // --- Fill Colliders list with all agents of the given mask inside the sphere ---
        Colliders.AddRange(Physics.OverlapSphere(transform.position, search_radius, mask));
        Vector3 Desired_acceleration = Vector3.zero;

        // --- Iterate list and compute separation vectors ---
        for (int i = 0; i < Colliders.Count; ++i)
        {
            GameObject GO = Colliders[i].gameObject;

            // --- Do not evaluate ourselves ---
            if (GO == gameObject)
                continue;

            // --- Distance between us and another object ---
            Vector3 distance = transform.position - GO.transform.position;

            // --- Find Separation Acceleration strength using the curve ---
            float Acceleration = (1.0f - strength.Evaluate(distance.magnitude / search_radius)) * move.max_mov_acceleration;

            // --- Sum all separation vectors to have a final acceleration ---
            Desired_acceleration += distance.normalized * Acceleration;
        }
        // --- 
        if (Desired_acceleration.magnitude > 0.0f)
        {
                move.AccelerateMovement(Desired_acceleration, priority);        
        }

        Colliders.Clear();
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, search_radius);
    }

}
