using System;

// Token: 0x02000236 RID: 566
public class PsUIChestSlotPopup : PsUIHeaderedCanvas
{
	// Token: 0x060010F2 RID: 4338 RVA: 0x000A288C File Offset: 0x000A0C8C
	public PsUIChestSlotPopup(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.Initialize();
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x000A2980 File Offset: 0x000A0D80
	public virtual void Initialize()
	{
		if (PsState.GetCurrentVehicleType(false) == typeof(Motorcycle))
		{
			this.vehicleName = "Dirtbike";
		}
		else if (PsState.GetCurrentVehicleType(false) == typeof(OffroadCar))
		{
			this.vehicleName = "Offroader";
		}
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x000A29D4 File Offset: 0x000A0DD4
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, this.m_headerText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x000A2A4C File Offset: 0x000A0E4C
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, this.m_contentText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		uitextbox.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetSpacing(0.1f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
		this.m_open = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_open.SetAlign(1f, 1f);
		this.m_open.SetText(this.m_playButtonText, 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_open.SetBlueColors(true);
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x000A2B47 File Offset: 0x000A0F47
	public void SetPrice(int _price)
	{
		if (this.m_open != null)
		{
			this.m_open.SetDiamondPrice(_price, 0.03f);
			this.m_open.Update();
		}
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x000A2B70 File Offset: 0x000A0F70
	public override void Step()
	{
		if (this.m_open != null && this.m_open.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Play");
		}
		else if (this.m_exitButton != null && this.m_exitButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x040013E5 RID: 5093
	private PsUIGenericButton m_open;

	// Token: 0x040013E6 RID: 5094
	protected string m_headerText;

	// Token: 0x040013E7 RID: 5095
	protected string m_contentText;

	// Token: 0x040013E8 RID: 5096
	protected string m_playButtonText;

	// Token: 0x040013E9 RID: 5097
	protected string vehicleName = string.Empty;
}
