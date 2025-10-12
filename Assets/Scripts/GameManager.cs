using Unity.VisualScripting;
using UnityEngine;

public class GameManager: MonoBehaviour, MinigameSubscriber
{
    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    public float bpm = 150f;

    private float instantiationInterval;

    private float currScore = 0f;
    public float neededScore = 10f;

    public void OnMinigameStart()
    {
        
    }

    //Time is up, calculate score, return the result, and end the Minigame
    public void OnTimerEnd()
    {
        // Timer has expired
        if (currScore >= neededScore)
        {
            MinigameManager.SetStateToSuccess();
        }
        else
        {
            MinigameManager.SetStateToFailure();
        }
        MinigameManager.EndGame();
    }

    //I will be instantiating the arrows at y = 3
    void Start(){
        MinigameManager.Subscribe(this);
        instantiationInterval = 1 / (bpm / 60f); //Calculate the how many seconds between creating each arrow
        print(transform.GetChild(0));
    }
}
