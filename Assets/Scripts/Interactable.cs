using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private bool toggleState = false;
    [SerializeField] private UnityEvent<bool> interacted;
    [SerializeField]
    private Animator changingAnimator;
    [SerializeField]
    private string animatorField = "interactionToggle";
    public void doInteract()
    {
        toggleState = !toggleState;
        interacted.Invoke(toggleState);
        if(changingAnimator != null)
        {
            changingAnimator.SetBool(animatorField, toggleState);
        }
    }
}
