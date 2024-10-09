using System;
using TMPro;
using UnityEngine;

namespace GamePlay.ScoreSystem
{
    [Serializable]
    public class ScoreView
    {
        [SerializeField] private TextMeshProUGUI _txtScore;

        public void SetUp(int score)
        {
            _txtScore.text = score.ToString();
        }
    }
}
