using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class PsGameLoopNews : PsTimedEventLoop
{
	// Token: 0x060006F6 RID: 1782 RVA: 0x0004ECE4 File Offset: 0x0004D0E4
	public PsGameLoopNews(EventMessage _eventMessage, PsMinigameContext _context, PsPlanetPath _path)
		: base(_context, string.Empty, string.Empty, _path, 1, _path.m_nodeInfos.Count, 0, 0, -1, null, false, false)
	{
		this.m_eventMessage = _eventMessage;
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0004ED1C File Offset: 0x0004D11C
	public override void StartLoop()
	{
		if (PsState.m_activeGameLoop == null && this.m_popup == null && this.m_eventMessage != null)
		{
			PsMetrics.NewsOpened(this.m_eventMessage.eventType, this.m_eventMessage.eventName, this.m_eventMessage.header, "floatingNode");
			this.m_popup = new PsUIBasePopup(PsGameLoopNews.GetPopupType(this.m_eventMessage), null, null, null, false, true, InitialPage.Center, false, false, false);
			(this.m_popup.m_mainContent as PsUIEventMessagePopup).SetEventMessage(this.m_eventMessage, false);
			this.m_popup.SetAction("Continue", delegate
			{
				this.ClosePopup();
				this.Claim();
			});
			this.m_popup.SetAction("Claim", new Action(this.Claim));
			this.m_popup.SetAction("Exit", new Action(this.ClosePopup));
			this.m_popup.Update();
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		else
		{
			this.ClosePopup();
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0004EE58 File Offset: 0x0004D258
	public static Type GetPopupType(EventMessage _eventMessage)
	{
		Debug.Log(_eventMessage.eventName, null);
		if (_eventMessage.eventName == "bestGame")
		{
			return typeof(PsUICustomEventMessagePopup);
		}
		if (_eventMessage.eventType == "Survey")
		{
			return typeof(PsUICustomerSurveyPopup);
		}
		if (string.IsNullOrEmpty(_eventMessage.eventType))
		{
			return typeof(PsUIEventMessagePopup);
		}
		if (_eventMessage.eventType == "CreatorChallenge")
		{
			return typeof(PsUICreatorChallengePopup);
		}
		return typeof(PsUIEventMessagePopup);
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0004EEF6 File Offset: 0x0004D2F6
	public static bool IsAvailableForPlayer(EventMessage _eventMessage)
	{
		if (_eventMessage.eventType == "Survey")
		{
			return PsMetagameManager.GetOpenedChestCount() > 4;
		}
		return !(_eventMessage.eventType == "Tournament") || true;
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0004EF2E File Offset: 0x0004D32E
	public override string GetBannerString()
	{
		return this.m_eventMessage.header;
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0004EF3B File Offset: 0x0004D33B
	private void ClosePopup()
	{
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
		}
		this.m_popup = null;
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0004EF5C File Offset: 0x0004D35C
	private void Claim()
	{
		if (PsMetagameManager.m_patchNotes != null && this.m_eventMessage.messageId == PsMetagameManager.m_patchNotes.messageId)
		{
			PsMetagameManager.ClaimPatchNote(PsMetagameManager.m_patchNotes.messageId, new Action<HttpC>(PsMetagameManager.ClaimPatchNoteSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimPatchNoteFAILED), null);
			PsMetagameManager.m_patchNotes = null;
		}
		else if (PsMetagameManager.m_eventMessage != null && this.m_eventMessage.messageId == PsMetagameManager.m_eventMessage.messageId)
		{
			PsMetagameManager.ClaimEventMessage(PsMetagameManager.m_eventMessage.messageId, new Action<HttpC>(PsMetagameManager.ClaimEventMessageSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimEventMessageFAILED), null);
			PsMetagameManager.m_eventMessage = null;
		}
		(this.m_node as PsFloatingPlanetNode).DestroyNode();
		this.m_path.m_nodeInfos.Remove(this);
		this.CreateNewEventNode();
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0004F080 File Offset: 0x0004D480
	protected void CreateNewEventNode()
	{
		if (PsState.m_activeGameLoop == null)
		{
			PsMainMenuState.CreateEventNode();
		}
	}

	// Token: 0x04000789 RID: 1929
	public EventMessage m_eventMessage;

	// Token: 0x0400078A RID: 1930
	protected PsUIBasePopup m_popup;
}
