using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for all the modifiers that need the distance between the player and the vertex.
/// </summary>
public abstract class GFilterByDistance : GFilter
{

    private float _max_dist = 2;

    private float _min_dist = 0.5f;

    protected GFilterByDistance(GameObject guardian, float max_dist,
                                float min_dist) : base(guardian)
    {
        _max_dist = max_dist;
        _min_dist = min_dist;
    }

    /// <summary>
    /// This function is a decreasing linear function that projects [min_dist_guardian,max_dist_guardian] to [0,1].
    /// </summary>
    /// <param name="dist"></param>
    /// <returns> returns 0 for max_dist_guardian and 1 for min_dist_guardian. </returns>
    protected float distRatio(float dist)
    {
        return Mathf.Lerp(_max_dist, _min_dist, dist);
    }


}
