using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.ScoreSystem
{
    public struct OnChangeScorePayload
    {
        public EScoreType EScoreType;
        public int CurScore;
    }
    public class ScoreViewModal : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        private int _curScore;
        private EScoreType _eScoreType;
        private void Start()
        {
            Messenger.Default.Subscribe<OnChangeScorePayload>(OnChangeScore);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<OnChangeScorePayload>(OnChangeScore);
        }
        private void OnChangeScore(OnChangeScorePayload payload)
        {
            _curScore = payload.CurScore;
            _eScoreType = payload.EScoreType;
            UpdateView();
        }
        private void UpdateView()
        {
            _scoreView.SetUp(_curScore,_eScoreType);
        }
    }
}
