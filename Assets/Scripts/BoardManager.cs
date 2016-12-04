﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class BoardManager : MonoBehaviour
{
    public Transform PegSpot;
    public bool isHeld = false;
    public GameObject holdingPegGO;

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

    public bool easyMode = true;
    public bool mediumMode = false;
    public bool hardMode = false;

    public IEnumerator IEasyMode()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {

            Grid.Instance.Holes.ElementAt(0).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(1).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(2).Value.hasPeg = false;
            Grid.Instance.Holes.ElementAt(3).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(4).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(5).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(6).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(7).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(8).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(9).Value.hasPeg = false;
            Grid.Instance.Holes.ElementAt(10).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(11).Value.hasPeg = false;
            Grid.Instance.Holes.ElementAt(12).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(13).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(14).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(i).Value.CheckForPeg();
        }
    }

    public IEnumerator IMediumMode()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {
            Grid.Instance.Holes.ElementAt(0).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(1).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(2).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(3).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(4).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(5).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(6).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(7).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(8).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(9).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(10).Value.hasPeg = false;
            Grid.Instance.Holes.ElementAt(11).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(12).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(13).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(14).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(i).Value.CheckForPeg();
        }
    }

    public IEnumerator IHardMode()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {
            Grid.Instance.Holes.ElementAt(0).Value.hasPeg = false;
            Grid.Instance.Holes.ElementAt(1).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(2).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(3).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(4).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(5).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(6).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(7).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(8).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(9).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(10).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(11).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(12).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(13).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(14).Value.hasPeg = true;
            Grid.Instance.Holes.ElementAt(i).Value.CheckForPeg();

           
        }
    }

    void Start()
    {
        holdingPegGO.SetActive(false);
    }


    public bool check1;
    public bool check2;

    public void CheckTwoSurroundingHolesForValidity(int neighbor1, int neighbor2, int spot1, int spot2)
    {
        if (Grid.Instance.Holes.ElementAt(neighbor1).Value.hasPeg == true && Grid.Instance.Holes.ElementAt(neighbor2).Value.hasPeg == true)
        {
            Grid.Instance.Holes.ElementAt(spot1).Value.ChangeColorToGood();
            Grid.Instance.Holes.ElementAt(neighbor1).Value.firstChoice = true;
            Grid.Instance.Holes.ElementAt(spot1).Value.firstSpot = true;

            Grid.Instance.Holes.ElementAt(spot2).Value.ChangeColorToGood();
            Grid.Instance.Holes.ElementAt(neighbor2).Value.secondChoice = true;
            Grid.Instance.Holes.ElementAt(spot2).Value.secondSpot = true;

            Grid.Instance.Holes.ElementAt(neighbor1).Value.shouldBeRemovedBecauseOfJump = true;
            Grid.Instance.Holes.ElementAt(neighbor2).Value.shouldBeRemovedBecauseOfJump = true;
        }
        else if (Grid.Instance.Holes.ElementAt(neighbor1).Value.hasPeg == true && Grid.Instance.Holes.ElementAt(neighbor2).Value.hasPeg == false)
        {
            Grid.Instance.Holes.ElementAt(spot1).Value.ChangeColorToGood();
            Grid.Instance.Holes.ElementAt(neighbor1).Value.firstChoice = true;
            Grid.Instance.Holes.ElementAt(spot1).Value.firstSpot = true;
            Grid.Instance.Holes.ElementAt(neighbor1).Value.shouldBeRemovedBecauseOfJump = true;
            Grid.Instance.Holes.ElementAt(neighbor2).Value.ChangecolorToBad();
        }
        else if (Grid.Instance.Holes.ElementAt(neighbor1).Value.hasPeg == false && Grid.Instance.Holes.ElementAt(neighbor2).Value.hasPeg == true)
        {
            Grid.Instance.Holes.ElementAt(neighbor1).Value.ChangecolorToBad();
            Grid.Instance.Holes.ElementAt(spot2).Value.secondSpot = true;
            Grid.Instance.Holes.ElementAt(neighbor2).Value.secondChoice = true;
            Grid.Instance.Holes.ElementAt(spot2).Value.ChangeColorToGood();
            Grid.Instance.Holes.ElementAt(neighbor2).Value.shouldBeRemovedBecauseOfJump = true;
        }
        else
        {
            Grid.Instance.Holes.ElementAt(neighbor1).Value.ChangecolorToBad();
            Grid.Instance.Holes.ElementAt(neighbor2).Value.ChangecolorToBad();
            
        }

    }

    public void CheckFourSurroundingHolesForValidity(int neighbor1, int neighbor2, int neighbor3, int neighbor4, int spot1, int spot2, int spot3, int spot4)
    {
        if (Grid.Instance.Holes.ElementAt(neighbor1).Value.hasPeg == false)
        { Grid.Instance.Holes.ElementAt(neighbor1).Value.ChangecolorToBad(); }
        if (Grid.Instance.Holes.ElementAt(neighbor2).Value.hasPeg == false)
        { Grid.Instance.Holes.ElementAt(neighbor2).Value.ChangecolorToBad(); }
        if (Grid.Instance.Holes.ElementAt(neighbor3).Value.hasPeg == false)
        { Grid.Instance.Holes.ElementAt(neighbor3).Value.ChangecolorToBad(); }
        if (Grid.Instance.Holes.ElementAt(neighbor4).Value.hasPeg == false)
        { Grid.Instance.Holes.ElementAt(neighbor4).Value.ChangecolorToBad(); }

        if (Grid.Instance.Holes.ElementAt(neighbor1).Value.hasPeg == true)
        {
            Grid.Instance.Holes.ElementAt(spot1).Value.ChangeColorToGood();
            Grid.Instance.Holes.ElementAt(neighbor1).Value.shouldBeRemovedBecauseOfJump = true;

            Grid.Instance.Holes.ElementAt(neighbor1).Value.firstChoice = true;
            Grid.Instance.Holes.ElementAt(spot1).Value.firstSpot = true;
        }

        if (Grid.Instance.Holes.ElementAt(neighbor2).Value.hasPeg == true)
        {
            Grid.Instance.Holes.ElementAt(spot2).Value.ChangeColorToGood();
            Grid.Instance.Holes.ElementAt(neighbor2).Value.shouldBeRemovedBecauseOfJump = true;

            Grid.Instance.Holes.ElementAt(neighbor2).Value.secondChoice = true;
            Grid.Instance.Holes.ElementAt(spot2).Value.secondSpot = true;
        }

        if (Grid.Instance.Holes.ElementAt(neighbor3).Value.hasPeg == true)
        {
            Grid.Instance.Holes.ElementAt(spot3).Value.ChangeColorToGood();
            Grid.Instance.Holes.ElementAt(neighbor3).Value.shouldBeRemovedBecauseOfJump = true;

            Grid.Instance.Holes.ElementAt(neighbor3).Value.thirdChoice = true;
            Grid.Instance.Holes.ElementAt(spot3).Value.thirdSpot = true;
        }

        if (Grid.Instance.Holes.ElementAt(neighbor4).Value.hasPeg == true)
        {
            Grid.Instance.Holes.ElementAt(spot4).Value.ChangeColorToGood();
            Grid.Instance.Holes.ElementAt(neighbor4).Value.shouldBeRemovedBecauseOfJump = true;

            Grid.Instance.Holes.ElementAt(neighbor4).Value.fourthChoice = true;
            Grid.Instance.Holes.ElementAt(spot4).Value.fourthSpot = true;
        }
    }
    public int amountOfHolesChecked;
    public int amountOfHolesBad;

    public IEnumerator ICheckForLoss()
    {
        amountOfHolesChecked = 0;
        amountOfHolesBad = 0;
        for (int i = 0; i < Grid.Instance.Holes.Count; i++)
        {
            CheckMoves(Grid.Instance.Holes.Values.ElementAt(i).GetComponent<HoleController>());
        }

        if (amountOfHolesBad == amountOfHolesChecked)
        {
            StartCoroutine(TimeAndMenuManager.Instance.ILose());
        }
        else
        {
            for (int i = 0; i < Grid.Instance.Holes.Count; i++)
            {
                Grid.Instance.Holes.ElementAt(i).Value.ChangeColorToDefault();
            }
        }
        yield return null;
    }

    public void CheckMoves(HoleController holeController)
    {
        if (!holeController.hasPeg)
        { return; }
        else
        {
            if (holeController.GridPosition.X == 0 && holeController.GridPosition.Y == 0)
            {
                CheckTwoSurroundingHolesForValidity(1, 5, 2, 9);
            }
            else if (holeController.GridPosition.X == 1 && holeController.GridPosition.Y == 0)
            {
                CheckTwoSurroundingHolesForValidity(2, 6, 3, 10);
            }
            else if (holeController.GridPosition.X == 2 && holeController.GridPosition.Y == 0)
            {
                CheckFourSurroundingHolesForValidity(1, 3, 6, 7, 0, 4, 9, 11);
            }
            else if (holeController.GridPosition.X == 3 && holeController.GridPosition.Y == 0)
            {
                CheckTwoSurroundingHolesForValidity(2, 7, 1, 10);
            }
            else if (holeController.GridPosition.X == 4 && holeController.GridPosition.Y == 0)
            {
                CheckTwoSurroundingHolesForValidity(3, 8, 2, 11);
            }
            
            else if (holeController.GridPosition.X == 0 && holeController.GridPosition.Y == 1)
            {
                CheckTwoSurroundingHolesForValidity(6, 9, 7, 12);
            }
            else if (holeController.GridPosition.X == 1 && holeController.GridPosition.Y == 1)
            {
                CheckTwoSurroundingHolesForValidity(7, 10, 8, 13);
            }
            else if (holeController.GridPosition.X == 2 && holeController.GridPosition.Y == 1)
            {
                CheckTwoSurroundingHolesForValidity(6, 10, 5, 12);
            }
            else if (holeController.GridPosition.X == 3 && holeController.GridPosition.Y == 1)
            {
                CheckTwoSurroundingHolesForValidity(7, 11, 6, 13);
            }
            
            else if (holeController.GridPosition.X == 1 && holeController.GridPosition.Y == 2)
            {
                CheckFourSurroundingHolesForValidity(5, 6, 10, 12, 0, 2, 11, 14);
            }
            else if (holeController.GridPosition.X == 2 && holeController.GridPosition.Y == 2)
            {
                CheckTwoSurroundingHolesForValidity(6, 7, 1, 3);
            }
            else if (holeController.GridPosition.X == 3 && holeController.GridPosition.Y == 2)
            {
                CheckFourSurroundingHolesForValidity(7, 8, 10, 13, 2, 4, 9, 14);
            }
           
            else if (holeController.GridPosition.X == 1 && holeController.GridPosition.Y == 3)
            {
                CheckTwoSurroundingHolesForValidity(9, 10, 5, 7);
            }
            else if (holeController.GridPosition.X == 2 && holeController.GridPosition.Y == 3)
            {
                CheckTwoSurroundingHolesForValidity(10, 11, 6, 8);
            }

            else if (holeController.GridPosition.X == 2 && holeController.GridPosition.Y == 4)
            {
                CheckTwoSurroundingHolesForValidity(12, 13, 9, 11);
            }
        }
    }
}
