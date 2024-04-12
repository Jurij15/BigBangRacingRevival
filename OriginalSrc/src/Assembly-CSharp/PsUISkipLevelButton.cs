using System;

// Token: 0x0200023B RID: 571
public class PsUISkipLevelButton : PsUIGenericButton
{
	// Token: 0x06001152 RID: 4434 RVA: 0x000A7638 File Offset: 0x000A5A38
	public PsUISkipLevelButton(UIComponent _parent, Action _proceed, Action _cancel)
		: base(_parent, 0.25f, 0.25f, 0.005f, "Button")
	{
		this.m_proceed = _proceed;
		this.m_cancel = _cancel;
		this.m_vlist = new UIVerticalList(this, "SkipLevelVerticalList");
		this.m_vlist.SetWidth(0.3f, RelativeTo.ScreenHeight);
		this.m_vlist.SetVerticalAlign(1f);
		this.m_vlist.RemoveDrawHandler();
		UIText uitext = new UIText(this.m_vlist, false, string.Empty, PsStrings.Get(StringID.SKIP_LEVEL), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenShortest, null, null);
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x000A76D4 File Offset: 0x000A5AD4
	public override void Step()
	{
		bool flag = false;
		int num = PsMetagameManager.m_skipPrice;
		if (this.offsetCanvas == null)
		{
			this.offsetCanvas = new UICanvas(this, false, "LockButtonOffsetCanvas", null, string.Empty);
			this.offsetCanvas.SetRogue();
			this.offsetCanvas.SetHorizontalAlign(1f);
			this.offsetCanvas.SetSize(1f, 1f, RelativeTo.ParentHeight);
			this.offsetCanvas.SetMargins(0f, -0.5f, 0f, 0f, RelativeTo.ParentHeight);
			this.offsetCanvas.RemoveDrawHandler();
			this.m_lockButton = new PsUIGenericButton(this.offsetCanvas, 0.25f, 0.25f, 0.005f, "Button");
			this.m_lockButton.SetMargins(0.01f, 0.01f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			this.m_lockButton.SetDepthOffset(-20f);
			this.m_lockButton.SetOrangeColors(true);
			this.m_lockButton.SetIcon("menu_icon_unlock", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_lockButton.SetHorizontalAlign(1f);
			this.m_diamondPrice = num;
			this.m_lockButton.SetSkipPrice(this.m_diamondPrice, 0.03f);
			base.SetBlueColors(true);
			flag = true;
		}
		if (this.m_diamondPrice != num)
		{
			this.m_diamondPrice = num;
			this.m_lockButton.m_priceText.SetText(string.Empty + this.m_diamondPrice);
		}
		if (this.m_hit)
		{
			new PsSkipLevelFlow(this.m_proceed, this.m_cancel, PsMetagameManager.m_skipPrice);
		}
		if (flag)
		{
			this.Update();
		}
		base.Step();
	}

	// Token: 0x0400143F RID: 5183
	private UIVerticalList m_vlist;

	// Token: 0x04001440 RID: 5184
	private UIText m_unlockText;

	// Token: 0x04001441 RID: 5185
	private int m_diamondPrice;

	// Token: 0x04001442 RID: 5186
	private UICanvas offsetCanvas;

	// Token: 0x04001443 RID: 5187
	public PsUIGenericButton m_lockButton;

	// Token: 0x04001444 RID: 5188
	public Action m_proceed;

	// Token: 0x04001445 RID: 5189
	public Action m_cancel;
}
