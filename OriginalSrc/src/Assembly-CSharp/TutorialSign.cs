using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class TutorialSign : Unit
{
	// Token: 0x06000250 RID: 592 RVA: 0x0001E018 File Offset: 0x0001C418
	public TutorialSign(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.TutorialSign_GameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, "ArrowSign");
		TransformS.SetTransform(transformC, _graphElement.m_position + new Vector3(0f, -0f, 100f) + base.GetZBufferBias(), _graphElement.m_rotation);
		PrefabS.AddComponent(transformC, Vector3.zero, gameObject);
		ucpCircleShape ucpCircleShape = new ucpCircleShape(100f, Vector2.zero, 1118208U, 0f, 0f, 0f, (ucpCollisionType)8, true);
		this.m_cmb = ChipmunkProS.AddKinematicBody(transformC, new ucpCircleShape[] { ucpCircleShape }, this.m_unitC);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.TutorialSignCollisionHandler), (ucpCollisionType)8, (ucpCollisionType)3, true, false, true);
			CameraTargetC cameraTargetC = CameraS.AddTargetComponent(transformC, 150f, 150f, new Vector2(0f, 0f));
			cameraTargetC.activeRadius = 500f;
		}
		this.CreateEditorTouchArea(50f, 50f, transformC, default(Vector2));
		base.m_graphElement.m_isRotateable = true;
		base.m_graphElement.m_isFlippable = true;
		base.m_graphElement.m_isForegroundable = true;
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0001E17C File Offset: 0x0001C57C
	private void TutorialSignCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (_phase == ucpCollisionPhase.Begin)
		{
			this.m_collidingCount++;
			if (this.m_collidingCount == 1 && this.m_bubbleUI == null)
			{
				if (PsState.m_activeMinigame.m_currentSign != null)
				{
					PsState.m_activeMinigame.m_currentSign.DestroySign();
				}
				PsState.m_activeMinigame.m_currentSign = this;
				Debug.Log("START", null);
				string text = (base.m_graphElement as LevelTextNode).m_text;
				StringID stringID;
				try
				{
					stringID = (StringID)Enum.Parse(typeof(StringID), (base.m_graphElement as LevelTextNode).m_stringID);
				}
				catch
				{
					stringID = StringID.EMPTY;
				}
				if (stringID != StringID.EMPTY)
				{
					text = PsStrings.Get(stringID);
				}
				this.m_bubbleUI = new UIVerticalList(null, "ArrowSign Bubble");
				this.m_bubbleUI.SetDrawHandler(new UIDrawDelegate(this.BubbleDrawHandler));
				this.m_bubbleUI.SetMargins(0.05f, 0.05f, 0.025f, 0.025f, RelativeTo.ScreenHeight);
				UITextbox uitextbox = new UITextbox(this.m_bubbleUI, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
				uitextbox.SetWidth(0.3f, RelativeTo.ScreenHeight);
				uitextbox.SetDepthOffset(8f);
				this.m_bubbleUI.Update();
				this.m_currentTime = this.m_timerSeconds;
			}
		}
		else if (_phase == ucpCollisionPhase.Separate)
		{
			if (this.m_collidingCount == 1)
			{
				Debug.Log("Not colliding anymore, time left: " + this.m_currentTime, null);
			}
			this.m_collidingCount--;
		}
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0001E330 File Offset: 0x0001C730
	private void BubbleDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * 0.025f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * 0f, _c.m_actualHeight * -0.5f - _c.m_actualWidth * 0.05f),
			new Vector2(_c.m_actualWidth * -0.025f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f)
		};
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 10f, array, DebugDraw.HexToUint("00d4c8"), DebugDraw.HexToUint("00d4c8"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), CameraS.m_uiCamera, string.Empty, null);
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0001E4C8 File Offset: 0x0001C8C8
	public override void Update()
	{
		base.Update();
		if (this.m_currentTime != -1f)
		{
			this.m_currentTime -= Main.m_gameDeltaTime;
			if (this.m_currentTime <= 0f && this.m_bubbleUI != null && this.m_collidingCount < 1)
			{
				this.DestroySign();
				PsState.m_activeMinigame.m_currentSign = null;
			}
		}
		if (this.m_bubbleUI != null)
		{
			float num = 50f;
			Vector3 vector = CameraS.m_mainCamera.WorldToScreenPoint(this.m_minigame.m_playerTC.transform.position);
			Vector3 vector2 = CameraS.m_uiCamera.ScreenToWorldPoint(vector);
			Vector3 vector3 = vector2 + Vector3.up * ((float)Screen.height * 0.2f + this.m_bubbleUI.m_actualHeight * 0.5f);
			this.m_bubbleUI.m_TC.transform.position = new Vector3(vector3.x, vector3.y, num);
		}
		if (base.m_graphElement.m_flipped)
		{
			base.m_graphElement.m_flipped = false;
			this.m_oldText = (base.m_graphElement as LevelTextNode).m_text;
			this.m_model = new TextModel((base.m_graphElement as LevelTextNode).m_text, null);
			PsUICenterTutorialSignTextInput psUICenterTutorialSignTextInput = new PsUICenterTutorialSignTextInput(this.m_model);
			psUICenterTutorialSignTextInput.Update();
		}
		else if (base.m_graphElement.m_inFront)
		{
			Debug.LogError("In front");
			base.m_graphElement.m_inFront = false;
			this.m_stringId = true;
			this.m_oldText = (base.m_graphElement as LevelTextNode).m_stringID;
			this.m_model = new TextModel((base.m_graphElement as LevelTextNode).m_stringID, null);
			PsUICenterTutorialSignTextInput psUICenterTutorialSignTextInput2 = new PsUICenterTutorialSignTextInput(this.m_model);
			psUICenterTutorialSignTextInput2.Update();
		}
		if (this.m_model != null)
		{
			if (this.m_model.m_done && this.m_oldText != this.m_model.m_text)
			{
				Debug.Log(this.m_model.m_text, null);
				this.m_oldText = this.m_model.m_text;
				if (this.m_stringId)
				{
					(base.m_graphElement as LevelTextNode).m_stringID = this.m_model.m_text;
				}
				else
				{
					(base.m_graphElement as LevelTextNode).m_text = this.m_model.m_text;
				}
				this.m_model = null;
			}
			else if (this.m_model.m_cancelled)
			{
				this.m_model = null;
			}
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0001E768 File Offset: 0x0001CB68
	public void Hide()
	{
		if (this.m_bubbleUI != null && this.m_bubbleUI.m_TC.transform.gameObject.activeSelf)
		{
			this.m_bubbleUI.m_TC.transform.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0001E7BC File Offset: 0x0001CBBC
	public void Show()
	{
		if (this.m_bubbleUI != null && !this.m_bubbleUI.m_TC.transform.gameObject.activeSelf)
		{
			this.m_bubbleUI.m_TC.transform.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0001E80E File Offset: 0x0001CC0E
	public void DestroySign()
	{
		if (this.m_bubbleUI != null)
		{
			this.m_bubbleUI.Destroy();
			this.m_bubbleUI = null;
			this.m_currentTime = -1f;
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0001E838 File Offset: 0x0001CC38
	public override void Destroy()
	{
		this.DestroySign();
		base.Destroy();
	}

	// Token: 0x0400027A RID: 634
	private int m_collidingCount;

	// Token: 0x0400027B RID: 635
	private ChipmunkBodyC m_cmb;

	// Token: 0x0400027C RID: 636
	private string m_oldText;

	// Token: 0x0400027D RID: 637
	private TextModel m_model;

	// Token: 0x0400027E RID: 638
	private bool m_stringId;

	// Token: 0x0400027F RID: 639
	private UIVerticalList m_bubbleUI;

	// Token: 0x04000280 RID: 640
	private float m_timerSeconds = 4f;

	// Token: 0x04000281 RID: 641
	private float m_currentTime = -1f;
}
