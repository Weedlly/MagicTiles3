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
            
            int rdTileHolderIdx = Random.Range(0, 3);
            
            if (onDetectBeatPayload.BeatVal > 0.96f)
            {
                foreach (TileColHolder tileColHolder in _tileColHolders)
                {
                    if (!tileColHolder.IsEmptyFallingTile())
                        continue;
                
                    tileColHolder.SpawningLongTile(2);
                    return;
                }
                
                _tileColHolders[rdTileHolderIdx].SpawningLongTile(2);
                return;
            }
            foreach (TileColHolder tileColHolder in _tileColHolders)
            {
                if (!tileColHolder.IsEmptyFallingTile())
                    continue;
                
                tileColHolder.SpawningSingleTile();
                return;
            }
            
            _tileColHolders[rdTileHolderIdx].SpawningSingleTile();
        }
    }
}
