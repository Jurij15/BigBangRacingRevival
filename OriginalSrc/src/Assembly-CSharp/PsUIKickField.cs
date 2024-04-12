using System;
using UnityEngine;

// Token: 0x0200026A RID: 618
public class PsUIKickField : PsUIInputTextField
{
	// Token: 0x06001287 RID: 4743 RVA: 0x000B70B7 File Offset: 0x000B54B7
	public PsUIKickField()
		: base(null)
	{
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x000B70C0 File Offset: 0x000B54C0
	public PsUIKickField(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x000B70CC File Offset: 0x000B54CC
	protected override void ConstructUI()
	{
		base.SetMinMaxCharacterCount(3, 100);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uihorizontalList, true, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		uitextbox.m_tmc.m_textMesh.color = DebugDraw.HexToColor("#1B405A");
		uitextbox.SetDrawHandler(new UIDrawDelegate(this.LeftCommentDrawhandler));
		uitextbox.SetMinRows(2);
		uitextbox.SetMaxRows(4);
		uitextbox.SetWidth(0.4f, RelativeTo.ScreenWidth);
		base.SetTextField(uitextbox);
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x000B718C File Offset: 0x000B558C
	private void LeftCommentDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.015f * (float)Screen.height, 8, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#ffffff");
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}
}
