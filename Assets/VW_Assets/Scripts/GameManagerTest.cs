using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class GameManagerTest : MonoBehaviour
{
    public GameObject cubeprefab;
    public OVRBoundary boundary;

    [SerializeField]
    private GameObject guardian_prefab;

    [SerializeField]
    private float guardian_height = 5;
    public float Guardian_Height
    {
        get { return guardian_height; }
        set { this.guardian_height = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        boundary =  OVRManager.boundary;
        List<Vector3> boundary_vertices = new List<Vector3>();

        for (int i = 0; i < boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary).Length; i++)
         {
             Vector3 vec = boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary)[i];
             vec.y = 0;
             boundary_vertices.Add(vec);
         }
        drawGuardian(boundary_vertices);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void drawGuardian(List<Vector3> boundary)
    {
        GameObject guardian = Instantiate(guardian_prefab, Vector3.zero, Quaternion.identity);
        int size = boundary.Count;
        Vector3[] vertices = new Vector3[size*2];
        int i = 0;
        foreach (var point in boundary)
        {
            vertices[i] = point;
            vertices[size + i] = new Vector3(0, guardian_height, 0) + point;
            i++;
        }
        int[] triangles = new int[(size) * 6];
        for(int j = 0; j < size; j++)
        {
            triangles[6 * j] = (j + 1) % size;
            triangles[6 * j + 1] = size + j;
            triangles[6 * j + 2] = j;

            triangles[6 * j + 3] = (j + 1) % size;
            triangles[6 * j + 4] = size + (j + 1) % size;
            triangles[6 * j + 5] = size + j;
        }
        Mesh mesh = guardian.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
