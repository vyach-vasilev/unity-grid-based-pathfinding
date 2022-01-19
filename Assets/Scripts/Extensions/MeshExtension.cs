using UnityEngine;

public static class MeshExtension
{
    public static Mesh Quad(float width = 1, float height = 1)
    {
        var mesh = new Mesh();
        
        var vertices = new Vector3[] { new(0, 0, 0), new(width, 0, 0), new(0, 0, height), new(width, 0, height) };
        var triangles = new[] { 0, 2, 1, 2, 3, 1 };
        var normals = new[] { -Vector3.forward, -Vector3.forward, -Vector3.forward, -Vector3.forward };
        var uv = new Vector2[] { new(0, 0), new(1, 0), new(0, 1), new(1, 1) };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        return mesh;
    }
}