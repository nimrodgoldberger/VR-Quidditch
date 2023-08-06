using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public List<PlayerLogicManager> keepers1;
    public List<PlayerLogicManager> beaters1;
    public List<PlayerLogicManager> chasers1;
    public List<PlayerLogicManager> seekers1;

    public List<PlayerLogicManager> keepers2;
    public List<PlayerLogicManager> beaters2;
    public List<PlayerLogicManager> chasers2;
    public List<PlayerLogicManager> seekers2;


    public Material gryffindorMaterial; // Reference to Gryffindor material
    public Material hufflepuffMaterial; // Reference to Hufflepuff material
    public Material slytherinMaterial; // Reference to Gryffindor material
    public Material ravenclawMaterial; // Reference to Hufflepuff material

    // Set the team, HAS TO SET THE COLORS OF UNIFORMS TOO
    //public PlayerTeam team = PlayerTeam.Griffindor;
    private PlayerTeam team1;
    private PlayerTeam team2;
    private int team1Score;
    private int team2Score;

    // TODO Check with Dan
    //To set the starting points of the players, Only if needed!!!!!
    public TeamType teamType = TeamType.Team1Player;



    // Start is called before the first frame update
    void Start()
    {
        // Load the saved teams data from PlayerPrefs
        team1 = (PlayerTeam)PlayerPrefs.GetInt("Team1");
        team2 = (PlayerTeam)PlayerPrefs.GetInt("Team2");

       

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

    }

    private void initPlayerStateManagers(List<PlayerLogicManager> gameObjects, PlayerTeam team, PlayerType type)
    {
        foreach (PlayerLogicManager player in gameObjects)
        {
            player.PlayerTeam = team;
            player.PlayerType = type;
            SetPlayerTeamOutfit(player);
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

}
