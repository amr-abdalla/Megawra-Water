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

		MeshData meshData = MeshHelper.Subdivide(subdivisionsX, subdivisionsY, height, width);

		mesh.vertices = meshData.vertices.ToArray();

		mesh.triangles = meshData.tris.ToArray();

		mesh.RecalculateNormals();

		meshFilter.mesh = mesh;

	}

	public void Start()
	{
		init();
	}

}