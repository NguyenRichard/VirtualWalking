using UnityEngine;

public class GFilterHeightByDist : GFilterByDistance 
{
    private float _max_height = 5;

    private float _min_height = 0.5f;

    private float _max_dist = 2;

    private float _min_dist = 0.5f;

    public GFilterHeightByDist(GameObject guardian, 
                                float max_height,
                                float min_height,
                                float max_dist,
                                float min_dist) : base(guardian, max_dist, min_dist)
    {
        _max_height = max_height;
        _min_height = min_height;
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
            return _min_height + (_max_height-_min_height) * distRatio(dist);
        }
    }

    public override void Apply()
    {

        Debug.Assert(_guardianData, "The guardian was not instantiated");
        Vector3[] vertices = _guardianData.Vertices;

        for (int i = 0; i < vertices.Length/2; i++)
        {
            //vertices[i+vertices.Length / 2].y = heightFromDist(_guardianData.Distances[i]);
            vertices[i + vertices.Length / 2].y = heightFromDist(_guardianData.ClosestVertexDistance);
        }
    }

}
