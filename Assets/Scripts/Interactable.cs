using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private bool toggleState = false;
    [SerializeField] private UnityEvent<bool> interacted;
    
    public void doInteract()
    {
        toggleState = !toggleState;
        interacted.Invoke(toggleState);
    }
}
