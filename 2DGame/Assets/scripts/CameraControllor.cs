using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControllor : MonoBehaviour
{
    public float movingSpeed=2.0f;
    public float scaleSpeed=0.5f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y+Time.deltaTime*movingSpeed,
                                             transform.position.z);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y - Time.deltaTime * movingSpeed,
                                             transform.position.z);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime * movingSpeed,
                                             transform.position.y,
                                             transform.position.z);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * movingSpeed,
                                             transform.position.y,
                                             transform.position.z);
        }
        if (Input.GetAxis("Mouse ScrollWheel")<0)
        {
            Camera.main.orthographicSize += scaleSpeed;
            if (Camera.main.orthographicSize > 10)
                Camera.main.orthographicSize = 10;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize -= scaleSpeed;
            if (Camera.main.orthographicSize < 1.15f)
                Camera.main.orthographicSize = 1.15f;
        }
    }
}
