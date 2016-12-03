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

	void Start () {
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

    void OnMouseDown()
    {
        //CheckForPeg();
        if (!BoardManager.Instance.isHeld)
        {
            BoardManager.Instance.isHeld = true;
            canBePutInto = true;
            pegGO.SetActive(false);
            BoardManager.Instance.holdingPegGO.SetActive(true);
            //Transform oldPos = pegGO.transform;
            //pegGO.transform.position = BoardManager.Instance.PegSpot.position;
        }

        else if (BoardManager.Instance.isHeld && canBePutInto) {
            pegGO.SetActive(true);
            canBePutInto = false;
            hasPeg = true;
            BoardManager.Instance.isHeld = false;
            BoardManager.Instance.holdingPegGO.SetActive(false);
            if (!BoardManager.Instance.isHeld)
            {
                for (int i = 0; i < Grid.Instance.Holes.Count; i++)
                {
                    Grid.Instance.Holes.ElementAt(i).Value.ChangeColorToDefault();

                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (!BoardManager.Instance.isHeld)
        {
            CheckForPeg();
            Debug.Log("X:" + GridPosition.X + " Y:" + GridPosition.Y);
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
