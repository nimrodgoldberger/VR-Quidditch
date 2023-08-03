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
    [SerializeField] private float movementSpeed = 60f;


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
            Vector3 relativepos = new Vector3(0.35f, 0.35f, 0.2f);
            isQuaffleHeld = true;
            heldBy = player.PlayerTeam;
            transform.SetParent(player.transform);
            transform.localPosition = relativepos;
        }
        // TODO add else if taken: Wait 0.5 seconds before taking it;

        return isQuaffleHeld;
    }

    // XROrigin TakeQuaffle

    public void FlyToTarget(Targetable newTarget)
    {
        heldBy = PlayerTeam.None;
        transform.SetParent(null);
        //1. FlyToTarget
        //2. SetTheNewParent + team...
    }

}
