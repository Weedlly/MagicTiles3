using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace GamePlay.ScoreSystem
{
    public class ScoreEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _congratulationEffect;
        [SerializeField] private TextMeshProUGUI _textScore;
        [SerializeField] private CanvasGroup _canvasGroupText;
        [SerializeField] private ScoreDataConfig _scoreDataConfig;
        
        [Header("Config")]
        [SerializeField] private float _startScale = 1.5f;
        [SerializeField] private float _perStepDuration = 0.25f;
        [SerializeField] private float _nextStartScale = 1.2f;
        [SerializeField] private float _alphaStart = 0.7f;
        
        public async void PlayEffect(EScoreType eScoreType)
        {
            // start
            _textScore.gameObject.SetActive(true);
            _textScore.text = eScoreType.ToString();
            _textScore.transform.localScale = Vector3.one * _startScale;
            _canvasGroupText.alpha = _alphaStart;
            _congratulationEffect.Stop();
            
            // nextStart
            Tween tweenNextStartScale = _textScore.transform.DOScale(Vector3.one * _nextStartScale, _perStepDuration);
            Tween tweenFadeToOne = _canvasGroupText.DOFade(1, _perStepDuration).OnStart(() =>
            {
                _congratulationEffect.gameObject.SetActive(true);
                _congratulationEffect.Play();
            });
            
            await UniTask.Delay(TimeSpan.FromSeconds(_perStepDuration));
            Tween tweenScaleToOne = _textScore.transform.DOScale(Vector3.one, _perStepDuration);
            await UniTask.Delay(TimeSpan.FromSeconds(_perStepDuration));
            Tween tweenFadeToZero = _canvasGroupText.DOFade(0, _perStepDuration).OnComplete(() =>
            {
                _congratulationEffect.Stop();
                _congratulationEffect.gameObject.SetActive(false);
            });
        }
    }
}
