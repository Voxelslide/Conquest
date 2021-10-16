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
			sideBuffer = Camera.main.pixelWidth / 54;
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

	IEnumerator MoveC(Card card, Vector3 startPos, Vector3 endPos)
	{
		card.GetComponent<SpriteRenderer>().sortingOrder = 0;
		for (float i = 0; i <= 1f; i += 0.01f)
		{
			yield return new WaitForSeconds(0.0005f);
			card.transform.position = Vector3.Lerp(startPos, endPos, i);
		}
	}

	public void MoveCard(Card card, Deck startDeck, Deck endDeck)
	{
		Vector3 startPos = startDeck.transform.position;
		Vector3 endPos = endDeck.transform.position;
		StartCoroutine(MoveC(card, startPos, endPos));
	}
	public void MoveCard(Card card, Vector3 startPos, Vector3 endPos)
	{
		StartCoroutine(MoveC(card, startPos, endPos));
	}


	public void ArrangeDeck(Deck deck, bool isPlayerHand)
	{
		int yPos;
		if (isPlayerHand) 
		{
			yPos = 0;
			deck.Sort();
		}
		else yPos = Camera.main.pixelHeight;

		int cardDistance = (Camera.main.scaledPixelWidth - sideBuffer * 2) / deck.cards.Count;


		for (int i = 0; i< deck.cards.Count; i++)
		{
			Card card = deck.cards[i];
			card.transform.position = new Vector3(sideBuffer + cardDistance * i, yPos, card.transform.position.z);
			card.GetComponent<SpriteRenderer>().sortingOrder = i;
		}

		//do math for how everything should be arranged: L/R bounds, how spread out the cards should be based on on many there are (do something with percents or something), etc)
		//foreach card in playerHand --> cars.transform.position = MoveCard(card, where the card is right now, where the card should be)
		//ALSO IMPORTANT: make each card be behind/ infront of the following card, the cards should all be ascending on top of each other and shouldn't have weird overlaps
	}

	public void HighlightCard()
	{
		//if gameState.readyToPlayCard == true, and that card is in the playerHand, and the mouse is over it(in terms of camera?), then turn its highlighted bool on and offset its y pos by ____ amount
	}

	/* Final Result
	 
	 Have special UI thing that shows
					W/L ratio -- VIEWER
					SFX -- VIEWER
					Animations? --VIEWER
					Button to go back to main menu -- VIEWER/ UI
	 
	 */

	public void FinalResult()
	{

	}



}
