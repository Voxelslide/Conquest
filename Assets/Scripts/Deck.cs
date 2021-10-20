using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class Deck : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();

    public void AssignAllCardPositions()
    {
        //This sounds like a good idea
    }

    public void Sort()
    {
      //sorts cards
      QuickSort(0, cards.Count - 1);
    }


  //I'm having a problem where the value of the card is stored in the Card script attached to a GameObject that is the card in the game. I can't access the value of the card because my decks
  //are ArrayLists that store GameObject, not Card (and I think I ran into the same problem when I switched from ArrayLists to Lists). Since my cards are being stored as the GameObjects they are
  //the information stored by the Card script on the Card object is lost because I can't implicitly cast from a GameObject to a Card.
  //And when I instantiate the cards, I have to make them GameObjects. I can't instantiate Cards. How do I access the card part of my Card GameObjects?




    public void Shuffle()
	{
    //shuffles all cards
	}


    public void AllFlip()
    {
        //flips all cards in deck
    }

    public void AllFaceUp()
    {
        //makes all cards face up
    }

    public void AllFaceDown()
    {
        //makes all cards face down
    }






  //sorting helper functions

  private void Swap(int i, int j)
  {
    GameObject temp = cards[i];
    cards[i] = cards[j];
    cards[j] = temp;
  }

  private int Partition(int low, int high)
  {
    GameObject pivot = cards[high];
    int pivotNumber = pivot.GetComponent<Card>().number;
    int i = (low - 1);

    for (int j = low; j <= high - 1; j++)
    {
      GameObject jj = cards[j];
      if (jj.GetComponent<Card>().number < pivotNumber)
      {
        i++;
        Swap( i, j);
      }
    }
    Swap(i + 1, high);
    return (i + 1);
  }

  private void QuickSort(int low, int high)
  {
    if (low < high)
    {

      // pi is partitioning index, arr[p]
      // is now at right place 
      int pi = Partition(low, high);

      // Separately sort elements before
      // partition and after partition
      QuickSort(low, pi - 1);
      QuickSort(pi + 1, high);
    }
  }

}
