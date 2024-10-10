using GamePlay.ScoreSystem;
using SuperMaxim.Messaging;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GamePlay.TileGeneration
{
    public class LongTile : TileBase
    {
        [SerializeField] private Image _imgTileFill;
        private const float HoldingFactor = 0.15f;
        private bool _isPointerDown;
        private float _holdingDuration;
        private float _maxHoldingDuration;
        private int _longTileLength;
        public override void SetUp(Transform genTilePos, Transform outOfBoardCheckPoint, Action<GameObject> onReturnPool)
        {
            base.SetUp(genTilePos, outOfBoardCheckPoint, onReturnPool);
            _isPointerDown = false;
            _holdingDuration = 0f;
            _imgTileFill.fillAmount = 0;
        }
        public void SetLongTileLength(int longTileLength)
        {
            _longTileLength = longTileLength;
            _maxHoldingDuration = HoldingFactor * longTileLength;
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            _isPointerDown = true;
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (_isPointerDown)
                return;
            _isPointerDown = false;
            _canvasGroupTile.alpha = 0f;
            Messenger.Default.Publish(new OnTapTilePayload
            {
                Tile = gameObject,
                TileLength = _longTileLength,
            });
        }
        protected override void TileReached()
        {
            if (!_isPointerDown)
                return;
            Messenger.Default.Publish(new OnTapTilePayload
            {
                Tile = gameObject,
                TileLength = (int)TileLengthFilled(),
            });
        }
        private float TileLengthFilled()
        {
            return _holdingDuration != 0f ? _holdingDuration / _maxHoldingDuration : 0;
        }
        private void UpdateTileFill()
        {
            _imgTileFill.fillAmount = TileLengthFilled();
        }
        private void Update()
        {
            if (_isPointerDown)
            {
                _holdingDuration += Time.deltaTime;
                UpdateTileFill();
            }
        }
    }
}
