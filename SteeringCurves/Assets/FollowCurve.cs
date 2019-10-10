using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class FollowCurve : MonoBehaviour
{
    public BGCcMath curve;
    float ratio = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

        // desired_initialpos = math.CalcPositionByClosestPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        ratio += (0.1f * Time.deltaTime);

        if (ratio > 1)
            ratio = 0;

        Vector3 final_position = curve.CalcPositionByDistanceRatio(ratio);

        transform.position = final_position;
    }
}
