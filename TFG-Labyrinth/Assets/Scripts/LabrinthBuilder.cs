using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LabrinthBuilder : MonoBehaviour
{
    public GameObject wall;
    public GameObject floor;
    public GameObject UI_wall;
    //public GameObject pilar;
    public GameObject enemySpawner;
    public GameObject coin;
    public GameObject door;
    public GameObject key;


    public Transform player;
    public Transform miniMapCamera;

    public int width = 4;
    public int height = 4;

    public int centerSize = 4;

    public int enemiesNumber = 4;
    public int coinsNumber = 10;
    public int keysNumber = 3;

    private float uom; //unit of mesure. Depends on size of wall
    private LabrinthEngine labrinthEngine;

    //other adjustments
    private float doorOffset = 4f;

    void buildOuterWalls()
    {
        for (int i = 0; i < width; i++){
            Instantiate(wall, new Vector3(0, uom / 2, i*uom+uom/2),Quaternion.Euler(0,0,0), transform);
            Instantiate(UI_wall, new Vector3(0, uom / 2, i * uom + uom / 2), Quaternion.Euler(0, 0, 0), transform);
        }
        for (int i = 0; i < width; i++)
        {
            Instantiate(wall, new Vector3(uom*height, uom / 2, i * uom + uom / 2), Quaternion.Euler(0, 0, 0), transform);
            Instantiate(UI_wall, new Vector3(uom * height, uom / 2, i * uom + uom / 2), Quaternion.Euler(0, 0, 0), transform);
        }
        for (int i = 0; i < height; i++)
        {
            Instantiate(wall, new Vector3(i * uom + uom / 2, uom / 2, 0), Quaternion.Euler(0, 90, 0), transform);
            Instantiate(UI_wall, new Vector3(i * uom + uom / 2, uom / 2, 0), Quaternion.Euler(0, 90, 0), transform);
        }
        for (int i = 0; i < height; i++)
        {
            Instantiate(wall, new Vector3(i * uom + uom / 2, uom / 2, uom * width), Quaternion.Euler(0, 90, 0), transform);
            Instantiate(UI_wall, new Vector3(i * uom + uom / 2, uom / 2, uom * width), Quaternion.Euler(0, 90, 0), transform);
        }
    }

    private bool isCenter(LabrinthEngine.vertex v) // used for leaveing space on center of the maze
    {
        if(v.A.X > (width / 2) - centerSize && v.A.X < (width / 2) + centerSize)
        {
            if (v.A.Y > (height / 2) - centerSize && v.A.Y < (height / 2) + centerSize)
            {
                return true;
            }
        }

        return false;
    }

    void buildLabr()
    {

        LabrinthEngine.vertex[] labVertices = labrinthEngine.getVertices();
        Vector3 offset = new Vector3(1, 0, 1); //offset the labirinth walls but not the height. 

        foreach(LabrinthEngine.vertex v in labVertices)
        {
            if (!v.conected && !isCenter(v)) //if not connected and not in center, place a wall in between
            {

                Vector3 aux = new Vector3((v.A.X + v.B.X) / 2f, 0.5f, (v.A.Y + v.B.Y) / 2f) * uom;
                aux = aux + offset * (uom / 2f);

                if (v.Vertical)
                {
                    Instantiate(wall, aux, Quaternion.Euler(0, 90, 0), transform);
                    Instantiate(UI_wall, aux, Quaternion.Euler(0, 90, 0), transform);
                }
                else
                {
                    Instantiate(wall, aux, Quaternion.Euler(0, 0, 0), transform);
                    Instantiate(UI_wall, aux, Quaternion.Euler(0, 0, 0), transform);
                }
                
            }
        }
        
            
    }

    void placeFloor()
    {

        GameObject go = Instantiate(floor, new Vector3(width / 2f * uom, 0, height / 2f * uom), Quaternion.identity);
        go.transform.localScale = new Vector3(width*1.5f, 1, height*1.5f); // scales each wall size by 1.5 
        Material m = go.GetComponent<Renderer>().sharedMaterial;
        m.mainTextureScale = new Vector2(width, height);
    }

    void placePlayer() //place player at center of maze
    {
        player.position = new Vector3(width / 2f * uom,2f, height / 2f * uom);
    }
    void placeCameraMinimap () //place player at center of maze
    {
        miniMapCamera.position = new Vector3(width / 2f * uom, 50, height / 2f * uom);
        miniMapCamera.GetComponent<Camera>().orthographicSize = uom * uom / 2f + uom;
    }
    /*
    void placePilar()
    {
        for (int i=0; i<height;i++)
        {
            for (int j = 0; i<width;j++)
            {
                Instantiate(pilar, new Vector3(j*uom,uom/2f,i*uom), Quaternion.Euler(0, 0, 0), transform);
            }
        }
    }
    */

    void placeEnemies()
    {
        for(int i = 0; i < enemiesNumber; i++)
        {
            Instantiate(enemySpawner, new Vector3(Random.Range(0,height)*uom+(uom/2),2, Random.Range(0, width) * uom + (uom / 2)), transform.rotation);
        }
    }
    void placeCoins()
    {
        for (int i = 0; i < coinsNumber; i++)
        {
            Instantiate(coin, new Vector3(Random.Range(0, height) * uom + (uom / 2), 2, Random.Range(0, width) * uom + (uom / 2)), transform.rotation);
        }
    }
    void placeKeys()
    {
        for (int i = 0; i < keysNumber; i++)
        {
            Instantiate(key, new Vector3(Random.Range(0, height) * uom + (uom / 2), 2, Random.Range(0, width) * uom + (uom / 2)), transform.rotation);
        }
    }

    void placeDoor() //place player at center of maze
    {
        Vector3 playerPos = player.position;
        
        Instantiate(door, new Vector3(playerPos.x, 0, playerPos.z + doorOffset), transform.rotation);
    }



    // Start is called before the first frame update
    void Start()
    {

        uom = wall.transform.localScale.z;
        labrinthEngine = new LabrinthEngine(width, height, 0, 50);


        buildOuterWalls();
        buildLabr();
        placeFloor();
        placePlayer();

        placeCameraMinimap();
        //placePilar();

        placeDoor();
        placeEnemies();
        placeCoins();
        placeKeys();


    }
}
