using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        startButton.onClick.AddListener(OnClickeStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickeStartButton()
    {
        GameManager.instance.StartGame();
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }
    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

}
