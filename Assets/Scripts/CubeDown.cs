using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.UI;

//IMPORTANT : activate the voice commands using VR pushing button, it is explained in https://www.youtube.com/watch?v=SJ96P-ZhBoc&t=213s.

///////////////////////////////////////////////////////////
//using UnityEngine.InputSystem; //to recognize Keyboad
//using Meta.WitAi;
//using Meta.WitAi.Json;
//using Oculus.Voice;
///////////////////////////////////////////////////////////




public class CubeDown : MonoBehaviour
{
    public Text displayText; // Reference to the UI Text component

    /////////////////////////////////////////////////////////////////////////////////////////
    //private bool appVoiceActive = false; //for the Keyboard activation try
    //public Wit wit; //not usefull yet but will be when activating using VR key pushing
    /////////////////////////////////////////////////////////////////////////////////////////

    [Header("Cubes")]
    [SerializeField]
    public List<GameObject> cubes;//needs to be under serializefield without any spaces to recognize the list of gameobjects that is linked to the script under cubehandler.

    public void SetDirection(string[] directionString) // type is string[] because the string is the phrase said in the voice command which we will recognise each word
    {
        float distance = 1.5f;  // distance we want the cube to move
        float duration = 0.5f;  //how long the cube should take to travel this distance

        switch (directionString[0]) //first word in the phrase recognized in the voice command. (here we only have one word)
        {
            case "up":
                StartCoroutine(MoveCube(Vector3.up * distance, duration)); //Vector3.up is (0,1,0)
                break;
            case "down":
                StartCoroutine(MoveCube(Vector3.down * distance, duration));//Vector3.down is (0,-1,0) //the coroutine function here is MoveCube.
                //In Unity, a coroutine is a function that can run alongside other functions in your game. When you call a coroutine function, it will start running and will continue running until it reaches a yield statement. At that point, the coroutine function will pause and allow other code to run, and then it will resume from where it left off on the next frame.
                break;
            case "fast":
                StartCoroutine(SpeedBoost(duration * 2));
                break;
        }
    }

    private IEnumerator SpeedBoost(float duration)
    {
        Debug.Log("In SpeedBoost!"); // TEMPORARY
        float elapsedTime = 0.0f;
        float boost = 1.5f;

        //cubes[0].transform.position = Vector3.up * 2f;// TEST

        //displayText.text = "Hello, world!"; //FOR COOL DOWN!!!! Sets the text value of the UI Text component 

        cubes[0].GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed *= boost;
        while (elapsedTime < duration) //move for "duration" seconds
        {
            elapsedTime += Time.deltaTime; //Time.deltaTime is the time in seconds it took for the last frame to be rendered. it creates a smooth animationand the speed is independent from frame rate so it will be identical on all machines.
        }

        cubes[0].GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed = cubes[0].GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed / boost;

        yield return null;
    }

    private IEnumerator MoveCube(Vector3 direction, float duration) //IEnumerator is the return value of a coroutine function. //A coroutine is a function that can be paused and resumed at any point during its execution.
    {
        float elapsedTime = 0.0f;

        Vector3 startingPos = cubes[1].transform.position; //vector (x,y,z) //done on cubes[1] but could be done on any gameobject of the cubes list using a voice command that chooses the gameobject on which to do the command.
        Vector3 targetPos = startingPos + direction; //vector (x,y,z) + (0,+/-distance,0) so it adds the distance to the y of the vector according to up/down

        while (elapsedTime < duration) //move for "duration" seconds
        {
            float t = elapsedTime / duration; //how much of the time has passed out of duration (elapsedTime = 1 , duration = 2, t = 1/2 => half the time passed)
            cubes[1].transform.position = Vector3.Lerp(startingPos, targetPos, t); //Vector3.Lerp returns a 3D vector that corresponds to the interpolated position between start and end at time t.
            elapsedTime += Time.deltaTime; //Time.deltaTime is the time in seconds it took for the last frame to be rendered. it creates a smooth animationand the speed is independent from frame rate so it will be identical on all machines.
            yield return null; //At that point, the coroutine function will pause and allow other code to run, and then it will resume from where it left off on the next frame.
        }
    }





    ////Tryout to activate using keyboard

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //private void Awake()
    //{
    //    appVoiceExperience.events.OnRequestCreated.AddListener((request) =>
    //    {
    //        appVoiceActive = true;
    //        Logger.Instance.LogInfor("OnRequestCompleted Active");
    //    });

    //    appVoiceExperience.events.OnRequestCompleted.AddListener(() =>
    //    {
    //        appVoiceActive = false;
    //        Logger.Instance.LogInfor("OnRequestCompleted Active");
    //    });

    //}

    //private void Update()
    //{
    //    if(Keyboard.current.spaceKey.wasPressedThisFrame && appVoiceActive)
    //    {
    //        //activate voice experience
    //        appVoiceExperience.Activate();
    //    }

    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    ////Old SetDirection

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //public void SetDirection(string[] directionString)
    //{
    //    int i = 0;
    //    // get the current position of the cube
    //    Vector3 currentPosition = cubes[0].transform.position;
    //    while (i < 20)
    //    { 
    //        switch (directionString[0])
    //        {
    //            case "up":
    //                //currentPosition.y += speed * Time.deltaTime;
    //                cubes[0].transform.Translate(Vector3.up * speed * Time.deltaTime);
    //                break;
    //            case "down":
    //                cubes[0].transform.Translate(Vector3.down * speed * Time.deltaTime);
    //                break; 
    //        }
    //        i++;
    //    }
    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
