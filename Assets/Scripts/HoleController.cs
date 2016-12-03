using UnityEngine;
using System.Collections;
using System.Linq;

public class HoleController : MonoBehaviour
{
    public Color[] statesOfHole;
    public bool hasPeg;
    public Point GridPosition { get; private set; }
    private SpriteRenderer sprite;
    public GameObject pegGO;
    public bool canBePutInto;
    public bool startingHole = false;
    public bool shouldBeRemovedBecauseOfJump = false;

    public bool firstChoice;
    public bool secondChoice;
    public bool thirdChoice;
    public bool fourthChoice;

    public bool firstSpot;
    public bool secondSpot;
    public bool thirdSpot;
    public bool fourthSpot;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = statesOfHole[0];
    }

    // Used in BoardManager when setting up the board
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

    IEnumerator IMovePeg(Transform moveMe, Vector3 startPos, Vector3 endPos, float time)
    {
        
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            moveMe.position = Vector3.Lerp(startPos, endPos, i);
            
            yield return new WaitForSeconds (0.0f);
            
        }
        if (!BoardManager.Instance.isHeld)
        {
            BoardManager.Instance.holdingPegGO.SetActive(false);
        }
    }

    
    void OnMouseDown()
    {
        // BoardManager checks for Valid holes in OnMouseOver
        if (!BoardManager.Instance.isHeld)
        {
            // Checks to see if it's the hole you clicked; used later to deactivate the pegGO on the hole.
            startingHole = true;
            // Used to keep holes highlighted and is used in the IMovePeg
            BoardManager.Instance.isHeld = true;
            canBePutInto = true;
            pegGO.SetActive(false);
            sprite.color = statesOfHole[3];
            BoardManager.Instance.holdingPegGO.SetActive(true);
            StartCoroutine(IMovePeg(BoardManager.Instance.holdingPegGO.transform, gameObject.transform.position, BoardManager.Instance.PegSpot.position, 0.2f));
            
        }
        else if (BoardManager.Instance.isHeld && canBePutInto && startingHole)
        {
            
            pegGO.SetActive(true);
            canBePutInto = false;
            startingHole = false;
            BoardManager.Instance.isHeld = false;
            ReturnPegsToDefault();
            StartCoroutine(IMovePeg(BoardManager.Instance.holdingPegGO.transform, BoardManager.Instance.PegSpot.position, gameObject.transform.position,  0.2f));
            
        }
        else if (BoardManager.Instance.isHeld && canBePutInto && !startingHole)
        {
            CheckPegsForJump();
            StartCoroutine(IMovePeg(BoardManager.Instance.holdingPegGO.transform, BoardManager.Instance.PegSpot.position, gameObject.transform.position,  0.2f));
            ReturnPegsToDefault();
            
        }
    }

    private void CheckPegsForJump()
    {
        
        pegGO.SetActive(true);
        canBePutInto = false;
        hasPeg = true;
        BoardManager.Instance.isHeld = false;

        for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {
            if (Grid.Instance.Holes.ElementAt(i).Value.startingHole == true)
            {
                Grid.Instance.Holes.ElementAt(i).Value.pegGO.SetActive(false);
                Grid.Instance.Holes.ElementAt(i).Value.hasPeg = false;
                Grid.Instance.Holes.ElementAt(i).Value.canBePutInto = false;
            }
        }
        if (firstSpot)
        {
            for (int i = 0; i < Grid.Instance.Holes.Count; i++)
            {
                if (Grid.Instance.Holes.ElementAt(i).Value.firstChoice == true)
                {
                    Grid.Instance.Holes.ElementAt(i).Value.pegGO.SetActive(false);
                    Grid.Instance.Holes.ElementAt(i).Value.hasPeg = false;
                    Grid.Instance.Holes.ElementAt(i).Value.canBePutInto = false;
                }
            }
        }
        if (secondSpot)
        {
            for (int i = 0; i < Grid.Instance.Holes.Count; i++)
            {
                if (Grid.Instance.Holes.ElementAt(i).Value.secondChoice == true)
                {
                    Grid.Instance.Holes.ElementAt(i).Value.pegGO.SetActive(false);
                    Grid.Instance.Holes.ElementAt(i).Value.hasPeg = false;
                    Grid.Instance.Holes.ElementAt(i).Value.canBePutInto = false;
                }
            }
        }
        if (thirdSpot)
        {
            for (int i = 0; i < Grid.Instance.Holes.Count; i++)
            {
                if (Grid.Instance.Holes.ElementAt(i).Value.thirdChoice == true)
                {
                    Grid.Instance.Holes.ElementAt(i).Value.pegGO.SetActive(false);
                    Grid.Instance.Holes.ElementAt(i).Value.hasPeg = false;
                    Grid.Instance.Holes.ElementAt(i).Value.canBePutInto = false;
                }
            }
        }
        if (fourthSpot)
        {
            for (int i = 0; i < Grid.Instance.Holes.Count; i++)
            {
                if (Grid.Instance.Holes.ElementAt(i).Value.fourthChoice == true)
                {
                    Grid.Instance.Holes.ElementAt(i).Value.pegGO.SetActive(false);
                    Grid.Instance.Holes.ElementAt(i).Value.hasPeg = false;
                    Grid.Instance.Holes.ElementAt(i).Value.canBePutInto = false;
                }
            }
        }
    }

    public void ReturnPegsToDefault()
    {
        for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {
            Grid.Instance.Holes.ElementAt(i).Value.ChangeColorToDefault();
            Grid.Instance.Holes.ElementAt(i).Value.shouldBeRemovedBecauseOfJump = false;
            Grid.Instance.Holes.ElementAt(i).Value.firstChoice = false;
            Grid.Instance.Holes.ElementAt(i).Value.secondChoice = false;
            Grid.Instance.Holes.ElementAt(i).Value.thirdChoice = false;
            Grid.Instance.Holes.ElementAt(i).Value.fourthChoice = false;
            Grid.Instance.Holes.ElementAt(i).Value.firstSpot = false;
            Grid.Instance.Holes.ElementAt(i).Value.secondSpot = false;
            Grid.Instance.Holes.ElementAt(i).Value.thirdSpot = false;
            Grid.Instance.Holes.ElementAt(i).Value.fourthSpot = false;
            Grid.Instance.Holes.ElementAt(i).Value.canBePutInto = false;
            Grid.Instance.Holes.ElementAt(i).Value.startingHole = false;
        }
    }

    private void OnMouseOver()
    {
        if (!BoardManager.Instance.isHeld)
        {
            BoardManager.Instance.CheckMoves(gameObject.GetComponent<HoleController>());
        }
    }

    private void OnMouseExit()
    {
        if (!BoardManager.Instance.isHeld)
        {
            ReturnPegsToDefault();
        }
    }

    public void ChangeColorToDefault()
    {
        sprite.color = statesOfHole[0];
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
