using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    private enum State
    {
        WaitingToStart, CountdownToStart, GamePlaying, GameOver
    }

    private State state;
    private float waitingToStartTimer = 1;
    private float countdownToStartTimer = 3;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 20;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;
        }

        //Debug.Log("GAME MANAGER state=" + state);
    }

    public bool IsGamePlaying => state == State.GamePlaying;

    public bool IsCountdownToStartActive => state == State.CountdownToStart;

    public float CountdownToStartTimer => countdownToStartTimer;

    public bool IsGameOver => state == State.GameOver;

    public float GamePlayingTimerNormalized => gamePlayingTimer / gamePlayingTimerMax;

}
