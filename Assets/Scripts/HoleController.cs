using UnityEngine;
using System.Collections;

public class HoleController : MonoBehaviour {

    public Color[] statesOfHole;
    public bool isEmpty;
    public bool isOK;
    public bool isBad;

    private SpriteRenderer sprite;

	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	

	void Update () {

        if (isEmpty)
        {
            sprite.color = statesOfHole[0];
        }
        if (isOK)
        {
            sprite.color = statesOfHole[1];
        }
        if (isBad)
        {
            sprite.color = statesOfHole[2];
        }
	}
}
