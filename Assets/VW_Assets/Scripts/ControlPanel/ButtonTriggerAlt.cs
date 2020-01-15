using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerAlt : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandR") || other.CompareTag("HandL"))
        {
            transform.parent.GetComponent<TunnelChoiceHandler>().ChangeTunnel(id);
            ChangeButtonState();
        }
    }

    void ChangeButtonState()
    {
        // Rajouter effet bouton enfoncé

    }
}
