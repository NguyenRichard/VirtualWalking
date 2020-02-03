using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInWall : MonoBehaviour
{

    //Script a appliquer sur un gameObject contenant un Sphere Collider
    //Le booleen isInWall prend la valeur true si cette sphere entre dans un objet possédant le tag wall.

    private float distMax = 0.20f;

    public float DistMax
    {
        get { return distMax; }

        set
        {
            distMax = value;
        }
    }

    [SerializeField]
    private AnimationCurve opacityCurb;

    //courbe d'évolution de l'opacité de l'écran noir en fonction de la distance
    public AnimationCurve OpacityCurb
    {
        get { return opacityCurb; }
        set { opacityCurb = value; }
    }
    
    //Booléen corespondant au fait que la caméra est dans le mur ou non
    [SerializeField]
    private bool isInWall = false;
    //Float correspondant à la distance entre la caméra et le mur
    private float distToWall = 0;

    //Int correspondant au nombre de murs dans lequel se trouve la caméra
    int numberOfInWall = 0;

    //Gameobject correspondant au blackscreen à instancier
    public GameObject blackScreen;

    //Si la caméra est dans le mur, affiche un écran noir.
    //Si elle est proche, affiche l'écran noir avec un niveau de transparence variant en fonction la distance au mur
    //Sinon, désactive l'écran noir
    void Update()
    {
        if (isInWall)
        {
            blackScreen.SetActive(true);
            Color baseColor = blackScreen.GetComponent<Renderer>().material.color;
            baseColor.a = 1;
            blackScreen.GetComponent<Renderer>().material.color = baseColor;
        }
        
        else if(WallDistToPlayer.closestWallHead != null)
        {
            blackScreen.SetActive(true);
            distToWall = Vector3.Magnitude(WallDistToPlayer.closestWallHead.Direction);
            Color baseColor = blackScreen.GetComponent<Renderer>().material.color;
            baseColor.a = CalculateOpacity(distToWall);
            blackScreen.GetComponent<Renderer>().material.color = baseColor;
            
        }
        else
        {
            blackScreen.SetActive(false);
        }
    }

    //Calcule l'opacité à donner à l'écran noir en fonction de la distance
    private float CalculateOpacity(float distance)
    {
        float value = Mathf.Lerp(1, 0, Mathf.InverseLerp(0, distMax, distToWall));

        value = opacityCurb.Evaluate(value);

        if(value > 1)
        {
            value = 1;
        }
        else if(value < 0)
        {
            value = 0;
        }

        return value;
    }

    //Si on rentre dans un mur, met isInWall à true si c'est le premier mur avec lequel on rentre en contact et augmente le nombre de murs en collision de 1
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            numberOfInWall++;
            if (numberOfInWall == 1)
            {
                other.gameObject.GetComponent<Renderer>().enabled = false;
                isInWall = true;
            }
        }
    }

    //Si on sort d'un mur, diminue le nombre de mur en collision, et si ce nombre devient 1 passe IsInWall à false
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            numberOfInWall--;
            other.gameObject.GetComponent<Renderer>().enabled = true;
            if (numberOfInWall == 0)
            {
                isInWall = false;
            }
        }
    }
}
