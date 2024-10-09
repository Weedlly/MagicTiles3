using DG.Tweening;
using GamePlay.ScoreSystem;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.TileGeneration
{
    public abstract class TileBase : MonoBehaviour
    {
        [SerializeField] protected Image _imgTile;
        [SerializeField] private Button _btn;
        
        protected Transform _genTilePos;
        protected Transform _outOfBoardCheckPoint;
        private Action<GameObject> _onReturnPool;
        private void Start()
        {
            _btn.onClick.AddListener(OnTapTile);
        }
        public virtual void SetUp(Transform genTilePos, Transform outOfBoardCheckPoint, Action<GameObject> onReturnPool)
        {
            _genTilePos = genTilePos;
            transform.localPosition = _genTilePos.localPosition;
            
            _outOfBoardCheckPoint = outOfBoardCheckPoint;
            _onReturnPool = onReturnPool;
            gameObject.SetActive(true);
            _imgTile.enabled = true;
        }
        public void TileFalling(float fallingSpeed)
        {
            if (!IsReached())
            {
                transform.DOLocalMoveY(_outOfBoardCheckPoint.localPosition.y, fallingSpeed).OnComplete(() =>
                {
                    _onReturnPool?.Invoke(gameObject);
                });
            }
        }
        protected virtual void OnTapTile()
        {
            _imgTile.enabled = false;
            Messenger.Default.Publish(new OnTapTilePayload
            {
                Tile = gameObject,
            });
        }
            
        protected virtual bool IsReached()
        {
            return transform.localPosition.y <= _outOfBoardCheckPoint.localPosition.y;
        }
    }
}
