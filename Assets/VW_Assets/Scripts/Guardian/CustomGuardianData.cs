using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script contains all the data of the custom Guardian(play area) and update them.
/// </summary>
public class CustomGuardianData : MonoBehaviour
{
    //Main Camera
    private Transform _camera;

    //Mesh of the GameObject representing the Guardian.
    private Mesh _customGuardianMesh;

    //Array containing the color of each vertices.
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
    
    //Array containing the distance between the main camera and the vertices on the XZ plan.
    private float[] _distances;
    public float[] Distances
    {
        get { return _distances; }
    }

    //Minimum distance between the main camera and the vertices of the mesh.
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
        _distances = new float[_vertices.Length / 2]; //We only need half of the point because we don't consider the distance on Y axis.
        _verticesColor = new Color[_vertices.Length];

    }


    public void UpdateGuardian()
    {
        //Calculate the closest vertex.
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
