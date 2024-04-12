using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class DecorationalGemTorch : Unit
{
	// Token: 0x06000230 RID: 560 RVA: 0x0001B988 File Offset: 0x00019D88
	public DecorationalGemTorch(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		ResourcePool.Asset burningGemPilePrefab_GameObject = RESOURCE.BurningGemPilePrefab_GameObject;
		GameObject gameObject = ResourceManager.GetGameObject(burningGemPilePrefab_GameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, "GemTorch");
		float num = ((!base.m_graphElement.m_inFront) ? 150f : (-75f));
		PrefabC prefabC = PrefabS.AddComponent(transformC, Vector3.zero, gameObject);
		PrefabS.SetCamera(prefabC, CameraS.m_mainCamera);
		if (!this.m_minigame.m_editing)
		{
			this.m_torchSound = SoundS.AddCombineSoundComponent(transformC, "GemTorchSound", "/InGame/Units/Decorations/GemTorchLoop", 2f);
		}
		else
		{
			this.m_torchSound = null;
		}
		TransformS.SetTransform(transformC, _graphElement.m_position + new Vector3(0f, 0f, num) + base.GetZBufferBias(), _graphElement.m_rotation);
		this.CreateEditorTouchArea(100f, 400f, transformC, new Vector2(0f, 150f));
		LevelDecorationNode levelDecorationNode = base.m_graphElement as LevelDecorationNode;
		if (base.m_graphElement != null)
		{
			levelDecorationNode.m_isFlippable = false;
			levelDecorationNode.m_isForegroundable = true;
		}
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0001BAA6 File Offset: 0x00019EA6
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted)
		{
			SoundS.PlaySound(this.m_torchSound, false);
		}
	}

	// Token: 0x0400023A RID: 570
	private SoundC m_torchSound;
}
