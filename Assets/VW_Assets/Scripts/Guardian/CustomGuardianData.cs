using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGuardianData : MonoBehaviour
{
    private Transform _camera;
    private Mesh _customGuardianMesh;

    public Color[] _verticesColor;
    public Color[] VerticesColor
    {
        get { return _verticesColor; }
    }

    private Vector3[] _vertices;
    public Vector3[] Vertices
    {
        get { return _vertices; }
    }
    private float[] _distances;
    public float[] Distances
    {
        get { return _distances; }
    }

    private float _closestVertexDistance;
    public float ClosestVertexDistance
    {
        get { return _closestVertexDistance; }
    }
    private int _indexClosestVertex;
    public int IndexClosestVertex => _indexClosestVertex;

    void Start()
    {
        _camera = Camera.main.transform;
        _customGuardianMesh = gameObject.GetComponent<MeshFilter>().mesh;
        _vertices = _customGuardianMesh.vertices;
        _distances = new float[_vertices.Length / 2];
        _verticesColor = new Color[_vertices.Length];

    }


    void Update()
    {
        _indexClosestVertex = 0;
        _closestVertexDistance = distToCamera(_vertices[0]);
        _distances[0] = distToCamera(_vertices[0]);
        for (int i = 1; i < _vertices.Length / 2; i++)
        {
            _distances[i] = distToCamera(_vertices[i]);
            if(_distances[i] < _closestVertexDistance)
            {
                _indexClosestVertex = i;
                _closestVertexDistance = _distances[i];
            }
        }


        _customGuardianMesh.vertices = _vertices;
        _customGuardianMesh.colors = _verticesColor;

        _customGuardianMesh.RecalculateNormals();
    }

    //This function returns the distance between a vertex and the camera position in the X/Z plan.
    private float distToCamera(Vector3 point)
    {
        return Vector2.Distance(new Vector2(point.x, point.z), new Vector2(_camera.position.x, _camera.position.z));
    }
}
