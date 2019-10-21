using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GFilter
{
    protected CustomGuardianData _guardian;
    protected Mesh _mesh;

    public GFilter(GameObject guardian)
    { 
        _guardian = guardian.GetComponent<CustomGuardianData>();
        Debug.Assert(_guardian, "The guardian was not instantiated");
        _mesh = guardian.GetComponent<MeshFilter>().mesh;
    }

    public abstract void Apply();
    
}
