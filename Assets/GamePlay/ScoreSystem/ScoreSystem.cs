using SuperMaxim.Messaging;
using System;
using UnityEngine;

namespace GamePlay.ScoreSystem
{
    public struct OnTapTilePayload
    {
        public GameObject Tile;
        public int TileLength;
    }

    public enum EScoreType
    {
        None = 0,
        Cool = 1,
        Great = 2,
        Perfect = 3,
    }

    [Serializable]
    public class CalculateScoreSystem
    {
        [SerializeField] private float _overlapPercentsForGreatScore = 10f;
        [SerializeField] private float _longTileScoreFactor = 1f;
        public EScoreType GetScoreType(RectTransform rectTransformTile, RectTransform rectTransformScoreLine)
        {
            Rect rect1 = GetWorldRect(rectTransformTile);
            Rect rect2 = GetWorldRect(rectTransformScoreLine);

            if (!IsImagesOverlapping(rect1, rect2))
                return EScoreType.Cool;
            float overlapPercents = GetOverlapPercents(rect1, rect2);
            return IsGreatScore(overlapPercents) ? EScoreType.Great : EScoreType.Perfect;
        }
        public int GetScoreByTileLength(int tileLength)
        {
            return (int)(tileLength * _longTileScoreFactor);
        }
        private bool IsImagesOverlapping(Rect rect1, Rect rect2)
        {
            return rect1.Overlaps(rect2);
        }
        private float GetOverlapPercents(Rect rect1, Rect rect2)
        {
            float middleYRect2 = rect2.height / 2 + rect2.y - rect1.y;
            if (middleYRect2 == 0f)
                return 0f;
            return middleYRect2 / rect1.height * 100f;
        }
        private bool IsGreatScore(float overlapPercents)
        {
            return overlapPercents < _overlapPercentsForGreatScore || 100f - overlapPercents < _overlapPercentsForGreatScore;
        }
        private Rect GetWorldRect(RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            Vector3 bottomLeft = corners[0];
            Vector3 topRight = corners[2];

            return new Rect(bottomLeft, topRight - bottomLeft);
        }
    }

    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private RectTransform _objScoreLineRt;
        [SerializeField] private CalculateScoreSystem _calculateScoreSystem;
        [SerializeField] private ScoreDataConfig _scoreDataConfig;

        private int _curScore;
        private void Start()
        {
            _curScore = 0;
            Messenger.Default.Subscribe<OnTapTilePayload>(CalScore);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<OnTapTilePayload>(CalScore);
        }
        private void CalScore(OnTapTilePayload onTapTilePayload)
        {
            RectTransform rectTransformTile = onTapTilePayload.Tile.GetComponent<RectTransform>();
            EScoreType eScoreType = _calculateScoreSystem.GetScoreType(rectTransformTile, _objScoreLineRt);

            _curScore += _scoreDataConfig.GeConfigByKey(eScoreType).ScoreVal + _calculateScoreSystem.GetScoreByTileLength(onTapTilePayload.TileLength);

            if (eScoreType == EScoreType.Cool)
            {
                NotifyUpdateScoreBarPos(rectTransformTile.position);
            }
            NotifyUpdateScoreView(eScoreType, _curScore);
        }
        private void NotifyUpdateScoreView(EScoreType eScoreType, int curScore)
        {
            Messenger.Default.Publish(new OnChangeScorePayload
            {
                EScoreType = eScoreType,
                CurScore = curScore,
            });
        }
        private void NotifyUpdateScoreBarPos(Vector2 pos)
        {
            Messenger.Default.Publish(new OnChangeScoreBarPosPayload
            {
                NewPos = pos,
            });
        }
    }
}
