using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NavMeshDrawer : MonoBehaviour
{

    void Awake()
    {
        //NavMeshの三角形集合取得
        NavMeshTriangulation triangles = NavMesh.CalculateTriangulation();

        //三角形集合からMeshを生成
        Mesh mesh = new Mesh();
        mesh.vertices = triangles.vertices;
        mesh.triangles = triangles.indices;

        //MeshFilterに生成したMeshを渡す
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
    }
}
