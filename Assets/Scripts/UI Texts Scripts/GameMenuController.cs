using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuController : MonoBehaviour
{

    [SerializeField] public GameObject menu;
    [SerializeField] public InputActionProperty showButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}
