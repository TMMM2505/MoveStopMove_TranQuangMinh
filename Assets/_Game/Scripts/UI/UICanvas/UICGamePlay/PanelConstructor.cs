using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PanelConstructor : UICanvas
{
    [SerializeField] private Transform[] Path;

    public Action OnClick;
    private void Start()
    {
        Vector3[] path = new Vector3[Path.Length];
        for (int i = 0; i < Path.Length; i++)
        {
            path[i] = Path[i].position;
        }

        transform.DOPath(path, 4f, PathType.CatmullRom)
                 .SetOptions(true)      
                 .SetEase(Ease.Linear)  
                 .SetLoops(-1);
    }

}
