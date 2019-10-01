using UnityEngine;
using System.Collections;

public class KinematicFaceMovement : MonoBehaviour {

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () {
        // TODO 7: rotate the whole tank to look in the movement direction
        float angle = Mathf.Atan2((float)move.mov_velocity.x, (float)move.mov_velocity.z);
        move.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);


        // Extremnely similar to TODO 2
    }
}
