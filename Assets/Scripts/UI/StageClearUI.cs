using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClearUI : BaseUI
{
    public Button NextStageButton;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        if (NextStageButton != null)
        {
            NextStageButton.onClick.RemoveAllListeners();
            NextStageButton.onClick.AddListener(OnNextStageButton);
        }
    }


    public void OnNextStageButton()
    {
        GameManager.Instance.Stage += 1;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    protected override UIState GetUIState()
    {
        return UIState.StageClear;
    }
}