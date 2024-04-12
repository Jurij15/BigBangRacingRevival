using System;
using UnityEngine;

// Token: 0x020005AA RID: 1450
public class UITextbox : UIComponent
{
	// Token: 0x06002A0B RID: 10763 RVA: 0x000ACEC4 File Offset: 0x000AB2C4
	public UITextbox(UIComponent _parent, bool _touchable, string _tag, string _text, string _fontResourcePath, float _fontSize, RelativeTo _fontSizeRelativeTo, bool _adjustWidthToTextWidth = false, Align _horizontalAlign = Align.Left, Align _verticalAlign = Align.Top, string _color = null, bool _intFontSize = true, string _shadowColor = null)
		: base(_parent, _touchable, _tag, null, null, string.Empty)
	{
		if (_text == null)
		{
			_text = "null";
		}
		this.SetWidth(UITextbox.m_defaultWidth, UITextbox.m_defaultWidthRelativeTo);
		this.SetHeight(UITextbox.m_defaultHeight, UITextbox.m_defaultHeightRelativeTo);
		this.SetMargins(UITextbox.m_defaultMargins.l, UITextbox.m_defaultMargins.r, UITextbox.m_defaultMargins.t, UITextbox.m_defaultMargins.b, UITextbox.m_defaultMarginsRelativeTo);
		this.SetAlign(0.5f, 0.5f);
		this.m_color = ((_color == null) ? null : _color.Replace("#", string.Empty));
		this.m_text = _text;
		this.m_fontSize = _fontSize;
		this.m_intFontSize = _intFontSize;
		this.m_fontSizeRelativeTo = _fontSizeRelativeTo;
		this.m_adjustWidthToTextWidth = _adjustWidthToTextWidth;
		this.m_fontResourcePath = _fontResourcePath;
		this.m_textHorizontalAlign = _horizontalAlign;
		this.m_textVerticalAlign = _verticalAlign;
		this.m_maxRows = -1;
		this.m_maxWidth = 0f;
		this.m_shadowColor = ((_shadowColor == null) ? null : _shadowColor.Replace("#", string.Empty));
		float num = this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r;
		float num2 = this.m_actualHeight - this.m_actualMargins.t - this.m_actualMargins.b;
		this.m_tmc = TextMeshS.AddComponent(this.m_TC, Vector3.zero, this.m_fontResourcePath, num, num2, this.m_fontSize, this.m_textHorizontalAlign, this.m_textVerticalAlign, this.m_camera, "Text");
		if (this.m_shadowColor != null)
		{
			this.m_shadowtmc = TextMeshS.AddComponent(this.m_TC, this.m_shadowDirection, _fontResourcePath, 0f, 0f, this.m_fontSize, this.m_textHorizontalAlign, this.m_textVerticalAlign, this.m_camera, _tag + "Text");
			Vector2 vector;
			vector..ctor(-0.5f, -1f);
			this.SetShadowShift(vector.normalized, 0.1f);
		}
	}

	// Token: 0x06002A0C RID: 10764 RVA: 0x000AD0E0 File Offset: 0x000AB4E0
	public void SetColor(string _color, string _shadowColor = null)
	{
		this.m_color = ((_color == null) ? null : _color.Replace("#", string.Empty));
		this.m_shadowColor = ((_shadowColor == null) ? null : _shadowColor.Replace("#", string.Empty));
	}

	// Token: 0x06002A0D RID: 10765 RVA: 0x000AD131 File Offset: 0x000AB531
	public void SetShadowShift(Vector2 _direction, float _distanceRelativeToFontSize = 0.1f)
	{
		this.m_shadowDirection = new Vector2(_direction.x, _direction.y);
		this.m_shadowDistance = _distanceRelativeToFontSize;
	}

	// Token: 0x06002A0E RID: 10766 RVA: 0x000AD154 File Offset: 0x000AB554
	public void SetText(string _text)
	{
		this.m_text = _text;
		if (this.m_tmc != null)
		{
			if (this.m_color != null)
			{
				TextMeshS.SetText(this.m_tmc, string.Concat(new string[] { "<color=#", this.m_color, ">", _text, "</color>" }), true);
			}
			else
			{
				TextMeshS.SetText(this.m_tmc, _text, true);
			}
			TextMeshS.WrapTextToTextbox(this.m_tmc, this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r, this.m_actualHeight - this.m_actualMargins.t - this.m_actualMargins.b, _text, this.m_maxRows);
			float num = (this.m_actualMargins.l - this.m_actualMargins.r) * 0.5f;
			float num2 = (this.m_actualMargins.b - this.m_actualMargins.t) * 0.5f;
			this.m_tmc.m_go.transform.Translate(new Vector3(num, num2));
		}
	}

	// Token: 0x06002A0F RID: 10767 RVA: 0x000AD272 File Offset: 0x000AB672
	public void SetMinRows(int _minRows)
	{
		this.m_minRows = _minRows;
	}

	// Token: 0x06002A10 RID: 10768 RVA: 0x000AD27B File Offset: 0x000AB67B
	public void SetMaxRows(int _maxRows)
	{
		this.m_maxRows = _maxRows;
	}

	// Token: 0x06002A11 RID: 10769 RVA: 0x000AD284 File Offset: 0x000AB684
	public void UseDotsWhenWrapping(bool _flag)
	{
		this.m_dots = _flag;
	}

	// Token: 0x06002A12 RID: 10770 RVA: 0x000AD28D File Offset: 0x000AB68D
	public void SetMaxWidth(float _maxWidth)
	{
		this.m_maxWidth = _maxWidth;
	}

	// Token: 0x06002A13 RID: 10771 RVA: 0x000AD298 File Offset: 0x000AB698
	public override void Update()
	{
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateMargins();
		float num = ((!this.m_intFontSize) ? (this.m_fontSize * this.m_fontSizeRelative) : ((float)Mathf.FloorToInt(this.m_fontSize * this.m_fontSizeRelative)));
		TextMeshS.SetFontSize(this.m_tmc, num);
		float num2 = (this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r) * ((float)TextMeshS.m_defaultCharacterSize / this.m_tmc.m_textMesh.characterSize);
		string text = TextMeshS.WrapText(this.m_tmc, this.m_text, num2, this.m_maxRows, this.m_dots);
		Vector2 textSize = TextMeshS.GetTextSize(this.m_tmc, text);
		if (this.m_maxWidth > 0f)
		{
			text = TextMeshS.CutTextToWidth(this.m_tmc, text, this.m_maxWidth);
		}
		if (this.m_adjustWidthToTextWidth)
		{
			if (this.m_fontSizeRelativeTo != RelativeTo.World)
			{
				this.SetWidth((textSize.x + this.m_actualMargins.l + this.m_actualMargins.r) / (float)Screen.width, RelativeTo.ScreenWidth);
			}
			else
			{
				this.SetWidth(textSize.x + this.m_actualMargins.l + this.m_actualMargins.r, RelativeTo.World);
			}
		}
		if (this.m_fontSizeRelativeTo != RelativeTo.World)
		{
			if (textSize.y < ((float)this.m_tmc.m_textMesh.fontSize + this.m_tmc.m_textMesh.lineSpacing) * (float)this.m_minRows)
			{
				this.SetHeight((((float)this.m_tmc.m_textMesh.fontSize + this.m_tmc.m_textMesh.lineSpacing) * (float)this.m_minRows + this.m_actualMargins.t + this.m_actualMargins.b) / (float)Screen.height, RelativeTo.ScreenHeight);
			}
			else
			{
				this.SetHeight((textSize.y + this.m_actualMargins.t + this.m_actualMargins.b) / (float)Screen.height, RelativeTo.ScreenHeight);
			}
		}
		else if (textSize.y < ((float)this.m_tmc.m_textMesh.fontSize + this.m_tmc.m_textMesh.lineSpacing) * (float)this.m_minRows)
		{
			this.SetHeight(((float)this.m_tmc.m_textMesh.fontSize + this.m_tmc.m_textMesh.lineSpacing) * (float)this.m_minRows + this.m_actualMargins.t + this.m_actualMargins.b, RelativeTo.World);
		}
		else
		{
			float num3 = textSize.y / ((float)TextMeshS.m_defaultCharacterSize / this.m_tmc.m_textMesh.characterSize);
			this.SetHeight(num3 + this.m_actualMargins.t + this.m_actualMargins.b, RelativeTo.World);
		}
		this.CalculateReferenceSizes();
		this.UpdateSize();
		string text2 = text;
		if (this.m_color != null)
		{
			text = string.Concat(new string[] { "<color=#", this.m_color, ">", text, "</color>" });
		}
		float num4 = this.m_actualWidth - this.m_actualMargins.l - this.m_actualMargins.r;
		float num5 = this.m_actualHeight - this.m_actualMargins.t - this.m_actualMargins.b;
		TextMeshS.WrapTextToTextbox(this.m_tmc, num4, num5, text, this.m_maxRows);
		float num6 = (this.m_actualMargins.l - this.m_actualMargins.r) * 0.5f;
		float num7 = (this.m_actualMargins.b - this.m_actualMargins.t) * 0.5f;
		this.m_tmc.m_go.transform.Translate(new Vector3(num6, num7));
		if (this.m_shadowtmc != null)
		{
			TextMeshS.SetFontSize(this.m_shadowtmc, num);
			TextMeshS.WrapTextToTextbox(this.m_shadowtmc, num4, num5, string.Concat(new string[] { "<color=#", this.m_shadowColor, ">", text2, "</color>" }), this.m_maxRows);
			this.m_shadowtmc.m_go.transform.Translate(new Vector3(num6, num7) + this.m_shadowDirection * num * this.m_shadowDistance + Vector3.forward * 5f);
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

	// Token: 0x06002A14 RID: 10772 RVA: 0x000AD76C File Offset: 0x000ABB6C
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

	// Token: 0x06002A15 RID: 10773 RVA: 0x000AD991 File Offset: 0x000ABD91
	public override void DrawHandler(UIComponent _c)
	{
	}

	// Token: 0x06002A16 RID: 10774 RVA: 0x000AD994 File Offset: 0x000ABD94
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

	// Token: 0x04002F50 RID: 12112
	public static float m_defaultWidth = 1f;

	// Token: 0x04002F51 RID: 12113
	public static float m_defaultHeight = 1f;

	// Token: 0x04002F52 RID: 12114
	public static cpBB m_defaultMargins = new cpBB(0f);

	// Token: 0x04002F53 RID: 12115
	public static RelativeTo m_defaultWidthRelativeTo = RelativeTo.ParentWidth;

	// Token: 0x04002F54 RID: 12116
	public static RelativeTo m_defaultHeightRelativeTo = RelativeTo.ParentHeight;

	// Token: 0x04002F55 RID: 12117
	public static RelativeTo m_defaultMarginsRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002F56 RID: 12118
	public TextMeshC m_tmc;

	// Token: 0x04002F57 RID: 12119
	public TextMeshC m_shadowtmc;

	// Token: 0x04002F58 RID: 12120
	public Vector2 m_shadowDirection;

	// Token: 0x04002F59 RID: 12121
	public float m_shadowDistance;

	// Token: 0x04002F5A RID: 12122
	private string m_shadowColor;

	// Token: 0x04002F5B RID: 12123
	public string m_text;

	// Token: 0x04002F5C RID: 12124
	public float m_fontSize;

	// Token: 0x04002F5D RID: 12125
	public bool m_intFontSize;

	// Token: 0x04002F5E RID: 12126
	public RelativeTo m_fontSizeRelativeTo;

	// Token: 0x04002F5F RID: 12127
	public float m_fontSizeRelative;

	// Token: 0x04002F60 RID: 12128
	public bool m_adjustWidthToTextWidth;

	// Token: 0x04002F61 RID: 12129
	public string m_fontResourcePath;

	// Token: 0x04002F62 RID: 12130
	public Align m_textHorizontalAlign;

	// Token: 0x04002F63 RID: 12131
	public Align m_textVerticalAlign;

	// Token: 0x04002F64 RID: 12132
	public int m_maxRows;

	// Token: 0x04002F65 RID: 12133
	public int m_minRows;

	// Token: 0x04002F66 RID: 12134
	protected string m_color;

	// Token: 0x04002F67 RID: 12135
	private float m_maxWidth;

	// Token: 0x04002F68 RID: 12136
	private bool m_dots;
}
