using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, writePerm: NetworkVariableWritePermission.Owner);

    [Header("Movement")]
    [SerializeField] float movementMultiplier = 0.08f;
    private float moveSpeed = 30f;

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (previousValue, newValue) =>
        {
            Debug.Log(OwnerClientId + "; randomNumber: " + randomNumber.Value);
        };
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            randomNumber.Value = Random.Range(0, 100);
            TestClientRpc(randomNumber.Value);
        }

        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(KeyCode.W)) moveDir.z = +5f;
        if (Input.GetKeyDown(KeyCode.S)) moveDir.z = -5f;
        if (Input.GetKeyDown(KeyCode.A)) moveDir.x = -5f;
        if (Input.GetKeyDown(KeyCode.D)) moveDir.x = +5f;


        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    [ServerRpc]
    private void TestServerRpc(int testVariable)
    {
        Debug.Log($"[{OwnerClientId}]Server Rpc Invoked: " + testVariable);
    }

    [ClientRpc]
    private void TestClientRpc(int testVariable)
    {
        Debug.Log($"[{OwnerClientId}]Server Rpc Invoked: " + testVariable);
    }


    public void MoveRight()
    {
        
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        transform.position += transform.right * movementMultiplier;
        Debug.Log("MoveRight: " + transform.position);
    } 

    public void MoveLeft()
    {
        Debug.Log("MoveLeft: " + transform.position);
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}

