using System;
using UnityEngine;

// Token: 0x0200023E RID: 574
public class PsUISkullRiderCharacter
{
	// Token: 0x0600115D RID: 4445 RVA: 0x000A7D18 File Offset: 0x000A6118
	public PsUISkullRiderCharacter(Entity _entity)
	{
		this.m_camera = CameraS.AddCamera("SkullRiderCamera", true, 3);
		CameraS.MoveToBehindOther(this.m_camera, CameraS.m_uiCamera);
		this.m_skullRiderOnScreenPosition = new Vector3((float)Screen.width * 0.45f, (float)(-(float)Screen.height / 2), 0f);
		this.m_skullRiderTC = TransformS.AddComponent(_entity);
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.MenuCharSkullRiderPrefab_GameObject);
		PrefabC prefabC = PrefabS.AddComponent(this.m_skullRiderTC, Vector3.zero, gameObject);
		PrefabS.SetCamera(prefabC, this.m_camera);
		this.m_controller = prefabC.p_gameObject.GetComponent<BossController>();
		TransformS.SetRotation(this.m_skullRiderTC, Vector3.up * 180f);
		TransformS.SetScale(this.m_skullRiderTC, Vector3.one * ((float)Screen.height * 0.0026f));
		TransformS.SetPosition(this.m_skullRiderTC, this.m_skullRiderOnScreenPosition);
		this.m_effectOnScreenPosition = new Vector3(0f, (float)(-(float)Screen.height / 2), 0f);
		this.m_effectTC = TransformS.AddComponent(_entity);
		GameObject gameObject2 = ResourceManager.GetGameObject(RESOURCE.MenuBossGlow_GameObject);
		PrefabC prefabC2 = PrefabS.AddComponent(this.m_effectTC, Vector3.zero, gameObject2);
		PrefabS.SetCamera(prefabC2, this.m_camera);
		TransformS.SetRotation(this.m_effectTC, new Vector3(0f, -90f, 90f));
		Bounds bounds = this.m_effectTC.transform.GetChild(0).GetComponent<Renderer>().bounds;
		Rect rect = this.BoundsToScreenRect(bounds);
		float num = (float)Screen.width / rect.width;
		TransformS.SetScale(this.m_effectTC, Vector3.one * num);
		TransformS.SetPosition(this.m_effectTC, this.m_effectOnScreenPosition);
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x000A7ED8 File Offset: 0x000A62D8
	private Rect BoundsToScreenRect(Bounds bounds)
	{
		Vector3 vector = CameraS.m_uiCamera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.max.y, 0f));
		Vector3 vector2 = CameraS.m_uiCamera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.min.y, 0f));
		return new Rect(vector.x, (float)Screen.height - vector.y, vector2.x - vector.x, vector.y - vector2.y);
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x000A7F88 File Offset: 0x000A6388
	public void ShowSpeechBubble(StringID _text, StringID _title = StringID.EMPTY)
	{
		SoundS.PlaySingleShot("/Ingame/Characters/Dialogue_SkullRider", Vector2.zero, 1f);
		string text = PsStrings.Get(_text);
		this.m_speechBubble = new UIHorizontalList(null, "bubble");
		this.m_speechBubble.SetMargins(0.05f, RelativeTo.ScreenHeight);
		this.m_speechBubble.SetDrawHandler(new UIDrawDelegate(this.BossSpeechBubble));
		this.m_speechBubble.SetAlign(0.55f, 0.75f);
		this.m_speechBubble.SetDepthOffset(100f);
		UITextbox uitextbox = new UITextbox(this.m_speechBubble, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, "#e9eff3", true, null);
		uitextbox.SetWidth(0.6f, RelativeTo.ScreenHeight);
		if (_title != StringID.EMPTY)
		{
			UICanvas uicanvas = new UICanvas(uitextbox, false, string.Empty, null, string.Empty);
			uicanvas.SetRogue();
			uicanvas.SetMargins(0f, 0f, -0.09f, 0f, RelativeTo.ScreenHeight);
			uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
			uicanvas.RemoveDrawHandler();
			UIText uitext = new UIText(uicanvas, false, string.Empty, PsStrings.Get(_title), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.06f, RelativeTo.ScreenHeight, "#ffffff", "#000000");
			uitext.SetVerticalAlign(1f);
		}
		this.m_speechBubble.Update();
		TweenS.AddTransformTween(this.m_speechBubble.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.65f, Vector3.one, 1f, 0f, true);
		TweenS.AddTransformTween(this.m_speechBubble.m_TC, TweenedProperty.Position, TweenStyle.CubicOut, new Vector3((float)Screen.width * 0.1f, (float)Screen.height * 0.1f, 0f), this.m_speechBubble.m_TC.transform.position, 0.5f, 0f, true);
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x000A8160 File Offset: 0x000A6560
	public void Enter()
	{
		this.m_controller.PopIn();
		TimerS.AddComponent(this.m_skullRiderTC.p_entity, string.Empty, 1f, 0f, false, new TimerComponentDelegate(this.EnterDone));
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x000A819C File Offset: 0x000A659C
	private void EnterDone(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		int num = Random.Range(1, 11);
		this.ShowSpeechBubble((StringID)Enum.Parse(typeof(StringID), "BOSS_BATTLE_STARTSCREEN_TAUNT" + num.ToString()), StringID.EMPTY);
		this.m_controller.Talk();
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x000A81F9 File Offset: 0x000A65F9
	public void Talk()
	{
		this.m_controller.Idle();
		TimerS.AddComponent(this.m_skullRiderTC.p_entity, string.Empty, 0.25f, 0f, false, new TimerComponentDelegate(this.TalkDone));
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x000A8234 File Offset: 0x000A6634
	private void TalkDone(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		StringID stringID = StringID.BOSS_BATTLE_STARTSCREEN_TAUNT1;
		if (PsState.m_activeMinigame.m_playerReachedGoal)
		{
			if (PsState.m_activeMinigame.m_collectedStars < 3)
			{
				int num = Random.Range(1, 6);
				stringID = (StringID)Enum.Parse(typeof(StringID), "BOSS_BATTLE_LOSSSCREEN_MISSED" + num.ToString());
			}
			else
			{
				int num2 = Random.Range(2, 9);
				stringID = (StringID)Enum.Parse(typeof(StringID), "BOSS_BATTLE_LOSSSCREEN_TOOSLOW" + num2.ToString());
			}
		}
		this.ShowSpeechBubble(stringID, StringID.BOSS_BATTLE_YOULOST);
		this.m_controller.Talk();
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x000A82F1 File Offset: 0x000A66F1
	public void Taunt()
	{
		this.m_controller.Idle();
		TimerS.AddComponent(this.m_skullRiderTC.p_entity, string.Empty, 0.25f, 0f, false, new TimerComponentDelegate(this.TauntDone));
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x000A832C File Offset: 0x000A672C
	private void TauntDone(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		StringID stringID = StringID.BOSS_BATTLE_STARTSCREEN_TAUNT1;
		switch (PsState.m_lastDeathReason)
		{
		case DeathReason.EJECT:
		case DeathReason.EXPLODED:
		{
			int num = Random.Range(2, 4);
			stringID = (StringID)Enum.Parse(typeof(StringID), "BOSS_BATTLE_LOSSSCREEN_EXPLODED" + num.ToString());
			break;
		}
		case DeathReason.ELECTRIFIED:
		{
			int num = Random.Range(2, 4);
			stringID = (StringID)Enum.Parse(typeof(StringID), "BOSS_BATTLE_LOSSSCREEN_ELECTROCUTED" + num.ToString());
			break;
		}
		case DeathReason.NECKSNAP:
		{
			int num = Random.Range(1, 4);
			stringID = (StringID)Enum.Parse(typeof(StringID), "BOSS_BATTLE_LOSSSCREEN_NECKSNAP" + num.ToString());
			break;
		}
		case DeathReason.BLACK_HOLE:
		{
			int num = Random.Range(1, 4);
			stringID = (StringID)Enum.Parse(typeof(StringID), "BOSS_BATTLE_LOSSSCREEN_BLACK_HOLE" + num.ToString());
			break;
		}
		}
		this.ShowSpeechBubble(stringID, StringID.BOSS_BATTLE_YOULOST);
		this.m_controller.Taunt();
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x000A8468 File Offset: 0x000A6868
	public void Exit(Action _exitDoneAction)
	{
		this.d_exitDoneAction = _exitDoneAction;
		this.m_controller.Idle();
		TimerS.AddComponent(this.m_skullRiderTC.p_entity, string.Empty, 0.25f, 0f, false, new TimerComponentDelegate(this.ExitDone));
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x000A84B4 File Offset: 0x000A68B4
	private void ExitDone(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		int num = Random.Range(1, 11);
		this.ShowSpeechBubble((StringID)Enum.Parse(typeof(StringID), "BOSS_BATTLE_WINSCREEN_TAUNT" + num.ToString()), StringID.BOSS_BATTLE_YOUWON);
		TimerS.AddComponent(this.m_skullRiderTC.p_entity, string.Empty, 1.5f, 0f, false, new TimerComponentDelegate(this.ExitDone2));
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x000A8534 File Offset: 0x000A6934
	private void ExitDone2(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		SoundS.PlaySingleShot("/Ingame/Events/SkullRider_Death", Vector2.zero, 1f);
		this.m_controller.Death();
		TimerS.AddComponent(this.m_skullRiderTC.p_entity, string.Empty, 1.5f, 0f, false, new TimerComponentDelegate(this.ExitDone3));
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x000A8598 File Offset: 0x000A6998
	private void ExitDone3(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		if (this.m_speechBubble != null)
		{
			TweenS.AddTransformTween(this.m_speechBubble.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one, Vector3.zero, 0.25f, 0f, true);
			TweenS.AddTransformTween(this.m_speechBubble.m_TC, TweenedProperty.Position, TweenStyle.CubicOut, new Vector3((float)Screen.width * 0.1f, (float)Screen.height * 0.1f, 0f), 0.25f, 0f, true);
		}
		TimerS.AddComponent(this.m_skullRiderTC.p_entity, string.Empty, 1f, 0f, false, new TimerComponentDelegate(this.ExitDone4));
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x000A864C File Offset: 0x000A6A4C
	private void ExitDone4(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		this.d_exitDoneAction.Invoke();
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x000A8660 File Offset: 0x000A6A60
	private void BossSpeechBubble(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = (float)Screen.height * 0.05f;
		float num2 = (float)Screen.height * 0.025f;
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f - num2, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f + num2, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * 0.3f + num, _c.m_actualHeight * -0.5f - num * 0.5f * 0.3f),
			new Vector2(_c.m_actualWidth * 0.3f + num * 1.1f, _c.m_actualHeight * -0.5f - num * 0.5f * 0.3f - num - num2),
			new Vector2(_c.m_actualWidth * 0.3f + num, _c.m_actualHeight * -0.5f - num * 0.5f * 0.3f - num - num2),
			new Vector2(_c.m_actualWidth * 0.3f, _c.m_actualHeight * -0.5f - num * 0.5f * 0.3f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f - num * 0.5f)
		};
		GGData ggdata = new GGData(array);
		Color color = DebugDraw.HexToColor("#131213");
		Color color2 = DebugDraw.HexToColor("#131213");
		Color color3 = DebugDraw.HexToColor("#b21777");
		Color color4 = DebugDraw.HexToColor("#b21777");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, array, 0.02f * (float)Screen.height, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x000A88DC File Offset: 0x000A6CDC
	public void Destroy()
	{
		if (this.m_camera != null)
		{
			CameraS.RemoveCamera(this.m_camera);
			this.m_camera = null;
		}
		if (this.m_speechBubble != null)
		{
			this.m_speechBubble.Destroy();
			this.m_speechBubble = null;
		}
	}

	// Token: 0x0400144C RID: 5196
	private Vector3 m_skullRiderOnScreenPosition;

	// Token: 0x0400144D RID: 5197
	private Vector3 m_effectOnScreenPosition;

	// Token: 0x0400144E RID: 5198
	public Camera m_camera;

	// Token: 0x0400144F RID: 5199
	private TransformC m_skullRiderTC;

	// Token: 0x04001450 RID: 5200
	private TransformC m_effectTC;

	// Token: 0x04001451 RID: 5201
	private PsDialogueCharacterPosition m_position;

	// Token: 0x04001452 RID: 5202
	private TweenC m_skullRiderTween;

	// Token: 0x04001453 RID: 5203
	private TweenC m_effectTween;

	// Token: 0x04001454 RID: 5204
	public BossController m_controller;

	// Token: 0x04001455 RID: 5205
	public UIHorizontalList m_speechBubble;

	// Token: 0x04001456 RID: 5206
	private Action d_exitDoneAction;
}
