using System;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class PsUIWinOpponents : PsUIOpponents
{
	// Token: 0x0600157D RID: 5501 RVA: 0x000DE374 File Offset: 0x000DC774
	public PsUIWinOpponents(UIComponent _parent)
		: base(_parent)
	{
		this.SetSpacing(0.0785f, RelativeTo.ScreenHeight);
		this.m_winScreen = true;
		this.GetLoopData();
		this.m_playerStartPos = this.m_startPosition;
		if (this.m_playerStartPos <= 0)
		{
			if (this.m_raceGhostCount > 2)
			{
				this.m_playerStartPos = 4;
			}
			else if (this.m_raceGhostCount > 1)
			{
				this.m_playerStartPos = 3;
			}
			else
			{
				this.m_playerStartPos = 2;
			}
		}
		if (this.m_currentPos <= 0)
		{
			if (this.m_raceGhostCount > 2)
			{
				this.m_currentPos = 4;
			}
			else if (this.m_raceGhostCount > 1)
			{
				this.m_currentPos = 3;
			}
			else
			{
				this.m_currentPos = 2;
			}
		}
		this.m_moveAmount = this.m_playerStartPos - this.m_currentPos;
		this.m_playerCurrentPos = this.m_playerStartPos;
		base.AddPlayer(this.m_playerStartPos);
		for (int i = 0; i < this.m_profiles.Count; i++)
		{
			this.CreateProfile(this.m_profiles[i], i, false);
		}
		this.CreateTrophyColumn();
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x000DE49C File Offset: 0x000DC89C
	protected virtual void CreateTrophyColumn()
	{
		if (!this.m_ghostWon || (this.m_ghostWon && !this.m_briefingShown))
		{
			this.m_infoList = new UIVerticalList(this, "infoArea");
			this.m_infoList.SetSpacing(this.m_playerSpacing, RelativeTo.ScreenHeight);
			this.m_infoList.SetDrawHandler(new UIDrawDelegate(base.RewardInfoDrawhandler));
			this.m_infoList.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
			this.m_infoList.SetWidth(0.17f, RelativeTo.ScreenHeight);
			for (int i = 0; i < this.m_profiles.Count; i++)
			{
				this.CreateRewardInfo(i);
			}
			this.CreateTriesRibbon(this.m_infoList);
			this.m_rewardHl = new UICanvas(this.m_infoList, false, "RewardHighLight", null, string.Empty);
			this.m_rewardHl.SetRogue();
			this.m_rewardHl.SetHeight(0.085f, RelativeTo.ScreenHeight);
			this.m_rewardHl.SetWidth(0.17f, RelativeTo.ScreenHeight);
			this.m_rewardHl.SetDrawHandler(new UIDrawDelegate(this.HighlightDrawhandler));
		}
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x000DE5C8 File Offset: 0x000DC9C8
	protected virtual void CreateTriesRibbon(UIComponent _parent)
	{
		if (!this.m_ghostWon)
		{
			UICanvas uicanvas = new UICanvas(this.m_infoList, false, string.Empty, null, string.Empty);
			uicanvas.SetWidth(0.23f, RelativeTo.ScreenHeight);
			uicanvas.SetHeight(0.05f, RelativeTo.ScreenHeight);
			uicanvas.SetRogue();
			uicanvas.SetAlign(1f, 1f);
			uicanvas.SetMargins(0.04f, -0.04f, -0.04f, 0.04f, RelativeTo.ScreenHeight);
			uicanvas.SetDepthOffset(-20f);
			uicanvas.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_bg_ribbon", null), true, true);
			uifittedSprite.SetHeight(0.05f, RelativeTo.ScreenHeight);
			uifittedSprite.SetMargins(0.015f, 0.03f, 0.006f, 0.006f, RelativeTo.ScreenHeight);
			uifittedSprite.SetDepthOffset(2f);
			int num = 6 - this.m_heatNumber + this.m_purchasedRuns;
			string text = num + " " + PsStrings.Get(StringID.TRIES);
			if (num == 1)
			{
				text = num + " " + PsStrings.Get(StringID.TRIES_SINGLE);
			}
			UIFittedText uifittedText = new UIFittedText(uifittedSprite, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "000000", null);
		}
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x000DE714 File Offset: 0x000DCB14
	private void HighlightDrawhandler(UIComponent _c)
	{
		float num = 0.625f * (float)Screen.height * 0.02f;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num2 = 0.2f * (float)Screen.height;
		float num3 = _c.m_actualHeight * 0.975f;
		float num4 = (float)Screen.height * 0.0034f;
		Vector2 zero = Vector2.zero;
		Vector2[] array = new Vector2[41];
		Vector2[] arc = DebugDraw.GetArc(num4, 10, 85f, 255f, new Vector2(num2 * 0.5f - num4 - num * 1f, num3 * -0.5f + num4) + zero);
		Vector2[] arc2 = DebugDraw.GetArc(num4, 10, 60f, 180f, new Vector2(num2 * -0.5f + num4 - num * 1f, num3 * -0.5f + num4) + zero);
		Vector2[] arc3 = DebugDraw.GetArc(num4, 10, 85f, 75f, new Vector2(num2 * -0.5f + num4 + num * 0f, num3 * 0.5f - num4) + zero);
		Vector2[] arc4 = DebugDraw.GetArc(num4, 10, 60f, 0f, new Vector2(num2 * 0.5f - num4 + num * 0f, num3 * 0.5f - num4) + zero);
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		arc3.CopyTo(array, 20);
		arc4.CopyTo(array, 30);
		array[array.Length - 1] = arc[0];
		Color color = DebugDraw.HexToColor("#0a4aa0");
		Color color2 = DebugDraw.HexToColor("#0f6ff8");
		Color color3 = DebugDraw.HexToColor("#3891ff");
		Color black = Color.black;
		black.a = 0.75f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f, array, (float)Screen.height * 0.0075f, color3, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 2f + Vector3.down * 0.005f * (float)Screen.height, array, (float)Screen.height * 0.015f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 1f, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000DE9BC File Offset: 0x000DCDBC
	public virtual void CreateRewardInfo(int _index)
	{
		PsGameLoopRacing psGameLoopRacing = PsState.m_activeGameLoop as PsGameLoopRacing;
		PsGameModeRacing psGameModeRacing = psGameLoopRacing.m_gameMode as PsGameModeRacing;
		int num = 0;
		for (int i = 0; i < psGameModeRacing.m_allGhosts.Count; i++)
		{
			if (i < _index)
			{
				num += psGameModeRacing.m_allGhosts[i].trophyLoss;
			}
			else
			{
				num += psGameModeRacing.m_allGhosts[i].trophyWin;
			}
		}
		string text = "a3fd3a";
		string text2 = "+" + num.ToString();
		if (num < 0)
		{
			text = "fd601c";
			text2 = num.ToString();
		}
		UIComponent uicomponent = new UIComponent(this.m_infoList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetSize(0.17f, 0.085f, RelativeTo.ScreenHeight);
		uicomponent.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uicomponent, string.Empty);
		uihorizontalList.SetHeight(0.085f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.RemoveDrawHandler();
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, text, "313131");
		uitext.SetShadowShift(new Vector2(0.5f, -0.1f), 0.05f);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_trophy_trophy", null), true, true);
		uifittedSprite.SetHeight(0.085f, RelativeTo.ScreenHeight);
		if (this.m_profiles[_index].wonAtCreate)
		{
			uitext.m_tmc.m_textMesh.GetComponent<Renderer>().material.shader = Shader.Find("Framework/FontShader");
			uitext.m_shadowtmc.m_textMesh.GetComponent<Renderer>().material.shader = Shader.Find("Framework/FontShader");
			Color white = Color.white;
			white.a = 0f;
			uitext.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = white;
			uitext.m_shadowtmc.m_textMesh.GetComponent<Renderer>().material.color = white;
			uifittedSprite.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			uifittedSprite.SetColor(white);
		}
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x000DEC14 File Offset: 0x000DD014
	public virtual void AnimateOpponents()
	{
		Vector3 localPosition = this.m_parent.m_TC.transform.localPosition;
		TransformS.SetPosition(this.m_parent.m_TC, this.m_parent.m_TC.transform.localPosition + new Vector3((float)Screen.width * 0.5f, 0f, 0f));
		TweenC tweenC = TweenS.AddTransformTween(this.m_parent.m_TC, TweenedProperty.Position, TweenStyle.CubicIn, localPosition, 0.3f, 0f, true);
		tweenC.useUnscaledDeltaTime = true;
		if (this.m_infoList != null)
		{
			for (int i = 0; i < this.m_playerList.m_childs.Count; i++)
			{
				Vector3 localPosition2 = this.m_infoList.m_childs[i].m_TC.transform.localPosition;
				TransformS.SetPosition(this.m_infoList.m_childs[i].m_TC, this.m_infoList.m_childs[i].m_TC.transform.localPosition + new Vector3((float)Screen.width * 0.5f, 0f, 0f));
				tweenC = TweenS.AddTransformTween(this.m_infoList.m_childs[i].m_TC, TweenedProperty.Position, TweenStyle.BackOut, localPosition2, 0.3f, (float)i * 0.2f, true);
				tweenC.useUnscaledDeltaTime = true;
			}
			Vector3 localPosition3 = this.m_rewardHl.m_TC.transform.localPosition;
			TransformS.SetPosition(this.m_rewardHl.m_TC, this.m_rewardHl.m_TC.transform.localPosition + new Vector3((float)Screen.width * 0.5f, 0f, 0f));
			tweenC = TweenS.AddTransformTween(this.m_rewardHl.m_TC, TweenedProperty.Position, TweenStyle.BackOut, localPosition3, 0.3f, (float)(this.m_playerCurrentPos - 1) * 0.2f, true);
			tweenC.useUnscaledDeltaTime = true;
		}
		for (int j = 0; j < this.m_playerList.m_childs.Count; j++)
		{
			Vector3 localPosition4 = this.m_playerList.m_childs[j].m_TC.transform.localPosition;
			TransformS.SetPosition(this.m_playerList.m_childs[j].m_TC, this.m_playerList.m_childs[j].m_TC.transform.localPosition + new Vector3((float)Screen.width * 0.5f, 0f, 0f));
			tweenC = TweenS.AddTransformTween(this.m_playerList.m_childs[j].m_TC, TweenedProperty.Position, TweenStyle.BackOut, localPosition4, 0.3f, (float)j * 0.2f, true);
			tweenC.useUnscaledDeltaTime = true;
			if (j == this.m_playerList.m_childs.Count - 1)
			{
				TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
				{
					this.m_opponentsAnimated = true;
				});
			}
		}
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x000DEF18 File Offset: 0x000DD318
	public void PositionOpponents()
	{
		float num = 0.625f * (float)Screen.height * 0.02f;
		float num2 = num + num * (this.m_playerSpacing / 0.085f);
		for (int i = 0; i < this.m_players.Count; i++)
		{
			Vector3 vector = this.m_players[i].m_TC.transform.localPosition + new Vector3(-num2 * (float)i, 0f, 0f);
			TransformS.SetPosition(this.m_players[i].m_TC, vector);
		}
		if (this.m_infoList != null)
		{
			for (int j = 0; j < this.m_players.Count; j++)
			{
				Vector3 vector2 = this.m_infoList.m_childs[j].m_TC.transform.localPosition + new Vector3(-num2 * (float)j, 0f, 0f);
				TransformS.SetPosition(this.m_infoList.m_childs[j].m_TC, vector2);
			}
			Vector3 vector3 = this.m_infoList.m_childs[0].m_TC.transform.localPosition + new Vector3(-num2 * (float)(this.m_playerCurrentPos - 1), -(0.085f + this.m_playerSpacing) * (float)Screen.height * (float)(this.m_playerCurrentPos - 1), 0f);
			TransformS.SetPosition(this.m_rewardHl.m_TC, vector3);
		}
		for (int k = 0; k < this.m_profiles.Count; k++)
		{
			if (this.m_profiles[k].rival && this.m_profiles[k].won)
			{
				TransformS.SetScale(this.m_players[k].m_winIcon.m_TC, Vector3.zero);
				break;
			}
		}
		this.AnimateOpponents();
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x000DF126 File Offset: 0x000DD526
	protected void GhostWon()
	{
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x000DF128 File Offset: 0x000DD528
	public override void Step()
	{
		this.GetLoopData();
		if (!this.m_moving && this.m_moveAmount > 0 && this.m_opponentsAnimated)
		{
			int num = this.m_playerCurrentPos - 1;
			int num2 = num - 1;
			Vector3 localPosition = this.m_players[num2].m_TC.transform.localPosition;
			Vector3 localPosition2 = this.m_players[num].m_TC.transform.localPosition;
			TweenC tweenC = TweenS.AddTransformTween(this.m_players[num].m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_players[num].m_TC.transform.localPosition, localPosition, 1f, 0f, true);
			tweenC.customObject = num2;
			TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.PlayerMoveEventHandler));
			SoundS.PlaySingleShot("/Ingame/Events/RaceListClimb", Vector3.zero, 1f);
			TweenC tweenC2 = TweenS.AddTransformTween(this.m_players[num2].m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_players[num2].m_TC.transform.localPosition, localPosition2, 1f, 0f, true);
			tweenC2.customObject = num;
			TweenS.AddTweenEndEventListener(tweenC2, new TweenEventDelegate(this.MoveEventHandler));
			if (this.m_rewardHl != null)
			{
				float num3 = 0.625f * (float)Screen.height * 0.02f;
				float num4 = num3 + num3 * (this.m_playerSpacing / 0.085f);
				Vector3 vector = this.m_rewardHl.m_TC.transform.localPosition + new Vector3(num4, (0.085f + this.m_playerSpacing) * (float)Screen.height, 0f);
				TweenS.AddTransformTween(this.m_rewardHl.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, vector, 1f, 0f, true);
			}
			this.m_moving = true;
		}
		else if (this.m_opponentsAnimated && !this.m_moving && this.m_moveAmount == 0 && ((this.m_ghostWon && this.m_rewardTrophies) || (!this.m_ghostWon && PsState.m_activeGameLoop is PsGameLoopRacing && this.m_heatNumber >= 6 + (PsState.m_activeGameLoop as PsGameLoopRacing).m_purchasedRuns) || this.m_nextRacePressed) && this.m_trophiesRewarded != this.m_initialTrophiesRewarded && !this.m_afterSki && this.m_infoList != null)
		{
			this.m_afterSki = true;
			int positionIndex = this.m_playerCurrentPos - 1;
			for (int i = 0; i < this.m_playerList.m_childs.Count; i++)
			{
				if (i < positionIndex)
				{
					this.FadeChildren(this.m_infoList.m_childs[i].m_childs[0] as UIHorizontalList, 0.35f, 0.25f);
				}
			}
			float num5 = 0f;
			if (!this.m_nextRacePressed && this.m_playerStartPos == this.m_playerCurrentPos)
			{
				num5 = 0.5f;
			}
			TimerS.AddComponent(this.m_infoList.m_childs[positionIndex].m_TC.p_entity, string.Empty, 0.35f, num5, false, delegate(TimerC t)
			{
				TimerS.RemoveComponent(t);
				TweenC tweenC3 = TweenS.AddTransformTween(this.m_infoList.m_childs[positionIndex].m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.one * 1.25f, 0.5f, 0f, true);
				TweenS.SetAdditionalTweenProperties(tweenC3, 0, true, TweenStyle.CubicInOut);
				TweenS.AddTransformTween(this.m_infoList.m_childs[positionIndex].m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_infoList.m_childs[positionIndex].m_TC.transform.localPosition + Vector3.up * (float)Screen.height * 0.05f, 1f, 0.5f, true);
				this.FadeChildren(this.m_infoList.m_childs[positionIndex].m_childs[0] as UIHorizontalList, 0.75f, 0.5f);
				TimerS.AddComponent(this.m_infoList.m_childs[positionIndex].m_TC.p_entity, string.Empty, 0.3f, 0f, false, delegate(TimerC c)
				{
					TimerS.RemoveComponent(c);
					this.CreateParticleEffect(this.m_infoList.m_childs[positionIndex].m_TC);
					if ((PsState.m_activeGameLoop as PsGameLoopRacing).m_rewardTrophyAmount > 0)
					{
						SoundS.PlaySingleShot("/InGame/Events/TrophyCollected", Vector3.zero, 1f);
					}
					else
					{
						SoundS.PlaySingleShot("/Ingame/Events/TrophyReduced", Vector3.zero, 1f);
					}
					TweenS.AddTransformTween(this.m_playerList.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_playerList.m_TC.transform.localPosition + new Vector3(0.22f * (float)Screen.height, 0f, 0f), 1f, 1.2f, true);
					TweenC tweenC4 = TweenS.AddTransformTween(this.m_infoList.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_infoList.m_TC.transform.localPosition + new Vector3(0.5f * (float)Screen.height, 0f, 0f), 1f, 1.2f, true);
					if (this.m_nextRacePressed && this.m_nextRaceAction != null)
					{
						TweenS.AddTweenEndEventListener(tweenC4, delegate(TweenC tw)
						{
							this.m_nextRaceAction.Invoke();
						});
					}
				});
			});
		}
		base.Step();
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x000DF4B0 File Offset: 0x000DD8B0
	protected void CreateParticleEffect(TransformC _parent)
	{
		PrefabC prefabC = PrefabS.AddComponent(_parent, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.StarReward0_GameObject));
		prefabC.p_gameObject.transform.position -= new Vector3(0f, 0f, 250f);
		prefabC.p_gameObject.transform.Rotate(90f, 0f, 0f);
		prefabC.p_gameObject.transform.localScale = Vector3.one * 0.5f;
		PrefabS.SetCamera(prefabC, this.m_camera);
		ParticleSystem[] array = prefabC.p_gameObject.GetComponents<ParticleSystem>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Play();
		}
		array = prefabC.p_gameObject.GetComponentsInChildren<ParticleSystem>();
		for (int j = 0; j < array.Length; j++)
		{
			array[j].Play();
		}
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x000DF59C File Offset: 0x000DD99C
	public void FadeChildren(UIHorizontalList _list, float _dur, float _delay)
	{
		for (int i = 0; i < _list.m_childs.Count; i++)
		{
			TweenC tweenC = TweenS.AddTransformTween(_list.m_childs[i].m_TC, TweenedProperty.Alpha, TweenStyle.CubicOut, Vector3.one, Vector3.zero, _dur, _delay, true);
			if (i == 0)
			{
				TweenS.SetTweenAlphaProperties(tweenC, false, false, true, Shader.Find("Framework/FontShader"));
			}
			else
			{
				TweenS.SetTweenAlphaProperties(tweenC, false, true, false, Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			}
		}
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x000DF61C File Offset: 0x000DDA1C
	public virtual void MoveEventHandler(TweenC _c)
	{
		int num = (int)_c.customObject;
		RacerProfile racerProfile = this.m_profiles[num - 1];
		UIFittedSprite positionSprite = this.m_players[num - 1].m_positionSprite;
		positionSprite.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame(base.GetPositionIconName(num), null));
		positionSprite.Update();
		if (racerProfile.rival && PsMetagameManager.m_vehicleGachaData.m_rivalWonCount <= 4 && !(PsState.m_activeGameLoop as PsGameLoopRacing).m_initialGachaFull)
		{
			UIComponent winIcon = this.m_players[num - 1].m_winIcon;
			TweenS.AddTransformTween(winIcon.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.one, 0.35f, 0f, true);
			SoundS.PlaySingleShot("/Metagame/BadgeCollected", Vector3.zero, 1f);
		}
		this.m_players[num - 1].m_childs[0].ArrangeContents();
		this.m_players[num - 1].MoveToIndexAtParentsChildList(num);
		this.MoveProfileToIndex(racerProfile, num);
		this.MovePlayerToIndex(this.m_players[num - 1], num);
		this.m_playerCurrentPos--;
		this.m_moveAmount--;
		this.m_moving = false;
		this.m_players[num].m_childs[0].SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TiltedFadedDrawhandler));
		this.m_players[num].m_childs[0].d_Draw(this.m_players[num].m_childs[0]);
		if (this.m_infoList != null)
		{
			this.FadeChildren(this.m_infoList.m_childs[num].m_childs[0] as UIHorizontalList, 0.35f, 0.25f);
		}
		this.m_players[num - 1].m_childs[0].ArrangeContents();
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x000DF830 File Offset: 0x000DDC30
	public void PlayerMoveEventHandler(TweenC _c)
	{
		int num = (int)_c.customObject;
		UIFittedSprite positionSprite = this.m_players[num + 1].m_positionSprite;
		positionSprite.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame(base.GetPositionIconName(num), null));
		positionSprite.Update();
		this.m_players[num + 1].m_childs[0].ArrangeContents();
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x000DF8A0 File Offset: 0x000DDCA0
	private void IndicateWin(ResourceType _type, int _amount, UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, "FLYER");
		uihorizontalList.SetRogue();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetDepthOffset(-30f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, _amount.ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.04f, RelativeTo.ScreenHeight, null, null);
		uitext.m_tmc.m_textMesh.GetComponent<Renderer>().material.color = DebugDraw.HexToColor((_amount <= 0) ? "#ff793f" : "#81f02b");
		string text = string.Empty;
		if (_type == ResourceType.Trophies)
		{
			text = "menu_trophy_small_full";
		}
		else if (_type == ResourceType.Gacha)
		{
			text = "menu_scoreboard_prize_chest";
		}
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
		uifittedSprite.SetSize(0.06f, 0.06f, RelativeTo.ScreenShortest);
		Vector3 vector;
		vector..ctor(0f, (float)Screen.height * 0.05f, 0f);
		if (_amount < 0)
		{
			vector..ctor(0f, (float)(-(float)Screen.height) * 0.05f, 0f);
		}
		uihorizontalList.Update();
		for (int i = 0; i < uihorizontalList.m_childs.Count; i++)
		{
			TweenC tweenC = TweenS.AddTransformTween(uihorizontalList.m_childs[i].m_TC, TweenedProperty.Alpha, TweenStyle.CubicOut, Vector3.one, Vector3.zero, 0.4f, 0.3f, true);
			if (i == 0)
			{
				TweenS.SetTweenAlphaProperties(tweenC, false, false, true, Shader.Find("Framework/FontShader"));
			}
			else
			{
				TweenS.SetTweenAlphaProperties(tweenC, false, true, false, Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			}
		}
		TweenC tweenC2 = TweenS.AddTransformTween(uihorizontalList.m_TC, TweenedProperty.Position, TweenStyle.CubicOut, vector, 0.75f, 0f, false);
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x000DFA7C File Offset: 0x000DDE7C
	public void MoveProfileToIndex(RacerProfile _profile, int _newIndex)
	{
		this.m_profiles.Remove(_profile);
		this.m_profiles.Insert(_newIndex, _profile);
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x000DFA98 File Offset: 0x000DDE98
	public void MovePlayerToIndex(PsUIRaceProfile _player, int _newIndex)
	{
		this.m_players.Remove(_player);
		this.m_players.Insert(_newIndex, _player);
	}

	// Token: 0x0400184B RID: 6219
	protected bool m_moving;

	// Token: 0x0400184C RID: 6220
	protected int m_playerStartPos = -1;

	// Token: 0x0400184D RID: 6221
	protected int m_moveAmount;

	// Token: 0x0400184E RID: 6222
	protected int m_playerCurrentPos;

	// Token: 0x0400184F RID: 6223
	protected bool m_afterSki;

	// Token: 0x04001850 RID: 6224
	protected bool m_opponentsAnimated;

	// Token: 0x04001851 RID: 6225
	public bool m_nextRacePressed;

	// Token: 0x04001852 RID: 6226
	public Action m_nextRaceAction;

	// Token: 0x04001853 RID: 6227
	private UICanvas m_rewardHl;
}
