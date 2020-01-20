using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class GuardianManager : MonoBehaviour
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
    private float max_height = 5;
    public float Max_height
    {
        get { return max_height; }
        set { this.max_height = value; }
    }

    [SerializeField]
    private float min_height = 0.5f;
    public float Min_height
    {
        get { return min_height; }
        set { this.min_height = value; }
    }

    [SerializeField]
    private float max_dist = 2;
    public float Max_dist
    {
        get { return max_dist; }
        set { this.max_dist = value; }
    }


    [SerializeField]
    private float min_dist = 0.5f;
    public float Min_dist
    {
        get { return min_dist; }
        set { this.min_dist = value; }
    }


    [SerializeField]
    private Color _colorNear;

    [SerializeField]
    private Color _colorFar;

    [SerializeField]
    private float refreshTime = 1;

    [SerializeField]
    private Transform sceneOrigin;

    private GameObject guardian;
    private Mesh mesh;

    List<GFilter> filters;
    private List<Vector3> boundary_vertices;

    // Start is called before the first frame update
    void Start()
    {
        boundary_vertices = new List<Vector3>();
        guardian_height = max_height;
        filters = new List<GFilter>();

#if UNITY_EDITOR

        boundary_vertices.Add(new Vector3(5, 0, 5));
            boundary_vertices.Add(new Vector3(-5, 0, 5));
            boundary_vertices.Add(new Vector3(-5, 0, -5));
            boundary_vertices.Add(new Vector3(5, 0, -5));
        createGuardian();
#else
        boundary =  OVRManager.boundary;

        for (int i = 0; i < boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary).Length; i++)
        {
            Vector3 vec = boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary)[i];
            vec.y = 0;
            boundary_vertices.Add(vec);
        }

        createGuardian();
        filters.Add(new GFilterCalibrate(guardian, refreshTime,sceneOrigin));
#endif

        filters.Add(new GFilterHeightByDist(guardian, max_height, min_height, max_dist, min_dist));
        filters.Add(new GFilterRGBAByDist(guardian, _colorNear, _colorFar, max_dist, min_dist));

        AudioFile _guardianWarningSound = AudioManager.Find("GuardianWarningSound");
        filters.Add(new GFilterBySound(guardian, _guardianWarningSound, max_dist, min_dist));


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Assert(guardian, "The guardian was not instantiated");
        ApplyFilters();
    }

    //This function creates the guardian using updateGuardian().
    private void createGuardian()
    {
        guardian = Instantiate(guardian_prefab, Vector3.zero, Quaternion.identity);

        //guardian.GetComponent<Renderer>().enabled = false;

        updateGuardian();

        guardian.AddComponent<CustomGuardianData>();
        guardian.GetComponent<Renderer>().enabled = false;

    }

    //This function takes as an input a list of vertices that corresponds to a 2D polygon.
    //From, this list, it will instantiate a wall of a certain height.
    private void updateGuardian()
    {
        int size = boundary_vertices.Count;
        Vector3[] vertices = new Vector3[size * 2];
        int i = 0;
        foreach (var point in boundary_vertices)
        {
            vertices[i] = point;
            vertices[size + i] = new Vector3(0, guardian_height, 0) + point;
            i++;
        }
        int[] triangles = new int[(size) * 6];
        for (int j = 0; j < size; j++)
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

    }

    private void ApplyFilters()
    {
        foreach (var filter in filters)
        {
            filter.Apply();
        }

        GFilter.UpdateGuardian();
    }

}
