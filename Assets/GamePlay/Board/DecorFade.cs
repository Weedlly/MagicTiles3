using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Board
{
    public class DecorFade : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _oneTimeFadeDuration;
        [SerializeField] private AnimationCurve _animationFade;
        private void Start()
        {
            _image.DOFade(1, _oneTimeFadeDuration).SetEase(_animationFade).SetLoops(-1);
        }
    }
}
