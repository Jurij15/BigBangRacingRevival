using System;
using UnityEngine;

// Token: 0x020004D4 RID: 1236
public class TLTouch : IPoolable
{
	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x060022EB RID: 8939 RVA: 0x00191508 File Offset: 0x0018F908
	// (set) Token: 0x060022EC RID: 8940 RVA: 0x00191510 File Offset: 0x0018F910
	public int m_index
	{
		get
		{
			return this._index;
		}
		set
		{
			this._index = value;
		}
	}

	// Token: 0x060022ED RID: 8941 RVA: 0x0019151C File Offset: 0x0018F91C
	public void Reset()
	{
		this.m_consumed = false;
		this.m_consumingCamera = null;
		this.m_cancelled = false;
		this.m_dragged = false;
		this.m_primaryArea = null;
		this.m_secondaryArea = null;
		this.m_secondaryPhase = TouchAreaPhase.RollOut;
		this.m_primaryPhase = TouchAreaPhase.ReleaseOut;
		this.m_lockedSecondary = null;
		this.m_pressure = 1f;
	}

	// Token: 0x060022EE RID: 8942 RVA: 0x00191573 File Offset: 0x0018F973
	public void Destroy()
	{
	}

	// Token: 0x04002980 RID: 10624
	private int _index;

	// Token: 0x04002981 RID: 10625
	public int m_fingerId;

	// Token: 0x04002982 RID: 10626
	public Vector2 m_startPosition;

	// Token: 0x04002983 RID: 10627
	public Vector2 m_currentPosition;

	// Token: 0x04002984 RID: 10628
	public Vector2 m_deltaPosition;

	// Token: 0x04002985 RID: 10629
	public TouchPhase m_phase;

	// Token: 0x04002986 RID: 10630
	public TouchType m_type;

	// Token: 0x04002987 RID: 10631
	public int m_tapCount;

	// Token: 0x04002988 RID: 10632
	public bool m_consumed;

	// Token: 0x04002989 RID: 10633
	public Camera m_consumingCamera;

	// Token: 0x0400298A RID: 10634
	public bool m_cancelled;

	// Token: 0x0400298B RID: 10635
	public bool m_dragged;

	// Token: 0x0400298C RID: 10636
	public float m_pressure;

	// Token: 0x0400298D RID: 10637
	public TouchAreaC m_primaryArea;

	// Token: 0x0400298E RID: 10638
	public TouchAreaPhase m_primaryPhase;

	// Token: 0x0400298F RID: 10639
	public TouchAreaC m_secondaryArea;

	// Token: 0x04002990 RID: 10640
	public TouchAreaPhase m_secondaryPhase;

	// Token: 0x04002991 RID: 10641
	public TouchAreaC m_lockedSecondary;
}
