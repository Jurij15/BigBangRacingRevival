using System;
using System.Collections.Generic;

// Token: 0x02000408 RID: 1032
public class EventGifts
{
	// Token: 0x06001C87 RID: 7303 RVA: 0x00141610 File Offset: 0x0013FA10
	public List<EventMessage> GetActiveGifts()
	{
		List<EventMessage> list = new List<EventMessage>();
		if (this.m_gifts != null)
		{
			for (int i = 0; i < this.m_gifts.Count; i++)
			{
				if ((double)this.m_gifts[i].localEndTime > Main.m_EPOCHSeconds && (double)this.m_gifts[i].localStartTime < Main.m_EPOCHSeconds && this.lastClaimedGift >= this.m_gifts[i].messageId)
				{
					list.Add(this.m_gifts[i]);
				}
			}
		}
		return list;
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x001416B4 File Offset: 0x0013FAB4
	public bool IsGiftActive(Type _giftComponentType)
	{
		if (this.m_gifts != null)
		{
			for (int i = 0; i < this.m_gifts.Count; i++)
			{
				if ((double)this.m_gifts[i].localEndTime > Main.m_EPOCHSeconds && (double)this.m_gifts[i].localStartTime < Main.m_EPOCHSeconds && this.lastClaimedGift >= this.m_gifts[i].messageId && this.m_gifts[i].giftContent.GetType() == _giftComponentType)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x0014175C File Offset: 0x0013FB5C
	public EventMessage GetNextActiveGift()
	{
		if (this.m_gifts != null)
		{
			for (int i = 0; i < this.m_gifts.Count; i++)
			{
				if ((double)this.m_gifts[i].localEndTime > Main.m_EPOCHSeconds && this.m_gifts[i].messageId > this.lastClaimedGift)
				{
					return this.m_gifts[i];
				}
				Debug.Log(string.Concat(new object[]
				{
					"Holiday: ",
					((double)this.m_gifts[i].localEndTime - Main.m_EPOCHSeconds).ToString(),
					" seconds left, last claimed gift: ",
					this.lastClaimedGift,
					" giftId : ",
					this.m_gifts[i].messageId
				}), null);
			}
		}
		return null;
	}

	// Token: 0x04001FB5 RID: 8117
	public int lastClaimedGift;

	// Token: 0x04001FB6 RID: 8118
	public List<EventMessage> m_gifts;
}
