using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UICGamePlay : UICanvas
{
    public TextMeshProUGUI EnemyRemain;
    public Joystick js;
    public Button BtnSetting;
    public PanelConstructor Constructor;
    public Button BtnConstructor;
    public TimerCountDown timer;

    [SerializeField] private GameObject Joystick;

    private bool checkStart = true;

    private void Awake()
    {
        BtnSetting.onClick.AddListener(OnClickSetting);
        Constructor.OnClick = SetConstructor;
        BtnConstructor.onClick.AddListener(OnClickConstructor);
    }
    private void OnClickConstructor()
    {
        timer.OnStart();
        Constructor.OnClick?.Invoke();
    }

    private void Update()
    {
        EnemyRemain.text = LevelManager.Ins.GetEnemyRemain().ToString();
    }
    public void ResetTimer()
    {
        timer.OnStart();
    }
    private void SetConstructor()
    {
        Constructor.gameObject.SetActive(false);
        ActiveJs();

        LevelManager.Ins.ActiveEnemy();
        LevelManager.Ins.GetPlayer().SetCanMove(true);
    }
    private void OnClickSetting()
    {
        Close(0);
        Time.timeScale = 0f;
        GameManager.Ins.ChangeState(GameState.Setting);
    }

    public void ActiveJs()
    {
        Joystick.gameObject.SetActive(true);
    }
    public void DeactiveJs()
    {
        Joystick.gameObject.SetActive(false);
    }
    public void OnStart()
    {
        DeactiveJs();
        Constructor.gameObject.SetActive(true);
        timer.DisplayTime(180f);
    }
}
