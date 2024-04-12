using System;
using UnityEngine;

// Token: 0x02000209 RID: 521
public static class GameLevelPreview
{
	// Token: 0x06000F1B RID: 3867 RVA: 0x0008FED4 File Offset: 0x0008E2D4
	public static void InitLevelPreview(bool _showLevelPreview = false)
	{
		if (!GameLevelPreview.m_levelPreviewRunning)
		{
			GameLevelPreview.m_camIsTurned = true;
			GameLevelPreview.m_turnCamera = false;
			GameLevelPreview.m_startDelay = 120;
			CameraS.m_mainCameraRotationOffset = Vector3.up * -50f;
			CameraS.m_mainCameraPositionOffset = new Vector3(0f, 25f, 75f);
			CameraS.m_mainCameraZoomOffset = 100f;
			if (_showLevelPreview)
			{
				PsGameModeBase gameMode = PsState.m_activeGameLoop.m_gameMode;
				if (gameMode.m_playbackGhosts.Count > 0)
				{
					GameLevelPreview.m_ghost = gameMode.m_playbackGhosts[0].m_ghost;
					GameLevelPreview.m_ghostNode = GameLevelPreview.m_ghost.m_nodes["chassis"] as GhostNode;
					GameLevelPreview.m_keyFrame = 0f;
					GameLevelPreview.m_previewCameraTransform = EntityManager.AddEntityWithTC();
					Vector3 vector = GameLevelPreview.m_ghostNode.GetKeyFramePos((int)GameLevelPreview.m_keyFrame);
					TransformS.SetGlobalPosition(GameLevelPreview.m_previewCameraTransform, vector);
					GameLevelPreview.m_camTarget = CameraS.AddTargetComponent(GameLevelPreview.m_previewCameraTransform, 500f, 500f, Vector2.zero);
					CameraS.ResetFramePosition(GameLevelPreview.m_camTarget);
					for (int i = 0; i < CameraS.m_cameraTargetComponents.m_aliveCount; i++)
					{
						CameraTargetC cameraTargetC = CameraS.m_cameraTargetComponents.m_array[CameraS.m_cameraTargetComponents.m_aliveIndices[i]];
						if (cameraTargetC != GameLevelPreview.m_camTarget && cameraTargetC.m_active)
						{
							cameraTargetC.m_active = false;
							cameraTargetC.m_wasActive = true;
						}
					}
					CameraS.SnapMainCameraPos(vector);
					CameraS.SnapMainCameraFrame();
					CameraS.m_mainCameraRotationOffset = Vector3.up * -20f;
					CameraS.m_mainCameraPositionOffset = new Vector3(160f, 25f, 75f);
					CameraS.m_mainCameraZoomOffset = 0f;
					GameLevelPreview.m_levelPreviewRunning = true;
				}
			}
			else
			{
				for (int j = 0; j < CameraS.m_cameraTargetComponents.m_aliveCount; j++)
				{
					CameraTargetC cameraTargetC2 = CameraS.m_cameraTargetComponents.m_array[CameraS.m_cameraTargetComponents.m_aliveIndices[j]];
					if (cameraTargetC2 != GameLevelPreview.m_camTarget && cameraTargetC2.m_active)
					{
						cameraTargetC2.m_active = false;
						cameraTargetC2.m_wasActive = true;
					}
				}
				CameraS.SnapMainCameraFrame();
				(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.m_camTarget.m_active = true;
			}
		}
		else
		{
			Debug.LogError("Level preview already initialized!");
		}
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x00090128 File Offset: 0x0008E528
	public static void LevelPreviewWithTarget(TransformC _tc, bool _clearExistingTarget, bool _snapToTarget = true)
	{
		GameLevelPreview.m_camIsTurned = true;
		GameLevelPreview.m_turnCamera = false;
		GameLevelPreview.m_target = true;
		if ((PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.m_camTarget != null)
		{
			CameraS.RemoveTargetComponent((PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.m_camTarget);
			(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.m_camTarget = null;
		}
		else
		{
			Debug.LogError("NOT FINDING PLAYER");
		}
		if (_clearExistingTarget && GameLevelPreview.m_camTarget != null)
		{
			CameraS.RemoveTargetComponent(GameLevelPreview.m_camTarget);
		}
		GameLevelPreview.m_camTarget = CameraS.AddTargetComponent(_tc, 700f, 500f, new Vector2(0f, 35f));
		GameLevelPreview.m_camTarget.interpolateSpeed = 0.033333335f;
		GameLevelPreview.m_camTarget.frameGrowVelocityMultiplier = new Vector2(10f, 10f);
		GameLevelPreview.m_camTarget.framePosVelocityMultiplier = new Vector2(41f, 30f);
		GameLevelPreview.m_camTarget.frameScaleVelocityMultiplier = 0.02f;
		GameLevelPreview.m_camTarget.frameSlopRadiusMinMax = new Vector2(0f, 200f);
		GameLevelPreview.m_camTarget.velAngleChangeMult = new Vector2(2f, 3.5f);
		GameLevelPreview.m_camTarget.angleLimits = new Vector2(8f, 16f);
		GameLevelPreview.m_camTarget.framePeekShiftMax = new Vector2(0f, 0f);
		GameLevelPreview.m_camTarget.framePeekShiftMultiplier = 8f;
		GameLevelPreview.m_camTarget.frameWorldBounds.b = (float)(-(float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight) * 0.5f - 100f;
		SoundS.SetListener(_tc.transform.gameObject, true);
		if (_snapToTarget)
		{
			Vector3 position = _tc.transform.position;
			CameraS.SnapMainCameraPos(position);
			CameraS.SnapMainCameraFrame();
		}
		CameraS.m_mainCameraRotationOffset = Vector3.up * 0f;
		CameraS.m_mainCameraPositionOffset = new Vector3(160f, 25f, 75f);
		CameraS.m_mainCameraZoomOffset = 0f;
		GameLevelPreview.m_camIsTurned = false;
		GameLevelPreview.m_levelPreviewRunning = true;
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x00090350 File Offset: 0x0008E750
	public static void RemoveLevelPreview()
	{
		CameraS.m_mainCameraPositionOffset = Vector3.zero;
		CameraS.m_mainCameraRotationOffset = Vector3.zero;
		CameraS.m_mainCameraZoomOffset = 0f;
		GameLevelPreview.m_turnCamera = false;
		if (GameLevelPreview.m_previewCameraTransform != null)
		{
			EntityManager.RemoveEntity(GameLevelPreview.m_previewCameraTransform.p_entity);
			GameLevelPreview.m_previewCameraTransform = null;
		}
		GameLevelPreview.m_levelPreviewRunning = false;
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x000903A8 File Offset: 0x0008E7A8
	public static void TurnCameraToStartPos()
	{
		GameLevelPreview.m_cameraPositionOffset = CameraS.m_mainCameraPositionOffset;
		GameLevelPreview.m_cameraRotationOffset = CameraS.m_mainCameraRotationOffset;
		GameLevelPreview.m_cameraZoomOffset = CameraS.m_mainCameraZoomOffset;
		GameLevelPreview.m_cameraTurnStartTime = Main.m_resettingGameTime;
		GameLevelPreview.m_turnCamera = true;
		for (int i = 0; i < CameraS.m_cameraTargetComponents.m_aliveCount; i++)
		{
			CameraTargetC cameraTargetC = CameraS.m_cameraTargetComponents.m_array[CameraS.m_cameraTargetComponents.m_aliveIndices[i]];
			if (!cameraTargetC.m_active && cameraTargetC.m_wasActive)
			{
				cameraTargetC.m_active = true;
			}
		}
		if (GameLevelPreview.m_levelPreviewRunning)
		{
			GameLevelPreview.m_levelPreviewRunning = false;
			if (GameLevelPreview.m_previewCameraTransform != null)
			{
				Vector3 currentPosition = GameLevelPreview.m_ghost.GetCurrentPosition(GameLevelPreview.m_ghostNode, 0f, -1);
				TransformS.SetGlobalPosition(GameLevelPreview.m_previewCameraTransform, currentPosition);
				CameraS.ResetFramePosition(GameLevelPreview.m_camTarget);
				GameLevelPreview.m_camTarget.m_active = false;
				GameLevelPreview.m_camTarget.m_wasActive = false;
				EntityManager.RemoveEntity(GameLevelPreview.m_previewCameraTransform.p_entity);
				GameLevelPreview.m_previewCameraTransform = null;
				if ((PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.m_camTarget == null)
				{
					(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.CreateCamTarget((PsState.m_activeMinigame.m_playerUnit as Vehicle).m_camTargetTC);
				}
				CameraS.SnapMainCameraPos(currentPosition);
				CameraS.SnapMainCameraFrame();
			}
			else if (GameLevelPreview.m_target)
			{
				if ((PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.m_camTarget == null)
				{
					(PsState.m_activeMinigame.m_playerUnit as Vehicle).m_alienCharacter.CreateCamTarget((PsState.m_activeMinigame.m_playerUnit as Vehicle).m_camTargetTC);
				}
				CameraS.SnapMainCameraPos((PsState.m_activeMinigame.m_playerUnit as Vehicle).m_camTargetTC.transform.position);
				CameraS.SnapMainCameraFrame();
				GameLevelPreview.m_target = false;
			}
		}
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x0009058C File Offset: 0x0008E98C
	public static void StepLevelPreview()
	{
		if (GameLevelPreview.m_turnCamera)
		{
			float num = 2.5f;
			float num2 = Main.m_resettingGameTime - GameLevelPreview.m_cameraTurnStartTime;
			if (num2 <= num)
			{
				float num3 = TweenS.tween(TweenStyle.QuadInOut, Mathf.Min(num2, num), num, 1f, -1f);
				CameraS.m_mainCameraPositionOffset = GameLevelPreview.m_cameraPositionOffset * num3;
				CameraS.m_mainCameraRotationOffset = GameLevelPreview.m_cameraRotationOffset * num3;
				CameraS.m_mainCameraZoomOffset = GameLevelPreview.m_cameraZoomOffset * num3;
			}
			else
			{
				GameLevelPreview.m_camIsTurned = false;
				GameLevelPreview.m_turnCamera = false;
			}
		}
		if (GameLevelPreview.m_levelPreviewRunning && !GameLevelPreview.m_target)
		{
			Vector3 currentPosition = GameLevelPreview.m_ghost.GetCurrentPosition(GameLevelPreview.m_ghostNode, GameLevelPreview.m_keyFrame, -1);
			TransformS.SetGlobalPosition(GameLevelPreview.m_previewCameraTransform, currentPosition);
			if (GameLevelPreview.m_startDelay < 0)
			{
				GameLevelPreview.m_keyFrame += 1f / (float)GameLevelPreview.m_ghost.m_frameSkip * (float)GameLevelPreview.m_previewDir;
				if (GameLevelPreview.m_keyFrame > (float)(GameLevelPreview.m_ghost.m_keyframeCount - 1))
				{
					GameLevelPreview.m_keyFrame = (float)Mathf.Max(0, GameLevelPreview.m_ghost.m_keyframeCount - 1);
					GameLevelPreview.m_previewDir = -1;
				}
				else if (GameLevelPreview.m_keyFrame < 0f)
				{
					GameLevelPreview.m_keyFrame = 0f;
					GameLevelPreview.m_previewDir = 1;
				}
			}
			else
			{
				GameLevelPreview.m_startDelay--;
			}
		}
	}

	// Token: 0x040011F0 RID: 4592
	public static Vector3 m_cameraPositionOffset;

	// Token: 0x040011F1 RID: 4593
	public static Vector3 m_cameraRotationOffset;

	// Token: 0x040011F2 RID: 4594
	public static float m_cameraZoomOffset;

	// Token: 0x040011F3 RID: 4595
	public static float m_cameraTurnStartTime;

	// Token: 0x040011F4 RID: 4596
	public static bool m_camIsTurned = true;

	// Token: 0x040011F5 RID: 4597
	private static bool m_turnCamera;

	// Token: 0x040011F6 RID: 4598
	public static bool m_levelPreviewRunning;

	// Token: 0x040011F7 RID: 4599
	public static int m_previewDir = 1;

	// Token: 0x040011F8 RID: 4600
	public static TransformC m_previewCameraTransform;

	// Token: 0x040011F9 RID: 4601
	public static Ghost m_ghost;

	// Token: 0x040011FA RID: 4602
	public static GhostNode m_ghostNode;

	// Token: 0x040011FB RID: 4603
	private static int m_startDelay;

	// Token: 0x040011FC RID: 4604
	private static float m_keyFrame;

	// Token: 0x040011FD RID: 4605
	private static CameraTargetC m_camTarget;

	// Token: 0x040011FE RID: 4606
	private static bool m_target;
}
