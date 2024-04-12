using System;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class PsUILinkUrl : UITextbox
{
	// Token: 0x060018DD RID: 6365 RVA: 0x0010F1A0 File Offset: 0x0010D5A0
	public PsUILinkUrl(UIComponent _parent, bool _touchable, string _tag, string _url, string _fontResourcePath, float _fontSize, RelativeTo _fontSizeRelativeTo, bool _adjustWidthToTextWidth = false, Align _horizontalAlign = Align.Left, Align _verticalAlign = Align.Top, string _color = "38a6ea", bool _intFontSize = true, string _shadowColor = null)
		: base(_parent, true, _tag, _url, _fontResourcePath, _fontSize, _fontSizeRelativeTo, _adjustWidthToTextWidth, _horizontalAlign, _verticalAlign, _color, _intFontSize, _shadowColor)
	{
		this.m_url = _url;
		base.SetMaxRows(1);
		base.UseDotsWhenWrapping(true);
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetDrawHandler(new UIDrawDelegate(this.Underline));
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x0010F200 File Offset: 0x0010D600
	private void Underline(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = _c.m_actualWidth;
		num = Mathf.Min(num, TextMeshS.GetTextSize(this.m_tmc, this.m_text).x);
		Vector2[] line = DebugDraw.GetLine(new Vector2(num * -0.5f, _c.m_actualHeight * -0.5f), new Vector2(num * 0.5f, _c.m_actualHeight * -0.5f), 0);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, line, 0.003f * (float)Screen.height, DebugDraw.HexToColor(this.m_color), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line16Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x060018DF RID: 6367 RVA: 0x0010F2B7 File Offset: 0x0010D6B7
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollIn(_touch, _secondary);
		base.SetColor("bc3ef4", null);
		this.Update();
	}

	// Token: 0x060018E0 RID: 6368 RVA: 0x0010F2D3 File Offset: 0x0010D6D3
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		base.SetColor("bc3ef4", null);
		this.Update();
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x0010F2EE File Offset: 0x0010D6EE
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollOut(_touch, _secondary);
		base.SetColor("38a6ea", null);
		this.Update();
	}

	// Token: 0x060018E2 RID: 6370 RVA: 0x0010F30A File Offset: 0x0010D70A
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		base.SetColor("38a6ea", null);
		this.Update();
	}

	// Token: 0x060018E3 RID: 6371 RVA: 0x0010F326 File Offset: 0x0010D726
	public override void Step()
	{
		if (this.m_hit)
		{
			Application.OpenURL(this.m_url);
		}
		base.Step();
	}

	// Token: 0x04001B72 RID: 7026
	private UIText m_link;

	// Token: 0x04001B73 RID: 7027
	public string m_url;
}
