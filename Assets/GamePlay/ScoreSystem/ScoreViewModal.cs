using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.ScoreSystem
{
    public struct OnChangeScorePayload
    {
        public EScoreType EScoreType;
    }
    public class ScoreViewModal : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private ScoreDataConfig _scoreDataConfig;
        private int _curScore;
        private void Start()
        {
            _curScore = 0;
            Messenger.Default.Subscribe<OnChangeScorePayload>(OnChangeScore);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<OnChangeScorePayload>(OnChangeScore);
        }
        private void OnChangeScore(OnChangeScorePayload payload)
        {
            _curScore += _scoreDataConfig.GeConfigByKey(payload.EScoreType).ScoreVal;
            UpdateView();
        }
        private void UpdateView()
        {
            _scoreView.SetUp(_curScore);
        }
    }
}
