using UnityEngine;
using System.Collections;

public class SteeringSeek : SteeringAbstract
{
    Move move;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        Steer(move.target.transform.position, priority);
    }

    public void Steer(Vector3 target, int given_priority)
    {
        if (move == null)
        {
            move = GetComponent<Move>();
        }

        Vector3 velocity = ((target - transform.position).normalized * move.max_mov_acceleration);

        move.AccelerateMovement(velocity,given_priority);

    }
}
