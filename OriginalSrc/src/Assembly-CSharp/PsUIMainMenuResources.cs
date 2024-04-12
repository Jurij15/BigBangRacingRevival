using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class PsUIMainMenuResources : UIVerticalList
{
	// Token: 0x060011DA RID: 4570 RVA: 0x000AF774 File Offset: 0x000ADB74
	public PsUIMainMenuResources(Camera _camera, bool _listHorizontally, bool _showCoins, bool _showDiamonds, float _topMargin, bool _showShopButtons, bool _separateShopButton = false, Action _customCloseAction = null, bool _showCopper = false, bool _showShards = false)
		: base(null, "PlayerResources")
	{
		this.m_customShopCloseAction = _customCloseAction;
		this.RemoveDrawHandler();
		this.m_topMargin = _topMargin;
		this.SetMargins(0f, 0.025f, this.m_topMargin, 0f, RelativeTo.ScreenShortest);
		this.SetSpacing(0f, RelativeTo.ScreenShortest);
		this.SetDepthOffset(-200f);
		if (_camera != null)
		{
			this.m_camera = _camera;
		}
		this.m_listHorizontally = _listHorizontally;
		this.m_showCoins = _showCoins;
		this.m_showDiamonds = _showDiamonds;
		this.m_showCopper = _showCopper;
		this.m_showShards = _showShards;
		this.m_showShopButtons = _showShopButtons;
		this.m_showSeparateShopButton = _separateShopButton;
		this.CreateUI();
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x000AF846 File Offset: 0x000ADC46
	public void SetShopActivity(bool _active)
	{
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x000AF848 File Offset: 0x000ADC48
	public void CalculateControlMinMaxValues(float[] _control0, float[] _control1, float[] _height, ref float _min0, ref float _min1, ref float _max0, ref float _max1, ref float _minY, ref float _maxY)
	{
		float[] array = _control0;
		if (array == null)
		{
			array = new float[] { -0.45f, 0.35f };
		}
		float[] array2 = _control1;
		if (array2 == null)
		{
			array2 = new float[] { -0.95f, -0.75f };
		}
		float[] array3 = _height;
		if (array3 == null)
		{
			array3 = new float[] { -0.25f, 0.25f };
		}
		if (array.Length == 1)
		{
			_min0 = (_max0 = array[0]);
		}
		else
		{
			_min0 = array[0];
			_max0 = array[1];
		}
		if (array2.Length == 1)
		{
			_min1 = (_max1 = array2[0]);
		}
		else
		{
			_min1 = array2[0];
			_max1 = array2[1];
		}
		if (array3.Length == 1)
		{
			_minY = (_maxY = array3[0]);
		}
		else
		{
			_minY = array3[0];
			_maxY = array3[1];
		}
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x000AF924 File Offset: 0x000ADD24
	public void CreateFlyingResources(int _amount, Vector2 _startScreenPos, ResourceType _type, float _delay = 0f, TweenEventDelegate _tweenDelegate = null, float[] _control0XMinMax = null, float[] _control1XMinMax = null, float[] _heightMinMax = null, Vector2 _endScreenPos = default(Vector2))
	{
		if ((_type == ResourceType.Coins || _type == ResourceType.Diamonds || _type == ResourceType.Trophies || _type == ResourceType.Gacha || _type == ResourceType.Shards || _type == ResourceType.Map) && _amount > 0)
		{
			TimerComponentDelegate timerComponentDelegate = null;
			string empty = string.Empty;
			UIComponent coinRow = this.m_coinRow;
			this.SetResourceProperties(_type, _amount, ref empty, ref coinRow, ref timerComponentDelegate);
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			this.CalculateControlMinMaxValues(_control0XMinMax, _control1XMinMax, _heightMinMax, ref num, ref num2, ref num3, ref num4, ref num5, ref num6);
			int num7 = _amount;
			if (_amount > 20)
			{
				num7 = 20;
			}
			if (_type != ResourceType.Trophies && _type != ResourceType.Gacha && _type != ResourceType.Map)
			{
				for (int i = 0; i < num7; i++)
				{
					FlyingResource flyingResource = new FlyingResource(_type, 0.06f, RelativeTo.ScreenHeight, _startScreenPos, this.m_flyerCamera);
					Vector3 vector = Vector3.zero;
					if (coinRow != null)
					{
						vector = coinRow.m_camera.WorldToScreenPoint(coinRow.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f + new Vector3(coinRow.m_actualWidth * 0.5f - 0.05f * (float)Screen.height, 0f, 0f);
					}
					else
					{
						vector = _endScreenPos;
					}
					float num8 = Random.Range(num, num3);
					float num9 = Random.Range(num2, num4);
					float num10 = Random.Range(num5, num6);
					Vector3 normalized = (vector - flyingResource.m_TC.transform.localPosition).normalized;
					Vector3 vector2 = Vector3.Cross(normalized, -Vector3.forward);
					Vector3 vector3 = flyingResource.m_TC.transform.localPosition + normalized * (float)Screen.height * num8 + vector2 * (float)Screen.height * num10;
					Vector3 vector4 = vector + normalized * (float)Screen.height * num9 + vector2 * (float)Screen.height * num10;
					float num11 = 1.5f + Random.Range(-0.175f, 0.175f);
					if (i == 0)
					{
						TimerC timerC = TimerS.AddComponent(flyingResource.m_TC.p_entity, string.Empty, num11 - 0.45f, _delay, false, timerComponentDelegate);
						timerC.customObject = _type;
						timerC = TimerS.AddComponent(flyingResource.m_TC.p_entity, string.Empty, 0f, _delay + num11 / 1.5f, false, new TimerComponentDelegate(this.IndicatorCreationDelegate));
						timerC.customObject = new ResourceData
						{
							amount = _amount,
							delay = 0f,
							fadeOutDelay = 1f,
							iconName = empty,
							target = coinRow
						};
					}
					TweenC tweenC = TweenS.AddTransformTween(flyingResource.m_TC, TweenedProperty.Alpha, TweenStyle.CubicIn, Vector3.one, Vector3.zero, 0.3f, num11 / 1.5f + _delay, true);
					TweenS.SetTweenAlphaProperties(tweenC, false, true, false, null);
					tweenC = TweenS.AddCurvedTransformTween(flyingResource.m_TC, TweenedProperty.Position, TweenStyle.CubicOut, vector, vector3, vector4, num11, _delay, false);
					TweenS.AddTweenEndEventListener(tweenC, (_tweenDelegate == null) ? new TweenEventDelegate(this.TweenDelegate) : _tweenDelegate);
					this.m_flyers.Add(flyingResource);
				}
			}
			else
			{
				for (int j = 0; j < num7; j++)
				{
					UIFittedSprite uifittedSprite = new UIFittedSprite(null, false, "FlyingResource", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(empty, null), true, true);
					if (_type == ResourceType.Gacha)
					{
						uifittedSprite.SetSize(0.12f, 0.12f, RelativeTo.ScreenShortest);
					}
					else
					{
						uifittedSprite.SetSize(0.06f, 0.06f, RelativeTo.ScreenShortest);
					}
					uifittedSprite.SetCamera(this.m_flyerCamera, true, false);
					uifittedSprite.Update();
					Vector3 vector5 = _startScreenPos;
					TransformS.SetPosition(uifittedSprite.m_TC, vector5);
					Vector3 vector6 = Vector3.zero;
					if (coinRow != null)
					{
						vector6 = coinRow.m_camera.WorldToScreenPoint(coinRow.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f + new Vector3(coinRow.m_actualWidth * 0.5f - 0.05f * (float)Screen.height, 0f, 0f);
					}
					else
					{
						vector6 = _endScreenPos;
					}
					float num12 = Random.Range(num, num3);
					float num13 = Random.Range(num2, num4);
					float num14 = Random.Range(num5, num6);
					Vector3 normalized2 = (vector6 - uifittedSprite.m_TC.transform.localPosition).normalized;
					Vector3 vector7 = Vector3.Cross(normalized2, -Vector3.forward);
					Vector3 vector8 = uifittedSprite.m_TC.transform.localPosition + normalized2 * (float)Screen.height * num12 + vector7 * (float)Screen.height * num14;
					Vector3 vector9 = vector6 + normalized2 * (float)Screen.height * num13 + vector7 * (float)Screen.height * num14;
					float num15 = 1.5f + Random.Range(-0.175f, 0.175f);
					if (j == 0)
					{
						TimerC timerC2 = TimerS.AddComponent(uifittedSprite.m_TC.p_entity, string.Empty, num15 - 0.45f, _delay, false, timerComponentDelegate);
						timerC2.customObject = _type;
						if (_type != ResourceType.Trophies && _type != ResourceType.Gacha && _type != ResourceType.Map)
						{
							timerC2 = TimerS.AddComponent(uifittedSprite.m_TC.p_entity, string.Empty, 0f, _delay + num15 / 1.5f, false, new TimerComponentDelegate(this.IndicatorCreationDelegate));
							timerC2.customObject = new ResourceData
							{
								amount = _amount,
								delay = 0f,
								fadeOutDelay = 1f,
								iconName = empty,
								target = coinRow
							};
						}
					}
					TweenC tweenC2 = TweenS.AddTransformTween(uifittedSprite.m_TC, TweenedProperty.Alpha, TweenStyle.CubicIn, Vector3.one, Vector3.zero, 0.3f, num15 / 1.5f + _delay, true);
					TweenS.SetTweenAlphaProperties(tweenC2, false, true, false, Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
					tweenC2 = TweenS.AddCurvedTransformTween(uifittedSprite.m_TC, TweenedProperty.Position, TweenStyle.CubicOut, vector6, vector8, vector9, num15, _delay, false);
					TweenS.AddTweenEndEventListener(tweenC2, (_tweenDelegate == null) ? new TweenEventDelegate(this.TweenDelegate) : _tweenDelegate);
					this.m_flyers.Add(uifittedSprite);
				}
			}
		}
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x000B0018 File Offset: 0x000AE418
	public void CreateAddedResources(ResourceType _type, int _amount, float _delay)
	{
		if (_amount != 0 && (_type == ResourceType.Coins || _type == ResourceType.Diamonds || _type == ResourceType.Shards))
		{
			TimerComponentDelegate timerComponentDelegate = null;
			string empty = string.Empty;
			UIComponent coinRow = this.m_coinRow;
			this.SetResourceProperties(_type, _amount, ref empty, ref coinRow, ref timerComponentDelegate);
			this.CreateResourceIndicator(coinRow, _amount, empty, _delay, 0f);
			this.StartCumulate(_type);
		}
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x000B0074 File Offset: 0x000AE474
	private void CreateResourceIndicator(UIComponent _target, int _amount, string _iconName, float _delay, float _fadeOutDelay = 0f)
	{
		if (this.m_flyerCamera == null)
		{
			this.m_flyerCamera = CameraS.AddCamera("FlyerCamera", true, 3);
		}
		float num = 0.5f + _delay + _fadeOutDelay;
		string text = "#81f02b";
		string text2 = "+" + _amount;
		if (_amount < 0)
		{
			text = "#ff793f";
			text2 = _amount.ToString();
		}
		UIHorizontalList uihorizontalList = new UIHorizontalList(null, string.Empty);
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.04f, RelativeTo.ScreenHeight, null, null);
		uitext.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = DebugDraw.HexToColor(text);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(_iconName, null), true, true);
		uifittedSprite.SetSize(0.06f, 0.06f, RelativeTo.ScreenShortest);
		uihorizontalList.SetCamera(this.m_flyerCamera, true, false);
		Vector3 vector = _target.m_camera.WorldToScreenPoint(_target.m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f;
		Vector3 vector2 = vector + new Vector3(0f, (float)Screen.height * 0.05f, 0f);
		if (_amount < 0)
		{
			vector2 = vector - new Vector3(0f, (float)Screen.height * 0.05f, 0f);
		}
		TransformS.SetPosition(uihorizontalList.m_TC, vector);
		uihorizontalList.Update();
		for (int i = 0; i < uihorizontalList.m_childs.Count; i++)
		{
			TweenC tweenC = TweenS.AddTransformTween(uihorizontalList.m_childs[i].m_TC, TweenedProperty.Alpha, TweenStyle.CubicOut, Vector3.one, Vector3.zero, 0.2f, _delay + 0.3f + _fadeOutDelay, true);
			if (i == 0)
			{
				TweenS.SetTweenAlphaProperties(tweenC, false, false, true, Shader.Find("Framework/FontShader"));
			}
			else
			{
				TweenS.SetTweenAlphaProperties(tweenC, false, true, false, Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			}
		}
		TweenC tweenC2 = TweenS.AddTransformTween(uihorizontalList.m_TC, TweenedProperty.Position, TweenStyle.CubicOut, vector, vector2, 0.5f, _delay, false);
		TimerS.AddComponent(uihorizontalList.m_TC.p_entity, string.Empty, num, 0f, false, new TimerComponentDelegate(this.IndicatorTimerDelegate));
		this.m_flyers.Add(uihorizontalList);
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x000B0302 File Offset: 0x000AE702
	private void IndicatorCreationDelegate(TimerC _c)
	{
		this.m_indicatorCreations.Add(_c);
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x000B0310 File Offset: 0x000AE710
	private void IndicatorTimerDelegate(TimerC _c)
	{
		IComponent componentByEntity = EntityManager.GetComponentByEntity(ComponentType.Tween, _c.p_entity);
		if (componentByEntity != null)
		{
			this.m_removeList.Add(componentByEntity as TweenC);
		}
		TimerS.RemoveComponent(_c);
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x000B0348 File Offset: 0x000AE748
	public void SetResourceProperties(ResourceType _type, int _amount, ref string _iconName, ref UIComponent _target, ref TimerComponentDelegate _timerDelegate)
	{
		switch (_type)
		{
		case ResourceType.Coins:
			_iconName = "menu_resources_coin_icon";
			_target = this.m_coinRow;
			this.m_addCoinAmount += _amount;
			this.m_coinAddTimer = 45;
			break;
		case ResourceType.Diamonds:
			_iconName = "menu_resources_diamond_icon";
			_target = this.m_diamondRow;
			this.m_addDiamondAmount += _amount;
			this.m_diamondAddTimer = 45;
			break;
		case ResourceType.Trophies:
			_iconName = "menu_trophy_small_full";
			_target = null;
			break;
		case ResourceType.Gacha:
			_iconName = "menu_chest_badge_active";
			_target = null;
			break;
		case ResourceType.Shards:
			_iconName = "menu_resources_small_shard_icon";
			_target = this.m_shardRow;
			this.m_addShardAmound += _amount;
			this.m_shardAddTimer = 45;
			break;
		case ResourceType.Map:
			_iconName = "hud_map_piece1";
			_target = null;
			break;
		}
		_timerDelegate = new TimerComponentDelegate(this.TimerDelegate);
		if (this.m_flyerCamera == null)
		{
			this.m_flyerCamera = CameraS.AddCamera("FlyerCamera", true, 3);
		}
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x000B045B File Offset: 0x000AE85B
	private void TweenDelegate(TweenC _c)
	{
		this.m_removeList.Add(_c);
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x000B046C File Offset: 0x000AE86C
	private void TimerDelegate(TimerC _c)
	{
		ResourceType resourceType = (ResourceType)_c.customObject;
		this.StartCumulate(resourceType);
		TimerS.RemoveComponent(_c);
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x000B0494 File Offset: 0x000AE894
	public void StartCumulate(ResourceType _type)
	{
		if (_type != ResourceType.Coins)
		{
			if (_type != ResourceType.Diamonds)
			{
				if (_type == ResourceType.Shards)
				{
					this.m_cumulateShards = true;
					this.TweenResourceRow(this.m_shardRow, new Vector3(1.2f, 1.2f, 1.2f), 0f);
				}
			}
			else
			{
				this.m_cumulateDiamonds = true;
				this.TweenResourceRow(this.m_diamondRow, new Vector3(1.2f, 1.2f, 1.2f), 0f);
			}
		}
		else
		{
			this.m_cumulateCoins = true;
			this.TweenResourceRow(this.m_coinRow, new Vector3(1.2f, 1.2f, 1.2f), 0f);
		}
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x000B0550 File Offset: 0x000AE950
	private void CreateUI()
	{
		this.DestroyChildren();
		this.m_coins = null;
		this.m_diamonds = null;
		this.m_shopParent = this;
		if (this.m_listHorizontally)
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
			uihorizontalList.SetSpacing(0.015f, RelativeTo.ScreenShortest);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetHorizontalAlign(1f);
			this.m_shopParent = uihorizontalList;
		}
		if (this.m_showSeparateShopButton)
		{
			UIHorizontalList uihorizontalList2 = new UIHorizontalList(this, string.Empty);
			uihorizontalList2.SetSpacing(0.015f, RelativeTo.ScreenShortest);
			uihorizontalList2.RemoveDrawHandler();
			uihorizontalList2.SetHorizontalAlign(1f);
			uihorizontalList2.SetVerticalAlign(1f);
			this.m_separateShopButton = new PsUIGenericButton(uihorizontalList2, 0.25f, 0.25f, 0.0065f, "Button");
			this.m_separateShopButton.SetSound("/UI/ButtonTransition");
			this.m_separateShopButton.SetMenuButton(PsStrings.Get(StringID.SHOP_BUTTON), "menu_icon_shop", null, false);
			if (PlayerPrefsX.GetShopNotification())
			{
				this.m_separateShopButton.SetNotification(string.Empty);
			}
			this.CreateSpecialOfferTimer();
			UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList2, "ResourceVlist");
			uiverticalList.SetSpacing(0f, RelativeTo.ScreenShortest);
			uiverticalList.SetDepthOffset(-200f);
			uiverticalList.RemoveDrawHandler();
			this.m_shopParent = uiverticalList;
		}
		float num = 0.06f;
		float num2 = 0.05f;
		if (this.m_showCoins)
		{
			this.m_coinRow = new UIHorizontalList(this.m_shopParent, string.Empty);
			this.m_coinRow.SetHorizontalAlign(1f);
			this.m_coinRow.SetHeight(num, RelativeTo.ScreenShortest);
			this.m_coinRow.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.CoinResourceBackground));
			UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_coinRow, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_icon", null), true, true);
			uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_coins = new UIText(this.m_coinRow, false, string.Empty, this.GetCoinCounterString(), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), 0.035f, RelativeTo.ScreenShortest, "#efe431", "000000");
			this.m_coins.SetShadowShift(new Vector2(0.5f, -0.1f), 0.1f);
			this.m_coins.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
			if (this.m_showShopButtons)
			{
				this.m_shopButtonCoins = new PsUIGenericButton(this.m_coinRow, 0.25f, 0.25f, 0.005f, "Button");
				this.m_shopButtonCoins.SetSound("/UI/ButtonTransition");
				this.m_shopButtonCoins.SetIcon("hud_icon_plus", 0.035f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
				this.m_shopButtonCoins.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
				this.m_shopButtonCoins.SetPurpleColors();
				this.m_shopButtonCoins.SetHeight(0.8f, RelativeTo.ParentHeight);
			}
			else
			{
				this.m_coinRow.SetMargins(0f, 0.025f, 0f, 0f, RelativeTo.ScreenShortest);
			}
			if (this.m_listHorizontally)
			{
				this.m_coinRow.MoveToIndexAtParentsChildList((!this.m_showSeparateShopButton) ? 0 : 1);
			}
		}
		if (this.m_showDiamonds)
		{
			this.m_diamondRow = new UIHorizontalList(this.m_shopParent, string.Empty);
			this.m_diamondRow.SetHorizontalAlign(1f);
			this.m_diamondRow.SetHeight(num, RelativeTo.ScreenShortest);
			this.m_diamondRow.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DiamondResourceBackground));
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(this.m_diamondRow, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_diamond_icon", null), true, true);
			uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_diamonds = new UIText(this.m_diamondRow, false, string.Empty, PsMetagameManager.m_playerStats.diamonds.ToString(), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), 0.035f, RelativeTo.ScreenShortest, "#d6a6f2", "000000");
			this.m_diamonds.SetShadowShift(new Vector2(0.5f, -0.1f), 0.1f);
			this.m_diamonds.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
			if (this.m_showShopButtons)
			{
				this.m_shopButtonDiamonds = new PsUIGenericButton(this.m_diamondRow, 0.25f, 0.25f, 0.005f, "Button");
				this.m_shopButtonDiamonds.SetSound("/UI/ButtonTransition");
				this.m_shopButtonDiamonds.SetIcon("hud_icon_plus", 0.035f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
				this.m_shopButtonDiamonds.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
				this.m_shopButtonDiamonds.SetPurpleColors();
				this.m_shopButtonDiamonds.SetHeight(0.8f, RelativeTo.ParentHeight);
			}
			else
			{
				this.m_diamondRow.SetMargins(0f, 0.025f, 0f, 0f, RelativeTo.ScreenShortest);
			}
			if (this.m_listHorizontally)
			{
				this.m_diamondRow.MoveToIndexAtParentsChildList((!this.m_showSeparateShopButton) ? 0 : 1);
			}
		}
		if (this.m_showCopper)
		{
			this.m_copperRow = new UIHorizontalList(this.m_shopParent, string.Empty);
			this.m_copperRow.SetHorizontalAlign(0.9f);
			this.m_copperRow.SetHeight(num2, RelativeTo.ScreenShortest);
			this.m_copperRow.SetMargins(0f, 0.005f, 0f, 0f, RelativeTo.ScreenHeight);
			this.m_copperRow.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.CoinResourceBackground));
			UIFittedSprite uifittedSprite3 = new UIFittedSprite(this.m_copperRow, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_copper_icon", null), true, true);
			uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_copper = new UIText(this.m_copperRow, false, string.Empty, this.GetCopperCounterString(), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), 0.03f, RelativeTo.ScreenShortest, "#f17e34", "000000");
			this.m_copper.SetShadowShift(new Vector2(0.5f, -0.1f), 0.1f);
			this.m_copper.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		}
		if (this.m_showShards)
		{
			this.m_shardRow = new UIHorizontalList(this.m_shopParent, string.Empty);
			this.m_shardRow.SetHorizontalAlign(0.9f);
			this.m_shardRow.SetHeight(num2, RelativeTo.ScreenShortest);
			this.m_shardRow.SetMargins(0f, 0.005f, 0f, 0f, RelativeTo.ScreenHeight);
			this.m_shardRow.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DiamondResourceBackground));
			UIFittedSprite uifittedSprite4 = new UIFittedSprite(this.m_shardRow, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_small_shard_icon", null), true, true);
			uifittedSprite4.SetHeight(1f, RelativeTo.ParentHeight);
			this.m_shards = new UIText(this.m_shardRow, false, string.Empty, this.GetShardCounterString(), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), 0.03f, RelativeTo.ScreenShortest, "#d6a6f2", "000000");
			this.m_shards.SetShadowShift(new Vector2(0.5f, -0.1f), 0.1f);
			this.m_shards.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		}
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x000B0D1C File Offset: 0x000AF11C
	public void CreateSpecialOfferTimer()
	{
		List<PsTimedSpecialOffer> startedTimedSpecialOffers = PsMetagameManager.GetStartedTimedSpecialOffers();
		if (startedTimedSpecialOffers != null && startedTimedSpecialOffers.Count > 0 && this.m_separateShopButton != null)
		{
			this.RemoveSpecialOfferUI();
			this.m_specialOfferShine = new UISprite(this.m_separateShopButton, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shine", null), true);
			this.m_specialOfferShine.SetRogue();
			this.m_specialOfferShine.SetSize(0.2f, 0.2f, RelativeTo.ScreenHeight);
			this.m_specialOfferShine.SetDepthOffset(-1.2f);
			this.m_specialOfferShineRotationTween = TweenS.AddTransformTween(this.m_specialOfferShine.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, -360f), 19f, 0f, false);
			this.m_specialOfferShineRotationTween.repeats = -1;
			this.m_specialOfferShineScaleTween = TweenS.AddTransformTween(this.m_specialOfferShine.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(1.2f, 1.2f, 1f), 1.6f, 0f, false);
			TweenS.AddTweenEndEventListener(this.m_specialOfferShineScaleTween, new TweenEventDelegate(this.SpecialOfferShineScaleSmallerTween));
			this.m_activeSpecialOffer = startedTimedSpecialOffers[0];
			this.m_specialOfferTimerContainer = new UICanvas(this.m_separateShopButton, false, string.Empty, null, string.Empty);
			this.m_specialOfferTimerContainer.SetSize(0.22f, 0.078f, RelativeTo.ScreenHeight);
			this.m_specialOfferTimerContainer.SetMargins(0f, 0f, 1f, -1f, RelativeTo.OwnHeight);
			this.m_specialOfferTimerContainer.SetRogue();
			this.m_specialOfferTimerContainer.RemoveDrawHandler();
			UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_specialOfferTimerContainer, string.Empty);
			uihorizontalList.SetHeight(0.55f, RelativeTo.ParentHeight);
			uihorizontalList.SetVerticalAlign(0f);
			uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SpeechBubbleSpecialOffer));
			UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas.SetSize(1f, 1f, RelativeTo.ParentHeight);
			uicanvas.SetMargins(-0.15f, RelativeTo.OwnHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_special_offer_icon", null), true, true);
			uifittedSprite.SetDepthOffset(-5f);
			UICanvas uicanvas2 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas2.RemoveDrawHandler();
			uicanvas2.SetSize(4f, 0.7f, RelativeTo.ParentHeight);
			uicanvas2.SetMargins(0.2f, 0.2f, 0f, 0f, RelativeTo.OwnHeight);
			this.m_specialOfferTimerSeconds = Mathf.CeilToInt((float)this.m_activeSpecialOffer.m_timeLeft);
			string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_specialOfferTimerSeconds, true, true);
			this.m_specialOfferTimer = new UIFittedText(uicanvas2, false, string.Empty, timeStringFromSeconds, PsFontManager.GetFont(PsFonts.KGSecondChancesMN), true, "#FC1368", null);
		}
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x000B1008 File Offset: 0x000AF408
	public void CumulateTimerAndAmount(ref int _amount, ref int _timer, ref UIText _text, ref UIHorizontalList _row, ref bool _updateAll, ref bool _cumulateBool, int _maxVal = 0)
	{
		if (_timer > 0 && _amount != 0 && _cumulateBool)
		{
			_updateAll = false;
			int num = 0;
			if (Mathf.Abs(_amount) > _timer)
			{
				num = _amount / _timer;
			}
			else if (_timer % _amount == 0)
			{
				num = ((_amount <= 0) ? (-1) : 1);
			}
			int num2 = Convert.ToInt32(_text.m_text);
			int num3 = Mathf.FloorToInt(Mathf.Log10((float)num2) + 1f);
			int num4 = Mathf.FloorToInt(Mathf.Log10((float)(num2 + num)) + 1f);
			if (_maxVal > 0)
			{
				if (num2 + num > _maxVal)
				{
					this.m_cumulateResourceDidWrap = true;
				}
				_text.SetText(ToolBox.getRolledValue(num2 + num, 0, _maxVal).ToString());
			}
			else
			{
				_text.SetText((num2 + num).ToString());
			}
			_amount -= num;
			_timer--;
			if (num3 != num4)
			{
				if (this.m_listHorizontally)
				{
					this.m_shopParent.Update();
					this.ArrangeContents();
				}
				else
				{
					_row.Update();
					this.m_shopParent.ArrangeContents();
				}
			}
		}
		else
		{
			this.TweenResourceRow(_row, Vector3.one, 0.15f);
			_cumulateBool = false;
		}
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x000B1158 File Offset: 0x000AF558
	public void TweenResourceRow(UIComponent _c, Vector3 _endValue, float _delay = 0f)
	{
		_c.Hide();
		for (int i = 0; i < _c.m_childs.Count; i++)
		{
			_c.m_childs[i].Hide();
		}
		TweenC tweenC = TweenS.AddTransformTween(_c.m_TC, TweenedProperty.Scale, TweenStyle.QuadInOut, _endValue, 0.1f, _delay, true);
		if (_endValue == Vector3.one)
		{
			TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.TweenBackEventhandler));
			tweenC.customObject = _c;
		}
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x000B11D8 File Offset: 0x000AF5D8
	private void TweenBackEventhandler(TweenC _c)
	{
		UIComponent uicomponent = _c.customObject as UIComponent;
		if (uicomponent != null && uicomponent.m_TC.p_entity.m_visible && uicomponent.m_TC.transform.localScale == Vector3.one)
		{
			uicomponent.Show();
			for (int i = 0; i < uicomponent.m_childs.Count; i++)
			{
				uicomponent.m_childs[i].Show();
			}
			uicomponent.d_Draw(uicomponent);
		}
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x000B126C File Offset: 0x000AF66C
	public void CumulateResources()
	{
		if (this.m_cumulateCoins || this.m_cumulateDiamonds || this.m_cumulateShards)
		{
			bool flag = true;
			if (this.m_cumulateCoins)
			{
				this.CumulateTimerAndAmount(ref this.m_addCoinAmount, ref this.m_coinAddTimer, ref this.m_coins, ref this.m_coinRow, ref flag, ref this.m_cumulateCoins, 0);
			}
			if (this.m_cumulateDiamonds)
			{
				this.CumulateTimerAndAmount(ref this.m_addDiamondAmount, ref this.m_diamondAddTimer, ref this.m_diamonds, ref this.m_diamondRow, ref flag, ref this.m_cumulateDiamonds, 0);
			}
			if (this.m_cumulateShards)
			{
				this.CumulateTimerAndAmount(ref this.m_addShardAmound, ref this.m_shardAddTimer, ref this.m_shards, ref this.m_shardRow, ref flag, ref this.m_cumulateShards, 99);
			}
			if (flag && this.m_flyers.Count == 0)
			{
				this.ResetCumulateValues();
			}
		}
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x000B1350 File Offset: 0x000AF750
	private void SpecialOfferShineScaleBiggerTween(TweenC _tweenC)
	{
		if (this.m_specialOfferShineScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_specialOfferShineScaleTween);
			this.m_specialOfferShineScaleTween = null;
		}
		this.m_specialOfferShineScaleTween = TweenS.AddTransformTween(this.m_specialOfferShine.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(1.1f, 1.1f, 1f), 1.6f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_specialOfferShineScaleTween, new TweenEventDelegate(this.SpecialOfferShineScaleSmallerTween));
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x000B13C8 File Offset: 0x000AF7C8
	private void SpecialOfferShineScaleSmallerTween(TweenC _tweenC)
	{
		if (this.m_specialOfferShineScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_specialOfferShineScaleTween);
			this.m_specialOfferShineScaleTween = null;
		}
		this.m_specialOfferShineScaleTween = TweenS.AddTransformTween(this.m_specialOfferShine.m_TC, TweenedProperty.Scale, TweenStyle.Linear, new Vector3(0.9f, 0.9f, 1f), 1.6f, 0f, false);
		TweenS.AddTweenEndEventListener(this.m_specialOfferShineScaleTween, new TweenEventDelegate(this.SpecialOfferShineScaleBiggerTween));
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x000B1440 File Offset: 0x000AF840
	private void RemoveSpecialOfferUI()
	{
		if (this.m_specialOfferTimerContainer != null)
		{
			this.m_specialOfferTimerContainer.Destroy();
			this.m_specialOfferTimerContainer = null;
			this.m_specialOfferTimer = null;
		}
		if (this.m_specialOfferShine != null)
		{
			this.RemoveSpecialOfferTweens();
			this.m_specialOfferShine.Destroy();
			this.m_specialOfferShine = null;
		}
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x000B1494 File Offset: 0x000AF894
	private void RemoveSpecialOfferTweens()
	{
		if (this.m_specialOfferShineRotationTween != null)
		{
			TweenS.RemoveComponent(this.m_specialOfferShineRotationTween);
			this.m_specialOfferShineRotationTween = null;
		}
		if (this.m_specialOfferShineScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_specialOfferShineScaleTween);
			this.m_specialOfferShineScaleTween = null;
		}
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x000B14D0 File Offset: 0x000AF8D0
	public override void Step()
	{
		if (this.m_camera == null)
		{
			this.SetCamera(CameraS.m_uiCamera, true, false);
		}
		if (this.m_activeSpecialOffer != null)
		{
			int num = Mathf.CeilToInt((float)this.m_activeSpecialOffer.m_timeLeft);
			if (this.m_specialOfferTimer != null && num != this.m_specialOfferTimerSeconds)
			{
				this.m_specialOfferTimerSeconds = num;
				string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_specialOfferTimerSeconds, true, true);
				this.m_specialOfferTimer.SetText(timeStringFromSeconds);
			}
			if (this.m_specialOfferTimerSeconds <= 0 || this.m_activeSpecialOffer.m_state == 2)
			{
				this.m_activeSpecialOffer = null;
				if (this.m_activeSpecialOffer == null)
				{
					this.RemoveSpecialOfferUI();
				}
			}
		}
		if (this.m_indicatorCreations.Count > 0)
		{
			for (int i = 0; i < this.m_indicatorCreations.Count; i++)
			{
				ResourceData resourceData = (ResourceData)this.m_indicatorCreations[i].customObject;
				this.CreateResourceIndicator(resourceData.target, resourceData.amount, resourceData.iconName, resourceData.delay, resourceData.fadeOutDelay);
				TimerS.RemoveComponent(this.m_indicatorCreations[i]);
			}
			this.m_indicatorCreations.Clear();
		}
		if (this.m_removeList.Count > 0)
		{
			for (int j = this.m_removeList.Count - 1; j >= 0; j--)
			{
				for (int k = this.m_flyers.Count - 1; k >= 0; k--)
				{
					FlyingResource flyingResource = this.m_flyers[k] as FlyingResource;
					UIComponent uicomponent = this.m_flyers[k] as UIComponent;
					if (flyingResource != null && this.m_removeList[j].p_entity == flyingResource.m_entity)
					{
						flyingResource.Destroy();
						this.m_flyers.RemoveAt(k);
						this.m_removeList.RemoveAt(j);
						break;
					}
					if (uicomponent != null && this.m_removeList[j].p_entity == uicomponent.m_TC.p_entity)
					{
						uicomponent.Destroy();
						this.m_flyers.RemoveAt(k);
						this.m_removeList.RemoveAt(j);
						break;
					}
				}
			}
		}
		this.CumulateResources();
		if (this.m_flyers.Count == 0 && this.m_flyerCamera != null)
		{
			CameraS.RemoveCamera(this.m_flyerCamera);
			this.m_flyerCamera = null;
		}
		bool flag = Main.m_currentGame.m_sceneManager.GetCurrentScene() is PsMenuScene && Main.m_currentGame.m_sceneManager.GetCurrentScene().GetCurrentState() is PsMainMenuState && PsState.m_activeGameLoop != null;
		if (!flag && this.m_shopButtonDiamonds != null && this.m_shopButtonDiamonds.m_hit && this.m_shop == null)
		{
			this.SetLastView();
			if (this.m_customShopOpenAction != null)
			{
				PsUICenterShopAll.m_scrollIndex = 1;
				this.m_customShopOpenAction.Invoke();
			}
			else if (Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() is PsMainMenuState)
			{
				PsUIBaseState psUIBaseState = new PsUIBaseState(typeof(PsUICenterShopAll), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
				psUIBaseState.SetAction("Exit", delegate
				{
					Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
				});
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState);
				PsUICenterShopAll.m_scrollIndex = 1;
			}
			else
			{
				this.m_shop = new PsUIBasePopup(typeof(PsUICenterShopAll), typeof(PsUITopBackButton), null, null, true, true, InitialPage.Center, false, false, false);
				this.m_shop.SetAction("Exit", new Action(this.CloseShop));
				(this.m_shop.m_mainContent as PsUICenterShopAll).ScrollToGemShop();
				this.m_shop.Step();
			}
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
		}
		if (!flag && this.m_shopButtonCoins != null && this.m_shopButtonCoins.m_hit && this.m_shop == null)
		{
			this.SetLastView();
			if (this.m_customShopOpenAction != null)
			{
				PsUICenterShopAll.m_scrollIndex = 2;
				this.m_customShopOpenAction.Invoke();
			}
			else if (Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() is PsMainMenuState)
			{
				PsUIBaseState psUIBaseState2 = new PsUIBaseState(typeof(PsUICenterShopAll), typeof(PsUITopBackButton), null, null, false, InitialPage.Center);
				psUIBaseState2.SetAction("Exit", delegate
				{
					Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
				});
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState2);
				PsUICenterShopAll.m_scrollIndex = 2;
			}
			else
			{
				this.m_shop = new PsUIBasePopup(typeof(PsUICenterShopAll), typeof(PsUITopBackButton), null, null, true, true, InitialPage.Center, false, false, false);
				this.m_shop.SetAction("Exit", new Action(this.CloseShop));
				(this.m_shop.m_mainContent as PsUICenterShopAll).ScrollToCoinShop();
				this.m_shop.Step();
			}
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
		}
		base.Step();
		if (PsMetagameManager.m_playerStats.updated)
		{
			bool flag2 = false;
			List<UIComponent> list = new List<UIComponent>();
			if (this.m_showCoins && this.m_coins.m_TC.p_entity.m_visible)
			{
				flag2 = true;
				string coinCounterString = this.GetCoinCounterString();
				if (this.CheckUpdateNeed(ref list, ref this.m_coinRow, this.m_coins.m_text, coinCounterString))
				{
					this.m_coins.SetText(coinCounterString);
				}
			}
			if (this.m_showCopper && this.m_copper.m_TC.p_entity.m_visible)
			{
				flag2 = true;
				string copperCounterString = this.GetCopperCounterString();
				if (this.CheckUpdateNeed(ref list, ref this.m_copperRow, this.m_copper.m_text, copperCounterString))
				{
					this.m_copper.SetText(copperCounterString);
				}
			}
			if (this.m_showShards && this.m_shards.m_TC.p_entity.m_visible)
			{
				flag2 = true;
				string shardCounterString = this.GetShardCounterString();
				if (this.CheckUpdateNeed(ref list, ref this.m_shardRow, this.m_shards.m_text, shardCounterString))
				{
					this.m_shards.SetText(shardCounterString);
				}
			}
			if (this.m_showDiamonds && this.m_diamonds.m_TC.p_entity.m_visible)
			{
				flag2 = true;
				string text = PsMetagameManager.m_playerStats.diamonds.ToString();
				if (this.CheckUpdateNeed(ref list, ref this.m_diamondRow, this.m_diamonds.m_text, text))
				{
					this.m_diamonds.SetText(text);
				}
			}
			if (flag2)
			{
				if (list.Count != 0)
				{
					if (this.m_listHorizontally)
					{
						this.m_shopParent.Update();
						this.ArrangeContents();
					}
					else
					{
						for (int l = 0; l < list.Count; l++)
						{
							list[l].Update();
						}
						this.m_shopParent.ArrangeContents();
					}
				}
				this.ResetCumulateValues();
			}
			PsMetagameManager.m_playerStats.updated = false;
		}
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x000B1CD8 File Offset: 0x000B00D8
	public string GetCoinCounterString()
	{
		return PsMetagameManager.m_playerStats.coins.ToString();
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x000B1D00 File Offset: 0x000B0100
	public string GetCopperCounterString()
	{
		string text = PsMetagameManager.m_playerStats.copper.ToString();
		if (text.Length == 1)
		{
			text = text.PadLeft(2, ' ');
		}
		return text;
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x000B1D40 File Offset: 0x000B0140
	public string GetShardCounterString()
	{
		string text = PsMetagameManager.m_playerStats.shards.ToString();
		if (text.Length == 1)
		{
			text = text.PadLeft(2, ' ');
		}
		return text;
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x000B1D80 File Offset: 0x000B0180
	public LastResourceView SetLastView()
	{
		this.m_lastView = new LastResourceView();
		this.m_lastView.coins = this.m_showCoins;
		this.m_lastView.diamonds = this.m_showDiamonds;
		this.m_lastView.shards = this.m_showShards;
		this.m_lastView.horizontal = this.m_listHorizontally;
		this.m_lastView.shopButtons = this.m_showShopButtons;
		this.m_lastView.separateShopButton = this.m_showSeparateShopButton;
		this.m_lastView.margin = this.m_topMargin;
		this.m_lastView.camera = this.m_camera;
		this.m_lastView.m_shopOpenAction = this.m_customShopOpenAction;
		this.m_lastView.m_shopCloseAction = this.m_customShopCloseAction;
		return this.m_lastView;
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x000B1E48 File Offset: 0x000B0248
	public bool CheckUpdateNeed(ref List<UIComponent> _addList, ref UIHorizontalList _add, string _oldValue, string _newValue)
	{
		if (!_oldValue.Equals(_newValue))
		{
			_addList.Add(_add);
			return true;
		}
		return false;
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x000B1E64 File Offset: 0x000B0264
	private void ResetCumulateValues()
	{
		this.m_cumulateCoins = (this.m_cumulateShards = (this.m_cumulateDiamonds = false));
		this.m_addCoinAmount = (this.m_addShardAmound = (this.m_addDiamondAmount = 0));
		this.m_coinAddTimer = (this.m_addShardAmound = (this.m_diamondAddTimer = 0));
		this.m_cumulateResourceDidWrap = false;
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x000B1EC4 File Offset: 0x000B02C4
	private void CloseShop()
	{
		if (this.m_createdBlur)
		{
			CameraS.RemoveBlur();
		}
		this.m_createdBlur = false;
		this.m_shop.Destroy();
		this.m_shop = null;
		if (this.m_customShopCloseAction != null)
		{
			this.m_customShopCloseAction.Invoke();
		}
		this.ShowLastView(this.m_lastView);
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x000B1F1C File Offset: 0x000B031C
	public void ShowLastView(LastResourceView m_lastView)
	{
		Camera camera = m_lastView.camera;
		bool horizontal = m_lastView.horizontal;
		bool coins = m_lastView.coins;
		bool diamonds = m_lastView.diamonds;
		bool separateShopButton = m_lastView.separateShopButton;
		float margin = m_lastView.margin;
		bool shopButtons = m_lastView.shopButtons;
		bool shards = m_lastView.shards;
		PsMetagameManager.ShowResources(camera, horizontal, coins, diamonds, separateShopButton, margin, shopButtons, false, shards);
		PsMetagameManager.m_menuResourceView.m_customShopOpenAction = m_lastView.m_shopOpenAction;
		PsMetagameManager.m_menuResourceView.m_customShopCloseAction = m_lastView.m_shopCloseAction;
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x000B1F98 File Offset: 0x000B0398
	public override void Destroy()
	{
		this.RemoveSpecialOfferTweens();
		this.m_indicatorCreations.Clear();
		if (this.m_flyers.Count > 0)
		{
			for (int i = this.m_flyers.Count - 1; i >= 0; i--)
			{
				FlyingResource flyingResource = this.m_flyers[i] as FlyingResource;
				UIComponent uicomponent = this.m_flyers[i] as UIComponent;
				if (flyingResource != null)
				{
					flyingResource.Destroy();
				}
				else if (uicomponent != null)
				{
					uicomponent.Destroy();
				}
				this.m_flyers.RemoveAt(i);
			}
			this.m_removeList.Clear();
		}
		if (this.m_flyerCamera != null)
		{
			CameraS.RemoveCamera(this.m_flyerCamera);
			this.m_flyerCamera = null;
		}
		base.Destroy();
	}

	// Token: 0x040014E8 RID: 5352
	public bool m_listHorizontally;

	// Token: 0x040014E9 RID: 5353
	public bool m_showCoins;

	// Token: 0x040014EA RID: 5354
	public bool m_showCopper;

	// Token: 0x040014EB RID: 5355
	public bool m_showDiamonds;

	// Token: 0x040014EC RID: 5356
	public bool m_showShards;

	// Token: 0x040014ED RID: 5357
	public bool m_showShopButtons;

	// Token: 0x040014EE RID: 5358
	public UIText m_coins;

	// Token: 0x040014EF RID: 5359
	public UIText m_copper;

	// Token: 0x040014F0 RID: 5360
	public UIText m_shards;

	// Token: 0x040014F1 RID: 5361
	public UIText m_diamonds;

	// Token: 0x040014F2 RID: 5362
	public PsUIGenericButton m_shopButtonDiamonds;

	// Token: 0x040014F3 RID: 5363
	public PsUIGenericButton m_shopButtonCoins;

	// Token: 0x040014F4 RID: 5364
	public PsUIGenericButton m_separateShopButton;

	// Token: 0x040014F5 RID: 5365
	public PsUIBasePopup m_shop;

	// Token: 0x040014F6 RID: 5366
	private UIHorizontalList m_coinRow;

	// Token: 0x040014F7 RID: 5367
	private UIHorizontalList m_copperRow;

	// Token: 0x040014F8 RID: 5368
	private UIHorizontalList m_diamondRow;

	// Token: 0x040014F9 RID: 5369
	private UIHorizontalList m_shardRow;

	// Token: 0x040014FA RID: 5370
	public UIComponent m_shopParent;

	// Token: 0x040014FB RID: 5371
	private UIFittedText m_rewind;

	// Token: 0x040014FC RID: 5372
	public List<object> m_flyers = new List<object>();

	// Token: 0x040014FD RID: 5373
	public List<TweenC> m_removeList = new List<TweenC>();

	// Token: 0x040014FE RID: 5374
	public List<TimerC> m_indicatorCreations = new List<TimerC>();

	// Token: 0x040014FF RID: 5375
	public Camera m_flyerCamera;

	// Token: 0x04001500 RID: 5376
	public float m_topMargin;

	// Token: 0x04001501 RID: 5377
	private int m_addCoinAmount;

	// Token: 0x04001502 RID: 5378
	private int m_addDiamondAmount;

	// Token: 0x04001503 RID: 5379
	public int m_addShardAmound;

	// Token: 0x04001504 RID: 5380
	private int m_coinAddTimer;

	// Token: 0x04001505 RID: 5381
	private int m_diamondAddTimer;

	// Token: 0x04001506 RID: 5382
	private int m_shardAddTimer;

	// Token: 0x04001507 RID: 5383
	private bool m_cumulateCoins;

	// Token: 0x04001508 RID: 5384
	private bool m_cumulateDiamonds;

	// Token: 0x04001509 RID: 5385
	private bool m_cumulateShards;

	// Token: 0x0400150A RID: 5386
	public Action m_customShopCloseAction;

	// Token: 0x0400150B RID: 5387
	public Action m_customShopOpenAction;

	// Token: 0x0400150C RID: 5388
	private LastResourceView m_lastView;

	// Token: 0x0400150D RID: 5389
	private bool m_createdBlur;

	// Token: 0x0400150E RID: 5390
	public bool m_showSeparateShopButton;

	// Token: 0x0400150F RID: 5391
	public bool m_cumulateResourceDidWrap;

	// Token: 0x04001510 RID: 5392
	private UICanvas m_specialOfferTimerContainer;

	// Token: 0x04001511 RID: 5393
	private PsTimedSpecialOffer m_activeSpecialOffer;

	// Token: 0x04001512 RID: 5394
	private UIFittedText m_specialOfferTimer;

	// Token: 0x04001513 RID: 5395
	private int m_specialOfferTimerSeconds;

	// Token: 0x04001514 RID: 5396
	private UISprite m_specialOfferShine;

	// Token: 0x04001515 RID: 5397
	private TweenC m_specialOfferShineRotationTween;

	// Token: 0x04001516 RID: 5398
	private TweenC m_specialOfferShineScaleTween;
}
