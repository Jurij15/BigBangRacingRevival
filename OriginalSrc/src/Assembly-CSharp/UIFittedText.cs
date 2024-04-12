using System;
using UnityEngine;

// Token: 0x020005A7 RID: 1447
public class UIFittedText : UIComponent
{
	// Token: 0x060029F1 RID: 10737 RVA: 0x001B3280 File Offset: 0x001B1680
	public UIFittedText(UIComponent _parent, bool _touchable, string _tag, string _text, string _fontResourcePath, bool _fitTextToWidth, string _color = null, string _shadowColor = null)
		: base(_parent, _touchable, _tag, null, null, string.Empty)
	{
		this.m_fitTextToWidth = _fitTextToWidth;
		this.m_text = _text;
		this.m_color = ((_color == null) ? null : _color.Replace("#", string.Empty));
		this.m_shadowColor = ((_shadowColor == null) ? null : _shadowColor.Replace("#", string.Empty));
		base.SetWidth(UIFittedText.m_defaultWidth, UIFittedText.m_defaultWidthRelativeTo);
		base.SetHeight(UIFittedText.m_defaultHeight, UIFittedText.m_defaultHeightRelativeTo);
		this.SetMargins(UIFittedText.m_defaultMargins.l, UIFittedText.m_defaultMargins.r, UIFittedText.m_defaultMargins.t, UIFittedText.m_defaultMargins.b, UIFittedText.m_defaultMarginsRelativeTo);
		this.RemoveDrawHandler();
		this.m_tmc = TextMeshS.AddComponent(this.m_TC, Vector3.zero, _fontResourcePath, 0f, 0f, 10f, Align.Center, Align.Middle, this.m_camera, _tag + "Text");
		if (this.m_shadowColor != null)
		{
			this.m_shadowtmc = TextMeshS.AddComponent(this.m_TC, this.m_shadowDirection, _fontResourcePath, 0f, 0f, 10f, Align.Center, Align.Middle, this.m_camera, _tag + "Text");
			Vector2 vector;
			vector..ctor(-0.5f, -1f);
			this.SetShadowShift(vector.normalized, 0.1f);
		}
	}

	// Token: 0x060029F2 RID: 10738 RVA: 0x001B33F6 File Offset: 0x001B17F6
	public void SetShadowShift(Vector2 _direction, float _distanceRelativeToFontSize = 0.1f)
	{
		this.m_shadowDirection = new Vector2(_direction.x, _direction.y);
		this.m_shadowDistance = _distanceRelativeToFontSize;
	}

	// Token: 0x060029F3 RID: 10739 RVA: 0x001B3418 File Offset: 0x001B1818
	public void SetColor(string _color, string _shadowColor = null)
	{
		this.m_color = ((_color == null) ? null : _color.Replace("#", string.Empty));
		this.m_shadowColor = ((_shadowColor == null) ? null : _shadowColor.Replace("#", string.Empty));
	}

	// Token: 0x060029F4 RID: 10740 RVA: 0x001B346C File Offset: 0x001B186C
	public void SetText(string _text)
	{
		float num = (this.m_actualMargins.l - this.m_actualMargins.r) * 0.5f;
		float num2 = (this.m_actualMargins.t - this.m_actualMargins.b) * -0.5f;
		this.m_text = _text;
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

	// Token: 0x060029F5 RID: 10741 RVA: 0x001B35E8 File Offset: 0x001B19E8
	public override void SetHeight(float _heightRatio, RelativeTo _relativeTo)
	{
		base.SetHeight(_heightRatio, _relativeTo);
	}

	// Token: 0x060029F6 RID: 10742 RVA: 0x001B35F2 File Offset: 0x001B19F2
	public override void SetWidth(float _widthRatio, RelativeTo _relativeTo)
	{
		base.SetWidth(_widthRatio, _relativeTo);
	}

	// Token: 0x060029F7 RID: 10743 RVA: 0x001B35FC File Offset: 0x001B19FC
	public override void Update()
	{
		base.SetWidth(UIFittedText.m_defaultWidth, UIFittedText.m_defaultWidthRelativeTo);
		base.SetHeight(UIFittedText.m_defaultHeight, UIFittedText.m_defaultHeightRelativeTo);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateMargins();
		float num = (this.m_actualMargins.l - this.m_actualMargins.r) * 0.5f;
		float num2 = (this.m_actualMargins.t - this.m_actualMargins.b) * -0.5f;
		if (this.m_color != null)
		{
			TextMeshS.FitTextToRect(this.m_tmc, this.m_actualWidth, this.m_actualHeight, string.Concat(new string[] { "<color=#", this.m_color, ">", this.m_text, "</color>" }), this.m_fitTextToWidth);
		}
		else
		{
			TextMeshS.FitTextToRect(this.m_tmc, this.m_actualWidth, this.m_actualHeight, this.m_text, this.m_fitTextToWidth);
		}
		this.m_tmc.m_go.transform.Translate(new Vector3(num, num2));
		if (this.m_shadowtmc != null)
		{
			TextMeshS.FitTextToRect(this.m_shadowtmc, this.m_actualWidth, this.m_actualHeight, string.Concat(new string[] { "<color=#", this.m_shadowColor, ">", this.m_text, "</color>" }), this.m_fitTextToWidth);
			this.m_shadowtmc.m_go.transform.Translate(new Vector3(num, num2) + this.m_shadowDirection * (float)this.m_tmc.m_textMesh.fontSize * this.m_shadowDistance + Vector3.forward * 5f);
		}
		Vector2 textSize = TextMeshS.GetTextSize(this.m_tmc, this.m_text);
		base.SetWidth((textSize.x + this.m_actualMargins.l + this.m_actualMargins.r) / (float)Screen.width, RelativeTo.ScreenWidth);
		base.SetHeight((textSize.y + this.m_actualMargins.t + this.m_actualMargins.b) / (float)Screen.height, RelativeTo.ScreenHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateMargins();
		this.UpdateAlign();
		if (!this.m_hidden && this.d_Draw != null)
		{
			this.d_Draw(this);
		}
		this.UpdateUniqueCamera();
		this.UpdateChildren();
		this.ArrangeContents();
	}

	// Token: 0x060029F8 RID: 10744 RVA: 0x001B3890 File Offset: 0x001B1C90
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

	// Token: 0x04002F26 RID: 12070
	public static float m_defaultWidth = 1f;

	// Token: 0x04002F27 RID: 12071
	public static float m_defaultHeight = 1f;

	// Token: 0x04002F28 RID: 12072
	public static cpBB m_defaultMargins = new cpBB(0f);

	// Token: 0x04002F29 RID: 12073
	public static RelativeTo m_defaultWidthRelativeTo = RelativeTo.ParentWidth;

	// Token: 0x04002F2A RID: 12074
	public static RelativeTo m_defaultHeightRelativeTo = RelativeTo.ParentHeight;

	// Token: 0x04002F2B RID: 12075
	public static RelativeTo m_defaultMarginsRelativeTo = RelativeTo.ScreenShortest;

	// Token: 0x04002F2C RID: 12076
	public TextMeshC m_tmc;

	// Token: 0x04002F2D RID: 12077
	public TextMeshC m_shadowtmc;

	// Token: 0x04002F2E RID: 12078
	public Vector2 m_shadowDirection;

	// Token: 0x04002F2F RID: 12079
	public float m_shadowDistance;

	// Token: 0x04002F30 RID: 12080
	private string m_color;

	// Token: 0x04002F31 RID: 12081
	private string m_shadowColor;

	// Token: 0x04002F32 RID: 12082
	public string m_text;

	// Token: 0x04002F33 RID: 12083
	public bool m_fitTextToWidth;
}
