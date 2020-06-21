using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellow_command : MonoBehaviour
{
    public Color color;
    public Sprite finalSprite;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = finalSprite;
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        color = new Color(0.87450f, 1.0f, 0.0f, 1.0f);

        sprite.color = color;

        this.gameObject.GetComponent<Base_command>().status = 4;
        //this.gameObject.GetComponent<Base_command>().HP=0;
    }
}
