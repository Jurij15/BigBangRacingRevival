using System;
using AdMediation;

// Token: 0x02000338 RID: 824
public class PsUICenterUnlockChest : PsUICenterConfirmPurchase
{
	// Token: 0x060017FD RID: 6141 RVA: 0x00104798 File Offset: 0x00102B98
	public PsUICenterUnlockChest(UIComponent _parent)
		: base(_parent)
	{
		this.m_startTime = Main.m_gameTicks;
	}

	// Token: 0x060017FE RID: 6142 RVA: 0x001047AC File Offset: 0x00102BAC
	public override void SetBackground()
	{
	}

	// Token: 0x060017FF RID: 6143 RVA: 0x001047AE File Offset: 0x00102BAE
	protected override void SetSize()
	{
		this.SetWidth(0.78f, RelativeTo.ScreenHeight);
		this.SetHeight(0.8f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x001047C8 File Offset: 0x00102BC8
	public override void CreateContent(UIComponent _parent, int _price, bool _coins, Action<UIComponent, object> _d_CreateSubContent, object _parameter)
	{
		_parent.RemoveTouchAreas();
		_d_CreateSubContent.Invoke(_parent, _parameter);
		this.m_hlist = new UIHorizontalList(this, string.Empty);
		this.m_hlist.RemoveDrawHandler();
		this.m_hlist.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		this.m_hlist.SetAlign(0.5f, 0f);
		this.m_hlist.SetMargins(0f, 0f, 0.05f, -0.05f, RelativeTo.ScreenHeight);
		this.m_buy = new PsUIGenericButton(this.m_hlist, 0.25f, 0.25f, 0.005f, "Button");
		this.m_buy.SetText(PsStrings.Get(StringID.OPEN_NOW), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_buy.SetSound("/UI/ButtonChestOpen");
		this.m_buy.SetGreenColors(true);
		this.m_buy.SetSkipPrice(_price, 0.035f);
		this.CreateAdButton(this.m_hlist, false);
		this.m_timeleftSeconds = PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft;
		string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft);
		string text = PsStrings.Get(StringID.NODE_HEADER_OPEN_IN);
		text = text.Replace("%1", timeStringFromSeconds + string.Empty);
		this.m_timeleft = new UIText(this, false, "timeleft", text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
		this.m_timeleft.SetVerticalAlign(0.925f);
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x00104948 File Offset: 0x00102D48
	private void CreateAdButton(UIComponent _parent, bool _update = false)
	{
		if (PsGachaManager.m_gachas[this.m_slotIndex] == null)
		{
			return;
		}
		if (this.m_watchAdButton != null)
		{
			this.m_watchAdButton.Destroy();
		}
		if (PsAdMediation.adsAvailable())
		{
			this.m_watchAdButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
			this.m_watchAdButton.SetDepthOffset(-5f);
			if (PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft - 1800 > 0)
			{
				this.m_watchAdButton.SetText("-" + 30 + "m", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			}
			else
			{
				this.m_watchAdButton.SetText(PsStrings.Get(StringID.OPEN_NOW), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			}
			this.m_watchAdButton.SetAdIcon();
			if (_update)
			{
				this.m_watchAdButton.Update();
				_parent.Update();
			}
			PsMetrics.AdOffered("chestTimerReduction");
		}
		else
		{
			PsMetrics.AdNotAvailable("chestTimerReduction");
		}
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x00104A61 File Offset: 0x00102E61
	public override void HandleButtonPresses()
	{
		if (this.m_watchAdButton != null && this.m_watchAdButton.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			TouchAreaS.Disable();
			PsAdMediation.ShowAd(new Action<AdResult>(this.AdWatched), null);
		}
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x00104A9C File Offset: 0x00102E9C
	public override void Step()
	{
		if (PsGachaManager.m_gachas[this.m_slotIndex] != null && Main.m_gameTicks - this.m_startTime > 60)
		{
			int unlockTimeLeft = PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft;
			if (unlockTimeLeft != this.m_timeleftSeconds)
			{
				this.m_timeleftSeconds = unlockTimeLeft;
				int num = PsMetagameManager.SecondsToDiamonds(this.m_timeleftSeconds);
				if (this.m_currentPrice != num)
				{
					this.m_currentPrice = num;
					this.m_buy.SetSkipPrice(this.m_currentPrice, 0.035f);
					this.m_buy.Update();
				}
				if (this.m_timeleft != null)
				{
					string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeleftSeconds);
					string text = PsStrings.Get(StringID.NODE_HEADER_OPEN_IN);
					text = text.Replace("%1", timeStringFromSeconds + string.Empty);
					this.m_timeleft.SetText(text);
				}
			}
		}
		base.Step();
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x00104B80 File Offset: 0x00102F80
	private void AdWatched(AdResult _result)
	{
		TouchAreaS.Enable();
		SoundS.PauseMixer(false);
		PsMetrics.AdWatched("chestTimerReduction", _result.ToString());
		if (_result == AdResult.Finished)
		{
			int num = 1800;
			PsGachaManager.ReduceGachaTime(this.m_slotIndex, num, true);
			if (PsGachaManager.m_gachas[this.m_slotIndex].m_unlockTimeLeft <= 0)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Confirm");
			}
			else
			{
				this.CreateAdButton(this.m_hlist, true);
			}
		}
		else if (_result == AdResult.Failed || _result == AdResult.Skipped)
		{
			this.CreateAdButton(this.m_hlist, true);
		}
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x00104C28 File Offset: 0x00103028
	protected override void SetHeaderIcon(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetRogue();
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetMargins(-0.5f, 0.5f, -0.125f, -0.125f, RelativeTo.ParentHeight);
		uicanvas.SetDepthOffset(-2f);
		uicanvas.RemoveDrawHandler();
		string text = ((!this.m_adventure) ? "menu_chest_badge_active" : "menu_debrief_adventure_map_piece3");
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(text, null);
		UISprite uisprite = new UISprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true);
		uisprite.SetSize(0.6f, 0.6f * (frame.height / frame.width), RelativeTo.ParentHeight);
		uisprite.SetHorizontalAlign(0f);
		uisprite.SetDepthOffset(-2f);
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x00104CFC File Offset: 0x001030FC
	public void SetInfo(string _gachaType, int price, string _label, bool _adventure, int _slotIndex)
	{
		this.m_slotIndex = _slotIndex;
		this.m_adventure = _adventure;
		this.CreateContent(this, price, false, new Action<UIComponent, object>(this.GetChest), _gachaType);
		base.CreateHeaderContent(this.m_header, _label, PsStrings.Get(StringID.LEVEL).ToUpper() + " " + (PsMetagameManager.m_playerStats.gachaLevel + 1));
		this.m_header.Update();
		this.Update();
	}

	// Token: 0x04001AD0 RID: 6864
	private int m_slotIndex;

	// Token: 0x04001AD1 RID: 6865
	private bool m_adventure;

	// Token: 0x04001AD2 RID: 6866
	private PsUIGenericButton m_watchAdButton;

	// Token: 0x04001AD3 RID: 6867
	private const int m_minutesToDecrease = 30;

	// Token: 0x04001AD4 RID: 6868
	private UIText m_timeleft;

	// Token: 0x04001AD5 RID: 6869
	private UIHorizontalList m_hlist;

	// Token: 0x04001AD6 RID: 6870
	private int m_timeleftSeconds;

	// Token: 0x04001AD7 RID: 6871
	private int m_currentPrice;

	// Token: 0x04001AD8 RID: 6872
	private int m_startTime;
}
