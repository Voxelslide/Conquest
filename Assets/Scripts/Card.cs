using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
  public GameObject cardObject;
  public int number;
  public bool faceUp;
  public bool highlighted = false;
  public Sprite cardFace;
  public Sprite cardBack;
  public BoxCollider2D collider = new BoxCollider2D();

  private float rotationSpeed = 2.0f;

  public Card(int num, bool fUp, GameObject gO, Sprite cardFront, Sprite cardBacking)
	{
    number = num;
    faceUp = fUp;
    cardObject = gO;
    cardFace = cardFront;
    cardBack = cardBacking;

    if (fUp) {
      gO.GetComponent<SpriteRenderer>().sprite = cardFront;
    }
    else gO.GetComponent<SpriteRenderer>().sprite = cardBack;
  }
  IEnumerator Flip()
  {
    //squish x-scale to 0
    for(float i = 1f; i> 0f; i -= 0.1f)
		{
      cardObject.transform.position.Scale(new Vector3(i, 1, 1));
      yield return new WaitForSeconds(0.05f);
		}

    //change card sprite
    if (faceUp) cardObject.GetComponent<SpriteRenderer>().sprite = cardBack;
    else cardObject.GetComponent<SpriteRenderer>().sprite = cardFace;
    faceUp = !faceUp;

    //unsquish
    for (float i = 0f; i < 1f; i += 0.1f)
    {
      cardObject.transform.position.Scale(new Vector3(i, 1, 1));
      yield return new WaitForSeconds(0.05f);
    }

    //ensure that the card's X scale is for sure ==1
    cardObject.transform.position.Scale(new Vector3(1, 1, 1));
  }




}
