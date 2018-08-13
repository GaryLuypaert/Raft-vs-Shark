using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//2 touches en même temps
public class PlayerController : MonoBehaviour {

    public Vector2 targetPos;

    public float Yincrement;
    public float Xincrement;
    public float speed;
    public bool isBuilding = false;

    public Animator animator;

    private bool canMove;


    public GameObject interfaceBar;

    public GameObject holeFocused;

    public AudioSource audio;

    public AudioClip deathSound;
//    public AudioClip hammerSound;

    //public Camera cam;


    public List<int> tilesAdjacentIndexList;


    public BoardManager Bm;


 //   public GameObject[] GreenSquares;


    private void Awake()
    {
        //transform.SetParent(GameObject.Find("Board").transform);
        holeFocused = null;
        Bm = GameObject.Find("BoardManager").GetComponent<BoardManager>();
        transform.position = targetPos;
        //PositionChanged();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        //canMove = false;

        if(collision.CompareTag("Hole")) {
            Bm.GameStarted = false;
            Debug.Log("death");
            animator.SetTrigger("Dead");
            audio.PlayOneShot(deathSound,1);
            Bm.gameOver();

            interfaceBar.SetActive(false);

         //   GameObject.Find("BoardManager").GetComponent<BoardManager>().effortBar.gameObject.SetActive(false);

        }


    }

    public void GetCrossPosition(int posX, int posY)
    //public List<int> getCrossPositions(int posX, int posY)
    {

        //int numTile = getNumOfTile(posX, posY);


        /* Debug.Log("-----");
         Debug.Log("Position : " + posX + " " + posY);

         Debug.Log((posX + 1) + " " + posY);
         Debug.Log((posX - 1) + " " + posY);
         Debug.Log(posX + " " + (posY + 1));
         Debug.Log(posX + " " + (posY - 1));*/

        for (var i = 0; i < Bm.holeList.Count; i++)
        {
            Bm.holeList[i].GetComponent<holeScript>().isAdjacentToPlayer = false;
        }

        List<GameObject> crossList = new List<GameObject>();


        for (var i = 0; i < Bm.levelTiles.Count; i++)
        {

            if (posX + 1 == Bm.levelTiles[i].GetComponent<oneTile>().pos.x && posY == Bm.levelTiles[i].GetComponent<oneTile>().pos.y)
            {
                Debug.Log(i);
                crossList.Add(Bm.levelTiles[i]);
               // GreenSquares[3].SetActive(true);
            } else {
               // GreenSquares[3].SetActive(false);
            }

            if (posX - 1 == Bm.levelTiles[i].GetComponent<oneTile>().pos.x && posY == Bm.levelTiles[i].GetComponent<oneTile>().pos.y)
            {
                Debug.Log(i);
                crossList.Add(Bm.levelTiles[i]);
                //GreenSquares[2].SetActive(true);
            } else {
               // GreenSquares[2].SetActive(false);
            }

            if (posX == Bm.levelTiles[i].GetComponent<oneTile>().pos.x && posY + 1 == Bm.levelTiles[i].GetComponent<oneTile>().pos.y)
            {
                Debug.Log(i);
                crossList.Add(Bm.levelTiles[i]);
               // GreenSquares[0].SetActive(true);
            } else {
               // GreenSquares[0].SetActive(false);
            }

            if (posX == Bm.levelTiles[i].GetComponent<oneTile>().pos.x && posY - 1 == Bm.levelTiles[i].GetComponent<oneTile>().pos.y)
            {
                Debug.Log(i);
                crossList.Add(Bm.levelTiles[i]);
               // GreenSquares[1].SetActive(true);
            }
            else {
                //GreenSquares[1].SetActive(false);
            }
        }

        for (var i = 0; i < crossList.Count;i++) {


            if(crossList[i].transform.GetChild(0).tag == "Hole") {
                crossList[i].transform.GetChild(0).GetComponent<holeScript>().isAdjacentToPlayer = true;
            }

           /* if(Bm.holeList.Contains(Bm.holeList[crossList[i]])) {
                Debug.Log("voisin = trou");
            }*/
        }


       
    }



    public void PositionChanged()
    {

            GetCrossPosition((int)targetPos.x,(int)targetPos.y);


    

    }



	// Use this for initialization
	void Start () {

      
        tilesAdjacentIndexList = new List<int>();
        animator = GetComponent<Animator>();
       // PositionChanged();

	}


    public void StopBuilding()
    {
        isBuilding = false;
        CheckBuilding();
    }

    public void CheckBuilding(){
        if (isBuilding) {
            animator.SetBool("Hammer", true);
          //  isBuilding = false;
        } else { 
            animator.SetBool("Hammer", false);
        } 

    }
    // Update is called once per frame
    void Update()
    {




       // float mouseX = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)).x;
        //float mouseY = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)).y;
     /*   if ( mouseX > transform.position.x+1)
        {
            animator.SetInteger("Direction", 3);
        }
        else if (mouseX < transform.position.x - 1)
        {
            animator.SetInteger("Direction", 2);
        }
        else if (mouseY > transform.position.y)
        {
            animator.SetInteger("Direction", 1);
        }
        else
        {
            animator.SetInteger("Direction", 0);
        }*/

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        //PositionChanged();
        // if (!isBuilding)    
        //{

        //DNRY ATTENTION

        if (Bm.GameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
            {

                if (canMove)
                {

                    GetCrossPosition((int)transform.position.x, (int)transform.position.y);
                    canMove = false;
                    animator.SetInteger("Direction", 1);
                    animator.SetBool("Hammer", false);
                    targetPos = new Vector2(transform.position.x, transform.position.y + Yincrement);
                    //   PositionChanged();

                    Bm.effortBar.fillAmount = 0;
                    for (var i = 0; i < Bm.holeList.Count; i++)
                    {
                        Bm.holeList[i].GetComponent<holeScript>().CancelConstruction();
                    }
                }

            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (canMove)
                {
                    canMove = false;
                    animator.SetInteger("Direction", 0);
                    animator.SetBool("Hammer", false);
                    targetPos = new Vector2(transform.position.x, transform.position.y - Yincrement);

                    Bm.effortBar.fillAmount = 0;
                    for (var i = 0; i < Bm.holeList.Count; i++)
                    {
                        Bm.holeList[i].GetComponent<holeScript>().CancelConstruction();
                    }
                    //  PositionChanged();
                }

            }
            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (canMove)
                {
                    canMove = false;
                    animator.SetInteger("Direction", 2);
                    animator.SetBool("Hammer", false);
                    targetPos = new Vector2(transform.position.x - Xincrement, transform.position.y);
                    Bm.effortBar.fillAmount = 0;
                    for (var i = 0; i < Bm.holeList.Count; i++)
                    {
                        Bm.holeList[i].GetComponent<holeScript>().CancelConstruction();
                    }
                    //  PositionChanged();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (canMove)
                {
                    canMove = false;
                    animator.SetInteger("Direction", 3);
                    animator.SetBool("Hammer", false);
                    targetPos = new Vector2(transform.position.x + Xincrement, transform.position.y);
                    Bm.effortBar.fillAmount = 0;
                    for (var i = 0; i < Bm.holeList.Count; i++)
                    {
                        Bm.holeList[i].GetComponent<holeScript>().CancelConstruction();
                    }
                    // PositionChanged();
                }
            }
            else
            {
                canMove = true;
            }

            PositionChanged();
        }



      //  }


         

       /* if (Input.GetKey(KeyCode.Space)) {
            isBuilding = true;
            animator.SetBool("Hammer", true);
        } else {
            isBuilding = false;
            animator.SetBool("Hammer", false);
        }*/



    }

}
