using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class keyboardMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 30f;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if(view.IsMine)
        {
            Vector3 moveDir = new Vector3(0, 0, 0);

            if (Input.GetKeyDown(KeyCode.W)) moveDir.z = +5f;
            if (Input.GetKeyDown(KeyCode.S)) moveDir.z = -5f;
            if (Input.GetKeyDown(KeyCode.A)) moveDir.x = -5f;
            if (Input.GetKeyDown(KeyCode.D)) moveDir.x = +5f;

            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }
     
}
