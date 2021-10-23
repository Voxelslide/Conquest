using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer : MonoBehaviour
{

	public GameState gameState;
	public float cardMoveSpeed = 1f;
	private float sideBuffer;

	//put UI object reference here-- UI will just be a child of Viewer?



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
		float cardWidth = 0.8f;

		if (isPlayerHand) 
		{
			deck.AllFaceUp();
			deck.Sort();
		}

		float yPos = deck.transform.position.y;
		float cardDistance = 17f / (deck.cards.Count == 0 ? 1 : deck.cards.Count);

		for (int i = 0; i< deck.cards.Count; i++)
		{
			Vector3 startPos = deck.cards[i].transform.position;
			Vector3 targetPos = new Vector3(-8.5f + sideBuffer + cardWidth  +  cardDistance * i, yPos, 0);
			MoveCard(deck.cards[i], startPos, targetPos);
			if (isPlayerHand)
			{
				deck.cards[i].GetComponent<SpriteRenderer>().sortingOrder = i;
			}
			else
			{
				deck.cards[i].GetComponent<SpriteRenderer>().sortingOrder = deck.cards.Count - 1 - i;
			}
		}
	}

	//Have a method to start showing the boss winRate/lossRate onscreen as you fight the boss
	public void StartShowingBossWL()
	{

	}


	//Is called when game ends/ player wins/loses
	public void FinalResult()
	{
		if (!gameState.gameLost)
		{
			//this only happens when the player has a positive/even win rate with the final boss
			//if tie
			if(gameState.playerBossWins == gameState.playerBossLosses)
			{
				//tie
				ShowResults(0, "That was close! You tied with the boss deck.");

			}
			else//player wins
			{
				Debug.Log("Player Wins!");
				ShowResults(1, "You beat the boss deck!");

			}
		}

		else
		{
			Debug.Log("Player Loses.");
			//if player hasn't reached the boss yet or ran out of cards during the boss fight
			if ((gameState.playerBossWins == 0 && gameState.playerBossLosses == 0) || gameState.pHand.cards.Count == 0 && gameState.pWDeck.cards.Count == 0)
			{
				ShowResults(-1, "You ran out of cards...");
			}
			else // else the player has a losing record against the boss
			{
				ShowResults(-1, "You lost to the boss...");
			}
		}
	}

	private void ShowResults(int win, string endMessage)
	{
		//int win: win = 1 | tie = 0 | loss = -1
		//EndMessage is info on the result of the game
		/*
		 win : "You beat the boss deck!"
		 tie : "That was close! You ties with the boss deck."
		 loss : "You lost to the boss..."
		 early loss : "You ran out of cards..."
		 */

		//shows UI stuff for if the player won or lost
		if (win == 1)
		{
			//show Win on screen
		}
		if (win == 0)
		{
			//show tie on screen
		}
		else
		{
			//show lose on screen
		}
		//show endMessage

		//also show player total winRate/lossRate

		//BTW the boss winRate will be shown already if the player has made it to the boss
	}

}
