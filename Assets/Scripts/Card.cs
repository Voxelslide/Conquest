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


  public int GetNumber()
	{
    return number;
	}

    IEnumerator Flip()
    {
/*      if (cardObject.transform.eulerAngles.z < 89.5)
      {
        //cardObject.transform.rotation = Quaternion.Slerp(new Quaternion(Quaternion), new Quaternion(0, 0, 90), rotationSpeed * Time.deltaTime);
        // Vector3 * (rotationSpeed * Time.deltaTime));;
        cardObject.transform.Rotate(new Vector3(0, 0, 90) * (rotationSpeed * Time.deltaTime));
      }
      else if (cardObject.transform.eulerAngles.z > 89.5)
		  {
        //cardObject.transform.rotation = new Vector3(0, 0, 90);
		  }*/

     //just scale the sprite x to basically 0, change sprite, and then unscale

      faceUp = !faceUp;

      return null;
    }


  //public void OnCOllisionWithMouse --  call SendInput to GameManager with this as the parameter


}
