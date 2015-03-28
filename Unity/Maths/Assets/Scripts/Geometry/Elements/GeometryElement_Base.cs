using UnityEngine;
using System.Collections;

public abstract class GeometryElement_Base : MonoBehaviour 
{
	protected GameObject display_ = null;
	protected GeometryContext_Base context_ = null;

	public string Name
	{
		get { return gameObject.name; }
	}

	protected virtual void Awake()
	{
		gameObject.layer = UnityHelpers.GetLayerNum ( "GeometryElements" );
	}

	protected virtual void Init( GeometryContext_Base c)
	{
		context_ = c;
		c.AddElement(this);
	}

	protected virtual void Start()
	{
		display_ = transform.FindChild ("Display").gameObject;
		if (display_ == null) 
		{
			Debug.LogError ( Name + " has no 'Display' child"); 
		}
	}

	protected virtual void Update()
	{
	}
}
