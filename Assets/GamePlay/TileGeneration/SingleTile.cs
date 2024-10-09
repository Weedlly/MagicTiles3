using GamePlay.ScoreSystem;
using SuperMaxim.Messaging;
using UnityEngine.EventSystems;

namespace GamePlay.TileGeneration
{
    public class SingleTile : TileBase
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            _canvasGroupTile.alpha = 0f;
            Messenger.Default.Publish(new OnTapTilePayload
            {
                Tile = gameObject,
                TileLength = 0,
            });
        }
    }
}
