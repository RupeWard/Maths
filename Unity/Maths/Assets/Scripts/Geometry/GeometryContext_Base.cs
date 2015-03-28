using UnityEngine;
using System.Collections;

public abstract class GeometryContext_Base : MonoBehaviour 
{
	private Transform elementContainer_ = null;

	private float defaultPointSize_ = 1f;
	public float DefaultPointSize
	{
		get { return defaultPointSize_; } 
	}

	public void AddElement( GeometryElement_Base e)
	{
		e.transform.parent = elementContainer_;
	}

	protected virtual void Awake()
	{
		GameObject go = new GameObject();
		go.name = "Elements";
		go.transform.parent = transform;
		elementContainer_ = go.transform;
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
