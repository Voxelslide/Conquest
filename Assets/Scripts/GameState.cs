using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameState{
	//Game Logic Variables
	private int maxRoundsPlayed = 4;
	public int roundsPlayed = 0;
	public int playerWins = 0;
	public int playerLosses = 0;
	public int playerBossWins = 0;
	public int playerBossLosses = 0;
	public bool gameLost = false;
	private int maxDuels;
	private int currentDuels = 1;
	public bool readyToPlayCard = false;

	public Viewer viewer;

	//Decks
	public Deck pHand;      //playerHand
	public Deck pWDeck;		//playerWinDeck
	public Deck oDeck1;			//opponentDeck1
	public Deck oDeck2;			//opponentDeck2
	public Deck oDeck3;			//opponentDeck3
	public Deck bDeck;			//bossDeck
	public Deck cODeck;			//currentOpponentDeck
	public Deck oWDeck;		//opponentWinDeck
	public Deck pDDeck;//playerDuelingDeck  //When player plays a card, it goes into this deck. If there's a tie, then the player plays another card and it goes into this deck
	public Deck oDDeck;//opponentDuelingDeck  //When opponent plays a card, it goes into this deck. If there's a tie, then the opponent plays another card and it goes into this deck


	//Gamelogic functions
	public void TransferDeck(Deck transferDeck, Deck destinationDeck)
	{
		//transfers all of the cards from the first Deck into the second Deck
		foreach (GameObject card in transferDeck.cards)
		{
			destinationDeck.cards.Add(card);
			card.GetComponent<Card>().ChangeDeck(destinationDeck);
			viewer.MoveCard(card, transferDeck, destinationDeck);
		}
		transferDeck.cards.Clear();
	}

	public void TransferCard(GameObject card, Deck previousDeck, Deck destinationDeck)
	{
		//remove card from current deck
		previousDeck.cards.Remove(card);
		//change card's reference to its deck
		card.GetComponent<Card>().ChangeDeck(destinationDeck);
		//add card to destinationDeck
		destinationDeck.cards.Add(card);
		//move card visually
		viewer.MoveCard(card, previousDeck, destinationDeck);
	}

	public void NextOpponentDeck(int roundsPlayed)
	{
		if (roundsPlayed == 0) //transfer opponentDeck1 into currentOpponentDeck
		{
			TransferDeck(oDeck1, cODeck);
			setMaxDuels();
		}
		else if (roundsPlayed == 1) //transfer opponentDeck2 into currentOpponentDeck, opponentWinDeck to bossDeck, and playerWinDeck to playerHandDeck
		{
			TransferDeck(cODeck, oWDeck);
			TransferDeck(oWDeck, bDeck);
			TransferDeck(oDeck2, cODeck);
			TransferDeck(pWDeck, pHand);
			setMaxDuels();
		}
		else if (roundsPlayed == 2) //transfer opponentDeck3 into currentOpponentDeck, opponentWinDeck to bossDeck, and playerWinDeck to playerHandDeck
		{
			TransferDeck(cODeck, oWDeck);
			TransferDeck(oWDeck, bDeck);
			TransferDeck(oDeck3, cODeck);
			TransferDeck(pWDeck, pHand);
			setMaxDuels();
		}
		//boss time
		else if (roundsPlayed == 3) //transfer bossDeck into CurrentOpponentDeck and playerWinDeck to playerHandDeck
		{
			TransferDeck(bDeck, cODeck);
			setMaxDuels();
		} else if (roundsPlayed >= 4)
		{
			Debug.Log("Game Over");
		}


	}
	public void setMaxDuels()
	{
		if (cODeck.cards.Count > pHand.cards.Count) maxDuels = cODeck.cards.Count;
		else maxDuels = pHand.cards.Count;
		Debug.Log("Max Duels: " + maxDuels);


		currentDuels = 1;

		viewer.ArrangeDeck(cODeck, false);
		readyToPlayCard = true;
	}

/*	public void StartDuel()
	{
		StartCoroutine(Duel());
	}*/

	//IEnumerator Duel()
	public void Duel()
	{
		readyToPlayCard = false;
		//play opponent card at random
		int randOpponentCardIndex = Random.Range(0, cODeck.cards.Count - 1);
		TransferCard(cODeck.cards[randOpponentCardIndex], cODeck, oDDeck);

		//Get the top cards from the duel decks
		GameObject playerCard = pDDeck.cards[pDDeck.cards.Count - 1];
		GameObject opponentCard = oDDeck.cards[oDDeck.cards.Count - 1];

		//yield return new WaitForSeconds(2);
		Debug.Log("Player: " + playerCard.GetComponent<Card>().number + " | Opponent: " + opponentCard.GetComponent<Card>().number);

		if (playerCard.GetComponent<Card>().number > opponentCard.GetComponent<Card>().number)
		{
			//win SFX --> Viewer
			//Debug.Log("Player Wins Duel");
			TransferDeck(oDDeck, pWDeck);
			TransferDeck(pDDeck, pWDeck);
			//Debug.Log("PlayerWinDeck count: " + pDDeck.cards.Count);
		}
		else if(playerCard.GetComponent<Card>().number < opponentCard.GetComponent<Card>().number)
		{
			//lose SFX --> Viewer
			//Debug.Log("Opponent Wins Duel");
			TransferDeck(oDDeck, oWDeck);
			TransferDeck(pDDeck, oWDeck);
			//Debug.Log("OpponentWinDeck count: " + oDDeck.cards.Count);
		}
		//ELSE IT'S A TIE, but you would just wait for the player to click on another card -- WHAT TO DO IF THERE'S A TIE AND NO CARDS LEFT
		currentDuels++;

		//checks to see if player or opponent need to shuffle their wins into their hand/currentDeck for next duel
		if(currentDuels < maxDuels && (pHand.cards.Count == 0 || cODeck.cards.Count == 0))
		{
			//if(pHand.cards.Count == 0) shuffle the cards back into player hand and make them facedown so the player can't see what they're playing
			//shuffleBackWins
		}

		if (currentDuels > maxDuels) Debug.Log("currentDuels > maxDuels ERROR");
		if (currentDuels == maxDuels)
		{
			readyToPlayCard = false;
			roundsPlayed++;
			NextOpponentDeck(roundsPlayed);
		}
		else{
			readyToPlayCard = true;
			viewer.ArrangeDeck(pHand, true);
		}
	}



	public void shuffleBackWins(Deck winDeck, Deck destinationDeck, bool allFaceUp)
	{
		TransferDeck(winDeck, destinationDeck);
		destinationDeck.Shuffle();
		if (allFaceUp) destinationDeck.AllFaceUp();
		else { destinationDeck.AllFaceDown(); }
	}


	

	/*
	 //What needs to be done in GameState

	**NextDeck
			CheckForLoss -- GAMEMANAGER
				//Check if player win deck size == 0 && player hand size == 0 --> if true then end game as loss (checking player hand size is for when player shuffles wins into hand)
			NextOpponentDeck -- GAMESTATE
			PlayerWinDeckToHand -- GAMESTATE
			sort player cards numerically -- GAMESTATE
			Move stuff into position -- VIEWER
	 
	 **Duel (executes all methods below) -- GAMESTATE
					SelectCard (for the player) --GAMESTATE waits for input from VIEWER
					GetOpponentNextCard -- GAMESTATE
					Move the player card and opponent card at the same time (When the player clicks, the opponent card also starts to move) -- VIEWER
					CompareCards -- GAMESTATE
							Tie -- GAMESTATE
								if(playerHand.size != 0 && opponentDeck != 0) duel again (maybe make a tie method that allows the player to play more cards but doesn't resolve, so the
																																					ResolveDuel function below can run properly)
								else if either Player and/or Opponent need to shuffle in order to play, then do that and then call tie function
								else if the duel ends in a tie and either the player or opponent  doesn't have any cards in their hand/deck and winDeck, then flip a coin to see who gets the cards)
					ResolveDuel (Move all played cards from opponent deck/ player hand and move them to the appropriate win deck) -- GAMESTATE
	 
	 
	 */




}
