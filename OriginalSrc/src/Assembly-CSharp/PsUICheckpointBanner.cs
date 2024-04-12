using System;
using UnityEngine;

// Token: 0x0200023F RID: 575
public class PsUICheckpointBanner
{
	// Token: 0x0600116D RID: 4461 RVA: 0x000A892C File Offset: 0x000A6D2C
	public PsUICheckpointBanner(PsGameLoop _gameLoop, TransformC _parent, TransformC _checkpointTC, int _gachaSlotIndex)
	{
		if (PsGachaManager.m_gachas[_gachaSlotIndex] == null)
		{
			PsGameLoopBlockLevel psGameLoopBlockLevel = _gameLoop as PsGameLoopBlockLevel;
			if (psGameLoopBlockLevel != null)
			{
				PsGachaManager.AddGacha(new PsGacha(psGameLoopBlockLevel.m_gachaType), _gachaSlotIndex, false);
			}
			else
			{
				PsGachaManager.AddGacha(new PsGacha(GachaType.SILVER), _gachaSlotIndex, false);
			}
			PsGachaManager.UnlockGacha(PsGachaManager.m_gachas[_gachaSlotIndex], true);
		}
		this.m_gameLoop = _gameLoop;
		this.m_state = PsUICheckpointBanner.State.UPDATE;
		this.m_gachaSlotIndex = _gachaSlotIndex;
		this.m_checkpointTC = _checkpointTC;
		this.m_bannerTC = _parent;
		this.m_timeString = PsMetagameManager.GetTimeStringFromSeconds(PsGachaManager.m_gachas[this.m_gachaSlotIndex].m_unlockTimeLeft);
		this.m_price = PsMetagameManager.SecondsToDiamonds(PsGachaManager.m_gachas[this.m_gachaSlotIndex].m_unlockTimeLeft);
		SpriteSheet planetSpriteSheet = this.m_gameLoop.m_planet.m_planetSpriteSheet;
		string text = PsStrings.Get(StringID.OPEN_NOW);
		string text2 = "#ffffff";
		Frame frame = planetSpriteSheet.m_atlas.GetFrame("menu_cp_sign_1", null);
		this.m_height = frame.height / frame.width * this.m_width;
		this.m_yOffset += this.m_height / 2f;
		this.m_backgroundSprite = SpriteS.AddComponent(this.m_bannerTC, frame, planetSpriteSheet);
		SpriteS.SetDimensions(this.m_backgroundSprite, this.m_width, this.m_height);
		SpriteS.SetOffset(this.m_backgroundSprite, new Vector3(this.m_xOffset, this.m_yOffset, this.m_zOffset), 0f);
		SpriteS.SetColor(this.m_backgroundSprite, Color.white);
		SpriteS.SetSortValue(this.m_backgroundSprite, 0f);
		Frame frame2 = planetSpriteSheet.m_atlas.GetFrame("menu_cp_arrow_1", null);
		this.m_arrowSprite = SpriteS.AddComponent(this.m_bannerTC, frame2, planetSpriteSheet);
		this.m_widthRatio = frame2.width / frame.width;
		this.m_arrowWidth = this.m_width * this.m_widthRatio;
		this.m_arrowHeight = frame2.height / frame2.width * this.m_arrowWidth;
		this.SetArrowShape(0f);
		SpriteS.SetColor(this.m_arrowSprite, Color.white);
		SpriteS.SetSortValue(this.m_arrowSprite, 1f);
		Shader shader = Shader.Find("Framework/FontShaderFg");
		Vector3 vector;
		vector..ctor(this.m_xOffset, this.m_height * 0.6f + this.m_yOffset, -0.2f + this.m_zOffset);
		Frame frame3 = planetSpriteSheet.m_atlas.GetFrame("menu_timer_badge", null);
		this.m_timerBackgroundSprite = SpriteS.AddComponent(this.m_bannerTC, frame3, planetSpriteSheet);
		this.m_widthRatio = frame3.width / frame.width;
		SpriteS.SetDimensions(this.m_timerBackgroundSprite, this.m_width * this.m_widthRatio, frame3.height / frame3.width * this.m_width * this.m_widthRatio);
		SpriteS.SetOffset(this.m_timerBackgroundSprite, vector, 0f);
		SpriteS.SetColor(this.m_timerBackgroundSprite, Color.white);
		SpriteS.SetSortValue(this.m_timerBackgroundSprite, -3f);
		this.m_timerText = TextMeshS.AddComponent(this.m_bannerTC, vector + new Vector3(0f, 0f, -0.1f), PsFontManager.GetFont(PsFonts.KGSecondChances), 2f, 2f, this.m_height * 2.8f, Align.Center, Align.Middle, this.m_gameLoop.m_planet.m_planetCamera, string.Empty);
		TextMeshS.SetText(this.m_timerText, this.m_timeString, false);
		this.m_timerText.m_go.transform.localScale = Vector3.one;
		this.m_timerText.m_textMesh.characterSize = 1f;
		this.m_timerText.m_renderer.material.shader = shader;
		this.m_timerText.m_renderer.material.color = DebugDraw.HexToColor(text2);
		this.m_buttonTC = TransformS.AddComponent(this.m_bannerTC.p_entity);
		TransformS.ParentComponent(this.m_buttonTC, this.m_bannerTC, new Vector3(this.m_xOffset, this.m_yOffset, this.m_zOffset));
		Frame frame4 = planetSpriteSheet.m_atlas.GetFrame("menu_cp_button_bg", null);
		this.m_buttonSprite = SpriteS.AddComponent(this.m_buttonTC, frame4, planetSpriteSheet);
		this.m_widthRatio = frame4.width / frame.width;
		SpriteS.SetDimensions(this.m_buttonSprite, this.m_width * this.m_widthRatio, frame4.height / frame4.width * this.m_width * this.m_widthRatio);
		SpriteS.SetOffset(this.m_buttonSprite, new Vector3(0f, 0f, -0.1f), 0f);
		SpriteS.SetColor(this.m_buttonSprite, Color.white);
		SpriteS.SetSortValue(this.m_buttonSprite, -2f);
		TouchAreaC touchAreaC = TouchAreaS.AddRectArea(this.m_buttonTC, "BannerButton", this.m_buttonSprite.width, this.m_buttonSprite.height, this.m_gameLoop.m_planet.m_planetCamera, null, default(Vector2));
		TouchAreaS.AddTouchEventListener(touchAreaC, new TouchEventDelegate(this.TouchHandler));
		Vector3 vector2;
		vector2..ctor(this.m_width * -0.42f, 0f, -0.3f);
		this.m_buttonText = TextMeshS.AddComponent(this.m_buttonTC, vector2, PsFontManager.GetFont(PsFonts.KGSecondChances), 2f, 2f, this.m_height * 2.8f, Align.Left, Align.Middle, this.m_gameLoop.m_planet.m_planetCamera, string.Empty);
		TextMeshS.SetText(this.m_buttonText, text, false);
		this.m_buttonText.m_go.transform.localScale = Vector3.one;
		this.m_buttonText.m_textMesh.characterSize = 1f;
		this.m_buttonText.m_renderer.material.shader = shader;
		this.m_buttonText.m_renderer.material.color = DebugDraw.HexToColor(text2);
		float num = 0.55f * this.m_width;
		if (this.m_buttonText.m_renderer.bounds.size.x > num)
		{
			while (this.m_buttonText.m_renderer.bounds.size.x > num)
			{
				this.m_buttonText.m_textMesh.characterSize -= 0.01f;
			}
		}
		vector2..ctor(this.m_buttonText.m_renderer.bounds.size.x * -0.5f - 0.15f * this.m_width, 0f, -0.3f);
		this.m_buttonText.m_go.transform.localPosition = vector2;
		Vector3 vector3 = vector2 + new Vector3(0.1f, -0.1f, 0.1f);
		this.m_buttonTextShadow = TextMeshS.AddComponent(this.m_buttonTC, vector3, PsFontManager.GetFont(PsFonts.KGSecondChances), 2f, 2f, this.m_height * 2.8f, Align.Left, Align.Middle, this.m_gameLoop.m_planet.m_planetCamera, string.Empty);
		TextMeshS.SetText(this.m_buttonTextShadow, text, false);
		this.m_buttonTextShadow.m_go.transform.localScale = Vector3.one;
		this.m_buttonTextShadow.m_textMesh.characterSize = this.m_buttonText.m_textMesh.characterSize;
		this.m_buttonTextShadow.m_renderer.material.shader = shader;
		this.m_buttonTextShadow.m_renderer.material.color = DebugDraw.HexToColor("#333333");
		Vector3 vector4;
		vector4..ctor(this.m_width * 0.26f, 0f, -0.3f);
		this.m_priceText = TextMeshS.AddComponent(this.m_buttonTC, vector4, PsFontManager.GetFont(PsFonts.KGSecondChances), 2f, 2f, this.m_height * 3f, Align.Right, Align.Middle, this.m_gameLoop.m_planet.m_planetCamera, string.Empty);
		TextMeshS.SetText(this.m_priceText, this.m_price.ToString(), false);
		this.m_priceText.m_go.transform.localScale = Vector3.one;
		this.m_priceText.m_textMesh.characterSize = 1f;
		this.m_priceText.m_renderer.material.shader = shader;
		this.m_priceText.m_renderer.material.color = DebugDraw.HexToColor(text2);
		Vector3 vector5 = vector4 + new Vector3(0.1f, -0.1f, 0.1f);
		this.m_priceShadow = TextMeshS.AddComponent(this.m_buttonTC, vector5, PsFontManager.GetFont(PsFonts.KGSecondChances), 2f, 2f, this.m_height * 3f, Align.Right, Align.Middle, this.m_gameLoop.m_planet.m_planetCamera, string.Empty);
		TextMeshS.SetText(this.m_priceShadow, this.m_price.ToString(), false);
		this.m_priceShadow.m_go.transform.localScale = Vector3.one;
		this.m_priceShadow.m_textMesh.characterSize = 1f;
		this.m_priceShadow.m_renderer.material.shader = shader;
		this.m_priceShadow.m_renderer.material.color = DebugDraw.HexToColor("#333333");
		Frame frame5 = planetSpriteSheet.m_atlas.GetFrame("menu_resources_diamond_icon", null);
		this.m_diamondSprite = SpriteS.AddComponent(this.m_buttonTC, frame5, planetSpriteSheet);
		this.m_widthRatio = frame5.width / frame.width;
		SpriteS.SetDimensions(this.m_diamondSprite, this.m_width * this.m_widthRatio * 0.7f, frame5.height / frame5.width * this.m_width * this.m_widthRatio * 0.75f);
		SpriteS.SetOffset(this.m_diamondSprite, new Vector3((this.m_buttonSprite.width / 2f - this.m_diamondSprite.width / 2f) * 0.9f, 0f, -0.3f), 0f);
		SpriteS.SetColor(this.m_diamondSprite, Color.white);
		SpriteS.SetSortValue(this.m_diamondSprite, -3f);
		TransformS.SetScale(this.m_bannerTC, new Vector3(0f, 0f, 1f));
		this.m_hidden = true;
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x000A93F0 File Offset: 0x000A77F0
	public void UpdateBannerText()
	{
		Vector3 vector;
		vector..ctor(this.m_width * -0.42f, 0f, -0.3f);
		string text = PsStrings.Get(StringID.OPEN_NOW);
		this.m_buttonText.m_textMesh.characterSize = 1f;
		TextMeshS.SetText(this.m_buttonText, text, false);
		TextMeshS.SetText(this.m_buttonTextShadow, text, false);
		float num = 0.55f * this.m_width;
		if (this.m_buttonText.m_renderer.bounds.size.x > num)
		{
			while (this.m_buttonText.m_renderer.bounds.size.x > num)
			{
				this.m_buttonText.m_textMesh.characterSize -= 0.01f;
			}
		}
		vector..ctor(this.m_buttonText.m_renderer.bounds.size.x * -0.5f - 0.15f * this.m_width, 0f, -0.3f);
		this.m_buttonText.m_go.transform.localPosition = vector;
		this.m_buttonTextShadow.m_textMesh.characterSize = this.m_buttonText.m_textMesh.characterSize;
		this.m_buttonTextShadow.m_go.transform.localPosition = vector + new Vector3(0.1f, -0.1f, 0.1f);
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x000A957C File Offset: 0x000A797C
	public void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		Debug.LogWarning("T");
		if (_touchPhase == TouchAreaPhase.RollIn)
		{
			if (!this.m_scaledUp)
			{
				if (this.m_touchScaleTween != null)
				{
					TweenS.RemoveComponent(this.m_touchScaleTween);
					this.m_touchScaleTween = null;
				}
				Debug.LogWarning("1");
				this.m_touchScaleTween = TweenS.AddTransformTween(this.m_buttonTC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale + new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, false);
				this.m_currentScale += new Vector3(0.05f, 0.05f, 0.05f);
				this.m_scaledUp = true;
			}
		}
		else if (_touchPhase == TouchAreaPhase.Began)
		{
			if (!this.m_scaledUp)
			{
				if (this.m_touchScaleTween != null)
				{
					TweenS.RemoveComponent(this.m_touchScaleTween);
					this.m_touchScaleTween = null;
				}
				Debug.LogWarning("2");
				this.m_touchScaleTween = TweenS.AddTransformTween(this.m_buttonTC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale + new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, false);
				this.m_currentScale += new Vector3(0.05f, 0.05f, 0.05f);
				this.m_scaledUp = true;
			}
		}
		else if (_touchPhase == TouchAreaPhase.RollOut)
		{
			if (this.m_scaledUp)
			{
				if (this.m_touchScaleTween != null)
				{
					TweenS.RemoveComponent(this.m_touchScaleTween);
					this.m_touchScaleTween = null;
				}
				Debug.LogWarning("3");
				this.m_touchScaleTween = TweenS.AddTransformTween(this.m_buttonTC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale - new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, false);
				this.m_currentScale -= new Vector3(0.05f, 0.05f, 0.05f);
				this.m_scaledUp = false;
			}
		}
		else if ((_touchPhase == TouchAreaPhase.ReleaseIn || _touchPhase == TouchAreaPhase.ReleaseOut) && (_touchPhase == TouchAreaPhase.ReleaseIn || this.m_scaledUp))
		{
			if (_touchPhase == TouchAreaPhase.ReleaseIn)
			{
				this.TouchEvent();
			}
			Debug.LogWarning("4");
			if (this.m_scaledUp)
			{
				if (this.m_touchScaleTween != null)
				{
					TweenS.RemoveComponent(this.m_touchScaleTween);
					this.m_touchScaleTween = null;
				}
				this.m_touchScaleTween = TweenS.AddTransformTween(this.m_buttonTC, TweenedProperty.Scale, TweenStyle.CubicInOut, this.m_currentScale - new Vector3(0.05f, 0.05f, 0.05f), 0.1f, 0f, false);
				this.m_currentScale -= new Vector3(0.05f, 0.05f, 0.05f);
				this.m_scaledUp = false;
			}
		}
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x000A9850 File Offset: 0x000A7C50
	public void TouchEvent()
	{
		if (this.m_popup != null)
		{
			return;
		}
		SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
		Debug.LogWarning("PsCheckpointBanner.TouchEvent()");
		CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
		this.m_popup = new PsUIBasePopup(typeof(PsUICenterUnlockChest), null, null, null, true, true, InitialPage.Center, false, false, false);
		int num = PsMetagameManager.SecondsToDiamonds(PsGachaManager.m_gachas[this.m_gachaSlotIndex].m_unlockTimeLeft);
		(this.m_popup.m_mainContent as PsUICenterUnlockChest).SetInfo(PsGachaManager.m_gachas[this.m_gachaSlotIndex].m_gachaType.ToString(), num, PsStrings.Get(StringID.GACHA_LABEL_ADVENTURE).ToUpper(), true, this.m_gachaSlotIndex);
		this.m_popup.SetAction("Confirm", delegate
		{
			new PsUnlockGachaFlow(new Action(this.UnlockGachaWithDiamonds), delegate
			{
				this.DestroyPopup();
				CameraS.RemoveBlur();
			}, PsMetagameManager.SecondsToDiamonds(PsGachaManager.m_gachas[this.m_gachaSlotIndex].m_unlockTimeLeft));
		});
		this.m_popup.SetAction("Exit", delegate
		{
			this.DestroyPopup();
			CameraS.RemoveBlur();
		});
		TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x000A9989 File Offset: 0x000A7D89
	public void DestroyPopup()
	{
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
			this.m_popup = null;
		}
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x000A99A8 File Offset: 0x000A7DA8
	private void UnlockGachaWithDiamonds()
	{
		int num = PsMetagameManager.SecondsToDiamonds(PsGachaManager.m_gachas[this.m_gachaSlotIndex].m_unlockTimeLeft);
		PsMetagameManager.m_playerStats.diamonds -= num;
		PsMetagameManager.m_playerStats.SetDirty(true);
		BossBattles.AlterBothVehicleHandicaps(BossBattles.instaOpenChestChange);
		FrbMetrics.SpendVirtualCurrency("chckpoint_chest_timer", "gems", (double)num);
		this.m_gameLoop.StartLoop();
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x000A9A10 File Offset: 0x000A7E10
	public void SetArrowShape(float _value)
	{
		_value = Mathf.Clamp(_value, 0f, 1f);
		SpriteS.SetDimensions(this.m_arrowSprite, this.m_arrowWidth * ((_value + 1f) / 2f), this.m_arrowHeight * (1f / (_value + 1f)) * 0.5f);
		SpriteS.SetOffset(this.m_arrowSprite, new Vector3(this.m_xOffset, (-this.m_height / 2f + -this.m_arrowSprite.height / 2f) * 0.98f + this.m_yOffset, 0.01f + this.m_zOffset), 0f);
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x000A9ABC File Offset: 0x000A7EBC
	public void Hide(Action _callback = null)
	{
		if (this.m_hidden)
		{
			return;
		}
		if (this.m_tweenC != null)
		{
			TweenS.RemoveComponent(this.m_tweenC);
			this.m_tweenC = null;
		}
		this.m_tweenC = TweenS.AddTransformTween(this.m_bannerTC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(0f, 0f, 1f), 0.2f, 0f, false);
		this.m_hidden = true;
		TweenS.AddTweenEndEventListener(this.m_tweenC, delegate(TweenC _c)
		{
			TweenS.RemoveComponent(this.m_tweenC);
			this.m_tweenC = null;
			if (_callback != null)
			{
				_callback.Invoke();
			}
		});
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x000A9B58 File Offset: 0x000A7F58
	public void Show()
	{
		if (!this.m_hidden)
		{
			return;
		}
		if (this.m_tweenC != null)
		{
			TweenS.RemoveComponent(this.m_tweenC);
			this.m_tweenC = null;
		}
		this.m_tweenC = TweenS.AddTransformTween(this.m_bannerTC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.one, 0.4f, 0f, false);
		this.m_hidden = false;
		TweenS.AddTweenEndEventListener(this.m_tweenC, delegate(TweenC _c)
		{
			TweenS.RemoveComponent(this.m_tweenC);
			this.m_tweenC = null;
		});
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x000A9BD0 File Offset: 0x000A7FD0
	public void Notice()
	{
		if (this.m_hidden)
		{
			return;
		}
		if (this.m_tweenC != null)
		{
			TweenS.RemoveComponent(this.m_tweenC);
			this.m_tweenC = null;
		}
		this.m_tweenC = TweenS.AddTransformTween(this.m_bannerTC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.1f, 1.1f, 1f), 0.15f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_tweenC, delegate(TweenC _c)
		{
			if (this.m_tweenC != null)
			{
				TweenS.RemoveComponent(this.m_tweenC);
				this.m_tweenC = null;
			}
			this.m_tweenC = TweenS.AddTransformTween(this.m_bannerTC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.one, 0.15f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_tweenC, delegate(TweenC _c2)
			{
				TweenS.RemoveComponent(this.m_tweenC);
				this.m_tweenC = null;
			});
		});
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x000A9C50 File Offset: 0x000A8050
	public void Step()
	{
		if (this.m_timerText != null)
		{
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(PsGachaManager.m_gachas[this.m_gachaSlotIndex].m_unlockTimeLeft);
			if (timeStringFromSeconds != this.m_timeString)
			{
				this.m_timeString = timeStringFromSeconds;
				TextMeshS.SetText(this.m_timerText, timeStringFromSeconds, false);
				if (this.m_priceText != null && this.m_priceShadow != null)
				{
					int num = PsMetagameManager.SecondsToDiamonds(PsGachaManager.m_gachas[this.m_gachaSlotIndex].m_unlockTimeLeft);
					if (num != this.m_price)
					{
						this.m_price = num;
						TextMeshS.SetText(this.m_priceText, this.m_price.ToString(), false);
						TextMeshS.SetText(this.m_priceShadow, this.m_price.ToString(), false);
					}
				}
			}
		}
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x000A9D20 File Offset: 0x000A8120
	public void Destroy()
	{
		this.DestroyPopup();
		if (this.m_timerBackgroundSprite != null)
		{
			SpriteS.RemoveComponent(this.m_timerBackgroundSprite);
			this.m_timerBackgroundSprite = null;
		}
		if (this.m_buttonText != null)
		{
			TextMeshS.RemoveComponent(this.m_buttonText);
			this.m_buttonText = null;
		}
		if (this.m_timerText != null)
		{
			TextMeshS.RemoveComponent(this.m_timerText);
			this.m_timerText = null;
		}
		if (this.m_buttonTextShadow != null)
		{
			TextMeshS.RemoveComponent(this.m_buttonTextShadow);
			this.m_buttonTextShadow = null;
		}
		if (this.m_priceText != null)
		{
			TextMeshS.RemoveComponent(this.m_priceText);
			this.m_priceText = null;
		}
		if (this.m_priceShadow != null)
		{
			TextMeshS.RemoveComponent(this.m_priceShadow);
			this.m_priceShadow = null;
		}
		if (this.m_buttonSprite != null)
		{
			SpriteS.RemoveComponent(this.m_buttonSprite);
			this.m_buttonSprite = null;
		}
		if (this.m_diamondSprite != null)
		{
			SpriteS.RemoveComponent(this.m_diamondSprite);
			this.m_diamondSprite = null;
		}
		if (this.m_backgroundSprite != null)
		{
			SpriteS.RemoveComponent(this.m_backgroundSprite);
			this.m_backgroundSprite = null;
		}
		if (this.m_arrowSprite != null)
		{
			SpriteS.RemoveComponent(this.m_arrowSprite);
			this.m_arrowSprite = null;
		}
		if (this.m_bannerTC != null)
		{
			TransformS.RemoveComponent(this.m_bannerTC);
			this.m_bannerTC = null;
		}
	}

	// Token: 0x04001457 RID: 5207
	public PsGameLoop m_gameLoop;

	// Token: 0x04001458 RID: 5208
	public TransformC m_bannerTC;

	// Token: 0x04001459 RID: 5209
	public TransformC m_checkpointTC;

	// Token: 0x0400145A RID: 5210
	private TextMeshC m_buttonText;

	// Token: 0x0400145B RID: 5211
	private TextMeshC m_buttonTextShadow;

	// Token: 0x0400145C RID: 5212
	private SpriteC m_backgroundSprite;

	// Token: 0x0400145D RID: 5213
	private SpriteC m_arrowSprite;

	// Token: 0x0400145E RID: 5214
	private float m_arrowHeight;

	// Token: 0x0400145F RID: 5215
	private float m_arrowWidth;

	// Token: 0x04001460 RID: 5216
	private float m_xOffset;

	// Token: 0x04001461 RID: 5217
	private float m_yOffset = 4f;

	// Token: 0x04001462 RID: 5218
	private float m_zOffset = -10f;

	// Token: 0x04001463 RID: 5219
	private float m_width = 21f;

	// Token: 0x04001464 RID: 5220
	private float m_height;

	// Token: 0x04001465 RID: 5221
	private float m_widthRatio;

	// Token: 0x04001466 RID: 5222
	public PsUICheckpointBanner.State m_state;

	// Token: 0x04001467 RID: 5223
	public bool m_hidden;

	// Token: 0x04001468 RID: 5224
	private TweenC m_tweenC;

	// Token: 0x04001469 RID: 5225
	private int m_price = 7;

	// Token: 0x0400146A RID: 5226
	private TextMeshC m_priceText;

	// Token: 0x0400146B RID: 5227
	private TextMeshC m_priceShadow;

	// Token: 0x0400146C RID: 5228
	private TextMeshC m_timerText;

	// Token: 0x0400146D RID: 5229
	private string m_timeString;

	// Token: 0x0400146E RID: 5230
	private SpriteC m_timerBackgroundSprite;

	// Token: 0x0400146F RID: 5231
	private SpriteC m_diamondSprite;

	// Token: 0x04001470 RID: 5232
	private SpriteC m_buttonSprite;

	// Token: 0x04001471 RID: 5233
	public int m_gachaSlotIndex;

	// Token: 0x04001472 RID: 5234
	private TransformC m_buttonTC;

	// Token: 0x04001473 RID: 5235
	public bool m_scaledUp;

	// Token: 0x04001474 RID: 5236
	public TweenC m_touchScaleTween;

	// Token: 0x04001475 RID: 5237
	public Vector3 m_currentScale = new Vector3(1f, 1f, 1f);

	// Token: 0x04001476 RID: 5238
	private PsUIBasePopup m_popup;

	// Token: 0x02000240 RID: 576
	public enum State
	{
		// Token: 0x04001478 RID: 5240
		FOLLOW,
		// Token: 0x04001479 RID: 5241
		STAND,
		// Token: 0x0400147A RID: 5242
		UPDATE
	}
}
