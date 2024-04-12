using System;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class PsUITeamChatComment : UIHorizontalList
{
	// Token: 0x060019E4 RID: 6628 RVA: 0x0011CC10 File Offset: 0x0011B010
	public PsUITeamChatComment(UIComponent _parent, CommentData _data)
		: this(_parent, _data, false)
	{
	}

	// Token: 0x060019E5 RID: 6629 RVA: 0x0011CC1C File Offset: 0x0011B01C
	public PsUITeamChatComment(UIComponent _parent, CommentData _data, bool _showTeam)
		: base(_parent, "ChatComment")
	{
		this.m_comment = _data;
		this.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		if (this.m_comment.playerId == PlayerPrefsX.GetUserId())
		{
			this.CreateComment(false);
			this.CreateProfile(false);
		}
		else
		{
			this.CreateProfile(true);
			this.CreateComment(true);
		}
	}

	// Token: 0x060019E6 RID: 6630 RVA: 0x0011CC9C File Offset: 0x0011B09C
	public virtual void CreateProfile(bool _left = true)
	{
		this.m_profileImage = new PsUIProfileImage(this, true, string.Empty, this.m_comment.facebookId, this.m_comment.gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, true, true);
		this.m_profileImage.m_TAC.m_letTouchesThrough = true;
		this.m_profileImage.SetSize(0.1f, 0.1f, RelativeTo.ParentWidth);
		if (_left)
		{
			this.m_profileImage.SetVerticalAlign(1f);
		}
		else
		{
			this.m_profileImage.SetVerticalAlign(0f);
		}
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x0011CD3C File Offset: 0x0011B13C
	public virtual void CreateComment(bool _right = true)
	{
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetSpacing(0f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
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
		string text = ((!PsMetagameManager.IsFriend(this.m_comment.playerId)) ? "ffffff" : "#ABFF2A");
		string text2 = "#011532";
		if (this.m_comment.admin)
		{
			text = "#FFFFFF";
		}
		UIFittedText uifittedText = new UIFittedText(this.m_nameArea, false, string.Empty, this.m_comment.name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, text, text2);
		uifittedText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		uifittedText.m_shadowtmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, this.m_comment.comment, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "#1B405A", true, null);
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

	// Token: 0x060019E8 RID: 6632 RVA: 0x0011CFF0 File Offset: 0x0011B3F0
	protected void NameAreaDrawhandler(UIComponent _c)
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
		Color color = DebugDraw.HexToColor("#1165A7");
		Color color2 = DebugDraw.HexToColor("#5893c1");
		if (this.m_comment.admin)
		{
			color = DebugDraw.HexToColor("#ad004e");
			color2 = DebugDraw.HexToColor("#E70069");
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060019E9 RID: 6633 RVA: 0x0011D160 File Offset: 0x0011B560
	protected void OwnNameDrawhandler(UIComponent _c)
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
		Color color = DebugDraw.HexToColor("#84FF38");
		Color color2 = DebugDraw.HexToColor("#1CA71C");
		if (this.m_comment.admin)
		{
			color2 = DebugDraw.HexToColor("#ad004e");
			color = DebugDraw.HexToColor("#E70069");
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060019EA RID: 6634 RVA: 0x0011D2D0 File Offset: 0x0011B6D0
	protected void RightCommentDrawhandler(UIComponent _c)
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
		Color color = DebugDraw.HexToColor("#ffffff");
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, array, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060019EB RID: 6635 RVA: 0x0011D4C8 File Offset: 0x0011B8C8
	protected void AnnouncementDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.015f * (float)Screen.height;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, num, 6, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color bubbleColor = this.m_bubbleColor;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, bubbleColor, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, bubbleColor, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060019EC RID: 6636 RVA: 0x0011D578 File Offset: 0x0011B978
	protected void LeftCommentDrawhandler(UIComponent _c)
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
		Color color = DebugDraw.HexToColor("#ffffff");
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, array, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060019ED RID: 6637 RVA: 0x0011D770 File Offset: 0x0011BB70
	private void OpenPlayerProfilePopup()
	{
		SoundS.PlaySingleShot("/UI/Popup", Vector3.zero, 1f);
		this.m_popup = new PsUIBasePopup(typeof(PsUICenterProfilePopup), null, null, null, true, true, InitialPage.Center, false, false, false);
		(this.m_popup.m_mainContent as PsUICenterProfilePopup).SetUser(this.m_comment.playerId, false);
		this.m_popup.SetAction("Exit", delegate
		{
			this.m_popup.Destroy();
		});
		this.m_popup.Update();
		TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
	}

	// Token: 0x060019EE RID: 6638 RVA: 0x0011D834 File Offset: 0x0011BC34
	public override void Step()
	{
		if (((this.m_nameArea != null && this.m_nameArea.m_hit) || (this.m_profileImage != null && this.m_profileImage.m_hit)) && this.m_comment.playerId != PlayerPrefsX.GetUserId())
		{
			TouchAreaS.CancelAllTouches(null);
			this.OpenPlayerProfilePopup();
		}
		base.Step();
	}

	// Token: 0x04001C46 RID: 7238
	public CommentData m_comment;

	// Token: 0x04001C47 RID: 7239
	public Color m_bubbleColor = DebugDraw.HexToColor("#ffffff");

	// Token: 0x04001C48 RID: 7240
	protected UICanvas m_nameArea;

	// Token: 0x04001C49 RID: 7241
	private PsUIProfileImage m_profileImage;

	// Token: 0x04001C4A RID: 7242
	private PlayerData? m_playerData;

	// Token: 0x04001C4B RID: 7243
	private PsUIBasePopup m_popup;
}
