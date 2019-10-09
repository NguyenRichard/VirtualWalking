using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class GameManagerTest : MonoBehaviour
{
    public GameObject cubeprefab;
    public OVRBoundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        boundary =  OVRManager.boundary;
        for(int i = 0; i < boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary).Length; i++)
        {
            Vector3 vec = boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary)[i];
            vec.y = 0;
            Instantiate(cubeprefab, vec , Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
