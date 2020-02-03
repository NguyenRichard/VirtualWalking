using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe permettant de stocker des informations sur les distances d'un collider associé au script par rapport aux murs
/// </summary>
public class WallData
{

    Vector3 wallClosestPoint;

    /// <summary>
    /// Vecteur contenant le point du mur le plus proche du collider associé
    /// </summary>
    public Vector3 WallClosestPoint
    {
        get { return wallClosestPoint; }
        set { wallClosestPoint = value; }
    }

    private Vector3 direction;

    /// <summary>
    /// Vecteur contenant la direction entre le collider associé et le point du mur le plus proche de celui-ci
    /// </summary>
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    /// <summary>
    /// Contructeur de WallData
    /// </summary>
    public WallData()
    {
        wallClosestPoint = Vector3.zero;
        direction = Vector3.zero;
    }
}
