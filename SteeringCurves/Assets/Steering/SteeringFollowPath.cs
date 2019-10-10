using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class SteeringFollowPath : MonoBehaviour {

	Move move;
	SteeringArrive arrive;
    public BGCcMath curve;

    public float ratio_increment = 0.1f;
    public float min_distance = 3.0f;
    float current_ratio = 0.0f;

    Vector3 closest_point;


	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		arrive = GetComponent<SteeringArrive>();

        // TODO 1: Calculate the closest point from the tank to the curve
        closest_point = curve.CalcPositionByClosestPoint(move.transform.position, out current_ratio);

        ratio_increment = move.max_mov_speed / curve.GetDistance();

        current_ratio /= curve.GetDistance();
        
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path

        Vector3 distance_to_target = closest_point - move.transform.position;
        //distance_to_target.Normalize();


        if (distance_to_target.magnitude < min_distance)
        {
            current_ratio += ratio_increment;
            closest_point = curve.CalcPositionByDistanceRatio(current_ratio);
        }
        if (current_ratio > 1)
            current_ratio = 0;

        arrive.Steer(closest_point);

	}

	void OnDrawGizmosSelected() 
	{

		if(isActiveAndEnabled)
		{
			// Display the explosion radius when selected
			Gizmos.color = Color.green;
			// Useful if you draw a sphere were on the closest point to the path
		}
        Gizmos.DrawSphere(closest_point, 3);
	}
}
