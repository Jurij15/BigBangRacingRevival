using System;

// Token: 0x0200026D RID: 621
public class PsUIPlainTextField : PsUIInputTextField
{
	// Token: 0x06001291 RID: 4753 RVA: 0x000B75FF File Offset: 0x000B59FF
	public PsUIPlainTextField()
		: base(null)
	{
	}

	// Token: 0x06001292 RID: 4754 RVA: 0x000B7608 File Offset: 0x000B5A08
	public PsUIPlainTextField(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001293 RID: 4755 RVA: 0x000B7614 File Offset: 0x000B5A14
	protected override void ConstructUI()
	{
		UITextbox uitextbox = new UITextbox(this, true, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		uitextbox.m_tmc.m_textMesh.color = DebugDraw.HexToColor("#ffffff");
		uitextbox.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.TextfieldOutlined));
		uitextbox.SetMaxRows(1);
		uitextbox.UseDotsWhenWrapping(true);
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
		base.SetTextField(uitextbox);
	}

	// Token: 0x06001294 RID: 4756 RVA: 0x000B76C0 File Offset: 0x000B5AC0
	public override void SetText(string text)
	{
		base.SetText(text);
		if (this.m_root != null && this.m_root is PsUIPlainTextField)
		{
			base.SetMinMaxCharacterCount((this.m_root as PsUIPlainTextField).m_minCharacterCount, (this.m_root as PsUIPlainTextField).m_maxCharacterCount);
		}
	}
}
