using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return (dist - _max_dist) / (_min_dist - _max_dist);
    }


}
