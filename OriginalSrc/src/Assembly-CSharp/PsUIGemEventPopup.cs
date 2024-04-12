using System;
using UnityEngine;

// Token: 0x02000293 RID: 659
public class PsUIGemEventPopup : PsUIBaseEventPopup
{
	// Token: 0x060013D3 RID: 5075 RVA: 0x000C647C File Offset: 0x000C487C
	public PsUIGemEventPopup(UIComponent _parent, string _tag, EventMessage _event)
		: base(_parent, _tag, _event)
	{
		base.SetColor(DebugDraw.HexToColor("#F484FC"), DebugDraw.HexToColor("#D652EC"));
		base.SetAboutText(PsStrings.Get(StringID.EVENT_GOOD_OR_BAD_HEADER));
		this.m_cooldownLeft = PsMetagameManager.GetFreshTimeOutSeconds();
		this.m_mainContainer.SetDrawHandler(new UIDrawDelegate(this.DiamondDrawhandler));
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x000C64E0 File Offset: 0x000C48E0
	protected override void SetRightHeaderContent()
	{
		if (this.m_header != null && this.m_event != null)
		{
			UICanvas uicanvas = new UICanvas(this.m_header, true, string.Empty, null, string.Empty);
			uicanvas.SetWidth(0.35f, RelativeTo.ParentWidth);
			uicanvas.SetHorizontalAlign(0f);
			uicanvas.SetMargins(-0.1f, 0f, -0.08f, 0.06f, RelativeTo.OwnWidth);
			uicanvas.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_gamebanner_gamemode_diamonds", null), true, true);
			uifittedSprite.SetVerticalAlign(0f);
			UICanvas uicanvas2 = new UICanvas(this.m_header, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.78f, RelativeTo.ParentHeight);
			uicanvas2.SetWidth(0.65f, RelativeTo.ParentWidth);
			uicanvas2.SetMargins(0.2f, 0.1f, 0.1f, 0.1f, RelativeTo.OwnHeight);
			uicanvas2.SetAlign(1f, 1f);
			uicanvas2.RemoveDrawHandler();
			UICanvas uicanvas3 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
			uicanvas3.SetHeight(0.5f, RelativeTo.ParentHeight);
			uicanvas3.SetVerticalAlign(1f);
			uicanvas3.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas3, false, string.Empty, PsStrings.Get(StringID.GOOD_OR_BAD), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
			uifittedText.SetHorizontalAlign(0f);
			UICanvas uicanvas4 = new UICanvas(uicanvas2, false, string.Empty, null, string.Empty);
			uicanvas4.SetHeight(0.4f, RelativeTo.ParentHeight);
			uicanvas4.SetAlign(0f, 0f);
			uicanvas4.RemoveDrawHandler();
			UICanvas uicanvas5 = new UICanvas(uicanvas4, false, string.Empty, null, string.Empty);
			uicanvas5.SetWidth(0.8f, RelativeTo.ParentWidth);
			uicanvas5.SetHorizontalAlign(0f);
			uicanvas5.RemoveDrawHandler();
			string text = PsStrings.Get(StringID.SPECIAL_OFFER_VALUE);
			text = text.Replace("%1", "2x");
			UIFittedText uifittedText2 = new UIFittedText(uicanvas5, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#f051d9", "#000");
			uifittedText2.SetHorizontalAlign(0f);
			if (this.m_event == null)
			{
				uicanvas2.SetAlign(0.5f, 1f);
				uicanvas2.SetMargins(0.1f, RelativeTo.OwnHeight);
				uicanvas4.SetHeight(0.6f, RelativeTo.ParentHeight);
				uicanvas4.SetVerticalAlign(0.4f);
			}
			UICanvas uicanvas6 = new UICanvas(this.m_header, false, string.Empty, null, string.Empty);
			uicanvas6.SetHeight(0.03f, RelativeTo.ScreenHeight);
			uicanvas6.SetVerticalAlign(0f);
			uicanvas6.SetDrawHandler(new UIDrawDelegate(this.DrawHandlerSprite));
			if (this.m_event != null)
			{
				string text2 = this.m_duration + PsStrings.Get(StringID.TOUR_HOURS);
				UIText uitext = new UIText(uicanvas6, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.HurmeBold), 0.8f, RelativeTo.ParentHeight, "#272A19", null);
			}
		}
	}

	// Token: 0x060013D5 RID: 5077 RVA: 0x000C67D0 File Offset: 0x000C4BD0
	protected override void SetRightBottomContent()
	{
		if (this.m_bottomContainer != null)
		{
			this.m_bottomContainer.DestroyChildren();
			this.m_timerContainer = null;
			UICanvas uicanvas = new UICanvas(this.m_bottomContainer, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.4f, RelativeTo.ParentHeight);
			uicanvas.RemoveDrawHandler();
			UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
			if (!this.m_hasEnded)
			{
				uicanvas2.SetHeight(0.45f, RelativeTo.ParentHeight);
				uicanvas2.SetVerticalAlign(1f);
			}
			else
			{
				uicanvas2.SetHeight(0.6f, RelativeTo.ParentHeight);
				uicanvas2.SetVerticalAlign(0.5f);
			}
			uicanvas2.RemoveDrawHandler();
			string text = PsStrings.Get(StringID.CHALLENGE_TIMER_TIMELEFT);
			if (!this.m_hasStarted)
			{
				text = PsStrings.Get(StringID.TOUR_STARTS_IN);
			}
			else if (this.m_hasEnded)
			{
				text = PsStrings.Get(StringID.EVENT_HAS_ENDED);
			}
			UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
			if (!this.m_hasEnded)
			{
				this.m_timerContainer = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
				this.m_timerContainer.SetHeight(0.45f, RelativeTo.ParentHeight);
				this.m_timerContainer.SetVerticalAlign(0f);
				this.m_timerContainer.RemoveDrawHandler();
				this.m_timeleftText = new UIFittedText(this.m_timerContainer, false, string.Empty, PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true), PsFontManager.GetFont(PsFonts.HurmeBoldMN), true, null, null);
			}
		}
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x000C694C File Offset: 0x000C4D4C
	protected override void SetRightButton()
	{
		if (this.m_playButtonParent != null && this.m_event != null)
		{
			this.m_playButtonParent.DestroyChildren();
			this.m_playButton = null;
			this.m_cooldownTimer = null;
			if (!this.m_hasEnded && this.m_hasStarted)
			{
				this.m_playButton = new PsUIGenericButton(this.m_playButtonParent, 0.25f, 0.25f, 0.005f, "Button");
				this.m_playButton.m_TAC.m_letTouchesThrough = false;
				UICanvas uicanvas = new UICanvas(this.m_playButton, false, string.Empty, null, string.Empty);
				uicanvas.SetMargins(0.1f, RelativeTo.OwnHeight);
				uicanvas.SetHeight(0.11f, RelativeTo.ScreenHeight);
				uicanvas.SetWidth(0.4f, RelativeTo.ScreenHeight);
				uicanvas.RemoveDrawHandler();
				UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
				uicanvas2.RemoveDrawHandler();
				UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.PLAY), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
				bool flag = false;
				foreach (PsFloatingPlanetNode psFloatingPlanetNode in PsPlanetManager.GetCurrentPlanet().m_floatingNodeList)
				{
					if (psFloatingPlanetNode is PsFloatingFreshAndFreeNode)
					{
						flag = true;
					}
				}
				if (PsMetagameManager.IsFreshLevelAvailable() || flag)
				{
					uicanvas2.SetHeight(0.8f, RelativeTo.ParentHeight);
					this.m_playButton.EnableTouchAreas(true);
					this.m_playButton.SetMagentaColors();
				}
				else
				{
					uicanvas2.SetHeight(0.65f, RelativeTo.ParentHeight);
					uicanvas2.SetVerticalAlign(1f);
					UICanvas uicanvas3 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
					uicanvas3.SetHeight(0.3f, RelativeTo.ParentHeight);
					uicanvas3.SetVerticalAlign(0f);
					uicanvas3.RemoveDrawHandler();
					string text = PsStrings.Get(StringID.GOOD_OR_BAD_COOLDOWN);
					string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_cooldownLeft, false, true);
					text = text.Replace("%1", timeStringFromSeconds);
					this.m_cooldownTimer = new UIFittedText(uicanvas3, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBoldMN), true, null, null);
					this.m_playButton.DisableTouchAreas(true);
					this.m_playButton.SetGrayColors();
				}
			}
			else
			{
				Debug.LogError("Gem event button thinks Ended");
			}
		}
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x000C6BA0 File Offset: 0x000C4FA0
	private void SetCooldownText(int _seconds)
	{
		if (this.m_cooldownTimer != null)
		{
			string text = PsStrings.Get(StringID.GOOD_OR_BAD_COOLDOWN);
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(_seconds, false, true);
			text = text.Replace("%1", timeStringFromSeconds);
			this.m_cooldownTimer.SetText(text);
		}
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x000C6BE8 File Offset: 0x000C4FE8
	protected override void PlayButtonPressed()
	{
		TouchAreaS.CancelAllTouches(null);
		PsGameLoop psGameLoop = null;
		foreach (PsFloatingPlanetNode psFloatingPlanetNode in PsPlanetManager.GetCurrentPlanet().m_floatingNodeList)
		{
			if (psFloatingPlanetNode is PsFloatingFreshAndFreeNode && (psFloatingPlanetNode as PsFloatingFreshAndFreeNode).m_loop != null)
			{
				psGameLoop = (psFloatingPlanetNode as PsFloatingFreshAndFreeNode).m_loop;
			}
		}
		if (psGameLoop == null)
		{
			psGameLoop = new PsGameLoopFresh();
			PsMetagameManager.FreshLevelIsCreated();
		}
		psGameLoop.StartLoop();
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x000C6C88 File Offset: 0x000C5088
	protected override void UpdateEventInfo()
	{
		if (this.m_event != null)
		{
			int num = (int)Math.Ceiling((double)this.m_event.localStartTime - Main.m_EPOCHSeconds);
			int num2 = (int)Math.Ceiling((double)this.m_event.localEndTime - Main.m_EPOCHSeconds);
			if (num > 0)
			{
				if (this.m_hasStarted)
				{
					this.m_hasStarted = false;
				}
				if (num != this.m_timeLeft)
				{
					this.m_timeLeft = num;
					if (this.m_timeleftText != null && this.m_timeLeft >= 0)
					{
						string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true);
						this.m_timeleftText.SetText(timeStringFromSeconds);
						this.m_timeleftText.Update();
					}
				}
			}
			else
			{
				if (!this.m_hasStarted)
				{
					this.m_hasStarted = true;
					this.UpdateRightContent();
				}
				if (num2 != this.m_timeLeft)
				{
					this.m_timeLeft = num2;
					if (this.m_timeleftText != null && this.m_timeLeft >= 0)
					{
						if (this.m_hasEnded)
						{
							this.m_hasEnded = false;
						}
						string timeStringFromSeconds2 = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true);
						this.m_timeleftText.SetText(timeStringFromSeconds2);
						this.m_timeleftText.Update();
					}
					else if (this.m_timeLeft < 0 && !this.m_hasEnded)
					{
						this.m_hasEnded = true;
						this.UpdateRightContent();
						if (this.m_timerContainer != null)
						{
							this.m_timerContainer.Update();
						}
					}
				}
			}
		}
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x000C6DF8 File Offset: 0x000C51F8
	public override void Step()
	{
		int freshTimeOutSeconds = PsMetagameManager.GetFreshTimeOutSeconds();
		if (this.m_cooldownLeft != freshTimeOutSeconds && this.m_cooldownTimer != null)
		{
			if (freshTimeOutSeconds > 0)
			{
				this.m_cooldownLeft = freshTimeOutSeconds;
				this.SetCooldownText(this.m_cooldownLeft);
				this.m_cooldownTimer.Update();
			}
			else
			{
				this.m_cooldownLeft = freshTimeOutSeconds;
				this.SetRightButton();
				if (this.m_playButton != null)
				{
					this.m_playButton.Update();
				}
			}
		}
		base.Step();
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x000C6E78 File Offset: 0x000C5278
	protected virtual void DiamondDrawhandler(UIComponent _c)
	{
		_c.m_TC.transform.localScale = Vector3.one;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, false);
		float num = _c.m_actualHeight * this.m_header.m_height;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.02f, 15, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth, num, _c.m_actualHeight * 0.02f, 15, Vector2.zero);
		Color black = Color.black;
		black.a = 0.35f;
		GGData ggdata = new GGData(roundedRect);
		GGData ggdata2 = new GGData(roundedRect2);
		float num2 = _c.m_actualWidth / 2f;
		float num3 = 0f;
		while (num2 > 2f)
		{
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_event_gem_background", null);
			frame.x += 1f;
			frame.width -= 2f;
			Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_event_gem_background", null);
			frame2.x += 1f;
			frame2.width -= 2f;
			float num4 = ((num2 <= frame.width) ? num2 : frame.width);
			num2 -= num4;
			num3 += num4;
			frame2.x += frame2.width - num4;
			frame2.width = num4;
			frame.width = num4;
			SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC, num4, _c.m_actualHeight - num);
			SpriteS.SetOffset(spriteC, new Vector3(num3 / -2f, num / -2f), 0f);
			SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC2, num4, _c.m_actualHeight - num);
			SpriteS.SetOffset(spriteC2, new Vector3(num3 / 2f, num / -2f), 0f);
			num3 += num4;
		}
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, new Vector3(0f, (_c.m_actualHeight - num) / 2f), ggdata2, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.back, roundedRect, (float)Screen.height * 0.009f, this.m_bottomColor, this.m_topColor, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x000C7134 File Offset: 0x000C5534
	public void DrawHandlerSprite(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_event_row_purple", null);
		float num = frame.width / 2f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num / 2f, frame.y, num, frame.height);
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

	// Token: 0x04001698 RID: 5784
	private UIFittedText m_cooldownTimer;

	// Token: 0x04001699 RID: 5785
	private int m_cooldownLeft;

	// Token: 0x0400169A RID: 5786
	private UICanvas m_timerContainer;
}
