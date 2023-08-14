using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldSnitchState : State
{
    [SerializeField] TeamPlayersManager teamManager;
    [SerializeField] GameObject scoreManager;
    public override State RunCurrentState()
    {
        //Activates winning and loosing animations
        PlayerTeam winningTeam = scoreManager.GetComponent<ScoreManager>().GetWinner();
        //StartCoroutine(teamManager.GoalAnimations(winningTeam));
        teamManager.SetBackAllPlayersToIdleState(winningTeam);
        int scoreTeam1 = scoreManager.GetComponent<ScoreManager>().GetScoreForTeam(teamManager.GetTeam1());
        int scoreTeam2 = scoreManager.GetComponent<ScoreManager>().GetScoreForTeam(teamManager.GetTeam2());
        teamManager.GameOver(scoreTeam1, scoreTeam2);
        Debug.Log("I HAVE CAUGHT THE SNITCH!!!!!!!!!!");
        //Activates winning and loosing animations
        return this; //TODO MAYBE VICTORY ANIMATION? + scene +closing menu
    }

}
