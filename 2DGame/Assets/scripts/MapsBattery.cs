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
    private bool showInfo;
    private int mapID;
    private int costMoney;
    private int costWater;
    private int costElectric;
    private string mapName;
    private TeerainType mapType;
    /*public enum TeerainType
    {
        COMMON,     //普通
        NOMANSLAND, //无人区
        PLAIN,      //平原
        VALLEY,     //山谷
        CITY        //城市
    }*/


    public GameObject BuildEffect;

    void Start()
    {
        showInfo = false;
        mapID = this.gameObject.GetComponent<Base_command>().number;
        onColor = new Color(0.39f, 0.39f, 0.39f, 1.0f);
        srcColor = this.GetComponent<SpriteRenderer>().color;
        costMoney = this.transform.gameObject.GetComponent<Base_command>().terrainData.extraCost;
        costWater = this.transform.gameObject.GetComponent<Base_command>().terrainData.extraWaterCost;
        costElectric = this.transform.gameObject.GetComponent<Base_command>().terrainData.extraElectricCost;
        print(costMoney);
        print(costWater);
        print(costElectric);
        mapType = this.transform.gameObject.GetComponent<Base_command>().terrainData.teerainType;
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

    //移入显示提示框，标明三大资源消耗
    void OnMouseEnter()
    {
        showInfo = true;
        //print("OnMouseEnter");
        if(BatteryOnMaps == null && EventSystem.current.IsPointerOverGameObject() == false) //
        {
            this.GetComponent<SpriteRenderer>().color = onColor;
        }
    }

    void OnMouseExit()
    {
        showInfo = false;
        /*private const int WHITE = 0;
        private const int GREEN = 1;
        private const int RED = 2;
        private const int BLUE = 3;
        private const int YELLOW = 4;*/
        //print("OnMouseExit");
        //判断状态，根据状态回归本色
        int status = this.gameObject.GetComponent<Base_command>().status;
        if (status == 2) this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.67843f, 0.72549f, 1.0f);
        else if(status == 1) this.GetComponent<SpriteRenderer>().color = new Color(0.46275f, 0.87059f, 0.50589f, 1.0f);
        else if(status == 4) this.GetComponent<SpriteRenderer>().color = new Color(0.87450f, 1.0f, 0.0f, 1.0f);
        else this.GetComponent<SpriteRenderer>().color = srcColor;
    }

    void OnGUI()
    {
        if (showInfo)
        {
            //string name;
            switch ((int)mapType)
            {
                case 0:
                    mapName = "普通地形";
                    break;
                case 1:
                    mapName = "无人区";
                    break;
                case 2:
                    mapName = "平原";
                    break;
                case 3:
                    mapName = "山谷";
                    break;
                case 4:
                    mapName = "城市";
                    break;
                default:
                    break;
            }
            /*GUIStyle style1 = new GUIStyle();
            style1.fontSize = 15;
            style1.normal.textColor = Color.red;
            GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 400, 50), mapName, style1);*/
            GUI.Window(0, new Rect(Input.mousePosition.x+20, Screen.height - Input.mousePosition.y, 160, 80), MyWindow, mapName);
        }
    }

    void MyWindow(int WindowID)
    {
        GUILayout.Label("金钱消耗：" + costMoney + "\n水源消耗：" + costWater + "\n电力消耗：" + costElectric);
    }
}
