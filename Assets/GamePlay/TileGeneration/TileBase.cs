using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay.TileGeneration
{
    public abstract class TileBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] protected CanvasGroup _canvasGroupTile;

        protected Transform _genTilePos;
        protected Transform _outOfBoardCheckPoint;
        private Action<GameObject> _onReturnPool;
        public virtual void SetUp(Transform genTilePos, Transform outOfBoardCheckPoint, Action<GameObject> onReturnPool)
        {
            _genTilePos = genTilePos;
            _outOfBoardCheckPoint = outOfBoardCheckPoint;
            transform.localPosition = _genTilePos.localPosition;

            _onReturnPool = onReturnPool;

            gameObject.SetActive(true);
            _canvasGroupTile.alpha = 1f;
        }
        public void TileFalling(float fallingSpeed)
        {
            if (!IsReached())
            {
                transform.DOLocalMoveY(_outOfBoardCheckPoint.localPosition.y, fallingSpeed).SetEase(Ease.Linear).OnComplete(() =>
                {
                    TileReached();
                    _onReturnPool?.Invoke(gameObject);
                });
            }
        }
        protected virtual void TileReached()
        {

        }
        protected virtual bool IsReached()
        {
            return transform.localPosition.y <= _outOfBoardCheckPoint.localPosition.y;
        }
        public abstract void OnPointerDown(PointerEventData eventData);
        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}
