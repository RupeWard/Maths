using UnityEngine;
using System.Collections;

public class GeometryElementLine : GeometryElement_Base 
{ 
	private Material lineMaterial_ = null;

	private Vector3 [] ends_ = null;

	private GeometryElementPoint[] points_ = new GeometryElementPoint[2]{ null, null}; 

	public Color pointColour_ = Color.magenta;
	public Color lineColour_ = Color.magenta;

	public float pointSize_ = 0f;
	public float lineSize_ = 0f;

	private static readonly string s_linePrefabPath = "Geometry/Prefabs/Elements/Line"; 

	private static GameObject s_linePrefab_ = null;
	private static GameObject LinePrefab
	{
		get 
		{
			if (s_linePrefab_ == null)
			{
				s_linePrefab_ = Resources.Load < GameObject > (s_linePrefabPath) as GameObject;
				if (s_linePrefab_ == null)
				{
					Debug.LogError ("No prefab: '"+s_linePrefabPath+"'");
				}
			}
			return s_linePrefab_;
		}
	}

	public static GeometryElementLine Create( GeometryContext_Base context, string n, Vector3[] ends, Color colour)
	{
		GameObject go = Instantiate(LinePrefab) as GameObject;
		go.name = n;
		GeometryElementLine gel = go.GetComponent< GeometryElementLine >();
		if (gel == null)
		{
			Debug.LogError ("Couldn't find LinePrefab Component on '"+n+"' made from '"+s_linePrefabPath+"'"); 
			UnityHelpers.Destroy(go);
			return null;
		}
		else
		{
			gel.Init( context, colour, ends);
			return gel;
		}

	}

	protected override void Awake()
	{
		base.Awake ();
	}

	protected void Init( GeometryContext_Base context, Color colour, Vector3[] ends)
	{
		base.Init(context);
		ends_ = ends;
		lineColour_ = pointColour_ = colour;
		pointSize_ = context.DefaultPointSize;
		lineSize_ = 0.67f*pointSize_;
	}

	protected override void Start () 
	{
		base.Start ( );
		StartCoroutine ( CreateMeshCR ( ) );
	}

	private IEnumerator CreateMeshCR()
	{
		MeshRenderer mesh = display_.GetComponent< MeshRenderer > ();
		lineMaterial_ = GeometryHelpers.GetPlainColourMaterial();
		lineMaterial_.SetColor("_MainTint", lineColour_);
		mesh.material = lineMaterial_;

		Vector3[] worldPositions = new Vector3[]
		{
			context_.GetWorldPosition( ends_[0], GeometryContextPlane.s_defaultPlaneZ),
			context_.GetWorldPosition( ends_[1], GeometryContextPlane.s_defaultPlaneZ)
		};

		transform.position = 0.5f * ( worldPositions[0]+worldPositions[1]);

		Vector3 line = worldPositions[1]-worldPositions[0];

		float xDist = line.x;
		float yDist = line.y;
		float zDist = line.z;
		display_.transform.localScale = new Vector3( line.magnitude,
		                                            lineSize_, lineSize_);

		Vector2 endsCentre = 0.5f * ( ends_[1]+ends_[0]);
		Vector2 endsDiff = ends_[1]-ends_[0];

		Vector2 magnitudeVec = new Vector2( endsDiff.magnitude, 0f);

		points_ [ 0 ] = GeometryElementPoint.Create ( context_, "end0", 
		                                             endsCentre - (0.5f*magnitudeVec), 
		                                             pointColour_ );
		points_ [ 1 ] = GeometryElementPoint.Create ( context_, "end1", 
		                                             endsCentre + 0.5f*magnitudeVec, 
		                                             pointColour_ );

		/*
		points_ [ 0 ] = GeometryElementPoint.Create ( context_, "end0", 
			                                             transform.position - 0.5f*magnitudeVec, 
			                                             pointColour_ );
		points_ [ 1 ] = GeometryElementPoint.Create ( context_, "end1", 
		                                             transform.position + 0.5f*magnitudeVec, 
		                                             pointColour_ );
		  */                                           

		for (int i = 0 ; i<2; i++)
		{
			points_[i].transform.parent = this.transform;
		}
		yield return null;

		float zAngle = Mathf.Atan (yDist / xDist) * 180f/Mathf.PI;
		float yAngle = Mathf.Atan (xDist / zDist) * 180f/Mathf.PI;
		float xAngle = Mathf.Atan (zDist / yDist) * 180f/Mathf.PI;

		transform.Rotate(0f, 0f, zAngle );
	}
	
	protected override void Update () 
	{
	
	}
}
