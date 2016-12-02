using UnityEngine;
using System.Collections;
using System.Linq;

public class BoardManager : MonoBehaviour {
    private static BoardManager _Instance;
    public static BoardManager Instance
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

    public void CheckMoves(HoleController holeController)
    {
        if (!holeController.hasPeg)
        { return; }

        else
        {
            if (holeController.GridPosition.X == 2 && holeController.GridPosition.Y == 4)
            {
                Debug.Log("Top");

            }
            else if (holeController.GridPosition.X == 0 && holeController.GridPosition.Y == 0)
            {
                Debug.Log("Bottom Left");

                if (Grid.Instance.Holes.ElementAt(1).Value.hasPeg == true && Grid.Instance.Holes.ElementAt(5).Value.hasPeg == true)
                {
                    Debug.Log("Can jump");
                    Grid.Instance.Holes.ElementAt(2).Value.ChangeColorToGood();
                    Grid.Instance.Holes.ElementAt(9).Value.ChangeColorToGood();
                }
                else if (Grid.Instance.Holes.ElementAt(1).Value.hasPeg == true && Grid.Instance.Holes.ElementAt(5).Value.hasPeg == false)
                {
                    Debug.Log("Can jump");
                    Grid.Instance.Holes.ElementAt(2).Value.ChangeColorToGood();
                    Grid.Instance.Holes.ElementAt(5).Value.ChangecolorToBad();
                }
                else if (Grid.Instance.Holes.ElementAt(1).Value.hasPeg == false && Grid.Instance.Holes.ElementAt(5).Value.hasPeg == true)
                {
                    Debug.Log("Can jump");
                    Grid.Instance.Holes.ElementAt(9).Value.ChangeColorToGood();
                    Grid.Instance.Holes.ElementAt(1).Value.ChangecolorToBad();
                }
                else
                {
                    Debug.Log("Cannot Jump");
                    Grid.Instance.Holes.ElementAt(1).Value.ChangecolorToBad();
                    Grid.Instance.Holes.ElementAt(5).Value.ChangecolorToBad();
                }
            }
            else if (holeController.GridPosition.X == 4 && holeController.GridPosition.Y == 0)
            {
                Debug.Log("Bottom Right");
            }
        }
    }

}
