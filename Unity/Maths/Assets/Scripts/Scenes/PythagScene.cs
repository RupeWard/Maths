using UnityEngine;
using System.Collections;

public class PythagScene : MonoBehaviour 
{
	private GeometryContextPlane geometryContext_ = null;

	// Use this for initialization
	void Start () 
	{
		geometryContext_ = GeometryContextPlane.Create ("PlaneContext", gameObject  );

		GeometryElementPoint p = geometryContext_.CreatePoint( "p0", new Vector2(0.5f*Screen.width, 0.5f*Screen.height), Color.magenta);
		GeometryElementLine l 
			= geometryContext_.CreateLine( "l0", 
			    new Vector2[2]{ 
				new Vector2( 0.2f * Screen.width, 0.1f * Screen.height),
				new Vector2( 0.8f * Screen.width, 0.6f * Screen.height)
				},
			Color.green);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
