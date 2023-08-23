using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    private List<PlayerLogicManager> allPlayers = new List<PlayerLogicManager>();

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

    private PlayerTeam scoringTeam;
    private float effectDuration = 5.0f;

    // TODO Check with Dan
    //To set the starting points of the players, Only if needed!!!!!
    public TeamType teamType = TeamType.Team1Player;

    private bool playersInitialized = false; // Keep track if players are already initialized
    [SerializeField] private BallsPositionManager ballsPositionManager;

    public static int onlinePlayerCount = 0;

    void Start()
    {
        //if (!(SceneManager.GetActiveScene().name == "Online"))
        //{
            if (!playersInitialized)
            {
                // Load the saved teams data from PlayerPrefs
                team1 = (PlayerTeam)PlayerPrefs.GetInt("Team1");
                team2 = (PlayerTeam)PlayerPrefs.GetInt("Team2");

                if (team1 == team2)
                {
                    team2 = (PlayerTeam)(((int)team2 + 1) % 3); //modulo 3 to get another team every time
                }

                AssignGoalsToTeams(team1Goals, team2Goals);

                //TODO initialize according to XR origin team and according to multiplayer

                //INIT TEAM 1
                initPlayerStateManagers(keepers1, team1, PlayerType.Keeper);
                initPlayerStateManagers(beaters1, team1, PlayerType.Beater);
                initPlayerStateManagers(chasers1, team1, PlayerType.Chaser);
                initPlayerStateManagers(seekers1, team1, PlayerType.VRPlayer);

                //TODO for multi do this for both teams

                //INIT TEAM 2
                initPlayerStateManagers(keepers2, team2, PlayerType.Keeper);
                initPlayerStateManagers(beaters2, team2, PlayerType.Beater);
                initPlayerStateManagers(chasers2, team2, PlayerType.Chaser);
                initPlayerStateManagers(seekers2, team2, PlayerType.Seeker);

                playersInitialized = true;
                ballsPositionManager.StartingGame();
            }
        //}
    }



    private void initPlayerStateManagers(List<PlayerLogicManager> players, PlayerTeam team, PlayerType type)
    {
        int chaserIndex = 0;
        int beaterIndex = 0;

        foreach(PlayerLogicManager player in players)
        {
            player.PlayerTeam = team;
            player.PlayerType = type;

            SetPlayerTeamOutfit(player);
            switch(player.PlayerType)
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
            switch(player.PlayerType)
            {
                case PlayerType.Beater:
                    player.startingPosition = StartingPositionsBeatersTeam1[typeIndex].transform.position;
                    break;
                case PlayerType.Chaser:
                    player.startingPosition = StartingPositionsChasersTeam1[typeIndex].transform.position;
                    break;
                case PlayerType.Seeker:
                case PlayerType.VRPlayer:
                    player.startingPosition = StartingPositionsSeekerTeam1[typeIndex].transform.position;
                    break;
                case PlayerType.Keeper:
                    player.startingPosition = StartingPositionsKeeperTeam1[typeIndex].transform.position;
                    break;
            }

            player.transform.position = player.startingPosition;
        }
        else if(player.PlayerTeam == team2)
        {
            switch(player.PlayerType)
            {
                case PlayerType.Beater:
                    player.startingPosition = StartingPositionsBeatersTeam2[typeIndex].transform.position;
                    break;
                case PlayerType.Chaser:
                    player.startingPosition = StartingPositionsChasersTeam2[typeIndex].transform.position;
                    break;
                case PlayerType.Seeker:
                case PlayerType.VRPlayer:
                    player.startingPosition = StartingPositionsSeekerTeam2[typeIndex].transform.position;
                    break;
                case PlayerType.Keeper:
                    player.startingPosition = StartingPositionsKeeperTeam2[typeIndex].transform.position;
                    break;
            }

            player.transform.position = player.startingPosition;
        }
    }

    private void Update()
    {
        //if (onlinePlayerCount == 2 && playersInitialized == false && seekers2.Count == 1)
        //{
        //    initializeOnline();
        //}
    }
    // Call this method to change the material of the body based on the team
    public void SetPlayerTeamOutfit(PlayerLogicManager player)
    {
        if(player.PlayerType!= PlayerType.VRPlayer)
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
        foreach(ScoreArea goal in team1Goals)
        {
            goal.SetTeam(team1);
        }

        foreach(ScoreArea goal in team2Goals)
        {
            goal.SetTeam(team2);
        }
    }

    public IEnumerator GoalAnimations()
    {
        //SetBackAllPlayersToIdleState();
        
        float startTime = Time.time;

        while(Time.time - startTime < effectDuration)
        {
            if(scoringTeam == team1)
            {
                LoosingAnimations(keepers2);
                LoosingAnimations(beaters2);
                LoosingAnimations(chasers2);
                LoosingAnimations(seekers2);

                WinningAnimations(keepers1);
                WinningAnimations(beaters1);
                WinningAnimations(chasers1);
                WinningAnimations(seekers1);
            }
            else if(scoringTeam == team2)
            {
                LoosingAnimations(keepers1);
                LoosingAnimations(beaters1);
                LoosingAnimations(chasers1);
                LoosingAnimations(seekers1);

                WinningAnimations(keepers2);
                WinningAnimations(beaters2);
                WinningAnimations(chasers2);
                WinningAnimations(seekers2);
            }
            else //TIE
            {
                WinningAnimations(keepers1);
                WinningAnimations(beaters1);
                WinningAnimations(chasers1);
                WinningAnimations(seekers1);

                WinningAnimations(keepers2);
                WinningAnimations(beaters2);
                WinningAnimations(chasers2);
                WinningAnimations(seekers2);
            }
            yield return null;
        }

        if(scoringTeam == team1)
        {

            StopLoosingAnimations(keepers2);
            StopLoosingAnimations(beaters2);
            StopLoosingAnimations(chasers2);
            StopLoosingAnimations(seekers2);

            StopWinningAnimations(keepers1);
            StopWinningAnimations(beaters1);
            StopWinningAnimations(chasers1);
            StopWinningAnimations(seekers1);


        }
        else if(scoringTeam == team2)
        {
            StopLoosingAnimations(keepers1);
            StopLoosingAnimations(beaters1);
            StopLoosingAnimations(chasers1);
            StopLoosingAnimations(seekers1);

            StopWinningAnimations(keepers2);
            StopWinningAnimations(beaters2);
            StopWinningAnimations(chasers2);
            StopWinningAnimations(seekers2);
        }
        else //TIE
        {
            StopWinningAnimations(keepers1);
            StopWinningAnimations(beaters1);
            StopWinningAnimations(chasers1);
            StopWinningAnimations(seekers1);

            StopWinningAnimations(keepers2);
            StopWinningAnimations(beaters2);
            StopWinningAnimations(chasers2);
            StopWinningAnimations(seekers2);
        }


    }

    public void WinningAnimations(List<PlayerLogicManager> players)
    {
        Animator animator;
        foreach (PlayerLogicManager player in players)
        {
            player.speed = 0;
            animator = player.GetAnimator();
            if (animator != null)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Stupefy", false);
                animator.SetBool("Winner", true);
            }
        }
    }
    public void LoosingAnimations(List<PlayerLogicManager> players)
    {
        Animator animator;
        foreach (PlayerLogicManager player in players)
        {
            player.speed = 0;
            animator = player.GetAnimator();
            if (animator != null)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Stupefy", false);
                animator.SetBool("Loser", true);
            }
        }
    }

    public void StopWinningAnimations(List<PlayerLogicManager> players)
    {
        Animator animator;
        foreach (PlayerLogicManager player in players)
        {
            animator = player.GetAnimator();
            if (animator != null)
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Stupefy", false);
                animator.SetBool("Winner", false);
            }
            player.speed = 15;
        }
    }
    public void StopLoosingAnimations(List<PlayerLogicManager> players)
    {
        Animator animator;
        foreach (PlayerLogicManager player in players)
        {
            animator = player.GetAnimator();
            if (animator != null)
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Stupefy", false);
                animator.SetBool("Loser", false);
            }
            player.speed = 15;
        }
    }

    public void GameOver(int team1Score, int team2Score)
    {
        //pass scores and teams over to next scene
        PlayerPrefs.SetInt("Team1", (int)team1);
        PlayerPrefs.SetInt("Team2", (int)team2);
        PlayerPrefs.SetInt("Team1Score", team1Score);
        PlayerPrefs.SetInt("Team2Score", team2Score);
        PlayerPrefs.SetInt("IsOver", 1);

        SceneManager.LoadScene("MenuScene");
    }

    public void SetBackAllPlayersToIdleState(PlayerTeam currScoringTeam)
    {
        scoringTeam = currScoringTeam;

        SetPlayerListToIdleState(keepers1);
        SetPlayerListToIdleState(beaters1);
        SetPlayerListToIdleState(chasers1);
        SetPlayerListToIdleState(seekers1);

        SetPlayerListToIdleState(keepers2);
        SetPlayerListToIdleState(beaters2);
        SetPlayerListToIdleState(chasers2);
        SetPlayerListToIdleState(seekers2);

        //WaitForAllPlayersToBeInIdle();
    }


    private void SetPlayerListToIdleState(List<PlayerLogicManager> players)
    {
        foreach (PlayerLogicManager player in players)
        {
            if(player.PlayerType != PlayerType.VRPlayer)//?
            {
                player.goalScored = true;
                player.scoringTeam = scoringTeam;
            }
        }
    }

    private void WaitForAllPlayersToBeInIdle()
    {
        CombineLists();
        foreach (PlayerLogicManager player in allPlayers)
        {
            if (player.PlayerType != PlayerType.VRPlayer)
            {
                while (player.goalScored);
            }
        }
    }

    private void CombineLists()
    {
        allPlayers.AddRange(keepers1);
        allPlayers.AddRange(beaters1);
        allPlayers.AddRange(chasers1);
        allPlayers.AddRange(seekers1);

        allPlayers.AddRange(keepers2);
        allPlayers.AddRange(beaters2);
        allPlayers.AddRange(chasers2);
        allPlayers.AddRange(seekers2);
    }

    public void initializeOnline()
    {
        if (!playersInitialized)
        {
            // Load the saved teams data from PlayerPrefs
            team1 = (PlayerTeam)PlayerPrefs.GetInt("Team1");
            team2 = (PlayerTeam)PlayerPrefs.GetInt("Team2");

            if (team1 == team2)
            {
                team2 = (PlayerTeam)(((int)team2 + 1) % 3); //modulo 3 to get another team every time
            }

            AssignGoalsToTeams(team1Goals, team2Goals);

            //TODO initialize according to XR origin team and according to multiplayer

            //INIT TEAM 1
            initPlayerStateManagers(keepers1, team1, PlayerType.Keeper);
            initPlayerStateManagers(beaters1, team1, PlayerType.Beater);
            initPlayerStateManagers(chasers1, team1, PlayerType.Chaser);
            initPlayerStateManagers(seekers1, team1, PlayerType.VRPlayer);

            //TODO for multi do this for both teams

            //INIT TEAM 2
            initPlayerStateManagers(keepers2, team2, PlayerType.Keeper);
            initPlayerStateManagers(beaters2, team2, PlayerType.Beater);
            initPlayerStateManagers(chasers2, team2, PlayerType.Chaser);
            initPlayerStateManagers(seekers2, team2, PlayerType.VRPlayer);

            playersInitialized = true;
            ballsPositionManager.StartingGame();
        }
    }
}





