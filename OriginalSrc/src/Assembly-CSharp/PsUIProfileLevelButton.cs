using System;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class PsUIProfileLevelButton : UICanvas
{
	// Token: 0x0600174D RID: 5965 RVA: 0x000FBA18 File Offset: 0x000F9E18
	public PsUIProfileLevelButton(UIComponent _parent, PsGameLoop _loop, bool _inScrollableList = true, bool _showCreator = false, bool _claimable = false)
		: base(_parent, true, "profileLevel", null, string.Empty)
	{
		this.m_claimed = true;
		this.m_inScrollableList = _inScrollableList;
		this.m_claimAmount = 0;
		this.m_gameloop = _loop;
		this.m_TAC.m_letTouchesThrough = true;
		this.m_friendLevel = PsMetagameManager.m_friends.IsFriend(this.m_gameloop.GetCreatorId());
		this.m_positive = this.m_gameloop.GetVisualLikes();
		this.m_negative = this.m_gameloop.GetVisualDislikes();
		if (_showCreator)
		{
			this.SetMargins(0f, 0f, 0.125f, 0f, RelativeTo.OwnWidth);
		}
		this.SetDrawHandler(new UIDrawDelegate(this.DropShadow));
		if (_showCreator)
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
			uihorizontalList.SetHeight(0.25f, RelativeTo.ParentWidth);
			uihorizontalList.SetMargins(0f, 0f, -0.125f, 0.125f, RelativeTo.ParentWidth);
			uihorizontalList.SetVerticalAlign(1f);
			uihorizontalList.SetSpacing(0.075f, RelativeTo.ParentWidth);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetDepthOffset(-15f);
			PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uihorizontalList, false, string.Empty, this.m_gameloop.GetFacebookId(), this.m_gameloop.GetGamecenterId(), -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, true, true);
			psUIProfileImage.SetSize(0.95f, 0.95f, RelativeTo.ParentHeight);
			UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.585f, RelativeTo.ParentHeight);
			uicanvas.SetWidth(3f, RelativeTo.ParentHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, this.m_gameloop.GetCreatorName(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, (!this.m_friendLevel) ? "#FFFFF6" : "#A6FF32", "313131");
			uifittedText.SetHorizontalAlign(0f);
			uifittedText.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			uifittedText.m_shadowtmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		}
		PsUIScreenshot psUIScreenshot = new PsUIScreenshot(this, false, string.Empty, Vector2.zero, _loop, true, true, 0.03f, false);
		psUIScreenshot.SetDrawHandler(new UIDrawDelegate(PsUIScreenshot.BasicDrawHandler));
		psUIScreenshot.SetHeight(1f, RelativeTo.ParentHeight);
		psUIScreenshot.SetWidth(1f, RelativeTo.ParentWidth);
		psUIScreenshot.SetDepthOffset(-2f);
		string text = "item_mode_adventure";
		if (this.m_gameloop.GetGameMode() == PsGameMode.Race)
		{
			text = "item_mode_timeattack";
		}
		UIFittedSprite uifittedSprite = new UIFittedSprite(psUIScreenshot, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
		uifittedSprite.SetHeight(0.4f, RelativeTo.ParentHeight);
		if (!_showCreator)
		{
			uifittedSprite.SetAlign(0f, 1f);
		}
		else
		{
			uifittedSprite.SetAlign(0f, 0.75f);
		}
		uifittedSprite.SetDepthOffset(-3f);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetVerticalAlign(0f);
		uiverticalList.SetWidth(1.04f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0.01f, 0.01f, 0f, 0.01f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(new UIDrawDelegate(this.InfoDrawhandler));
		uiverticalList.SetDepthOffset(-3f);
		UICanvas uicanvas2 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.025f, RelativeTo.ScreenHeight);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetVerticalAlign(0f);
		uicanvas2.SetDepthOffset(-2f);
		uicanvas2.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, _loop.GetName(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#AFFF2E", "#13245E");
		uifittedText2.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		uifittedText2.m_shadowtmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UICanvas uicanvas3 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetHeight(0.01f, RelativeTo.ScreenHeight);
		uicanvas3.SetDrawHandler(new UIDrawDelegate(this.SimpleLikeDrawhandler));
		uicanvas3.SetDepthOffset(-1f);
		if (_claimable && this.m_gameloop.m_minigameMetaData.rewardCoins > 0)
		{
			this.m_claimed = false;
			this.m_claimAmount = this.m_gameloop.m_minigameMetaData.rewardCoins;
			UICanvas uicanvas4 = new UICanvas(this, false, string.Empty, null, string.Empty);
			uicanvas4.SetHeight(0.085f, RelativeTo.ScreenHeight);
			uicanvas4.SetWidth(0.15f, RelativeTo.ScreenHeight);
			uicanvas4.SetMargins(-0.5f, 0.5f, -0.475f, 0.475f, RelativeTo.ParentHeight);
			uicanvas4.SetAlign(1f, 0f);
			uicanvas4.RemoveDrawHandler();
			uicanvas4.SetDepthOffset(-10f);
			UICanvas uicanvas5 = new UICanvas(uicanvas4, false, string.Empty, null, string.Empty);
			uicanvas5.SetHeight(1f, RelativeTo.ParentHeight);
			uicanvas5.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas5.SetMargins(0.0075f, 0.0075f, 0.015f, 0.0225f, RelativeTo.ScreenHeight);
			uicanvas5.SetDrawHandler(new UIDrawDelegate(this.ClaimBubbleDrawhandler));
			UICanvas uicanvas6 = new UICanvas(uicanvas5, false, string.Empty, null, string.Empty);
			uicanvas6.SetHeight(1f, RelativeTo.ParentHeight);
			uicanvas6.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas6.SetMargins(0f, 1f, 0f, 0f, RelativeTo.OwnHeight);
			uicanvas6.RemoveDrawHandler();
			string text2 = this.m_claimAmount.ToString();
			if (this.m_claimAmount >= 10000)
			{
				text2 = "FULL";
			}
			UIFittedText uifittedText3 = new UIFittedText(uicanvas6, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "ffffff", "#ad4d00");
			uifittedText3.SetHorizontalAlign(1f);
			uifittedText3.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			uifittedText3.m_shadowtmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			UICanvas uicanvas7 = new UICanvas(uicanvas6, false, string.Empty, null, string.Empty);
			uicanvas7.SetHeight(1f, RelativeTo.ParentHeight);
			uicanvas7.SetWidth(1f, RelativeTo.ParentHeight);
			uicanvas7.SetHorizontalAlign(1f);
			uicanvas7.SetMargins(1f, -1f, 0f, 0f, RelativeTo.ParentHeight);
			uicanvas7.RemoveDrawHandler();
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas7, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_scoreboard_prize_coins", null), true, true);
			uifittedSprite2.SetHeight(0.975f, RelativeTo.ParentHeight);
			uifittedSprite2.SetHorizontalAlign(1f);
			TweenC tweenC = TweenS.AddTransformTween(uicanvas5.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(1.1f, 1.1f, 1.1f), 0.5f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
		}
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000FC158 File Offset: 0x000FA558
	private void ClaimBubbleDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] circle = DebugDraw.GetCircle(_c.m_actualWidth * 0.5f, 50, Vector2.zero);
		DebugDraw.ScaleVectorArray(circle, new Vector2(1f, 0.65f));
		circle[5] += new Vector2(0.0125f * (float)Screen.height, -0.0165f * (float)Screen.height);
		GGData ggdata = new GGData(circle);
		Color color = DebugDraw.HexToColor("#4D1F0A");
		Color color2 = DebugDraw.HexToColor("#E2C435");
		Color color3 = DebugDraw.HexToColor("#C84C09");
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.down * 0.05f * _c.m_actualHeight + Vector3.forward, circle, 0.015f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color3, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, circle, 0.0075f * (float)Screen.height, color3, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x000FC2B4 File Offset: 0x000FA6B4
	private void DropShadow(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = _c.m_actualHeight - _c.m_actualMargins.t - _c.m_actualMargins.b;
		float num2 = (-_c.m_actualMargins.t - _c.m_actualMargins.b) * 0.5f;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, num, 0.03f * (float)Screen.height, 8, new Vector2(0f, num2));
		Color black = Color.black;
		black.a = 0.8f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.down * 0.01f * (float)Screen.height, roundedRect, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x000FC390 File Offset: 0x000FA790
	private void InfoDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight * 0.9f, 0.01f * (float)Screen.height, 8, Vector2.zero);
		Vector2[] array = new Vector2[18];
		for (int i = 0; i < 16; i++)
		{
			array[i] = roundedRect[i];
		}
		array[array.Length - 2] = new Vector2(-_c.m_actualWidth / 2f, _c.m_actualHeight * 0.9f / 2f);
		array[array.Length - 1] = new Vector2(_c.m_actualWidth / 2f, _c.m_actualHeight * 0.9f / 2f);
		Color color = DebugDraw.HexToColor("#174387");
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.down * 0.2f * _c.m_actualHeight, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.down * 0.2f * _c.m_actualHeight + Vector3.forward * -1f, array, 0.1f * _c.m_actualHeight, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x000FC518 File Offset: 0x000FA918
	public void SimpleLikeDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.5f;
		if (this.m_positive + this.m_negative != 0)
		{
			num = (float)this.m_positive / (float)(this.m_positive + this.m_negative);
		}
		float num2 = _c.m_actualWidth * num;
		if (num2 <= _c.m_actualHeight)
		{
			num2 = _c.m_actualHeight * 1.01f;
		}
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.5f, 6, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(num2, _c.m_actualHeight, _c.m_actualHeight * 0.5f, 6, Vector2.zero);
		Vector2[] roundedRect3 = DebugDraw.GetRoundedRect(_c.m_actualWidth - _c.m_actualHeight * 0.4f, _c.m_actualHeight * 0.35f, _c.m_actualHeight * 0.35f, 6, Vector2.up * _c.m_actualHeight * 0.325f);
		if (_c.m_actualWidth > num2)
		{
			int num3 = 12;
			for (int i = num3; i < 6 + num3; i++)
			{
				roundedRect2[i] = new Vector2(num2 / 2f, _c.m_actualHeight / 2f);
			}
			num3 = 18;
			for (int j = num3; j < 6 + num3; j++)
			{
				roundedRect2[j] = new Vector2(num2 / 2f, -_c.m_actualHeight / 2f);
			}
		}
		GGData ggdata = new GGData(roundedRect);
		GGData ggdata2 = new GGData(roundedRect2);
		GGData ggdata3 = new GGData(roundedRect3);
		Color color = DebugDraw.HexToColor("#DF492F");
		Color color2 = DebugDraw.HexToColor("#68FF38");
		Color white = Color.white;
		white.a = 0.35f;
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, roundedRect, 0.1f * _c.m_actualHeight, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		if (num > 0f)
		{
			PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, new Vector3(-_c.m_actualWidth / 2f + _c.m_actualWidth * num / 2f, 0f, 0f) + Vector3.forward * -2f, ggdata2, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(-_c.m_actualWidth / 2f + _c.m_actualWidth * num / 2f, 0f, 0f) + Vector3.forward * -3f, roundedRect2, 0.1f * _c.m_actualHeight, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		}
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * -4f, ggdata3, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -5f, roundedRect3, 0.05f * _c.m_actualHeight, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x000FC894 File Offset: 0x000FAC94
	public override void Step()
	{
		if (this.m_hit)
		{
			if (!this.m_claimed)
			{
				this.m_claimed = true;
				this.DestroyChildren(2);
				PsMetagameManager.ClaimMinigameReward(this.m_gameloop.m_minigameMetaData, this.m_camera.WorldToScreenPoint(this.m_TC.transform.position) - new Vector2((float)Screen.width, (float)Screen.height) * 0.5f);
			}
			else
			{
				if (PsUIProfileLevelButton.m_card != null)
				{
					PsUIProfileLevelButton.m_card.Destroy();
					PsUIProfileLevelButton.m_card = null;
				}
				SoundS.PlaySingleShot("/UI/UpgradeSelect", Vector3.zero, 1f);
				PsUIProfileLevelButton.m_card = new PsUIProfileLevelCard(this, this.m_gameloop, delegate
				{
					PsUIProfileLevelButton.m_card = null;
				}, this.m_friendLevel);
				PsUIProfileLevelButton.m_card.SetDepthOffset(-25f);
				PsUIProfileLevelButton.m_card.Update();
				float num = PsUIProfileLevelButton.m_card.m_TC.transform.position.y + PsUIProfileLevelButton.m_card.m_actualHeight * 0.5f - (this.m_parent.m_parent.m_TC.transform.position.y + this.m_parent.m_parent.m_actualHeight * 0.5f);
				float num2 = num / (float)Screen.height;
				num2 += 0.01f;
				cpBB margins = this.m_parent.m_parent.m_margins;
				cpBB margins2 = this.m_parent.m_parent.m_margins;
				margins.t = Mathf.Max(margins.t, margins.t + num2);
				float num3 = PsUIProfileLevelButton.m_card.m_TC.transform.position.y - PsUIProfileLevelButton.m_card.m_actualHeight * 0.6f - (this.m_parent.m_parent.m_TC.transform.position.y - this.m_parent.m_parent.m_actualHeight * 0.5f);
				float num4 = num3 / (float)(-(float)Screen.height);
				num4 += 0.01f;
				margins.b = Mathf.Max(margins.b, margins.b + num4);
				this.m_parent.m_parent.SetMargins(margins, RelativeTo.ScreenHeight);
				UIVerticalList uiverticalList = this.m_parent.m_parent as UIVerticalList;
				if (uiverticalList != null)
				{
					uiverticalList.SetHeight(1f, RelativeTo.ParentHeight);
					uiverticalList.CalculateReferenceSizes();
					uiverticalList.UpdateSize();
					uiverticalList.ArrangeContents();
					uiverticalList.UpdateMargins();
					uiverticalList.UpdateDimensions();
					uiverticalList.UpdateSize();
					uiverticalList.ArrangeContents();
					if (uiverticalList.d_Draw != null)
					{
						uiverticalList.d_Draw(uiverticalList);
					}
					if (uiverticalList.m_parent != null && uiverticalList.m_parent is UIVerticalList)
					{
						(uiverticalList.m_parent as UIVerticalList).SetHeight(1f, RelativeTo.ParentHeight);
						(uiverticalList.m_parent as UIVerticalList).CalculateReferenceSizes();
						(uiverticalList.m_parent as UIVerticalList).UpdateSize();
						(uiverticalList.m_parent as UIVerticalList).ArrangeContents();
						(uiverticalList.m_parent as UIVerticalList).UpdateDimensions();
						(uiverticalList.m_parent as UIVerticalList).UpdateSize();
						(uiverticalList.m_parent as UIVerticalList).ArrangeContents();
						if (uiverticalList.m_parent.m_parent != null && uiverticalList.m_parent.m_parent is UIVerticalList)
						{
							(uiverticalList.m_parent.m_parent as UIVerticalList).SetHeight(1f, RelativeTo.ParentHeight);
							(uiverticalList.m_parent.m_parent as UIVerticalList).CalculateReferenceSizes();
							(uiverticalList.m_parent.m_parent as UIVerticalList).UpdateSize();
							(uiverticalList.m_parent.m_parent as UIVerticalList).ArrangeContents();
							(uiverticalList.m_parent.m_parent as UIVerticalList).UpdateDimensions();
							(uiverticalList.m_parent.m_parent as UIVerticalList).UpdateSize();
							(uiverticalList.m_parent.m_parent as UIVerticalList).ArrangeContents();
						}
					}
				}
				if (margins.t - margins2.t > 0.01f || margins.b - margins2.b > 0.01f)
				{
					for (UIComponent uicomponent = this.m_parent; uicomponent != null; uicomponent = uicomponent.m_parent)
					{
						if (uicomponent is PsUICenterSearch || uicomponent is PsUIFriendProfiles)
						{
							if (uicomponent is PsUICenterSearch)
							{
								(uicomponent as PsUICenterSearch).Arrange(false);
							}
							else
							{
								(uicomponent as PsUIFriendProfiles).Arrange(false);
							}
							break;
						}
					}
				}
				if (this.m_inScrollableList)
				{
					UIComponent uicomponent2 = this.m_parent;
					while (!(uicomponent2 is UIScrollableCanvas))
					{
						if (uicomponent2 is UIScrollableCanvas || uicomponent2.m_parent == null)
						{
							break;
						}
						uicomponent2 = uicomponent2.m_parent;
					}
					if (uicomponent2 != null)
					{
						(uicomponent2 as UIScrollableCanvas).ArrangeContents();
						bool flag = true;
						bool flag2 = true;
						float num5 = PsUIProfileLevelButton.m_card.m_TC.transform.position.y + PsUIProfileLevelButton.m_card.m_actualHeight * 0.5f;
						float num6 = PsUIProfileLevelButton.m_card.m_TC.transform.position.y - PsUIProfileLevelButton.m_card.m_actualHeight * 0.6f;
						float num7 = (uicomponent2 as UIScrollableCanvas).m_scrollTC.transform.position.y + (uicomponent2 as UIScrollableCanvas).m_actualHeight * 0.5f;
						float num8 = (uicomponent2 as UIScrollableCanvas).m_scrollTC.transform.position.y - (uicomponent2 as UIScrollableCanvas).m_actualHeight * 0.5f;
						if (num5 > num7)
						{
							flag = false;
						}
						if (num6 < num8)
						{
							flag2 = false;
						}
						float num9 = 0f;
						if (!flag2)
						{
							num9 = num6 - num8;
						}
						if (!flag)
						{
							num9 = num5 - num7;
						}
						Vector2 vector = (uicomponent2 as UIScrollableCanvas).m_scrollTC.transform.position + new Vector2(0f, num9);
						(uicomponent2 as UIScrollableCanvas).ScrollToPosition(vector, null);
					}
				}
			}
		}
		base.Step();
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x000FCF11 File Offset: 0x000FB311
	public override void Destroy()
	{
		PsUIProfileLevelButton.m_card = null;
		base.Destroy();
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x000FCF20 File Offset: 0x000FB320
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollIn(_touch, _secondary);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
	}

	// Token: 0x06001755 RID: 5973 RVA: 0x000FCF88 File Offset: 0x000FB388
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x000FCFEC File Offset: 0x000FB3EC
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollOut(_touch, _secondary);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x000FD054 File Offset: 0x000FB454
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
	}

	// Token: 0x04001A15 RID: 6677
	public static PsUIProfileLevelCard m_card;

	// Token: 0x04001A16 RID: 6678
	public PsGameLoop m_gameloop;

	// Token: 0x04001A17 RID: 6679
	public bool m_claimed;

	// Token: 0x04001A18 RID: 6680
	public int m_claimAmount;

	// Token: 0x04001A19 RID: 6681
	private bool m_inScrollableList;

	// Token: 0x04001A1A RID: 6682
	public TweenC m_touchScaleTween;

	// Token: 0x04001A1B RID: 6683
	private int m_positive;

	// Token: 0x04001A1C RID: 6684
	private int m_negative;

	// Token: 0x04001A1D RID: 6685
	private bool m_friendLevel;
}
