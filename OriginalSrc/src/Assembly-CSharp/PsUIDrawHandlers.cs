using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020003DC RID: 988
public static class PsUIDrawHandlers
{
	// Token: 0x06001BD4 RID: 7124 RVA: 0x00135CE3 File Offset: 0x001340E3
	public static void Test(TransformC tc)
	{
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x00135CE5 File Offset: 0x001340E5
	public static void Black(UIComponent _c)
	{
		PsUIDrawHandlers.BackgroundColor(_c, new Color(0f, 0f, 0f), 1f);
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x00135D06 File Offset: 0x00134106
	public static void Red(UIComponent _c)
	{
		PsUIDrawHandlers.BackgroundColor(_c, new Color(1f, 0f, 0f), 1f);
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x00135D27 File Offset: 0x00134127
	public static void BlueTP(UIComponent _c)
	{
		PsUIDrawHandlers.BackgroundColor(_c, new Color(0.15686275f, 0.33333334f, 0.4392157f), 0.5f);
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x00135D48 File Offset: 0x00134148
	private static void BackgroundColor(UIComponent _c, Color _color, float _alpha)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Color color = _color;
		color.a = _alpha;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x00135DB1 File Offset: 0x001341B1
	public static void TransparentDark(UIComponent _c)
	{
		PsUIDrawHandlers.BackgroundColor(_c, new Color(0f, 0f, 0f), 0.6f);
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x00135DD4 File Offset: 0x001341D4
	private static void BGBlueTint(UIComponent _c, Color _color, float _alpha)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Vector2[] rect2 = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight / 3f, Vector2.zero);
		Color color = _color;
		color.a = _alpha;
		GGData ggdata = new GGData(rect);
		GGData ggdata2 = new GGData(rect2);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabC prefabC = PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata2, color, new Color(1f, 0f, 0f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		prefabC.p_parentTC.transform.Rotate(new Vector3(0f, 0f, -90f));
		PrefabC prefabC2 = PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata2, color, new Color(1f, 1f, 1f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		prefabC2.p_parentTC.transform.Rotate(new Vector3(0f, 0f, 90f));
	}

	// Token: 0x06001BDB RID: 7131 RVA: 0x00135F20 File Offset: 0x00134320
	public static void BgDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x00135FA0 File Offset: 0x001343A0
	public static void UpgradeBarsBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth * 1.1f, _c.m_actualHeight * 1.1f, (float)Screen.width * 0.03f, 8, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth * 1.1f - (float)Screen.height * 0.02f, _c.m_actualHeight * 1.1f - (float)Screen.height * 0.02f, (float)Screen.width * 0.02f, 8, Vector2.zero);
		Vector2[] roundedRect3 = DebugDraw.GetRoundedRect(_c.m_actualWidth * 1.1f - (float)Screen.height * 0.02f, _c.m_actualHeight * 1.1f - (float)Screen.height * 0.02f, (float)Screen.width * 0.02f, 8, Vector2.zero);
		string text = "#2b7db0";
		string text2 = "#86d9f9";
		string text3 = "#41acee";
		string text4 = text2;
		string text5 = "#0f689f";
		string text6 = "#0a3c5a";
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 10f, roundedRect, DebugDraw.HexToUint(text), DebugDraw.HexToUint(text2), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, roundedRect, (float)Screen.height * 0.005f, DebugDraw.HexToColor(text), DebugDraw.HexToColor(text2), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 6f + Vector3.down * 2f, roundedRect, (float)Screen.height * 0.02f, DebugDraw.HexToColor(text6), DebugDraw.HexToColor(text5), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 5f, roundedRect2, DebugDraw.HexToUint("#072a3e"), DebugDraw.HexToUint("#072a3e"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, roundedRect3, (float)Screen.height * 0.0075f, DebugDraw.HexToColor(text3), DebugDraw.HexToColor(text4), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -3f, roundedRect3, (float)Screen.height * 0.025f, new Color(1f, 1f, 1f, 0.25f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Inside, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_glare", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, _c.m_actualHeight * 1.1f * 0.5f - (float)Screen.height * 0.055f, -5f), 0f);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth * 1.1f - (float)Screen.height * 0.065f, (float)Screen.height * 0.05f);
		SpriteS.SetColor(spriteC, new Color(1f, 1f, 1f, 0.7f));
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_shine", null);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(_c.m_actualWidth * 1.1f * -0.5f + (float)Screen.height * 0.055f, _c.m_actualHeight * 1.1f * 0.5f - (float)Screen.height * 0.05f, -6f), 25f);
		SpriteS.SetDimensions(spriteC2, (float)Screen.height * 0.03f, (float)Screen.height * 0.015f);
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC3, new Vector3(_c.m_actualWidth * 1.1f * 0.5f - (float)Screen.height * 0.055f, _c.m_actualHeight * 1.1f * -0.5f + (float)Screen.height * 0.05f, -6f), 205f);
		SpriteS.SetDimensions(spriteC3, (float)Screen.height * 0.015f, (float)Screen.height * 0.0095f);
		SpriteS.SetColor(spriteC3, new Color(1f, 1f, 1f, 0.6f));
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x00136478 File Offset: 0x00134878
	public static void ScrollingUIHeader(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		string text = "#3c8195";
		string text2 = "#1d4964";
		Vector2[] roundedBezierRect = DebugDraw.GetRoundedBezierRect(_c.m_actualWidth - (float)Screen.height * 0.02f, _c.m_actualHeight - (float)Screen.height * 0.01f, (float)Screen.height * 0.045f, -1f, 0f, 0f, true, true, false, false);
		Color black = Color.black;
		black.a = 0.765f;
		GGData ggdata = new GGData(roundedBezierRect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.up * (float)(-(float)Screen.height) * 0.01f + Vector3.forward * 6f, ggdata, DebugDraw.HexToColor(text2), DebugDraw.HexToColor(text2), ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), _c.m_camera);
		Vector2[] line = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * -0.5f + (float)Screen.height * 0.01f, _c.m_actualHeight * -0.5f - (float)Screen.height * 0.005f), new Vector2(_c.m_actualWidth * 0.5f - (float)Screen.height * 0.01f, _c.m_actualHeight * -0.5f - (float)Screen.height * 0.005f), 0);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, line, (float)Screen.height * 0.0075f, DebugDraw.HexToColor(text), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, false);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 7f + Vector3.down * 0.0135f * (float)Screen.height, line, (float)Screen.height * 0.03f, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x00136674 File Offset: 0x00134A74
	public static void ScrollingUIFooter(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		string text = "#3c8195";
		string text2 = "#1d4964";
		Vector2[] roundedBezierRect = DebugDraw.GetRoundedBezierRect(_c.m_actualWidth - (float)Screen.height * 0.02f, _c.m_actualHeight - (float)Screen.height * 0.01f, (float)Screen.height * 0.045f, -1f, 0f, 0f, false, false, true, true);
		Color black = Color.black;
		black.a = 0.765f;
		GGData ggdata = new GGData(roundedBezierRect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.up * (float)(-(float)Screen.height) * 0.01f + Vector3.forward * 6f, ggdata, DebugDraw.HexToColor(text2), DebugDraw.HexToColor(text2), ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), _c.m_camera);
		Vector2[] line = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * -0.5f + (float)Screen.height * 0.015f, _c.m_actualHeight * 0.5f - (float)Screen.height * 0.0125f), new Vector2(_c.m_actualWidth * 0.5f - (float)Screen.height * 0.015f, _c.m_actualHeight * 0.5f - (float)Screen.height * 0.0125f), 0);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, line, (float)Screen.height * 0.0075f, DebugDraw.HexToColor(text), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, false);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 7f + Vector3.down * 0.0135f * (float)Screen.height, line, (float)Screen.height * 0.03f, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x00136870 File Offset: 0x00134C70
	public static void ScrollingUIBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedBezierRect = DebugDraw.GetRoundedBezierRect(_c.m_actualWidth, _c.m_actualHeight, (float)Screen.height * 0.05f, -1f, -1f, 0.2f, true, true, true, true);
		Vector2[] roundedBezierRect2 = DebugDraw.GetRoundedBezierRect(_c.m_actualWidth - (float)Screen.height * 0.02f, _c.m_actualHeight - (float)Screen.height * 0.02f, (float)Screen.height * 0.045f, -1f, -1f, 0.2f, true, true, true, true);
		string text = "#217eb3";
		string text2 = "#165882";
		string text3 = "#86d9f9";
		string text4 = "#1fb3e9";
		string text5 = "#3c90be";
		string text6 = "#114d71";
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 10f, roundedBezierRect, DebugDraw.HexToUint(text2), DebugDraw.HexToUint(text), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, roundedBezierRect, (float)Screen.height * 0.0075f, DebugDraw.HexToColor(text6), DebugDraw.HexToColor(text5), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 7f + Vector3.down * 2f, roundedBezierRect, (float)Screen.height * 0.02f, DebugDraw.HexToColor("#0a3c5a"), DebugDraw.HexToColor("#0f689f"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 8f, roundedBezierRect2, DebugDraw.HexToUint("#072a3e"), DebugDraw.HexToUint("#072a3e"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f, roundedBezierRect2, (float)Screen.height * 0.0075f, DebugDraw.HexToColor(text4), DebugDraw.HexToColor(text3), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, roundedBezierRect2, (float)Screen.height * 0.025f, new Color(1f, 1f, 1f, 0.25f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Inside, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_glare", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, _c.m_actualHeight * 0.5f - (float)Screen.height * 0.0425f, -5f), 0f);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth - (float)Screen.height * 0.065f, (float)Screen.height * 0.05f);
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_shine", null);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(_c.m_actualWidth * -0.5f + (float)Screen.height * 0.055f, _c.m_actualHeight * 0.5f - (float)Screen.height * 0.035f, -6f), 25f);
		SpriteS.SetDimensions(spriteC2, (float)Screen.height * 0.03f, (float)Screen.height * 0.015f);
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC3, new Vector3(_c.m_actualWidth * 0.5f - (float)Screen.height * 0.04f, _c.m_actualHeight * -0.5f + (float)Screen.height * 0.04f, -6f), 205f);
		SpriteS.SetDimensions(spriteC3, (float)Screen.height * 0.015f, (float)Screen.height * 0.0095f);
		SpriteS.SetColor(spriteC3, new Color(1f, 1f, 1f, 0.6f));
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x00136CC4 File Offset: 0x001350C4
	public static void CoinLabelBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.1f * _c.m_actualHeight, 8, Vector2.zero);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, DebugDraw.HexToColor("#ffcc1b"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect, DebugDraw.HexToUint("#FFFFFF"), DebugDraw.HexToUint("#FFFFFF"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x00136D88 File Offset: 0x00135188
	public static void BoltLabelBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.1f * _c.m_actualHeight, 8, Vector2.zero);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, DebugDraw.HexToColor("#4dcfff"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect, DebugDraw.HexToUint("#FFFFFF"), DebugDraw.HexToUint("#FFFFFF"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x00136E4C File Offset: 0x0013524C
	public static void KeyLabelBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.1f * _c.m_actualHeight, 8, Vector2.zero);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, roundedRect, 0.005f * (float)Screen.height, DebugDraw.HexToColor("#416d87"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * -1f, roundedRect, DebugDraw.HexToUint("#FFFFFF"), DebugDraw.HexToUint("#FFFFFF"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x00136F18 File Offset: 0x00135318
	public static void DiamondLabelBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.1f * _c.m_actualHeight, 8, Vector2.zero);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, DebugDraw.HexToColor("#f15bfb"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect, DebugDraw.HexToUint("#FFFFFF"), DebugDraw.HexToUint("#FFFFFF"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x00136FDC File Offset: 0x001353DC
	public static void TicketLabelBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.1f * _c.m_actualHeight, 8, Vector2.zero);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, DebugDraw.HexToColor("#5bd7fb"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect, DebugDraw.HexToUint("#FFFFFF"), DebugDraw.HexToUint("#FFFFFF"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x001370A0 File Offset: 0x001354A0
	public static void RaceTimerBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_background", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 0f), 0f);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x0013712C File Offset: 0x0013552C
	public static void ResourceLabelBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_background", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 0f), 0f);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth, _c.m_actualHeight * 0.65f);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x001371C0 File Offset: 0x001355C0
	public static void CoinResourceBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[21];
		float num = (float)Screen.height * 0.015f;
		Vector2[] arc = DebugDraw.GetArc(num, 10, 90f, 90f, new Vector2(_c.m_actualWidth * -0.425f + num, _c.m_actualHeight * 0.325f - num));
		Vector2[] arc2 = DebugDraw.GetArc(num, 10, 90f, 0f, new Vector2(_c.m_actualWidth * 0.425f - num, _c.m_actualHeight * 0.325f - num));
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		array[array.Length - 1] = arc[0];
		GGData ggdata = new GGData(array);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth * 0.85f, _c.m_actualHeight * 0.65f, num, 8, Vector2.zero);
		GGData ggdata2 = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#985718");
		Color black = Color.black;
		Color white = Color.white;
		white.a = 0.15f;
		black.a = 0.4f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, roundedRect, 0.0075f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 3f, ggdata2, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x0013738C File Offset: 0x0013578C
	public static void CurrencyLabelBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.25f * _c.m_actualHeight, 8, Vector2.zero);
		Color white = Color.white;
		uint num = DebugDraw.ColorToUInt(Color.white);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, roundedRect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(0f, 0f, 0.1f), roundedRect, 0.002f * (float)Screen.height, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x0013744C File Offset: 0x0013584C
	public static void DiamondResourceBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[21];
		float num = (float)Screen.height * 0.015f;
		Vector2[] arc = DebugDraw.GetArc(num, 10, 90f, 90f, new Vector2(_c.m_actualWidth * -0.425f + num, _c.m_actualHeight * 0.325f - num));
		Vector2[] arc2 = DebugDraw.GetArc(num, 10, 90f, 0f, new Vector2(_c.m_actualWidth * 0.425f - num, _c.m_actualHeight * 0.325f - num));
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		array[array.Length - 1] = arc[0];
		GGData ggdata = new GGData(array);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth * 0.85f, _c.m_actualHeight * 0.65f, num, 8, Vector2.zero);
		GGData ggdata2 = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#68009c");
		Color black = Color.black;
		Color white = Color.white;
		white.a = 0.15f;
		black.a = 0.4f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, roundedRect, 0.0075f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 3f, ggdata2, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x00137618 File Offset: 0x00135A18
	public static void TransparentRoundedRectBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth + _c.m_actualHeight * 0.35f, _c.m_actualHeight * 0.65f, 0.1f * _c.m_actualHeight, 8, new Vector2(_c.m_actualHeight * 0.35f, 0f));
		Color black = Color.black;
		black.a = 0.6f;
		uint num = DebugDraw.ColorToUInt(black);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 2f, roundedRect, 0.005f * (float)Screen.height, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x00137708 File Offset: 0x00135B08
	public static void TransparentBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Color black = Color.black;
		black.a = 0.6f;
		uint num = DebugDraw.ColorToUInt(black);
		Material material = new Material(Shader.Find("Framework/VertexColorUnlit"));
		material.renderQueue = 3000;
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, material, _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x00137794 File Offset: 0x00135B94
	public static void DebriefBackground(UIComponent _c)
	{
		float num = _c.m_actualWidth;
		float num2 = _c.m_actualHeight;
		UIScrollableCanvas uiscrollableCanvas = _c as UIScrollableCanvas;
		if (uiscrollableCanvas != null)
		{
			num = Mathf.Max(num, uiscrollableCanvas.m_contentWidth);
			num2 = Mathf.Max(num2, uiscrollableCanvas.m_contentHeight);
			num *= 1.5f;
			num2 *= 1.5f;
		}
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, num2 * 0.25f), false);
		Vector2[] rect2 = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, -num2 * 0.25f), false);
		uint num3 = DebugDraw.ColorToUInt(DebugDraw.GetColor(0f, 0f, 0f, 150f));
		uint num4 = DebugDraw.ColorToUInt(DebugDraw.GetColor(0f, 0f, 0f, 150f));
		uint num5 = DebugDraw.ColorToUInt(DebugDraw.GetColor(0f, 0f, 0f, 150f));
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect, num4, num3, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect2, num5, num4, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x00137970 File Offset: 0x00135D70
	public static void LeagueBackgroundInactive(UIComponent _c)
	{
		float num = _c.m_actualWidth;
		float num2 = _c.m_actualHeight;
		UIScrollableCanvas uiscrollableCanvas = _c as UIScrollableCanvas;
		if (uiscrollableCanvas != null)
		{
			num = Mathf.Max(num, uiscrollableCanvas.m_contentWidth);
			num2 = Mathf.Max(num2, uiscrollableCanvas.m_contentHeight);
			num *= 1.5f;
			num2 *= 1.5f;
		}
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, num2 * 0.25f), false);
		Vector2[] rect2 = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, -num2 * 0.25f), false);
		Color color = new Color32(0, 0, 0, 200);
		uint num3 = DebugDraw.ColorToUInt(color);
		uint num4 = DebugDraw.ColorToUInt(color);
		uint num5 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_league_border", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, num2 / 2f, -5f), 0f);
		SpriteS.SetDimensions(spriteC, num, 0.005f * (float)Screen.height);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, camera, true, null);
		float num6 = 0.02f * (float)Screen.height;
		Color color2 = color;
		Vector2[] array = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, num2)
		};
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(0f, num2),
			new Vector2(0f, 0f)
		};
		Material material = new Material(ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(num / 2f + num6 / 2f, -num2 / 2f, -2f), array, num6, color2, material, _c.m_camera, Position.Center, false);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(-num / 2f - num6 / 2f, -num2 / 2f, -2f), array2, num6, color2, material, _c.m_camera, Position.Center, false);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect, num4, num3, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect2, num5, num4, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BEE RID: 7150 RVA: 0x00137CA0 File Offset: 0x001360A0
	public static void LeagueBackgroundActive(UIComponent _c)
	{
		float num = _c.m_actualWidth;
		float num2 = _c.m_actualHeight;
		UIScrollableCanvas uiscrollableCanvas = _c as UIScrollableCanvas;
		if (uiscrollableCanvas != null)
		{
			num = Mathf.Max(num, uiscrollableCanvas.m_contentWidth);
			num2 = Mathf.Max(num2, uiscrollableCanvas.m_contentHeight);
			num *= 1.5f;
			num2 *= 1.5f;
		}
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, num2 * 0.25f), false);
		Vector2[] rect2 = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, -num2 * 0.25f), false);
		Color color = new Color32(0, 0, 0, 120);
		uint num3 = DebugDraw.ColorToUInt(color);
		uint num4 = DebugDraw.ColorToUInt(color);
		uint num5 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_league_border", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, -num2 / 2f, -5f), 0f);
		SpriteS.SetDimensions(spriteC, num, 0.005f * (float)Screen.height);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, camera, true, null);
		float num6 = 0.02f * (float)Screen.height;
		Color color2 = color;
		Vector2[] array = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, num2)
		};
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(0f, num2),
			new Vector2(0f, 0f)
		};
		Material material = new Material(ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(num / 2f + num6 / 2f, -num2 / 2f, -2f), array, num6, color2, material, _c.m_camera, Position.Center, false);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(-num / 2f - num6 / 2f, -num2 / 2f, -2f), array2, num6, color2, material, _c.m_camera, Position.Center, false);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect, num4, num3, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect2, num5, num4, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BEF RID: 7151 RVA: 0x00137FD0 File Offset: 0x001363D0
	public static void LeagueBackgroundHighlight(UIComponent _c)
	{
		float num = _c.m_actualWidth;
		float num2 = _c.m_actualHeight;
		UIScrollableCanvas uiscrollableCanvas = _c as UIScrollableCanvas;
		if (uiscrollableCanvas != null)
		{
			num = Mathf.Max(num, uiscrollableCanvas.m_contentWidth);
			num2 = Mathf.Max(num2, uiscrollableCanvas.m_contentHeight);
			num *= 1.5f;
			num2 *= 1.5f;
		}
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, num2 * 0.25f), false);
		Vector2[] rect2 = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, -num2 * 0.25f), false);
		Color color = new Color32(0, 0, 0, 65);
		uint num3 = DebugDraw.ColorToUInt(color);
		uint num4 = DebugDraw.ColorToUInt(color);
		uint num5 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		float num6 = 0.02f * (float)Screen.height;
		Color color2 = color;
		Vector2[] array = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, num2)
		};
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(0f, num2),
			new Vector2(0f, 0f)
		};
		Material material = new Material(ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material));
		material.shader = Shader.Find("WOE/Unlit/ColorUnlitTransparentBg");
		material.color = color;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(num / 2f + num6 / 2f, -num2 / 2f, -2f), array, num6, color2, material, _c.m_camera, Position.Center, false);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(-num / 2f - num6 / 2f, -num2 / 2f, -2f), array2, num6, color2, material, _c.m_camera, Position.Center, false);
		Material material2 = new Material(ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material));
		material2.shader = Shader.Find("WOE/Unlit/ColorUnlitTransparentBg");
		material2.color = color;
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect, num4, num3, material2, camera, string.Empty, null);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect2, num5, num4, material2, camera, string.Empty, null);
	}

	// Token: 0x06001BF0 RID: 7152 RVA: 0x001382C0 File Offset: 0x001366C0
	public static void TransparentSmoothEdges(UIComponent _c)
	{
		float num = _c.m_actualWidth;
		float num2 = _c.m_actualHeight;
		UIScrollableCanvas uiscrollableCanvas = _c as UIScrollableCanvas;
		if (uiscrollableCanvas != null)
		{
			num = Mathf.Max(num, uiscrollableCanvas.m_contentWidth);
			num2 = Mathf.Max(num2, uiscrollableCanvas.m_contentHeight);
			num *= 1.5f;
			num2 *= 1.5f;
		}
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, num2 * 0.25f), false);
		Vector2[] rect2 = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, -num2 * 0.25f), false);
		Color color = new Color32(0, 0, 0, 120);
		uint num3 = DebugDraw.ColorToUInt(color);
		uint num4 = DebugDraw.ColorToUInt(color);
		uint num5 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		float num6 = 0.02f * (float)Screen.height;
		Color color2 = color;
		Vector2[] array = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, num2)
		};
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(0f, num2),
			new Vector2(0f, 0f)
		};
		Material material = new Material(ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material));
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(num / 2f + num6 / 2f, -num2 / 2f, -2f), array, num6, color2, material, _c.m_camera, Position.Center, false);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(-num / 2f - num6 / 2f, -num2 / 2f, -2f), array2, num6, color2, material, _c.m_camera, Position.Center, false);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect, num4, num3, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect2, num5, num4, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BF1 RID: 7153 RVA: 0x0013857C File Offset: 0x0013697C
	public static void DashedBottom(UIComponent _c)
	{
		float num = _c.m_actualWidth;
		float num2 = _c.m_actualHeight;
		UIScrollableCanvas uiscrollableCanvas = _c as UIScrollableCanvas;
		if (uiscrollableCanvas != null)
		{
			num = Mathf.Max(num, uiscrollableCanvas.m_contentWidth);
			num2 = Mathf.Max(num2, uiscrollableCanvas.m_contentHeight);
			num *= 1.5f;
			num2 *= 1.5f;
		}
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_league_border", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, -num2 / 2f, -5f), 0f);
		SpriteS.SetDimensions(spriteC, num, 0.005f * (float)Screen.height);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, camera, true, null);
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x0013868C File Offset: 0x00136A8C
	public static void DashedTopBottom(UIComponent _c)
	{
		float num = _c.m_actualWidth;
		float num2 = _c.m_actualHeight;
		UIScrollableCanvas uiscrollableCanvas = _c as UIScrollableCanvas;
		if (uiscrollableCanvas != null)
		{
			num = Mathf.Max(num, uiscrollableCanvas.m_contentWidth);
			num2 = Mathf.Max(num2, uiscrollableCanvas.m_contentHeight);
			num *= 1.5f;
			num2 *= 1.5f;
		}
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_league_border", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(0f, -num2 / 2f, -5f), 0f);
		SpriteS.SetDimensions(spriteC, num, 0.005f * (float)Screen.height);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(0f, num2 / 2f, -5f), 0f);
		SpriteS.SetDimensions(spriteC2, num, 0.005f * (float)Screen.height);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, camera, true, null);
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x001387E4 File Offset: 0x00136BE4
	public static void DescriptionField(UIComponent _c)
	{
		float num = 0.078f;
		float num2 = 0.024f;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualWidth * num2, 8, Vector2.zero);
		Vector2[] array = new Vector2[roundedRect.Length + 2];
		Array.Copy(roundedRect, 0, array, 0, 16);
		array[16] = new Vector2(roundedRect[16].x, roundedRect[16].y - _c.m_actualWidth * num);
		array[17] = new Vector2(roundedRect[16].x - _c.m_actualWidth * num, roundedRect[16].y - _c.m_actualWidth * (num / 2f));
		Array.Copy(roundedRect, 16, array, 18, roundedRect.Length - 16);
		Color color = DebugDraw.HexToColor("#fffec6");
		uint num3 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(_c.m_actualWidth * 0.004f, _c.m_actualWidth * -0.004f, 0.3f), array, _c.m_actualWidth * 0.03f, DebugDraw.HexToColor("#666666"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num3, num3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, array, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
	}

	// Token: 0x06001BF4 RID: 7156 RVA: 0x001389B4 File Offset: 0x00136DB4
	public static void SpeechBubbleLeft(UIComponent _c)
	{
		float num = 0.1f;
		float num2 = 0.124f;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth + 0.05f * (float)Screen.height, _c.m_actualHeight + 0.05f * (float)Screen.height, _c.m_actualWidth * num2, 8, Vector2.zero);
		Vector2[] array = DebugDraw.AddSpeechHandleToVectorArray(roundedRect, SpeechBubbleHandlePosition.Left, SpeechBubbleHandleType.SmallToLeft);
		Array.Copy(roundedRect, 0, array, 0, 16);
		array[16] = new Vector2(roundedRect[15].x - _c.m_actualWidth * num, roundedRect[15].y + _c.m_actualWidth * num / 2f);
		array[17] = new Vector2(roundedRect[15].x, roundedRect[15].y + _c.m_actualWidth * num);
		Array.Copy(roundedRect, 16, array, 18, roundedRect.Length - 16);
		Color color = DebugDraw.HexToColor("#ffffff");
		Color color2 = DebugDraw.HexToColor("#000000");
		uint num3 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(_c.m_actualWidth * 0.004f, _c.m_actualWidth * -0.004f, 0.3f), array, _c.m_actualWidth * 0.065f, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(_c.m_actualWidth * 0.004f, _c.m_actualWidth * -0.004f, -0.3f), array, _c.m_actualWidth * 0.05f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num3, num3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x00138BBC File Offset: 0x00136FBC
	public static void SpeechBubbleRight(UIComponent _c)
	{
		float num = 0.1f;
		float num2 = 0.124f;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num3 = _c.m_actualWidth + 0.025f * (float)Screen.height;
		float num4 = _c.m_actualHeight + 0.025f * (float)Screen.height;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(num3, num4, num3 * num2, 8, Vector2.zero);
		Vector2[] array = new Vector2[roundedRect.Length + 2];
		Array.Copy(roundedRect, 0, array, 0, 32);
		array[32] = new Vector2(roundedRect[0].x, roundedRect[0].y + num3 * num);
		array[33] = new Vector2(roundedRect[0].x + num3 * num, roundedRect[0].y + num3 * num / 2f);
		array[34] = new Vector2(roundedRect[0].x, roundedRect[0].y);
		Color color = DebugDraw.HexToColor("#ffffff");
		Color color2 = DebugDraw.HexToColor("#000000");
		uint num5 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(0f, (float)Screen.height * -0.0025f, 2f), array, (float)Screen.height * 0.0125f, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, array, (float)Screen.height * 0.025f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num5, num5, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x00138DB8 File Offset: 0x001371B8
	public static void SpeechBubbleTopLeft(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.2f, 8, Vector2.zero);
		List<Vector2> list = Enumerable.ToList<Vector2>(array);
		Vector2 vector = array[23];
		list.Insert(24, vector + new Vector2(0.075f * _c.m_actualHeight, 0f));
		list.Insert(25, vector + new Vector2(0.225f * _c.m_actualHeight, 0.15f * _c.m_actualHeight));
		list.Insert(26, vector + new Vector2(0.375f * _c.m_actualHeight, 0f));
		array = list.ToArray();
		Color color = DebugDraw.HexToColor("#ffffff");
		Color color2 = DebugDraw.HexToColor("#000000");
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 2f, array, _c.m_actualWidth * 0.055f, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, array, _c.m_actualWidth * 0.05f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x00138F68 File Offset: 0x00137368
	public static void SpeechBubbleSpecialOffer(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.2f, 8, Vector2.zero);
		List<Vector2> list = Enumerable.ToList<Vector2>(array);
		Vector2 vector = array[23];
		list.Insert(24, vector + new Vector2(0.475f * _c.m_actualHeight, 0f));
		list.Insert(25, vector + new Vector2(0.675f * _c.m_actualHeight, 0.4f * _c.m_actualHeight));
		list.Insert(26, vector + new Vector2(0.875f * _c.m_actualHeight, 0f));
		array = list.ToArray();
		Color color = DebugDraw.HexToColor("#FED631");
		Color color2 = DebugDraw.HexToColor("#000000");
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, array, _c.m_actualWidth * 0.05f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BF8 RID: 7160 RVA: 0x001390DC File Offset: 0x001374DC
	public static void SpeechBubbleBottomLeft(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.2f, 8, Vector2.zero);
		List<Vector2> list = Enumerable.ToList<Vector2>(array);
		Vector2 vector = array[8];
		list.Insert(8, vector + new Vector2(0.075f * _c.m_actualHeight, 0f));
		list.Insert(8, vector + new Vector2(0.225f * _c.m_actualHeight, -0.15f * _c.m_actualHeight));
		list.Insert(8, vector + new Vector2(0.375f * _c.m_actualHeight, 0f));
		array = list.ToArray();
		Color color = DebugDraw.HexToColor("#ffffff");
		Color color2 = DebugDraw.HexToColor("#000000");
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 2f, array, _c.m_actualWidth * 0.055f, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, array, _c.m_actualWidth * 0.05f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BF9 RID: 7161 RVA: 0x00139288 File Offset: 0x00137688
	public static void RatingSpeechBubble(UIComponent _c)
	{
		float num = 0.1f;
		float num2 = 0.024f;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualWidth * num2, 8, Vector2.zero);
		Vector2[] array = new Vector2[roundedRect.Length + 2];
		Array.Copy(roundedRect, 0, array, 0, 16);
		array[16] = new Vector2(roundedRect[16].x, roundedRect[16].y - _c.m_actualWidth * num);
		array[17] = new Vector2(roundedRect[16].x - _c.m_actualWidth * num, roundedRect[16].y - _c.m_actualWidth * (num / 2f));
		Array.Copy(roundedRect, 16, array, 18, roundedRect.Length - 16);
		Color color = DebugDraw.HexToColor("#ffffff");
		uint num3 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(_c.m_actualWidth * 0.004f, _c.m_actualWidth * -0.004f, 0.3f), array, _c.m_actualWidth * 0.05f, DebugDraw.HexToColor("#00b6ea"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num3, num3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x00139428 File Offset: 0x00137828
	public static void CustomisationInfoBox(UIComponent _c)
	{
		float num = 0.05f;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualWidth * num, 8, Vector2.zero);
		Color color = DebugDraw.HexToColor("#000000");
		uint num2 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(0f, 0f, -0.3f), roundedRect, (float)Screen.height * 0.012f, DebugDraw.HexToColor("#6666ff"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, roundedRect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x0013950C File Offset: 0x0013790C
	public static void UpgradeInfoBox(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_upgrade_selection_background", null);
		float num = frame.width / 3f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num, frame.y, num, frame.height);
		Frame frame4 = new Frame(frame.x + num * 2f, frame.y, num, frame.height);
		float num2 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * (frame2.width / frame2.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.left * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame4, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.right * (_c.m_actualWidth - num3) / 2f, 0f);
		float num4 = _c.m_actualWidth - num2 - num3;
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC3, num4, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001BFC RID: 7164 RVA: 0x001396BC File Offset: 0x00137ABC
	public static void SpecialOfferBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_special_offer_box", null);
		float num = frame.width / 3f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num, frame.y, num, frame.height);
		Frame frame4 = new Frame(frame.x + num * 2f, frame.y, num, frame.height);
		float num2 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * (frame2.width / frame2.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.left * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame4, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.right * (_c.m_actualWidth - num3) / 2f, 0f);
		float num4 = _c.m_actualWidth - num2 - num3;
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC3, num4, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001BFD RID: 7165 RVA: 0x0013986C File Offset: 0x00137C6C
	public static void MenuTournamentNotificationBubble(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight * 0.4f, Vector2.zero);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		List<Vector2> list = Enumerable.ToList<Vector2>(array);
		Vector2 vector = array[2];
		list.Insert(3, vector + new Vector2(_c.m_actualWidth / 2f - 0.25f * _c.m_actualHeight, 0f));
		list.Insert(4, vector + new Vector2(_c.m_actualWidth / 2f, 0.3f * _c.m_actualHeight));
		list.Insert(5, vector + new Vector2(_c.m_actualWidth / 2f + 0.25f * _c.m_actualHeight, 0f));
		array = list.ToArray();
		Color color = DebugDraw.HexToColor("#000000");
		color.a = 0.7f;
		Color color2 = DebugDraw.HexToColor("#000000");
		color2.a = 0.3f;
		uint num = DebugDraw.ColorToUInt(color);
		uint num2 = DebugDraw.ColorToUInt(color2);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.up * (_c.m_actualHeight * 0.3f), array, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.back, rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x00139A28 File Offset: 0x00137E28
	public static void TournamentBannerCenter(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_top_banner_bg", null);
		float num = frame.width / 2f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num, frame.y, num - 1f, frame.height);
		Frame frame4 = new Frame(frame.x, frame.y, num, frame.height, true, false);
		float num2 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * (frame2.width / frame2.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.left * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame4, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.right * (_c.m_actualWidth - num3) / 2f, 0f);
		float num4 = _c.m_actualWidth - num2 - num3;
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC3, num4, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001BFF RID: 7167 RVA: 0x00139BD8 File Offset: 0x00137FD8
	public static void TournamentBannerRight(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_top_corner", null);
		float width = frame.width;
		Frame frame2 = new Frame(frame.x, frame.y, width, frame.height);
		float actualWidth = _c.m_actualWidth;
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, actualWidth, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C00 RID: 7168 RVA: 0x00139C68 File Offset: 0x00138068
	public static void TournamentBannerLeft(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_top_corner", null);
		frame.flipX = true;
		float width = frame.width;
		float num = 36f;
		Frame frame2 = new Frame(frame.x, frame.y, width, num);
		float actualWidth = _c.m_actualWidth;
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, actualWidth, _c.m_actualHeight * (num / frame.height));
		SpriteS.SetOffset(spriteC, Vector3.up * (_c.m_actualHeight - _c.m_actualHeight * (num / frame.height)) / 2f, 0f);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C01 RID: 7169 RVA: 0x00139D44 File Offset: 0x00138144
	public static void TournamentBannerLeftChat(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_top_corner", null);
		frame.flipX = true;
		float width = frame.width;
		Frame frame2 = new Frame(frame.x, frame.y, width, frame.height);
		float actualWidth = _c.m_actualWidth;
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, actualWidth, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C02 RID: 7170 RVA: 0x00139DDC File Offset: 0x001381DC
	public static void TournamentBannerRightPriceBanner(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_prize_banner", null);
		float num = frame.width / 2f;
		Frame frame2 = new Frame(frame.x + 1f, frame.y, num - 1f, frame.height);
		Frame frame3 = new Frame(frame.x + num, frame.y, num, frame.height);
		float num2 = _c.m_actualHeight * (frame3.width / frame3.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, num2, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.right * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualWidth - num2;
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.left * (_c.m_actualWidth - num3) / 2f, 0f);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C03 RID: 7171 RVA: 0x00139F28 File Offset: 0x00138328
	public static void TournamentFooterRight(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_bottom_corner_extension", null);
		float num = frame.width / 2f;
		Frame frame2 = new Frame(frame.x + num, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x, frame.y, num, frame.height);
		float num2 = _c.m_actualHeight * (frame3.width / frame3.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * (frame3.width / frame3.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.left * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualWidth - num2;
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.right * (_c.m_actualWidth - num3) / 2f, 0f);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x0013A07C File Offset: 0x0013847C
	public static void TournamentChatBGDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = (float)Screen.width * 0.005f;
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_border", null);
		frame.flipX = true;
		frame.y += 1f;
		frame.x += 1f;
		frame.height -= 2f;
		frame.width -= 2f;
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_bg", null);
		frame2.flipX = true;
		frame2.y += 1f;
		frame2.x += 1f;
		frame2.height -= 2f;
		frame2.width -= 2f;
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(_c.m_actualWidth / 2f - num / 2f, 0f, 2f), 0f);
		SpriteS.SetDimensions(spriteC, num, _c.m_actualHeight);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(0f, 0f, 0f), 0f);
		SpriteS.SetDimensions(spriteC2, _c.m_actualWidth, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C05 RID: 7173 RVA: 0x0013A218 File Offset: 0x00138618
	public static void TournamentLeaderboardBGDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_border", null);
		frame.y += 1f;
		frame.x += 1f;
		frame.height -= 2f;
		frame.width -= 2f;
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_bg", null);
		frame2.y += 1f;
		frame2.x += 1f;
		frame2.height -= 2f;
		frame2.width -= 2f;
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(-_c.m_actualWidth / 2f, 0f, 0f), 0f);
		SpriteS.SetDimensions(spriteC, (float)Screen.width * 0.005f, _c.m_actualHeight);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(0f, 0f, 0f), 0f);
		SpriteS.SetDimensions(spriteC2, _c.m_actualWidth, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C06 RID: 7174 RVA: 0x0013A39C File Offset: 0x0013879C
	public static void TopLabelBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_garage_header", null);
		float num = frame.width / 3f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num, frame.y, num, frame.height);
		Frame frame4 = new Frame(frame.x + num * 2f, frame.y, num, frame.height);
		float num2 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetColor(spriteC, Color.grey);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * (frame2.width / frame2.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.left * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame4, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.right * (_c.m_actualWidth - num3) / 2f, 0f);
		float num4 = _c.m_actualWidth - num2 - num3;
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetColor(spriteC3, Color.grey);
		SpriteS.SetDimensions(spriteC3, num4, _c.m_actualHeight);
		List<PrefabC> list = SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C07 RID: 7175 RVA: 0x0013A570 File Offset: 0x00138970
	public static void UpgradeInfoBoxBackBike(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_upgrade_selection_background_backside_bike", null);
		PsUIDrawHandlers.UpgradeInfoBoxBack(_c, frame);
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x0013A5AC File Offset: 0x001389AC
	public static void UpgradeInfoBoxBackCar(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_upgrade_selection_background_backside_car", null);
		PsUIDrawHandlers.UpgradeInfoBoxBack(_c, frame);
	}

	// Token: 0x06001C09 RID: 7177 RVA: 0x0013A5E8 File Offset: 0x001389E8
	public static void UpgradeInfoBoxBack(UIComponent _c, Frame _frame)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = _frame.width / 3f;
		Frame frame = new Frame(_frame.x, _frame.y, num, _frame.height);
		Frame frame2 = new Frame(_frame.x + num, _frame.y, num, _frame.height);
		Frame frame3 = new Frame(_frame.x + num * 2f, _frame.y, num, _frame.height);
		float num2 = _c.m_actualHeight * (frame.width / frame.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * (frame.width / frame.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.left * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualHeight * (frame.width / frame.height);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.right * (_c.m_actualWidth - num3) / 2f, 0f);
		float num4 = _c.m_actualWidth - num2 - num3;
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC3, num4, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C0A RID: 7178 RVA: 0x0013A780 File Offset: 0x00138B80
	public static void NotificationDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] circle = DebugDraw.GetCircle(_c.m_actualHeight / 2f, 30, Vector2.zero, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, circle, (float)Screen.height * 0.0075f, Color.white, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(circle);
		Color color = DebugDraw.HexToColor("#cc0909");
		Color color2 = DebugDraw.HexToColor("#FF0C0C");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001C0B RID: 7179 RVA: 0x0013A840 File Offset: 0x00138C40
	public static void EditorItemSelectorDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Texture texture = ResourceManager.GetTexture(RESOURCE.menu_editor_tab_bg_Texture2D);
		Material material = new Material(ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material));
		material.mainTexture = texture;
		float num = _c.m_actualWidth / ((float)texture.width * (_c.m_actualHeight / (float)texture.height));
		material.mainTextureScale = new Vector2(num, 1f);
		PrefabS.CreateRect(_c.m_TC, Vector3.zero, _c.m_actualWidth, _c.m_actualHeight, Color.white, material, _c.m_camera);
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x0013A8D8 File Offset: 0x00138CD8
	public static void StartDarkDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = (float)Mathf.RoundToInt(_c.m_margins.r / 0.03f) * 0.03f * (float)Screen.height;
		float num2 = _c.m_actualWidth * 0.9f;
		float actualHeight = _c.m_actualHeight;
		float num3 = _c.m_actualHeight * 0.04f;
		Vector2 zero = Vector2.zero;
		Vector2[] array = new Vector2[41];
		Vector2[] arc = DebugDraw.GetArc(num3, 10, 85f, 255f, new Vector2(num2 * 0.5f - num3, actualHeight * -0.5f + num3) + zero);
		Vector2[] arc2 = DebugDraw.GetArc(num3, 10, 60f, 180f, new Vector2(num2 * -0.5f + num3, actualHeight * -0.5f + num3) + zero);
		Vector2[] arc3 = DebugDraw.GetArc(num3, 10, 85f, 75f, new Vector2(num2 * -0.45f + num3, actualHeight * 0.5f - num3) + zero);
		Vector2[] arc4 = DebugDraw.GetArc(num3, 10, 60f, 0f, new Vector2(num2 * 0.55f - num3, actualHeight * 0.5f - num3) + zero);
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		arc3.CopyTo(array, 20);
		arc4.CopyTo(array, 30);
		array[array.Length - 1] = arc[0];
		Color color = DebugDraw.HexToColor("#000000");
		color.a = 0.95f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f + new Vector3(-num, 0f, 0f), array, (float)Screen.height * 0.0075f, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 5f + new Vector3(-num, 0f, 0f), ggdata, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x0013AB1C File Offset: 0x00138F1C
	public static void StartFadedDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = (float)Mathf.RoundToInt(_c.m_margins.r / 0.03f) * 0.03f * (float)Screen.height;
		float num2 = _c.m_actualWidth * 0.9f;
		float actualHeight = _c.m_actualHeight;
		float num3 = _c.m_actualHeight * 0.04f;
		Vector2 zero = Vector2.zero;
		Vector2[] array = new Vector2[41];
		Vector2[] arc = DebugDraw.GetArc(num3, 10, 85f, 255f, new Vector2(num2 * 0.5f - num3, actualHeight * -0.5f + num3) + zero);
		Vector2[] arc2 = DebugDraw.GetArc(num3, 10, 60f, 180f, new Vector2(num2 * -0.5f + num3, actualHeight * -0.5f + num3) + zero);
		Vector2[] arc3 = DebugDraw.GetArc(num3, 10, 85f, 75f, new Vector2(num2 * -0.45f + num3, actualHeight * 0.5f - num3) + zero);
		Vector2[] arc4 = DebugDraw.GetArc(num3, 10, 60f, 0f, new Vector2(num2 * 0.55f - num3, actualHeight * 0.5f - num3) + zero);
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		arc3.CopyTo(array, 20);
		arc4.CopyTo(array, 30);
		array[array.Length - 1] = arc[0];
		Color color = DebugDraw.HexToColor("#000000");
		color.a = 0.55f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f + new Vector3(-num, 0f, 0f), array, (float)Screen.height * 0.0075f, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 5f + new Vector3(-num, 0f, 0f), ggdata, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x0013AD60 File Offset: 0x00139160
	public static void TiltedDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.625f * (float)Screen.height * 0.02f;
		float num2 = (float)Mathf.RoundToInt(_c.m_margins.r / 0.02f) * 0.02f * (float)Screen.height;
		float num3 = 0.625f * (float)Screen.height;
		float actualHeight = _c.m_actualHeight;
		float num4 = _c.m_actualHeight * 0.04f;
		Vector2 zero = Vector2.zero;
		Vector2[] array = new Vector2[41];
		Vector2[] arc = DebugDraw.GetArc(num4, 10, 85f, 255f, new Vector2(num3 * 0.5f - num4, actualHeight * -0.5f + num4) + zero);
		Vector2[] arc2 = DebugDraw.GetArc(num4, 10, 60f, 180f, new Vector2(num3 * -0.5f + num4, actualHeight * -0.5f + num4) + zero);
		Vector2[] arc3 = DebugDraw.GetArc(num4, 10, 85f, 75f, new Vector2(num3 * -0.5f + num4 + num, actualHeight * 0.5f - num4) + zero);
		Vector2[] arc4 = DebugDraw.GetArc(num4, 10, 60f, 0f, new Vector2(num3 * 0.5f - num4 + num, actualHeight * 0.5f - num4) + zero);
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		arc3.CopyTo(array, 20);
		arc4.CopyTo(array, 30);
		array[array.Length - 1] = arc[0];
		Color color = DebugDraw.HexToColor("#000000");
		color.a = 0.65f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f + new Vector3(-num2, 0f, 0f), array, (float)Screen.height * 0.0075f, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 5f + new Vector3(-num2, 0f, 0f), ggdata, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x0013AFC8 File Offset: 0x001393C8
	public static void TiltedFadedDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.625f * (float)Screen.height * 0.02f;
		float num2 = (float)Mathf.RoundToInt(_c.m_margins.r / 0.02f) * 0.02f * (float)Screen.height;
		float num3 = 0.625f * (float)Screen.height;
		float actualHeight = _c.m_actualHeight;
		float num4 = _c.m_actualHeight * 0.04f;
		Vector2 zero = Vector2.zero;
		Vector2[] array = new Vector2[41];
		Vector2[] arc = DebugDraw.GetArc(num4, 10, 85f, 255f, new Vector2(num3 * 0.5f - num4, actualHeight * -0.5f + num4) + zero);
		Vector2[] arc2 = DebugDraw.GetArc(num4, 10, 60f, 180f, new Vector2(num3 * -0.5f + num4, actualHeight * -0.5f + num4) + zero);
		Vector2[] arc3 = DebugDraw.GetArc(num4, 10, 85f, 75f, new Vector2(num3 * -0.5f + num4 + num, actualHeight * 0.5f - num4) + zero);
		Vector2[] arc4 = DebugDraw.GetArc(num4, 10, 60f, 0f, new Vector2(num3 * 0.5f - num4 + num, actualHeight * 0.5f - num4) + zero);
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		arc3.CopyTo(array, 20);
		arc4.CopyTo(array, 30);
		array[array.Length - 1] = arc[0];
		Color color = DebugDraw.HexToColor("#000000");
		color.a = 0.55f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f + new Vector3(-num2, 0f, 0f), array, (float)Screen.height * 0.0075f, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 5f + new Vector3(-num2, 0f, 0f), ggdata, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x0013B230 File Offset: 0x00139630
	public static void TiltedExtendedDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.0325f * (float)Screen.height;
		float num2 = 0.69f * (float)Screen.height;
		float actualHeight = _c.m_actualHeight;
		float num3 = _c.m_actualHeight * 0.04f;
		Vector2 zero = Vector2.zero;
		Vector2[] array = new Vector2[41];
		Vector2[] arc = DebugDraw.GetArc(num3, 10, 85f, 255f, new Vector2(num2 * 0.5f - num3, actualHeight * -0.5f + num3) + zero);
		Vector2[] arc2 = DebugDraw.GetArc(num3, 10, 60f, 180f, new Vector2(num2 * -0.5f + num3, actualHeight * -0.5f + num3) + zero);
		Vector2[] arc3 = DebugDraw.GetArc(num3, 10, 85f, 75f, new Vector2(num2 * -0.45f + num3, actualHeight * 0.5f - num3) + zero);
		Vector2[] arc4 = DebugDraw.GetArc(num3, 10, 60f, 0f, new Vector2(num2 * 0.55f - num3, actualHeight * 0.5f - num3) + zero);
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		arc3.CopyTo(array, 20);
		arc4.CopyTo(array, 30);
		array[array.Length - 1] = arc[0];
		Color color = DebugDraw.HexToColor("#000000");
		color.a = 0.95f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f + new Vector3(num, 0f, 0f), array, (float)Screen.height * 0.0075f, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 5f + new Vector3(num, 0f, 0f), ggdata, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x0013B45C File Offset: 0x0013985C
	public static void TiltedBlueDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.625f * (float)Screen.height * 0.02f;
		float num2 = (float)Mathf.RoundToInt(_c.m_margins.r / 0.02f) * 0.02f * (float)Screen.height;
		float num3 = 0.625f * (float)Screen.height;
		float actualHeight = _c.m_actualHeight;
		float num4 = _c.m_actualHeight * 0.04f;
		Vector2 zero = Vector2.zero;
		Vector2[] array = new Vector2[41];
		Vector2[] arc = DebugDraw.GetArc(num4, 10, 85f, 255f, new Vector2(num3 * 0.5f - num4, actualHeight * -0.5f + num4) + zero);
		Vector2[] arc2 = DebugDraw.GetArc(num4, 10, 60f, 180f, new Vector2(num3 * -0.5f + num4, actualHeight * -0.5f + num4) + zero);
		Vector2[] arc3 = DebugDraw.GetArc(num4, 10, 85f, 75f, new Vector2(num3 * -0.5f + num4 + num, actualHeight * 0.5f - num4) + zero);
		Vector2[] arc4 = DebugDraw.GetArc(num4, 10, 60f, 0f, new Vector2(num3 * 0.5f - num4 + num, actualHeight * 0.5f - num4) + zero);
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		arc3.CopyTo(array, 20);
		arc4.CopyTo(array, 30);
		array[array.Length - 1] = arc[0];
		Color color = DebugDraw.HexToColor("#0a4aa0");
		Color color2 = DebugDraw.HexToColor("#0f6ff8");
		Color color3 = DebugDraw.HexToColor("#3891ff");
		Color black = Color.black;
		black.a = 0.75f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f + new Vector3(-num2, 0f, 0f), array, (float)Screen.height * 0.0075f, color3, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 7f + Vector3.down * 0.005f * (float)Screen.height + new Vector3(-num2, 0f, 0f), array, (float)Screen.height * 0.015f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 5f + new Vector3(-num2, 0f, 0f), ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x0013B758 File Offset: 0x00139B58
	public static void EmptySpaceRectDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		int num = Mathf.CeilToInt(_c.m_actualWidth / (0.0125f * (float)Screen.height));
		if (num % 2 != 0)
		{
			num++;
		}
		int num2 = Mathf.CeilToInt(_c.m_actualHeight / (0.0125f * (float)Screen.height));
		if (num2 % 2 != 0)
		{
			num2++;
		}
		List<Vector2[]> list = new List<Vector2[]>();
		list.Add(DebugDraw.GetLine(rect[0], rect[1], num));
		list.Add(DebugDraw.GetLine(rect[1], rect[2], num2));
		list.Add(DebugDraw.GetLine(rect[2], rect[3], num));
		list.Add(DebugDraw.GetLine(rect[3], rect[0], num2));
		Color color = DebugDraw.HexToColor("f4f4f4");
		color.a = 0.4f;
		for (int i = 0; i < list.Count; i++)
		{
			for (int j = 0; j < list[i].Length; j++)
			{
				if (j % 2 != 0 && j < list[i].Length - 1)
				{
					Vector2[] array = new Vector2[]
					{
						list[i][j],
						list[i][j + 1]
					};
					PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, array, 0.0125f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, false);
				}
			}
		}
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x0013B958 File Offset: 0x00139D58
	public static void LightShadowBackground(UIComponent _c)
	{
		float num = _c.m_actualWidth;
		float num2 = _c.m_actualHeight;
		UIScrollableCanvas uiscrollableCanvas = _c as UIScrollableCanvas;
		if (uiscrollableCanvas != null)
		{
			num = Mathf.Max(num, uiscrollableCanvas.m_contentWidth);
			num2 = Mathf.Max(num2, uiscrollableCanvas.m_contentHeight);
			num *= 1.5f;
			num2 *= 1.5f;
		}
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, num2 * 0.25f), false);
		Vector2[] rect2 = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, -num2 * 0.25f), false);
		uint num3 = DebugDraw.ColorToUInt(DebugDraw.GetColor(0f, 0f, 0f, 50f));
		uint num4 = DebugDraw.ColorToUInt(DebugDraw.GetColor(0f, 0f, 0f, 50f));
		uint num5 = DebugDraw.ColorToUInt(DebugDraw.GetColor(0f, 0f, 0f, 50f));
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect, num4, num3, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect2, num5, num4, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x0013BB34 File Offset: 0x00139F34
	public static void ProfileCardBackgroundDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.01f * (float)Screen.height, 6, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#35A1FF");
		Color color2 = DebugDraw.HexToColor("#0F3EA1");
		Color color3 = DebugDraw.HexToColor("#1055C4");
		Color color4 = DebugDraw.HexToColor("#0F2158");
		Color black = Color.black;
		black.a = 0.8f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, roundedRect, 0.01f * (float)Screen.height, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * -1f, ggdata, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.down * 0.0035f * (float)Screen.height, roundedRect, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x0013BC74 File Offset: 0x0013A074
	public static void ProfileBannerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.01f * (float)Screen.height, 6, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth - 0.05f * _c.m_actualHeight, _c.m_actualHeight * 0.15f, _c.m_actualHeight * 0.075f, 6, Vector2.up * _c.m_actualHeight * 0.42f);
		GGData ggdata = new GGData(roundedRect);
		GGData ggdata2 = new GGData(roundedRect2);
		Color color = DebugDraw.HexToColor("#64DEFD");
		Color color2 = DebugDraw.HexToColor("#1B58A2");
		Color color3 = DebugDraw.HexToColor("#64DEFD");
		Color color4 = DebugDraw.HexToColor("#1B58A2");
		Color black = Color.black;
		black.a = 0.8f;
		Color white = Color.white;
		white.a = 0.35f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f, roundedRect, 0.01f * (float)Screen.height, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 4f, ggdata, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 5f + Vector3.down * 0.0035f * (float)Screen.height, roundedRect, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata2, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_shine", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(-_c.m_actualWidth * 0.5f + 0.125f * _c.m_actualHeight, _c.m_actualHeight * 0.42f, 1f), 40f);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * 0.2f, _c.m_actualHeight * 0.135f);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(_c.m_actualWidth * 0.5f - 0.125f * _c.m_actualHeight, -_c.m_actualHeight * 0.4f, 1f), 205f);
		SpriteS.SetDimensions(spriteC2, _c.m_actualHeight * 0.15f, _c.m_actualHeight * 0.1f);
		SpriteS.SetColor(spriteC2, new Color(1f, 1f, 1f, 0.6f));
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x0013BF88 File Offset: 0x0013A388
	public static void CreatorChallengeBannerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = _c.m_actualHeight * 0.85f;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, num, 0.01f * (float)Screen.height, 6, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth - 0.05f * _c.m_actualHeight, num * 0.15f, num * 0.075f, 6, Vector2.up * num * 0.42f);
		GGData ggdata = new GGData(roundedRect);
		GGData ggdata2 = new GGData(roundedRect2);
		Color color = DebugDraw.HexToColor("#1FBEFC");
		Color color2 = DebugDraw.HexToColor("#10A6FE");
		Color color3 = DebugDraw.HexToColor("#3BB8EA");
		Color color4 = DebugDraw.HexToColor("#0494F5");
		Color black = Color.black;
		black.a = 0.8f;
		Color white = Color.white;
		white.a = 0.35f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f, roundedRect, 0.01f * (float)Screen.height, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 4f, ggdata, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 5f + Vector3.down * 0.0035f * (float)Screen.height, roundedRect, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata2, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_shine", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(-_c.m_actualWidth * 0.5f + 0.125f * _c.m_actualHeight, num * 0.42f, 1f), 40f);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * 0.2f, _c.m_actualHeight * 0.135f);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(_c.m_actualWidth * 0.5f - 0.125f * _c.m_actualHeight, -num * 0.4f, 1f), 205f);
		SpriteS.SetDimensions(spriteC2, _c.m_actualHeight * 0.15f, _c.m_actualHeight * 0.1f);
		SpriteS.SetColor(spriteC2, new Color(1f, 1f, 1f, 0.6f));
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x0013C290 File Offset: 0x0013A690
	public static void CreatorChallengeActiveBannerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = _c.m_actualHeight * 0.85f;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, num, 0.01f * (float)Screen.height, 6, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth - 0.05f * _c.m_actualHeight, num * 0.15f, num * 0.075f, 6, Vector2.up * num * 0.42f);
		GGData ggdata = new GGData(roundedRect);
		GGData ggdata2 = new GGData(roundedRect2);
		Color color = DebugDraw.HexToColor("#97E529");
		Color color2 = DebugDraw.HexToColor("#4BB82F");
		Color color3 = DebugDraw.HexToColor("#95DC3D");
		Color color4 = DebugDraw.HexToColor("#48B52A");
		Color black = Color.black;
		black.a = 0.8f;
		Color white = Color.white;
		white.a = 0.35f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f, roundedRect, 0.01f * (float)Screen.height, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 4f, ggdata, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 5f + Vector3.down * 0.0035f * (float)Screen.height, roundedRect, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata2, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_shine", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(-_c.m_actualWidth * 0.5f + 0.125f * _c.m_actualHeight, num * 0.42f, 1f), 40f);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * 0.2f, _c.m_actualHeight * 0.135f);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(_c.m_actualWidth * 0.5f - 0.125f * _c.m_actualHeight, -num * 0.4f, 1f), 205f);
		SpriteS.SetDimensions(spriteC2, _c.m_actualHeight * 0.15f, _c.m_actualHeight * 0.1f);
		SpriteS.SetColor(spriteC2, new Color(1f, 1f, 1f, 0.6f));
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x0013C598 File Offset: 0x0013A998
	public static void ProfileBannerInactiveDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.01f * (float)Screen.height, 6, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth - 0.05f * _c.m_actualHeight, _c.m_actualHeight * 0.15f, _c.m_actualHeight * 0.075f, 6, Vector2.up * _c.m_actualHeight * 0.42f);
		GGData ggdata = new GGData(roundedRect);
		GGData ggdata2 = new GGData(roundedRect2);
		Color color = DebugDraw.HexToColor("#5D6C72");
		Color color2 = DebugDraw.HexToColor("#575E65");
		Color color3 = DebugDraw.HexToColor("#5D6C72");
		Color color4 = DebugDraw.HexToColor("#575E65");
		Color black = Color.black;
		black.a = 0.8f;
		Color white = Color.white;
		white.a = 0.35f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f, roundedRect, 0.01f * (float)Screen.height, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 4f, ggdata, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 5f + Vector3.down * 0.0035f * (float)Screen.height, roundedRect, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata2, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_shine", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(-_c.m_actualWidth * 0.5f + 0.125f * _c.m_actualHeight, _c.m_actualHeight * 0.42f, 1f), 40f);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * 0.2f, _c.m_actualHeight * 0.135f);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(_c.m_actualWidth * 0.5f - 0.125f * _c.m_actualHeight, -_c.m_actualHeight * 0.4f, 1f), 205f);
		SpriteS.SetDimensions(spriteC2, _c.m_actualHeight * 0.15f, _c.m_actualHeight * 0.1f);
		SpriteS.SetColor(spriteC2, new Color(1f, 1f, 1f, 0.6f));
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x0013C8AC File Offset: 0x0013ACAC
	public static void OwnProfileBannerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.01f * (float)Screen.height, 6, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth - 0.05f * _c.m_actualHeight, _c.m_actualHeight * 0.15f, _c.m_actualHeight * 0.075f, 6, Vector2.up * _c.m_actualHeight * 0.42f);
		GGData ggdata = new GGData(roundedRect);
		GGData ggdata2 = new GGData(roundedRect2);
		Color color = DebugDraw.HexToColor("#56FB1A");
		Color color2 = DebugDraw.HexToColor("#1DA823");
		Color color3 = DebugDraw.HexToColor("#56FB1A");
		Color color4 = DebugDraw.HexToColor("#1DA823");
		Color black = Color.black;
		black.a = 0.8f;
		Color white = Color.white;
		white.a = 0.35f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f, roundedRect, 0.01f * (float)Screen.height, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 4f, ggdata, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 5f + Vector3.down * 0.0035f * (float)Screen.height, roundedRect, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata2, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_shine", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(-_c.m_actualWidth * 0.5f + 0.125f * _c.m_actualHeight, _c.m_actualHeight * 0.42f, 1f), 40f);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * 0.2f, _c.m_actualHeight * 0.135f);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(_c.m_actualWidth * 0.5f - 0.125f * _c.m_actualHeight, -_c.m_actualHeight * 0.4f, 1f), 205f);
		SpriteS.SetDimensions(spriteC2, _c.m_actualHeight * 0.15f, _c.m_actualHeight * 0.1f);
		SpriteS.SetColor(spriteC2, new Color(1f, 1f, 1f, 0.6f));
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x0013CBC0 File Offset: 0x0013AFC0
	public static void LevelListBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = (float)Screen.height * 2f;
		if (_c.m_actualHeight > (float)Screen.height)
		{
			num = _c.m_actualHeight * 2f;
		}
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, num, Vector2.zero);
		Color black = Color.black;
		black.a = 0.45f;
		uint num2 = DebugDraw.ColorToUInt(black);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x0013CC60 File Offset: 0x0013B060
	public static void ProfileLevelsBackground(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Color black = Color.black;
		black.a = 0.45f;
		uint num = DebugDraw.ColorToUInt(black);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x0013CCD8 File Offset: 0x0013B0D8
	public static void SeasonTopDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.1f),
			new Vector2(_c.m_actualWidth * -0.375f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.375f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.1f)
		};
		GGData ggdata = new GGData(array);
		Color color = DebugDraw.HexToColor("#FD8008");
		Color color2 = DebugDraw.HexToColor("#FDA115");
		Color color3 = DebugDraw.HexToColor("#FED65D");
		Color color4 = DebugDraw.HexToColor("#C6640E");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, array, 0.0085f * (float)Screen.height, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x0013CE8C File Offset: 0x0013B28C
	public static void SeasonBottomDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.4f),
			new Vector2(_c.m_actualWidth * 0.375f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.375f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.4f)
		};
		GGData ggdata = new GGData(array);
		Color color = DebugDraw.HexToColor("#495F60");
		Color color2 = DebugDraw.HexToColor("#848483");
		Color color3 = DebugDraw.HexToColor("#808E81");
		Color color4 = DebugDraw.HexToColor("#5C5249");
		Color black = Color.black;
		black.a = 0.8f;
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, array, 0.0085f * (float)Screen.height, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward + Vector3.down * 0.01f * (float)Screen.height, array, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x0013D0A4 File Offset: 0x0013B4A4
	public static void SeasonTopRewardDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.35f),
			new Vector2(_c.m_actualWidth * -0.25f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.25f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.35f)
		};
		GGData ggdata = new GGData(array);
		Color color = DebugDraw.HexToColor("#FD8008");
		Color color2 = DebugDraw.HexToColor("#FDA115");
		Color color3 = DebugDraw.HexToColor("#FED65D");
		Color color4 = DebugDraw.HexToColor("#C6640E");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, array, 0.008f * (float)Screen.height, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x0013D258 File Offset: 0x0013B658
	public static void SeasonBottomRewardDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f + (float)Screen.height * 0.04f),
			new Vector2(_c.m_actualWidth * 0.18f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.18f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f + (float)Screen.height * 0.04f)
		};
		GGData ggdata = new GGData(array);
		Color color = DebugDraw.HexToColor("#495F60");
		Color color2 = DebugDraw.HexToColor("#848483");
		Color color3 = DebugDraw.HexToColor("#808E81");
		Color color4 = DebugDraw.HexToColor("#5C5249");
		Color black = Color.black;
		black.a = 0.8f;
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, array, 0.0085f * (float)Screen.height, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward + Vector3.down * 0.01f * (float)Screen.height, array, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001C20 RID: 7200 RVA: 0x0013D48C File Offset: 0x0013B88C
	public static void DarkBlueBGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.0125f * (float)Screen.height, 8, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#173D5B");
		Color color2 = DebugDraw.HexToColor("#0C254A");
		Color color3 = DebugDraw.HexToColor("#88E1FF");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001C21 RID: 7201 RVA: 0x0013D558 File Offset: 0x0013B958
	public static void ComponentDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.0125f * (float)Screen.height, 8, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(roundedRect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001C22 RID: 7202 RVA: 0x0013D5D8 File Offset: 0x0013B9D8
	public static void StickerDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.00625f * (float)Screen.height, 8, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#FFFFFF");
		Color color2 = DebugDraw.HexToColor("#FFFFFF");
		Color color3 = DebugDraw.HexToColor("#000000");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}
}
