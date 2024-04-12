using System;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class PsUIInfoBar : UIHorizontalList
{
	// Token: 0x060011C7 RID: 4551 RVA: 0x000ACC8C File Offset: 0x000AB08C
	public PsUIInfoBar(UIComponent _parent, string _infoText = "", bool _useTween = false)
		: base(_parent, string.Empty)
	{
		this.m_useTween = _useTween;
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
		this.SetMargins(0.025f, 0.025f, 0.015f, 0.015f, RelativeTo.ScreenHeight);
		this.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		UISprite uisprite = new UISprite(this, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_icon_bar_info", null), true);
		uisprite.SetSize(0.062f, 0.062f * (uisprite.m_width / uisprite.m_height), RelativeTo.ScreenHeight);
		uisprite.SetColor(DebugDraw.HexToColor("#9df534") * Color.gray);
		this.m_text = new UITextbox(this, false, "InfoText", _infoText, PsFontManager.GetFont(PsFonts.HurmeBold), 0.028f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "#d1fc93", true, null);
		this.m_tipStrings = new string[] { "Not enough coins for upgrade? Use gems instead!", "Can't clear a level? Try the RENTAL vehicle!", "When others LIKE the levels you've made, you EARN COINS", "Level too hard? Use the SKIP button." };
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x000ACDAC File Offset: 0x000AB1AC
	public void SetText(string _infoText)
	{
		this.m_text.SetText(_infoText);
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x000ACDBC File Offset: 0x000AB1BC
	public override void Update()
	{
		if (string.IsNullOrEmpty(this.m_text.m_text))
		{
			this.SetText(this.m_tipStrings[Random.Range(0, this.m_tipStrings.Length)]);
		}
		base.Update();
		if (this.m_useTween)
		{
			TweenS.AddTransformTween(this.m_TC, TweenedProperty.Position, TweenStyle.BackOut, this.m_TC.transform.localPosition + new Vector3(0f, -80f, 0f), this.m_TC.transform.localPosition, 0.3f, 0f, true);
			this.m_useTween = false;
		}
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x000ACE64 File Offset: 0x000AB264
	public void TweenAway()
	{
		TweenS.AddTransformTween(this.m_TC, TweenedProperty.Position, TweenStyle.BackOut, this.m_TC.transform.localPosition, this.m_TC.transform.localPosition + new Vector3(0f, -80f, 0f), 0.3f, 0f, true);
	}

	// Token: 0x040014BF RID: 5311
	private UITextbox m_text;

	// Token: 0x040014C0 RID: 5312
	private string[] m_tipStrings;

	// Token: 0x040014C1 RID: 5313
	private bool m_useTween;
}
