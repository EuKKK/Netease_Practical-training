using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapCreater : MonoBehaviour
{
    //5种地形数据
    public TerrainData commonTerrain;
    public TerrainData noMansLandTerrain;
    public TerrainData plainTerrain;
    public TerrainData valleyTerrain;
    public TerrainData cityTerrain;
    //地形数组，存储对应格子的地形，暂时没有更加好的加载方法
    public int[] terrainArray;

    public GameObject map;
    public GameObject[] maps;
    public GameObject red_effect;
    public int red_r, red_c, yellow_r, yellow_c, green_r, green_c;

    public GameObject number1;
    public GameObject number2;
    public GameObject number3;
    public GameObject textGo;
    public GameObject num1;
    public GameObject num2;
    public GameObject num3;
    public GameObject tgo;
    public Vector3 countDownPos = new Vector3(0, 0, -1);

    public int level;
    private int rows;
    private int cols;
    public float width;                     //格子宽度
    private const float rates = 1.73205f;
    public bool is_align_center=true;       //是否居中
    public Vector3 top_left_coordinate;     //左上角坐标，居中的话自己计算不需要提供
    // Start is called before the first frame update
    void Start()
    {
        switch (level)
        {
            case 1:
                {
                    rows = 4;
                    cols = 6;
                    terrainArray = new int[24] { 0,0,0,4,4,4,
                                                 2,2,4,4,4,4,
                                                 2,2,0,4,4,4,
                                                 0,0,3,3,3,3 };
                    red_r = 0;
                    red_c = 4;
                    green_r = 2;
                    green_c = 1;
                    yellow_r = 3;
                    yellow_c = 4;
                    break;
                }
            case 2:
                {
                    rows = 9;
                    cols = 12;
                    terrainArray = new int[108] { 4,4,3,3,0,4,4,0,3,3,0,0,
                                                  4,3,3,2,0,4,1,1,0,0,3,0,
                                                  2,2,0,2,2,2,3,1,0,1,1,0,
                                                  0,0,0,3,3,1,1,1,1,0,4,4,
                                                  1,1,1,1,2,1,1,1,1,0,4,2,
                                                  1,0,0,0,1,1,1,0,1,0,0,4,
                                                  1,2,0,1,2,1,1,0,4,4,4,2,
                                                  1,1,2,3,2,2,0,4,2,3,2,4,
                                                  1,3,3,3,2,0,4,4,2,3,3,4 };
                    red_r = 6;
                    red_c = 7;
                    green_r = 2;
                    green_c = 2;
                    yellow_r = 2;
                    yellow_c = 8;
                    break;
                }
            case 3:
                {
                    rows = 20;
                    cols = 24;
                    terrainArray = new int[480]
                                                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,3,3,3,1,1,0,
                                                  1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,2,2,3,3,3,1,0,0,
                                                  1,1,1,1,0,3,2,1,1,1,1,1,1,1,1,3,3,3,2,3,2,1,1,0,
                                                  1,1,1,0,0,2,0,0,1,1,1,1,1,1,1,2,2,4,2,2,4,0,1,4,
                                                  1,1,1,0,0,4,0,1,1,1,1,1,1,1,1,0,0,1,3,3,1,1,1,1,
                                                  1,1,3,1,1,2,3,0,1,1,1,1,1,1,1,1,1,1,1,2,1,1,0,1,
                                                  4,0,3,2,1,0,3,2,1,1,1,1,1,1,1,1,0,0,0,1,1,1,0,3,
                                                  0,2,4,1,1,1,1,2,0,1,1,1,1,1,1,1,2,0,1,4,1,1,4,3,
                                                  0,2,3,1,1,0,2,4,2,0,1,1,1,1,1,1,1,2,0,1,2,0,0,2,
                                                  2,3,1,1,1,0,3,3,4,0,2,1,1,1,1,1,0,0,2,2,0,0,0,2,
                                                  1,1,1,1,1,4,2,2,0,2,1,1,1,1,1,0,0,0,4,3,2,0,0,2,
                                                  1,1,1,1,3,3,1,0,1,2,1,1,1,1,2,4,2,0,2,3,0,4,0,0,
                                                  1,1,1,1,1,1,1,1,1,1,1,1,4,0,0,0,0,0,2,2,0,2,0,0,
                                                  1,1,1,1,1,1,1,1,0,1,0,2,0,0,2,0,4,0,0,0,2,2,2,2,
                                                  1,1,1,1,0,0,0,1,0,4,0,2,3,0,2,2,0,0,2,0,0,0,0,2,
                                                  1,1,1,1,1,0,4,2,0,0,4,3,0,0,0,0,3,2,2,0,0,2,0,4,
                                                  1,1,1,1,1,1,1,0,0,0,0,2,2,2,0,2,3,0,0,4,0,0,2,0,
                                                  1,1,1,1,1,1,1,0,0,0,0,0,0,0,2,3,2,0,2,0,2,0,4,0,
                                                  1,1,1,1,1,1,1,0,2,2,2,0,0,3,2,2,4,0,3,2,3,0,2,0,
                                                  1,1,1,1,1,1,1,4,2,2,0,0,4,3,3,2,3,2,3,3,3,3,0,0
                                                };
                    red_r = 7;
                    red_c = 19;
                    green_r = 7;
                    green_c = 2;
                    yellow_r = 19;
                    yellow_c = 7;
                    break;
                }
            case 4:
                {
                   rows = 16;
                    cols = 27;////////////////////1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7
                    terrainArray = new int[432] { 1,4,3,2,0,0,4,3,3,2,3,2,3,3,3,3,0,0,0,2,2,3,2,0,2,3,0,    //1
                                                  3,0,3,2,0,2,3,3,3,3,3,3,3,3,2,2,4,0,0,0,2,3,0,0,2,0,0,    //2
                                                  0,3,0,2,2,3,3,3,2,2,3,4,0,2,0,0,0,2,2,0,4,0,0,0,0,4,1,    //3
                                                  2,0,0,4,0,3,0,0,0,0,0,0,2,0,0,1,0,0,2,2,0,0,1,1,0,1,0,    //4
                                                  2,0,0,0,2,2,0,4,2,2,1,2,3,3,0,1,1,4,0,0,2,0,1,1,1,1,1,    //5
                                                  0,2,2,0,4,2,1,1,1,1,1,1,1,4,0,1,1,0,2,4,3,1,1,1,1,1,1,    //6
                                                  0,0,0,0,1,1,1,1,1,1,1,4,1,2,0,0,0,1,1,2,0,0,1,1,1,1,0,    //7
                                                  0,0,0,1,0,1,1,1,1,1,1,1,1,1,1,4,3,0,1,0,2,4,1,0,0,2,2,    //8
                                                  3,2,2,1,1,1,1,1,1,1,1,0,1,1,1,1,2,1,1,2,4,1,0,0,2,3,3,    //9
                                                  0,2,1,1,1,1,1,1,1,1,1,1,1,4,0,1,1,1,1,1,0,1,0,3,0,2,0,    //0
                                                  2,1,1,1,1,1,1,1,1,1,1,1,1,3,1,1,1,1,1,1,1,1,1,0,0,0,0,    //1
                                                  1,1,1,1,1,1,0,1,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,    //2
                                                  1,2,2,1,0,4,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,    //3
                                                  0,2,0,0,0,2,2,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,3,    //4
                                                  3,0,3,0,0,0,0,0,0,0,0,4,0,1,0,1,0,0,4,1,1,1,0,1,2,4,3,    //6
                                                  3,3,0,0,0,0,0,0,0,2,2,0,2,0,0,1,3,3,3,0,0,2,0,1,1,0,0     //6 
                    };
                    red_r = 3;
                    red_c = 11;
                    green_r = 3;
                    green_c = 3;
                    yellow_r = 8;
                    yellow_c = 3;
                    break;
                }
            default:
                {
                    rows = 8;
                    cols = 12;
                    terrainArray=new int[96] {  0,0,0,0,0,0,0,0,0,0,0,0,
                                                0,0,0,0,0,0,0,0,0,0,0,0,
                                                0,0,0,0,0,0,0,0,0,0,0,0,
                                                0,0,0,0,0,0,0,0,0,0,0,0,
                                                0,0,0,0,0,0,0,0,0,0,0,0,
                                                0,0,0,0,0,0,0,0,0,0,0,0,
                                                0,0,0,0,0,0,0,0,0,0,0,0,
                                                0,0,0,0,0,0,0,0,0,0,0,0 };
                    break;
                }
        }



        maps = new GameObject[rows*cols];
        map = Resources.Load("Maps") as GameObject;
        number1 = Resources.Load("Num1") as GameObject;
        number2 = Resources.Load("Num2") as GameObject;
        number3 = Resources.Load("Num3") as GameObject;
        textGo = Resources.Load("Go") as GameObject;

        red_effect = Resources.Load("redEffect") as GameObject;

        if (is_align_center)
        {
            top_left_coordinate.x = -width * (cols % 2 == 0 ? (cols / 2 - 0.5f) : (cols / 2));
            top_left_coordinate.y = (rates / 2) * width * (rows % 2 == 0 ? (rows / 2 - 0.5f) : (rows / 2));
        }

        for (int j = 0; j < cols; j++)
        {
            for (int i = 0; i < rows; i++)
            {
                if (j % 2 == 0)
                {
                    maps[rows * j + i] = Instantiate(map, new Vector3(top_left_coordinate.x + j * width * 3 / 4, top_left_coordinate.y - i * width * rates / 2, 0), Quaternion.identity);
                    maps[rows * j + i].GetComponent<red_command>().red_effect = Instantiate(red_effect, new Vector3(top_left_coordinate.x + j * width * 3 / 4, top_left_coordinate.y - i * width * rates / 2, -50), Quaternion.identity);
                }
                else
                {
                    maps[rows * j + i] = Instantiate(map, new Vector3(top_left_coordinate.x + j * width * 3 / 4, top_left_coordinate.y - i * width * rates / 2 + 0.5f * rates / 2, 0), Quaternion.identity);
                    maps[rows * j + i].GetComponent<red_command>().red_effect = Instantiate(red_effect, new Vector3(top_left_coordinate.x + j * width * 3 / 4, top_left_coordinate.y - i * width * rates / 2 + 0.5f * rates / 2, -50), Quaternion.identity);
                }
                //根据terrainArray存储的信息，为格子加载地形
                switch (terrainArray[cols * i + j ])
                {
                    case 0:
                        maps[rows * j + i].GetComponent<Base_command>().terrainData = commonTerrain;
                        break;
                    case 1:
                        maps[rows * j + i].GetComponent<Base_command>().terrainData = noMansLandTerrain;
                        break;
                    case 2:
                        maps[rows * j + i].GetComponent<Base_command>().terrainData = plainTerrain;
                        break;
                    case 3:
                        maps[rows * j + i].GetComponent<Base_command>().terrainData = valleyTerrain;
                        break;
                    case 4:
                        maps[rows * j + i].GetComponent<Base_command>().terrainData = cityTerrain;
                        break;
                    default:
                        break;
                }
                maps[rows * j + i].GetComponent<Base_command>().ChangeTerrain();
                maps[rows * j + i].GetComponent<Base_command>().number = rows * j + i;
                
            }
        }


        maps[red_r+red_c*rows].transform.gameObject.GetComponent<red_command>().enabled=true;
        maps[red_r + red_c * rows].transform.gameObject.GetComponent<Base_command>().status = 2;
        maps[red_r + red_c * rows].transform.gameObject.GetComponent<red_command>().turnRedEffects();
        maps[red_r + red_c * rows].transform.gameObject.GetComponent<Base_command>().HP = -50;

        maps[green_r+green_c*rows].transform.gameObject.GetComponent<green_command>().enabled=true;
        maps[green_r + green_c * rows].transform.gameObject.GetComponent<Base_command>().status = 1;
        maps[green_r + green_c * rows].transform.gameObject.GetComponent<green_command>().turnGreenEffects();

        //根据地形计算第一块绿块的资源影响速率
        GreenNumber.numGreen += maps[green_c * rows + green_r].transform.gameObject.GetComponent<Base_command>().terrainData.incRate;
        GreenNumber.numWater += maps[green_c * rows + green_r].transform.gameObject.GetComponent<Base_command>().terrainData.incWaterRate;
        GreenNumber.numElectric += maps[green_c * rows + green_r].transform.gameObject.GetComponent<Base_command>().terrainData.incElectricRate;
        //print("Green inc Rate: " + GreenNumber.numGreen);

        maps[yellow_r + yellow_c * rows].transform.gameObject.GetComponent<yellow_command>().enabled = true;
        maps[yellow_r + yellow_c * rows].transform.gameObject.GetComponent<Base_command>().status = 4;


        //地图创建好后进行倒计时，不至于猝不及防
        num3 = GameObject.Instantiate(number3, countDownPos, Quaternion.identity);
        GameObject.Destroy(num3, 1);
        Invoke("SetNum2", 1f);
        Invoke("SetNum1", 3f);
        Invoke("SetGo", 5f);
        Invoke("MapScriptSelected", 8f);
    }


    /*IEnumerator callYieldFunc()
    {
        yield return new WaitForSeconds(10);
    }*/
    public void SetNum2()
    {
        num2 = GameObject.Instantiate(number2, countDownPos, Quaternion.identity);
        GameObject.Destroy(num2, 2);
    }
    public void SetNum1()
    {
        num1 = GameObject.Instantiate(number1, countDownPos, Quaternion.identity);
        GameObject.Destroy(num1, 2);
    }
    public void SetGo()
    {
        tgo = GameObject.Instantiate(textGo, countDownPos, Quaternion.identity);
        GameObject.Destroy(tgo, 1);
    }

    public void MapScriptSelected()
    {
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                maps[i * cols + j].transform.gameObject.GetComponent<Base_command>().enabled = true;
            }
        }
        /*
        maps[6 * 6].transform.gameObject.GetComponent<red_command>().enabled = true;
        maps[3 * 3 + 3].transform.gameObject.GetComponent<green_command>().enabled = true;
        maps[20].transform.gameObject.GetComponent<yellow_command>().enabled = true;
        */
    }
    /*public void DestoryNum1()
    {
        GameObject.Destroy(num1);
    }
    public void DestoryNum2()
    {
        GameObject.Destroy(effect, 1);
    }
    public void DestoryNum3()
    {
        GameObject.Destroy(effect, 1);
    }
    public void DestoryText()
    {
        GameObject.Destroy(effect, 1);
    }*/

    ArrayList getGrids(int rows, int cols, int n, int k)
    {
        ArrayList ans = new ArrayList();
        int x = n / rows;
        int y = n % rows;
        for (int i = x - k; i <= x + k; i++)
        {
            if (i < 0 || i >= cols)
                continue;
            int difference = (i - x) >= 0 ? (i - x) : (x - i);
            int baseN = 2 * k - difference;
            int y1 = y - (int)(difference / 2) - (difference % 2) * ((x % 2) == 0 ? 0 : 1) - k + difference;
            for (int j = y1; j <= y1 + baseN; j++)
            {
                if (j < 0 || j >= rows || (i == x && j == y))
                    continue;
                ans.Add(maps[i * rows + j]);
            }
        }
        return ans;
    }

    //对外的接口，获得该编号格子作为k个单位内的格子，不包括自身
    public ArrayList getGrids(int n, int k)
    {
        return getGrids(rows, cols, n, k);
    }

    GameObject getMap(float fx, float fy, int rows, int cols, float x, float y)
    {
        //左上角
        float lx = fx - 0.5f;
        float ly = fy + rates / 2;
        //右下角
        float rx = fx + (cols - 1) * 0.75f + 0.5f;
        float ry = fy - rates * rows / 2 + rates / 4;
        if (lx <= x && x <= rx && ly >= y && y >= ry)
        {
            if (x <= lx + 0.5f)
            {
                if (y <= ly - rates / 4)
                {
                    int cur_y = (int)((y - ry) / (rates / 2));
                    return maps[rows - cur_y - 1];
                }
            }
            else if (x >= rx - 0.5f)
            {
                Debug.Log("catch");
                if (y >= ry + ((cols - 1) % 2 == 1 ? rates / 4 : 0))
                {
                    int cur_y = (int)((y - ry - ((cols - 1) % 2 == 1 ? (rates / 4) : 0)) / (rates / 2));
                    return maps[rows * cols - cur_y - 1];
                }
            }
            else
            {
                int cur_x = (int)((x - fx) / 0.75f);
                int cur_y = (int)(rows - 1 - (int)((y - ry - ((cur_x % 2 == 1) ? (rates / 4) : 0)) / (rates / 2)));
                if (cur_x % 2 == 0)
                {
                    float d1 = getDistance(new Vector3(x, y), maps[cur_x * rows + cur_y].transform.position);
                    float d2 = getDistance(new Vector3(x, y), maps[(cur_x + 1) * rows + cur_y].transform.position);
                    if (cur_y == rows - 1)
                    {
                        if (d1 < d2)
                            return maps[cur_x * rows + cur_y];
                        else
                            return maps[(cur_x + 1) * rows + cur_y];
                    }
                    else
                    {
                        float d3 = getDistance(new Vector3(x, y), maps[(cur_x + 1) * rows + cur_y + 1].transform.position);
                        if (d1 < d2)
                            if (d1 < d3)
                                return maps[cur_x * rows + cur_y];
                            else
                                return maps[(cur_x + 1) * rows + cur_y + 1];
                        else
                            if (d2 < d3)
                            return maps[(cur_x + 1) * rows + cur_y];
                        else
                            return maps[(cur_x + 1) * rows + cur_y + 1];
                    }
                }
                else
                {
                    float d1 = getDistance(new Vector3(x, y), maps[cur_x * rows + cur_y].transform.position);
                    float d2 = getDistance(new Vector3(x, y), maps[(cur_x + 1) * rows + cur_y].transform.position);
                    if (cur_y == 0)
                    {
                        if (d1 < d2)
                            return maps[cur_x * rows + cur_y];
                        else
                            return maps[(cur_x + 1) * rows + cur_y];
                    }
                    else
                    {
                        float d3 = getDistance(new Vector3(x, y), maps[(cur_x + 1) * rows + cur_y - 1].transform.position);
                        if (d1 < d2)
                            if (d1 < d3)
                                return maps[cur_x * rows + cur_y];
                            else
                                return maps[(cur_x + 1) * rows + cur_y - 1];
                        else
                            if (d2 < d3)
                            return maps[(cur_x + 1) * rows + cur_y];
                        else
                            return maps[(cur_x + 1) * rows + cur_y - 1];
                    }
                }
            }
        }
        return null;
    }

    //获取这个坐标上的格子，如果没有就会返回null
    public GameObject getPointingMap(float x, float y)
    {
        return getMap(top_left_coordinate.x, top_left_coordinate.y, rows, cols, x, y);
    }
    float getDistance(Vector3 a, Vector3 b)
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y);
    }
}
