using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maps上炮台管理操作
public class MapsBattery : MonoBehaviour
{
    [HideInInspector]
    public GameObject BatteryOnMaps; //Maps上的炮台
    public BatteryData batteryData; //炮台数据

    public GameObject BuildEffect;

    public void BuildBattery(GameObject batteryPrefab, BatteryData data)
    {
        //print(transform.position);
        batteryData = data;
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        BatteryOnMaps = GameObject.Instantiate(batteryPrefab, position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(BuildEffect, position, Quaternion.identity);
        GameObject.Destroy(effect, 1);
    }

    public BatteryData getBatteryData()
    {
        return batteryData;
    }
}
