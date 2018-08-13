using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{


    //SUPPRIMER LA BAR
    //GERER LA COULEUR DES TILES


    // de la gauche à la droite et droite gauche ainsi que haut bas et bas haut
    //Rotation du Game


    private float timeBtwSpawn;
    public float startTimeBtwsSpawn;
    public float decreaseTime;
    public float minTime = 1.25f;



    public AudioSource explosionSound;

    public AudioSource audio;
    public AudioClip constructSound;

    private float timeBtwSpawnLine;
    public float startTimeBtwsSpawnLine;
    public float decreaseTimeLine;
    public float minTimeLine = 1.25f;




    private int columns = 9;  //7                                       
    private int rows = 7; //5
    public int tileSize = 32;

    public GameObject MainTile;

    private GameObject theBoard;
    private List<Vector3> gridPositions = new List<Vector3>();

    public GameObject[] tilesStateArray;



    public List<GameObject> levelTiles = new List<GameObject>();


    public List<int> indexUnavailables = new List<int>();
    public List<int> indexAvailables = new List<int>();

    public List<GameObject> holeList = new List<GameObject>();


    public Image effortBar;




    public bool GameStarted = false;




    public Text scoreText;
    public Text gameOverText;
    public Text titleText;
    public Button startButton;
    public Button retryButton;
    // public float effortScore;


    float TimeInterval;
    public int Score;
    // Use this for initialization
    void Awake()
    {
        InitialiseList();
        SetupScene();




    }

    //public void 
    public void SetupScene()
    {
        //Creates the outer walls and floor.
        BoardSetup();


    }


    private void Start()
    {
        //startGame();
    }



    public void startGame(){
        GameStarted = true;
        GameObject.Find("Beach").GetComponent<Animator>().SetTrigger("launchRaft");
        scoreText.text = "Score : " + 0 + " m ";
        startButton.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
    }

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }


    void BoardSetup()
    {

        theBoard = GameObject.Find("Board");//new GameObject("Board");



        int num = 0;

        for (int x = 0; x < columns; x++)
        {

            for (int y = 0; y < rows; y++)
            {


               
                GameObject toInstantiate = MainTile;


                GameObject instance =
                Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                instance.GetComponent<oneTile>().pos.x = x;
                instance.GetComponent<oneTile>().pos.y = y;


                if (x != 0 && x != 8 && y != 0 && y != 6)
                {
                    instance.GetComponent<oneTile>().isWalkable = true;
                    indexAvailables.Add(num);

                }
                else
                {
                    instance.GetComponent<oneTile>().isWalkable = false;
                    indexUnavailables.Add(num);
                 
                }

               
                levelTiles.Add(instance);
             




              

                //creer les tiles de base
                if (x != 0 && x != 8 && y != 0 && y != 6)
                {
                    GameObject tileFloor = Instantiate(tilesStateArray[0], new Vector3(x, y, 0), Quaternion.identity);
                    tileFloor.transform.SetParent(instance.transform);

                } else {
                    GameObject tileFloor = Instantiate(tilesStateArray[3], new Vector3(x, y, 0), Quaternion.identity);
                    tileFloor.transform.SetParent(instance.transform);

                    tileFloor.GetComponent<holeScript>().holeIndex = num;

                    holeList.Add(tileFloor);
                   // holeList.Add(tileFloor);
                }



                instance.transform.SetParent(theBoard.transform);

                num++;

            }
        }


        /* for (var i = 0; i < levelTiles.Count;i++) {
             Debug.Log(i + ": " + levelTiles[i].GetComponent<oneTile>().pos.x + " " + levelTiles[i].GetComponent<oneTile>().pos.y);

         }*/

        //  float BoardX = -columns / 2;
        // float BoardY = -rows / 2;
        // theBoard.transform.position = new Vector3(BoardX, BoardY);

        //Debug.Log(BoardX);
        //Debug.Log(BoardY);

    }


    /*void BoardSetup()
    {

        theBoard = GameObject.Find("Board");//new GameObject("Board");



        int num = 0;

        for (int x = 0; x < columns; x++)
        {

            for (int y = 0; y < rows; y++)
            {

                GameObject toInstantiate = MainTile;


                GameObject instance =
                Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                instance.GetComponent<oneTile>().pos.x = x;
                instance.GetComponent<oneTile>().pos.y = y;

                levelTiles.Add(instance);
                indexAvailables.Add(num);

                num++;

                //creer les tiles de base
                GameObject tileFloor = Instantiate(tilesStateArray[0], new Vector3(x, y, 0), Quaternion.identity);
                tileFloor.transform.SetParent(instance.transform);





                instance.transform.SetParent(theBoard.transform);



            }
        }


       

    }*/

   


    int getNumOfTile(int posX, int posY)
    {
        for (var i = 0; i < levelTiles.Count; i++)
        {
            if (posX == levelTiles[i].GetComponent<oneTile>().pos.x && posY == levelTiles[i].GetComponent<oneTile>().pos.y)
            {
                return i;
            }
        }
        return 0;
    }



  
    //A ADAPTER AVEC UN INT PLUTOT (PR AVOIR LES NUMEROS DIRECT DES POSITION

    int[] giveMeARowPosition(){
        

        myPoint[] rowPositions = new myPoint[columns];
        int[] indexObjects = new int[columns];

        int randomRow = Random.Range(0, rows);



        for (var i = 0; i < columns; i++)
        {


          
                // Debug.Log(i);
                rowPositions[i].y = randomRow;
                rowPositions[i].x = i;


                int index = getNumOfTile(rowPositions[i].x, rowPositions[i].y);



                indexObjects[i] = index;


         

        }


      //  for (var i = 0; i < indexObjects.Length;i++) {


        //}

        return indexObjects;

    }





    int[] giveMeAColumnPosition()
    {


        myPoint[] columnPositions = new myPoint[rows];
        int[] indexObjects = new int[rows];

        int randomColumn = Random.Range(0, rows);
        for (var i = 0; i < rows; i++)
        {
            // Debug.Log(i);
            columnPositions[i].y = i;
            columnPositions[i].x = randomColumn;


            int index = getNumOfTile(columnPositions[i].x, columnPositions[i].y);
            indexObjects[i] = index;

        }

        return indexObjects;

    }







   /* public void GetCrossPosition(int posX, int posY)
    //public List<int> getCrossPositions(int posX, int posY)
    {

        //int numTile = getNumOfTile(posX, posY);


        Debug.Log("-----");

        Debug.Log(posX + 1 + " " + posY);
        Debug.Log(posX - 1 + " " + posY);
        Debug.Log(posX + " " + posY + 1);
        Debug.Log(posX + " " + posY - 1);

       //List<int> indexCross = new List<int>();
        for (var i = 0; i < levelTiles.Count; i++)
        {
            if (posX+1 == levelTiles[i].GetComponent<oneTile>().pos.x && posY == levelTiles[i].GetComponent<oneTile>().pos.y)
            {
                indexCross.Add(i);
            }

            if (posX - 1 == levelTiles[i].GetComponent<oneTile>().pos.x && posY == levelTiles[i].GetComponent<oneTile>().pos.y)
            {
                indexCross.Add(i);
            }
                  
            if (posX  == levelTiles[i].GetComponent<oneTile>().pos.x && posY + 1 == levelTiles[i].GetComponent<oneTile>().pos.y)
            {
                indexCross.Add(i);
            }

            if (posX == levelTiles[i].GetComponent<oneTile>().pos.x && posY - 1 == levelTiles[i].GetComponent<oneTile>().pos.y)
            {
                indexCross.Add(i);
            }
        }

        //return indexCross;
    }*/




    public void createFloorAgain(int index)
    {


        audio.PlayOneShot(constructSound, 1);
        GameObject tileFloor = Instantiate(tilesStateArray[0], new Vector3(levelTiles[index].transform.position.x, levelTiles[index].transform.position.y, 0), Quaternion.identity);

        //int numTile = getNumOfTile(pos.x,pos.y);
        tileFloor.transform.SetParent(levelTiles[index].transform);
        levelTiles[index].GetComponent<oneTile>().isWalkable = true;
      //  tileFloor.GetComponent<oneTile>().isWalkable = true;
        indexAvailables.Add(index);

        GameObject.Find("Player").GetComponent<PlayerController>().interfaceBar.SetActive(false);



    }


    // OUVRIR UNE LIGNE
    private IEnumerator OpenAHoleAtARow(int[] rowIndexs, float time)
    {
        yield return new WaitForSeconds(time);

        foreach (int t in rowIndexs)
        {
            levelTiles[t].GetComponent<oneTile>().StopAnimation();

        }

        foreach (int t in rowIndexs)
        {

            if (indexUnavailables.Contains(t) == false && levelTiles[t].GetComponent<oneTile>().isWalkable == true)
            {
                indexAvailables.Remove(t);
                indexUnavailables.Add(t);
                Destroy(levelTiles[t].transform.GetChild(0).gameObject);

                explosionSound.Play(); //SONS

                GameObject hole = Instantiate(tilesStateArray[1], new Vector3(levelTiles[t].transform.position.x, levelTiles[t].transform.position.y, 0), Quaternion.identity);
                hole.transform.SetParent(levelTiles[t].transform);

                hole.GetComponent<holeScript>().holeIndex = t;
                holeList.Add(hole);


                yield return new WaitForSeconds(0.3f);
            }

        }

        yield return null;

    }






    //Update is called once per frame
    void Update()
    {

        if (GameStarted)
        {
            if (timeBtwSpawn <= 0)
            {

                if (indexAvailables.Count > 0)
                {
                    PutAHoleAtRandom();
                }


                timeBtwSpawn = startTimeBtwsSpawn;
                if (startTimeBtwsSpawn > minTime)
                {
                    startTimeBtwsSpawn -= decreaseTime;
                }
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }


            if (timeBtwSpawnLine <= 0)
            {

                //  if (indexAvailables.Count > 0)
                //{


                 int rand = Random.Range(0, 2);

                // rand = 2;
                //Debug.Log(rand);
                if (rand == 1)
                {
                    StartCoroutine(PutAHoleAtColumn());
                }
                else
                {
                    StartCoroutine(PutAHoleAtRow());
                }

                timeBtwSpawnLine = startTimeBtwsSpawnLine;
                if (startTimeBtwsSpawnLine > minTimeLine)
                {
                    startTimeBtwsSpawnLine -= decreaseTimeLine;
                }
            }
            else
            {
                timeBtwSpawnLine -= Time.deltaTime;
            }
        }

     


  

      /*  if (Input.GetKeyDown(KeyCode.G))
        {
            List<int> test = getCrossPositions(5,0);
            for (var i = 0; i < test.Count;i++) {
                Debug.Log(levelTiles[test[i]].transform.position);
            }
        }*/








    }


    void LateUpdate()
    {

        if (GameStarted)
        {
            // ones per in seconds
            TimeInterval += Time.deltaTime;
            if (TimeInterval >= 1)
            {
                TimeInterval = 0;
                // Performance friendly code here
                increaseScore();
            }
        }
    }



 

    public void increaseScore(){
        Score++;
        scoreText.text = "Score : " + Score + " m ";
    }



    IEnumerator PutAHoleAtColumn()
    {


        int[] columnIndexs = giveMeAColumnPosition();

       


        foreach (int t in columnIndexs)
        {
            //if (indexUnavailables.Contains(t) == false )
            //{
              //  levelTiles[t].transform.GetChild(0).GetComponent<FloorScript>().LaunchAnimation();

                levelTiles[t].GetComponent<oneTile>().LaunchAnimation();
 

                yield return new WaitForSeconds(0.1f);


          // }
        }

        StartCoroutine(OpenAHoleAtARow(columnIndexs, 3));

        yield return null;
    }





    IEnumerator PutAHoleAtRow() {
        

        int[] rowIndexs = giveMeARowPosition();

        foreach (int t in rowIndexs)
        {
            if (indexUnavailables.Contains(t) == false)
            {
                //levelTiles[t].transform.GetChild(0).GetComponent< FloorScript>().LaunchAnimation();

                levelTiles[t].GetComponent<oneTile>().LaunchAnimation();
 
                yield return new WaitForSeconds(0.1f);
            }
        }

        StartCoroutine(OpenAHoleAtARow(rowIndexs, 3));

        yield return null;
    }


    void PutAHoleAtRandom() {
        //if(gridPositions.Count > 0) {

       
            Vector3 rPosition = RandomPositionV2();
            int rPositionX = (int)rPosition.x;
            int rPositionY = (int)rPosition.y;


            
            int index = getNumOfTile(rPositionX, rPositionY);


        
            if (indexUnavailables.Contains(index) == false && levelTiles[index].GetComponent<oneTile>().isWalkable == true)
            {

                indexAvailables.Remove(index);
                indexUnavailables.Add(index);
                levelTiles[index].GetComponent<oneTile>().LaunchAnimation();
                //levelTiles[index].transform.GetChild(0).GetComponent<FloorScript>().LaunchAnimation();

                StartCoroutine(OpenAHoleAtRandom(index, 2));
             
            }
            
           
      //  } 
    }



    IEnumerator OpenAHoleAtRandom(int index, float time)
    {


        yield return new WaitForSeconds(time);
        levelTiles[index].GetComponent<oneTile>().StopAnimation();
        Destroy(levelTiles[index].transform.GetChild(0).gameObject);
        GameObject hole = Instantiate(tilesStateArray[2], new Vector3(levelTiles[index].transform.position.x, levelTiles[index].transform.position.y, 0), Quaternion.identity);
       
        explosionSound.Play(); //SONS


        hole.transform.SetParent(levelTiles[index].transform);
        hole.GetComponent<holeScript>().holeIndex = index;

        holeList.Add(hole);
    /*    indexAvailables.Remove(index);
        indexUnavailables.Add(index);*/ //DEPLACEE

        yield return null;
    }




    //OUVRIR UNE COLONNE

    private IEnumerator OpenAHoleAtAColumn(int[] columnIndexs, float time)
    {


        foreach (int t in columnIndexs)
        {
            levelTiles[t].GetComponent<oneTile>().StopAnimation();

        }


        yield return new WaitForSeconds(time);

        foreach (int t in columnIndexs)
        {

            indexAvailables.Remove(t);
            indexUnavailables.Add(t);
            Destroy(levelTiles[t].transform.GetChild(0).gameObject);


            explosionSound.Play(); //SONS


            GameObject hole = Instantiate(tilesStateArray[1], new Vector3(levelTiles[t].transform.position.x, levelTiles[t].transform.position.y, 0), Quaternion.identity);
            hole.transform.SetParent(levelTiles[t].transform);

            hole.GetComponent<holeScript>().holeIndex = t;
            holeList.Add(hole);
            // indexUnavailables.Add(t);
            yield return new WaitForSeconds(0.3f);
            //   }
        }

        yield return null;

    }



    //RandomPosition returns a random position from our list gridPositions.
    Vector3 RandomPositionV2()
    {

        //Debug.Log("av:" + indexAvailables.Count);
        //   int randomIndex = Random.Range(0, indexAvailables.Count);


        //Vector3 randomPosition = levelTiles[indexAvailables[randomIndex]].transform.localPosition;//.position

        Vector3 randomPosition = GameObject.Find("Player").transform.localPosition;
       
    
        return randomPosition;
    }


   

    //RandomPosition returns a random position from our list gridPositions.
    Vector3 RandomPosition()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count);

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridPositions[randomIndex];

        //Remove the entry at randomIndex from the list so that it can't be re-used.
       // gridPositions.RemoveAt(randomIndex);



        //Return the randomly selected Vector3 position.
        return randomPosition;
    }

   


    public void gameOver(){
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

    }

    public void reloadGame(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
