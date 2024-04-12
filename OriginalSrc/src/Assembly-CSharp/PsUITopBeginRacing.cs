using System;

// Token: 0x020002DC RID: 732
public class PsUITopBeginRacing : UICanvas
{
	// Token: 0x060015AF RID: 5551 RVA: 0x000E13E0 File Offset: 0x000DF7E0
	public PsUITopBeginRacing(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_back = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_back.SetOrangeColors(true);
		this.m_back.SetIcon("hud_icon_back", 0.085f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_back.SetSound("/UI/ButtonBack");
		this.m_back.SetAlign(0.03f, 0.97f);
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x000E14B8 File Offset: 0x000DF8B8
	public override void Step()
	{
		if (this.m_back != null && this.m_back.m_TC.p_entity != null && this.m_back.m_TC.p_entity.m_active && (this.m_back.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Back");
		}
		base.Step();
	}

	// Token: 0x04001863 RID: 6243
	private PsUIGenericButton m_back;
}
