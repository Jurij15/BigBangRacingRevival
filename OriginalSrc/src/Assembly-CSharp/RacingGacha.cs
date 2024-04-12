using System;
using UnityEngine;

// Token: 0x020001AD RID: 429
public class RacingGacha : MonoBehaviour
{
	// Token: 0x06000D65 RID: 3429 RVA: 0x0007E2DD File Offset: 0x0007C6DD
	public void CompleteTransition()
	{
		base.GetComponent<Animator>().SetTrigger("Complete");
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0007E2F0 File Offset: 0x0007C6F0
	public void RevealPiece(int pieceIndex)
	{
		GameObject gameObject = this.medalPieces[pieceIndex];
		if (gameObject != null)
		{
			gameObject.SetActive(true);
			gameObject.GetComponent<Animator>().SetTrigger("Reveal");
		}
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0007E329 File Offset: 0x0007C729
	public void ShowPiece(int pieceIndex)
	{
		if (this.medalPieces[pieceIndex] != null)
		{
			this.medalPieces[pieceIndex].SetActive(true);
		}
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0007E34C File Offset: 0x0007C74C
	public void ShowPiece(int[] pieceIndex)
	{
		foreach (int num in pieceIndex)
		{
			if (this.medalPieces[num] != null)
			{
				this.medalPieces[num].SetActive(true);
			}
		}
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0007E394 File Offset: 0x0007C794
	public void HidePiece(int pieceIndex)
	{
		if (this.medalPieces[pieceIndex] != null)
		{
			this.medalPieces[pieceIndex].SetActive(false);
		}
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0007E3B8 File Offset: 0x0007C7B8
	public void HidePiece(int[] pieceIndex)
	{
		foreach (int num in pieceIndex)
		{
			if (this.medalPieces[num] != null)
			{
				this.medalPieces[num].SetActive(false);
			}
		}
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0007E400 File Offset: 0x0007C800
	public void HideAll()
	{
		for (int i = 0; i < this.medalPieces.Length; i++)
		{
			if (this.medalPieces[i] != null)
			{
				this.medalPieces[i].SetActive(false);
			}
		}
	}

	// Token: 0x04000E95 RID: 3733
	public GameObject[] medalPieces;
}
