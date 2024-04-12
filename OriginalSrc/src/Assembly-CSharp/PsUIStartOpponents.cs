using System;

// Token: 0x020002D5 RID: 725
public class PsUIStartOpponents : PsUIOpponents
{
	// Token: 0x0600157C RID: 5500 RVA: 0x000DE27C File Offset: 0x000DC67C
	public PsUIStartOpponents(UIComponent _parent)
		: base(_parent)
	{
		this.m_playerList.SetSpacing(this.m_playerSpacing, RelativeTo.ScreenHeight);
		UICanvas uicanvas = new UICanvas(this.m_playerList, false, string.Empty, null, string.Empty);
		uicanvas.SetMargins(0f, 0.1f, 0f, 0.1f, RelativeTo.ScreenHeight);
		uicanvas.SetHeight(0.035f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.035f, RelativeTo.ScreenHeight);
		uicanvas.SetHorizontalAlign(0.6f);
		uicanvas.RemoveDrawHandler();
		new UIText(uicanvas, false, string.Empty, "Racing with", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, "76f427", "313131")
		{
			m_tmc = 
			{
				m_textMesh = 
				{
					fontStyle = 2
				}
			}
		}.SetHorizontalAlign(0.5f);
		for (int i = 0; i < this.m_profiles.Count; i++)
		{
			this.CreateProfile(this.m_profiles[i], i, true);
		}
	}
}
