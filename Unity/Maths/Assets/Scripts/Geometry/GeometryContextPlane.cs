using UnityEngine;
using System.Collections;

/*
 * 		Sets z = 0 for all.
 */
public class GeometryContextPlane : GeometryContext_Base 
{
	public static GeometryContextPlane Create(string n, GameObject parent)
	{
		GameObject go = new GameObject ( );
		go.name = n;
		go.transform.parent = parent.transform;
		GeometryContextPlane gcp =  go.AddComponent< GeometryContextPlane > ( );
		if ( gcp == null )
		{
			Debug.LogError ( "Failed to add GeometryContextPlane to '" + n + "'" );
			UnityHelpers.Destroy ( go );
			return null;
		}
		else
		{
			return gcp;
		}
	}

	// Use this for initialization
	protected override void Start () 
	{
		base.Start ( );	
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update ( );
	}

	public GeometryElementPoint CreatePoint( string n, Vector2 pos, Color colour)
	{
		GeometryElementPoint result = GeometryElementPoint.Create( this, n, new Vector3( pos.x, pos.y, 0f), colour);
		return result;
	}
	                                       
}
