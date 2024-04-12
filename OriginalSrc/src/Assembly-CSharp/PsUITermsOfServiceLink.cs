using System;
using UnityEngine;

// Token: 0x020002A5 RID: 677
public class PsUITermsOfServiceLink : UICanvas
{
	// Token: 0x0600145D RID: 5213 RVA: 0x000D00F4 File Offset: 0x000CE4F4
	public PsUITermsOfServiceLink(UIComponent _parent)
		: base(_parent, true, string.Empty, null, string.Empty)
	{
		this.SetHeight(0.04f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.RemoveDrawHandler();
		this.m_color = "38a6ea";
		this.m_tos = new UIText(this, false, string.Empty, "http://www.traplightgames.com/terms/", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.02f, RelativeTo.ScreenHeight, this.m_color, null);
		this.m_tos.SetDrawHandler(new UIDrawDelegate(this.Underline));
		this.m_tos.SetHorizontalAlign(0f);
		this.Update();
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x000D0194 File Offset: 0x000CE594
	private void Underline(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] line = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f), new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f), 0);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, line, 0.003f * (float)Screen.height, DebugDraw.HexToColor(this.m_color), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line16Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x000D022E File Offset: 0x000CE62E
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollIn(_touch, _secondary);
		this.m_color = "bc3ef4";
		this.m_tos.SetColor(this.m_color, null);
		this.m_tos.Update();
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x000D0260 File Offset: 0x000CE660
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		this.m_color = "bc3ef4";
		this.m_tos.SetColor(this.m_color, null);
		this.m_tos.Update();
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x000D0291 File Offset: 0x000CE691
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollOut(_touch, _secondary);
		this.m_color = "38a6ea";
		this.m_tos.SetColor(this.m_color, null);
		this.m_tos.Update();
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x000D02C3 File Offset: 0x000CE6C3
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		this.m_color = "38a6ea";
		this.m_tos.SetColor(this.m_color, null);
		this.m_tos.Update();
	}

	// Token: 0x04001729 RID: 5929
	private UIText m_tos;

	// Token: 0x0400172A RID: 5930
	private string m_color;
}
