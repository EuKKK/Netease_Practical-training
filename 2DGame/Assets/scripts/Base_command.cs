using System.Collections;
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
    
    //格子的编号
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        //status=0;
    }

    // Update is called once per frame
    public Collider2D[] col;
    void Update()
    {

        //寻找能够攻击到本格子的目标，将他们的影响加到自身上
        //col = Physics2D.OverlapCircleAll(this.transform.position, 0.5f);
        //foreach (Collider2D cc in col)
        //{
        //    Base_command b = cc.gameObject.GetComponent<Base_command>();
        //    float attrackValue = b.getAttackValue(this.gameObject);
        //    //对于每一次攻击，都需要先减去护甲计算
        //    if (attrackValue > 0)
        //        attrackValue = (attrackValue - terrainData.armor)>0? (attrackValue - terrainData.armor):0;
        //    else
        //        attrackValue = (attrackValue + terrainData.armor) < 0 ? (attrackValue + terrainData.armor) : 0;
        //    if (status == BLUE)
        //    {
        //        //若为蓝色，应对炮台施加影响，减去炮台HP
        //        this.gameObject.GetComponent<MapsBattery>().batteryData.HP += Time.deltaTime * attrackValue;
        //        this.gameObject.GetComponent<MapsBattery>().BatterySlider.value = this.gameObject.GetComponent<MapsBattery>().batteryData.HP / this.gameObject.GetComponent<MapsBattery>().batteryData.totalHP;
        //    }
        //    else
        //    {
        //        HP += Time.deltaTime * attrackValue;
        //    }
        //}

        //自己对自己施加影响
        //if (status == BLUE)
        //{
        //    //若为蓝色，应对炮台施加影响，减去炮台HP
        //    this.gameObject.GetComponent<MapsBattery>().batteryData.HP += Time.deltaTime * this.getAttackValue(this.gameObject);
        //    this.gameObject.GetComponent<MapsBattery>().BatterySlider.value = this.gameObject.GetComponent<MapsBattery>().batteryData.HP / this.gameObject.GetComponent<MapsBattery>().batteryData.totalHP;
        //}
        //else
        //{
        //    HP += Time.deltaTime * this.getAttackValue(this.gameObject);
        //}
        //HP += Time.deltaTime * this.getAttackValue(this.gameObject);
        //新增---by lee 保存临时状态
        /*
            6.13转变为主动攻击，寻找范围内目标，操作他们的血量 
            当且仅当格子状态为红蓝才会攻击
            6.15
            血量操作*SpeedControllor.gameSpeed，和游戏速度关联
        */
        if (status == RED || status == BLUE)
        {
            GameObject mapsCreater = GameObject.Find("mapCreater");
            foreach (GameObject g in mapsCreater.GetComponent<mapCreater>().getGrids(number, GetAttackDistance()))
            {
                g.GetComponent<Base_command>().ChangeHP(Time.deltaTime * getAttackValue(g)*SpeedControllor.gameSpeed);
            }
            //对自己操作
            ChangeHP(Time.deltaTime * getAttackValue(gameObject) * SpeedControllor.gameSpeed);
        }
    }

    //改变血量会引起状态改变因此封装到一起
    public void ChangeHP(float attack)
    {
        //对于每一次攻击，都需要先减去护甲计算
        if (attack > 0)
            attack = (attack - terrainData.armor) > 0 ? (attack - terrainData.armor) : 0;
        else
            attack = (attack + terrainData.armor) < 0 ? (attack + terrainData.armor) : 0;
        if (attack == 0)
            return;
        //血量操作
        if (status == BLUE)
        {
            //若为蓝色，应对炮台施加影响，减去炮台HP
            gameObject.GetComponent<MapsBattery>().batteryData.HP += attack;
            gameObject.GetComponent<MapsBattery>().BatterySlider.value = this.gameObject.GetComponent<MapsBattery>().batteryData.HP / this.gameObject.GetComponent<MapsBattery>().batteryData.totalHP;
        }
        else
        {
            HP += attack;
        }
        //状态转换
        if (status == BLUE)
        {
            //炮塔HP未到达0则无影响
            //上下界处理
            if (gameObject.GetComponent<MapsBattery>().batteryData.HP > gameObject.GetComponent<MapsBattery>().batteryData.totalHP)
            {
                gameObject.GetComponent<MapsBattery>().batteryData.HP = gameObject.GetComponent<MapsBattery>().batteryData.totalHP;
            }
            else if (gameObject.GetComponent<MapsBattery>().batteryData.HP <= redThreshold)//《=可能有问题
            {
                //摧毁炮台，改变状态
                Vector3 position = gameObject.GetComponent<MapsBattery>().position;
                GameObject.Destroy(gameObject.GetComponent<MapsBattery>().BatteryOnMaps);
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
            //超过绿色转换界限
            if (HP >= greenThreshold)
            {
                if (status == WHITE || status == RED || status == YELLOW)
                {
                    if (status == RED)
                        gameObject.GetComponent<red_command>().ReSetRedEffects();
                    status = GREEN;
                    this.gameObject.GetComponent<red_command>().enabled = false;
                    this.gameObject.GetComponent<green_command>().enabled = true;
                    //GreenNumber.numGreen += 1;
                    GreenNumber.numGreen += terrainData.incRate;
                    GreenNumber.numWater += terrainData.incWaterRate;
                    GreenNumber.numElectric += terrainData.incElectricRate;
                    //转换成绿色的具体操作
                    this.gameObject.GetComponent<green_command>().turnGreenEffects();
                }
                //判断变色之前是否为黄色，是则赢了
                if (temp_status == YELLOW)
                    SceneManager.LoadScene(3);
            }
            else if (HP <= redThreshold)    //超过红色转换界限
            {
                if (status != RED)
                {
                    if (status == GREEN)
                    {
                        GreenNumber.numGreen -= terrainData.incRate;
                        GreenNumber.numWater -= terrainData.incWaterRate;
                        GreenNumber.numElectric -= terrainData.incElectricRate;
                    }
                        //GreenNumber.numGreen -= 1;
                    
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
                    SceneManager.LoadScene(2);
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

    //获取当前格子的攻击距离
    int GetAttackDistance()
    {
        if (status == RED)
            return gameObject.GetComponent<red_command>().GetAttackDistance();
        else if (status == BLUE)
            return gameObject.GetComponent<blue_command>().GetAttackDistance();
        return 0;
    }
}
