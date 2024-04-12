using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029B RID: 667
public class PsUICenterOpenGacha : UICanvas
{
	// Token: 0x0600141F RID: 5151 RVA: 0x000CAA00 File Offset: 0x000C8E00
	public PsUICenterOpenGacha(UIComponent _parent)
		: base(_parent, true, string.Empty, null, string.Empty)
	{
		this.m_firstCard = true;
		if (!CameraS.m_renderTextureViewCamera.enabled)
		{
			this.m_createdBlur = true;
			if (Main.m_currentGame.m_currentScene is PsMenuScene)
			{
				CameraS.CreateBlur(PsPlanetManager.GetCurrentPlanet().m_planetCamera, null);
			}
			else
			{
				CameraS.CreateBlur(CameraS.m_mainCamera, null);
			}
		}
		this.m_boss = PsGachaManager.m_lastGachaRewards.m_boss;
		this.m_coins = PsGachaManager.m_lastGachaRewards.m_coins;
		this.m_gems = PsGachaManager.m_lastGachaRewards.m_gems;
		this.m_nitros = PsGachaManager.m_lastGachaRewards.m_nitros;
		this.m_hatIdentifier = PsGachaManager.m_lastGachaRewards.m_hat;
		this.m_bonusMoney = PsGachaManager.m_lastGachaRewards.m_bonusMoney;
		this.m_bonusHatIdentifier = PsGachaManager.m_lastGachaRewards.m_bonusHat;
		this.m_upgradeKeys = new List<string>(PsGachaManager.m_lastGachaRewards.m_upgradeItems.Keys);
		this.m_upgradeRevealIndex = 0;
		this.m_editorItemKeys = new List<string>(PsGachaManager.m_lastGachaRewards.m_editorItems.Keys);
		this.m_editorItemRevealIndex = 0;
		this.m_itemCount = this.m_upgradeKeys.Count + this.m_editorItemKeys.Count;
		this.m_itemCount += ((this.m_boss <= 0) ? 0 : 1);
		this.m_itemCount += ((this.m_coins <= 0) ? 0 : 1);
		this.m_itemCount += ((this.m_gems <= 0) ? 0 : 1);
		this.m_itemCount += ((this.m_nitros <= 0) ? 0 : 1);
		this.m_itemCount += ((this.m_bonusMoney <= 0) ? 0 : 1);
		this.m_itemCount += (string.IsNullOrEmpty(this.m_hatIdentifier) ? 0 : 1);
		this.m_itemCount += (string.IsNullOrEmpty(this.m_bonusHatIdentifier) ? 0 : 1);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		this.m_rewardArea = new UIHorizontalList(this, string.Empty);
		this.m_rewardArea.SetAlign(0.45f, 0.618f);
		this.m_rewardArea.RemoveDrawHandler();
		this.m_rewardArea.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		this.m_cardHolder = new UICanvas(this.m_rewardArea, false, string.Empty, null, string.Empty);
		this.m_cardHolder.SetWidth(0.25f, RelativeTo.ScreenHeight);
		this.m_cardHolder.SetHeight(0.6f, RelativeTo.ScreenHeight);
		this.m_cardHolder.RemoveDrawHandler();
		this.m_textHolder = new UIVerticalList(this.m_rewardArea, string.Empty);
		this.m_textHolder.SetVerticalAlign(1f);
		this.m_textHolder.SetWidth(0.63f, RelativeTo.ScreenHeight);
		this.m_textHolder.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_chest_card", null);
		this.m_cardCounter = new UIFittedSprite(this, false, string.Empty, PsState.m_uiSheet, frame, true, true);
		this.m_cardCounter.SetHeight(0.125f, RelativeTo.ScreenHeight);
		this.m_cardCounter.SetMargins(0.1f, RelativeTo.OwnWidth);
		this.m_cardCounterText = new UIFittedText(this.m_cardCounter, false, string.Empty, this.m_itemCount.ToString(), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
		this.m_chestCanvas = new UI3DRenderTextureCanvas(this, string.Empty, null, false);
		this.m_chestCanvas.SetWidth(0.5f, RelativeTo.ParentWidth);
		this.m_chestCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_chestCanvas.SetAlign(1f, 0f);
		this.m_chestCanvas.m_3DCamera.fieldOfView = 22f;
		this.m_chestCanvas.m_3DCameraPivot.transform.position = Vector3.right * 0.3f;
		this.m_chestCanvas.m_3DCameraPivot.transform.Rotate(0f, -10f, 0f, 0);
		this.m_chestCanvas.m_3DCameraPivot.transform.Rotate(15f, 0f, 0f, 1);
		this.m_chestCanvas.m_3DCameraOffset = -12f;
		this.m_chestCanvas.m_3DCamera.nearClipPlane = 1f;
		this.m_chestCanvas.m_3DCamera.farClipPlane = 500f;
		this.m_chestCanvas.RemoveTouchAreas();
		this.m_chestCanvas.SetDepthOffset(25f);
		CameraS.MoveToBehindOther(this.m_chestCanvas.m_3DCamera, this.m_camera);
		Texture texture = null;
		ResourcePool.Asset asset = RESOURCE.RewardChest_GameObject;
		switch (PsGachaManager.m_lastOpenedGacha)
		{
		case GachaType.WOOD:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureWood_Texture2D);
			break;
		case GachaType.COMMON:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureWood_Texture2D);
			break;
		case GachaType.BRONZE:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureBronze_Texture2D);
			break;
		case GachaType.SILVER:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureSilver_Texture2D);
			break;
		case GachaType.GOLD:
			texture = ResourceManager.GetTexture(RESOURCE.ChestTextureGold_Texture2D);
			break;
		case GachaType.RARE:
			asset = RESOURCE.ShopRewardChestT1_GameObject;
			break;
		case GachaType.EPIC:
			asset = RESOURCE.ShopRewardChestT2_GameObject;
			break;
		case GachaType.SUPER:
			asset = RESOURCE.ShopRewardChestT3_GameObject;
			break;
		case GachaType.BOSS:
			asset = RESOURCE.ShopRewardChestBoss_GameObject;
			break;
		}
		PrefabC prefabC = this.m_chestCanvas.AddGameObject(ResourceManager.GetGameObject(RESOURCE.ChestHolderPrefab_GameObject), Vector3.up * -2.1f, new Vector3(0f, 180f, 0f));
		this.m_holderEffects = prefabC.p_gameObject.GetComponent<VisualsChestHolder>();
		this.m_chestLocator = prefabC.p_gameObject.transform.Find("ChestHolder/ChestHolderStart/ChestHolderEndMiddle/ChestHolderEnd/ChestLocator");
		PrefabC prefabC2 = this.m_chestCanvas.AddGameObject(ResourceManager.GetGameObject(asset), new Vector3(0f, 0f, 0f), new Vector3(0f, 180f, 0f));
		prefabC2.p_gameObject.transform.parent = this.m_chestLocator;
		prefabC2.p_gameObject.transform.localPosition = Vector3.zero;
		prefabC2.p_gameObject.transform.localRotation = Quaternion.Euler(Vector3.right * 90f);
		Transform transform = prefabC2.p_gameObject.transform.Find("Shadow");
		transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
		if (texture != null)
		{
			Renderer[] componentsInChildren = prefabC2.p_gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].material.name.StartsWith("RewardChest"))
				{
					componentsInChildren[i].material.mainTexture = texture;
				}
			}
		}
		this.m_chestEffects = prefabC2.p_gameObject.GetComponent<VisualsRewardChest>();
		this.m_chestEffects.SetToActiveState();
		this.m_holderEffects.PopIn();
		this.Update();
		this.DisableTouchAreas(true);
		TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 0.5f, 0f, false, delegate(TimerC _c)
		{
			TimerS.RemoveComponent(_c);
			this.EnableTouchAreas(true);
		});
		SoundS.PlaySingleShot("/Metagame/ChestAppear", Vector3.zero, 1f);
		this.m_chestScreenPos = this.m_chestCanvas.m_3DCamera.WorldToScreenPoint(this.m_chestLocator.transform.position) - Vector3.up * (float)Screen.height * 0.5f;
		this.m_cardCounter.m_TC.transform.position = this.m_chestScreenPos + Vector3.right * (float)Screen.height * 0.25f;
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x000CB208 File Offset: 0x000C9608
	public void RevealNextCard()
	{
		if (this.m_revealingCard)
		{
			if (this.m_cardScaleTween != null)
			{
				TweenS.UpdateTween(this.m_cardScaleTween, this.m_cardScaleTween.duration);
				this.m_cardScaleTween.tweenEndEvent(this.m_cardScaleTween);
			}
			if (this.m_cardFlipTween != null)
			{
				TweenS.UpdateTween(this.m_cardFlipTween, this.m_cardFlipTween.duration);
				this.m_cardFlipTween.tweenEndEvent(this.m_cardFlipTween);
			}
			if (this.m_cardFlipTimer != null)
			{
				this.m_cardFlipTimer.timeoutHandler(this.m_cardFlipTimer);
			}
			if (this.m_cardPositionTween != null)
			{
				TweenS.UpdateTween(this.m_cardPositionTween, this.m_cardPositionTween.duration);
				this.m_cardPositionTween.tweenEndEvent(this.m_cardPositionTween);
			}
			return;
		}
		if (this.m_upgradeKeys.Count > this.m_upgradeRevealIndex || (this.m_bonusMoney > 0 && !this.m_bonusMoneyGiven) || (this.m_boss > 0 && !this.m_bossGiven) || (this.m_coins > 0 && !this.m_coinsGiven) || (this.m_gems > 0 && !this.m_gemsGiven) || (this.m_nitros > 0 && !this.m_nitrosGiven) || this.m_editorItemKeys.Count > this.m_editorItemRevealIndex)
		{
			this.m_revealingCard = true;
			bool flag = this.m_upgradeKeys.Count == this.m_upgradeRevealIndex && this.m_editorItemKeys.Count - 1 == this.m_editorItemRevealIndex;
			if (this.m_firstCard)
			{
				this.m_chestEffects.OpenChest();
				this.m_firstCard = false;
				SoundS.PlaySingleShot("/Metagame/ChestOpen", Vector3.zero, 1f);
				TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 0.25f, 0f, false, delegate(TimerC _c)
				{
					TimerS.RemoveComponent(_c);
					this.SpawnLoot();
				});
			}
			else
			{
				SoundS.PlaySingleShot("/Metagame/ChestItemFly", Vector3.zero, 1f);
				this.m_chestEffects.PopReward(flag);
				this.SpawnLoot();
			}
		}
		else
		{
			this.m_cardHolder.DestroyChildren();
			this.m_textHolder.DestroyChildren();
			this.m_holderEffects.PopOut();
			this.DisableTouchAreas(true);
			SoundS.PlaySingleShot("/Metagame/ChestDisappear", Vector3.zero, 1f);
			TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 1f, 0f, false, delegate(TimerC _c)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			});
		}
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x000CB4B4 File Offset: 0x000C98B4
	private void SpawnLoot()
	{
		this.m_holderEffects.Nudge();
		this.m_itemCount--;
		if (this.m_itemCount > 0)
		{
			this.m_cardCounterText.SetText(this.m_itemCount.ToString());
		}
		else
		{
			this.m_cardCounter.Destroy();
			this.m_cardCounter = null;
		}
		this.m_cardHolder.DestroyChildren();
		this.m_textHolder.DestroyChildren();
		if (this.m_bonusHatIdentifier != null && !this.m_bonusHatGiven)
		{
			string iconName = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(this.m_bonusHatIdentifier).m_iconName;
			this.m_currentCard = this.RevealResource(iconName, 0.8f);
			if (this.m_currentCard == null)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
		}
		else if (this.m_bonusMoney > 0 && !this.m_bonusMoneyGiven)
		{
			this.m_currentCard = this.RevealResource("menu_scoreboard_prize_coins", 1f);
			if (this.m_currentCard == null)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
		}
		else if (this.m_boss > 0 && !this.m_bossGiven)
		{
			this.m_currentCard = this.RevealResource("menu_skullrider_sabotage_icon", 1f);
			if (this.m_currentCard == null)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
		}
		else if (this.m_coins > 0 && !this.m_coinsGiven)
		{
			this.m_currentCard = this.RevealResource("menu_scoreboard_prize_coins", 1f);
			if (this.m_currentCard == null)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
		}
		else if (this.m_gems > 0 && !this.m_gemsGiven)
		{
			this.m_currentCard = this.RevealResource("hud_big_diamond_top", 1f);
			if (this.m_currentCard == null)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
		}
		else if (this.m_nitros > 0 && !this.m_nitrosGiven)
		{
			this.m_currentCard = this.RevealResource("hud_boost_boost", 1f);
			if (this.m_currentCard == null)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
		}
		else if (!string.IsNullOrEmpty(this.m_hatIdentifier) && !this.m_hatGiven)
		{
			string iconName2 = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(this.m_hatIdentifier).m_iconName;
			this.m_currentCard = this.RevealResource(iconName2, 0.8f);
			if (this.m_currentCard == null)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
		}
		else if (this.m_upgradeKeys.Count > this.m_upgradeRevealIndex)
		{
			PsUpgradeItem psUpgradeItem;
			PsUIUpgradeView.UpgradeItemInfo upgradeItemInfo;
			if (this.m_upgradeKeys[this.m_upgradeRevealIndex].StartsWith("Car"))
			{
				psUpgradeItem = PsUpgradeManager.GetUpgradeItem(typeof(OffroadCar), this.m_upgradeKeys[this.m_upgradeRevealIndex]);
				upgradeItemInfo = new PsUIUpgradeView.UpgradeItemInfo(this.m_cardHolder, psUpgradeItem, true, null, "revealCardFront", false);
				upgradeItemInfo.m_contentHolder.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.UpgradeInfoBoxBackCar));
			}
			else
			{
				psUpgradeItem = PsUpgradeManager.GetUpgradeItem(typeof(Motorcycle), this.m_upgradeKeys[this.m_upgradeRevealIndex]);
				upgradeItemInfo = new PsUIUpgradeView.UpgradeItemInfo(this.m_cardHolder, psUpgradeItem, true, null, "revealCardFront", false);
				upgradeItemInfo.m_contentHolder.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.UpgradeInfoBoxBackBike));
			}
			upgradeItemInfo.SetVerticalAlign(1f);
			upgradeItemInfo.SetWidth(1f, RelativeTo.ParentWidth);
			this.m_cardHolder.Update();
			int num = PsGachaManager.m_lastGachaRewards.m_upgradeItems[this.m_upgradeKeys[this.m_upgradeRevealIndex]];
			upgradeItemInfo.m_resourceBar.SetValues(PsUpgradeManager.GetUpgradeResourceCount(this.m_upgradeKeys[this.m_upgradeRevealIndex]) - num, psUpgradeItem.m_nextLevelResourceRequrement);
			upgradeItemInfo.ShowBack();
			this.m_currentCard = upgradeItemInfo;
			PrefabC effectPrefab2 = null;
			if (psUpgradeItem.m_rarity == PsRarity.Epic || psUpgradeItem.m_rarity == PsRarity.Rare)
			{
				float num2 = (float)Screen.height / 812f;
				GameObject gameObject;
				if (psUpgradeItem.m_rarity == PsRarity.Epic)
				{
					gameObject = ResourceManager.GetGameObject(RESOURCE.CardRevealEffectEpicPrefab_GameObject);
				}
				else
				{
					gameObject = ResourceManager.GetGameObject(RESOURCE.CardRevealEffectRarePrefab_GameObject);
				}
				effectPrefab2 = PrefabS.AddComponent(upgradeItemInfo.m_TC, Vector3.zero, gameObject);
				PrefabS.SetCamera(effectPrefab2, upgradeItemInfo.m_camera);
				effectPrefab2.p_gameObject.transform.localScale = Vector3.one * num2;
				effectPrefab2.p_gameObject.transform.Translate(new Vector3(0f, 6f * num2, -4f));
				Renderer[] componentsInChildren = effectPrefab2.p_gameObject.transform.GetComponentsInChildren<Renderer>();
				if (componentsInChildren != null)
				{
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						if (componentsInChildren[i].material != null)
						{
							componentsInChildren[i].material.renderQueue = 2800;
						}
					}
				}
			}
			Vector3 localPosition = this.m_currentCard.m_TC.transform.localPosition;
			Vector3 vector = this.m_chestScreenPos + Vector3.right * (float)Screen.width * 0.2f + Vector3.up * (float)Screen.height * -0.075f;
			this.m_cardPositionTween = TweenS.AddCurvedTransformTween(this.m_currentCard.m_TC, TweenedProperty.Position, TweenStyle.Linear, vector, localPosition, vector + Vector3.up * 200f, localPosition + Vector3.right * 300f, 1f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_cardPositionTween, delegate(TweenC _c)
			{
				if (effectPrefab2 != null)
				{
					effectPrefab2.p_gameObject.SetActive(true);
				}
				this.CardRevealed(_c);
			});
			this.m_cardScaleTween = TweenS.AddTransformTween(this.m_currentCard.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, new Vector3(0f, 0f, 0f), new Vector3(-1f, 1f, 1f), 0.3f, 0.1f, false);
			TweenS.AddTweenEndEventListener(this.m_cardScaleTween, new TweenEventDelegate(this.FlipCard));
			TransformS.SetScale(this.m_currentCard.m_TC, 0f);
			this.m_cardFlipTimer = TimerS.AddComponent(this.m_currentCard.m_TC.p_entity, "backside", 0.65f, 0f, false, delegate(TimerC _c)
			{
				this.ShowCardFront(_c);
				if (effectPrefab2 != null)
				{
					effectPrefab2.p_gameObject.SetActive(false);
				}
			});
		}
		else
		{
			PsEditorItem psEditorItem = null;
			for (int j = 0; j < PsMetagameData.m_units.Count - 1; j++)
			{
				for (int k = 0; k < PsMetagameData.m_units[j].m_items.Count; k++)
				{
					if (PsMetagameData.m_units[j].m_items[k].m_identifier == this.m_editorItemKeys[this.m_editorItemRevealIndex])
					{
						psEditorItem = PsMetagameData.m_units[j].m_items[k] as PsEditorItem;
					}
				}
			}
			int num3 = PsGachaManager.m_lastGachaRewards.m_editorItems[this.m_editorItemKeys[this.m_editorItemRevealIndex]];
			int editorResourceCount = PsMetagameManager.m_playerStats.GetEditorResourceCount(this.m_editorItemKeys[this.m_editorItemRevealIndex]);
			PsUIEditorItemCard psUIEditorItemCard = new PsUIEditorItemCard(this.m_cardHolder, psEditorItem, false, false);
			psUIEditorItemCard.SetItemCount(0);
			psUIEditorItemCard.SetVerticalAlign(1f);
			psUIEditorItemCard.RemoveTouchAreas();
			this.m_cardHolder.Update();
			this.m_currentCard = psUIEditorItemCard;
			PrefabC effectPrefab = null;
			if (psEditorItem.m_rarity == PsRarity.Epic || psEditorItem.m_rarity == PsRarity.Rare)
			{
				float num4 = (float)Screen.height / 812f;
				GameObject gameObject2;
				if (psEditorItem.m_rarity == PsRarity.Epic)
				{
					gameObject2 = ResourceManager.GetGameObject(RESOURCE.CardRevealEffectEpicPrefab_GameObject);
				}
				else
				{
					gameObject2 = ResourceManager.GetGameObject(RESOURCE.CardRevealEffectRarePrefab_GameObject);
				}
				effectPrefab = PrefabS.AddComponent(psUIEditorItemCard.m_TC, Vector3.zero, gameObject2);
				PrefabS.SetCamera(effectPrefab, psUIEditorItemCard.m_camera);
				effectPrefab.p_gameObject.transform.localScale = new Vector3(num4, num4 * 0.81f, num4);
				effectPrefab.p_gameObject.transform.Translate(new Vector3(0f, 6f * num4, -4f));
				Renderer[] componentsInChildren2 = effectPrefab.p_gameObject.transform.GetComponentsInChildren<Renderer>();
				if (componentsInChildren2 != null)
				{
					for (int l = 0; l < componentsInChildren2.Length; l++)
					{
						if (componentsInChildren2[l].material != null)
						{
							componentsInChildren2[l].material.renderQueue = 2800;
						}
					}
				}
			}
			Vector3 localPosition2 = this.m_currentCard.m_TC.transform.localPosition;
			Vector3 vector2 = this.m_chestScreenPos + Vector3.right * (float)Screen.width * 0.2f;
			this.m_cardPositionTween = TweenS.AddCurvedTransformTween(this.m_currentCard.m_TC, TweenedProperty.Position, TweenStyle.Linear, vector2, localPosition2, vector2 + Vector3.up * 200f, localPosition2 + Vector3.right * 300f, 1f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_cardPositionTween, delegate(TweenC _c)
			{
				if (effectPrefab != null)
				{
					effectPrefab.p_gameObject.SetActive(true);
				}
				this.CardRevealed(_c);
			});
			this.m_cardScaleTween = TweenS.AddTransformTween(this.m_currentCard.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f), 0.3f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_cardScaleTween, delegate(TweenC _c2)
			{
				TweenS.RemoveComponent(this.m_cardScaleTween);
				this.m_cardScaleTween = null;
			});
		}
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x000CBF64 File Offset: 0x000CA364
	public void IncreaseCardCount(TimerC _c)
	{
		if (this.m_currentCard is PsUIUpgradeView.UpgradeItemInfo)
		{
			(this.m_currentCard as PsUIUpgradeView.UpgradeItemInfo).m_resourceBar.Increase(1);
			int num = Math.Min(_c.m_identifier, 49);
			SoundS.PlaySingleShotWithParameter("/Metagame/ChestOpen_Counter", Vector3.zero, "ItemNumber", (float)num, 1f);
		}
		else if (this.m_currentCard is PsUIEditorItemCard)
		{
			(this.m_currentCard as PsUIEditorItemCard).SetItemCount((int)_c.customObject);
		}
		TimerS.RemoveComponent(_c);
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x000CBFF8 File Offset: 0x000CA3F8
	public UIComponent RevealResource(string frame, float _scale = 1f)
	{
		if (frame != string.Empty)
		{
			UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_cardHolder, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(frame, null), true, true);
			uifittedSprite.SetWidth(_scale, RelativeTo.ParentWidth);
			uifittedSprite.SetDepthOffset(-5f);
			uifittedSprite.SetVerticalAlign(1f);
			this.m_cardHolder.Update();
			Vector3 localPosition = uifittedSprite.m_TC.transform.localPosition;
			Vector3 vector = this.m_chestScreenPos + Vector3.right * (float)Screen.width * 0.2f;
			this.m_cardPositionTween = TweenS.AddCurvedTransformTween(uifittedSprite.m_TC, TweenedProperty.Position, TweenStyle.Linear, vector, localPosition, vector + Vector3.up * 200f, localPosition + Vector3.right * 300f, 1f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_cardPositionTween, new TweenEventDelegate(this.CardRevealed));
			this.m_cardScaleTween = TweenS.AddTransformTween(uifittedSprite.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, new Vector3(0f, 0f, 0f), new Vector3(-1f, 1f, 1f), 0.3f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_cardScaleTween, delegate(TweenC _c2)
			{
				TweenS.RemoveComponent(this.m_cardScaleTween);
				this.m_cardScaleTween = null;
			});
			return uifittedSprite;
		}
		return null;
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x000CC160 File Offset: 0x000CA560
	private void FlipCard(TweenC _c)
	{
		TweenS.RemoveComponent(this.m_cardScaleTween);
		this.m_cardScaleTween = null;
		this.m_cardFlipTween = TweenS.AddTransformTween(this.m_currentCard.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, new Vector3(-1f, 1f, 1f), new Vector3(1f, 1f, 1f), 0.5f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_cardFlipTween, delegate(TweenC _c2)
		{
			TweenS.RemoveComponent(this.m_cardFlipTween);
			this.m_cardFlipTween = null;
		});
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x000CC1E1 File Offset: 0x000CA5E1
	private void ShowCardFront(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		this.m_cardFlipTimer = null;
		if (this.m_currentCard is PsUIUpgradeView.UpgradeItemInfo)
		{
			(this.m_currentCard as PsUIUpgradeView.UpgradeItemInfo).ShowFront();
		}
	}

	// Token: 0x06001426 RID: 5158 RVA: 0x000CC210 File Offset: 0x000CA610
	private void CardRevealed(TweenC _c)
	{
		this.m_revealingCard = false;
		TweenS.RemoveComponent(this.m_cardPositionTween);
		this.m_cardPositionTween = null;
		string text = string.Empty;
		string text2 = "+";
		string text3 = PsStrings.Get(StringID.GACHA_OPEN_RESOURCES_LABEL).ToUpper();
		string text4 = "#f1f4d5";
		if (this.m_bonusHatIdentifier != null && !this.m_bonusHatGiven)
		{
			SoundS.PlaySingleShot("/Metagame/ChestOpen_Hat", Vector3.zero, 1f);
			this.m_bonusHatGiven = true;
			PsCustomisationData vehicleCustomisationData = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar));
			PsCustomisationItem itemByIdentifier = vehicleCustomisationData.GetItemByIdentifier(this.m_bonusHatIdentifier);
			text = PsStrings.Get(itemByIdentifier.m_title);
			text2 = string.Empty;
			PsRarity rarity = itemByIdentifier.m_rarity;
			if (rarity != PsRarity.Common)
			{
				if (rarity != PsRarity.Rare)
				{
					if (rarity == PsRarity.Epic)
					{
						text3 = PsStrings.Get(StringID.GACHA_OPEN_EPIC_HAT).ToUpper();
						text4 = "#f387ff";
					}
				}
				else
				{
					text3 = PsStrings.Get(StringID.GACHA_OPEN_RARE_HAT).ToUpper();
					text4 = "#8ef7ff";
				}
			}
			else
			{
				text3 = PsStrings.Get(StringID.GACHA_OPEN_COMMON_HAT).ToUpper();
				text4 = "#cfdce2";
			}
		}
		else if (this.m_bonusMoney > 0 && !this.m_bonusMoneyGiven)
		{
			SoundS.PlaySingleShot("/Metagame/ChestOpen_Coin", Vector3.zero, 1f);
			this.m_bonusMoneyGiven = true;
			text = PsStrings.Get(StringID.TOUR_TOURNAMENT_REWARD);
			text2 += this.m_bonusMoney;
		}
		else if (this.m_boss > 0 && !this.m_bossGiven)
		{
			SoundS.PlaySingleShot("/Metagame/ChestOpen_Hat", Vector3.zero, 1f);
			this.m_bossGiven = true;
			text = PsStrings.Get(StringID.GACHA_OPEN_SABOTAGE);
			text2 = string.Empty;
			text3 = PsStrings.Get(StringID.GACHA_OPEN_SABOTAGE_INFO).ToUpper();
			text4 = "#f1f4d5";
		}
		else if (this.m_coins > 0 && !this.m_coinsGiven)
		{
			SoundS.PlaySingleShot("/Metagame/ChestOpen_Coin", Vector3.zero, 1f);
			this.m_coinsGiven = true;
			text = PsStrings.Get(StringID.GACHA_OPEN_COINS);
			text2 += this.m_coins;
		}
		else if (this.m_gems > 0 && !this.m_gemsGiven)
		{
			SoundS.PlaySingleShot("/Metagame/ChestOpen_Diamond", Vector3.zero, 1f);
			this.m_gemsGiven = true;
			text = PsStrings.Get(StringID.GACHA_OPEN_GEMS);
			text2 += this.m_gems;
			text3 = PsStrings.Get(StringID.GACHA_OPEN_RESOURCES_LABEL).ToUpper();
			text4 = "#f5dff5";
		}
		else if (this.m_nitros > 0 && !this.m_nitrosGiven)
		{
			SoundS.PlaySingleShot("/Metagame/ChestOpen_Nitro", Vector3.zero, 1f);
			this.m_nitrosGiven = true;
			text = PsStrings.Get(StringID.GACHA_OPEN_NITROS).ToUpper();
			text2 += this.m_nitros;
			text3 = PsStrings.Get(StringID.GACHA_OPEN_NITRO_INFO).ToUpper();
			text4 = "#f5dff5";
		}
		else if (!string.IsNullOrEmpty(this.m_hatIdentifier) && !this.m_hatGiven)
		{
			SoundS.PlaySingleShot("/Metagame/ChestOpen_Hat", Vector3.zero, 1f);
			this.m_hatGiven = true;
			PsCustomisationData vehicleCustomisationData2 = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar));
			PsCustomisationItem itemByIdentifier2 = vehicleCustomisationData2.GetItemByIdentifier(this.m_hatIdentifier);
			text = PsStrings.Get(itemByIdentifier2.m_title);
			text2 = string.Empty;
			PsRarity rarity2 = itemByIdentifier2.m_rarity;
			if (rarity2 != PsRarity.Common)
			{
				if (rarity2 != PsRarity.Rare)
				{
					if (rarity2 == PsRarity.Epic)
					{
						text3 = PsStrings.Get(StringID.GACHA_OPEN_EPIC_HAT).ToUpper();
						text4 = "#f387ff";
					}
				}
				else
				{
					text3 = PsStrings.Get(StringID.GACHA_OPEN_RARE_HAT).ToUpper();
					text4 = "#8ef7ff";
				}
			}
			else
			{
				text3 = PsStrings.Get(StringID.GACHA_OPEN_COMMON_HAT).ToUpper();
				text4 = "#cfdce2";
			}
		}
		else if (this.m_upgradeKeys.Count > this.m_upgradeRevealIndex)
		{
			Type type = ((!this.m_upgradeKeys[this.m_upgradeRevealIndex].StartsWith("Car")) ? typeof(Motorcycle) : typeof(OffroadCar));
			string text5 = "/Metagame/ChestOpen_Upgrade";
			PsUpgradeItem upgradeItem = PsUpgradeManager.GetUpgradeItem(type, this.m_upgradeKeys[this.m_upgradeRevealIndex]);
			text = PsStrings.Get(upgradeItem.m_title).ToUpper();
			PsRarity rarity3 = upgradeItem.m_rarity;
			if (rarity3 != PsRarity.Common)
			{
				if (rarity3 != PsRarity.Rare)
				{
					if (rarity3 == PsRarity.Epic)
					{
						text3 = PsStrings.Get(StringID.GACHA_OPEN_EPIC_PART).ToUpper();
						text4 = "#f387ff";
						text5 = "/Metagame/ChestOpen_EpicCard";
					}
				}
				else
				{
					text3 = PsStrings.Get(StringID.GACHA_OPEN_RARE_PART).ToUpper();
					text4 = "#8ef7ff";
					text5 = "/Metagame/ChestOpen_RareCard";
				}
			}
			else
			{
				text3 = PsStrings.Get(StringID.GACHA_OPEN_COMMON_PART).ToUpper();
				text4 = "#cfdce2";
			}
			int num = PsGachaManager.m_lastGachaRewards.m_upgradeItems[this.m_upgradeKeys[this.m_upgradeRevealIndex]];
			text2 += num;
			float num2 = 0f;
			float num3 = 0.15f;
			for (int i = 0; i < num; i++)
			{
				float num4 = num2;
				num2 += num3 * ToolBox.limitBetween(1f - ToolBox.getPositionBetween((float)i, 0f, (float)num), 0.4f, 1f);
				TimerC timerC = TimerS.AddComponent(this.m_currentCard.m_TC.p_entity, string.Empty, num4, 0f, false, new TimerComponentDelegate(this.IncreaseCardCount));
				timerC.m_identifier = i;
			}
			this.m_upgradeRevealIndex++;
			SoundS.PlaySingleShot(text5, Vector3.zero, 1f);
		}
		else
		{
			PsEditorItem psEditorItem = null;
			string text6 = "/Metagame/ChestOpen_Upgrade";
			for (int j = 0; j < PsMetagameData.m_units.Count - 1; j++)
			{
				for (int k = 0; k < PsMetagameData.m_units[j].m_items.Count; k++)
				{
					if (PsMetagameData.m_units[j].m_items[k].m_identifier == this.m_editorItemKeys[this.m_editorItemRevealIndex])
					{
						psEditorItem = PsMetagameData.m_units[j].m_items[k] as PsEditorItem;
					}
				}
			}
			text = PsStrings.Get(psEditorItem.m_name);
			PsRarity rarity4 = psEditorItem.m_rarity;
			if (rarity4 != PsRarity.Common)
			{
				if (rarity4 != PsRarity.Rare)
				{
					if (rarity4 == PsRarity.Epic)
					{
						text3 = PsStrings.Get(StringID.GACHA_OPEN_EPIC_ITEM).ToUpper();
						text4 = "#f387ff";
						text6 = "/Metagame/ChestOpen_EpicCard";
					}
				}
				else
				{
					text3 = PsStrings.Get(StringID.GACHA_OPEN_RARE_ITEM).ToUpper();
					text4 = "#8ef7ff";
					text6 = "/Metagame/ChestOpen_RareCard";
				}
			}
			else
			{
				text3 = PsStrings.Get(StringID.GACHA_OPEN_COMMON_ITEM).ToUpper();
				text4 = "#cfdce2";
			}
			int num5 = PsGachaManager.m_lastGachaRewards.m_editorItems[this.m_editorItemKeys[this.m_editorItemRevealIndex]];
			int editorResourceCount = PsMetagameManager.m_playerStats.GetEditorResourceCount(this.m_editorItemKeys[this.m_editorItemRevealIndex]);
			text2 += num5;
			this.m_editorItemRevealIndex++;
			SoundS.PlaySingleShot(text6, Vector3.zero, 1f);
		}
		UITextbox uitextbox = new UITextbox(this.m_textHolder, false, "title", text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, false, Align.Left, Align.Middle, null, true, "#000000");
		uitextbox.SetMaxRows(2);
		uitextbox.SetHorizontalAlign(0f);
		UITextbox uitextbox2 = new UITextbox(this.m_textHolder, false, string.Empty, text3 + "\n", PsFontManager.GetFont(PsFonts.HurmeBold), 0.035f, RelativeTo.ScreenHeight, false, Align.Left, Align.Middle, text4, true, "#000000");
		uitextbox2.SetHorizontalAlign(0f);
		UIText uitext = new UIText(this.m_textHolder, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.08f, RelativeTo.ScreenHeight, null, "#000000");
		uitext.SetHorizontalAlign(0f);
		this.m_textHolder.Update();
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x000CCA14 File Offset: 0x000CAE14
	public override void Step()
	{
		this.m_chestScreenPos = this.m_chestCanvas.m_3DCamera.WorldToScreenPoint(this.m_chestLocator.transform.position) - Vector3.up * (float)Screen.height * 0.5f;
		if (this.m_cardCounter != null)
		{
			this.m_cardCounter.m_TC.transform.position = this.m_chestScreenPos + Vector3.right * (float)Screen.height * 0.25f + Vector3.up * (float)Screen.height * 0.1f;
		}
		if (this.m_hit)
		{
			this.RevealNextCard();
		}
		base.Step();
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x000CCAE1 File Offset: 0x000CAEE1
	public override void Destroy()
	{
		PsMetagameManager.m_playerStats.updated = true;
		base.Destroy();
		if (this.m_createdBlur)
		{
			CameraS.RemoveBlur();
		}
	}

	// Token: 0x040016C9 RID: 5833
	private PsGachaReward m_rewards;

	// Token: 0x040016CA RID: 5834
	private List<string> m_upgradeKeys;

	// Token: 0x040016CB RID: 5835
	private int m_upgradeRevealIndex;

	// Token: 0x040016CC RID: 5836
	private List<string> m_editorItemKeys;

	// Token: 0x040016CD RID: 5837
	private int m_editorItemRevealIndex;

	// Token: 0x040016CE RID: 5838
	private UIComponent m_currentCard;

	// Token: 0x040016CF RID: 5839
	private UIHorizontalList m_rewardArea;

	// Token: 0x040016D0 RID: 5840
	private VisualsRewardChest m_chestEffects;

	// Token: 0x040016D1 RID: 5841
	private VisualsChestHolder m_holderEffects;

	// Token: 0x040016D2 RID: 5842
	private UICanvas m_cardHolder;

	// Token: 0x040016D3 RID: 5843
	private UIVerticalList m_textHolder;

	// Token: 0x040016D4 RID: 5844
	private UIFittedSprite m_cardCounter;

	// Token: 0x040016D5 RID: 5845
	private UIFittedText m_cardCounterText;

	// Token: 0x040016D6 RID: 5846
	private UI3DRenderTextureCanvas m_chestCanvas;

	// Token: 0x040016D7 RID: 5847
	private Transform m_chestLocator;

	// Token: 0x040016D8 RID: 5848
	private Vector3 m_chestScreenPos;

	// Token: 0x040016D9 RID: 5849
	private int m_boss;

	// Token: 0x040016DA RID: 5850
	private bool m_bossGiven;

	// Token: 0x040016DB RID: 5851
	private int m_coins;

	// Token: 0x040016DC RID: 5852
	private bool m_coinsGiven;

	// Token: 0x040016DD RID: 5853
	private int m_gems;

	// Token: 0x040016DE RID: 5854
	private bool m_gemsGiven;

	// Token: 0x040016DF RID: 5855
	private int m_nitros;

	// Token: 0x040016E0 RID: 5856
	private bool m_nitrosGiven;

	// Token: 0x040016E1 RID: 5857
	private string m_hatIdentifier;

	// Token: 0x040016E2 RID: 5858
	private bool m_hatGiven;

	// Token: 0x040016E3 RID: 5859
	private string m_bonusHatIdentifier;

	// Token: 0x040016E4 RID: 5860
	private bool m_bonusHatGiven;

	// Token: 0x040016E5 RID: 5861
	private int m_bonusMoney;

	// Token: 0x040016E6 RID: 5862
	private bool m_bonusMoneyGiven;

	// Token: 0x040016E7 RID: 5863
	private bool m_createdBlur;

	// Token: 0x040016E8 RID: 5864
	private bool m_firstCard = true;

	// Token: 0x040016E9 RID: 5865
	private TweenC m_cardPositionTween;

	// Token: 0x040016EA RID: 5866
	private TweenC m_cardScaleTween;

	// Token: 0x040016EB RID: 5867
	private TweenC m_cardFlipTween;

	// Token: 0x040016EC RID: 5868
	private TimerC m_cardFlipTimer;

	// Token: 0x040016ED RID: 5869
	private int m_itemCount;

	// Token: 0x040016EE RID: 5870
	private bool m_revealingCard;
}
