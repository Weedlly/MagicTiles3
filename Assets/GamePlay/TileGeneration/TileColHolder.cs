using Common.Scripts.Pooler;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.TileGeneration
{
    public class TilePooling
    {
        
    }
    public class TileColHolder : MonoBehaviour
    {
        [SerializeField] private PoolingBase _poolingSingleTile;
        [SerializeField] private Transform _genTilePos;
        [SerializeField] private Transform _outOfBoardCheckPoint;
        [SerializeField] private float _tileFallingDuration = 0.5f;
        public bool IsEmptyFallingTile() => _curFallingTile == 0;
        
        private int _curFallingTile;
        private void Start()
        {
            _curFallingTile = 0;
            _poolingSingleTile.InitPool();
        }
        public void SpawningTile()
        {
            _curFallingTile++;
            TileBase tile = _poolingSingleTile.GetInstance().GetComponent<TileBase>();
            tile.SetUp(_genTilePos, _outOfBoardCheckPoint, OnTileReturnPool);
            tile.TileFalling(_tileFallingDuration);
        }
        private void OnTileReturnPool(GameObject go)
        {
            _curFallingTile--;
            _poolingSingleTile.ReturnPool(go);
        }
    }
}
