using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTag : Button
{
    public Image icon;

    public void ChangeColorWhenClicked()
    {
        icon.GetComponent<Image>().color = Color.white;
        gameObject.GetComponent<Image>().color = Color.gray;
    }
    public void ChangeDefaultColor()
    {
        icon.GetComponent<Image>().color = Color.gray;
        gameObject.GetComponent<Image>().color = Color.black;
    }
}
