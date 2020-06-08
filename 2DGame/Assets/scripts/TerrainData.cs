using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainData
{
    //地形种类
    public TeerainType teerainType;
    //图案
    public Sprite sprite;
    //护甲
    public float armor;
    //额外HP
    public float extraHP;
    //额外花费
    public int extraCost;
}
public enum TeerainType
{
    COMMON,     //普通
    NOMANSLAND, //无人区
    PLAIN,      //平原
    VALLEY      //山谷
}
