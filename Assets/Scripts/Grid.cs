using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Grid : MonoBehaviour
{

    public int xSize, ySize;
    public float spacing = 6.5f;
    public GameObject pegGO;

    public Dictionary<Point, HoleController> Holes { get; set; }

    private static Grid _Instance;
    public static Grid Instance
    {
        get { return _Instance; }
    }
    void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    private int[,] board = new int[,]
        {
            { 1,1,1,1,1},
            { 1,1,1,1,0},
            { 0,1,1,1,0},
            { 0,1,1,0,0},
            { 0,0,1,0,0}
        };
    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        Holes = new Dictionary<Point, HoleController>();

        for (int y = 0; y < board.GetLength(0); y++)
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                if (board[y, x] == 1)
                {
                    GameObject triangleGO = (GameObject)Instantiate(pegGO);
                    if (y % 2 == 0)
                    {
                        HoleController newHole = triangleGO.GetComponent<HoleController>();
                        newHole.Setup(new Point(x, y));
                        triangleGO.transform.position = new Vector3(triangleGO.transform.position.x + x - 3.5f, triangleGO.transform.position.y + y - 3, triangleGO.transform.position.z) * spacing;
                        triangleGO.transform.parent = gameObject.transform;
                    }
                    else
                    {
                        HoleController newHole = triangleGO.GetComponent<HoleController>();
                        newHole.Setup(new Point(x, y));
                        triangleGO.transform.position = new Vector3(triangleGO.transform.position.x + x - 3, triangleGO.transform.position.y + y - 3, triangleGO.transform.position.z) * spacing;
                        triangleGO.transform.parent = gameObject.transform;
                    }
                }
            }
        }
    }
}


