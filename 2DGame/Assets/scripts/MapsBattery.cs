using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//maps上炮台管理操作
public class MapsBattery : MonoBehaviour
{
    [HideInInspector]
    public GameObject BatteryOnMaps; //Maps上的炮台
    public BatteryData batteryData; //炮台数据
    public Slider BatterySlider;
    public Vector3 position;
    public Color onColor;
    private Color srcColor;

    public GameObject BuildEffect;

    void Start()
    {
        onColor = new Color(0.39f, 0.39f, 0.39f, 1.0f);
        srcColor = this.GetComponent<SpriteRenderer>().color;
    }

    public void BuildBattery(GameObject batteryPrefab, BatteryData data)
    {
        //print(transform.position);
        batteryData = data;
        position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        Vector3 positionHP = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
        BatteryOnMaps = GameObject.Instantiate(batteryPrefab, position, Quaternion.identity);
        BatterySlider = BatteryOnMaps.GetComponentInChildren<Slider>();
        GameObject effect = GameObject.Instantiate(BuildEffect, position, Quaternion.identity);
        GameObject.Destroy(effect, 1);
    }

    public BatteryData getBatteryData()
    {
        return batteryData;
    }

    void OnMouseEnter()
    {
        print("OnMouseEnter");
        if(BatteryOnMaps == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            this.GetComponent<SpriteRenderer>().color = onColor;
        }
    }

    void OnMouseExit()
    {
        print("OnMouseExit");
        this.GetComponent<SpriteRenderer>().color = srcColor;
    }
}
