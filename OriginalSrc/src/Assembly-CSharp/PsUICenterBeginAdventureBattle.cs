using System;
using UnityEngine;

// Token: 0x020002C3 RID: 707
public class PsUICenterBeginAdventureBattle : PsUICenterBeginAdventure
{
	// Token: 0x060014EB RID: 5355 RVA: 0x000DA3BF File Offset: 0x000D87BF
	public PsUICenterBeginAdventureBattle(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x000DA3C8 File Offset: 0x000D87C8
	protected override void CreateCenterContent()
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "mapArea");
		uihorizontalList.SetAlign(0.5f, 0.8f);
		uihorizontalList.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uihorizontalList, false, string.Empty, PsStrings.Get(StringID.BOSS_BATTLE_HOWTOWIN), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "#C2FF16", true, "313131");
		uitextbox.SetShadowShift(new Vector2(0.5f, -0.2f), 0.1f);
		uitextbox.SetWidth(0.65f, RelativeTo.ScreenHeight);
	}
}
