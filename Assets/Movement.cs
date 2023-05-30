using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{

    [SerializeField] private InputActionReference triggerPull;
    [SerializeField] private InputActionReference gripPull;
    [SerializeField] float movementMultiplier = 3;
    [SerializeField] Transform front;

    Vector3 forwardMove;

    void Start()
    {
        triggerPull.action.performed += Boost;
    }

    void Update()
    {

    }

    private void Boost(InputAction.CallbackContext obj)
    {
        transform.Translate(front.forward * movementMultiplier * Time.deltaTime);
    }
}