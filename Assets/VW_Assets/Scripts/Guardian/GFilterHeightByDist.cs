using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFilterHeightByDist : GFilter 
{
    private float _max_height = 5;

    private float _min_height = 0.5f;

    private float _max_dist = 2;

    private float _min_dist = 0.5f;

    private Camera main_camera;

    public GFilterHeightByDist(GameObject guardian, 
                                float max_height,
                                float min_height,
                                float max_dist,
                                float min_dist) : base(guardian)
    {
        _max_dist = max_dist;
        _min_dist = min_dist;
        _max_height = max_height;
        _min_height = min_height;
        main_camera = Camera.main;
    }


    //This function returns the distance between a vertex and the camera position in the X/Z plan.
    float distToCamera(Vector3 point)
    {
        return Vector2.Distance(new Vector2(point.x, point.z), new Vector2(main_camera.transform.position.x, main_camera.transform.position.z));
    }

    //This function is a decreasing linear function that projects [min_dist_guardian,max_dist_guardian] to [0,1].
    float distRatio(float dist)
    {
        return (dist - _max_dist) / (_min_dist - _max_dist);
    }

    float heightFromDist(float dist)
    {
        if (dist <= _min_dist)
        {
            return _max_height;
        }
        else if (dist >= _max_dist)
        {
            return _min_height;
        }
        else
        {
            return _min_height + (_max_dist - _min_dist) * distRatio(dist);
        }
    }

    public override void Apply()
    {

        Debug.Assert(_guardian, "The guardian was not instantiated");
        Vector3[] vertices = _mesh.vertices;

        for (int i = vertices.Length / 2; i < vertices.Length; i++)
        {
            vertices[i].y = heightFromDist(distToCamera(vertices[i]));
        }

        _mesh.vertices = vertices;
        _mesh.RecalculateNormals();
    }

}
