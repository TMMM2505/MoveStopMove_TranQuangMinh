using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class IconOnOff : MonoBehaviour
{
    public void OnClick(Vector3 pos)
    {
        transform.DOMove(pos, 1f, true).SetEase(Ease.Linear);
    }
}
