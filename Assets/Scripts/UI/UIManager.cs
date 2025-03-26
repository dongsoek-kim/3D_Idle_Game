using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum UIState
{
    InGame,
    GameEnd,
    Pause
}

public class UIManager : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] public InGameUI inGameUI;
    [SerializeField] public GameEndUI gameEndUI;
    [SerializeField] public PauseUI pauseUI;


    private UIState currentState;


    protected void Awake()
    {
        inGameUI = GetComponentInChildren<InGameUI>(true);
        inGameUI.Init(this);
        gameEndUI = GetComponentInChildren<GameEndUI>(true);
        gameEndUI.Init(this);
        pauseUI = GetComponentInChildren<PauseUI>(true);
        pauseUI.Init(this);
        ChangeState(UIState.InGame);
    }
    public void Init()
    {
        inGameUI = GetComponentInChildren<InGameUI>(true);
        inGameUI.Init(this);
        gameEndUI = GetComponentInChildren<GameEndUI>(true);
        gameEndUI.Init(this);
        pauseUI = GetComponentInChildren<PauseUI>(true);
        pauseUI.Init(this);
        ChangeState(UIState.InGame);
    }
    public void ChangeState(UIState state)
    {
        currentState = state;
        inGameUI.SetActive(currentState);
        gameEndUI.SetActive(currentState);
        pauseUI.SetActive(currentState);
    }
}