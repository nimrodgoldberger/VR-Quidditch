using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO Dan For multiplayer we need to check if we need to act differently for different teams
public enum TeamType
{
    Team1Player=0,
    Team2Player=1,
    Team1AI=2,
    Team2AI=3
}

public enum PlayerTeam
{
    Griffindor = 0,
    Hufflepuff = 1,
    Ravenclaw = 2,
    Slitheryn = 3
}

public class TeamPlayersManager : MonoBehaviour
{
    public PlayerStateManager[] keepers;
    public PlayerStateManager[] beaters;
    public PlayerStateManager[] chasers;
    public PlayerStateManager[] seekers;

    // Set the team, HAS TO SET THE COLORS OF UNIFORMS TOO
    public PlayerTeam team;
    
    // TODO Check with Dan
    //To set the starting points of the players, Only if needed!!!!!
    public TeamType teamType;



    // Start is called before the first frame update
    void Start()
    {
        initPlayerStateManagers(keepers,PlayerType.Keeper);
        initPlayerStateManagers(beaters,PlayerType.Beater);
        initPlayerStateManagers(chasers,PlayerType.Chaser);
        initPlayerStateManagers(seekers,PlayerType.Seeker);
    }

    private void initPlayerStateManagers(PlayerStateManager[] players, PlayerType type)
    {
        foreach(PlayerStateManager player in players)
        {
            player.team = team;
            player.playerType = type;
        }
    }
}
