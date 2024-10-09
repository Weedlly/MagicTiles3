using Common.Scripts;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.TileGeneration
{
    public class GenerationTileSystem
    {
        
    }
    public class GenTiles : MonoBehaviour
    {
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
            if (onDetectBeatPayload.BeatVal < 0.8f)
                return;
            foreach (TileColHolder tileColHolder in _tileColHolders)
            {
                if (!tileColHolder.IsEmptyFallingTile())
                    continue;
                
                tileColHolder.SpawningTile();
                return;
            }
            int rdTileHolderIdx = Random.Range(0, 3);
            _tileColHolders[rdTileHolderIdx].SpawningTile();
        }
    }
}
