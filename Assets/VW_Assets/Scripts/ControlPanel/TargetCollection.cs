using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollection : MonoBehaviour
{
    [SerializeField] TunnelChoiceHandler _choiceHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            other.GetComponent<Animator>().SetTrigger("Collected");
            // Send update to TunnelInfo
            _choiceHandler.EnableButton(other.GetComponent<Target>().ID);
            Destroy(other.gameObject, 2f);
        }
    }
}
