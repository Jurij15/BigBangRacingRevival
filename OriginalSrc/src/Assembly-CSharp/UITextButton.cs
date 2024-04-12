using System;
using UnityEngine;

// Token: 0x02000583 RID: 1411
public class UITextButton : UITextbox
{
	// Token: 0x0600290D RID: 10509 RVA: 0x001B3AB8 File Offset: 0x001B1EB8
	public UITextButton(UIComponent _parent, string _tag, string _text, string _fontResource = "HurmeRegular_Font", float _fontSize = 0.04f, RelativeTo _fontSizeRelativeTo = RelativeTo.ScreenHeight, bool _adjustWidthToTextWidth = true)
		: base(_parent, true, _tag, _text, _fontResource, _fontSize, _fontSizeRelativeTo, _adjustWidthToTextWidth, Align.Center, Align.Middle, null, true, null)
	{
	}

	// Token: 0x0600290E RID: 10510 RVA: 0x001B3ADC File Offset: 0x001B1EDC
	public override void Update()
	{
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateMargins();
		float num = this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r;
		float num2 = this.m_actualHeight - this.m_actualMargins.t - this.m_actualMargins.b;
		if (this.m_tmc == null)
		{
			this.m_tmc = TextMeshS.AddComponent(this.m_TC, Vector3.zero, this.m_fontResourcePath, num, num2, this.m_fontSize, this.m_textHorizontalAlign, this.m_textVerticalAlign, this.m_camera, "Text");
		}
		int num3 = Mathf.FloorToInt(this.m_fontSize * this.m_fontSizeRelative);
		TextMeshS.SetFontSize(this.m_tmc, (float)num3);
		Vector2 textSize = TextMeshS.GetTextSize(this.m_tmc, this.m_text);
		if (this.m_adjustWidthToTextWidth)
		{
			this.SetWidth((textSize.x + this.m_actualMargins.l + this.m_actualMargins.r) / (float)Screen.width, RelativeTo.ScreenWidth);
		}
		this.SetHeight((textSize.y + this.m_actualMargins.t + this.m_actualMargins.b) / (float)Screen.height, RelativeTo.ScreenHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		TextMeshS.SetTextToTextbox(this.m_tmc, this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r, this.m_actualHeight - this.m_actualMargins.t - this.m_actualMargins.b, this.m_text);
		float num4 = (this.m_actualMargins.l - this.m_actualMargins.r) * 0.5f;
		float num5 = (this.m_actualMargins.t - this.m_actualMargins.b) * -0.5f;
		this.m_tmc.m_go.transform.Translate(new Vector3(num4, num5));
		this.UpdateAlign();
		this.UpdateMargins();
		if (!this.m_hidden && this.d_Draw != null)
		{
			this.d_Draw(this);
		}
		this.UpdateUniqueCamera();
		this.UpdateChildren();
		this.ArrangeContents();
	}
}
