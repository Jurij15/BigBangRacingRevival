using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class CollectibleCoin
{
	// Token: 0x06000150 RID: 336 RVA: 0x0000FE14 File Offset: 0x0000E214
	public CollectibleCoin(PsCoin _coin)
	{
		this.m_coin = _coin;
		Vector2 pos = _coin.m_pos;
		this.m_entity = EntityManager.AddEntity(new string[] { "GTAG_AUTODESTROY", "GTAG_COIN" });
		this.m_tc = TransformS.AddComponent(this.m_entity, "Coin");
		TransformS.SetTransform(this.m_tc, pos + new Vector3(0f, 0f, 0f), Vector3.zero);
		PsS.AddCustomObject(this.m_entity, this);
		this.SelectPrefab(_coin.m_type);
		float num = 0f;
		if (!(PsState.m_activeGameLoop is PsGameLoopEditor) && PsState.m_activeGameLoop.m_selectedVehicle != PsSelectedVehicle.CreatorVehicle)
		{
			num = PsUpgradeManager.GetCurrentEfficiency(PsState.m_activeMinigame.m_playerUnit.GetType(), PsUpgradeManager.UpgradeType.COIN_MAGNET);
		}
		float num2 = 30f + num;
		ucpShape ucpShape = new ucpCircleShape(num2, Vector2.zero, 16777216U, 50f, 0.5f, 0.9f, (ucpCollisionType)9, true);
		this.m_body = ChipmunkProS.AddKinematicBody(this.m_tc, ucpShape, null);
		ChipmunkProS.AddCollisionHandler(this.m_body, new CollisionDelegate(this.CoinCollisionHandler), (ucpCollisionType)9, (ucpCollisionType)3, true, false, false);
	}

	// Token: 0x06000151 RID: 337 RVA: 0x0000FF4C File Offset: 0x0000E34C
	public void UpdateCollisionArea()
	{
		float currentEfficiency = PsUpgradeManager.GetCurrentEfficiency(PsState.m_activeMinigame.m_playerUnit.GetType(), PsUpgradeManager.UpgradeType.COIN_MAGNET);
		float num = 30f + currentEfficiency;
		for (int i = 0; i < this.m_body.shapes.Count; i++)
		{
			ChipmunkProWrapper.ucpCircleShapeSetRadius(this.m_body.shapes[i].shapePtr, num);
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x0000FFB8 File Offset: 0x0000E3B8
	private void SelectPrefab(CoinType _type)
	{
		if (this.m_prefab != null)
		{
			PrefabS.RemoveComponent(this.m_prefab, true);
			this.m_prefab = null;
		}
		switch (_type)
		{
		case CoinType.COPPER:
			this.m_prefab = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.CollectibleCoinBronze_GameObject));
			break;
		case CoinType.GOLD:
			this.m_prefab = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.CollectibleCoinGold_GameObject));
			break;
		case CoinType.SUPER_GOLD:
			this.m_prefab = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.CollectibleSuperCoinPrefab_GameObject));
			this.m_prefab.p_gameObject.transform.localScale = this.m_prefab.p_gameObject.transform.localScale * 0.8f;
			break;
		case CoinType.DIAMOND:
			this.m_prefab = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.CollectibleGemPrefab_GameObject));
			this.m_prefab.p_gameObject.transform.localScale = this.m_prefab.p_gameObject.transform.localScale * 0.8f;
			break;
		case CoinType.SHARD:
			this.m_prefab = PrefabS.AddComponent(this.m_tc, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.CollectibleShardSmallPrefab_GameObject));
			break;
		}
		PrefabS.SetCamera(this.m_prefab, CameraS.m_mainCamera);
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00010133 File Offset: 0x0000E533
	public void SetCoinValueBoosterFX()
	{
		TransformS.SetScale(this.m_tc, new Vector3(1.5f, 1.5f, 1.5f));
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00010154 File Offset: 0x0000E554
	private void CoinCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (this.m_entity.m_active)
		{
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
			UnitC unitC = chipmunkBodyC.customComponent as UnitC;
			if (unitC != null && unitC.m_unit != null)
			{
				if (!this.m_coin.m_following)
				{
					Vehicle vehicle = unitC.m_unit as Vehicle;
					if (!vehicle.m_hasBrokenDown)
					{
						vehicle.m_followingCoins.Add(this);
						this.m_followStrength = 0f;
						this.m_coin.m_following = true;
					}
				}
				ChipmunkProS.RemoveCollisionHandler(this.m_body, new CollisionDelegate(this.CoinCollisionHandler));
			}
		}
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00010204 File Offset: 0x0000E604
	public void CollectCoin()
	{
		if (this.m_entity != null && !this.m_coin.m_collected)
		{
			Entity entity = EntityManager.AddEntity(new string[] { "GTAG_AUTODESTROY" });
			this.m_coin.m_collected = true;
			TimerS.AddComponent(entity, "RemoveTimer", 1f, 0f, true, null);
			TransformC transformC = TransformS.AddComponent(entity, this.m_body.TC.transform.position);
			PrefabC prefabC = PrefabS.AddComponent(transformC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.PickUpSparks_GameObject), string.Empty, false, true);
			PrefabS.SetCamera(prefabC, CameraS.m_mainCamera);
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_body.body);
			if (this.m_coin.m_type == CoinType.COPPER)
			{
				int num = 5;
				if (PsMetagameManager.m_playerStats.coinDoubler)
				{
					num *= 5;
				}
				PsState.m_debugCopperValue += num;
				PsState.m_debugCopperCoinsCollected++;
				Minigame activeMinigame = PsState.m_activeMinigame;
				activeMinigame.m_collectedCopper += num;
				PsMetagameManager.m_playerStats.copper += num;
				PsMetagameManager.m_playerStats.updated = true;
				if (PsMetagameManager.m_playerStats.copper > 99)
				{
					PsMetagameManager.m_playerStats.copper = 0;
					PsMetagameManager.m_playerStats.copperReset = true;
					PsState.m_activeMinigame.m_collectedCopper = 0;
					PsMetagameManager.m_playerStats.coins++;
					Minigame activeMinigame2 = PsState.m_activeMinigame;
					activeMinigame2.m_collectedCoins = ObscuredInt.op_Increment(activeMinigame2.m_collectedCoins);
					Minigame activeMinigame3 = PsState.m_activeMinigame;
					activeMinigame3.m_collectedCoinsForDoubleUp = ObscuredInt.op_Increment(activeMinigame3.m_collectedCoinsForDoubleUp);
				}
				SoundS.PlaySingleShotWithParameter("/Ingame/Units/GotCoin", new Vector3(vector.x, vector.y, 0f), "Value", 1f, 1f);
			}
			else if (this.m_coin.m_type == CoinType.SHARD)
			{
				int num2 = 1;
				if (PsMetagameManager.m_doubleValueGoodOrBadEvent != null && PsMetagameManager.m_doubleValueGoodOrBadEvent.timeToStart <= 0 && PsMetagameManager.m_doubleValueGoodOrBadEvent.timeLeft > 0)
				{
					num2 *= 2;
				}
				Minigame activeMinigame4 = PsState.m_activeMinigame;
				activeMinigame4.m_collectedShards += num2;
				PsMetagameManager.m_playerStats.shards += num2;
				PsMetagameManager.m_playerStats.updated = true;
				if (PsMetagameManager.m_playerStats.shards > 99)
				{
					PsMetagameManager.m_playerStats.shards = 0;
					PsMetagameManager.m_playerStats.shardReset = true;
					PsState.m_activeMinigame.m_collectedShards = 0;
					PsMetagameManager.m_playerStats.CumulateDiamonds(1);
					Minigame activeMinigame5 = PsState.m_activeMinigame;
					activeMinigame5.m_collectedDiamonds = ObscuredInt.op_Increment(activeMinigame5.m_collectedDiamonds);
				}
				SoundS.PlaySingleShotWithParameter("/Ingame/Units/GotCoin", new Vector3(vector.x, vector.y, 0f), "Value", 1f, 1f);
			}
			else if (this.m_coin.m_type == CoinType.GOLD || this.m_coin.m_type == CoinType.SUPER_GOLD)
			{
				int num3 = 1;
				if (this.m_coin.m_type == CoinType.SUPER_GOLD)
				{
					num3 = 10;
					SoundS.PlaySingleShotWithParameter("/Ingame/Units/GotCoin", new Vector3(vector.x, vector.y, 0f), "Value", 3f, 1f);
				}
				else
				{
					SoundS.PlaySingleShotWithParameter("/Ingame/Units/GotCoin", new Vector3(vector.x, vector.y, 0f), "Value", 2f, 1f);
				}
				PsMetagameManager.m_playerStats.coins += num3;
				Minigame activeMinigame6 = PsState.m_activeMinigame;
				activeMinigame6.m_collectedCoins += num3;
				Minigame activeMinigame7 = PsState.m_activeMinigame;
				activeMinigame7.m_collectedCoinsForDoubleUp += num3;
				PsMetagameManager.m_playerStats.updated = true;
			}
			else if (this.m_coin.m_type == CoinType.DIAMOND)
			{
				PsMetagameManager.m_playerStats.diamonds++;
				Minigame activeMinigame8 = PsState.m_activeMinigame;
				activeMinigame8.m_collectedDiamonds = ObscuredInt.op_Increment(activeMinigame8.m_collectedDiamonds);
				PsMetagameManager.m_playerStats.updated = true;
				SoundS.PlaySingleShot("/Ingame/Units/GotDiamond", new Vector3(vector.x, vector.y, 0f), 1f);
			}
			EntityManager.RemoveEntity(this.m_entity);
			this.m_entity = null;
			this.m_coin = null;
		}
	}

	// Token: 0x04000134 RID: 308
	public PsCoin m_coin;

	// Token: 0x04000135 RID: 309
	public Entity m_entity;

	// Token: 0x04000136 RID: 310
	public ChipmunkBodyC m_body;

	// Token: 0x04000137 RID: 311
	private PrefabC m_prefab;

	// Token: 0x04000138 RID: 312
	private TransformC m_tc;

	// Token: 0x04000139 RID: 313
	public float m_followStrength;
}
