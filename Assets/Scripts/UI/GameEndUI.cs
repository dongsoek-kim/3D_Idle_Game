using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameEndUI : BaseUI
{

    public Button restartButton;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartButton);
        }
    }
    protected override UIState GetUIState()
    {
        return UIState.GameEnd;
    }
    public void OnRestartButton()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
