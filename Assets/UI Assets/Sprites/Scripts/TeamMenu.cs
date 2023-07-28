using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject TeamSortingSection; // Reference to the GameObject with RawImage component

    public void OnButtonClick()
    {
        Debug.Log("Sorting hat chooses your house!");

        //Deactivate the TeamSortingSection menu
        TeamSortingSection.SetActive(false);

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

        // Set the chosen team's image using RawImage component
        teamImageObject.GetComponent<RawImage>().texture = teamSprites[chosenTeamIndex].texture;

        // Set the chosen team's name and image
        teamNameText.GetComponent<TextMeshProUGUI>().text = teamNames[chosenTeamIndex];

        // Set the opoonent's team's image using RawImage component
        opponentTeamImageObject.GetComponent<RawImage>().texture = teamSprites[opponentTeamIndex].texture;

        // Set the opponent's team's name and image
        opponentTeamNameText.GetComponent<TextMeshProUGUI>().text = OpponentNames[opponentTeamIndex];
    }

}
