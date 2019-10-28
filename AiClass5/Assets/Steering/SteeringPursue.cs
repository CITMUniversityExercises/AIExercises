using UnityEngine;
using System.Collections;

public class SteeringPursue : MonoBehaviour {

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
        Move targetMove = move.target.GetComponent<Move>();
        if (targetMove)
        {
		    Steer(move.target.transform.position, targetMove.movement_vel, targetMove.max_mov_speed);
        }
	}

	public void Steer(Vector3 target, Vector3 velocity, float target_max_speed)
	{
		Vector3 diff = target - transform.position;
		float distance = diff.magnitude;
		float seconds_prediction = distance / target_max_speed;
        Vector3 prediction = target + velocity * seconds_prediction;
		arrive.Steer(prediction);
	}
}
