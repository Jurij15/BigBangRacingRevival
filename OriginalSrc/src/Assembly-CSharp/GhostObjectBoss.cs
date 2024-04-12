using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class GhostObjectBoss : GhostObject
{
	// Token: 0x06001D32 RID: 7474 RVA: 0x0014EBBC File Offset: 0x0014CFBC
	public GhostObjectBoss(GhostData _data, Ghost _ghost)
	{
		string empty = string.Empty;
		int time = _data.time;
		string playerId = _data.playerId;
		string ghostId = _data.ghostId;
		string countryCode = _data.countryCode;
		int version = _data.version;
		base..ctor(empty, time, playerId, ghostId, countryCode, _ghost, string.Empty, string.Empty, version);
		this.m_hideAtIntro = false;
	}

	// Token: 0x06001D33 RID: 7475 RVA: 0x0014EC18 File Offset: 0x0014D018
	protected override void SetGhost2DGraphics()
	{
	}

	// Token: 0x06001D34 RID: 7476 RVA: 0x0014EC1C File Offset: 0x0014D01C
	protected override void SetTrail(string _identifier, Vector3 _chassisOffset)
	{
		if (!string.IsNullOrEmpty(_identifier))
		{
			GameObject trailPrefabByIdentifier = PsCustomisationManager.GetTrailPrefabByIdentifier(_identifier);
			if (trailPrefabByIdentifier != null)
			{
				this.m_trail = Object.Instantiate<GameObject>(trailPrefabByIdentifier);
				Vector3 vector = this.m_trail.transform.position + Vector3.up * -10f + _chassisOffset;
				this.m_trail.transform.parent = this.m_chassis.transform;
				this.m_trail.transform.localPosition = vector;
				this.m_trailBase = this.m_trail.gameObject.GetComponent<PsTrailBase>();
				this.m_trailBase.Init();
			}
		}
	}

	// Token: 0x06001D35 RID: 7477 RVA: 0x0014ECCC File Offset: 0x0014D0CC
	public override void CreateVisuals(GhostPart[] _parts, string _trailIdentifier, GhostBoostEffect[] _boostEffects = null, bool _hasHat = false, bool _showAtStartOverride = false, string _identifier = "")
	{
		base.CreateVisuals(_parts, _trailIdentifier, _boostEffects, _hasHat, false, string.Empty);
		this.GetGhostCollectibleTimes(this.GetCollectiblePositions());
		this.m_ticksToCatch = 0f;
		this.m_freezeSeconds = 0f;
		this.m_catchSpeed = 0f;
		this.m_updateCatchSpeed = false;
		this.m_freezeMaterialList = new List<Material>();
		this.CollectFreezableMaterials(this.m_chassis.transform.gameObject);
	}

	// Token: 0x06001D36 RID: 7478 RVA: 0x0014ED3F File Offset: 0x0014D13F
	public override void SetAlienCharacter(Vector3 _offset, string _prefabName = "AlienBossPrefab_GameObject", string _driveState = "Drive")
	{
		base.SetAlienCharacter(_offset, _prefabName, _driveState);
		this.CollectFreezableMaterials(this.m_ghostCharacter.m_mainPC.p_gameObject);
	}

	// Token: 0x06001D37 RID: 7479 RVA: 0x0014ED60 File Offset: 0x0014D160
	private void CollectFreezableMaterials(GameObject _go)
	{
		List<Renderer> list = new List<Renderer>(_go.transform.GetComponentsInChildren<Renderer>());
		Shader shader = Shader.Find("WOE/Units/ReflectiveSpecularRimFreezeUnit");
		foreach (Renderer renderer in list)
		{
			if ((renderer.material != null) & (renderer.material.shader == shader))
			{
				this.m_freezeMaterialList.Add(renderer.material);
			}
		}
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x0014EE00 File Offset: 0x0014D200
	private void Freeze(float _value)
	{
		foreach (Material material in this.m_freezeMaterialList)
		{
			material.SetFloat("_FreezeAmount", _value);
		}
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x0014EE64 File Offset: 0x0014D264
	public void AddTickOffset(float _ticks)
	{
		this.m_ticksToCatch = _ticks;
		this.m_updateCatchSpeed = true;
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x0014EE74 File Offset: 0x0014D274
	public void AddFreeze(float _seconds)
	{
		if (this.m_entity == null)
		{
			return;
		}
		if (this.m_freezeSeconds > 0f)
		{
			this.m_freezeSeconds += _seconds;
		}
		else
		{
			this.m_freezeSeconds = _seconds;
			this.AddFreezeEffect();
		}
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x0014EEB4 File Offset: 0x0014D2B4
	private void AddFreezeEffect()
	{
		Vector3 position = this.m_chassis.transform.position;
		Vector3 eulerAngles = this.m_chassis.transform.localRotation.eulerAngles;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.EffectFreezePrefab_GameObject);
		Entity entity = EntityManager.AddEntity("GTAG_AUTODESTROY");
		this.m_freezeTC = TransformS.AddComponent(entity, "Freeze");
		this.m_freezeEffect = PrefabS.AddComponent(this.m_freezeTC, position, gameObject);
		this.m_freezeEffect.p_gameObject.transform.localEulerAngles = eulerAngles;
		SoundS.PlaySingleShot("/Ingame/Units/FreezeOn", this.m_parts[0].transform.position, 1f);
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x0014EF60 File Offset: 0x0014D360
	private void RemoveFreeze()
	{
		Vector3 position = this.m_chassis.transform.position;
		Vector3 eulerAngles = this.m_chassis.transform.localRotation.eulerAngles;
		PrefabS.RemoveComponent(this.m_freezeEffect, true);
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.EffectFreezeExplosionPrefab_GameObject);
		PrefabC prefabC = PrefabS.AddComponent(this.m_freezeTC, position, gameObject);
		prefabC.p_gameObject.transform.localEulerAngles = eulerAngles;
		TimerS.AddComponent(this.m_freezeTC.p_entity, string.Empty, 0f, 2f, true, null);
		SoundS.PlaySingleShot("/Ingame/Units/FreezeOff", this.m_parts[0].transform.position, 1f);
	}

	// Token: 0x06001D3D RID: 7485 RVA: 0x0014F014 File Offset: 0x0014D414
	protected override float GetPlaybackSpeed()
	{
		if (this.m_freezeShader < 1f && this.m_freezeSeconds > 0f)
		{
			this.m_freezeShader += 0.025f;
		}
		if (this.m_freezeSeconds > 0f)
		{
			this.m_freezeSeconds -= Main.m_gameDeltaTime * Main.m_timeScale;
			if (this.m_freezeSeconds <= 0f)
			{
				this.RemoveFreeze();
			}
			return 0f;
		}
		if (this.m_ticksToCatch > 0f)
		{
			if (this.m_updateCatchSpeed)
			{
				float num = ((float)this.m_ghost.m_keyframeCount - this.m_ghost.m_playbackTick) * (float)this.m_ghost.m_frameSkip;
				this.m_targetSpeed = this.m_ticksToCatch / num;
				this.m_targetSpeed = Mathf.Min(BossBattles.bossMaxAddedSpeed, this.m_targetSpeed);
				this.m_targetSpeed = Mathf.Round(this.m_targetSpeed * 100f) / 100f;
				float num2 = this.m_targetSpeed - this.m_catchSpeed;
				num2 = Mathf.Round(num2 * 100f);
				this.m_updateCatchSpeed = false;
			}
			if (this.m_catchSpeed != this.m_targetSpeed)
			{
				float num3 = ((this.m_catchSpeed >= this.m_targetSpeed) ? (-0.01f) : 0.01f);
				this.m_catchSpeed += num3;
			}
			this.m_ticksToCatch -= this.m_catchSpeed;
			return 1f + this.m_catchSpeed;
		}
		return 1f;
	}

	// Token: 0x06001D3E RID: 7486 RVA: 0x0014F1A8 File Offset: 0x0014D5A8
	private void GetGhostCollectibleTimes(List<Vector2> _positions)
	{
		if (this.m_manualCollectibleEventsAdded)
		{
			return;
		}
		this.m_manualCollectibleEventsAdded = true;
		if (this.m_version < 2)
		{
			List<int> list = new List<int>();
			GhostNode ghostNode = this.m_ghostNodes[0];
			int keyframeCount = this.m_ghost.m_keyframeCount;
			for (int i = 0; i < keyframeCount; i++)
			{
				for (int j = 0; j < this.m_ghost.m_frameSkip; j++)
				{
					float num = (float)i + (float)j * (1f / (float)this.m_ghost.m_frameSkip);
					Vector2 vector = this.m_ghost.GetCurrentPosition(ghostNode, num, -1);
					for (int k = _positions.Count - 1; k >= 0; k--)
					{
						if ((_positions[k] - vector).sqrMagnitude < 10000f)
						{
							_positions.RemoveAt(k);
							list.Add(Mathf.CeilToInt(num * (float)this.m_ghost.m_frameSkip + 0.05f));
							if (list.Count >= 3)
							{
								break;
							}
						}
					}
				}
			}
			this.m_collectedPieceTick = list;
			for (int l = 0; l < list.Count; l++)
			{
				this.m_ghost.AddEvent(GhostEventType.MapPieceCollected, list[l]);
			}
			this.m_ghost.m_events = Enumerable.ToList<GhostEvent>(Enumerable.OrderBy<GhostEvent, int>(this.m_ghost.m_events, (GhostEvent x) => x.m_tick));
		}
		else
		{
			this.m_collectedPieceTick = this.m_ghost.GetEventGameTicks(GhostEventType.MapPieceCollected);
		}
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x0014F358 File Offset: 0x0014D758
	protected override void CollectedMapPiece()
	{
		if (PsState.m_activeGameLoop is PsGameLoopAdventureBattle)
		{
			CollectibleStar closestCollectible = this.GetClosestCollectible();
			closestCollectible.m_bossScript.Wobble();
			Vector3 position = this.m_chassis.transform.position;
			SoundS.PlaySingleShot("/Ingame/Units/Checkpoint_Boss", new Vector3(position.x, position.y, 0f), 1f);
		}
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x0014F3BE File Offset: 0x0014D7BE
	private CollectibleStar GetClosestCollectible()
	{
		return Enumerable.First<CollectibleStar>(Enumerable.OrderBy<CollectibleStar, float>(this.m_starUnits, (CollectibleStar v2) => this.GetDistance(this.m_chassis.transform.position, v2.m_pos)));
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x0014F3DC File Offset: 0x0014D7DC
	private float GetDistance(Vector2 v1, Vector2 v2)
	{
		return (v1 - v2).sqrMagnitude;
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x0014F3F8 File Offset: 0x0014D7F8
	protected List<Vector2> GetCollectiblePositions()
	{
		List<Vector2> list = new List<Vector2>();
		List<Entity> entitiesByTag = EntityManager.GetEntitiesByTag("CollectibleStar");
		this.m_starUnits = new List<CollectibleStar>();
		for (int i = 0; i < entitiesByTag.Count; i++)
		{
			CollectibleStar collectibleStar = (EntityManager.GetComponentByEntity((ComponentType)30, entitiesByTag[i]) as UnitC).m_unit as CollectibleStar;
			this.m_starUnits.Add(collectibleStar);
			list.Add(collectibleStar.m_pos);
		}
		return list;
	}

	// Token: 0x04001FF0 RID: 8176
	public List<int> m_collectedPieceTick;

	// Token: 0x04001FF1 RID: 8177
	private float m_ticksToCatch;

	// Token: 0x04001FF2 RID: 8178
	private float m_catchSpeed;

	// Token: 0x04001FF3 RID: 8179
	private bool m_updateCatchSpeed;

	// Token: 0x04001FF4 RID: 8180
	private float m_freezeSeconds;

	// Token: 0x04001FF5 RID: 8181
	private PrefabC m_freezeEffect;

	// Token: 0x04001FF6 RID: 8182
	private TransformC m_freezeTC;

	// Token: 0x04001FF7 RID: 8183
	private float m_freezeShader;

	// Token: 0x04001FF8 RID: 8184
	private List<Material> m_freezeMaterialList;

	// Token: 0x04001FF9 RID: 8185
	private bool m_manualCollectibleEventsAdded;

	// Token: 0x04001FFA RID: 8186
	private List<Vector2> m_collectiblePositions;

	// Token: 0x04001FFB RID: 8187
	private List<CollectibleStar> m_starUnits;

	// Token: 0x04001FFC RID: 8188
	private const float ACCELERATE_PER_TICK = 0.01f;

	// Token: 0x04001FFD RID: 8189
	private float m_targetSpeed;
}
