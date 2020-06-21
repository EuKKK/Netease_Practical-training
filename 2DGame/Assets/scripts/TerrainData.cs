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
    public int extraWaterCost;
    public int extraElectricCost;
    //对资源增长速率的影响
    public float incRate;
    public float incWaterRate;
    public float incElectricRate;
}
public enum TeerainType
{
    COMMON,     //普通
    NOMANSLAND, //无人区
    PLAIN,      //平原
    VALLEY,     //山谷
    CITY        //城市
}
