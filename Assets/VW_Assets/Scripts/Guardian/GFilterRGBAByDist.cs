using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFilterRGBAByDist : GFilterByDistance
{

    private Color _colorFar;

    private Color _colorNear;

    private float _max_dist = 2;

    private float _min_dist = 0.5f;

    public GFilterRGBAByDist(GameObject guardian, 
                             Color colorNear, 
                             Color colorFar,
                             float max_dist,
                             float min_dist) : base(guardian,max_dist,min_dist)
    {
        _colorFar = colorFar;
        _colorNear = colorNear;
    }

    public override void Apply()
    {
        Color[] verticesColor = _guardianData.VerticesColor;
        
        for(int i = 0; i < verticesColor.Length/2; i++)
        {
            verticesColor[i] = Color.Lerp(_colorNear, _colorFar, distRatio(_guardianData.Distances[i]));
            verticesColor[i+verticesColor.Length/2] = Color.Lerp(_colorNear, _colorFar, distRatio(_guardianData.Distances[i]));
        }
    }
}
