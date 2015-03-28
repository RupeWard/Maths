using UnityEngine;
using System.Collections;

public abstract class GeometryContext_Base : MonoBehaviour 
{
	private Transform elementContainer_ = null;

	protected GeometryGrid_Base Grid
	{
		get { return this.GetGridBase ( ); }
	}

	abstract protected GeometryGrid_Base GetGridBase ( );

	protected abstract float ComputeDefaultPointSize ( );

	private float defaultPointSize_ = -1f;
	public float DefaultPointSize
	{
		get 
		{
			if (defaultPointSize_ == -1f)
			{
				defaultPointSize_ = this.ComputeDefaultPointSize();
			}
			return defaultPointSize_; 
		} 
	}

	public void AddElement( GeometryElement_Base e)
	{
		e.transform.parent = elementContainer_;
	}

	protected virtual void Awake()
	{
		gameObject.layer = UnityHelpers.GetLayerNum ( "GeometryContext" );

		GameObject go = new GameObject();
		go.name = "Elements";
		go.layer = UnityHelpers.GetLayerNum ( "GeometryContext" );

		go.transform.parent = transform;
		elementContainer_ = go.transform;
	}

	public virtual void Init()
	{
	}

	// Use this for initialization
	protected virtual void Start () 
	{
	
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
	
	}

}
