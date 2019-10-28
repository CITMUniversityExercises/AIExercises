using UnityEngine;
using System.Collections;

public class SteeringAlign : SteeringAbstract
{

    public float min_angle = 0.01f;
    public float slow_angle = 0.1f;
    public float time_to_target = 0.1f;

    Move move;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO 7: Very similar to arrive, but using angular velocities
        // Find the desired rotation and accelerate to it
        // Use Vector3.SignedAngle() to find the angle between two directions
        // Orientation we are trying to match

        float current_orientation = Mathf.Rad2Deg * Mathf.Atan2(transform.forward.x, transform.forward.z);
        float target_orientation = current_orientation + Vector3.SignedAngle(move.target.transform.position - move.transform.position, transform.forward, Vector3.up);
        float angle = -Mathf.DeltaAngle(current_orientation, target_orientation);

        if (Mathf.Abs(angle) < min_angle)
        {
            move.SetRotationVelocity(0.0f);
        }
        else
        {
            float desired_rotation = 0.0f;
            float slow_factor = Mathf.Abs(angle) / slow_angle;

            if (Mathf.Abs(angle) > slow_angle)
                desired_rotation = move.max_rot_speed;
            else
                desired_rotation = move.max_rot_speed * slow_factor;

            float acceleration = desired_rotation / time_to_target;

            if (angle < slow_angle)
            {
                acceleration = -acceleration;
            }
            move.AccelerateRotation(Mathf.Clamp(acceleration, -move.max_rot_acceleration, move.max_rot_acceleration),priority);

        }

    }
}