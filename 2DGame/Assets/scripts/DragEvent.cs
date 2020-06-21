using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragEvent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private RectTransform rectTransformSlot;
    private Vector2 offset;
    private Vector2 originPos;
    private enum DragState{ DragBegin, Draging};
    public BatteryData BatterySelectedData;

    private void Start()
    {
        rectTransform = transform as RectTransform; ;
        rectTransformSlot = transform.parent as RectTransform;
        originPos = new Vector2(rectTransform.localPosition.x, rectTransform.localPosition.y);
        Debug.Log("originPos:" + originPos);
    }

    /// <summary>
    /// 设置拖拽开始和结束的位置
    /// </summary>
    /// <param name="eventData"></param>
    private void SetDraggedPosition(PointerEventData eventData, DragState dragState)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Vector2 localPos;
            //RectTransformUtility类提供了UI屏幕坐标转换为世界坐标的方式
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransformSlot, Input.mousePosition,
                eventData.pressEventCamera, out localPos))
            {
                if(dragState == DragState.DragBegin)
                {
                    offset = new Vector2(transform.localPosition.x - localPos.x, transform.localPosition.y - localPos.y);
                    Debug.Log("offset:" + offset);
                }
                else if(dragState == DragState.Draging)
                {
                    transform.localPosition = localPos + offset;
                }
            }
        }
    }

    /// <summary>
    /// 开始拖拽！
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("DragBegin:" + eventData.position);
        SetDraggedPosition(eventData, DragState.DragBegin);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Draging:" + eventData.position);
        SetDraggedPosition(eventData, DragState.Draging);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("DragEnd:" + eventData.position);

        Vector3 touchInWordPos;
        touchInWordPos = Camera.main.ScreenToWorldPoint(eventData.position);
        touchInWordPos.z = -15.0f;
        Debug.Log("touchInWordPos:" + touchInWordPos);
        Ray ray = new Ray(touchInWordPos, new Vector3(0.0f, 0.0f, 1.0f));
        Debug.DrawRay(touchInWordPos, new Vector3(0.0f, 0.0f, 1.0f), Color.green, 10.0f);
        RaycastHit2D hit = Physics2D.Raycast(touchInWordPos, Vector2.zero, 1000f, 1 << LayerMask.NameToLayer("Maps"));
        Debug.Log("FLAG:" + hit);
        if (hit)
        {
            //在这里通过hit得到的transform及对应gameObject拥有的属性判断是否可以建设
            Debug.Log("raycastHitTransForm:" + hit.transform.gameObject);
            Debug.Log("currentBuilding:" + transform.gameObject.name);

            GameObject mapsCreater = GameObject.Find("mapCreater");
            GameObject gameManager = GameObject.Find("GameManager");
            BatteryManager batteryManager = gameManager.GetComponent<BatteryManager>();
            Debug.Log("BatteryManager:" + batteryManager);
            GameObject target = mapsCreater.GetComponent<mapCreater>().getPointingMap(touchInWordPos.x, touchInWordPos.y);
            Debug.Log("target:" + target);
            if (target != null)
            {
                MapsBattery battery = target.GetComponent<MapsBattery>();
                int status = target.GetComponent<Base_command>().status;
                if (battery.BatteryOnMaps == null && status == 1) //医院建立在绿色地皮上，其他炮台
                {
                    //map空即能进行建造
                    //判断资源知否足以创建炮台
                    //不同地形额外cost不一样需要加上
                    Debug.Log("cost:" + BatterySelectedData.cost);
                    Debug.Log("batteryManager.money:" + batteryManager.money);
                    int cost = BatterySelectedData.cost + target.GetComponent<Base_command>().terrainData.extraCost;
                    int costWater = BatterySelectedData.costWater + target.GetComponent<Base_command>().terrainData.extraWaterCost;
                    int costElectric = BatterySelectedData.costElectric + target.GetComponent<Base_command>().terrainData.extraElectricCost;
                    if (batteryManager.money >= cost && batteryManager.water >= costWater && batteryManager.electric >= costElectric)
                    {
                        batteryManager.ChangeMoney(-cost, -costWater, -costElectric);
                        battery.BuildBattery(BatterySelectedData.batteryPrefab, BatterySelectedData);
                        target.GetComponent<Base_command>().status = 3;
                        target.GetComponent<blue_command>().enabled = true;
                        target.GetComponent<blue_command>().turnBlueEffects();
                    }
                    else
                    {
                        if (batteryManager.money < cost) batteryManager.moneyAnimator.SetTrigger("NoMoney");
                        if (batteryManager.water < costWater) batteryManager.waterAnimator.SetTrigger("NoMoney");
                        if (batteryManager.electric < costElectric) batteryManager.electricAnimator.SetTrigger("NoMoney");
                    }
                }
            }
            if (hit.transform.name.Equals("Maps(Clone)"))
            {
                Debug.Log("DragEndRayCast:" + hit.transform.name);
                Debug.Log("被碰撞的物体是：" + hit.collider.gameObject.name);
            }
        }
        Debug.Log("originPos:" + originPos);
        transform.localPosition = originPos;
    }
}