using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverMoves : MonoBehaviour
{
    //新增 -- by lee
    public static bool moveLeft = true;
    private float left;
    private float left_higher;

    public float moveDistance;
    public float speed;

    private void Start()
    {
        moveDistance = 170.0f;
        speed = 80.0f;
        Vector3 p = gameObject.GetComponent<Transform>().position;
        //新增 -- by lee
        left_higher = p.x;
        left = left_higher - moveDistance;
    }
    // Update is called once per frame
    public void Update()
    {
        Vector3 p = gameObject.GetComponent<Transform>().position;
        if (moveLeft)
        {
            if (p.x > left)
            {
                //SceneManager.LoadScene(3);
                p.x += Time.deltaTime * speed;
                //p.x = max(p.x, left);
                gameObject.GetComponent<Transform>().position = p;
            }
        }
        // else
        // {SceneManager.LoadScene(3);
        //     if (p.x < left_higher)
        //     {
        //         p.x += Time.deltaTime * speed;
        //         p.x = min(p.x, left_higher);
        //         gameObject.GetComponent<Transform>().position = p;
        //     }
        // }

        //新增 --by lee
    }
    public void up(){
         Vector3 p = gameObject.GetComponent<Transform>().position;
        if (moveLeft)
        {
            if (p.x > left)
            {
                p.x -= Time.deltaTime * speed;
                p.x = max(p.x, left);
                gameObject.GetComponent<Transform>().position = p;
            }
        }
        else
        {
            if (p.x < left_higher)
            {
                p.x += Time.deltaTime * speed;
                p.x = min(p.x, left_higher);
                gameObject.GetComponent<Transform>().position = p;
            }
        }

    }
    float max(float a, float b)
    {
        return a > b ? a : b;
    }
    float min(float a, float b)
    {
        return a < b ? a : b;
    }
    //新增 --by lee
    public void OnButton_1(){
        SceneManager.LoadScene(3);
    }
    //新增 --by lee
    public void OnButton_2(){
        SceneManager.LoadScene(3);
    }
}
