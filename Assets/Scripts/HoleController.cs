using UnityEngine;
using System.Collections;
using System.Linq;

public class HoleController : MonoBehaviour {

    public Color[] statesOfHole;
    public bool isEmpty;
    public bool isOK;
    public bool isBad;
    public bool hasPeg;

    public Point GridPosition { get; private set; }

    private SpriteRenderer sprite;

    public GameObject pegGO;


    public bool canBePutInto;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = statesOfHole[0];
        CheckForPeg();
    }

    public void CheckForPeg()
    {
        if (hasPeg)
        {
            pegGO.SetActive(true);
        }
        else
        {
            pegGO.SetActive(false);
        }
    }

    public void Setup(Point gridPos)
    {
        this.GridPosition = gridPos;
        Grid.Instance.Holes.Add(gridPos, this);
    }


    public bool clickedHole = false;
    void OnMouseDown()
    {
        if (!BoardManager.Instance.isHeld)
        {
            clickedHole = true;
            BoardManager.Instance.isHeld = true;
            canBePutInto = true;
            pegGO.SetActive(false);
            // Comment BoardManager checks for Valid holes.
            BoardManager.Instance.holdingPegGO.SetActive(true);
        }
        else if (BoardManager.Instance.isHeld && canBePutInto && clickedHole)
        {
            BoardManager.Instance.holdingPegGO.SetActive(false);
            pegGO.SetActive(true);
            canBePutInto = false;
            clickedHole = false;
            BoardManager.Instance.isHeld = false;

        }
        else if (BoardManager.Instance.isHeld && canBePutInto && !clickedHole)
        {
            BoardManager.Instance.holdingPegGO.SetActive(false);
            pegGO.SetActive(true);
            canBePutInto = false;
            hasPeg = true;
            BoardManager.Instance.isHeld = false;
            for (int i = 0; i < Grid.Instance.Holes.Count; i++)
            {
                if (Grid.Instance.Holes.ElementAt(i).Value.clickedHole == true)
                {
                    Grid.Instance.Holes.ElementAt(i).Value.pegGO.SetActive(false);
                    Grid.Instance.Holes.ElementAt(i).Value.hasPeg = false;
                    Grid.Instance.Holes.ElementAt(i).Value.canBePutInto = false;
                }
            }
        }
    }
           
          
        
  

    private void OnMouseOver()
    {
        if (!BoardManager.Instance.isHeld)
        {
            CheckForPeg();
            //Debug.Log("X:" + GridPosition.X + " Y:" + GridPosition.Y);
            BoardManager.Instance.CheckMoves(gameObject.GetComponent<HoleController>());
        }
    }

    private void OnMouseExit()
    {
        if (!BoardManager.Instance.isHeld)
        {
            for (int i = 0; i < Grid.Instance.Holes.Count; i++)
            {
                Grid.Instance.Holes.ElementAt(i).Value.ChangeColorToDefault();
            }
        }
    }

    public void ChangeColorToDefault() {
        sprite.color = statesOfHole[0];
        canBePutInto = false;
        clickedHole = false;
        CheckForPeg();
    }
    public void ChangeColorToGood()
    {
        sprite.color = statesOfHole[1];
        canBePutInto = true;
    }
    public void ChangecolorToBad()
    {
        sprite.color = statesOfHole[2];
        canBePutInto = false;
    }


}
