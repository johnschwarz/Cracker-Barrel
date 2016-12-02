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

	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = statesOfHole[0];
	}

    public void Setup(Point gridPos)
    {
        this.GridPosition = gridPos;
        Grid.Instance.Holes.Add(gridPos, this);
        
    }

    private void OnMouseOver()
    {
        
        Debug.Log("X:" + GridPosition.X + " Y:" + GridPosition.Y);
        BoardManager.Instance.CheckMoves(gameObject.GetComponent<HoleController>());
        
    }

    public void ChangeColorToDefault() {
        sprite.color = statesOfHole[0];
    }
    public void ChangeColorToGood()
    {
        sprite.color = statesOfHole[1];
    }
    public void ChangecolorToBad()
    {
        sprite.color = statesOfHole[2];
    }

    private void OnMouseExit()
    {
       for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {
            Grid.Instance.Holes.ElementAt(i).Value.ChangeColorToDefault();
        }
     

    }

    void Update () {

        if (hasPeg)
        {
            sprite.color = statesOfHole[0];
            pegGO.SetActive(true);
        }
        else {
            pegGO.SetActive(false);
        }       
	}
}
