using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

[Name("EnableCurve")]
[Category("Custom")] // this is the location in the task menu

public class EnableCurve : ActionTask
{
    public BGCcMath math;

    protected override void OnExecute()
    {
        math.enabled = true;
        Debug.Log("executing code when action starts");
    }
    protected override void OnStop()
    {
        math.enabled = false;
        Debug.Log("executing code when action ends");
    }

}


