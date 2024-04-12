using System;

// Token: 0x02000233 RID: 563
public class PsUIChestOpenImmediatePopup : PsUIHeaderedCanvas
{
	// Token: 0x060010E6 RID: 4326 RVA: 0x000A25D0 File Offset: 0x000A09D0
	public PsUIChestOpenImmediatePopup(UIComponent _parent)
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

	// Token: 0x060010E7 RID: 4327 RVA: 0x000A26B8 File Offset: 0x000A0AB8
	public void Initialize()
	{
		this.m_headerText = "Locked Treasure";
		this.m_contentText = "Open the chest immediately?";
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x000A26D0 File Offset: 0x000A0AD0
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, this.m_headerText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x000A2748 File Offset: 0x000A0B48
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, this.m_contentText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetSpacing(0.1f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
		this.m_open = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_open.SetAlign(1f, 1f);
		this.m_open.SetText(PsStrings.Get(StringID.OPEN), 0.05f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_open.SetGreenColors(true);
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x000A2829 File Offset: 0x000A0C29
	public void SetPrice(int _price)
	{
		if (this.m_open != null)
		{
			this.m_open.SetDiamondPrice(_price, 0.03f);
			this.m_open.Update();
		}
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x000A2852 File Offset: 0x000A0C52
	public override void Step()
	{
		if (this.m_open != null && this.m_open.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Open");
		}
		base.Step();
	}

	// Token: 0x040013DF RID: 5087
	private PsUIGenericButton m_open;

	// Token: 0x040013E0 RID: 5088
	public VersusMetaData m_versusData;

	// Token: 0x040013E1 RID: 5089
	private string m_headerText;

	// Token: 0x040013E2 RID: 5090
	private string m_contentText;
}
