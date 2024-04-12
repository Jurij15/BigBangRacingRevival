using System;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class PsUICommentField : PsUIInputTextField
{
	// Token: 0x0600126D RID: 4717 RVA: 0x000B696D File Offset: 0x000B4D6D
	public PsUICommentField()
		: base(null)
	{
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x000B6976 File Offset: 0x000B4D76
	public PsUICommentField(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x000B6980 File Offset: 0x000B4D80
	protected override void ConstructUI()
	{
		base.SetMinMaxCharacterCount(0, 100);
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
		PsCustomisationItem installedItemByCategory = PsCustomisationManager.GetCharacterCustomisationData().GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory.HAT);
		string text = null;
		if (installedItemByCategory != null)
		{
			text = installedItemByCategory.m_identifier;
		}
		UIHorizontalList uihorizontalList2 = uihorizontalList;
		bool flag = false;
		string text2 = "ProfileImage";
		string facebookId = PlayerPrefsX.GetFacebookId();
		string gameCenterId = PlayerPrefsX.GetGameCenterId();
		string text3 = text;
		this.m_profileImage = new PsUIProfileImage(uihorizontalList2, flag, text2, facebookId, gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", text3, true, true);
		this.m_profileImage.SetSize(0.08f, 0.08f, RelativeTo.ScreenHeight);
		this.m_profileImage.SetAlign(1f, 0f);
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x000B6AD0 File Offset: 0x000B4ED0
	private void LeftCommentDrawhandler(UIComponent _c)
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

	// Token: 0x040015A2 RID: 5538
	public PsUIProfileImage m_profileImage;
}
