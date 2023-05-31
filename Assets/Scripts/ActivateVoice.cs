using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using UnityEngine.InputSystem;


public class ActivateVoice : MonoBehaviour
{
    [SerializeField]
    private Wit wit;

    private void Update()
    {
        if (wit == null)
            wit = GetComponent<Wit>();
    }
    public void BPress(InputAction.CallbackContext context)
    {
        if (context.performed)
            WitActivate();
    }
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

