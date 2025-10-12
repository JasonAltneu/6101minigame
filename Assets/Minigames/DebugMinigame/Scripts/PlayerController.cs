using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    This is an example script part of the debug minigame

    The purpose of it is to show you how to properly deal with input
    and use the provided MinigameManager.cs class
*/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))] // This component must be attached to the GameObject for input to register
public class PlayerController : MonoBehaviour, MinigameSubscriber
{
    private Rigidbody2D rb;

    void Start()
    {
        // Subscribes this class to the minigame manager. This gives access to the
        // 'OnMinigameStart()' and 'OnTimerEnd()' functions. Otherwise, they won't be called.
        MinigameManager.Subscribe(this);
        rb = GetComponent<Rigidbody2D>();
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
        } else if (Keyboard.current.sKey.isPressed)
        {
            print("S key pressed");
        } else if (Keyboard.current.dKey.isPressed)
        {
            print("D key pressed");
        }
    }

    public void OnMinigameStart()
    {
        Debug.Log("Minigame started!");
        // There isn't anything interesting that needs to happen in here for this example
    }

    public void OnTimerEnd()
    {
        // Timer has expired
        MinigameManager.SetStateToFailure();
        MinigameManager.EndGame();
    }
}
