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
