using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath : SteeringAbstract
{
    public GameObject target;
    Vector3 last_target_position;

    NavMeshAgent agent;
    NavMeshPath path;

    Vector3 waypoint;
    SteeringSeek seek;
    public float Min_distance = 1.0f;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
        seek = GetComponent<SteeringSeek>();
        last_target_position = target.transform.position;
        waypoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (last_target_position != target.transform.position)
        {
            agent.CalculatePath(target.transform.position, path);
            index = 0;
            waypoint = path.corners[index];
            
            last_target_position = target.transform.position;
        }


        if (Mathf.Abs((waypoint - transform.position).magnitude) > Min_distance)
            seek.Steer(waypoint);
        else
        {
            index++;

            if (index < path.corners.Length)
            waypoint = path.corners[index];
        }

    }
}
