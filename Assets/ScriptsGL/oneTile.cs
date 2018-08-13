using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct myPoint
{
    public int x;
    public int y;
}

public class oneTile : MonoBehaviour {



   // public string state;

    Animator anim;


    int tileIndex = 0;

    public myPoint pos;

    public bool isWalkable = false;

    public void LaunchAnimation()
    {
        GetComponent<SpriteRenderer>().sortingOrder += 10;

        
        anim.SetBool("prevent",true);
    }


    public void StopAnimation(){
        anim.SetBool("prevent",false);
    }




	// Use this for initialization
	void Start () {
      //  state = "ground";
        anim = GetComponent<Animator>();
	}
	

    public void turnHole(){

       
    }
	// Update is called once per frame
	void Update () {
		
	}
}
