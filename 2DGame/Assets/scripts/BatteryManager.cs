using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    public BatteryData HospitalData;
    public BatteryData PowerStationData;
    public BatteryData MaskFactoryData;

    public BatteryType h;

    private float lastTime;   //计时器
    private float curTime;

    //当前选择的炮台--拖动进行创建
    public BatteryData BatterySelectedData;
    private float incMoney = 0;
    private float incWater = 0;
    private float incElectric = 0;

    public int money;
    public int water;
    public int electric;

    //cost for battery
    public Text moneyText;
    public Text waterText;
    public Text electricText;
    public Animator moneyAnimator;
    public Animator waterAnimator;
    public Animator electricAnimator;

    public float n;

    //监听炮台是否被选择,选中进行更新
    public void OnHospitalSelected(bool isOn)
    {
        if (isOn)
        {
            BatterySelectedData = HospitalData;
        }
    }
    public void OnPowerStationSelected(bool isOn)
    {
        if (isOn)
        {
            BatterySelectedData = PowerStationData;
        }
    }
    public void OnMaskFactorySelected(bool isOn)
    {
        if (isOn)
        {
            BatterySelectedData = MaskFactoryData;
        }
    }

    //资源管理
    public void ChangeMoney(int change = 0, int changeWater =0, int changeElectric =0)
    {
        money += change;
        water += changeWater;
        electric += changeElectric;
        moneyText.text = "" + money;
        waterText.text = "" + water;
        electricText.text = "" + electric;
    }

    // Start is called before the first frame update
    void Start()
    {
        BatterySelectedData = HospitalData;
        lastTime = Time.time;
        //设置资源初始文本
        moneyText.text = "" + money;
        waterText.text = "" + water;
        electricText.text = "" + electric;
        //设置屏幕上相应炮台资源文本
        setBatteryText(HospitalData);
        setBatteryText(PowerStationData);
        setBatteryText(MaskFactoryData);
    }

    // Update is called once per frame
    void Update()
    {
        //倒计数后开始一切操作
        curTime = Time.time;
        if(curTime - lastTime >= 8)
        {
            //鼠标在UI上不做处理
            if (Input.GetMouseButtonDown(0))
            {
                //true即鼠标在UI上
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 10;
                    Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
                    //RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero, 1 << (LayerMask.NameToLayer("Maps")));
                    //Collider2D[] col = Physics2D.OverlapPointAll(screenPos);
                    GameObject mapsCreater = GameObject.Find("mapCreater");
                    GameObject target = mapsCreater.GetComponent<mapCreater>().getPointingMap(screenPos.x, screenPos.y);

                    if (target!=null)
                    {
                        MapsBattery battery = target.GetComponent<MapsBattery>();
                        int status = target.GetComponent<Base_command>().status;
                        if (battery.BatteryOnMaps == null && status == 1) //医院建立在绿色地皮上，其他炮台
                        {
                            //map空即能进行建造
                            //判断资源知否足以创建炮台
                            //不同地形额外cost不一样需要加上
                            int cost = BatterySelectedData.cost + target.GetComponent<Base_command>().terrainData.extraCost;
                            int costWater = BatterySelectedData.costWater + target.GetComponent<Base_command>().terrainData.extraWaterCost;
                            int costElectric = BatterySelectedData.costElectric + target.GetComponent<Base_command>().terrainData.extraElectricCost;
                            if (money >= cost && water >= costWater && electric >= costElectric)
                            {
                                ChangeMoney(-cost, -costWater, -costElectric);
                                battery.BuildBattery(BatterySelectedData.batteryPrefab, BatterySelectedData);
                                target.GetComponent<Base_command>().status = 3;
                                target.GetComponent<blue_command>().enabled = true;
                                target.GetComponent<blue_command>().turnBlueEffects();
                            }
                            else
                            {
                                if (money < cost) moneyAnimator.SetTrigger("NoMoney");
                                if (water < costWater) waterAnimator.SetTrigger("NoMoney");
                                if (electric < costElectric) electricAnimator.SetTrigger("NoMoney");
                            }
                        }
                        else
                        {
                            //TODO 
                        }
                    }
                    else
                    {
                    }

                }
                else
                {
                }
            }
            n = GreenNumber.numGreen;
            //根据绿块数量改变资源增长
            /*
                6.15
                加上*SpeedControllor.gameSpeed，即游戏速度的影响
            */
            if (GreenNumber.numGreen > 0)
            {
                incMoney += 0.05f * Mathf.Exp(GreenNumber.numGreen / 10000) * SpeedControllor.gameSpeed;
                //incMoney += (0.05f / (10 * Mathf.Exp(GreenNumber.numGreen / 1000000000))) * SpeedControllor.gameSpeed;
                //print("incMoney " + incMoney);
                if (incMoney > 1)
                {
                    ChangeMoney((int)incMoney,0,0);
                    incMoney = 0;
                }
            }
            if(GreenNumber.numWater > 0)
            {
                incWater += 0.05f * Mathf.Exp(GreenNumber.numWater / 10000) * SpeedControllor.gameSpeed;
                //incWater += (0.05f / (10 * Mathf.Exp(GreenNumber.numWater / 1000000000))) * SpeedControllor.gameSpeed;
                //print("incWater " + incWater);
                if (incWater > 1)
                {
                    ChangeMoney(0, (int)incWater, 0);
                    incWater = 0;
                }
            }
            if(GreenNumber.numElectric > 0)
            {
                incElectric += 0.05f * Mathf.Exp(GreenNumber.numElectric / 10000) * SpeedControllor.gameSpeed;
                //incElectric += (0.05f / (10 * Mathf.Exp(GreenNumber.numElectric / 1000000000))) * SpeedControllor.gameSpeed;
                //print("incElectric " + incElectric);
                if (incElectric > 1)
                {
                    ChangeMoney(0, 0, (int)incElectric);
                    incElectric = 0;
                }
            }
        }
    }

    float getDistance(Vector3 a, Vector3 b)
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y);
    }

    void setBatteryText(BatteryData bd)
    {
        bd.textMoney.text = "" + bd.cost;
        bd.textWater.text = "" + bd.costWater;
        bd.textElectric.text = "" + bd.costElectric;
    }
}
