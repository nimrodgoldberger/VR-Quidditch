using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO Dan For multiplayer we need to check if we need to act differently for different teams
public enum TeamType
{
    Team1Player = 0,
    Team2Player = 1,
    Team1AI = 2,
    Team2AI = 3
}

public enum PlayerTeam
{
    Griffindor = 0,
    Hufflepuff = 1,
    Ravenclaw = 2,
    Slitheryn = 3,
    None = 4
}

public class TeamPlayersManager : MonoBehaviour
{
    public List<GameObject> keepers;
    public List<GameObject> beaters;
    public List<GameObject> chasers;
    public List<GameObject> seekers;
    private List<StateManager> teamKeepers;
    private List<StateManager> teamBeaters;
    private List<StateManager> teamChasers;
    private List<StateManager> teamSeekers;

    // Set the team, HAS TO SET THE COLORS OF UNIFORMS TOO
    public PlayerTeam team = PlayerTeam.Griffindor;

    // TODO Check with Dan
    //To set the starting points of the players, Only if needed!!!!!
    public TeamType teamType = TeamType.Team1Player;



    // Start is called before the first frame update
    void Start()
    {
        //initPlayerStateManagers(keepers, teamKeepers, PlayerType.Keeper);
        //initPlayerStateManagers(beaters, teamBeaters, PlayerType.Beater);
        //initPlayerStateManagers(chasers, teamChasers, PlayerType.Chaser);
        //initPlayerStateManagers(seekers, teamSeekers, PlayerType.Seeker);
    }

    //private void initPlayerStateManagers(List<GameObject> gameObjects, List<StateManager> players, PlayerType type)
    //{
    //    foreach(GameObject gameObject in gameObjects)
    //    {

            
    //        player.team = team;
    //        player.playerType = type;
    //        //COLOR + LOCATION
    //        players.Add(player);
    //    }
    //}
}
