using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer : MonoBehaviour
{

	//viewer also needs a reference to the gamestate so it can update the UI
	//gamestate reference here

	public GameState gameState;
	public float cardMoveSpeed = 1f;
	private int sideBuffer;

	//put UI object reference here-- UI will just be a child of Viewer?


	//Positions of stuff/things?

	//viewer handles spacing out the PLayerHand deck

    // Start is called before the first frame update
    void Start()
    {

		}

    // Update is called once per frame
    void Update()
    {
        //get location of all cards and render them
    }

	//Make a Win SFX function
	public void WinSFX()
	{

	}

	//Make Loss SFX function
	public void LossWinSFX()
	{

	}

	//Make Boss Win SFX function
	public void BossWinSFX()
	{

	}

	/*Done in update:
	 * checks for player input and sends it to the gamemanager/gamestate???????
	 * checks every card and places it where it needs to be
	 * updates UI
	 */

	IEnumerator MoveC(GameObject card, Vector3 startPos, Vector3 endPos)
	{
		card.GetComponent<SpriteRenderer>().sortingOrder = 0;
		for (float i = 0; i <= 1f; i += 0.01f)
		{
			yield return new WaitForSeconds(0.0005f);
			card.transform.position = Vector3.Lerp(startPos, endPos, i);
		}
	}

	public void MoveCard(GameObject card, Deck startDeck, Deck endDeck)
	{
		Vector3 startPos = startDeck.transform.position;
		Vector3 endPos = endDeck.transform.position;
		StartCoroutine(MoveC(card, startPos, endPos));
	}
	public void MoveCard(GameObject card, Vector3 startPos, Vector3 endPos)
	{
		StartCoroutine(MoveC(card, startPos, endPos));
	}


	public void ArrangeDeck(Deck deck, bool isPlayerHand)
	{
		int cardWidth = (int) deck.cards[0].GetComponent<SpriteRenderer>().sprite.border.x/2;
		sideBuffer = 35 + cardWidth;

		Debug.Log("Arranging deck: " + deck.ToString());
		if (isPlayerHand) 
		{
			deck.Sort();
		}
		float yPos = Camera.main.WorldToScreenPoint(deck.transform.position).y;

		int cardDistance = (Camera.main.scaledPixelWidth - 2 * sideBuffer) / deck.cards.Count;

		for (int i = 0; i< deck.cards.Count; i++)
		{
			Vector3 startPos = deck.cards[i].transform.position;
			Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(sideBuffer*3 + cardWidth  +  cardDistance * i, yPos, 10));
			MoveC(deck.cards[i].gameObject, startPos, targetPos);
			deck.cards[i].GetComponent<SpriteRenderer>().sortingOrder = i;
		}
	}

	public void FinalResult()
	{

	}



}
