using UnityEngine;
using System.Collections;

public class SteeringPursue : MonoBehaviour {

    public float max_seconds_prediction;

	Move move;
    SteeringSeek seek;
    SteeringArrive arrive;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();
        arrive = GetComponent<SteeringArrive>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Steer(move.target.transform.position, move.target.GetComponent<Move>().movement, move.target.GetComponent<Move>().max_mov_speed);
	}

	public void Steer(Vector3 target, Vector3 target_velocity, float max_target_speed)
	{
        // TODO 5: Create a fake position to represent
        // enemies predicted movement. Then call Steer()
        // on our Steering Seek / Arrive with the predicted position in
        // max_seconds_prediction time
        // Be sure that arrive / seek's update is not called at the same time

        Vector3 distance = target - move.transform.position;

        float factor = distance.magnitude/move.movement.magnitude;
        max_seconds_prediction *= factor;

        Vector3 future_position = (target + target_velocity * max_seconds_prediction);

        max_seconds_prediction /= factor;

        arrive.Steer(future_position);


        // TODO 6: Improve the prediction based on the distance from
        // our target and the speed we have

    }
}
