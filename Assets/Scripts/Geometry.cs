using UnityEngine;
using System.Collections.Generic;

public class Geometry {
    public enum Direction { north, east, south, west, up, down }

    List<Vector3> verts = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();

    Mesh mesh = new Mesh();
    List<int>[] submeshes;

    public Geometry(int meshes = 1) {
        submeshes = new List<int>[meshes];
        for(int i = 0; i < meshes; i++)
            submeshes[i] = new List<int>();
    }

    public Mesh Mesh() {
        mesh.subMeshCount = submeshes.Length;
        mesh.vertices = verts.ToArray();
        for (int i = 0; i < submeshes.Length; i++)
            mesh.SetTriangles(submeshes[i].ToArray(), i);
        mesh.uv = uvs.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }

    public void CreateFace(int x, int y, int z, Direction direction, Vector2[] uv = null, int submesh = 0) {
        int i = verts.Count;

        switch (direction) {
            case Direction.north:
                verts.Add(new Vector3(x + 1, y, z + 1));
                verts.Add(new Vector3(x, y, z + 1));
                verts.Add(new Vector3(x, y + 1, z + 1));
                verts.Add(new Vector3(x + 1, y + 1, z + 1));
                break;

            case Direction.east:
                verts.Add(new Vector3(x + 1, y, z));
                verts.Add(new Vector3(x + 1, y, z + 1));
                verts.Add(new Vector3(x + 1, y + 1, z + 1));
                verts.Add(new Vector3(x + 1, y + 1, z));
                break;

            case Direction.south:
                verts.Add(new Vector3(x, y, z));
                verts.Add(new Vector3(x + 1, y, z));
                verts.Add(new Vector3(x + 1, y + 1, z));
                verts.Add(new Vector3(x, y + 1, z));
                break;

            case Direction.west:
                verts.Add(new Vector3(x, y, z + 1));
                verts.Add(new Vector3(x, y, z));
                verts.Add(new Vector3(x, y + 1, z));
                verts.Add(new Vector3(x, y + 1, z + 1));
                break;

            case Direction.up:
                verts.Add(new Vector3(x, y + 1, z));
                verts.Add(new Vector3(x + 1, y + 1, z));
                verts.Add(new Vector3(x + 1, y + 1, z + 1));
                verts.Add(new Vector3(x, y + 1, z + 1));
                break;

            case Direction.down:
                verts.Add(new Vector3(x + 1, y, z + 1));
                verts.Add(new Vector3(x + 1, y, z));
                verts.Add(new Vector3(x, y, z));
                verts.Add(new Vector3(x, y, z + 1));
                break;
        }

        submeshes[submesh].AddRange(new int[] {
            i + 1, i + 0, i + 2,
            i + 0, i + 3, i + 2,
        });

        if (uv != null)
            uvs.AddRange(uv);
        else {
            uvs.AddRange(new Vector2[] {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0),
            });
        }
    }

    public void Clear() {
        for (int i = 0; i < submeshes.Length; i++)
            submeshes[i].Clear();
        verts.Clear();
        uvs.Clear();
        mesh.Clear();
    }
}
