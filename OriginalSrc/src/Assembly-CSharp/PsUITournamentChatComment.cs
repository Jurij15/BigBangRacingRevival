using System;
using UnityEngine;

// Token: 0x0200037F RID: 895
public class PsUITournamentChatComment : PsUITeamChatComment
{
	// Token: 0x060019F0 RID: 6640 RVA: 0x0011DB49 File Offset: 0x0011BF49
	public PsUITournamentChatComment(UIComponent _parent, CommentData _data)
		: base(_parent, _data)
	{
	}

	// Token: 0x060019F1 RID: 6641 RVA: 0x0011DB54 File Offset: 0x0011BF54
	public override void CreateComment(bool _right = true)
	{
		if (this.m_comment.playerId == PsMetagameManager.m_activeTournament.tournament.ownerId)
		{
			this.m_tournamentHost = true;
		}
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetSpacing(0f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		string text = "#1B405A";
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.275f, RelativeTo.ScreenWidth);
		uicanvas.SetHeight(0.045f, RelativeTo.ScreenHeight);
		uicanvas.SetMargins(0.025f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetDepthOffset(3f);
		this.m_nameArea = new UICanvas(uicanvas, true, string.Empty, null, string.Empty);
		this.m_nameArea.SetWidth(0.8f, RelativeTo.ParentWidth);
		this.m_nameArea.SetHeight(0.045f, RelativeTo.ScreenHeight);
		this.m_nameArea.SetMargins(0.015f, 0.015f, 0.005f, 0.005f, RelativeTo.ScreenHeight);
		this.m_nameArea.SetHorizontalAlign(0f);
		this.m_nameArea.m_TAC.m_letTouchesThrough = true;
		if (this.m_comment.playerId == PlayerPrefsX.GetUserId())
		{
			this.m_nameArea.SetDrawHandler(new UIDrawDelegate(this.OwnNameDrawhandler));
		}
		else
		{
			this.m_nameArea.SetDrawHandler(new UIDrawDelegate(this.NameAreaDrawhandler));
		}
		string text2 = ((!PsMetagameManager.IsFriend(this.m_comment.playerId)) ? "ffffff" : "#ABFF2A");
		string text3 = "#011532";
		if (this.m_tournamentHost)
		{
			text = "#312823";
		}
		UIFittedText uifittedText = new UIFittedText(this.m_nameArea, false, string.Empty, this.m_comment.name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, text2, text3);
		uifittedText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		uifittedText.m_shadowtmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UIVerticalList uiverticalList2 = uiverticalList;
		bool flag = false;
		string empty = string.Empty;
		string comment = this.m_comment.comment;
		string font = PsFontManager.GetFont(PsFonts.KGSecondChances);
		float num = 0.0275f;
		RelativeTo relativeTo = RelativeTo.ScreenHeight;
		bool flag2 = false;
		string text4 = text;
		UITextbox uitextbox = new UITextbox(uiverticalList2, flag, empty, comment, font, num, relativeTo, flag2, Align.Left, Align.Top, text4, true, null);
		uitextbox.SetMargins(0.01f, RelativeTo.ScreenHeight);
		uitextbox.SetMinRows(2);
		uitextbox.SetMaxRows(4);
		uitextbox.SetWidth(0.85f, RelativeTo.ParentWidth);
		uitextbox.SetHorizontalAlign(0f);
		uitextbox.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		if (_right)
		{
			uitextbox.SetDrawHandler(new UIDrawDelegate(this.RightCommentDrawhandler));
		}
		else
		{
			uitextbox.SetDrawHandler(new UIDrawDelegate(this.LeftCommentDrawhandler));
		}
	}

	// Token: 0x060019F2 RID: 6642 RVA: 0x0011DE58 File Offset: 0x0011C258
	protected new void NameAreaDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.2f, 8, Vector2.zero);
		int num = 0;
		for (int i = num; i < 6 + num; i++)
		{
			roundedRect[i] = new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f);
		}
		num = 8;
		for (int j = num; j < 8 + num; j++)
		{
			roundedRect[j] = new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f);
		}
		GGData ggdata = new GGData(roundedRect);
		Color color2;
		Color color;
		if (this.m_tournamentHost)
		{
			color = (color2 = DebugDraw.HexToColor("#C5591B"));
		}
		else
		{
			color = DebugDraw.HexToColor("#1165A7");
			color2 = DebugDraw.HexToColor("#5893c1");
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060019F3 RID: 6643 RVA: 0x0011DFBC File Offset: 0x0011C3BC
	protected new void OwnNameDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.2f, 8, Vector2.zero);
		int num = 0;
		for (int i = num; i < 6 + num; i++)
		{
			roundedRect[i] = new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f);
		}
		num = 8;
		for (int j = num; j < 8 + num; j++)
		{
			roundedRect[j] = new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f);
		}
		GGData ggdata = new GGData(roundedRect);
		Color color2;
		Color color;
		if (this.m_tournamentHost)
		{
			color = (color2 = DebugDraw.HexToColor("#C5591B"));
		}
		else
		{
			color2 = DebugDraw.HexToColor("#84FF38");
			color = DebugDraw.HexToColor("#1CA71C");
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060019F4 RID: 6644 RVA: 0x0011E120 File Offset: 0x0011C520
	protected new void RightCommentDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.015f * (float)Screen.height;
		Vector2[] array = new Vector2[27];
		Vector2[] arc = DebugDraw.GetArc(num, 8, 90f, 270f, new Vector2(_c.m_actualWidth * 0.5f - num, _c.m_actualHeight * -0.5f + num) + Vector2.zero);
		Vector2[] arc2 = DebugDraw.GetArc(num, 8, 90f, 180f, new Vector2(_c.m_actualWidth * -0.5f + num, _c.m_actualHeight * -0.5f + num) + Vector2.zero);
		Vector2[] arc3 = DebugDraw.GetArc(num, 8, 90f, 0f, new Vector2(_c.m_actualWidth * 0.5f - num, _c.m_actualHeight * 0.5f - num) + Vector2.zero);
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 8);
		array[16] = new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.5f - (float)Screen.height * 0.0175f);
		array[17] = new Vector2(_c.m_actualWidth * -0.535f, _c.m_actualHeight * 0.5f);
		arc3.CopyTo(array, 18);
		array[array.Length - 1] = array[0];
		GGData ggdata = new GGData(array);
		Color color;
		if (this.m_tournamentHost)
		{
			color = DebugDraw.HexToColor("#FD9727");
		}
		else
		{
			color = DebugDraw.HexToColor("#ffffff");
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, array, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060019F5 RID: 6645 RVA: 0x0011E334 File Offset: 0x0011C734
	protected new void LeftCommentDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.015f * (float)Screen.height;
		Vector2[] array = new Vector2[27];
		Vector2[] arc = DebugDraw.GetArc(num, 8, 90f, 180f, new Vector2(_c.m_actualWidth * -0.5f + num, _c.m_actualHeight * -0.5f + num) + Vector2.zero);
		Vector2[] arc2 = DebugDraw.GetArc(num, 8, 90f, 90f, new Vector2(_c.m_actualWidth * -0.5f + num, _c.m_actualHeight * 0.5f - num) + Vector2.zero);
		Vector2[] arc3 = DebugDraw.GetArc(num, 8, 90f, 0f, new Vector2(_c.m_actualWidth * 0.5f - num, _c.m_actualHeight * 0.5f - num) + Vector2.zero);
		array[0] = new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f + (float)Screen.height * 0.0175f);
		array[1] = new Vector2(_c.m_actualWidth * 0.535f, _c.m_actualHeight * -0.5f);
		arc.CopyTo(array, 2);
		arc2.CopyTo(array, 10);
		arc3.CopyTo(array, 18);
		array[array.Length - 1] = array[0];
		GGData ggdata = new GGData(array);
		Color color;
		if (this.m_tournamentHost)
		{
			color = DebugDraw.HexToColor("#FD9727");
		}
		else
		{
			color = DebugDraw.HexToColor("#ffffff");
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, array, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x04001C4C RID: 7244
	private bool m_tournamentHost;
}
