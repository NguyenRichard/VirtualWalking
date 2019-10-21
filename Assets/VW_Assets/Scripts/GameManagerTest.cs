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

    private float guardian_height = 5;
    public float Guardian_Height
    {
        get { return guardian_height; }
        set { this.guardian_height = value; }
    }

    [SerializeField]
    private float max_guardian_height = 5;

    [SerializeField]
    private float min_guardian_height = 0.5f;

    [SerializeField]
    private float max_dist_gardian = 2;

    [SerializeField]
    private float min_dist_gardian = 0.5f;

    private GameObject guardian;
    private Mesh mesh;

    private Camera main_camera;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> boundary_vertices = new List<Vector3>();
        guardian_height = max_guardian_height;

       /* boundary_vertices.Add(new Vector3(5, 0, 5));
        boundary_vertices.Add(new Vector3(-5, 0, 5));
        boundary_vertices.Add(new Vector3(-5, 0, -5));
        boundary_vertices.Add(new Vector3(5, 0, -5));*/

        boundary =  OVRManager.boundary;

        for (int i = 0; i < boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary).Length; i++)
        {
            Vector3 vec = boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary)[i];
            vec.y = 0;
            boundary_vertices.Add(vec);
        }
        drawGuardian(boundary_vertices);

        main_camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Assert(guardian, "The guardian was not instantiated");
        Vector3[] vertices = mesh.vertices;

        for(int i = vertices.Length/2; i < vertices.Length; i++)
        {
            vertices[i].y = heightFromDist(distToCamera(vertices[i]));
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    //This function takes as an input a list of vertices that corresponds to a 2D polygon.
    //From, this list, it will instantiate a wall of a certain height.
    private void drawGuardian(List<Vector3> boundary)
    {
        guardian = Instantiate(guardian_prefab, Vector3.zero, Quaternion.identity);
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
        mesh = guardian.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    //This function returns the distance between a vertex and the camera position in the X/Z plan.
    float distToCamera(Vector3 point)
    {
        return Vector2.Distance(new Vector2(point.x,point.z), new Vector2(main_camera.transform.position.x, main_camera.transform.position.z));
    }

    //This function is a decreasing linear function that projects [min_dist_guardian,max_dist_guardian] to [0,1].
    float distRatio(float dist)
    {
        return (dist - max_dist_gardian) / (min_dist_gardian - max_dist_gardian);
    }

    float heightFromDist(float dist)
    {
        if (dist <= min_dist_gardian)
        {
            return max_guardian_height;
        }
        else if (dist >= max_dist_gardian)
        {
            return min_guardian_height;
        }
        else
        {
            return min_guardian_height + (max_dist_gardian - min_guardian_height) * distRatio(dist);
        }
    }
}
