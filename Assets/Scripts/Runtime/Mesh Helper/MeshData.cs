using System.Collections.Generic;
using UnityEngine;

public struct MeshData
{
	public List<Vector3> vertices;
	public List<int> tris;
	public List<Vector2> uvs;

	public MeshData(List<Vector3> i_vertices, List<Vector2> i_uvs, List<int> i_tris)
	{
		vertices = i_vertices;
		uvs = i_uvs;
		tris = i_tris;
	}
}
