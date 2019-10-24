using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GFilter
{
    protected CustomGuardianData _guardianData;
    protected Mesh _mesh;

    protected GFilter(GameObject guardian)
    { 
        _guardianData = guardian.GetComponent<CustomGuardianData>();
        Debug.Assert(_guardianData, "The guardian was not instantiated");
        _mesh = guardian.GetComponent<MeshFilter>().mesh;
    }

    public abstract void Apply();
    
}
