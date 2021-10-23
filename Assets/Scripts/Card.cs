using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
  public int number;
  public bool faceUp;
  //public bool highlighted = false;
  public Sprite cardFace;
  public Sprite cardBack;
  public GameManager gameManager;
  public Deck deck;
  

  private float rotationSpeed = 2.0f;

  public void SetUp(int num, bool fUp, Sprite cardFront, Sprite cardBacking, GameManager gM, Deck d)
	{
    number = num;
    faceUp = fUp;
    cardFace = cardFront;
    cardBack = cardBacking;
    gameManager = gM;
    deck = d;

    //determines starting orientation
    if (fUp) {
      transform.GetComponent<SpriteRenderer>().sprite = cardFront;
    }
    else transform.GetComponent<SpriteRenderer>().sprite = cardBack;

    //reset collider
    Destroy(GetComponent<PolygonCollider2D>());
    gameObject.AddComponent<PolygonCollider2D>();
  }

  public void ChangeDeck(Deck d)
	{
    deck = d;
	}


  public void Flip()
	{
    StartCoroutine(FlipCard());
	}

  IEnumerator FlipCard() //The card is flipped but the animation doesn't work
  {
    //squish x-scale to 0
    for(float i = 1f; i> 0f; i -= 0.1f)
		{
      transform.position.Scale(new Vector3(i, 1, 1));
      yield return new WaitForSeconds(0.05f);
		}

    //change card sprite
    if (faceUp) GetComponent<SpriteRenderer>().sprite = cardBack;
    else GetComponent<SpriteRenderer>().sprite = cardFace;
    faceUp = !faceUp;

    //unsquish
    for (float i = 0f; i < 1f; i += 0.1f)
    {
      transform.position.Scale(new Vector3(i, 1, 1));
      yield return new WaitForSeconds(0.05f);
    }

    //ensure that the card's X scale is for sure ==1
    transform.position.Scale(new Vector3(1, 1, 1));
  }

  public void OnMouseDown()
	{

    //only do this if the game is still in play
    if (!gameManager.gameState.gameOver)
    {
      Debug.Log("Card " + this.ToString() + " clicked.");
      gameManager.SendInput(this.gameObject, deck);
    }
	}

}
