using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class DecorationalGemRain : Unit
{
	// Token: 0x06000208 RID: 520 RVA: 0x0001931C File Offset: 0x0001771C
	public DecorationalGemRain(GraphNode _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		ResourcePool.Asset gemMeteorStormPrefab_GameObject = RESOURCE.GemMeteorStormPrefab_GameObject;
		GameObject gameObject = ResourceManager.GetGameObject(gemMeteorStormPrefab_GameObject);
		this.m_TC = TransformS.AddComponent(this.m_entity, "GemMeteorStorm");
		PrefabC prefabC = PrefabS.AddComponent(this.m_TC, Vector3.zero, gameObject);
		PrefabS.SetCamera(prefabC, CameraS.m_mainCamera);
		this.m_touchEntity = EntityManager.AddEntity();
		this.m_touchTC = TransformS.AddComponent(this.m_touchEntity, "MeteorTouch");
		TransformS.ParentComponent(this.m_touchTC, this.m_TC, Vector3.zero);
		this.m_largeMeteor = prefabC.p_gameObject.transform.Find("GemLargeMeteor");
		this.m_largeMeteor.position = this.m_domeSizePositionOffsets[this.m_minigame.m_domeSizeIndex];
		this.m_touchTC.transform.position = this.m_largeMeteor.transform.position;
		this.CreateEditorTouchArea(10000f, 10000f, this.m_touchTC, default(Vector2));
		if (!this.m_minigame.m_editing)
		{
			this.m_rainSound = SoundS.AddCombineSoundComponent(CameraS.m_mainCameraTC, "GemRainSound", "/InGame/Units/Decorations/GemRainLoop", 1f);
		}
		else
		{
			this.m_minigame.AddDomeSizeChangeListener(new Action<int>(this.DomeSizeChangeListener));
			this.m_rainSound = null;
		}
		_graphElement.m_isMoveable = false;
		_graphElement.m_isFlippable = false;
		_graphElement.m_isCopyable = false;
		_graphElement.m_isRotateable = false;
		_graphElement.m_isForegroundable = false;
		_graphElement.m_isRemovable = true;
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00019510 File Offset: 0x00017910
	public override void Destroy()
	{
		this.m_minigame.RemoveDomeSizeChangeListener(new Action<int>(this.DomeSizeChangeListener));
		EntityManager.RemoveEntity(this.m_touchEntity);
		this.m_touchEntity = null;
		base.Destroy();
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00019544 File Offset: 0x00017944
	private void DomeSizeChangeListener(int _domeindex)
	{
		if (this.m_minigame.m_editing)
		{
			this.m_largeMeteor.position = this.m_domeSizePositionOffsets[this.m_minigame.m_domeSizeIndex];
			this.m_touchTC.transform.position = this.m_largeMeteor.transform.position;
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x000195A7 File Offset: 0x000179A7
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted && this.m_rainSound != null)
		{
			SoundS.PlaySound(this.m_rainSound, false);
		}
	}

	// Token: 0x040001F3 RID: 499
	private Entity m_touchEntity;

	// Token: 0x040001F4 RID: 500
	private SoundC m_rainSound;

	// Token: 0x040001F5 RID: 501
	private Vector3[] m_domeSizePositionOffsets = new Vector3[]
	{
		new Vector3(-499f, 12260f, 46814f),
		new Vector3(11749f, 11630f, 46814f),
		new Vector3(15000f, 10010f, 38065f)
	};

	// Token: 0x040001F6 RID: 502
	private TransformC m_TC;

	// Token: 0x040001F7 RID: 503
	private TransformC m_touchTC;

	// Token: 0x040001F8 RID: 504
	private Transform m_largeMeteor;
}
