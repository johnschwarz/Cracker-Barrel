using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour {

    public int xSize, ySize;

    void Awake()
    {
        GenerateGrid();
    }

    private Vector3[] vertices;
    private void GenerateGrid()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i ++)
            {
                if ((y%2) ==  0)
                { vertices[i] = new Vector3(x +0.5f, y); }
                else
                {
                    vertices[i] = new Vector3(x, y);
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices == null) { return; }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
