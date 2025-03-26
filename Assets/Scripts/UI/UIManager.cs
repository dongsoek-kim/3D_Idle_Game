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
    StageClear
}

public class UIManager : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] public InGameUI inGameUI;
    [SerializeField] public GameEndUI gameEndUI;
    [SerializeField] public StageClearUI stageClearUI;


    private UIState currentState;


    protected void Awake()
    {
        inGameUI = GetComponentInChildren<InGameUI>(true);
        inGameUI.Init(this);
        gameEndUI = GetComponentInChildren<GameEndUI>(true);
        gameEndUI.Init(this);
        stageClearUI = GetComponentInChildren<StageClearUI>(true);
        stageClearUI.Init(this);
        ChangeState(UIState.InGame);
    }
    public void Init()
    {
        inGameUI = GetComponentInChildren<InGameUI>(true);
        inGameUI.Init(this);
        gameEndUI = GetComponentInChildren<GameEndUI>(true);
        gameEndUI.Init(this);
        stageClearUI = GetComponentInChildren<StageClearUI>(true);
        stageClearUI.Init(this);
        ChangeState(UIState.InGame);
    }
    public void ChangeState(UIState state)
    {
        currentState = state;
        inGameUI.SetActive(currentState);
        gameEndUI.SetActive(currentState);
        stageClearUI.SetActive(currentState);
    }
}