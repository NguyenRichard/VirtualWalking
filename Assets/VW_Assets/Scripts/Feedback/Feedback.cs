using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This abstract class defines how feedback are managed in the scene.
/// It allows users to change all parameters in inspector as well as initializing the scene by adding all the prefab required for the feedback.
/// </summary>
public abstract class Feedback : MonoBehaviour
{
    //liste des GameObjects correspondant à des feedbacks. Permettent e désactiver les feedbacks
    protected List<GameObject> components;

    protected bool isInit = false;

    [SerializeField]
    private bool isActive = true;
    public bool IsActive
    {
        get { return isActive; }
        set
        {
            isActive = value;
            SwitchActiveState();
        }
    }

    //Update le statut d'un feedback
    public void SwitchActiveState()
    { 
        if (isActive)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        components = new List<GameObject>();
        InitScene();
        Deactivate();
    }

    /// <summary>
    /// Adds all the required prefabs for the feedback.
    /// DON'T FORGET: to set isInit = true at the end of the function.
    /// </summary>
    protected abstract void InitScene();

    private void OnEnable()
    {
        SwitchActiveState();
    }

    private void OnDisable()
    {
        Deactivate();
    }

    private void Activate()
    {
        foreach(var component in components){
            if(component != null)
            {
                component.SetActive(true);
            }

        }
    }
    private void Deactivate()
    {
        foreach(var component in components)
        {
            if (component != null)
            {
                component.SetActive(false);
            }
        }
    }

    //fonction updatant le feedback, ainsi que ses parametres si necessaire.
    protected abstract void UpdateParameters();

    private void OnValidate()
    {
        if (isInit)
        {
            UpdateParameters();
        }
    }

    private void OnDestroy()
    {
        foreach(var component in components)
        {
            if(component != null)
            {
                Destroy(component);
            }
        }
    }

}
