using System;
using UnityEngine;

// Token: 0x0200024B RID: 587
public class PsUILevelHeader : UICanvas
{
	// Token: 0x060011CC RID: 4556 RVA: 0x000ADB30 File Offset: 0x000ABF30
	public PsUILevelHeader(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		PsGameLoop activeGameLoop = PsState.m_activeGameLoop;
		string text;
		string text2;
		string text3;
		string text4;
		if (activeGameLoop is PsGameLoopEditor)
		{
			text = PlayerPrefsX.GetUserName();
			if (activeGameLoop.m_minigameMetaData.timeSpentEditing > 0)
			{
				text2 = activeGameLoop.GetName();
			}
			else
			{
				text2 = "Unsaved Level";
			}
			text3 = string.Empty;
			text4 = PlayerPrefsX.GetFacebookId();
		}
		else
		{
			text = activeGameLoop.GetCreatorName();
			text2 = activeGameLoop.GetName();
			text3 = activeGameLoop.GetDescription();
			text4 = activeGameLoop.GetFacebookId();
		}
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_gamebanner_main_top", null);
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_gamebanner_main_bottom", null);
		Frame frame3 = null;
		if (activeGameLoop is PsGameLoopFresh)
		{
			frame3 = PsState.m_uiSheet.m_atlas.GetFrame("menu_gamebanner_gamemode_diamonds", null);
		}
		else
		{
			PsGameMode gameMode = activeGameLoop.GetGameMode();
			if (gameMode != PsGameMode.Race)
			{
				if (gameMode == PsGameMode.StarCollect || gameMode == PsGameMode.Any)
				{
					frame3 = PsState.m_uiSheet.m_atlas.GetFrame("menu_gamebanner_gamemode_adventure", null);
				}
			}
			else
			{
				frame3 = PsState.m_uiSheet.m_atlas.GetFrame("menu_gamebanner_gamemode_race", null);
			}
		}
		int num = Mathf.Max(activeGameLoop.m_scoreCurrent, activeGameLoop.m_scoreBest);
		if (activeGameLoop is PsGameLoopFresh)
		{
			Frame frame4 = PsState.m_uiSheet.m_atlas.GetFrame("menu_mode_diamond_" + num, null);
		}
		else
		{
			Frame frame4 = PsState.m_uiSheet.m_atlas.GetFrame("menu_mode_star_" + num, null);
		}
		string empty = string.Empty;
		string empty2 = string.Empty;
		switch (activeGameLoop.GetDifficulty())
		{
		case PsGameDifficulty.Easy:
		{
			Frame frame5 = PsState.m_uiSheet.m_atlas.GetFrame("menu_difficulty_casual", null);
			break;
		}
		case PsGameDifficulty.Tricky:
		{
			Frame frame5 = PsState.m_uiSheet.m_atlas.GetFrame("menu_difficulty_tricky", null);
			break;
		}
		case PsGameDifficulty.Insane:
		{
			Frame frame5 = PsState.m_uiSheet.m_atlas.GetFrame("menu_difficulty_hardcore", null);
			break;
		}
		case PsGameDifficulty.Impossible:
		{
			Frame frame5 = PsState.m_uiSheet.m_atlas.GetFrame("menu_difficulty_impossible", null);
			break;
		}
		}
		float num2 = 0.13f;
		float num3 = 0.65f;
		float num4 = 0.05f;
		float num5 = -0.005f;
		float num6 = frame.width / (frame.height + frame2.height) * (1f - 2f * (num4 + num5));
		float num7 = frame.height / (frame.height + frame2.height);
		cpBB cpBB = new cpBB(0f, num5, 0f, num4);
		cpBB cpBB2 = new cpBB(0f, num4, 0f, num5);
		this.SetHeight(num2, RelativeTo.ScreenHeight);
		base.SetWidth(num6 + num3, RelativeTo.OwnHeight);
		this.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(this, false, string.Empty, PsState.m_uiSheet, frame3, true, true);
		uifittedSprite.SetHorizontalAlign(0f);
		uifittedSprite.SetDepthOffset(-5f);
		uifittedSprite.SetMargins(0.05f, 0.15f, 0f, -0.05f, RelativeTo.ParentHeight);
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(num7, RelativeTo.ParentHeight);
		uicanvas.SetWidth(num6, RelativeTo.ParentHeight);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.SetHorizontalAlign(1f);
		uicanvas.SetMargins(cpBB, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, frame, true, true);
		uifittedSprite2.SetHorizontalAlign(0f);
		uifittedSprite2.SetVerticalAlign(0f);
		uifittedSprite2.SetMargins(1.35f, 0.9f, 0.15f, 0.2f, RelativeTo.ParentHeight);
		uifittedSprite2.SetDepthOffset(-1f);
		UIFittedText uifittedText = new UIFittedText(uifittedSprite2, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", "#000000");
		uifittedText.m_tmc.m_textMesh.fontStyle = 2;
		uifittedText.m_shadowtmc.m_textMesh.fontStyle = 2;
		UIFittedText uifittedText2 = uifittedText;
		Vector2 vector;
		vector..ctor(1f, -1f);
		uifittedText2.SetShadowShift(vector.normalized, 0.04f);
		uifittedText.SetHorizontalAlign(0f);
		UICanvas uicanvas2 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(1f - num7, RelativeTo.ParentHeight);
		uicanvas2.SetWidth(num6, RelativeTo.ParentHeight);
		uicanvas2.SetVerticalAlign(0f);
		uicanvas2.SetHorizontalAlign(1f);
		uicanvas2.SetMargins(cpBB2, RelativeTo.ParentHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, frame2, true, true);
		uifittedSprite3.SetMargins(0.2f, RelativeTo.ParentHeight);
		uifittedSprite3.SetHorizontalAlign(0f);
		uifittedSprite3.SetVerticalAlign(1f);
		uifittedSprite3.SetDepthOffset(0f);
		uifittedSprite3.SetMargins(3.7f, 0.3f, 0.15f, 0.2f, RelativeTo.ParentHeight);
		this.m_creatorName = new UIFittedText(uifittedSprite3, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#fffec6", "#000000");
		UIFittedText creatorName = this.m_creatorName;
		Vector2 vector2;
		vector2..ctor(1f, -1f);
		creatorName.SetShadowShift(vector2.normalized, 0.04f);
		this.m_creatorName.SetHorizontalAlign(0f);
		this.m_creatorName.SetMargins(0f, -1f, 0f, 0f, RelativeTo.ParentHeight);
		if (!(activeGameLoop is PsGameLoopEditor))
		{
			this.m_description = new UITextbox(uifittedSprite3, false, string.Empty, text3, PsFontManager.GetFont(PsFonts.HurmeRegular), 0.5f, RelativeTo.ParentHeight, false, Align.Left, Align.Top, null, true, null);
			this.m_description.SetWidth(0.84f, RelativeTo.ParentWidth);
			this.m_description.SetVerticalAlign(1f);
			this.m_description.SetMargins(0.15f, 0.15f, 0.05f, 0.15f, RelativeTo.ParentHeight);
			this.m_description.m_tmc.m_textMesh.color = DebugDraw.HexToColor("#000000");
			this.m_description.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DescriptionField));
			this.m_description.SetMinRows(1);
			this.m_description.SetMaxRows(4);
			this.m_description.SetDepthOffset(-6f);
		}
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uicanvas2, false, string.Empty, text4, activeGameLoop.GetGamecenterId(), -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, false, true);
		psUIProfileImage.SetSize(1.15f, 1.15f, RelativeTo.ParentHeight);
		psUIProfileImage.SetDepthOffset(-7f);
		psUIProfileImage.SetHorizontalAlign(0.155f);
		TransformS.SetRotation(psUIProfileImage.m_TC, Vector3.forward * 4f);
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x000AE254 File Offset: 0x000AC654
	public override void Update()
	{
		base.Update();
		if (this.m_description != null)
		{
			float num = this.m_creatorName.m_actualWidth / 2f + this.m_description.m_actualWidth * 1.25f;
			TransformS.SetPosition(this.m_description.m_TC, new Vector3(this.m_creatorName.m_TC.transform.position.x, this.m_description.m_TC.transform.localPosition.y, this.m_description.m_TC.transform.localPosition.z));
			TransformS.Move(this.m_description.m_TC, new Vector3(num, 0f, 0f));
		}
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x000AE322 File Offset: 0x000AC722
	public new void SetWidth(float _width, RelativeTo _relativeTo)
	{
		Debug.LogWarning("Do not use PsUILevelHeader.SetWidth() method!");
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x000AE32E File Offset: 0x000AC72E
	public new void SetSize(float _width, float _height, RelativeTo _relativeTo)
	{
		Debug.LogWarning("Do not use PsUILevelHeader.SetSize() method!");
	}

	// Token: 0x040014C4 RID: 5316
	private UITextbox m_description;

	// Token: 0x040014C5 RID: 5317
	private UIFittedText m_creatorName;
}
