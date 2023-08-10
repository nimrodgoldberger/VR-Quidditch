using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

// TODO Dan For multiplayer we need to check if we need to act differently for different teams
public enum TeamType
{
    Team1Player = 0,
    Team2Player = 1,
    Team1AI = 2,
    Team2AI = 3
}

public enum PlayerTeam //ORDER IS NOT TO BE CHANGED
{
    Griffindor = 0,
    Hufflepuff = 1,
    Slitheryn = 2,
    Ravenclaw = 3,
    None = 4
}

public class TeamPlayersManager : MonoBehaviour
{
    public string[] teamNames; // Array of team names (size: 4)

    public GameObject scoreText; // Reference to the Text component for displaying the team name

    public List<ScoreArea> team1Goals;
    public List<ScoreArea> team2Goals;

    public List<PlayerLogicManager> keepers1;
    public List<PlayerLogicManager> beaters1;
    public List<PlayerLogicManager> chasers1;
    public List<PlayerLogicManager> seekers1;

    public List<PlayerLogicManager> keepers2;
    public List<PlayerLogicManager> beaters2;
    public List<PlayerLogicManager> chasers2;
    public List<PlayerLogicManager> seekers2;

    public List<GameObject> StartingPositionsChasersTeam1;
    public List<GameObject> StartingPositionsBeatersTeam1;
    public List<GameObject> StartingPositionsSeekerTeam1;
    public List<GameObject> StartingPositionsKeeperTeam1;

    public List<GameObject> StartingPositionsChasersTeam2;
    public List<GameObject> StartingPositionsBeatersTeam2;
    public List<GameObject> StartingPositionsSeekerTeam2;
    public List<GameObject> StartingPositionsKeeperTeam2;


    public Material gryffindorMaterial; // Reference to Gryffindor material
    public Material hufflepuffMaterial; // Reference to Hufflepuff material
    public Material slytherinMaterial; // Reference to Gryffindor material
    public Material ravenclawMaterial; // Reference to Hufflepuff material

    // Set the team, HAS TO SET THE COLORS OF UNIFORMS TOO
    private PlayerTeam team1;
    private PlayerTeam team2;
    private int team1Score;
    private int team2Score;

    // TODO Check with Dan
    //To set the starting points of the players, Only if needed!!!!!
    public TeamType teamType = TeamType.Team1Player;

    private bool playersInitialized = false; // Keep track if players are already initialized

   

    // Start is called before the first frame update
    void Start()
    {
        if(!playersInitialized)
        {
            // Load the saved teams data from PlayerPrefs
            team1 = (PlayerTeam)PlayerPrefs.GetInt("Team1");
            team2 = (PlayerTeam)PlayerPrefs.GetInt("Team2");

            AssignGoalsToTeams(team1Goals, team2Goals);

            //TODO initialize according to XR origin team and according to multiplayer

            //INIT TEAM 1
            initPlayerStateManagers(keepers1, team1, PlayerType.Keeper);
            initPlayerStateManagers(beaters1, team1, PlayerType.Beater);
            initPlayerStateManagers(chasers1, team1, PlayerType.Chaser);
            initPlayerStateManagers(seekers1, team1, PlayerType.Seeker);

            //INIT TEAM 2
            initPlayerStateManagers(keepers2, team2, PlayerType.Keeper);
            initPlayerStateManagers(beaters2, team2, PlayerType.Beater);
            initPlayerStateManagers(chasers2, team2, PlayerType.Chaser);
            initPlayerStateManagers(seekers2, team2, PlayerType.Seeker);

            playersInitialized = true;

            
        }

    }

    

    private void initPlayerStateManagers(List<PlayerLogicManager> players, PlayerTeam team, PlayerType type)
    {
        int chaserIndex = 0;
        int beaterIndex = 0;

        foreach (PlayerLogicManager player in players)
        {
            player.PlayerTeam = team;
            player.PlayerType = type;
            SetPlayerTeamOutfit(player);
            switch (player.PlayerType)
            {
                case PlayerType.Beater:
                    SetPlayerStartingPos(player, beaterIndex);
                    beaterIndex++;
                    break;
                case PlayerType.Chaser:
                    SetPlayerStartingPos(player, chaserIndex);
                    chaserIndex++;
                    break;
                default:
                    SetPlayerStartingPos(player, 0);
                    break;
            }
            if(team == team1)
            {
                player.SetGoals(team1Goals, team2Goals);
            }
            if(team == team2)
            {
                player.SetGoals(team2Goals, team1Goals);
            }
            
        }
    }


    public void SetPlayerStartingPos(PlayerLogicManager player, int typeIndex)
    {
        if(player.PlayerTeam == team1)
        {
            switch (player.PlayerType)
            {
                case PlayerType.Beater:
                    player.startingPosition = StartingPositionsBeatersTeam1[typeIndex].transform.position;
                    break;
                case PlayerType.Chaser:
                    player.startingPosition = StartingPositionsChasersTeam1[typeIndex].transform.position;
                    break;
                case PlayerType.Seeker:
                    player.startingPosition = StartingPositionsSeekerTeam1[typeIndex].transform.position;
                    break;
                case PlayerType.Keeper:
                    player.startingPosition = StartingPositionsKeeperTeam1[typeIndex].transform.position;
                    break;
            }
        }
        else if (player.PlayerTeam == team2)
        {
            switch (player.PlayerType)
            {
                case PlayerType.Beater:
                    player.transform.position = StartingPositionsBeatersTeam2[typeIndex].transform.position;
                    break;
                case PlayerType.Chaser:
                    player.transform.position = StartingPositionsChasersTeam2[typeIndex].transform.position;
                    break;
                case PlayerType.Seeker:
                    player.transform.position = StartingPositionsSeekerTeam2[typeIndex].transform.position;
                    break;
                case PlayerType.Keeper:
                    player.transform.position = StartingPositionsKeeperTeam2[typeIndex].transform.position;
                    break;
            }
        }

    }

    // Call this method to change the material of the body based on the team
    public void SetPlayerTeamOutfit(PlayerLogicManager player)
    {
        
        Transform bodyTransform = player.transform.Find("Body");
        Renderer bodyRenderer = bodyTransform.GetComponent<Renderer>();

        // Get the materials array from the Renderer
        Material[] materials = bodyRenderer.materials;

        // Check the team and assign the appropriate material
        switch (player.PlayerTeam)
        {
            case PlayerTeam.Griffindor:
                materials[2] = gryffindorMaterial;
                break;
            case PlayerTeam.Hufflepuff:
                materials[2] = hufflepuffMaterial;
                break;
            case PlayerTeam.Slitheryn:
                materials[2] = slytherinMaterial;
                break;
            case PlayerTeam.Ravenclaw:
                materials[2] = ravenclawMaterial;
                break;
        }

        // Apply the modified materials array back to the Renderer
        bodyRenderer.materials = materials;

    }

    public PlayerTeam GetTeam1()
    {
        return team1;
    }

    public PlayerTeam GetTeam2()
    {
        return team2;
    }

    public void AssignGoalsToTeams(List<ScoreArea> team1Goals, List<ScoreArea> team2Goals)
    {
        foreach (ScoreArea goal in team1Goals)
        {
            goal.SetTeam(team1);
        }

        foreach (ScoreArea goal in team2Goals)
        {
            goal.SetTeam(team2);
        }
    }

    
}
