using System;
using UnityEngine;

[CreateAssetMenu]
public class UnitTemplate : ScriptableObject
{
    public UnitData[] unitData;
    public float spawnInterval;

    [Serializable]
    public struct UnitData
    {
        public Sprite sprite;
        public int direction;
        public float maxHealth;
    }
}
