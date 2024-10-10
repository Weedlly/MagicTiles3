using Common.Scripts.Pooler;
using UnityEngine;

namespace GamePlay.TileGeneration
{
    public class TileColHolder : MonoBehaviour
    {
        [SerializeField] private PoolingBase _poolingSingleTile;
        [SerializeField] private PoolingBase _poolingLongTile;
        [SerializeField] private Transform _genTilePos;
        [SerializeField] private Transform _outOfBoardCheckPoint;
        [SerializeField] private float _tileFallingDuration = 0.5f;
        public bool IsEmptyFallingTile() => _curFallingTile == 0;
        
        private int _curFallingTile;
        private void Start()
        {
            _curFallingTile = 0;
            _poolingSingleTile.InitPool();
            _poolingLongTile.InitPool();
        }
        public void SpawningTile(int tileLength)
        {
            if (tileLength <= 1)
            {
                SpawningSingleTile();
            }
            else
            {
                SpawningLongTile(tileLength);
            }
        }
        private void SpawningSingleTile()
        {
            _curFallingTile++;
            TileBase tile = _poolingSingleTile.GetInstance().GetComponent<TileBase>();
            tile.SetUp(_genTilePos, _outOfBoardCheckPoint, OnTileReturnPool);
            tile.TileFalling(_tileFallingDuration);
        }
        private void SpawningLongTile(int longTileLength)
        {
            _curFallingTile++;
            LongTile longTile = _poolingLongTile.GetInstance().GetComponent<LongTile>();
            longTile.SetUp(_genTilePos, _outOfBoardCheckPoint, OnTileReturnPool);
            longTile.SetLongTileLength(longTileLength);
            longTile.TileFalling(_tileFallingDuration);
        }
        private void OnTileReturnPool(GameObject go)
        {
            _curFallingTile--;
            _poolingSingleTile.ReturnPool(go);
        }
    }
}
