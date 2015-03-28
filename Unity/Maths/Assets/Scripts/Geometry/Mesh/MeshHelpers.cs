using UnityEngine;
using System.Collections;

public static class MeshHelpers 
{
	private static readonly string s_quadPrefabPath = "Geometry/Prefabs/Elements/Quad"; 
	
	private static GameObject s_quadPrefab = null;
	private static GameObject QuadPrefab
	{
		get 
		{
			if (s_quadPrefab == null)
			{
				s_quadPrefab = Resources.Load < GameObject > (s_quadPrefabPath) as GameObject;
				if (s_quadPrefab == null)
				{
					Debug.LogError ("No prefab: '"+s_quadPrefabPath+"'");
				}
			}
			return s_quadPrefab;
		}
	}

	public static GameObject MakeQuadObject(string n, GameObject parent, Rect r, float z, Color colour)
	{
		GameObject go = GameObject.Instantiate ( QuadPrefab ) as GameObject;
		go.transform.parent = parent.transform;
		go.name = n;

		go.transform.position = new Vector3( 0.5f * (r.xMin + r.xMax), 0.5f * (r.yMin + r.yMax), z);
		go.transform.localScale = new Vector3( r.width, r.height, 1f );

		Material mat = GeometryHelpers.GetPlainColourMaterial();
		mat.SetColor("_MainTint", colour);
		go.GetComponent< MeshRenderer >().material = mat;
		return go;
	}


}
