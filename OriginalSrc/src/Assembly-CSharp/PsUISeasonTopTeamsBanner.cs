using System;
using System.Collections.Generic;
using System.Linq;
using Server;

// Token: 0x02000376 RID: 886
public class PsUISeasonTopTeamsBanner : UICanvas
{
	// Token: 0x060019A9 RID: 6569 RVA: 0x00119C30 File Offset: 0x00118030
	public PsUISeasonTopTeamsBanner(UIComponent _parent, Dictionary<int, global::Server.Season.SeasonTop> _seasonTop, string _headerText, int _seasonNumber)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.m_seasonNumber = _seasonNumber;
		this.RemoveDrawHandler();
		this.m_seasonTop = _seasonTop;
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetMargins(0f, 0f, -0.8f, 0.8f, RelativeTo.OwnHeight);
		uicanvas.SetHeight(0.09f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.RemoveDrawHandler();
		this.m_header = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		this.m_header.SetMargins(0.075f, 0.075f, 0.005f, 0.0125f, RelativeTo.ScreenHeight);
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SeasonTopDrawhandler));
		this.m_buttonHolder = new UICanvas(this.m_header, false, string.Empty, null, string.Empty);
		this.m_buttonHolder.SetHeight(0.5f, RelativeTo.ParentHeight);
		this.m_buttonHolder.SetMargins(-1f, -1f, 0f, 0f, RelativeTo.OwnHeight);
		this.m_buttonHolder.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_triangle", null);
		frame.flipX = true;
		this.m_previousButton = new UIRectSpriteButton(this.m_buttonHolder, string.Empty, PsState.m_uiSheet, frame, true, false);
		this.m_previousButton.SetHorizontalAlign(0f);
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_triangle", null);
		this.m_nextButton = new UIRectSpriteButton(this.m_buttonHolder, string.Empty, PsState.m_uiSheet, frame2, true, false);
		this.m_nextButton.SetHorizontalAlign(1f);
		UICanvas uicanvas2 = new UICanvas(this.m_header, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetVerticalAlign(1f);
		uicanvas2.SetMargins(0.0015f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, _headerText.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FCF675", null);
		UICanvas uicanvas3 = new UICanvas(this.m_header, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetVerticalAlign(0f);
		uicanvas3.SetMargins(0.002f, RelativeTo.ScreenHeight);
		uicanvas3.RemoveDrawHandler();
		this.bottomText = new UIFittedText(uicanvas3, false, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#423312", null);
		this.m_textArea = new UIVerticalList(this, string.Empty);
		this.m_textArea.SetMargins(0.03f, 0.03f, 0.04f, 0.03f, RelativeTo.ScreenHeight);
		this.m_textArea.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		this.m_textArea.SetVerticalAlign(1f);
		this.m_textArea.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
		UICanvas uicanvas4 = new UICanvas(this.m_textArea, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.15f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		this.GetSeasonTop(this.m_seasonNumber, true);
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x00119FAC File Offset: 0x001183AC
	public virtual void PreviousSeason()
	{
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber - 1) && this.m_seasonTop[this.m_seasonNumber - 1].topTeams != null)
		{
			this.GetSeasonTop(this.m_seasonNumber - 1, false);
		}
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x0011A000 File Offset: 0x00118400
	public virtual void NextSeason()
	{
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber + 1) && this.m_seasonTop[this.m_seasonNumber + 1].topTeams != null)
		{
			this.GetSeasonTop(this.m_seasonNumber + 1, false);
		}
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x0011A054 File Offset: 0x00118454
	protected virtual void UpdateButtons()
	{
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber - 1) && this.m_seasonTop[this.m_seasonNumber - 1].topTeams != null)
		{
			if (this.m_previousButton == null)
			{
				Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_triangle", null);
				frame.flipX = true;
				this.m_previousButton = new UIRectSpriteButton(this.m_buttonHolder, string.Empty, PsState.m_uiSheet, frame, true, false);
				this.m_previousButton.SetHorizontalAlign(0f);
				this.m_previousButton.Update();
			}
		}
		else if (this.m_previousButton != null)
		{
			this.m_previousButton.Destroy();
			this.m_previousButton = null;
		}
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber + 1) && this.m_seasonTop[this.m_seasonNumber + 1].topTeams != null)
		{
			if (this.m_nextButton == null)
			{
				Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_triangle", null);
				this.m_nextButton = new UIRectSpriteButton(this.m_buttonHolder, string.Empty, PsState.m_uiSheet, frame2, true, false);
				this.m_nextButton.SetHorizontalAlign(1f);
				this.m_nextButton.Update();
			}
		}
		else if (this.m_nextButton != null)
		{
			this.m_nextButton.Destroy();
			this.m_nextButton = null;
		}
	}

	// Token: 0x060019AD RID: 6573 RVA: 0x0011A1CC File Offset: 0x001185CC
	public virtual void UpdateSeasonText()
	{
		string text;
		if (this.m_seasonNumber == PsMetagameManager.m_seasonEndData.currentSeason.number - 1)
		{
			text = PsStrings.Get(StringID.LEADERBOARD_PREVIOUS_SEASON);
		}
		else
		{
			text = PsStrings.Get(StringID.LEADERBOARD_SEASON_NUMBER);
			text = text.Replace("%1", this.m_seasonNumber.ToString());
		}
		this.bottomText.SetText(text);
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x0011A23C File Offset: 0x0011863C
	public virtual void GetSeasonTop(int _seasonNumber, bool _first = false)
	{
		if (this.m_seasonTop != null && this.m_seasonTop.Count > 0)
		{
			if (this.m_seasonTop.ContainsKey(_seasonNumber) && this.m_seasonTop[_seasonNumber].topTeams != null)
			{
				this.m_seasonNumber = _seasonNumber;
				this.UpdateSeasonText();
				this.UpdateButtons();
				this.m_textArea.DestroyChildren();
				int num = 1;
				foreach (global::Server.Season.SeasonTop.SeasonTopEntry seasonTopEntry in this.m_seasonTop[_seasonNumber].topTeams)
				{
					this.CreateEntry(seasonTopEntry, num);
					num++;
				}
				if (!_first)
				{
					this.m_textArea.Update();
				}
			}
			else if (this.m_seasonTop.ContainsKey(Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Key) && this.m_seasonTop[Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Key].topTeams != null)
			{
				this.m_seasonNumber = Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Key;
				this.UpdateSeasonText();
				this.UpdateButtons();
				this.m_textArea.DestroyChildren();
				int num2 = 1;
				foreach (global::Server.Season.SeasonTop.SeasonTopEntry seasonTopEntry2 in Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Value.topTeams)
				{
					this.CreateEntry(seasonTopEntry2, num2);
					num2++;
				}
				if (!_first)
				{
					this.m_textArea.Update();
				}
			}
			else
			{
				this.Destroy();
			}
		}
	}

	// Token: 0x060019AF RID: 6575 RVA: 0x0011A428 File Offset: 0x00118828
	protected virtual void CreateEntry(global::Server.Season.SeasonTop.SeasonTopEntry _item, int _position)
	{
		UICanvas uicanvas = new UICanvas(this.m_textArea, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uicanvas, false, string.Empty, _position + ". " + _item.name, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox.SetMaxRows(1);
		uitextbox.UseDotsWhenWrapping(true);
		uitextbox.SetWidth(0.6f, RelativeTo.ParentWidth);
		uitextbox.SetHorizontalAlign(0f);
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(0.25f, RelativeTo.ParentWidth);
		uicanvas2.SetHorizontalAlign(1f);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_iconFrame, null), true, true);
		uifittedSprite.SetHorizontalAlign(0f);
		UIText uitext = new UIText(uicanvas2, false, string.Empty, _item.score + string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(1f);
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x0011A55C File Offset: 0x0011895C
	public override void Step()
	{
		if (this.m_nextButton != null && this.m_nextButton.m_hit)
		{
			this.NextSeason();
		}
		else if (this.m_previousButton != null && this.m_previousButton.m_hit)
		{
			this.PreviousSeason();
		}
		base.Step();
	}

	// Token: 0x04001C22 RID: 7202
	public int m_seasonNumber;

	// Token: 0x04001C23 RID: 7203
	protected Dictionary<int, global::Server.Season.SeasonTop> m_seasonTop;

	// Token: 0x04001C24 RID: 7204
	protected UIVerticalList m_textArea;

	// Token: 0x04001C25 RID: 7205
	protected UIFittedText bottomText;

	// Token: 0x04001C26 RID: 7206
	private UICanvas m_header;

	// Token: 0x04001C27 RID: 7207
	protected UICanvas m_buttonHolder;

	// Token: 0x04001C28 RID: 7208
	protected UIRectSpriteButton m_previousButton;

	// Token: 0x04001C29 RID: 7209
	protected UIRectSpriteButton m_nextButton;

	// Token: 0x04001C2A RID: 7210
	protected string m_iconFrame = "menu_trophy_small_full";
}
