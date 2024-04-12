using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class CameraSensor : Unit
{
	// Token: 0x060000F4 RID: 244 RVA: 0x0000BC10 File Offset: 0x0000A010
	public CameraSensor(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		if (Main.m_currentGame.m_currentScene.GetType() != typeof(EditorScene))
		{
			return;
		}
		_graphElement.m_isFlippable = true;
		_graphElement.m_isCopyable = false;
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(transformC, _graphElement.m_position + new Vector3(0f, 0f, 50f), _graphElement.m_rotation);
		int num = (int)_graphElement.m_storedRotation.x;
		if (_graphElement.m_flipped)
		{
			_graphElement.m_flipped = false;
			num = (num + 1) % 4;
			_graphElement.m_storedRotation.x = (float)num;
		}
		this.m_distance = 600f + (float)(200 * num);
		if (!this.m_minigame.m_editing)
		{
			ucpCircleShape ucpCircleShape = new ucpCircleShape(45f, Vector2.zero, 17895696U, 0f, 0f, 0f, (ucpCollisionType)10, true);
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddStaticBody(transformC, ucpCircleShape, null);
			ChipmunkProS.AddCollisionHandler(chipmunkBodyC, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, false);
		}
		else
		{
			Vector2[] circle = DebugDraw.GetCircle(45f, 45, Vector2.zero);
			PrefabS.CreatePathPrefabComponentFromVectorArray(transformC, Vector3.zero, circle, 4f, Color.yellow, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), CameraS.m_mainCamera, Position.Center, true);
			float num2 = this.m_distance - 350f;
			num2 = 2f * this.m_distance * Mathf.Tan(EditorScene.m_screenshotCam.fieldOfView * 0.5f * 0.017453292f);
			Vector2[] rect = DebugDraw.GetRect(num2, num2 * 0.65f, Vector2.zero);
			PrefabS.CreatePathPrefabComponentFromVectorArray(transformC, Vector3.zero, rect, 4f, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), CameraS.m_mainCamera, Position.Center, true);
			Debug.LogError("visualRectSize: " + num2);
			if (this.m_minigame.m_miniGameSpriteSheet != null)
			{
				string text = "hud_camera_target_eye";
				Frame frame = this.m_minigame.m_miniGameSpriteSheet.m_atlas.GetFrame(text, null);
				SpriteC spriteC = SpriteS.AddComponent(transformC, frame, this.m_minigame.m_miniGameSpriteSheet);
				SpriteS.SetSortValue(spriteC, 1f);
				SpriteS.SetColor(spriteC, Color.white);
			}
		}
		if (this.m_minigame.m_editing)
		{
			this.CreateGraphElementTouchArea(45f, 45f, null, default(Vector2));
		}
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x0000BE84 File Offset: 0x0000A284
	protected void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (!this.m_screenShotTaken)
		{
			this.m_screenShotTaken = true;
			EditorScene.TakeManualScreenshot(this.m_distance);
		}
	}

	// Token: 0x040000B3 RID: 179
	private const int RADIUS = 45;

	// Token: 0x040000B4 RID: 180
	public int m_collidingCount;

	// Token: 0x040000B5 RID: 181
	public bool m_screenShotTaken;

	// Token: 0x040000B6 RID: 182
	public const int CAMERA_SIZES = 4;

	// Token: 0x040000B7 RID: 183
	public float m_distance;
}
