using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000273 RID: 627
public abstract class PsUILeaderboardList : UIVerticalList
{
	// Token: 0x060012B9 RID: 4793 RVA: 0x000B922C File Offset: 0x000B762C
	public PsUILeaderboardList(UIComponent _parent)
		: base(_parent, "LeaderboardList")
	{
		this.RemoveDrawHandler();
		this.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(1f);
		this.SetMargins(0f, 0f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.LoadingAnimation();
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x000B929A File Offset: 0x000B769A
	public virtual void LoadingAnimation()
	{
		if ((this.m_parent.m_parent as PsUICenterLeaderboard).m_leaderboard == null)
		{
			this.Load();
		}
		else
		{
			this.Update();
		}
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x000B92C8 File Offset: 0x000B76C8
	public override void Step()
	{
		if (this.m_parent != null && this.m_parent.m_parent != null && this.m_parent.m_parent is PsUICenterLeaderboard && (this.m_parent.m_parent as PsUICenterLeaderboard).m_leaderboard != null && !this.m_created)
		{
			this.CreateContent((this.m_parent.m_parent as PsUICenterLeaderboard).m_leaderboard);
		}
		if (this.m_entryList != null && this.m_entryList.Count > 0 && this.m_currentIndex <= this.m_entryList.Count - 1)
		{
			this.CreateBatch();
		}
		base.Step();
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x000B9388 File Offset: 0x000B7788
	private void CreateContentWithDelay(Leaderboard _leaderboard)
	{
		TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 0f, 0.3f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			this.CreateContent(_leaderboard);
		});
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x000B93D6 File Offset: 0x000B77D6
	public virtual void CreateContent(Leaderboard _leaderboard)
	{
		Debug.LogError("OVERRIDE");
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x000B93E2 File Offset: 0x000B77E2
	protected virtual void Load()
	{
		new PsUILoadingAnimation(this, false);
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x000B93EC File Offset: 0x000B77EC
	protected virtual void DestroyContent()
	{
		this.DestroyChildren();
	}

	// Token: 0x060012C0 RID: 4800 RVA: 0x000B93F4 File Offset: 0x000B77F4
	protected virtual void CreateContent(List<LeaderboardEntry> _entryList)
	{
		this.m_created = true;
		this.DestroyContent();
		if (_entryList == null)
		{
			return;
		}
		this.m_entryList = _entryList;
	}

	// Token: 0x060012C1 RID: 4801 RVA: 0x000B9414 File Offset: 0x000B7814
	protected virtual void CreateBatch()
	{
		int num = this.m_currentIndex + 10;
		num = Mathf.Min(num, this.m_entryList.Count);
		for (int i = this.m_currentIndex; i < num; i++)
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
			uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas.SetWidth(0.15f, RelativeTo.ScreenHeight);
			uicanvas.SetHeight(0.03f, RelativeTo.ScreenHeight);
			uicanvas.SetMargins(0f, 0.01f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, i + 1 + ".", PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), true, null, null);
			uifittedText.SetHorizontalAlign(1f);
			this.AddFlag(uihorizontalList, this.m_entryList[i].user.countryCode);
			UICanvas uicanvas2 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.03f, RelativeTo.ScreenHeight);
			uicanvas2.SetWidth(0.3f, RelativeTo.ScreenWidth);
			uicanvas2.RemoveDrawHandler();
			string text = ((!(this.m_entryList[i].user.playerId != PlayerPrefsX.GetUserId())) ? "#00d2d2" : "#FFFFFF");
			UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, this.m_entryList[i].user.name, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), true, text, "#000000");
			uifittedText2.SetHorizontalAlign(0f);
			this.CreateTrophies(uihorizontalList, this.m_entryList[i]);
			uihorizontalList.Update();
		}
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.ArrangeContents();
		base.UpdateDimensions();
		this.UpdateSize();
		this.UpdateAlign();
		this.UpdateChildrenAlign();
		this.ArrangeContents();
		if (num == this.m_entryList.Count)
		{
			(this.m_parent as UIScrollableCanvas).ArrangeContents();
		}
		this.m_currentIndex = num;
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x000B9644 File Offset: 0x000B7A44
	protected virtual void CreateTrophies(UIComponent _parent, LeaderboardEntry _entry)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.175f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0f, 0.01f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, _entry.trophies.ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), true, null, null);
		uifittedText.SetHorizontalAlign(1f);
		UISprite uisprite = new UISprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_small_full", null), true);
		uisprite.SetSize(0.03f, 0.03f, RelativeTo.ScreenHeight);
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x000B970C File Offset: 0x000B7B0C
	protected virtual void CreateLeaderboardEntry1(UIComponent _positionParent, UIComponent _nameParent, UIComponent _trophyParent, LeaderboardEntry _entry, int i)
	{
		UIText uitext = new UIText(_positionParent, false, string.Empty, (i + 1).ToString() + ".", PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), this.m_fontSize, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(1f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(_nameParent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0f);
		this.AddFlag(uihorizontalList, _entry.user.countryCode);
		string text = ((!(_entry.user.playerId != PlayerPrefsX.GetUserId())) ? "#00d2d2" : "#FFFFFF");
		UIText uitext2 = new UIText(uihorizontalList, false, string.Empty, _entry.user.name, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), this.m_fontSize, RelativeTo.ScreenHeight, text, "#000000");
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(_trophyParent, string.Empty);
		uihorizontalList2.SetHorizontalAlign(1f);
		uihorizontalList2.SetSpacing(0.0158f, RelativeTo.ScreenHeight);
		uihorizontalList2.RemoveDrawHandler();
		UIText uitext3 = new UIText(uihorizontalList2, false, string.Empty, _entry.trophies.ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), this.m_fontSize, RelativeTo.ScreenHeight, null, null);
		uitext3.SetVerticalAlign(0.7f);
		UISprite uisprite = new UISprite(uihorizontalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_small_full", null), true);
		uisprite.SetSize(0.03f, 0.03f, RelativeTo.ScreenHeight);
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x000B9898 File Offset: 0x000B7C98
	protected virtual void AddFlag(UIComponent _parent, string _countryCode)
	{
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(_countryCode, "_alien");
		UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, frame, true, true);
		uifittedSprite.SetDepthOffset(-5f);
		uifittedSprite.SetVerticalAlign(0.7f);
		uifittedSprite.SetHeight(this.m_fontSize * 0.6f, RelativeTo.ScreenHeight);
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x000B98FC File Offset: 0x000B7CFC
	protected virtual void CreateLeaderboardEntry(UIComponent _parent, LeaderboardEntry _entry, int i)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.06f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIText uitext = new UIText(uicanvas, false, string.Empty, (i + 1).ToString() + ".", PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(0f);
		UIComponent uicomponent = new UIComponent(uicanvas, false, string.Empty, null, null, string.Empty);
		uicomponent.SetHorizontalAlign(0f);
		uicomponent.SetWidth(0.4f, RelativeTo.ScreenWidth);
		uicomponent.SetMargins(-0.2f, 0.2f, 0f, 0f, RelativeTo.ScreenHeight);
		uicomponent.RemoveDrawHandler();
		string text = ((!(_entry.user.playerId != PlayerPrefsX.GetUserId())) ? "#00d2d2" : "#FFFFFF");
		UIText uitext2 = new UIText(uicomponent, false, string.Empty, _entry.user.name, PsFontManager.GetFont(PsFonts.HurmeBold), 0.04f, RelativeTo.ScreenHeight, text, "#000000");
		uitext2.m_tmc.m_horizontalAlign = Align.Left;
		uitext2.m_shadowtmc.m_horizontalAlign = Align.Left;
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicanvas, string.Empty);
		uihorizontalList.SetHorizontalAlign(1f);
		uihorizontalList.RemoveDrawHandler();
		UISprite uisprite = new UISprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_small_full", null), true);
		uisprite.SetSize(0.03f, 0.03f, RelativeTo.ScreenHeight);
		UIText uitext3 = new UIText(uihorizontalList, false, string.Empty, _entry.trophies.ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.04f, RelativeTo.ScreenHeight, null, null);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(PlayerPrefsX.GetCountryCode(), "_alien");
		UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, frame, true, true);
		uifittedSprite.SetHorizontalAlign(0.075f);
		uifittedSprite.SetDepthOffset(-5f);
		uifittedSprite.SetHeight(0.15f, RelativeTo.ParentHeight);
	}

	// Token: 0x040015CE RID: 5582
	protected bool m_created;

	// Token: 0x040015CF RID: 5583
	protected float m_fontSize = 0.03f;

	// Token: 0x040015D0 RID: 5584
	private int m_currentIndex;

	// Token: 0x040015D1 RID: 5585
	private List<LeaderboardEntry> m_entryList;
}
