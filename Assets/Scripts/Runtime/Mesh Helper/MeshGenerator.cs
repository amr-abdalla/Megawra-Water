using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MeshGenerator : MonoBehaviourBase
{
	[SerializeField] public float width;
	[SerializeField] private float height;
	[Min(1)] [SerializeField] private int subdivisionsX;
	[Min(1)] [SerializeField] private int subdivisionsY;
	[SerializeField] public Material material;
	[SerializeField] BoxCollider2D collider;

	private MeshFilter meshFilter;

	#region mesh Data
	private Dictionary<int, int> VerticesIndicesByIDs;
	private List<Vector3> vertices;
	private List<Vector2> uvs;
	private List<int> tris;
	public List<Vector3> surfaceVertices { get; private set; }
	#endregion

	#region UNITY
	private void Start()
	{
		Init();
		GenerateMesh();
	}

	#endregion

	#region PUBLIC API
	[ExposePublicMethod]
	public void GenerateMesh()
	{
		Mesh mesh = new Mesh();

		UpdateMeshData();

		mesh.vertices = vertices.ToArray();

		mesh.triangles = tris.ToArray();

		mesh.uv = uvs.ToArray();

		mesh.RecalculateNormals();

		meshFilter.mesh = mesh;

		UpdateCollider();
	}

	#endregion

	#region PRIVATE
	private void Init()
	{
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.sharedMaterial = material;
		meshFilter = gameObject.AddComponent<MeshFilter>();
	}

	private void UpdateCollider()
	{
		collider.size = new Vector2(width, 1f);
		collider.offset = new Vector2(width / 2f, height);
	}

	private void UpdateMeshData()
	{
		tris = new();
		VerticesIndicesByIDs = new();
		vertices = new();
		uvs = new();

		AddCornerVertices();

		int index1;
		int index2;
		int index3;

		for (int i = 1; i <= subdivisionsX; i++)
		{
			index1 = AddVertex(i - 1, 0);
			index2 = AddVertex(i, 0);

			for (int j = 1; j <= subdivisionsY; j++)
			{
				index3 = AddVertex(i - 1, j);

				AddTriangle(tris, index3, index2, index1);
				index1 = index2;
				index2 = index3;

				index3 = AddVertex(i, j);

				AddTriangle(tris, index3, index1, index2);
				index1 = index2;
				index2 = index3;
			}

		}

		foreach (var vertex in vertices)
		{
			uvs.Add(new Vector2(vertex.x / width, vertex.y / height));
		}

		UpdateSurfaceVertices();

	}

	private void UpdateSurfaceVertices()
	{
		surfaceVertices = new();

		foreach(Vector3 vertex in vertices)
		{
			if(vertex.y == height)
			{
				surfaceVertices.Add(vertex);
			}
		}
	}

	private void AddTriangle(List<int> tri, int index1, int index2, int index3)
	{
		tri.Add(index1);
		tri.Add(index2);
		tri.Add(index3);
	}

	private void AddCornerVertices()
	{
		AddVertex(0, 0);
		AddVertex(0, subdivisionsY);
		AddVertex(subdivisionsX, subdivisionsY);
		AddVertex(subdivisionsX, 0);
	}

	private int AddVertex(int i, int j)
	{
		int ID = (i << 16) | j;

		if (VerticesIndicesByIDs.TryGetValue(ID, out int index))
		{
			return index;
		}

		vertices.Add(new Vector3(width / subdivisionsX * i, height / subdivisionsY * j));
		VerticesIndicesByIDs.Add(ID, vertices.Count - 1);

		return vertices.Count - 1;
	}

	#endregion

}