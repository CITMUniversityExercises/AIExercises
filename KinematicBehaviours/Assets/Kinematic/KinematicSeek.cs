using UnityEngine;
using System.Collections;

public class KinematicSeek : MonoBehaviour {

    public GameObject target;
    Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 1: Set movement velocity to max speed in the direction of the target
        move.SetMovementVelocity((move.target.transform.position - transform.position) * move.max_mov_velocity);

        // Remember to enable this component in the inspector

	}
}
