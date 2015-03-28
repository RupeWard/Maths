using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 		Sets z = 0 for all.
 */
public class GeometryContextPlane : GeometryContext_Base 
{
	public static float  s_defaultBackgroundZ = 20f;
	public static float  s_defaultGridZ = 10f;
	public static float  s_defaultPlaneZ = 0f;

	protected Rect rectArea_ = new Rect();
	public Rect RectArea
	{
		get { return rectArea_; }
		set 
		{
			rectArea_ = value;
		}
	}

	protected override float ComputeDefaultPointSize ()
	{		
		float dim = Mathf.Min( Camera.main.pixelWidth, Camera.main.pixelHeight);
		float size = dim / 20f;
		Vector3 rad0 = Camera.main.ScreenToWorldPoint( new Vector3(0f,0f, s_defaultPlaneZ)); 
		Vector3 rad1 = Camera.main.ScreenToWorldPoint( new Vector3(0f,size, s_defaultPlaneZ)); 

		return (rad1-rad0).magnitude;
	}
		                                              
	public Vector3 GetWorldPosition(Vector2 v, float z)
	{
		return Camera.main.ScreenToWorldPoint( new Vector3(v.x, v.y, z));
	}
	
	public Rect MakeWorldRect(Rect r, float z)
	{
		Vector3 bottomLeft = GetWorldPosition ( new Vector2( r.xMin, r.yMin), z);
		Vector3 topRight = GetWorldPosition ( new Vector2( r.xMax, r.yMax), z);

		Rect rect = new Rect( bottomLeft.x, bottomLeft.y, (topRight.x - bottomLeft.x), (topRight.y - bottomLeft.y));
		Debug.Log("From "+r+" BL = "+bottomLeft+" TR = "+topRight+" r = "+rect);
		return rect;
	}
	

	GeometryGridPlane grid_ = null;
	GameObject background_ = null;

	protected override GeometryGrid_Base GetGridBase()
	{
		return grid_;
	}

	public static GeometryContextPlane Create(string n, GameObject parent)
	{
		float width = Camera.main.pixelWidth;
		float height = Camera.main.pixelHeight;

		Rect r = new Rect ( 0f, 0f, width, height);
		return Create(n, parent, r);
	}

	public static GeometryContextPlane Create(string n, GameObject parent, Rect r)
	{
		GameObject gcp_go = new GameObject ( );
		gcp_go.name = n;
		gcp_go.transform.parent = parent.transform;
		GeometryContextPlane gcp =  gcp_go.AddComponent< GeometryContextPlane > ( );
		if ( gcp == null )
		{
			Debug.LogError ( "Failed to add GeometryContextPlane to '" + n + "'" );
			UnityHelpers.Destroy ( gcp_go );
			return null;
		}
		else
		{
			gcp.Init (r);
			return gcp;
		}
	}

	public void Init( Rect r)
	{
		base.Init();
		RectArea = r;

		GameObject ggp_go = new GameObject();
		ggp_go.name = "GridPlane";
		ggp_go.transform.parent = this.transform;
		GeometryGridPlane ggp = ggp_go.AddComponent< GeometryGridPlane >();
		if (ggp_go == null)
		{
			Debug.LogError("Failed to add GeometryGridPlane to '"+gameObject.name+"'");
		}
		grid_ = ggp;
		grid_.Init(this, Color.blue, 20f, 1f);

		background_ = MeshHelpers.MakeQuadObject( "Background", 
		                                         this.gameObject, 
		                                         MakeWorldRect(RectArea, s_defaultBackgroundZ), 
		                                         s_defaultBackgroundZ, 
		                                         Color.white);


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
		GeometryElementPoint result = GeometryElementPoint.Create( this, n, new Vector3( pos.x, pos.y, s_defaultPlaneZ), colour);
		return result;
	}
	                                       
}
