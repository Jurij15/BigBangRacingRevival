using System;

// Token: 0x020002EB RID: 747
public class PsUICenterFinishAdventureBattle : UICanvas
{
	// Token: 0x06001619 RID: 5657 RVA: 0x000E6C5C File Offset: 0x000E505C
	public PsUICenterFinishAdventureBattle(UIComponent _parent)
		: base(_parent, false, "WinBattle", null, string.Empty)
	{
		PsUIGenericButton psUIGenericButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetText("Retry", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		psUIGenericButton.SetReleaseAction(delegate
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Start");
		});
		psUIGenericButton.SetAlign(1f, 0f);
	}
}
