using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    protected List<GameObject> components;

    [SerializeField]
    [OnChangedCall("SwitchActivateState")]
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


    // Start is called before the first frame update
    void Start()
    {
        InitScene();   
    }

    protected abstract void InitScene();

    private void OnEnable()
    {
        SwitchActiveState();
    }

    private void OnDisable()
    {
        Desactivate();
    }

    private void SwitchActiveState()
    {
        if (isActive)
        {
            Desactivate();
        }
        else
        {
            Activate();
        }
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

}
