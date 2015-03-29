using UnityEngine;
using System.Collections;

public class GeometryElementPoint : GeometryElement_Base 
{ 
	private Material material_ = null;

	private Vector3 pos_ = Vector3.zero;

	public Color colour_ = Color.magenta;
	public float size_ = 0f;

	private static readonly string s_pointPrefabPath = "Geometry/Prefabs/Elements/Point"; 

	private static GameObject s_pointPrefab_ = null;
	private static GameObject PointPrefab
	{
		get 
		{
			if (s_pointPrefab_ == null)
			{
				s_pointPrefab_ = Resources.Load < GameObject > (s_pointPrefabPath) as GameObject;
				if (s_pointPrefab_ == null)
				{
					Debug.LogError ("No prefab: '"+s_pointPrefabPath+"'");
				}
			}
			return s_pointPrefab_;
		}
	}

	public static GeometryElementPoint Create( GeometryContext_Base context, string n, Vector3 pos, Color colour)
	{
		GameObject go = Instantiate(PointPrefab) as GameObject;
		go.name = n;
		GeometryElementPoint gep = go.GetComponent< GeometryElementPoint >();
		if (gep == null)
		{
			Debug.LogError ("Couldn't find PointPrefab Component on '"+n+"' made from '"+s_pointPrefabPath+"'"); 
			UnityHelpers.Destroy(go);
			return null;
		}
		else
		{
			gep.Init( context, colour, pos );
			return gep;
		}

	}

	protected override void Awake()
	{
		base.Awake ();
		size_ = 1f;
	}

	protected void Init( GeometryContext_Base context, Color colour, Vector3 pos )
	{
		base.Init(context);
		pos_ = pos;
		colour_ = colour;
		size_ = context.DefaultPointSize;
	}

	protected override void Start () 
	{
		base.Start ();

		MeshRenderer mesh = display_.GetComponent< MeshRenderer > ();
		material_ = GeometryHelpers.GetPlainColourMaterial();
		material_.SetColor("_MainTint", colour_);

		mesh.material = material_;

		transform.position = context_.GetWorldPosition( new Vector2(pos_.x, pos_.y), GeometryContextPlane.s_defaultPlaneZ );
		Debug.Log ("Point "+pos_+" -> "+transform.position);
		display_.transform.localScale = size_ * Vector3.one;
	}
	
	protected override void Update () 
	{
	
	}
}
