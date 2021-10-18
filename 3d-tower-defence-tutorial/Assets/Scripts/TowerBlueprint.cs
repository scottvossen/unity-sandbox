using System;
using UnityEngine;

[Serializable]
public class TowerBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int sellValue;
}
