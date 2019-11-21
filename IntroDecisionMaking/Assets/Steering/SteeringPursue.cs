using UnityEngine;
using System.Collections;

public class SteeringPursue : SteeringAbstract {

	public float max_prediction;

	Move move;
	SteeringArrive arrive;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		arrive = GetComponent<SteeringArrive>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Steer(move.target.transform.position, move.target.GetComponent<Move>().movement);
	}

	public void Steer(Vector3 target, Vector3 velocity)
	{
		Vector3 diff = target - transform.position;
		float distance = diff.magnitude;
		float my_speed = move.movement.magnitude;
		float prediction;

		// is the speed too small ?
		if(my_speed < distance / max_prediction)
			prediction = max_prediction;
		else
			prediction = distance / my_speed;

		arrive.Steer(target + (velocity * prediction), priority);
	}
}
