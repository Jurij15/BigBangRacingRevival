using System;
using System.Collections;
using System.Collections.Generic;
using AdMediation;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class PsUIPowerFuels : UIVerticalList
{
	// Token: 0x060014EE RID: 5358 RVA: 0x000DA5F0 File Offset: 0x000D89F0
	public PsUIPowerFuels(UIComponent _parent, Action _purchaseCallback)
		: base(_parent, string.Empty)
	{
		this.m_purchaseCallback = _purchaseCallback;
		this.m_powerFuelDatas = new List<PsPowerFuelData>();
		this.m_powerFuelDatas.Add(new PsPowerFuelData(BossBattles.tuneUp1Price, 50, BossBattles.tuneUp1Change, "menu_skullrider_gas_1", StringID.BOSS_BATTLE_POWERFUEL_JETFUEL, "#ffc23b"));
		this.m_powerFuelDatas.Add(new PsPowerFuelData(BossBattles.tuneUp2Price, 100, BossBattles.tuneUp2Change, "menu_skullrider_gas_2", StringID.BOSS_BATTLE_POWERFUEL_MEGAFUEL, "#ff9249"));
		this.m_powerFuelDatas.Add(new PsPowerFuelData(BossBattles.tuneUp3Price, 200, BossBattles.tuneUp3Change, "menu_skullrider_gas_3", StringID.BOSS_BATTLE_POWERFUEL_SUPERFUEL, "#ff6efe"));
		this.m_fuelButtons = new List<PsUIPowerFuels.FuelButton>();
		this.RemoveDrawHandler();
		this.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_descriptionArea = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_descriptionArea.RemoveDrawHandler();
		this.m_descriptionArea.SetSize(0.01f, 0.01f, RelativeTo.ScreenHeight);
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetHeight(0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.42f, RelativeTo.ScreenWidth);
		this.m_title = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.BOSS_BATTLE_POWERFUEL_HEADER), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#ffffff", "#000000");
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetHeight(0.12f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		for (int i = 0; i < 3; i++)
		{
			PsUIPowerFuels.FuelButton fuelButton = new PsUIPowerFuels.FuelButton(uihorizontalList, this.m_powerFuelDatas[i], new Action<PsUIPowerFuels.FuelButton>(this.FuelButtonAction), this.m_purchaseCallback, i == 0);
			fuelButton.SetSize(fuelButton.m_frame.width / fuelButton.m_frame.height * 0.25f, 0.25f, RelativeTo.ScreenHeight);
			fuelButton.SetVerticalAlign(1f);
			this.m_fuelButtons.Add(fuelButton);
		}
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x000DA808 File Offset: 0x000D8C08
	public void FuelButtonAction(PsUIPowerFuels.FuelButton _fuelButton)
	{
		for (int i = 0; i < this.m_fuelButtons.Count; i++)
		{
			if (this.m_fuelButtons[i] == _fuelButton)
			{
				if (this.m_fuelButtons[i].m_isUp)
				{
					this.m_fuelButtons[i].MoveDown(delegate
					{
						EntityManager.SetActivityOfEntity(this.m_title.m_TC.p_entity, true, true, true, true, true, true);
					});
					this.HideDescription();
				}
				else
				{
					int index = i;
					this.m_fuelButtons[i].MoveUp(delegate
					{
						this.ShowDescription(this.m_fuelButtons[index].m_powerFuelData);
					});
					EntityManager.SetActivityOfEntity(this.m_title.m_TC.p_entity, false, true, true, true, true, true);
				}
			}
			else if (this.m_fuelButtons[i].m_isUp)
			{
				this.m_fuelButtons[i].MoveDown(null);
				this.HideDescription();
			}
		}
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000DA900 File Offset: 0x000D8D00
	private void ShowDescription(PsPowerFuelData _fuelData)
	{
		this.m_drawHandlerColor = _fuelData.m_color;
		UIVerticalList uiverticalList = new UIVerticalList(this.m_descriptionArea, string.Empty);
		uiverticalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0.02f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(new UIDrawDelegate(this.DarkBlueBGDrawhandler));
		uiverticalList.SetVerticalAlign(0f);
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(0.4f, 0.05f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(_fuelData.m_stringID).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, _fuelData.m_color, null);
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.BOSS_BATTLE_POWERFUEL_BODY1), PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox.SetWidth(0.45f, RelativeTo.ScreenHeight);
		this.m_descriptionArea.Update();
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000DA9F4 File Offset: 0x000D8DF4
	public void DarkBlueBGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.0125f * (float)Screen.height, 8, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#173D5B");
		Color color2 = DebugDraw.HexToColor("#0C254A");
		Color color3 = DebugDraw.HexToColor(this.m_drawHandlerColor);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.01f * (float)Screen.height, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x000DAAC0 File Offset: 0x000D8EC0
	private void HideDescription()
	{
		this.m_descriptionArea.DestroyChildren();
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000DAAD0 File Offset: 0x000D8ED0
	private void PerformanceDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.0075f * (float)Screen.height, 8, Vector2.zero);
		List<Vector2> list = new List<Vector2>(roundedRect);
		list.Insert(16, new Vector2(_c.m_actualWidth * -0.501f, _c.m_actualHeight * 0.1f));
		list.Insert(16, new Vector2(_c.m_actualWidth * -0.5f - 0.0265f * (float)Screen.height, 0f));
		list.Insert(16, new Vector2(_c.m_actualWidth * -0.501f, _c.m_actualHeight * -0.1f));
		GGData ggdata = new GGData(list.ToArray());
		Color color = DebugDraw.HexToColor("#1B435E");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, list.ToArray(), 0.0085f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x040017A2 RID: 6050
	private List<PsUIPowerFuels.FuelButton> m_fuelButtons;

	// Token: 0x040017A3 RID: 6051
	private UIFittedText m_title;

	// Token: 0x040017A4 RID: 6052
	private List<PsPowerFuelData> m_powerFuelDatas;

	// Token: 0x040017A5 RID: 6053
	public Action m_purchaseCallback;

	// Token: 0x040017A6 RID: 6054
	private UICanvas m_descriptionArea;

	// Token: 0x040017A7 RID: 6055
	private string m_drawHandlerColor;

	// Token: 0x020002C6 RID: 710
	public class FuelButton : UIRectSpriteButton
	{
		// Token: 0x060014F5 RID: 5365 RVA: 0x000DAC28 File Offset: 0x000D9028
		public FuelButton(UIComponent _parent, PsPowerFuelData _powerFuelData, Action<PsUIPowerFuels.FuelButton> _fuelButtonAction, Action _purchasedCallback, bool _canBuyWithAd)
			: base(_parent, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_powerFuelData.m_frameName, null), true, false)
		{
			this.m_fuelButtonAction = _fuelButtonAction;
			this.m_powerFuelData = _powerFuelData;
			this.m_purchasedCallback = _purchasedCallback;
			if (PsState.m_activeGameLoop is PsGameLoopAdventureBattle)
			{
				PsGameLoopAdventureBattle psGameLoopAdventureBattle = PsState.m_activeGameLoop as PsGameLoopAdventureBattle;
				if (psGameLoopAdventureBattle.m_purchasedPowerFuels == null || !psGameLoopAdventureBattle.m_purchasedPowerFuels.Contains(this.m_powerFuelData.m_cc))
				{
					if (_canBuyWithAd && PsAdMediation.adsAvailable())
					{
						this.m_watchAdButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
						this.m_watchAdButton.SetGreenColors(true);
						this.m_watchAdButton.SetText(PsStrings.Get(StringID.NITRO_FILL_WATCH_AD), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
						this.m_watchAdButton.SetIcon("menu_watch_ad_badge", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
						this.m_watchAdButton.SetVerticalAlign(0.16f);
						this.m_watchAdButton.SetMargins(0.032f, 0.032f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
						this.m_watchAdButton.SetDepthOffset(-10f);
						this.m_watchAdButton.DisableTouchAreas(true);
					}
					else
					{
						this.m_purchaseButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
						this.m_purchaseButton.SetGreenColors(true);
						this.m_purchaseButton.SetText(this.m_powerFuelData.m_price.ToString(), 0.03f, 0.06f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
						this.m_purchaseButton.SetIcon("menu_resources_diamond_icon", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
						this.m_purchaseButton.SetVerticalAlign(0.16f);
						this.m_purchaseButton.SetMargins(0.032f, 0.032f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
						this.m_purchaseButton.SetDepthOffset(-10f);
						this.m_purchaseButton.DisableTouchAreas(true);
					}
				}
				else
				{
					this.DisableTouchAreas(true);
					base.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
				}
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000DAE70 File Offset: 0x000D9270
		public override void Step()
		{
			if (this.m_hit && this.m_fuelButtonAction != null)
			{
				this.m_fuelButtonAction.Invoke(this);
			}
			else if (this.m_purchaseButton != null && this.m_purchaseButton.m_hit)
			{
				this.ApplyBoost(true);
			}
			else if (this.m_watchAdButton != null && this.m_watchAdButton.m_hit)
			{
				this.WatchAd(delegate
				{
					this.ApplyBoost(false);
				});
			}
			base.Step();
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x000DAF00 File Offset: 0x000D9300
		private void ApplyBoost(bool _consumeResources)
		{
			if (PsState.m_activeGameLoop is PsGameLoopAdventureBattle)
			{
				if (!_consumeResources || PsMetagameManager.m_playerStats.diamonds >= this.m_powerFuelData.m_price)
				{
					if (_consumeResources)
					{
						PsMetagameManager.m_playerStats.CumulateDiamonds(-this.m_powerFuelData.m_price);
					}
					PsGameLoopAdventureBattle psGameLoopAdventureBattle = PsState.m_activeGameLoop as PsGameLoopAdventureBattle;
					if (psGameLoopAdventureBattle.m_purchasedPowerFuels == null)
					{
						psGameLoopAdventureBattle.m_purchasedPowerFuels = new List<int>();
					}
					psGameLoopAdventureBattle.m_purchasedPowerFuels.Add(this.m_powerFuelData.m_cc);
					if (this.m_purchaseButton != null)
					{
						this.m_purchaseButton.Destroy();
						this.m_purchaseButton = null;
					}
					if (this.m_watchAdButton != null)
					{
						this.m_watchAdButton.Destroy();
						this.m_watchAdButton = null;
					}
					BossBattles.AlterHandicap(PsState.m_activeMinigame.m_playerUnit.GetType(), this.m_powerFuelData.m_handicap);
					Hashtable hashtable = ClientTools.GenerateProgressionPathJson(PsState.m_activeGameLoop, PsState.m_activeGameLoop.m_path.m_currentNodeId, true, true, true);
					PsMetagameManager.SetPlayerDataAndProgression(new Hashtable(), hashtable, PsPlanetManager.GetCurrentPlanet().GetPlanetInfo().GetIdentifier(), false);
					if (PsState.m_activeMinigame.m_gamePaused || !PsState.m_activeMinigame.m_gameStarted)
					{
						PsState.m_activeMinigame.m_playerNode.Reset();
					}
					this.DisableTouchAreas(true);
					base.SetOverrideShader(Shader.Find("WOE/Fx/GreyscaleUnlitAlpha"));
					this.m_fuelButtonAction.Invoke(this);
					if (this.m_purchasedCallback != null)
					{
						this.m_purchasedCallback.Invoke();
					}
					SoundS.PlaySingleShot("/UI/Purchase", Vector2.zero, 1f);
				}
				else
				{
					CameraS.CreateBlur(null);
					new PsGetDiamondsFlow(new Action(CameraS.RemoveBlur), new Action(CameraS.RemoveBlur), null);
				}
			}
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000DB0F0 File Offset: 0x000D94F0
		public override void Update()
		{
			base.Update();
			this.m_originalY = this.m_TC.transform.localPosition.y;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000DB124 File Offset: 0x000D9524
		public void MoveUp(Action _callback = null)
		{
			this.m_isUp = true;
			if (this.m_purchaseButton != null)
			{
				this.m_purchaseButton.EnableTouchAreas(true);
			}
			if (this.m_watchAdButton != null)
			{
				this.m_watchAdButton.EnableTouchAreas(true);
				PsMetrics.AdOffered("superfuel");
			}
			if (this.m_tween != null)
			{
				TweenS.RemoveComponent(this.m_tween);
				this.m_tween = null;
			}
			this.m_tween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Position, TweenStyle.BackOut, new Vector3(this.m_TC.transform.localPosition.x, this.m_originalY + (float)Screen.height * 0.11f, this.m_TC.transform.localPosition.z), 0.25f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_tween, delegate(TweenC _tweenC)
			{
				if (this.m_tween != null)
				{
					TweenS.RemoveComponent(this.m_tween);
					this.m_tween = null;
					if (_callback != null)
					{
						_callback.Invoke();
					}
				}
			});
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000DB220 File Offset: 0x000D9620
		public void MoveDown(Action _callback = null)
		{
			this.m_isUp = false;
			if (this.m_purchaseButton != null)
			{
				this.m_purchaseButton.DisableTouchAreas(true);
			}
			if (this.m_watchAdButton != null)
			{
				this.m_watchAdButton.DisableTouchAreas(true);
			}
			if (this.m_tween != null)
			{
				TweenS.RemoveComponent(this.m_tween);
				this.m_tween = null;
			}
			this.m_tween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Position, TweenStyle.Linear, new Vector3(this.m_TC.transform.localPosition.x, this.m_originalY, this.m_TC.transform.localPosition.z), 0.15f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_tween, delegate(TweenC _tweenC)
			{
				if (this.m_tween != null)
				{
					TweenS.RemoveComponent(this.m_tween);
					this.m_tween = null;
				}
				if (_callback != null)
				{
					_callback.Invoke();
				}
			});
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000DB303 File Offset: 0x000D9703
		public override void Destroy()
		{
			if (this.m_tween != null)
			{
				TweenS.RemoveComponent(this.m_tween);
				this.m_tween = null;
			}
			base.Destroy();
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000DB328 File Offset: 0x000D9728
		protected void WatchAd(Action _finishedAction)
		{
			if (PsAdMediation.adsAvailable())
			{
				TouchAreaS.Disable();
				PsAdMediation.ShowAd(delegate(AdResult c)
				{
					this.AdCallBack(c, _finishedAction);
				}, null);
			}
			else
			{
				PsMetrics.AdNotAvailable("superfuel");
				_finishedAction.Invoke();
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x000DB384 File Offset: 0x000D9784
		protected void AdCallBack(AdResult _result, Action _finishedAction)
		{
			TouchAreaS.Enable();
			SoundS.PauseMixer(false);
			Debug.Log("Ad display result: " + _result.ToString(), null);
			PsMetrics.AdWatched("superfuel", _result.ToString());
			if (_result == AdResult.Finished)
			{
				_finishedAction.Invoke();
			}
			else if (_result == AdResult.Failed || _result == AdResult.Skipped)
			{
				Debug.LogError("Ad skipped or failed");
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
		}

		// Token: 0x040017A8 RID: 6056
		private const string GREYSCALE_SHADER = "WOE/Fx/GreyscaleUnlitAlpha";

		// Token: 0x040017A9 RID: 6057
		public PsUIGenericButton m_purchaseButton;

		// Token: 0x040017AA RID: 6058
		public PsUIGenericButton m_watchAdButton;

		// Token: 0x040017AB RID: 6059
		private Action<PsUIPowerFuels.FuelButton> m_fuelButtonAction;

		// Token: 0x040017AC RID: 6060
		private Action m_purchasedCallback;

		// Token: 0x040017AD RID: 6061
		public PsPowerFuelData m_powerFuelData;

		// Token: 0x040017AE RID: 6062
		public bool m_isUp;

		// Token: 0x040017AF RID: 6063
		private TweenC m_tween;

		// Token: 0x040017B0 RID: 6064
		private float m_originalY;
	}
}
