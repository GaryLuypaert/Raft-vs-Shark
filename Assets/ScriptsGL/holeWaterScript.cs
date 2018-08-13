using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeWaterScript : MonoBehaviour {


    public bool isNextToPlayer = false;


    public float percentBuilding = 0;

    public int holeIndex = 0;

    private Animator anim;


    public bool isReperable = false;

    public bool isAdjacentToPlayer = false;

/*   private void Awake()
    {
        
    }*/

    //myPoint myPosition;
    // Use this for initialization
    void Start () {
     
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ActivateCollision() {
        
        GetComponent<BoxCollider2D>().enabled = true;
        isReperable = true;

    }



    void OnMouseDown()
    {


        if (Input.GetKey("mouse 0"))
        {


       

            //Reference au tableau ds trous
            if (GameObject.Find("BoardManager").GetComponent<BoardManager>().holeList.Count > 0)
            {
                foreach (var hole in GameObject.Find("BoardManager").GetComponent<BoardManager>().holeList)
                {
                    if (hole.GetComponent<holeScript>().holeIndex != holeIndex)
                    {
                        hole.GetComponent<holeScript>().percentBuilding = 0;
                    }
                }
            }


            if (isReperable & isAdjacentToPlayer)
            {

               
                anim.SetBool("isBuilding", true);

                GameObject.Find("Player").GetComponent<PlayerController>().holeFocused = gameObject;

                if (gameObject == GameObject.Find("Player").GetComponent<PlayerController>().holeFocused)
                {

                    if (transform.position.x > GameObject.Find("Player").transform.position.x)
                    {
                        GameObject.Find("Player").GetComponent<Animator>().SetInteger("Direction", 3);
                    }
                    else if (transform.position.x < GameObject.Find("Player").transform.position.x)
                    {
                        GameObject.Find("Player").GetComponent<Animator>().SetInteger("Direction", 2);
                    }
                    else if (transform.position.y > GameObject.Find("Player").transform.position.y)
                    {
                        GameObject.Find("Player").GetComponent<Animator>().SetInteger("Direction", 1);
                    }
                    else if (transform.position.y < GameObject.Find("Player").transform.position.y)
                    {
                        GameObject.Find("Player").GetComponent<Animator>().SetInteger("Direction", 0);
                    }


                      GameObject.Find("Player").GetComponent<PlayerController>().isBuilding = true;
                    GameObject.Find("Player").GetComponent<PlayerController>().CheckBuilding();
                    print("Box Clicked!");
                    percentBuilding += 0.25f;
                    //  Debug.Log(percentBuilding);

                    GameObject.Find("BoardManager").GetComponent<BoardManager>().effortBar.fillAmount = percentBuilding;
                }
            }

            if (percentBuilding == 1)
            {
            //    Debug.Log("100%");
                GameObject.Find("BoardManager").GetComponent<BoardManager>().createFloorAgain(holeIndex);

                GameObject.Find("BoardManager").GetComponent<BoardManager>().indexUnavailables.Remove(holeIndex);
                GameObject.Find("BoardManager").GetComponent<BoardManager>().holeList.Remove(gameObject);
              //  Debug.Log("Array:" + GameObject.Find("BoardManager").GetComponent<BoardManager>().holeList.Count);
                Destroy(gameObject);



                percentBuilding = 0;
                GameObject.Find("BoardManager").GetComponent<BoardManager>().effortBar.fillAmount = percentBuilding;
            }

        }

    }

    public void CancelConstruction(){
       // GameObject.Find("Player").GetComponent<PlayerController>().isBuilding = false;
       // GameObject.Find("Player").GetComponent<PlayerController>().CheckBuilding();

        if (GameObject.Find("BoardManager").GetComponent<BoardManager>().holeList.Count > 0)
        {
            foreach (var hole in GameObject.Find("BoardManager").GetComponent<BoardManager>().holeList)
            {
                if (hole.GetComponent<holeScript>().holeIndex != holeIndex)
                {
                    hole.GetComponent<holeScript>().percentBuilding = 0;
                    hole.GetComponent<Animator>().SetBool("isBuilding", false);
                }


            }
        }

       
    }

    private void OnMouseUp()
    {
        // anim.SetBool("isBuilding", false);

        //
        CancelConstruction();

    }


}
