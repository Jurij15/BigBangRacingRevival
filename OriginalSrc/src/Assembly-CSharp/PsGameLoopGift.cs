using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class PsGameLoopGift : PsGameLoopNews
{
	// Token: 0x060006FF RID: 1791 RVA: 0x0004F09F File Offset: 0x0004D49F
	public PsGameLoopGift(EventMessage _eventMessage, PsMinigameContext _context, PsPlanetPath _path)
		: base(_eventMessage, _context, _path)
	{
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0004F0AC File Offset: 0x0004D4AC
	public override void StartLoop()
	{
		if (PsState.m_activeGameLoop == null && this.m_eventMessage != null && !this.m_claimed)
		{
			if ((double)this.m_eventMessage.localEndTime > Main.m_EPOCHSeconds && (double)this.m_eventMessage.localStartTime < Main.m_EPOCHSeconds && this.m_eventMessage.messageId > PsMetagameManager.m_giftEvents.lastClaimedGift)
			{
				this.m_claimed = true;
				this.ClaimGift();
				this.m_popup = new PsUIBasePopup(typeof(PsUIEventGiftPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
				(this.m_popup.m_mainContent as PsUIEventGiftPopup).SetEventMessage(this.m_eventMessage, false);
				this.m_popup.Update();
				this.m_popup.SetAction("Continue", new Action(this.ExitPopup));
				TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
			}
			else if ((double)this.m_eventMessage.localStartTime > Main.m_EPOCHSeconds)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Holiday: Gift is not ready to open, timeleft: ",
					(double)this.m_eventMessage.localStartTime - Main.m_EPOCHSeconds,
					", gift content: ",
					this.m_eventMessage.giftContent.ToString()
				}), null);
			}
		}
		else
		{
			Debug.LogError("nope");
		}
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0004F244 File Offset: 0x0004D644
	public void ClaimGift()
	{
		Debug.Log("Claiming gift", null);
		Hashtable hashtable = new Hashtable();
		this.m_eventMessage.giftContent.Claim(hashtable);
		PsMetrics.ChristmasGiftClaimed(this.m_eventMessage.giftContent.GetName());
		PsMetagameManager.ClaimGift(this.m_eventMessage.messageId, hashtable, new Action<HttpC>(PsMetagameManager.ClaimGiftSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimGiftFAILED), null);
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0004F2D4 File Offset: 0x0004D6D4
	public void ExitPopup()
	{
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
			this.m_popup = null;
		}
		this.m_eventMessage.giftContent.EndAction(new Action(this.ContinueAction));
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0004F30F File Offset: 0x0004D70F
	private void ContinueAction()
	{
		(this.m_node as PsFloatingPlanetNode).DestroyNode();
		this.m_path.m_nodeInfos.Remove(this);
		base.CreateNewEventNode();
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0004F33C File Offset: 0x0004D73C
	public override string GetBannerString()
	{
		if ((double)this.m_eventMessage.localStartTime > Main.m_EPOCHSeconds)
		{
			string text = PsStrings.Get(StringID.NODE_HEADER_OPEN_IN);
			return text.Replace("%1", PsMetagameManager.GetTimeStringFromSeconds((int)((double)this.m_eventMessage.localStartTime - Main.m_EPOCHSeconds)));
		}
		return PsStrings.Get(StringID.NODE_HEADER_OPEN_NOW);
	}

	// Token: 0x0400078F RID: 1935
	private bool m_claimed;
}
