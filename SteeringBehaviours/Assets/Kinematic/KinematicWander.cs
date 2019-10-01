using UnityEngine;
using System.Collections;

public class KinematicWander : MonoBehaviour {

	public float max_angle = 0.5f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
        // TODO 9: Generate a velocity vector in a random rotation (use RandomBinominal) and some attenuation factor
        float angle = 0;
        Vector3 axis = Vector3.zero;
        Random.rotation.ToAngleAxis(out angle, out axis);

        //Vector3 direction = Mathf.Tan(angle);

        //move.SetMovementVelocity();
	}
}
