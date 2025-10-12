using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameManager: MonoBehaviour, MinigameSubscriber
{
    //Variables for the objects being instantiated
    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    //Music Variables
    public float bpm = 150f;
    public float quarterNotesPerMeasure = 4f;
    private float instantiationInterval; //This doubles as the lifespan of an arrow

    //Scoring Variables
    private float currScore = 0f;
    public float neededScore = 10f;

    //Game Management Variables
    private Vector2 leftSocketPosition;
    private Vector2 rightSocketPosition;
    private Vector2 upSocketPosition;
    private Vector2 downSocketPosition;

    private float time = 0f;
    public void OnMinigameStart()
    {
        GenerateArrows();
    }

    //Time is up, calculate score, return the result, and end the Minigame
    public void OnTimerEnd()
    {
        // Timer has expired
        if (currScore >= neededScore) {
            MinigameManager.SetStateToSuccess();
        }
        else {
            MinigameManager.SetStateToFailure();
        }
        MinigameManager.EndGame();
    }

    //I will be instantiating the arrows at y = 3
    void Start()
    {
        MinigameManager.Subscribe(this);
        instantiationInterval = quarterNotesPerMeasure/2/(bpm / 60f); //Calculate the how many seconds between creating each arrow
        downSocketPosition = transform.GetChild(0).position;
        leftSocketPosition = transform.GetChild(1).position;
        upSocketPosition = transform.GetChild(2).position;
        rightSocketPosition = transform.GetChild(3).position;
    }

    void OnMove(InputValue val)
    {
        if (!MinigameManager.IsReady()) // IMPORTANT: Don't allow any input while the countdown is still occuring
            return;

        if (Keyboard.current.aKey.isPressed)
        {
            print("A key pressed");
        }
        else if (Keyboard.current.wKey.isPressed)
        {
            print("W key pressed");
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            print("S key pressed");
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            print("D key pressed");
        }
    }

    IEnumerator GenerateArrows()
    {
        yield return new WaitForSeconds(instantiationInterval); // Wait for 3 seconds
        //This is where I have to create an arrow at the proper position
        GenerateArrows();
    }

    public float getInstantiationInterval()
    {
        return instantiationInterval;
    }
}
