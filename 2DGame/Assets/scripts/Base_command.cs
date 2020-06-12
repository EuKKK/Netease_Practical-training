﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**********************************************************
 * 
 * Maps 初始值为50（白块）
 * HP > 100， 转绿
 * HP < 0，转红
 * 
 * 假定阈值
 * 白色 [50,100]
 * 绿色 (100,150]
 * 红色 [-50,0)
 * ***********************************************************/
public class Base_command : MonoBehaviour
{
    public float HP = 50f;

    //地形数据
    public TerrainData terrainData;
    public GameObject DestoryEffect; //摧毁特效

    private const int WHITE = 0;
    private const int GREEN = 1;
    private const int RED = 2;
    private const int BLUE = 3;
    private const int YELLOW = 4;
 
    public BatteryData batData;

    public float greenThreshold = 100f;
    public float redThreshold = 0f;

    public float HPupperThreshold = 150f;
    public float HPlowerThreshold = -50f;

    public float speed = 10f;
    public int status = 0;
    // Start is called before the first frame update
    void Start()
    {
        //status=0;
    }

    // Update is called once per frame
    public Collider2D[] col;
    void Update()
    {
        //lrs原版
        /*col=Physics2D.OverlapCircleAll(this.transform.position, 0.5f);
        foreach(Collider2D cc in col){
            int ccstatus=cc.gameObject.GetComponent<Base_command>().status;
            if(ccstatus==2&&this.HP>0){
                this.HP-=Time.deltaTime*speed;
            }
            else if(ccstatus==1&&this.HP<100)
            {
                this.HP+=Time.deltaTime*speed;
            }
            if(HP<=0&&this.status!=2)
            {
                this.gameObject.GetComponent<red_command>().enabled=true;
                this.gameObject.GetComponent<green_command>().enabled=false;
            }
            else if(HP>=100&&this.status!=1)
            {
                this.gameObject.GetComponent<red_command>().enabled=false;
                this.gameObject.GetComponent<green_command>().enabled=true;
            }
        }*/
        /*
        //主动攻击代替被动接受  仅存在于红、蓝
        float attackdist;
        if(this.status == 2 || this.status == 3)
        {
            if (this.status == 3)
            {
                //获取炮台数据
                MapsBattery battery = this.gameObject.GetComponent<MapsBattery>();
                batData = battery.getBatteryData();
                attackdist = batData.cureDistance;
            }
            else attackdist = 0.5f;

            col = Physics2D.OverlapCircleAll(this.transform.position, attackdist);
            foreach (Collider2D cc in col)
            {
                if (this.status == 2)
                {
                    cc.gameObject.GetComponent<Base_command>().HP -= Time.deltaTime * speed;
                }
                else
                {
                    //暂定炮台治愈能力强于感染
                    cc.gameObject.GetComponent<Base_command>().HP += Time.deltaTime * speed * batData.cureVal;
                }

                float cchp = cc.gameObject.GetComponent<Base_command>().HP;
                int ccstatus = cc.gameObject.GetComponent<Base_command>().status;
                if (cchp <= 0 && ccstatus != 2)
                {
                    //若为蓝转红，摧毁上方炮台
                    if (ccstatus == 3)
                    {
                        GameObject.Destroy(cc.gameObject.GetComponent<MapsBattery>().BatteryOnMaps);
                        GreenNumber.numGreen -= 1;
                    }
                    else if(ccstatus == 1)
                    {
                        GreenNumber.numGreen -= 1;
                    }
                    cc.gameObject.GetComponent<Base_command>().HP = 0;
                    cc.gameObject.GetComponent<red_command>().enabled = true;
                    cc.gameObject.GetComponent<green_command>().enabled = false;
                }
                else if (cchp >= 100 && (ccstatus != 1&& ccstatus != 3))
                {
                    cchp = cc.gameObject.GetComponent<Base_command>().HP = 100;
                    cc.gameObject.GetComponent<red_command>().enabled = false;
                    cc.gameObject.GetComponent<green_command>().enabled = true;
                    GreenNumber.numGreen += 1;
                }
            }
        }
        */
        //寻找能够攻击到本格子的目标，将他们的影响加到自身上
        col = Physics2D.OverlapCircleAll(this.transform.position, 0.5f);
        foreach (Collider2D cc in col)
        {
            Base_command b = cc.gameObject.GetComponent<Base_command>();
            float attrackValue = b.getAttackValue(this.gameObject);
            //对于每一次攻击，都需要先减去护甲计算
            if (attrackValue > 0)
                attrackValue = (attrackValue - terrainData.armor)>0? (attrackValue - terrainData.armor):0;
            else
                attrackValue = (attrackValue + terrainData.armor) < 0 ? (attrackValue + terrainData.armor) : 0;
            if (status == BLUE)
            {
                //若为蓝色，应对炮台施加影响，减去炮台HP
                this.gameObject.GetComponent<MapsBattery>().batteryData.HP += Time.deltaTime * attrackValue;
                this.gameObject.GetComponent<MapsBattery>().BatterySlider.value = this.gameObject.GetComponent<MapsBattery>().batteryData.HP / this.gameObject.GetComponent<MapsBattery>().batteryData.totalHP;
            }
            else
            {
                HP += Time.deltaTime * attrackValue;
            }
        }

        //自己对自己施加影响
        if (status == BLUE)
        {
            //若为蓝色，应对炮台施加影响，减去炮台HP
            this.gameObject.GetComponent<MapsBattery>().batteryData.HP += Time.deltaTime * this.getAttackValue(this.gameObject);
            this.gameObject.GetComponent<MapsBattery>().BatterySlider.value = this.gameObject.GetComponent<MapsBattery>().batteryData.HP / this.gameObject.GetComponent<MapsBattery>().batteryData.totalHP;
        }
        else
        {
            HP += Time.deltaTime * this.getAttackValue(this.gameObject);
        }
        //HP += Time.deltaTime * this.getAttackValue(this.gameObject);
        //新增---by lee 保存临时状态
        if(status == BLUE)
        {
            //炮塔HP未到达0则无影响
            //上下界处理
            if(this.gameObject.GetComponent<MapsBattery>().batteryData.HP > this.gameObject.GetComponent<MapsBattery>().batteryData.totalHP)
            {
                this.gameObject.GetComponent<MapsBattery>().batteryData.HP = this.gameObject.GetComponent<MapsBattery>().batteryData.totalHP;
            }
            else if(this.gameObject.GetComponent<MapsBattery>().batteryData.HP <= redThreshold)
            {
                //摧毁炮台，改变状态
                Vector3 position = this.gameObject.GetComponent<MapsBattery>().position;
                GameObject.Destroy(this.gameObject.GetComponent<MapsBattery>().BatteryOnMaps);
                GameObject effect = GameObject.Instantiate(DestoryEffect, position, Quaternion.identity);
                GameObject.Destroy(effect, 1);
                status = GREEN;
                this.gameObject.GetComponent<red_command>().enabled = false;
                this.gameObject.GetComponent<green_command>().enabled = true;
                this.gameObject.GetComponent<blue_command>().enabled = false;
                //GreenNumber.numGreen -= 1;
            }
        }
        else
        {
            int temp_status = status;
            if (HP >= greenThreshold)
            {
                if (status == WHITE || status == RED || status == YELLOW)
                {
                    status = GREEN;
                    this.gameObject.GetComponent<red_command>().enabled = false;
                    this.gameObject.GetComponent<green_command>().enabled = true;
                    //GreenNumber.numGreen += 1;
                    GreenNumber.numGreen += terrainData.incRate;
                    //转换成绿色的具体操作
                    this.gameObject.GetComponent<green_command>().turnGreenEffects();
                }
                //判断变色之前是否为黄色，是则赢了
                if (temp_status == YELLOW)
                    SceneManager.LoadScene(2);
            }
            else if (HP <= redThreshold)
            {
                if (status != RED)
                {
                    if (status == GREEN)
                        //GreenNumber.numGreen -= 1;
                        GreenNumber.numGreen -= terrainData.incRate;
                    /*if (status == BLUE)
                    {
                        //摧毁炮塔
                        GameObject.Destroy(this.gameObject.GetComponent<MapsBattery>().BatteryOnMaps);
                        GreenNumber.numGreen -= 1;
                    }*/
                    status = RED;
                    this.gameObject.GetComponent<red_command>().enabled = true;
                    this.gameObject.GetComponent<green_command>().enabled = false;
                    this.gameObject.GetComponent<blue_command>().enabled = false;
                    //转换成红色的具体操作
                    this.gameObject.GetComponent<red_command>().turnRedEffects();
                }
                ///新增---by lee 判断先前颜色是否为黄色，是输了
                if (temp_status == YELLOW)
                    SceneManager.LoadScene(3);
            }
            //设置上下限
            //需要考虑地形额外的HP
            if (HP > HPupperThreshold + terrainData.extraHP)
                HP = HPupperThreshold;
            if (HP < HPlowerThreshold - terrainData.extraHP)
                HP = HPlowerThreshold;
        }

}

    public float getAttackValue(GameObject g)
    {
        if (this.status == 2)
        {
            //Debug.LogWarning("here");
            red_command r = this.gameObject.GetComponent<red_command>();
            return r.getAttackValue(g);
        }
        if (this.status == 3)
        {
            blue_command b = this.gameObject.GetComponent<blue_command>();
            return b.getAttackValue(g);
        }

        return 0f;
    }

    public void ChangeTerrain()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = terrainData.sprite;
    }
}
