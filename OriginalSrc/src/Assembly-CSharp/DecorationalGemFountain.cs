using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class DecorationalGemFountain : Unit
{
	// Token: 0x0600022E RID: 558 RVA: 0x0001B82C File Offset: 0x00019C2C
	public DecorationalGemFountain(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		ResourcePool.Asset gemFountainPrefab_GameObject = RESOURCE.GemFountainPrefab_GameObject;
		GameObject gameObject = ResourceManager.GetGameObject(gemFountainPrefab_GameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, "GemFountain");
		float num = ((!base.m_graphElement.m_inFront) ? 125f : (-125f));
		PrefabC prefabC = PrefabS.AddComponent(transformC, Vector3.zero, gameObject);
		PrefabS.SetCamera(prefabC, CameraS.m_mainCamera);
		if (!this.m_minigame.m_editing)
		{
			this.m_fountainSound = SoundS.AddCombineSoundComponent(transformC, "GemFountainSound", "/InGame/Units/Decorations/GemFountainLoop", 2f);
		}
		else
		{
			this.m_fountainSound = null;
		}
		TransformS.SetTransform(transformC, _graphElement.m_position + new Vector3(0f, 0f, num) + base.GetZBufferBias(), _graphElement.m_rotation);
		this.CreateEditorTouchArea(225f, 250f, transformC, new Vector2(0f, 100f));
		LevelDecorationNode levelDecorationNode = base.m_graphElement as LevelDecorationNode;
		if (base.m_graphElement != null)
		{
			levelDecorationNode.m_isFlippable = false;
			levelDecorationNode.m_isForegroundable = true;
			levelDecorationNode.m_isRotateable = false;
		}
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0001B952 File Offset: 0x00019D52
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted)
		{
			SoundS.PlaySound(this.m_fountainSound, false);
		}
	}

	// Token: 0x04000239 RID: 569
	private SoundC m_fountainSound;
}
