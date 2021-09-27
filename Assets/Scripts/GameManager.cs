using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{

	//Get the decks so it can pass them to the GameState
	//Decks
	public Deck playerHand;
	public Deck playerWinDeck;
	public Deck opponentDeck1;
	public Deck opponentDeck2;
	public Deck opponentDeck3;
	public Deck bossDeck;
	public Deck currentOpponentDeck;
	public Deck opponentWinDeck;
	public Deck playerDuelingDeck;  //When player plays a card, it goes into this deck. If there's a tie, then the player plays another card and it goes into this deck
	public Deck opponentDuelingDeck;  //When opponent plays a card, it goes into this deck. If there's a tie, then the opponent plays another card and it goes into this deck  

	public GameState gameState;
	public Viewer viewer;

	public string selectedCardFront = "Images/Cards/StandardCardFace/";
	public string selectedCardBack = "Images/Cards/CardBack/BlackGoldCardBack";

	// Start is called before the first frame update
	void Start()
 {
		//do the singleton instantiation stuff to create a gamestate


		//give the gamestate all of the decks that GameManager has a reference to - unless there's a better way to do this like the gaemstate itself accessing this stuff
		gameState = new GameState();
		gameState.pHand = playerHand;
		gameState.pWDeck = playerWinDeck;
		gameState.oDeck1 = opponentDeck1;
		gameState.oDeck2 = opponentDeck2;
		gameState.oDeck3 = opponentDeck3;
		gameState.bDeck = bossDeck;
		gameState.cODeck = currentOpponentDeck;
		gameState.oWDeck = opponentWinDeck;
		gameState.pDDeck = playerDuelingDeck; //When player plays a card, it goes into this deck. If there's a tie, then the player plays another card and it goes into this deck
		gameState.oDDeck = opponentDuelingDeck;
		gameState.viewer = viewer;

		viewer.gameState = gameState;

		GenerateAllCards();
 }

	/* This is how the game will play out

			Start
					NextDeck              
					Dueling
					roundsplayed++;
					NextDeck              
					Dueling
					roundsplayed++;
					NextDeck              
					Dueling
					roundsplayed++;
					NextDeck              
					Dueling
					roundsplayed++;
					NextDeck              
					Dueling
					roundsplayed++;

			FinalResult
				^^Button sends player back to main menu
	*/

	/*The functionality of major and gelper functions:

		**Start -- GOES INTO GAMEMANAGER
			GenerateAllCards
			AssignDeck(ArrayList<Card>[cards], ArrayList<Card>[destination deck]
					^^Start is gonna do this a few times total --> for all the appropriate decks

		**NextDeck
			CheckForLoss -- GAMEMANAGER
				//Check if player win deck size == 0 && player hand size == 0 --> if true then end game as loss (checking player hand size is for when player shuffles wins into hand)
			[Stuff happens in gamestate]
			Move stuff into position -- VIEWER
 

		**Dueling
			Get size of higher deck: player hand or opponent deck (can include boss deck)
				This will be the number of times the loop will run --> unless someone loses the game --> loses all of their cards/ windeck cards
			--loop (if turns played < max turns) -- GAMEMANAGER handles the calling of Duel
				Duel[GameState] (executes all methods below) -- GAMESTATE
					SelectCard (for the player) --GAMESTATE waits for input from VIEWER
					Duel Happens
								VIEWER>>(also update appropriate W/L, and play SFX/animation)
				Check to see if game should: -- Gamestate?
						Shuffle to continue play (playerhand or opponentdeck size == 0 && turnsPlayed +1 != max turns) -- GAMEMANAGER/GAMESTATE
							If true, then shuffle respective cards from windDeck and continue play (w/ special rules for player cards)
					or
						turnsPlayed++; -- GAMEMANAGER
					CheckForLoss -- Do I put this here or at the top of Dueling? -- GAMEMANAGER -but idk about putting check for loss here
					EndDuel -- Duel again -- HANDLED BY GAMEMANAGER


		**FinalResult -- GAMEMANAGER
		*Ui updates in the viewer
			

	*/

	/* Functions to make:

				General Functions
					*CheckForLoss
					SortCards -- Gamestate/gamemanager?

				Called by Start:
					GenerateAllCards -- gamemanager
					*TransferDeck(bunch of cards, destination deck) -- gamestate

				Called by NextDeck: -- gamestate
					NextOpponentDeck
							Set CurrentDeck = OpponentDeck# or BossDeck
					PlayerWinDeckToHand
					Not a function but there's a lot of moving cards around

				Called by Dueling/Duel
					SelectCard (for the player)  } --> (Move the player card and opponent card at the same time (When the player clicks, the opponent card also starts to move) -- viewer
					GetOpponentNextCard          } -- gamestate
					CompareCards -- gamestate (throw in a wait function for the player to see what's going on)
					Tie -- gamestate
					ResolveDuel -- gamestate
					ShuffleToContinuePlay -- gamestate

				Whatever needs to heppen in FinalResult, pretty sure that'll just be a bunch of things being called and deosn't need a ton of helper functions
				//viewer/ gamemanager
	 */

	public void SendInput(Card card)
	{
		if(gameState.readyToPlayCard && gameState.pHand.cards.Contains(card))
		{
			gameState.Duel();
		}
	}

	public void CheckForLoss()
    {
        //Check if player win deck size == 0 && player hand size == 0
        if(playerWinDeck.cards.Count == 0 && playerHand.cards.Count == 0)
        {
            gameState.gameLost = true;
        }
			if (gameState.gameLost)
			{
				viewer.FinalResult();
			}
    }

  public void GenerateAllCards()
  {
		//get the selected front and backs of the cards from the main menu
		//selectedCardFront = "Images/Cards/StandardCardFace/";

		//have cards spawn offscreen and then tell the viewer to moveCard to wherever it needs to go (have player cards spawn off the bottom of the screen and opponent cards spawn above the screen)

		GeneratePlayerHand();
		Debug.Log("Player hand created | Player Hand Count: " + gameState.pHand.cards.Count);
		GenerateOpponentDecks();
		Debug.Log("Opponent hands created. | ODeck1 Count: " + gameState.oDeck1.cards.Count + " | ODeck2 Count: " + gameState.oDeck2.cards.Count + " | ODeck3 Count: " + gameState.oDeck3.cards.Count);


		gameState.TransferDeck(gameState.oDeck1, gameState.cODeck);

		
		//Get the game going
		//gameState.NextOpponentDeck(gameState.roundsPlayed);
	}

	public void GeneratePlayerHand()
	{
		for (int i = 2; i < 15; i++)
		{
			//get front sprite path
			string pathString;
			if (i == 14) {
				pathString = selectedCardFront + "ace_of_hearts";
			}
			 else if(i == 11)
			{
				pathString = selectedCardFront + "jack_of_hearts2";
			}
			else if (i == 12)
			{
				pathString = selectedCardFront + "queen_of_hearts2";
			}
			else if (i == 13)
			{
				pathString = selectedCardFront + "king_of_hearts2";
			}
			else pathString = selectedCardFront + i + "_of_hearts";

			//get cardSpriteFront
			Sprite cardSpriteFront = Resources.Load<Sprite>(pathString);

			//get cardSpriteBack
			Sprite cardSpriteBack = Resources.Load<Sprite>(selectedCardBack);

			//create card
			GameObject cardObject = new GameObject();
			cardObject.AddComponent<SpriteRenderer>();
			Card card = new Card(i + 1, true, cardObject, cardSpriteFront, cardSpriteBack);
			cardObject.transform.position = gameState.pHand.transform.position;
			gameState.pHand.cards.Add(card);
		}

		viewer.ArrangePlayerHand();
		//Debug.Log("PlayerHand Count: " + gameState.pHand.cards.Count);

	}

	public void GenerateOpponentDecks()
	{
		for(int j = 0; j<3; j++)
		{
			string suit;
			if(j == 0) suit = "clubs";
			if (j == 1) suit = "diamonds";
			else suit = "spades";


			for (int i = 2; i < 15; i++)
			{
				//string pathString;
				string frontPathString;
				if (i == 14)
				{
					frontPathString = selectedCardFront + "ace_of_" + suit + "2";
				}
				else if (i == 11)
				{
					frontPathString = selectedCardFront + "jack_of_" + suit + "2";
				}
				else if (i == 12)
				{
					frontPathString = selectedCardFront + "queen_of_" + suit + "2";
				}
				else if (i == 13)
				{
					frontPathString = selectedCardFront + "king_of_" + suit + "2";
				}
				else frontPathString = selectedCardFront + i + "_of_" + suit;

				//get card front
				Sprite cardSpriteFront = Resources.Load<Sprite>(frontPathString);

				//get card back
				Sprite cardSpriteBack = Resources.Load<Sprite>(selectedCardBack);

				//create card
				GameObject cardObject = new GameObject();
				cardObject.AddComponent<SpriteRenderer>();
				Card card = new Card(i, true, cardObject, cardSpriteFront, cardSpriteBack);

				//assign card to proper opponent deck
				if (j == 0) {
					gameState.oDeck1.cards.Add(card);
					cardObject.transform.position = gameState.oDeck1.transform.position;
				}
				else if (j == 1)
				{
					gameState.oDeck2.cards.Add(card);
					cardObject.transform.position = gameState.oDeck2.transform.position;
				}
				else
				{
					gameState.oDeck3.cards.Add(card);
					cardObject.transform.position = gameState.oDeck3.transform.position;
				}
			}

			//debug stuff
/*			if (j == 0)
			{
				Debug.Log("OpponentDeck1" + " Count: " + gameState.oDeck1.cards.Count);
			}
			if (j == 1)
			{
				Debug.Log("OpponentDeck2" + " Count: " + gameState.oDeck2.cards.Count);
			}
			if (j == 2)
			{
				Debug.Log("OpponentDeck3" + " Count: " + gameState.oDeck3.cards.Count);
			}*/
		}



	}


}
