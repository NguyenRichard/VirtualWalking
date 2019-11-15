using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This abstract class defines how feedback are managed in the scene.
/// It allows users to change all parameters in inspector as well as initializing the scene by adding all the prefab required for the feedback.
/// </summary>
public abstract class Feedback : MonoBehaviour
{
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

    public void SwitchActiveState()
    { 
        if (isActive)
        {
            Activate();
        }
        else
        {
            Desactivate();
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        components = new List<GameObject>();
        InitScene();   
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
        Desactivate();
    }

    private void Activate()
    {
        foreach(var component in components){
            component.SetActive(true);
        }
    }
    private void Desactivate()
    {
        foreach(var component in components)
        {
            component.SetActive(false);
        }
    }

    protected abstract void UpdateParameters();

    private void OnValidate()
    {
        if (isInit)
        {
            UpdateParameters();
        }
    }

}
