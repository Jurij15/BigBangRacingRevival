using System;
using UnityEngine;

// Token: 0x020001AF RID: 431
public class TreasureMap : MonoBehaviour
{
	// Token: 0x06000D6D RID: 3437 RVA: 0x0007E450 File Offset: 0x0007C850
	public void SwapBackground(TreasureMapType background)
	{
		foreach (GameObject gameObject in this.backgrounds)
		{
			gameObject.SetActive(false);
		}
		if (background != TreasureMapType.OffroadCar)
		{
			if (background == TreasureMapType.Motorcycle)
			{
				this.backgrounds[1].SetActive(true);
			}
		}
		else
		{
			this.backgrounds[0].SetActive(true);
		}
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0007E4BB File Offset: 0x0007C8BB
	public void CompleteTransition()
	{
		base.GetComponent<Animator>().SetTrigger("Complete");
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x0007E4D0 File Offset: 0x0007C8D0
	public void RevealPiece(int pieceIndex)
	{
		GameObject gameObject = this.mapPieces[pieceIndex];
		if (gameObject != null)
		{
			gameObject.SetActive(true);
			gameObject.GetComponent<Animator>().SetTrigger("Reveal");
		}
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x0007E509 File Offset: 0x0007C909
	public void ShowPiece(int pieceIndex)
	{
		if (this.mapPieces[pieceIndex] != null)
		{
			this.mapPieces[pieceIndex].SetActive(true);
		}
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0007E52C File Offset: 0x0007C92C
	public void ShowPiece(int[] pieceIndex)
	{
		foreach (int num in pieceIndex)
		{
			if (this.mapPieces[num] != null)
			{
				this.mapPieces[num].SetActive(true);
			}
		}
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0007E574 File Offset: 0x0007C974
	public void HidePiece(int pieceIndex)
	{
		if (this.mapPieces[pieceIndex] != null)
		{
			this.mapPieces[pieceIndex].SetActive(false);
		}
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0007E598 File Offset: 0x0007C998
	public void HidePiece(int[] pieceIndex)
	{
		foreach (int num in pieceIndex)
		{
			if (this.mapPieces[num] != null)
			{
				this.mapPieces[num].SetActive(false);
			}
		}
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x0007E5E0 File Offset: 0x0007C9E0
	public void HideAll()
	{
		for (int i = 0; i < this.mapPieces.Length; i++)
		{
			if (this.mapPieces[i] != null)
			{
				this.mapPieces[i].SetActive(false);
			}
		}
	}

	// Token: 0x04000E99 RID: 3737
	public GameObject[] mapPieces;

	// Token: 0x04000E9A RID: 3738
	public GameObject[] backgrounds;
}
