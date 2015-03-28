using UnityEngine;
using System.Collections;

public class GeometryGridPlane : GeometryGrid_Base 
{
	private Color colour_ = Color.blue;
	private Vector2 spacing_ = new Vector2(float.NaN, float.NaN);
	private Vector2 lineWidths_ = new Vector2(float.NaN, float.NaN);
	private Vector2 origin_ = Vector2.zero;

	private GeometryContextPlane context_;

	protected override void Awake()
	{
		base.Awake ( );
	}

	public void Init(GeometryContextPlane gcp, Color colour, int numLines, float lineWidth)
	{
		float minDimension = gcp.RectArea.MinDimension();
		Init ( gcp, colour,  minDimension / (float) numLines, lineWidth);
	}
	
	public void Init(GeometryContextPlane gcp, Color colour, float spacing, float lineWidth)
	{
		context_ = gcp;

		origin_.Set ( context_.RectArea.MidX(), context_.RectArea.MidY () );
		lineWidths_ = lineWidth * Vector2.one;
		spacing_ = spacing * Vector2.one;
		colour_ = colour;
	}

	protected override void Start () 
	{
		float x = origin_.x;
		while ( x > context_.RectArea.xMin )
		{
			x -= spacing_.x;
		}
		int n = 0;
		while ( x < context_.RectArea.xMax )
		{
			Rect r = new Rect( x, context_.RectArea.y,
			                  lineWidths_.x, context_.RectArea.height);
			GameObject newLine = MeshHelpers.MakeQuadObject("x"+n.ToString(),
			                                                this.gameObject, 
			                                                context_.MakeWorldRect(r, GeometryContextPlane.s_defaultGridZ),
			                                                GeometryContextPlane.s_defaultGridZ,
			                                                colour_);

			x += spacing_.x;
			n++;
		}
		float y = origin_.y;
		while ( y > context_.RectArea.yMin )
		{
			y -= spacing_.y;
		}
		n = 0;
		while ( y < context_.RectArea.yMax )
		{
			Rect r = new Rect( context_.RectArea.x, y, 
			                  context_.RectArea.width,
			                  lineWidths_.y);
			GameObject newLine = MeshHelpers.MakeQuadObject("y"+n.ToString(),
			                                                this.gameObject, 
			                                                context_.MakeWorldRect(r, GeometryContextPlane.s_defaultGridZ),
			                                                GeometryContextPlane.s_defaultGridZ,
			                                                colour_);
			
			y += spacing_.y;
			n++;
		}
	}
	
	protected override void Update () 
	{
	
	}
}
