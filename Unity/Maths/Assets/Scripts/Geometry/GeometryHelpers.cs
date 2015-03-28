using UnityEngine;
using System.Collections;

public static class GeometryHelpers  
{
	private static readonly string s_plainColourMaterialPath = "Materials/PlainColourMaterial";
	private static Material s_plainColourMaterial = null;
	private static Material PlainColourMaterial
	{
		get 
		{
			if (s_plainColourMaterial == null)
			{
				s_plainColourMaterial = Resources.Load < Material > (s_plainColourMaterialPath) as Material;
				if (s_plainColourMaterial == null)
				{
					Debug.LogError("Failed to load '"+s_plainColourMaterialPath+"'");
				}
			}
			return s_plainColourMaterial;
		}
	}

	public static Material GetPlainColourMaterial()
	{
		return new Material( PlainColourMaterial);
	}

	public static Material GetPlainColourMaterial(Color colour)
	{
		Material mat = GetPlainColourMaterial();
		mat.SetColor("_MainTint", colour);
		return mat;
	}
}
