using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviourBase
{
    Mesh mesh;
    public Vector3[] vertices;
    public triangle[] triangles;

    [System.Serializable]
    public struct triangle
	{
        public int index1;
        public int index2;
        public int index3;
	}

    public void Start()
    {
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        meshRenderer.sharedMaterial.color = Color.red;

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        mesh = new Mesh();

        //Vector3[] vertices = new Vector3[4]
        //{
        //    new Vector3(0, 0, 0),
        //    new Vector3(width, 0, 0),
        //    new Vector3(0, width, 0),
        //    new Vector3(width, height, 0)
        //};

        mesh.vertices = vertices;

        mesh.triangles = GetIndices();

        //Vector3[] normals = new Vector3[4]
        //{
        //    -Vector3.forward,
        //    -Vector3.forward,
        //    -Vector3.forward,
        //    -Vector3.forward
        //};
        //mesh.normals = normals;

        mesh.RecalculateNormals();

        //Vector2[] uv = new Vector2[4]
        //{
        //    new Vector2(0, 0),
        //    new Vector2(1, 0),
        //    new Vector2(0, 1),
        //    new Vector2(1, 1)
        //};
        //mesh.uv = uv;

        meshFilter.mesh = mesh;
    }

    public int[] GetIndices()
	{
        List<int> result = new();

        foreach(triangle triangle in triangles)
		{
            result.Add(triangle.index1);
            result.Add(triangle.index2);
            result.Add(triangle.index3);
		}

        return result.ToArray();
	}

    [ExposePublicMethod]
    public void Subdivide()
	{
        MeshHelper.Subdivide4(mesh);
    }


}