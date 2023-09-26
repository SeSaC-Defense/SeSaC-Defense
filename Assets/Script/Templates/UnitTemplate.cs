using System;
using UnityEngine;

[CreateAssetMenu]
public class UnitTemplate : ScriptableObject
{
    public UnitPlayerSideData[] playerSideData;
    public float maxHealth;
    public float spawnInterval;

    [Serializable]
    public struct UnitPlayerSideData
    {
        public Sprite sprite;
        public int direction;
    }
}
