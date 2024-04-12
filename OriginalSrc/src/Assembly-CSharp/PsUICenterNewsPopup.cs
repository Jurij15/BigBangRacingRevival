using System;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class PsUICenterNewsPopup : UIVerticalList
{
	// Token: 0x06001652 RID: 5714 RVA: 0x000E98F8 File Offset: 0x000E7CF8
	public PsUICenterNewsPopup(UIComponent _parent)
		: base(_parent, string.Empty)
	{
		this.SetVerticalAlign(1f);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetMargins(0f, 0f, 0.05f, 0.06f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		this.m_contentList = new UIVerticalList(this, string.Empty);
		this.m_contentList.SetVerticalAlign(1f);
		this.m_contentList.SetWidth(0.75f, RelativeTo.ParentWidth);
		this.m_contentList.SetMargins(0f, 0f, 0.05f, 0.06f, RelativeTo.ScreenHeight);
		this.m_contentList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		this.m_contentList.RemoveDrawHandler();
		this.LoadContent();
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x000E99C0 File Offset: 0x000E7DC0
	public void LoadContent()
	{
		this.m_oldTitleCreated = (this.m_newTitleCreated = false);
		new PsUILoadingAnimation(this.m_contentList, false);
		HttpC feed = Event.GetFeed(new Action<HttpC>(this.DataSUCCEED), new Action<HttpC>(this.DataFAILED), null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, feed);
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x000E9A1C File Offset: 0x000E7E1C
	private void DataSUCCEED(HttpC _c)
	{
		Debug.Log("GET NEWS SUCCEED", null);
		this.m_contentList.DestroyChildren();
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("data") && dictionary["data"] != null)
		{
			List<object> list = dictionary["data"] as List<object>;
			Debug.Log(list.Count, null);
			List<EventMessage> list2 = new List<EventMessage>();
			List<EventMessage> list3 = new List<EventMessage>();
			List<EventMessage> list4 = new List<EventMessage>();
			for (int i = 0; i < list.Count; i++)
			{
				EventMessage eventMessage = ClientTools.ParseEventMessageFromDict(list[i] as Dictionary<string, object>);
				Debug.Log(eventMessage.header, null);
				if (eventMessage.tournament == null)
				{
					double num = (double)eventMessage.localEndTime - Main.m_EPOCHSeconds;
					if (!string.IsNullOrEmpty(eventMessage.eventType) && (eventMessage.eventType.ToLower().Contains("event") || eventMessage.eventType.ToLower().Contains("creatorchallenge")) && num > 0.0)
					{
						list3.Add(eventMessage);
					}
					else
					{
						list4.Add(eventMessage);
					}
				}
			}
			list3.Sort((EventMessage x, EventMessage y) => y.startTime.CompareTo(x.startTime));
			list4.Sort((EventMessage x, EventMessage y) => y.startTime.CompareTo(x.startTime));
			this.m_activeCount = list3.Count;
			list2.AddRange(list3);
			list2.AddRange(list4);
			this.m_messages = list2;
		}
		PsUICenterNewsPopup.m_secondsSinceLoad = 0;
		this.m_startSeconds = (int)Math.Floor(Main.m_EPOCHSeconds);
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x000E9BEF File Offset: 0x000E7FEF
	private void DataFAILED(HttpC _c)
	{
		Debug.Log("GET NEWS FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => Event.GetFeed(new Action<HttpC>(this.DataSUCCEED), new Action<HttpC>(this.DataFAILED), null), null);
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x000E9C20 File Offset: 0x000E8020
	public void CreateLoadingArea()
	{
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.33f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetMargins(0.035f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		PsUILoadingAnimation psUILoadingAnimation = new PsUILoadingAnimation(uicanvas, false);
		psUILoadingAnimation.SetVerticalAlign(1f);
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x000E9C80 File Offset: 0x000E8080
	public void CreateBatch()
	{
		int num = this.m_currentIndex + 10;
		num = Mathf.Min(num, this.m_messages.Count);
		for (int i = this.m_currentIndex; i < num; i++)
		{
			if (i == 0 && this.m_activeCount > 0 && !this.m_newTitleCreated)
			{
				UICanvas uicanvas = new UICanvas(this.m_contentList, false, string.Empty, null, string.Empty);
				uicanvas.SetHeight(0.0385f, RelativeTo.ScreenHeight);
				uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
				uicanvas.RemoveDrawHandler();
				UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_TITLE_ACTIVE_EVENTS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "ffffff", "000000");
				this.m_newTitleCreated = true;
				uicanvas.Update();
			}
			else if (i >= this.m_activeCount && !this.m_oldTitleCreated)
			{
				this.CreateOldTitle(this.m_contentList, this.m_newTitleCreated);
				this.m_oldTitleCreated = true;
				this.m_newTitleCreated = true;
			}
			PsUINewsBanner psUINewsBanner = new PsUINewsBanner(this.m_contentList, this.m_messages[i]);
			psUINewsBanner.Update();
		}
		this.m_contentList.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_contentList.CalculateReferenceSizes();
		this.m_contentList.UpdateSize();
		this.m_contentList.ArrangeContents();
		this.m_contentList.UpdateDimensions();
		this.m_contentList.UpdateSize();
		this.m_contentList.UpdateAlign();
		this.m_contentList.UpdateChildrenAlign();
		this.m_contentList.ArrangeContents();
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.ArrangeContents();
		base.UpdateDimensions();
		this.UpdateSize();
		this.UpdateAlign();
		this.UpdateChildrenAlign();
		this.ArrangeContents();
		(this.m_parent as UIScrollableCanvas).CalculateReferenceSizes();
		(this.m_parent as UIScrollableCanvas).UpdateSize();
		(this.m_parent as UIScrollableCanvas).ArrangeContents();
		this.m_currentIndex = num;
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x000E9E80 File Offset: 0x000E8280
	public void CreateOldTitle(UIComponent _parent, bool _newTitleCreated)
	{
		if (_newTitleCreated)
		{
			UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas.SetHeight(0.085f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.0385f, RelativeTo.ScreenHeight);
			uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas2.SetVerticalAlign(0f);
			uicanvas2.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_TITLE_ARCHIVES), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "ffffff", "000000");
			uicanvas.Update();
		}
		else
		{
			UICanvas uicanvas3 = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			uicanvas3.SetHeight(0.0385f, RelativeTo.ScreenHeight);
			uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas3.RemoveDrawHandler();
			UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_TITLE_ARCHIVES), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "ffffff", "000000");
			uicanvas3.Update();
		}
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x000E9F94 File Offset: 0x000E8394
	public override void Step()
	{
		if (this.m_messages != null && this.m_currentIndex < this.m_messages.Count)
		{
			this.CreateBatch();
		}
		if (this.m_startSeconds < (int)Math.Floor(Main.m_EPOCHSeconds))
		{
			this.m_startSeconds = (int)Math.Floor(Main.m_EPOCHSeconds);
			PsUICenterNewsPopup.m_secondsSinceLoad++;
		}
		base.Step();
	}

	// Token: 0x04001911 RID: 6417
	protected UIVerticalList m_contentList;

	// Token: 0x04001912 RID: 6418
	private List<EventMessage> m_messages;

	// Token: 0x04001913 RID: 6419
	private int m_activeCount;

	// Token: 0x04001914 RID: 6420
	private int m_currentIndex;

	// Token: 0x04001915 RID: 6421
	public static int m_secondsSinceLoad;

	// Token: 0x04001916 RID: 6422
	public int m_startSeconds;

	// Token: 0x04001917 RID: 6423
	private bool m_oldTitleCreated;

	// Token: 0x04001918 RID: 6424
	private bool m_newTitleCreated;
}
