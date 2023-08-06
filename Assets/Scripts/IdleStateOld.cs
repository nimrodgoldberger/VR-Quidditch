//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class IdleStateOld : PlayerState
//{
//    private Vector3 startingPosition;

//    private void Start()
//    {
//        startingPosition = transform.position;
//    }


//    public override PlayerState RunCurrentPlayerState()
//    {
//        PlayerState nextState = null;

//        switch(this.playerType)
//        {
//            case PlayerType.Keeper:
//                {
//                    nextState = RunKeeperIdle();
//                }
//                break;
//            case PlayerType.Beater:
//                {
//                    nextState = RunBeaterIdle();
//                }
//                break;
//            case PlayerType.Chaser:
//                {
//                    nextState = RunChaserIdle();
//                }
//                break;
//            case PlayerType.Seeker:
//                {
//                    nextState = RunSeekerIdle();
//                }
//                break;
//            default:
//                {
//                    nextState = this;
//                }
//                break;
//        }

//        return nextState;
//    }

//    private PlayerState RunKeeperIdle()
//    {
//        PlayerState nextState = this;
//        //Implementation___________________________________________________________
//        bool foundEnemy = searchForCloseOponents();

//        if(foundEnemy)
//        {
//            nextState = new DefendStateOld();
//        }
//        //_________________________________________________________________________
//        return nextState;
//    }

//    private PlayerState RunBeaterIdle()
//    {
//        PlayerState nextState = this;
//        //Implementation...
//        return nextState;
//    }

//    private PlayerState RunChaserIdle()
//    {
//        PlayerState nextState = this;
//        //Implementation...
//        return nextState;
//    }

//    private PlayerState RunSeekerIdle()
//    {
//        PlayerState nextState = this;
//        bool foundSnitch = IsSnitchClose();

//        if(foundSnitch)
//        {
//            nextState = new SeekSnitchStateOld();
//        }

//        return nextState;
//    }

//    private bool IsSnitchClose()
//    {
//        float detectionRadius = 10f;
//        float distance = Vector3.Distance(transform.position, target.transform.position);
//        return distance <= detectionRadius;
//    }




//    //private void initializeMovement()
//    //{

//    //    //if(!foundEnemy && transform.position != startingPosition)
//    //    //{
//    //    //    eState = eGuardianState.GoBack;
//    //    //}

//    //    //if(eState != eGuardianState.Idle)
//    //    //{
//    //    //    tackleTarget();
//    //    //}
//    //}


//    // TODO Change to CloseQuaffle in Enemy
//    private bool searchForCloseOponents()
//    {
//        bool found = false;
        
//        foreach(GameObject targetToDefend in targetsToDefend)
//        {
//            foreach(GameObject oponent in oponents)
//            {
//                if(IsOponentClose(targetToDefend, oponent))
//                {
//                    //eState = eGuardianState.Defend;
//                    this.target = oponent;
//                    found = true;
//                    break;
//                }
//            }

//            if(found)
//            {
//                break;
//            }
//        }

//        return found;
//    }

//    private bool IsOponentClose(GameObject targetToDefend, GameObject oponent)
//    {
//        bool isOponentClose = false;
        
//        if(Vector3.Distance(oponent.transform.position, targetToDefend.transform.position) < detectionRadius)
//        {
//            isOponentClose = true;
//        }

//        return isOponentClose;
//    }
//}
