using System.Collections.Generic;
using UnityEngine;

public struct MeshData
{
	public List<Vector3> vertices;
	public List<int> tris;

	public MeshData(List<Vector3> i_vertices, List<int> i_tris)
	{
		vertices = i_vertices;
		tris = i_tris;
	}
}

public static class MeshHelper
{
	private static Dictionary<int, int> IndicesByIDs;
	private static List<Vector3> vertices;
	private static float width;
	private static float height;
	private static int subdivisionsX;
	private static int subdivisionsY;

	public static MeshData Subdivide(int i_subdivisionsX, int i_subdivisionsY, float i_height, float i_width)
	{
		List<int> tris = new();
		IndicesByIDs = new();
		vertices = new();

		width = i_width;
		height = i_height;
		subdivisionsX = i_subdivisionsX;
		subdivisionsY = i_subdivisionsY;

		AddCornerVertices();

		int index1;
		int index2;
		int index3;

		for (int i = 1; i <= i_subdivisionsX; i++)
		{
			index1 = AddVertex(i-1, 0);
			index2 = AddVertex(i, 0);

			for (int j = 1; j <= i_subdivisionsY; j++)
			{
				index3 = AddVertex(i - 1, j);

				AddTriangle(tris, index3, index2, index1);
				index1 = index2;
				index2 = index3;

				index3 = AddVertex(i,j);

				AddTriangle(tris, index3, index1, index2);
				index1 = index2;
				index2 = index3;
			}

		}

		return new MeshData(vertices, tris);
	}

	private static int AddVertex(int i, int j)
	{
		int ID = (i << 16) | j;

		if (IndicesByIDs.TryGetValue(ID, out int index))
		{
			return index;
		}

		vertices.Add(new Vector3(width / subdivisionsX * i, height / subdivisionsY * j));
		IndicesByIDs.Add(ID, vertices.Count - 1);

		return vertices.Count - 1;
	}

	private static void AddTriangle(List<int> tri, int index1, int index2, int index3)
	{
		tri.Add(index1);
		tri.Add(index2);
		tri.Add(index3);
	}

	private static void AddCornerVertices()
	{
		AddVertex(0, 0);
		AddVertex(0, subdivisionsY);
		AddVertex(subdivisionsX, subdivisionsY);
		AddVertex(subdivisionsX, 0);
	}
}
