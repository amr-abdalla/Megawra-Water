using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.YamlDotNet.Serialization.NodeTypeResolvers;
using UnityEngine;
using UnityEngine.UIElements;

public static class MeshHelper
{
	static List<Vector3> vertices;
	static List<Vector3> normals;
	static List<Color> colors;
	static List<Vector2> uv;
	static List<Vector2> uv1;
	static List<Vector2> uv2;

	static List<int> indices;

	static void InitArrays(Mesh mesh)
	{
		vertices = new List<Vector3>(mesh.vertices);
		normals = new List<Vector3>(mesh.normals);
		colors = new List<Color>(mesh.colors);
		uv = new List<Vector2>(mesh.uv);
		uv1 = new List<Vector2>(mesh.uv2);
		uv2 = new List<Vector2>(mesh.uv2);
		indices = new List<int>();
	}

	#region Subdivide4 (2x2)
	static int GetNewVertex4(int i1, int i2)
	{
		int newIndex = vertices.Count;

		vertices.Add((vertices[i1] + vertices[i2]) * 0.5f);
		if (normals.Count > 0)
			normals.Add((normals[i1] + normals[i2]).normalized);
		if (colors.Count > 0)
			colors.Add((colors[i1] + colors[i2]) * 0.5f);
		if (uv.Count > 0)
			uv.Add((uv[i1] + uv[i2]) * 0.5f);
		if (uv1.Count > 0)
			uv1.Add((uv1[i1] + uv1[i2]) * 0.5f);
		if (uv2.Count > 0)
			uv2.Add((uv2[i1] + uv2[i2]) * 0.5f);

		return newIndex;
	}


	/// <summary>
	/// Devides each triangles into 4. A quad(2 tris) will be splitted into 2x2 quads( 8 tris )
	/// </summary>
	/// <param name="mesh"></param>
	public static void Subdivide4(Mesh mesh)
	{
		InitArrays(mesh);

		int[] triangles = mesh.triangles;
		for (int i = 0; i < triangles.Length; i += 3)
		{
			int i1 = triangles[i + 0];
			int i2 = triangles[i + 1];
			int i3 = triangles[i + 2];

			int a = GetNewVertex4(i1, i2);
			int b = GetNewVertex4(i2, i3);
			int c = GetNewVertex4(i3, i1);
			indices.Add(i1); indices.Add(a); indices.Add(c);
			indices.Add(i2); indices.Add(b); indices.Add(a);
			indices.Add(i3); indices.Add(c); indices.Add(b);
			indices.Add(a); indices.Add(b); indices.Add(c); // center triangle
		}
		mesh.vertices = vertices.ToArray();
		if (normals.Count > 0)
			mesh.normals = normals.ToArray();
		if (colors.Count > 0)
			mesh.colors = colors.ToArray();
		if (uv.Count > 0)
			mesh.uv = uv.ToArray();
		if (uv1.Count > 0)
			mesh.uv2 = uv1.ToArray();
		if (uv2.Count > 0)
			mesh.uv2 = uv2.ToArray();

		mesh.triangles = indices.ToArray();
	}
	#endregion Subdivide4 (2x2)


	public static List<int> SubdivideX(int subdivisionsX, int subdivisionsY, float height, float width, ref List<Vector3> vertices)
	{
		List<int> tris = new();
		int index1 = 0;
		int index2 = 1;
		int index3;

		for(int i = 0; i < subdivisionsX; i++)
		{
			Vector3 vertex0 = new Vector3(width / subdivisionsX * i, 0);
			index1 = vertices.IndexOf(vertex0);


			Vector3 newVertexX = new Vector3(width / subdivisionsX * (i + 1), 0);
			if(vertices.Any(vertex => vertex.Equals(newVertexX)))
			{
				index2 = vertices.IndexOf(newVertexX);
			}
			else
			{
				vertices.Add(newVertexX);
				index2 = vertices.Count - 1;
			}

			for (int j = 0; j < subdivisionsY; j++)
			{
				Vector3 newVertexY = new Vector3(width / subdivisionsX * i, height/subdivisionsY * (j+1));

				if (vertices.Any(vertex => vertex.Equals(newVertexY)))
				{
					index3 = vertices.IndexOf(newVertexY);
				}
				else
				{
					vertices.Add(newVertexY);
					index3 = vertices.Count - 1;
				}

				tris.Add(index3);
				tris.Add(index2);
				tris.Add(index1);

				index1 = index2;
				index2 = index3;

				Vector3 newerVertexY = new Vector3(width / subdivisionsX * (i + 1), height / subdivisionsY * (j + 1));

				if (vertices.Any(vertex => vertex.Equals(newerVertexY)))
				{
					index3 = vertices.IndexOf(newerVertexY);
				}
				else
				{
					vertices.Add(newerVertexY);
					index3 = vertices.Count - 1;
				}
				
				tris.Add(index3);
				tris.Add(index1);
				tris.Add(index2);

				index1 = index2;
				index2 = index3;

			}

		}

		return tris;
	}
}
