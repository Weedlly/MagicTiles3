using SuperMaxim.Messaging;
using UnityEngine;

namespace GamePlay.ScoreSystem
{
    public struct OnChangeScoreBarPosPayload
    {
        public Vector2 NewPos;
    }

    public class ScoreBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        private void Start()
        {
            Messenger.Default.Subscribe<OnChangeScoreBarPosPayload>(OnChangeBarYPosition);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<OnChangeScoreBarPosPayload>(OnChangeBarYPosition);
        }
        private void OnChangeBarYPosition(OnChangeScoreBarPosPayload payload)
        {
            _rectTransform.position = new Vector2(_rectTransform.position.x, payload.NewPos.y);
        }
    }
}
