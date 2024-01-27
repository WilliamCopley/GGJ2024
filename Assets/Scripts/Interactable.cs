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
    [SerializeField]
    private bool isInverseAnimation = false;

    private void Start()
    {
        if (changingAnimator != null)
        {
            changingAnimator.SetBool("isInverse", isInverseAnimation);
        }
    }
    public void doInteract()
    {
        toggleState = !toggleState;
        interacted.Invoke(toggleState);
        if(changingAnimator != null)
        {
            changingAnimator.SetBool(animatorField, toggleState);
        }
    }

    public void doInteractNoInvoke()
    {
        toggleState = !toggleState;
        if (changingAnimator != null)
        {
            changingAnimator.SetBool(animatorField, toggleState);
        }
    }

}
