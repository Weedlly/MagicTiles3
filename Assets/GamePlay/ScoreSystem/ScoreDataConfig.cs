using Common.Scripts.Data.DataConfig;
using System;
using UnityEngine;

namespace GamePlay.ScoreSystem
{
    [Serializable]
    public struct ScoreConfig
    {
        public int ScoreVal;
    }
    [CreateAssetMenu(menuName = "ScriptableObject/DataConfig/ScoreDataConfig",fileName = "ScoreDataConfig")]
    public class ScoreDataConfig : DataConfigBase<EScoreType,ScoreConfig>
    {
        
    }
}
