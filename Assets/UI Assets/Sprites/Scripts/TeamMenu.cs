using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TeamMenu : MonoBehaviour
{
    public string[] teamNames; // Array of team names (size: 4)
    public string[] OpponentNames; // Array of team names (size: 4)
    public Sprite[] teamSprites; // Array of team images (size: 4)

    public GameObject teamNameText; // Reference to the Text component for displaying the team name
    public GameObject teamImageObject; // Reference to the GameObject with RawImage component

    public GameObject opponentTeamNameText; // Reference to the Text component for displaying the team name
    public GameObject opponentTeamImageObject; // Reference to the GameObject with RawImage component

    public GameObject congratulationsMenu; // Reference to the GameObject with RawImage component
    public GameObject HatThinkingScreen; // Reference to the GameObject with RawImage component
    public GameObject TeamSortingSection; // Reference to the GameObject with RawImage component

    public void OnButtonClick()
    {
        //Debug.Log("Sorting hat chooses your house!");

        //Deactivate the TeamSortingSection menu
        TeamSortingSection.SetActive(false);


        // Activate the HatThinkingScreen
        HatThinkingScreen.SetActive(true);


        // Wait for 4 seconds and than moving to next scene and choosing teams
        StartCoroutine(WaitAndShowCongratulations(4f));
    }

    private IEnumerator WaitAndShowCongratulations(float waitTime)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        // Deactivate the HatThinkingScreen
        HatThinkingScreen.SetActive(false);

        // Activate the Congratulations menu
        congratulationsMenu.SetActive(true);

        // Randomly choose a team index
        int chosenTeamIndex = Random.Range(0, 4);
        int opponentTeamIndex;

        // Randomly choose an opponent team index
        do
        {
            opponentTeamIndex = Random.Range(0, 4);
        } while (opponentTeamIndex == chosenTeamIndex);

        // In order to pass the teams over to the main scene
        SaveTeamsData(chosenTeamIndex, opponentTeamIndex);

        // Set the chosen team's image using RawImage component
        teamImageObject.GetComponent<RawImage>().texture = teamSprites[chosenTeamIndex].texture;

        // Set the chosen team's name and image
        teamNameText.GetComponent<TextMeshProUGUI>().text = teamNames[chosenTeamIndex];

        // Set the opponents team's image using RawImage component
        opponentTeamImageObject.GetComponent<RawImage>().texture = teamSprites[opponentTeamIndex].texture;

        // Set the opponents team's name and image
        opponentTeamNameText.GetComponent<TextMeshProUGUI>().text = OpponentNames[opponentTeamIndex];
    }

    public void SaveTeamsData(int team1, int team2)
    {
        PlayerPrefs.SetInt("Team1", team1);
        PlayerPrefs.SetInt("Team1", team1);
    }

}



