using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToGuardState : PlayerState
{
    public override PlayerState RunCurrentPlayerState()
    {
        PlayerState playerState = null;

        switch(this.playerType)
        {
            case PlayerType.Keeper:
                {
                    playerState = RunKeeperBackToGuard();
                }
                break;
            // Irrelevant!
            //case PlayerType.Beater:
            //    {
            //    }
            //    break;
            //case PlayerType.Chaser:
            //    {
            //    }
            //    break;
            //case PlayerType.Seeker:
            //    {
            //    }
            //    break;
            default:
                {
                    playerState = this;
                }
                break;
        }

        return playerState;
    }

    private PlayerState RunKeeperBackToGuard()
    {
        PlayerState nextState = new IdleState();
        //Implementation...
        return nextState;
    }
}
