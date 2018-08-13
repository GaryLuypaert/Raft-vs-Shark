using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{

    public float speed;

    public float endX;
    public float startX;


    public float posY;

    void start(){
        posY = Random.Range(0, 6);
    }
    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("BoardManager").GetComponent<BoardManager>().GameStarted) {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= endX)
            {
                posY = Random.Range(0, 6);
                Vector2 pos = new Vector2(startX,posY);
                transform.position = pos;
            }
        }
       
    }
}