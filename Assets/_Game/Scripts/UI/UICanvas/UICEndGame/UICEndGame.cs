using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICEndGame : UICanvas
{
    [SerializeField] private ProcessBar processBar;
    public TextMeshProUGUI txtCoin;
    public GameObject PanelTxtInfoEnemy;
    public GameObject PanelVictory;
    public Button BtnReplay;
    private void Start()
    {
        BtnReplay.onClick.AddListener(BtnReplayOnClick);
        if(LevelManager.Ins.Victory())
        {
            PanelTxtInfoEnemy.SetActive(false);
            PanelVictory.SetActive(true);
        }
        else
        {
            PanelTxtInfoEnemy.SetActive(true);
            PanelVictory.SetActive(false);
        }
        ShowGainedCoin();
        processBar.OnInit(LevelManager.Ins.GetMaxScore(), UserData.Ins.GetCurrentExp());
        SetProcessBar();
    }
    public void SetProcessBar()
    {
        processBar.SetBarEndGame();
    }
    public void ShowGainedCoin()
    {
        txtCoin.text = LevelManager.Ins.GetPlayerScore().ToString();
    }
    private void BtnReplayOnClick()
    {
        LevelManager.Ins.Replay();
        LevelManager.Ins.OnInit();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        Close(0);
    }
}
