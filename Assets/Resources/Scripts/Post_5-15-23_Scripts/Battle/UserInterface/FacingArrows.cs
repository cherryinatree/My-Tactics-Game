using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingArrows : MonoBehaviour
{

    public enum FacingDirection { Left, Right, Up, Down }
    public FacingDirection Direction;
    public GameObject[] arrows;

    private Material startingMaterial;
    private Material pointingMaterial;

    CombatCharacter character;

    private void Start()
    {
        startingMaterial = arrows[1].GetComponent<Renderer>().material;
        pointingMaterial = arrows[0].GetComponent<Renderer>().material;
        ArrowsActive();
        character = transform.parent.GetComponent<CombatCharacter>();
    }






    public void SetDirectionArrow(FacingDirection facing)
    {
        /*
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].GetComponent<Renderer>().material = startingMaterial;
        }
        */
        switch (facing)
        {
            case FacingDirection.Left:
                character.FaceDirection(270);
                //arrows[0].GetComponent<Renderer>().material = pointingMaterial;
                break;
            case FacingDirection.Right:
                character.FaceDirection(90);
                //arrows[1].GetComponent<Renderer>().material = pointingMaterial;
                break;
            case FacingDirection.Down:
                character.FaceDirection(180);
                //arrows[2].GetComponent<Renderer>().material = pointingMaterial;
                break;
            case FacingDirection.Up:
                character.FaceDirection(0);
                //arrows[3].GetComponent<Renderer>().material = pointingMaterial;
                break;
        }
    }

    public void ArrowsActive()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].SetActive(!arrows[i].activeSelf);
        }
    }
}
