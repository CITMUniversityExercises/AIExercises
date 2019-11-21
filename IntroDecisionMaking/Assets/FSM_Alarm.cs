﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class FSM_Alarm : MonoBehaviour {
    private bool player_detected = false;
    private bool in_alarm = false;
    private Vector3 patrol_pos;
    public float min_distance = 1.0f;

    public GameObject alarm;
    public BansheeGz.BGSpline.Curve.BGCurve path;
    NavMeshAgent agent;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == alarm)
            in_alarm = true;
    }

    // Update is called once per frame
    void PerceptionEvent(PerceptionEvent ev)
    {
        if (ev.type == global::PerceptionEvent.types.NEW)
        {
            player_detected = true;
        }
    }

    // TODO 1: Create a coroutine that executes 20 times per second
    // and goes forever. Make sure to trigger it from Start()

    IEnumerator Patrol()
    {
        while (!player_detected)
        {
            yield return new WaitForSeconds(1/20);
            Debug.Log("Patrolling...");
        }

        player_detected = false;
        patrol_pos = transform.position;
        Debug.Log("Enemy spotted");
        yield return StartCoroutine("Alarm");
       
    }

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("Patrol");
    }


    // TODO 2: If player is spotted, jump to another coroutine that should
    // execute 20 times per second waiting for the player to reach the alarm
    IEnumerator Alarm()
    {
        path.gameObject.SetActive(false);
        agent.SetDestination(alarm.transform.position);

        while (!in_alarm)
        {
            Debug.Log("Alarm!!");
            yield return new WaitForSeconds(1/20);
        }

        in_alarm = false;
        Debug.Log("Reached Alarm");
        yield return StartCoroutine("ReturnToPath");
    }

    // TODO 3: Create the last coroutine to have the tank waiting to reach
    // the point where he left the path, and trigger again the patrol
    IEnumerator ReturnToPath()
    {
        agent.SetDestination(patrol_pos);

        while ((patrol_pos - agent.transform.position).magnitude > min_distance)
        {
            Debug.Log("Returning to patrol");
            yield return new WaitForSeconds(1 / 20);
        }

        agent.ResetPath();
        path.gameObject.SetActive(true);
        Debug.Log("Patrolling again");
        yield return StartCoroutine("Patrol");
    }

}