using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private Color defaultColor;
    private float alpha;
    private MeshRenderer render;
    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        defaultColor = render.material.color;
    }

    public void BeTransparent()
    {
        if(gameObject != null)
        {
            alpha = 0.5f;
            render.material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alpha);
        }
    }
    public void ResetAlpha()
    {
        if (gameObject != null)
        {
            alpha = 1f;
            render.material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alpha);
        }
    }
}
