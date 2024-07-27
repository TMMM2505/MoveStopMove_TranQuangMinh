using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProcessBar : MonoBehaviour
{
    public TextMeshProUGUI txtExp;
    public Image background;
    //[SerializeField] private Image Icon1;
    //[SerializeField] private Image IconTick1;
    //[SerializeField] private Image IconLock1;

    private float curScore, maxScore;
    private Tweener myTween;
    public void OnInit(float maxScore, float curScore)
    {
        this.curScore = curScore;
        this.maxScore = maxScore;
        txtExp.text = string.Format("{0:000}/{1:000}", curScore, maxScore);
        myTween = transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f)
                 .SetLoops(-1, LoopType.Yoyo);
    }
    public void SetBarEndGame()
    {
        if(UserData.Ins.lastMapIndex < LevelManager.Ins.mapData.mapDataConfig.Count - 1)
        {
            curScore = LevelManager.Ins.GetPlayerScore() + UserData.Ins.GetCurrentExp();
            maxScore = LevelManager.Ins.GetMaxScore();
            SetColorBG();
            txtExp.text = string.Format("{0:000}/{1:000}", curScore, maxScore);
            UserData.Ins.SetCurrentExp(curScore);
        }
        else
        {
            background.color = Color.green;
            txtExp.text = ">>>>>> Finish!!! <<<<<<";
        }
    }
    public void SetBarChoseMap()
    {
        if (UserData.Ins.lastMapIndex < LevelManager.Ins.mapData.mapDataConfig.Count - 1)
        {
            txtExp.text = string.Format("{0:000}/{1:000}", UserData.Ins.GetCurrentExp(), LevelManager.Ins.GetMaxScore());
            SetColorBG();
        }
        else
        {
            background.color = Color.green;
            txtExp.text = "Finish";
        }
    }
    public void SetColorBG()
    {
        if (UserData.Ins.GetCurrentExp() >= LevelManager.Ins.GetMaxScore())
        {
            background.color = Color.green;
            txtExp.color = Color.black;
            myTween.Play();
        }
        else
        {
            background.color = Color.black;
            txtExp.color = Color.white;
            myTween.Pause();
        }
    }

    //public void UnlockIcon1()
    //{
    //    Icon1.color = Color.green;
    //    IconTick1.gameObject.SetActive(true);
    //    IconLock1.gameObject.SetActive(false);
    //}
    //public void LockIcon1()
    //{
    //    Icon1.color = Color.black;
    //    IconTick1.gameObject.SetActive(false);
    //    IconLock1.gameObject.SetActive(true);
    //}

}
