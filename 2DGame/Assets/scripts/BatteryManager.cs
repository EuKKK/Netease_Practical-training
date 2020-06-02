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

    //当前选择的炮台--拖动进行创建
    private BatteryData BatterySelectedData;
    private float incMoney=0;

    private int money = 2000;
    public Text moneyText;
    public Animator moneyAnimator;

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
    void ChangeMoney(int change=0)
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //鼠标在UI上不做处理
        if (Input.GetMouseButtonDown(0))
        {
            //true即鼠标在UI上
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10;
                Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
                RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero, 1 << (LayerMask.NameToLayer("Maps")));
                if (hit)
                {
                    MapsBattery battery = hit.collider.GetComponent<MapsBattery>();
                    int status = hit.collider.GetComponent<Base_command>().status;
                    if (battery.BatteryOnMaps == null && status==1)
                    {
                        //map空即能进行建造
                        //判断资源知否足以创建炮台
                        if (money >= BatterySelectedData.cost)
                        {
                            ChangeMoney(-BatterySelectedData.cost);
                            battery.BuildBattery(BatterySelectedData.batteryPrefab, BatterySelectedData);
                        }
                        else
                        {
                            //TODO 进行资源不足提示
                            moneyAnimator.SetTrigger("NoMoney");
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

        //根据绿块数量改变资源增长
        if (GreenNumber.numGreen > 0)
        {
            incMoney += 1 / (10*Mathf.Exp(GreenNumber.numGreen / 1000000000));
            if (incMoney > 1)
            {
                ChangeMoney((int)incMoney);
                incMoney = 0;
            }
        }
    }
}
