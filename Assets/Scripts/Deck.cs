using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class Deck : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();

    public void Sort()
    {
      //sorts cards
      QuickSort(0, cards.Count - 1);
    }

    public void Shuffle()
	{
    //shuffles all cards
    int n = cards.Count;
    while (n > 1)
    {
      n--;
      int k = Random.Range(0, cards.Count-1);
      GameObject c = cards[k];
      cards[k] = cards[n];
      cards[n] = c;
    }
  }

    public void AllFaceUp()
    {
      foreach (GameObject card in cards)
      {
        if(!card.GetComponent<Card>().faceUp) card.GetComponent<Card>().Flip();
      }
    }

    public void AllFaceDown()
    {
      foreach (GameObject card in cards)
      {
        if (card.GetComponent<Card>().faceUp) card.GetComponent<Card>().Flip();
      }
    }






  //sorting helper functions

  private void Swap(int i, int j)
  {
    if (cards[i].GetComponent<Card>().number != cards[j].GetComponent<Card>().number)
    {
      GameObject temp = cards[i];
      cards[i] = cards[j];
      cards[j] = temp;
    }
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
