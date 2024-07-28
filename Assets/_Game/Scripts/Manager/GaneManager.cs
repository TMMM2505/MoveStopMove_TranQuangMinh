using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public enum GameState { MainMenu, Gameplay, Victory, EndGame, ShopWeapon, ShopSkin, Setting, ChoseMap}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] CameraFollower camera;
    [SerializeField] private UICIndicator uicIndicator;
    public ItemDataConfig ItemDataConfig;
    private UICEndGame uicEndGame;
    private UICGamePlay uicGamePlay;
    private UICChoseMap uICChoseMap;
    private UICMainMenu uicMainMenu;
    private UICSetting uicSetting;
    public GameState CurrentGameState { get; private set; }
    private bool Replay = false;
    private bool IsPlaying = false;

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }
    public bool CheckReplay() => Replay;
    public bool CheckIsPlaying() => IsPlaying;
    public void ChangeState(GameState newState)
    {
        CurrentGameState = newState;
        uicIndicator.gameObject.SetActive(false);

        switch (newState)
        {
            case GameState.MainMenu:
                {
                    MainMenu();
                    break;
                }
            case GameState.Gameplay:
                {
                    GamePlay();
                    break;
                }
            case GameState.EndGame:
                {
                    EndGame();
                    Replay = true;
                    break;
                }
            case GameState.ShopWeapon:
                {
                    ShopWeapon();
                    break;
                }
            case GameState.ShopSkin:
                {
                    ShopSkin();
                    break;
                }
            case GameState.Setting:
                {
                    Setting();
                    break;
                }
            case GameState.ChoseMap:
                {
                    ChoseMap();
                    break;
                }
        }
    }

    private void ChoseMap()
    {
        uICChoseMap = UIManager.Ins.OpenUI<UICChoseMap>();
        uICChoseMap.SetProcessBar();
        uICChoseMap.SetUpPerOpening();
    }

    private void Setting()
    {
        uicSetting = UIManager.Ins.OpenUI<UICSetting>();
        uicSetting.SetUpFunction();
    }
    private void ShopSkin()
    {
        UIManager.Ins.OpenUI<UICShopSkin>();
        LevelManager.Ins.DeactiveEnemy();
        LevelManager.Ins.GetPlayer().ChangeAnim("IsDance");
        camera.OnShopSkin();
    }

    private void ShopWeapon()
    {
        UIManager.Ins.OpenUI<UICShopWeapon>();
        LevelManager.Ins.DeactiveEnemy();
    }

    public void MainMenu()
    {
        camera.OnMainMenu();
        Time.timeScale = 1f;
        LevelManager.Ins.DeactiveEnemy();
        LevelManager.Ins.ActivePlayer();
        LevelManager.Ins.GetPlayer().SetEquippedSkin();

        uicMainMenu = UIManager.Ins.OpenUI<UICMainMenu>();
        uicMainMenu.SetUpFunction();
    }
    public void GamePlay()
    {
        uicGamePlay = UIManager.Ins.OpenUI<UICGamePlay>();
        uicGamePlay.OnStart();

        IsPlaying = true;
        
        camera.OnGamePlay();
        
        uicIndicator.gameObject.SetActive(true);
        
        LevelManager.Ins.GetPlayer().SetCanMove(false);
        LevelManager.Ins.DeactiveEnemy();

        if (Replay)
        {
            uicGamePlay.ResetTimer();
        }
        Replay = true;
    }
    public void EndGame()
    {
        UIManager.Ins.CloseUI<UICGamePlay>();
        uicEndGame = UIManager.Ins.OpenUI<UICEndGame>();
        uicEndGame.SetProcessBar();
    }
}
