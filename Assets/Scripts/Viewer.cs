using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer : MonoBehaviour
{

	//viewer also needs a reference to the gamestate so it can update the UI
	//gamestate reference here

	public GameState gameState;
	public float cardMoveSpeed = 10f;
	//put UI object reference here-- UI will just be a child of Viewer?


	//Positions of stuff/things?

	//viewer handles spaccing out the PLayerHand deck

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

	public void MoveCard(Card card, Deck startDeck, Deck endDeck)
	{
		//move a card from one deck to another
		Vector3 startPos = startDeck.transform.position;
		Vector3 endPos = endDeck.transform.position;
		card.cardObject.transform.position = Vector3.MoveTowards(startPos, endPos, cardMoveSpeed * Time.deltaTime);
	}

	public void MoveCard(Card card, Vector3 startPos, Vector3 endPos)
	{
		card.cardObject.transform.position = Vector3.MoveTowards(startPos, endPos, cardMoveSpeed * Time.deltaTime);
	}

	public void ArrangePlayerHand()
	{


		//do math for how everything should be arranged: L/R bounds, how spread out the cards should be based on on many there are (do something with percents or something), etc)
		//foreach card in playerHand --> cars.transform.position = MoveCard(card, where the card is right now, where the card should be)
		//ALSO IMPORTANT: make each card be behind/ infront of the following card, the cards should all be ascending on top of each other and shouldn't have weird overlaps
	}

	public void HighlightCard()
	{
		//if gameState.readyToPlayCard == true, and that card is in the playerHand, and the mouse is over it(in terms of camera?), then turn its highlighted bool on and offset its y pos by ____ amount
	}
	 public void SelectCard()
	{
		//if(gameState.readyToPlayCard && player clicks on a card that is in playerHand)
		//tell gameState to :put that card into the playerDuelDeck
		//                   get next opponent card and put it into opponentDuelDeck



		//gameState.readyToPlayCard == false;
		//gameState.Duel();
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
