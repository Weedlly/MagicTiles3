using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Pooler
{
    public class PoolingBase : MonoBehaviour
    {
        [SerializeField] private int _initNumber;
        [SerializeField] private GameObject _parent;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private List<GameObject> _poolObjects;
        public void InitPool()
        {
            _poolObjects = new List<GameObject>();
            for (int i = 0; i < _initNumber; i++)
            {
                _poolObjects.Add(InitObjectInstance());
            }
        }
        private GameObject InitObjectInstance()
        {
            GameObject instance = Instantiate(_prefab, _parent.transform, true);
            instance.SetActive(false);
            return instance;
        }
        public GameObject GetInstance()
        {
            foreach (GameObject i in _poolObjects)
                if (i.gameObject.activeSelf == false)
                    return i;
            GameObject go = InitObjectInstance();
            _poolObjects.Add(go);
            return go;
        }
        public void ReturnPool(GameObject gameObject)
        {
            gameObject.gameObject.SetActive(false);
        }
    }
}
