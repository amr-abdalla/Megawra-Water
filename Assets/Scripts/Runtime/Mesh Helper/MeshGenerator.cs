using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviourBase
{
	public int width;
	public int height;
	[Min(1)] public int subdivisionsX;
	[Min(1)] public int subdivisionsY;
	private MeshFilter meshFilter;

	public void init()
	{
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
		meshRenderer.sharedMaterial.color = Color.red;
		meshFilter = gameObject.AddComponent<MeshFilter>();
	}

	[ExposePublicMethod]
	public void GenerateMesh()
	{
		Mesh mesh = new Mesh();

		List<Vector3> vertices = new()
		{
			new Vector3(0, 0, 0),
			new Vector3(0, height, 0),
			new Vector3(width, height, 0),
			new Vector3(width, 0, 0)
		};

		List<int> tris = MeshHelper.SubdivideX(subdivisionsX, subdivisionsY, height, width, ref vertices);  //MeshHelper.SubdivideX(10, height, ref vertices);

		mesh.vertices = vertices.ToArray();

		mesh.triangles = tris.ToArray();

		mesh.RecalculateNormals();

		meshFilter.mesh = mesh;

	}

	public void Start()
	{
		init();
	}

}