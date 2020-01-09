using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GFilter
{
    protected static CustomGuardianData _guardianData;

    protected GFilter(GameObject guardian)
    { 
        if(_guardianData == null)
        {
            _guardianData = guardian.GetComponent<CustomGuardianData>();
        }
        Debug.Assert(_guardianData, "The guardian was not instantiated");
    }

    public abstract void Apply();

    public static void UpdateGuardian() { _guardianData.UpdateGuardian(); }
    
}
