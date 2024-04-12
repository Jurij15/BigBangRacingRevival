using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class PsCameraTarget : BasicAssembledClass
{
	// Token: 0x060000F6 RID: 246 RVA: 0x0000BEA4 File Offset: 0x0000A2A4
	public PsCameraTarget(GraphNode _graphNode)
		: base(_graphNode)
	{
		Entity entity = EntityManager.AddEntity();
		base.m_assembledEntities.Add(entity);
		this.m_tc = TransformS.AddComponent(entity, "Camera Target");
		TransformS.SetTransform(this.m_tc, _graphNode.m_position, _graphNode.m_rotation);
		_graphNode.m_isRotateable = false;
		float num = 1250f;
		if (_graphNode is LevelCameraTargetNode)
		{
			LevelCameraTargetNode levelCameraTargetNode = _graphNode as LevelCameraTargetNode;
			if (levelCameraTargetNode.m_cameraTargetSize == 0)
			{
				num = 1250f;
			}
			else if (levelCameraTargetNode.m_cameraTargetSize == 1)
			{
				num = 1000f;
			}
			else if (levelCameraTargetNode.m_cameraTargetSize == 2)
			{
				num = 800f;
			}
			else if (levelCameraTargetNode.m_cameraTargetSize == 3)
			{
				num = 600f;
			}
			else if (levelCameraTargetNode.m_cameraTargetSize == 4)
			{
				num = 400f;
			}
		}
		if (!this.m_minigame.m_editing)
		{
			CameraTargetC cameraTargetC = CameraS.AddTargetComponent(this.m_tc, 400f, 400f, Vector2.zero);
			cameraTargetC.activeRadius = num;
		}
		else
		{
			this.CreateGraphElementTouchArea(20f, this.m_tc);
			PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_tc, Vector3.forward * -150f, DebugDraw.GetCircle(num, 64, Vector2.zero, false), 10f, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), CameraS.m_mainCamera, Position.Center, true);
			if (this.m_minigame.m_miniGameSpriteSheet != null)
			{
				string text = "hud_camera_target_eye";
				Frame frame = this.m_minigame.m_miniGameSpriteSheet.m_atlas.GetFrame(text, null);
				SpriteC spriteC = SpriteS.AddComponent(this.m_tc, frame, this.m_minigame.m_miniGameSpriteSheet);
				SpriteS.SetSortValue(spriteC, 1f);
				SpriteS.SetColor(spriteC, Color.white);
			}
		}
	}

	// Token: 0x040000B8 RID: 184
	public TransformC m_tc;
}
