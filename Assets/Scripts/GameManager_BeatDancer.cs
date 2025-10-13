using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameManager_BeatDancer: MonoBehaviour, MinigameSubscriber
{
    //Variables for the objects being instantiated
    public GameObject[] Arrows;  //Order is down, left, up, right
    private Vector2[] socketPositions = new Vector2[4]; 

    //Music Variables
    public float bpm = 150f;
    public float quarterNotesPerMeasure = 4f;
    private float instantiationInterval; //This doubles as the lifespan of an arrow

    //Scoring Variables
    private float currScore = 0f;
    public float neededScore = 15f;

    private AudioSource soundFile;
    //Game Management Variables
    private System.Random rand = new System.Random();

    public void OnMinigameStart()
    {
        soundFile.Play();
        transform.AddComponent<AudioSource>();
        StartCoroutine(GenerateArrows());
    }

    //Time is up, calculate score, return the result, and end the Minigame
    public void OnTimerEnd()
    {
        // Timer has expired
        if (currScore <= neededScore) {
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
        instantiationInterval = quarterNotesPerMeasure / 2 / (bpm / 60f); //Calculate the how many seconds between creating each arrow
        Debug.Log(instantiationInterval);
        socketPositions[0] = transform.GetChild(0).position;
        socketPositions[1] = transform.GetChild(1).position;
        socketPositions[2] = transform.GetChild(2).position;
        socketPositions[3] = transform.GetChild(3).position;
        soundFile = GetComponent<AudioSource>();
    }

    void OnMove(InputValue val)
    {
        if (!MinigameManager.IsReady()) // IMPORTANT: Don't allow any input while the countdown is still occuring
            return;

        if ((Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) && transform.GetChild(4).GetComponent<ArrowMover_BeatDancer>().getSocketNum() == 0 ||
            (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) && transform.GetChild(4).GetComponent<ArrowMover_BeatDancer>().getSocketNum() == 1 ||
            (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) && transform.GetChild(4).GetComponent<ArrowMover_BeatDancer>().getSocketNum() == 2 ||
            (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) && transform.GetChild(4).GetComponent<ArrowMover_BeatDancer>().getSocketNum() == 3)
        {
            currScore += (transform.GetChild(4).transform.position[1] + 3.3f);
            Destroy(transform.GetChild(4).gameObject);
        }
        
    }

    IEnumerator GenerateArrows()
    {
        int idx = rand.Next(0, Arrows.Length);
        GameObject newArrow = Instantiate(Arrows[idx]);
        newArrow.AddComponent<ArrowMover_BeatDancer>();
        newArrow.GetComponent<ArrowMover_BeatDancer>().setSocketNumber(idx);
        newArrow.transform.parent = transform;  //Parent the instantiated arrow to the game manager
        newArrow.transform.position = new Vector2(-4.5f + 3f * idx, 3f);
        yield return new WaitForSeconds(instantiationInterval); // Wait for a little
        StartCoroutine(GenerateArrows());
    }


    public float getInstantiationInterval()
    {
        return instantiationInterval;
    }

    public void incrementScore(float score)
    {
        currScore += score;
    }

}
