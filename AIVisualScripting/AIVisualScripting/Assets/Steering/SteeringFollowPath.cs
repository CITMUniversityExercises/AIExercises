using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class SteeringFollowPath : SteeringAbstract {

	public BGCcMath path;
	public float accuracy = 1.0f;

	float current_percentage = 0.0f;
	float distance_ratio = 0.1f;
	Move move;
	SteeringSeek seek;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();

		// TODO 1: Calculate the closest point in the range [0,1] from this gameobject to the path
		path.CalcPositionByClosestPoint(transform.position, out current_percentage);
		distance_ratio = move.max_mov_velocity / path.GetDistance();
		current_percentage /= path.GetDistance();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(path != null)
		{
			Vector3 target = Vector3.zero;

			// TODO 2: Check if the tank is close enough to the desired point
			// If so, create a new point further ahead in the path
			target = path.CalcPositionByDistanceRatio(current_percentage);

			//float path_len = path.length;
			float distance = (target - transform.position).magnitude;

			if(distance < accuracy)
			{
				current_percentage += distance_ratio;
				if(current_percentage > 1.0f)
					current_percentage -= 1.0f;
			}

			seek.Steer(target, priority);
		}
	}

	void OnDrawGizmosSelected() 
	{

		if(isActiveAndEnabled)
		{
			// Display the explosion radius when selected
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(path.CalcPositionByClosestPoint(transform.position), accuracy);
		}

	}
}
