using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaffleLogicNew : Targetable
{
    public float takeDistance = 2f;
    public float takeTime = 0.5f;
    private bool isQuaffleHeld = false;
    private PlayerTeam heldBy = PlayerTeam.None;
    //private float[] teamTimers = {0.0f, 0.0f, 0.0f, 0.0f};

    // TODO For passing and throwing
    [SerializeField] public GameObject target;
    [SerializeField] private float movementSpeed = 60f;


    //private void FixedUpdate()
    //{

    //}


    public bool CanBeTaken(PlayerLogicManager potentialHolder)
    {
        // Check if the potential holder is within range of the ball
        return (Vector3.Distance(transform.position, potentialHolder.transform.position) < takeDistance) && (heldBy != potentialHolder.PlayerTeam);
    }

    public bool TryTakeQuaffle(PlayerLogicManager player)
    {
        bool result = false;

        if(heldBy != player.PlayerTeam)
        {
            // TODO Check if timer works
            // 0.5 seconds pass before taking it;
            player.quaffleTakeTime += Time.fixedDeltaTime;
            if(player.quaffleTakeTime > takeTime)
            {
                result = TakeQuaffle(player);
                player.quaffleTakeTime = 0.0f;
            }
        }
        else
        {
            player.quaffleTakeTime = 0.0f;
            if(heldBy == PlayerTeam.None)
            {
                result = TakeQuaffle(player);
            }
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
