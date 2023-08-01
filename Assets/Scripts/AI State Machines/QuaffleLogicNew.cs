using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaffleLogicNew : Targetable
{
    public float takeDistance = 2f;
    public float takeTime = 0.5f;
    private float takeTimer = 0f;
    private bool isQuaffleHeld = false;
    private PlayerTeam heldBy = PlayerTeam.None;

    // TODO For passing and throwing
    [SerializeField] public GameObject target;
    [SerializeField] private float movementSpeed = 50f;


    private void FixedUpdate()
    {
        
    }


    public bool CanBeTaken(GameObject potentialHolder)
    {
        // Check if the potential holder is within range of the ball
        return Vector3.Distance(transform.position, potentialHolder.transform.position) < takeDistance;
    }

    public bool TryTakeQuaffle(PlayerLogicManager player)
    {
        bool result = false;

        if(heldBy == PlayerTeam.None)
        {
            result= TakeQuaffle(player);
        }
        else if(heldBy != player.PlayerTeam)
        {
            // TODO Make 0.5 seconds pass before taking it;
            result = TakeQuaffle(player);
        }

        return result;
    }

    private bool TakeQuaffle(PlayerLogicManager player)
    {
        if(!isQuaffleHeld && Vector3.Distance(transform.position, player.transform.position) <= takeDistance)
        {
            isQuaffleHeld = true;
            heldBy = player.PlayerTeam;
            transform.SetParent(player.transform);
            transform.position = new Vector3(2f, 0f, 0f);
        }

        return isQuaffleHeld;
    }

    


}
