using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_command : MonoBehaviour
{
    public Color color;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        sprite.color = color;

        this.gameObject.GetComponent<Base_command>().status=2;
        this.gameObject.GetComponent<Base_command>().HP=0;
    }
}
