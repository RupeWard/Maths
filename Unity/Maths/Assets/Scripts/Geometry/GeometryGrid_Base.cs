using UnityEngine;
using System.Collections;

public class GeometryGrid_Base : MonoBehaviour 
{

	virtual protected void Awake()
	{
		gameObject.layer = UnityHelpers.GetLayerNum ( "GeometryContext" );
	}

	virtual protected void Start () 
	{
	
	}
	
	virtual protected void Update () 
	{
	
	}
}
