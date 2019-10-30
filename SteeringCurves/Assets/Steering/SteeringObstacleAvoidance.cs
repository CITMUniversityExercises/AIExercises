using UnityEngine;
using System.Collections;

[System.Serializable]

// TODO 2: Agents must avoid any collider in their way
// 1- Create your own (serializable) class for rays and make a public array with it
public class OwnRay
{
    public float Length = 5.0f;
    public Vector3 Direction = Vector3.forward;
}

public class SteeringObstacleAvoidance : SteeringAbstract
{

    public LayerMask mask;
    public float avoid_distance = 5.0f;
    public OwnRay[] rays;

    Move move;
    SteeringSeek seek;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();
    }

    // Update is called once per frame
    void Update()
    {
        // 2- Calculate a quaternion with rotation based on movement vector
        Quaternion Movementrotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(move.current_velocity.x, move.current_velocity.z), Vector3.up);

        // 3- Cast all rays. If one hit, get away from that surface using the hitpoint and normal info
        for (int i = 0; i < rays.Length; ++i)
        {
            RaycastHit ray_hit;

            Vector3 origin = new Vector3(transform.position.x, 1.0f, transform.position.z);
            Vector3 destination = new Vector3();
            destination = Movementrotation * rays[i].Direction.normalized;
            int maxDistance = (int)rays[i].Length;

            // --- Check if the ray hits a collider ---
            if (Physics.Raycast(origin, destination, out ray_hit, maxDistance, mask))
            {
                // --- If we hit a collider, compute evasion vector  ---

                // origin
                Vector3 target = new Vector3(ray_hit.point.x, transform.position.y, ray_hit.point.z);

                // Direction: 
                target += (transform.forward - ray_hit.collider.transform.position).normalized * avoid_distance;
              
                // Move
                seek.Steer(target, priority);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (move && this.isActiveAndEnabled)
        {
            Gizmos.color = Color.red;
            float angle = Mathf.Atan2(move.current_velocity.x, move.current_velocity.z);
            Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            // TODO 2: Debug draw thoise rays (Look at Gizmos.DrawLine)
            // 4- Make sure there is debug draw for all rays (below in OnDrawGizmosSelected)
            for (int i = 0; i < rays.Length; ++i)
            {
                Gizmos.DrawLine(transform.position, transform.position + (q * rays[i].Direction.normalized) * rays[i].Length);
            }
        }
    }
}

