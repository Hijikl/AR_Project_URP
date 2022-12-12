using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NavMeshDrawer : MonoBehaviour
{

    void Awake()
    {
        //NavMesh�̎O�p�`�W���擾
        NavMeshTriangulation triangles = NavMesh.CalculateTriangulation();

        //�O�p�`�W������Mesh�𐶐�
        Mesh mesh = new Mesh();
        mesh.vertices = triangles.vertices;
        mesh.triangles = triangles.indices;

        //MeshFilter�ɐ�������Mesh��n��
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
    }
}
