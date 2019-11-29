using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public bool enabled = false;
    public int id;
    private void OnTriggerEnter(Collider other)
    {
        enabled = !enabled;
        transform.parent.SendMessage("UpdateButtonState");
    }
}
