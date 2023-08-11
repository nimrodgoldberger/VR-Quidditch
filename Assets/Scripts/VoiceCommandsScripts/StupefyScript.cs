using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.UI;

public class StupefyScript : MonoBehaviour
{
    //TEAM 1 IS ALWAYS XR ORIGIN
    //TODO: change for MULTI

    //ENEMIES
    public List<PlayerLogicManager> keepers2;
    public List<PlayerLogicManager> beaters2;
    public List<PlayerLogicManager> chasers2;
    public List<PlayerLogicManager> seekers2;

    public float initialSwayIntensity = 1.0f;
    public float swaySpeed = 1.0f;
    public float effectDuration = 5.0f;

    public void StupefySpell(string[] Spell) // type is string[] because the string is the phrase said in the voice command which we will recognise each word
    {
        switch (Spell[0]) //first word in the phrase recognized in the voice command. (here we only have one word)
        {
            case "Stupefy":
                StartCoroutine(Stupefy(effectDuration));
                break;
        }
    }

    private IEnumerator Stupefy(float duration)
    {
        Debug.Log("Stupefy enemies for 5 seconds!!"); // TEMPORARY
        float startTime = Time.time;

        while (Time.time - startTime < effectDuration)
        {
            ApplyLosingControlEffect(keepers2);
            ApplyLosingControlEffect(beaters2);
            ApplyLosingControlEffect(chasers2);
            ApplyLosingControlEffect(seekers2);

            yield return null;
        }
        StopLosingControlEffect(keepers2);
        StopLosingControlEffect(beaters2);
        StopLosingControlEffect(chasers2);
        StopLosingControlEffect(seekers2);
    }

    private void ApplyLosingControlEffect(List<PlayerLogicManager> players) //IEnumerator is the return value of a coroutine function. //A coroutine is a function that can be paused and resumed at any point during its execution.
    {
        foreach(PlayerLogicManager player in players)
        {
            player.GetAnimator().SetBool("Idle", false);
            player.GetAnimator().SetBool("Stupefy", true);
        }
    }

    private void StopLosingControlEffect(List<PlayerLogicManager> players) //IEnumerator is the return value of a coroutine function. //A coroutine is a function that can be paused and resumed at any point during its execution.
    {
        foreach (PlayerLogicManager player in players)
        {
            player.GetAnimator().SetBool("Idle", true);
            player.GetAnimator().SetBool("Stupefy", false);
        }
    }
}
