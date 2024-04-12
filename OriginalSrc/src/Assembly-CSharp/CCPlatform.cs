using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class CCPlatform : Unit
{
	// Token: 0x060001E8 RID: 488 RVA: 0x000166F0 File Offset: 0x00014AF0
	public CCPlatform(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_overrideCC = base.m_graphElement.m_storedRotation.x;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.CCEditorPrefab_GameObject);
		this.m_TC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_TC, _graphElement.m_position, _graphElement.m_rotation);
		PrefabC prefabC = PrefabS.AddComponent(this.m_TC, new Vector3(0f, -100f, 0f) + base.GetZBufferBias(), gameObject);
		GameObject gameObject2 = prefabC.p_gameObject.transform.Find("CCEditor_Base").gameObject;
		this.m_animator = prefabC.p_gameObject.GetComponent<Animator>();
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject2, new Vector2(0f, -100f), 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, false);
		this.m_cmb = ChipmunkProS.AddStaticBody(this.m_TC, ucpPolyShape, this.m_unitC);
		this.CreateGraphElementTouchArea(150f, 225f, this.m_TC, default(Vector2));
		base.m_graphElement.m_isCopyable = false;
		base.m_graphElement.m_TC.transform.position = this.m_cmb.TC.transform.position;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x000168CC File Offset: 0x00014CCC
	protected virtual void CalculateOffsets()
	{
		if (this.m_minigame.m_editing)
		{
			GraphElement graphElement = LevelManager.m_currentLevel.m_currentLayer.m_childElements.Find((GraphElement c) => c.GetType() == typeof(LevelPlayerNode));
			if (graphElement != null)
			{
				LevelPlayerNode levelPlayerNode = graphElement as LevelPlayerNode;
				graphElement.m_isSelectable = false;
				if (levelPlayerNode.m_assembleClassType == typeof(Motorcycle))
				{
					if (levelPlayerNode.m_assembledClasses.Count > 0)
					{
						this.m_vehicleOffset = (levelPlayerNode.m_assembledClasses[0] as Motorcycle).m_centerOfGravityOffset;
					}
					else
					{
						this.m_vehicleOffset = this.m_motorcycleOffset;
					}
					this.m_offset = new Vector3(0f, 5f, 0f);
				}
				else if (levelPlayerNode.m_assembledClasses.Count > 0)
				{
					this.m_vehicleOffset = (levelPlayerNode.m_assembledClasses[0] as OffroadCar).m_centerOfGravityOffset;
				}
				else
				{
					this.m_vehicleOffset = this.m_offroaderOffset;
				}
				this.m_cmb.TC.transform.position = graphElement.m_position + new Vector3(0f, 100f, 0f) - this.m_platformOffset;
			}
			this.m_minigame.AddPlayerUnitSetListener(new Action<Unit>(this.PlayerUnitSet));
			this.m_minigame.AddGameModeSetListener(new Action<string>(this.GameModeSet));
			base.m_graphElement.m_TC.transform.position = this.m_cmb.TC.transform.position;
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00016A85 File Offset: 0x00014E85
	private void PlayerUnitSet(Unit _player)
	{
		_player.m_graphElement.m_isSelectable = false;
		this.SyncPositionToGraphElementPosition();
		this.CalculateOffsets();
		this.SyncPositionToGraphElementPosition();
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00016AA8 File Offset: 0x00014EA8
	private void GameModeSet(string _gamemodeName)
	{
		if (_gamemodeName == "Race")
		{
			if (PsState.UsingEditorResources())
			{
				EditorScene.CumulateReservedResources(base.m_graphElement.m_name, 1);
			}
			base.m_graphElement.Removed();
			base.m_graphElement.Dispose();
			LevelManager.m_currentLevel.ItemChanged();
		}
	}

	// Token: 0x060001EC RID: 492 RVA: 0x00016B00 File Offset: 0x00014F00
	public override void Update()
	{
		base.Update();
		if (this.m_minigame.m_editing || this.m_minigame.m_gameStarted)
		{
		}
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00016B28 File Offset: 0x00014F28
	public override void CreateEditorTouchArea(GameObject _collisionGO, TransformC _parentTC)
	{
		base.CreateEditorTouchArea(_collisionGO, _parentTC);
	}

	// Token: 0x060001EE RID: 494 RVA: 0x00016B34 File Offset: 0x00014F34
	public override void SyncPositionToGraphElementPosition()
	{
		base.SyncPositionToGraphElementPosition();
		if (PsState.m_activeMinigame.m_playerNode != null)
		{
			PsState.m_activeMinigame.m_playerNode.m_TC.transform.position = this.m_TC.transform.position - this.m_platformOffset;
			PsState.m_activeMinigame.m_playerNode.m_position = this.m_TC.transform.position - this.m_platformOffset;
		}
		if (PsState.m_activeMinigame.m_playerTC != null)
		{
			PsState.m_activeMinigame.m_playerTC.transform.position = PsState.m_activeMinigame.m_playerNode.m_position - this.m_vehicleOffset + this.m_offset;
		}
		if (PsState.m_activeMinigame.m_playerUnit != null)
		{
			PsState.m_activeMinigame.m_playerUnit.SyncPositionToGraphElementPosition();
		}
	}

	// Token: 0x060001EF RID: 495 RVA: 0x00016C1C File Offset: 0x0001501C
	public override void Destroy()
	{
		base.Destroy();
		this.m_minigame.RemovePlayerUnitSetListener(new Action<Unit>(this.PlayerUnitSet));
		this.m_minigame.RemoveGameModeSetListener(new Action<string>(this.GameModeSet));
		if (this.m_minigame.m_editing)
		{
			this.SetOverrideCC(-1f);
			if (PsState.m_activeMinigame != null && PsState.m_activeMinigame.m_playerNode != null)
			{
				PsState.m_activeMinigame.m_playerNode.m_isSelectable = true;
			}
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x00016CA4 File Offset: 0x000150A4
	public void SetOverrideCC(float _cc)
	{
		base.m_graphElement.m_storedRotation.x = _cc;
		PsState.m_activeGameLoop.m_minigameMetaData.overrideCC = _cc;
		PsState.m_activeGameLoop.SetOverrideCC(_cc);
		Debug.LogWarning("Level override CC is now " + _cc);
	}

	// Token: 0x040001C4 RID: 452
	protected float m_overrideCC;

	// Token: 0x040001C5 RID: 453
	private ChipmunkBodyC m_cmb;

	// Token: 0x040001C6 RID: 454
	private SoundC m_platformSound;

	// Token: 0x040001C7 RID: 455
	protected string m_level1 = "100CC";

	// Token: 0x040001C8 RID: 456
	protected string m_level2 = "250CC";

	// Token: 0x040001C9 RID: 457
	protected string m_level3 = "500CC";

	// Token: 0x040001CA RID: 458
	protected string m_level4 = "800CC";

	// Token: 0x040001CB RID: 459
	protected string m_level5 = "1200CC";

	// Token: 0x040001CC RID: 460
	protected Animator m_animator;

	// Token: 0x040001CD RID: 461
	protected Vector3 m_motorcycleOffset = Vector3.zero;

	// Token: 0x040001CE RID: 462
	protected Vector3 m_offroaderOffset = Vector3.zero;

	// Token: 0x040001CF RID: 463
	public Vector3 m_platformOffset = new Vector3(0f, 50f);

	// Token: 0x040001D0 RID: 464
	public Vector3 m_vehicleOffset = new Vector3(0f, 0f);

	// Token: 0x040001D1 RID: 465
	public Vector3 m_offset = Vector3.zero;

	// Token: 0x040001D2 RID: 466
	public int m_cc;

	// Token: 0x040001D3 RID: 467
	public TransformC m_TC;
}
