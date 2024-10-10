using System;
using TMPro;
using UnityEngine;

namespace GamePlay.ScoreSystem
{
    [Serializable]
    public class ScoreView
    {
        [SerializeField] private TextMeshProUGUI _txtScoreVal;
        [SerializeField] private ScoreEffect _scoreEffect;
        public void SetUp(int score, EScoreType eScoreType)
        {
            _txtScoreVal.text = score.ToString();
            _scoreEffect.PlayEffect(eScoreType);
        }
    }
}
