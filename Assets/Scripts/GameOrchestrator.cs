using System;
using System.Collections;
using System.Collections.Generic;
using Thanasis;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameOrchestrator : MonoBehaviour
{
    public float initialDelay = 2f;
    public Player firstPlayer = Player.Ground;
    public Player secondPlayer = Player.Earth;
    public GameObject groundPlayer;
    public float groundSeconds = 10f;
    public Transform groundLastPointFollower;
    public GameObject earthPlayer;
    public float earthSeconds = 30f;
    public Transform earthLastPointFollower;

    public CameraFollow cameraFollow;
    public Timer timer;

    public InternalPlayerState _playerState = InternalPlayerState.NoPlayer;

    public enum InternalPlayerState
    {
        NoPlayer,
        FirstIsPlaying,
        SecondIsPlaying,
        GameEnd
    }

    public enum Player
    {
        None,
        Ground,
        Earth
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        timer.onTimeEnd += OnTimeEnd;

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(initialDelay);
        if (firstPlayer != Player.None)
        {
            SetPlayerState(firstPlayer, true);
            _playerState = InternalPlayerState.FirstIsPlaying;
        }
    }

    private void OnTimeEnd()
    {
        if (_playerState == InternalPlayerState.FirstIsPlaying)
        {
            SetPlayerState(firstPlayer, false);
            SetPlayerState(secondPlayer, true);
            _playerState = InternalPlayerState.SecondIsPlaying;
        }
        else if (_playerState == InternalPlayerState.SecondIsPlaying)
        {
            _playerState = InternalPlayerState.GameEnd;
            Debug.Log("Game Over");
        }
    }

    private void SetPlayerState(Player player, bool active)
    {
        if (player == Player.Ground)
        {
            if (!active)
            {
                groundPlayer.GetComponentInChildren<DrawAutomatically>().enabled = false;
            }
        }
        else if (player == Player.Earth)
        {
            if (!active)
            {
                earthPlayer.GetComponentInChildren<DrawAutomatically>().enabled = false;
            }
        }

        if (active)
        {
            cameraFollow.targetObject = (player == Player.Ground ? groundLastPointFollower : earthLastPointFollower);
            cameraFollow.isChasing = true;
            cameraFollow.OnReadyToFollow += () =>
            {
                cameraFollow.isChasing = false;
                if (player == Player.Ground)
                    groundPlayer.SetActive(true);
                else if (player == Player.Earth)
                    earthPlayer.SetActive(true);
                timer.ResetTimer(player == Player.Ground ? groundSeconds : earthSeconds);
            };
            if (player == Player.Earth)
            {
                // timer.GetComponentInChildren<TextMeshProUGUI>(true).SetText($"{earthSeconds:00.0}");
            }

            cameraFollow.StartFollow();
        }
    }
}