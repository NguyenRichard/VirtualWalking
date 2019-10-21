using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GFilter
{
    protected GameObject _guardian;
    protected Mesh _mesh;

    public GFilter(GameObject guardian)
    {
        _guardian = guardian;
        _mesh = guardian.GetComponent<MeshFilter>().mesh;
    }

    public abstract void Apply();
    
}
