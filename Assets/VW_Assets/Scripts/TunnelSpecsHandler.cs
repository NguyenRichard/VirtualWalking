using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelSpecsHandler : MonoBehaviour
{
    [SerializeField] ButtonHandler _wallButtons;
    [SerializeField] ButtonHandler _ceilingButtons;
    [SerializeField] GameObject[] _walls;
    [SerializeField] GameObject _ceiling;
    [SerializeField] float _ceilingHeightStep = .5f;
    [SerializeField] float _wallWidthStep = .25f;
    
    void Setup()
    {
        WallSetup(_wallButtons.setupId);
        CeilingSetup(_ceilingButtons.setupId);
    }


    void WallSetup(int id)
    {
        _walls[0].transform.position = new Vector3(_wallWidthStep * id, transform.position.y, transform.position.z);
        _walls[0].transform.position = new Vector3(_wallWidthStep * id, transform.position.y, transform.position.z);
    }

    void CeilingSetup(int id)
    {
        _ceiling.transform.position = new Vector3(transform.position.x, 1 + _ceilingHeightStep * id, transform.position.z);
    }


}
