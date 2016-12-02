using UnityEngine;
using System.Collections;

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
	}

    public void Setup(Point gridPos)
    {
        this.GridPosition = gridPos;
    }

    private void OnMouseOver()
    {
        //show possible moves
        Debug.Log("X:" + GridPosition.X + " Y:" + GridPosition.Y);
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
