using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

[Name("Activate Deactivate path")]
[Category("Custom")]
public class BT_Patrol : ActionTask{

	public BansheeGz.BGSpline.Curve.BGCurve path;

	protected override void OnExecute(){
		Debug.Log("My agent is Patrolling");
		path.gameObject.SetActive(true);
	}

	protected override void OnStop(){
		Debug.Log("My agent stops Patrolling");
		path.gameObject.SetActive(false);
	}
}