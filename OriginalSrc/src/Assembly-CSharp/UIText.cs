using System;
using UnityEngine;

// Token: 0x020005A9 RID: 1449
public class UIText : UIComponent
{
	// Token: 0x06002A02 RID: 10754 RVA: 0x001B8D84 File Offset: 0x001B7184
	public UIText(UIComponent _parent, bool _touchable, string _tag, string _text, string _fontResourcePath, float _fontSize, RelativeTo _fontSizeRelativeTo, string _color = null, string _shadowColor = null)
		: base(_parent, _touchable, _tag, null, null, string.Empty)
	{
		this.SetWidth(UIText.m_defaultWidth, UIText.m_defaultWidthRelativeTo);
		this.SetHeight(UIText.m_defaultHeight, UIText.m_defaultHeightRelativeTo);
		this.SetMargins(UIText.m_defaultMargins.l, UIText.m_defaultMargins.r, UIText.m_defaultMargins.t, UIText.m_defaultMargins.b, UIText.m_defaultMarginsRelativeTo);
		this.SetAlign(0.5f, 0.5f);
		this.RemoveDrawHandler();
		this.m_text = _text;
		this.m_fontSize = _fontSize;
		this.m_fontSizeRelativeTo = _fontSizeRelativeTo;
		this.m_fontResourcePath = _fontResourcePath;
		this.m_color = ((_color == null) ? null : _color.Replace("#", string.Empty));
		this.m_shadowColor = ((_shadowColor == null) ? null : _shadowColor.Replace("#", string.Empty));
		this.m_tmc = TextMeshS.AddComponent(this.m_TC, Vector3.zero, _fontResourcePath, 0f, 0f, _fontSize, Align.Center, Align.Middle, this.m_camera, _tag + "Text");
		if (this.m_shadowColor != null)
		{
			this.m_shadowtmc = TextMeshS.AddComponent(this.m_TC, this.m_shadowDirection, _fontResourcePath, 0f, 0f, _fontSize, Align.Center, Align.Middle, this.m_camera, _tag + "Text");
			Vector2 vector;
			vector..ctor(-0.5f, -1f);
			this.SetShadowShift(vector.normalized, 0.1f);
		}
	}

	// Token: 0x06002A03 RID: 10755 RVA: 0x001B8F14 File Offset: 0x001B7314
	public void SetShadowShift(Vector2 _direction, float _distanceRelativeToFontSize = 0.1f)
	{
		this.m_shadowDirection = new Vector2(_direction.x, _direction.y);
		this.m_shadowDistance = _distanceRelativeToFontSize;
	}

	// Token: 0x06002A04 RID: 10756 RVA: 0x001B8F38 File Offset: 0x001B7338
	public void SetColor(string _color, string _shadowColor = null)
	{
		this.m_color = ((_color == null) ? null : _color.Replace("#", string.Empty));
		this.m_shadowColor = ((_shadowColor == null) ? null : _shadowColor.Replace("#", string.Empty));
		this.SetText(this.m_text);
	}

	// Token: 0x06002A05 RID: 10757 RVA: 0x001B8F95 File Offset: 0x001B7395
	public string GetColor()
	{
		return this.m_color;
	}

	// Token: 0x06002A06 RID: 10758 RVA: 0x001B8FA0 File Offset: 0x001B73A0
	public virtual void SetText(string _text)
	{
		this.m_text = _text;
		float num = (this.m_actualMargins.l - this.m_actualMargins.r) * 0.5f;
		float num2 = (this.m_actualMargins.t - this.m_actualMargins.b) * -0.5f;
		if (this.m_color != null)
		{
			TextMeshS.SetText(this.m_tmc, string.Concat(new string[] { "<color=#", this.m_color, ">", this.m_text, "</color>" }), true);
		}
		else
		{
			TextMeshS.SetText(this.m_tmc, this.m_text, true);
		}
		this.m_tmc.m_go.transform.Translate(new Vector3(num, num2));
		if (this.m_shadowtmc != null)
		{
			TextMeshS.SetText(this.m_shadowtmc, string.Concat(new string[] { "<color=#", this.m_shadowColor, ">", this.m_text, "</color>" }), true);
			this.m_shadowtmc.m_go.transform.Translate(new Vector3(num, num2) + this.m_shadowDirection * (float)this.m_shadowtmc.m_textMesh.fontSize * this.m_shadowDistance + Vector3.forward * 5f);
		}
	}

	// Token: 0x06002A07 RID: 10759 RVA: 0x001B911C File Offset: 0x001B751C
	public override void Update()
	{
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateMargins();
		int num = Mathf.FloorToInt(this.m_fontSize * this.m_fontSizeRelative);
		TextMeshS.SetFontSize(this.m_tmc, (float)num);
		if (this.m_shadowtmc != null)
		{
			TextMeshS.SetFontSize(this.m_shadowtmc, (float)num);
		}
		Vector2 textSize = TextMeshS.GetTextSize(this.m_tmc, this.m_text);
		this.SetWidth((textSize.x + this.m_actualMargins.l + this.m_actualMargins.r) / (float)Screen.width, RelativeTo.ScreenWidth);
		this.SetHeight((textSize.y + this.m_actualMargins.t + this.m_actualMargins.b) / (float)Screen.height, RelativeTo.ScreenHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		float num2 = (this.m_actualMargins.l - this.m_actualMargins.r) * 0.5f;
		float num3 = (this.m_actualMargins.t - this.m_actualMargins.b) * -0.5f;
		if (this.m_color != null)
		{
			TextMeshS.SetText(this.m_tmc, string.Concat(new string[] { "<color=#", this.m_color, ">", this.m_text, "</color>" }), true);
		}
		else
		{
			TextMeshS.SetText(this.m_tmc, this.m_text, true);
		}
		this.m_tmc.m_go.transform.Translate(new Vector3(num2, num3));
		if (this.m_shadowtmc != null)
		{
			TextMeshS.SetText(this.m_shadowtmc, string.Concat(new string[] { "<color=#", this.m_shadowColor, ">", this.m_text, "</color>" }), true);
			this.m_shadowtmc.m_go.transform.Translate(new Vector3(num2, num3) + this.m_shadowDirection * (float)num * this.m_shadowDistance + Vector3.forward * 5f);
		}
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

	// Token: 0x06002A08 RID: 10760 RVA: 0x001B9384 File Offset: 0x001B7784
	public override void CalculateReferenceSizes()
	{
		base.CalculateReferenceSizes();
		this.m_fontSizeRelative = (float)Screen.height;
		if (this.m_fontSizeRelativeTo == RelativeTo.World)
		{
			this.m_fontSizeRelative = 1f;
		}
		else if (this.m_fontSizeRelativeTo == RelativeTo.OwnHeight)
		{
			this.m_fontSizeRelative = this.m_actualHeight;
		}
		else if (this.m_fontSizeRelativeTo == RelativeTo.OwnWidth)
		{
			this.m_fontSizeRelative = this.m_actualWidth;
		}
		else if (this.m_fontSizeRelativeTo == RelativeTo.ParentHeight)
		{
			if (this.m_parent != null)
			{
				this.m_fontSizeRelative = this.m_parent.m_actualHeight;
			}
			else
			{
				this.m_fontSizeRelative = (float)Screen.height;
			}
		}
		else if (this.m_fontSizeRelativeTo == RelativeTo.ParentWidth)
		{
			if (this.m_parent != null)
			{
				this.m_fontSizeRelative = this.m_parent.m_actualWidth;
			}
			else
			{
				this.m_fontSizeRelative = (float)Screen.width;
			}
		}
		else if (this.m_fontSizeRelativeTo == RelativeTo.ParentLongest)
		{
			if (this.m_parent != null)
			{
				this.m_fontSizeRelative = Mathf.Max(this.m_parent.m_actualHeight, this.m_parent.m_actualWidth);
			}
			else
			{
				this.m_fontSizeRelative = (float)Mathf.Max(Screen.height, Screen.width);
			}
		}
		else if (this.m_fontSizeRelativeTo == RelativeTo.ParentShortest)
		{
			if (this.m_parent != null)
			{
				this.m_fontSizeRelative = Mathf.Min(this.m_parent.m_actualHeight, this.m_parent.m_actualWidth);
			}
			else
			{
				this.m_fontSizeRelative = (float)Mathf.Min(Screen.height, Screen.width);
			}
		}
		else if (this.m_fontSizeRelativeTo == RelativeTo.ScreenHeight)
		{
			this.m_fontSizeRelative = (float)Screen.height;
		}
		else if (this.m_fontSizeRelativeTo == RelativeTo.ScreenWidth)
		{
			this.m_fontSizeRelative = (float)Screen.width;
		}
		else if (this.m_fontSizeRelativeTo == RelativeTo.ScreenLongest)
		{
			this.m_fontSizeRelative = (float)Mathf.Max(Screen.height, Screen.width);
		}
		else if (this.m_fontSizeRelativeTo == RelativeTo.ScreenShortest)
		{
			this.m_fontSizeRelative = (float)Mathf.Min(Screen.height, Screen.width);
		}
	}

	// Token: 0x06002A09 RID: 10761 RVA: 0x001B95AC File Offset: 0x001B79AC
	public override void SetCamera(Camera _camera, bool _affectChildren, bool _uniqueCamera)
	{
		if (this.m_tmc != null)
		{
			this.m_tmc.m_go.layer = _camera.gameObject.layer;
		}
		if (this.m_shadowtmc != null)
		{
			this.m_shadowtmc.m_go.layer = _camera.gameObject.layer;
		}
		base.SetCamera(_camera, _affectChildren, _uniqueCamera);
	}

	// Token: 0x04002F3E RID: 12094
	public static float m_defaultWidth = 1f;

	// Token: 0x04002F3F RID: 12095
	public static float m_defaultHeight = 1f;

	// Token: 0x04002F40 RID: 12096
	public static cpBB m_defaultMargins = new cpBB(0f);

	// Token: 0x04002F41 RID: 12097
	public static RelativeTo m_defaultWidthRelativeTo = RelativeTo.ParentWidth;

	// Token: 0x04002F42 RID: 12098
	public static RelativeTo m_defaultHeightRelativeTo = RelativeTo.ParentHeight;

	// Token: 0x04002F43 RID: 12099
	public static RelativeTo m_defaultMarginsRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002F44 RID: 12100
	public TextMeshC m_tmc;

	// Token: 0x04002F45 RID: 12101
	public TextMeshC m_shadowtmc;

	// Token: 0x04002F46 RID: 12102
	public Vector2 m_shadowDirection;

	// Token: 0x04002F47 RID: 12103
	public float m_shadowDistance;

	// Token: 0x04002F48 RID: 12104
	private string m_color;

	// Token: 0x04002F49 RID: 12105
	private string m_shadowColor;

	// Token: 0x04002F4A RID: 12106
	public string m_text;

	// Token: 0x04002F4B RID: 12107
	public float m_fontSize;

	// Token: 0x04002F4C RID: 12108
	public RelativeTo m_fontSizeRelativeTo;

	// Token: 0x04002F4D RID: 12109
	public float m_fontSizeRelative;

	// Token: 0x04002F4E RID: 12110
	public bool m_adjustWidthToTextWidth;

	// Token: 0x04002F4F RID: 12111
	public string m_fontResourcePath;
}
