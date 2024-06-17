using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    public void OnStart(Enemy enemy);
    public void OnExecute(Enemy enemy);
    public void OnExit(Enemy enemy);
}
