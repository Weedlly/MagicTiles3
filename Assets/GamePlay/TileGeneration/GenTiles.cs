using SuperMaxim.Messaging;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.TileGeneration
{
    public class GenTiles : MonoBehaviour
    {
        [SerializeField] private float _minTileDurationFactor = 0.24f;
        [SerializeField] private List<TileColHolder> _tileColHolders;
        private void Start()
        {
            Messenger.Default.Subscribe<OnDetectBeatPayload>(SpawningTiles);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<OnDetectBeatPayload>(SpawningTiles);
        }
        private void SpawningTiles(OnDetectBeatPayload onDetectBeatPayload)
        {
            // Debug.Log(onDetectBeatPayload.Note.Name + "  " + onDetectBeatPayload.Note.Duration);

            int tileLength = (int)(onDetectBeatPayload.Note.Duration / _minTileDurationFactor) / 2;
            int rdTileHolderIdx = Random.Range(0, 3);
            
            foreach (TileColHolder tileColHolder in _tileColHolders)
            {
                if (!tileColHolder.IsEmptyFallingTile())
                    continue;
                
                tileColHolder.SpawningTile(tileLength);
                return;
            }
            
            _tileColHolders[rdTileHolderIdx].SpawningTile(tileLength);
        }
    }
}
