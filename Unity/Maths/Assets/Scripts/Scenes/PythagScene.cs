using UnityEngine;
using System.Collections;

public class PythagScene : MonoBehaviour 
{
	private GeometryContextPlane geometryContext_ = null;

	// Use this for initialization
	void Start () 
	{
		geometryContext_ = GeometryContextPlane.Create ("PlaneContext", gameObject  );

		GeometryElementPoint p = geometryContext_.CreatePoint( "p0", Vector2.zero, Color.magenta);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
