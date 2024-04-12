using System;
using System.Collections.Generic;
using System.Linq;
using Server;

// Token: 0x02000378 RID: 888
public class PsUISeasonTopPlayersBanner : PsUISeasonTopTeamsBanner
{
	// Token: 0x060019B8 RID: 6584 RVA: 0x0011ABC2 File Offset: 0x00118FC2
	public PsUISeasonTopPlayersBanner(UIComponent _parent, Dictionary<int, global::Server.Season.SeasonTop> _seasonTop, string _headerText, int _seasonNumber)
		: base(_parent, _seasonTop, _headerText, _seasonNumber)
	{
	}

	// Token: 0x060019B9 RID: 6585 RVA: 0x0011ABD0 File Offset: 0x00118FD0
	public override void GetSeasonTop(int _seasonNumber, bool _first = false)
	{
		if (this.m_seasonTop != null && this.m_seasonTop.Count > 0)
		{
			if (this.m_seasonTop.ContainsKey(_seasonNumber) && this.m_seasonTop[_seasonNumber].topPlayers != null)
			{
				this.m_seasonNumber = _seasonNumber;
				this.UpdateSeasonText();
				this.UpdateButtons();
				this.m_textArea.DestroyChildren();
				int num = 1;
				foreach (global::Server.Season.SeasonTop.SeasonTopEntry seasonTopEntry in this.m_seasonTop[_seasonNumber].topPlayers)
				{
					this.CreateEntry(seasonTopEntry, num);
					num++;
				}
				if (!_first)
				{
					this.m_textArea.Update();
				}
			}
			else if (this.m_seasonTop.ContainsKey(Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Key) && this.m_seasonTop[Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Key].topPlayers != null)
			{
				this.m_seasonNumber = Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Key;
				this.UpdateSeasonText();
				this.UpdateButtons();
				this.m_textArea.DestroyChildren();
				int num2 = 1;
				foreach (global::Server.Season.SeasonTop.SeasonTopEntry seasonTopEntry2 in Enumerable.First<KeyValuePair<int, global::Server.Season.SeasonTop>>(this.m_seasonTop).Value.topPlayers)
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

	// Token: 0x060019BA RID: 6586 RVA: 0x0011ADBC File Offset: 0x001191BC
	public new virtual void PreviousSeason()
	{
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber - 1) && this.m_seasonTop[this.m_seasonNumber - 1].topPlayers != null)
		{
			this.GetSeasonTop(this.m_seasonNumber - 1, false);
		}
	}

	// Token: 0x060019BB RID: 6587 RVA: 0x0011AE10 File Offset: 0x00119210
	public new virtual void NextSeason()
	{
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber + 1) && this.m_seasonTop[this.m_seasonNumber + 1].topPlayers != null)
		{
			this.GetSeasonTop(this.m_seasonNumber + 1, false);
		}
	}

	// Token: 0x060019BC RID: 6588 RVA: 0x0011AE64 File Offset: 0x00119264
	protected new virtual void UpdateButtons()
	{
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber - 1) && this.m_seasonTop[this.m_seasonNumber - 1].topPlayers != null)
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
		if (this.m_seasonTop.ContainsKey(this.m_seasonNumber + 1) && this.m_seasonTop[this.m_seasonNumber + 1].topPlayers != null)
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
}
