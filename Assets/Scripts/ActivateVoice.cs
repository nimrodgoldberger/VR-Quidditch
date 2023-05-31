using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateVoice : MonoBehaviour
{
    [SerializeField]
    private Wit wit;
    [SerializeField]
    private CustomMovement controller;


    private void FixedUpdate()
    {
        if (wit == null)
            wit = GetComponent<Wit>();
        if(controller.bIsPressed && !wit.Active)
            WitActivate();
    }

    //public void BPress(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //        WitActivate();
    //}

    //public void RightTriggerPress(InputAction.CallbackContext context)
    //{
    //    if(context.performed)
    //        WitActivate();
    //}
    public void WitActivate()
    {
        wit.Activate();
    }
}

