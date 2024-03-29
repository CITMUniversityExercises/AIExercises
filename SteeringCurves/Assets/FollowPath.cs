﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath : SteeringAbstract
{
    public GameObject target;
    Vector3 last_target_position;

    NavMeshPath path;

    Vector3 waypoint;
    SteeringSeek seek;
    public float Min_distance = 5.0f;
    int index = 1;

    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        seek = GetComponent<SteeringSeek>();
        last_target_position = target.transform.position;
        waypoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (last_target_position != target.transform.position)
        {
            NavMesh.CalculatePath(transform.position,target.transform.position, NavMesh.AllAreas,path);

            if (path.corners.Length != 0)
            {
                index = 1;
                waypoint = path.corners[index];
            }
            
            last_target_position = target.transform.position;
        }


        if (path != null && path.corners.Length != 0)
        {
            if (path.corners.Length > 2)
            {
                if(Mathf.Abs((transform.position - waypoint).magnitude) < Min_distance)
                {
                    index++;

                    if (index < path.corners.Length)
                        waypoint = path.corners[index];
                }

            }

            seek.Steer(waypoint, priority);
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(waypoint, 1);
    }
}
