using UnityEngine;
using System.Collections;


public class Grid : MonoBehaviour {

    public int xSize, ySize;
    public float spacing = 6.5f;
    public GameObject pegGO;
    public GameObject peg2GO;
    

    private int[,] board = new int[,]
        {
            { 0,0,1,0,0},
            { 0,1,1,0,0},
            { 0,1,1,1,0},
            { 1,1,1,1,0},
            { 1,1,1,1,1}
        };
    void Start()
    {
        Generate();
    }





    void Generate()
    {
        for (int y = 0; y < board.GetLength(0); y++)
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                if (board[y, x] == 1)
                {
                    
                    GameObject gridPlane = (GameObject)Instantiate(peg2GO);
                    if (y % 2 == 0)
                    {
                        gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x - 3.5f, gridPlane.transform.position.y + y - 3,
                                                            gridPlane.transform.position.z) * spacing;
                   
                    }
                    else
                    {

                        gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x - 3, gridPlane.transform.position.y + y - 3,
                                                                gridPlane.transform.position.z) * spacing;
               
                    }


                }
            }
        }
    }
}


