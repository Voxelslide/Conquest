using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataTransfer : MonoBehaviour
{
	public static string cardBackPath;
	public void Start()
	{
		cardBackPath = "Images/Cards/CardBack/BlackGoldCardBack";
	}

	public void SetCardBackGold()
	{
		cardBackPath = "Images/Cards/CardBack/BlackGoldCardBack";
	}

	public void SetCardBackBlue()
	{
		cardBackPath = "Images/Cards/CardBack/BlueCardBack";
	}

	public void SetCardBackP5R()
	{
		cardBackPath = "Images/Cards/CardBack/P5RCardBack";
	}


}

