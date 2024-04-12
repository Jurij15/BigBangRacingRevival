using System;
using System.Collections.Generic;
using System.Linq;
using Server;

// Token: 0x02000377 RID: 887
public class PsUISeasonTopCreatorsBanner : PsUISeasonTopTeamsBanner
{
	// Token: 0x060019B1 RID: 6577 RVA: 0x0011A5B6 File Offset: 0x001189B6
	public PsUISeasonTopCreatorsBanner(UIComponent _parent, Dictionary<int, global::Server.Season.SeasonTop> _seasonTop, string _headerText)
		: base(_parent, _seasonTop, _headerText, 1)
	{
		this.dataFetchedTime = PsMetagameManager.CurrentDateTime;
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x0011A5D0 File Offset: 0x001189D0
	public override void GetSeasonTop(int _seasonNumber, bool _first = false)
	{
		if (this.m_seasonTop != null && this.m_seasonTop.Count > 0)
		{
			if (this.m_seasonTop.ContainsKey(_seasonNumber) && this.m_seasonTop[_seasonNumber].topCreators != null)
			{
				this.m_seasonNumber = _seasonNumber;
				this.UpdateSeasonText();
				this.UpdateButtons();
				this.m_textArea.DestroyChildren();
				int num = 1;
				if (this.m_seasonTop[_seasonNumber].topCreators != null)
				{
					foreach (global::Server.Season.SeasonTop.SeasonTopEntry seasonTopEntry in this.m_seasonTop[_seasonNumber].topCreators)
					{
						this.CreateEntry(seasonTopEntry, num);
						num++;
					}
				}
				if (!_first)
				{
					this.m_textArea.Update();
				}
			}
			else if (this.m_seasonTop.ContainsKey(Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Key) && this.m_seasonTop[Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Key].topCreators != null)
			{
				this.m_seasonNumber = Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Key;
				this.UpdateSeasonText();
				this.UpdateButtons();
				this.m_textArea.DestroyChildren();
				int num2 = 1;
				foreach (global::Server.Season.SeasonTop.SeasonTopEntry seasonTopEntry2 in Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Value.topCreators)
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

	// Token: 0x060019B3 RID: 6579 RVA: 0x0011A7D0 File Offset: 0x00118BD0
	public override void PreviousSeason()
	{
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber + 1) && this.m_seasonTop[this.m_seasonNumber + 1].topCreators != null)
		{
			this.GetSeasonTop(this.m_seasonNumber + 1, false);
		}
	}

	// Token: 0x060019B4 RID: 6580 RVA: 0x0011A824 File Offset: 0x00118C24
	public override void NextSeason()
	{
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber - 1) && this.m_seasonTop[this.m_seasonNumber - 1].topCreators != null)
		{
			this.GetSeasonTop(this.m_seasonNumber - 1, false);
		}
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x0011A878 File Offset: 0x00118C78
	protected override void UpdateButtons()
	{
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber + 1) && this.m_seasonTop[this.m_seasonNumber + 1].topCreators != null && this.m_seasonTop[this.m_seasonNumber + 1].topCreators.Count > 0)
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
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber - 1) && this.m_seasonTop[this.m_seasonNumber - 1].topCreators != null)
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

	// Token: 0x060019B6 RID: 6582 RVA: 0x0011AA14 File Offset: 0x00118E14
	public override void UpdateSeasonText()
	{
		string text;
		if (this.m_seasonNumber == 1)
		{
			text = PsStrings.Get(StringID.LEADERBOARD_PREVIOUS_MONTH);
		}
		else
		{
			text = PsStrings.Get(StringID.LEADERBOARD_NUMBER_OF_THE_MONTH);
			int num = this.m_seasonNumber * -1;
			text = text.Replace("%1", this.dataFetchedTime.AddMonths(num).ToString("MM/yyyy"));
		}
		this.bottomText.SetText(text);
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x0011AA84 File Offset: 0x00118E84
	protected override void CreateEntry(global::Server.Season.SeasonTop.SeasonTopEntry _item, int _position)
	{
		this.m_iconFrame = "menu_thumbs_up_off";
		UICanvas uicanvas = new UICanvas(this.m_textArea, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uicanvas, false, string.Empty, _position + ". " + _item.name, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox.SetMaxRows(1);
		uitextbox.UseDotsWhenWrapping(true);
		uitextbox.SetWidth(0.6f, RelativeTo.ParentWidth);
		uitextbox.SetHorizontalAlign(0f);
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(0.35f, RelativeTo.ParentWidth);
		uicanvas2.SetHorizontalAlign(1f);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_iconFrame, null), true, true);
		uifittedSprite.SetHorizontalAlign(0f);
		UIText uitext = new UIText(uicanvas2, false, string.Empty, _item.score + string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(1f);
	}

	// Token: 0x04001C2D RID: 7213
	private DateTime dataFetchedTime;
}
