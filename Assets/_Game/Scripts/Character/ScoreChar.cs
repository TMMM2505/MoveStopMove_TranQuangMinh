using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreChar : GameUnit
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private void Start()
    {
        scoreText.text = "1";
    }
    private void Update()
    {
        transform.forward = Vector3.forward;
    }
    public void IncreaseScore(float score)
    {
        scoreText.text = score.ToString();
    }
}
