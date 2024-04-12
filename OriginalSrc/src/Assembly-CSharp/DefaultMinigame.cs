using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
public static class DefaultMinigame
{
	// Token: 0x06000BA0 RID: 2976 RVA: 0x00073444 File Offset: 0x00071844
	public static void ApplyWizard(Minigame _minigame = null)
	{
		if (string.IsNullOrEmpty(PsState.m_wizardPlayer))
		{
			PsState.m_wizardPlayer = "Motorcycle";
			PsState.m_wizardGameMode = "Race";
			int num = 0;
			foreach (PsUnlockable psUnlockable in PsMetagameData.m_gameAreas[0].m_items)
			{
				PsEditorItem psEditorItem = (PsEditorItem)psUnlockable;
				if (psEditorItem.m_unlocked)
				{
					if (psEditorItem.m_identifier == "AreaMedium")
					{
						num = 1;
					}
					if (psEditorItem.m_identifier == "AreaLarge")
					{
						num = 2;
						break;
					}
				}
			}
			PsState.m_wizardDomeIndex = num;
		}
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		if (_minigame != null)
		{
			minigame = _minigame;
			LevelManager.m_currentLevel = minigame;
		}
		minigame.m_groundNode = new LevelGroundNode();
		LevelManager.m_currentLevel.m_currentLayer.AddElement(minigame.m_groundNode);
		Vector2 vector = PsState.m_domeSizes[PsState.m_wizardDomeIndex] * 2048f;
		Vector2 vector2;
		vector2..ctor(-vector.x * 0.5f + 1200f, (float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * -0.5f + 90f);
		Vector2 vector3;
		vector3..ctor(vector.x * 0.5f - 1200f, (float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * -0.5f + 90f);
		GraphNode graphNode = new GraphNode(GraphNodeType.Normal, typeof(Goal), "Goal", vector3 + Vector2.right * 500f, Vector3.zero, Vector3.one);
		LevelPlayerNode levelPlayerNode = new LevelPlayerNode(Type.GetType(PsState.m_wizardPlayer), "Player", vector2 + Vector2.right * -500f, Vector3.zero, Vector3.one);
		LevelManager.AssembleCurrentLevel();
		minigame.UpdateAreaLimits(PsState.m_wizardDomeIndex, true, true);
		AutoGeometryManager.m_layers[0].PregenerateRandomGround(20f);
		AutoGeometryManager.m_layers[0].m_bytes.CopyTo(minigame.m_groundNode.m_AGLayerData[0], 0);
		AutoGeometryManager.m_layers[0].MarchTiles(new cpBB(0f, 0f, (float)AutoGeometryManager.m_width, (float)AutoGeometryManager.m_height));
		AutoGeometryManager.m_layers[0].TakeSnapshot();
		AutoGeometryManager.m_layers[0].CopyByteArrayToMaskTexture(AutoGeometryManager.m_layers[0].m_maskTexture, AutoGeometryManager.m_layers[0].m_bytes);
		AutoGeometryManager.UpdateMaxValueLookupTable(AutoGeometryManager.m_layers[0], LookupUpdateMode.IN_EDITOR, false);
		PsState.m_activeMinigame.SetGameMode(PsState.m_wizardGameMode, vector2, true);
		ucpSegmentQueryInfo ucpSegmentQueryInfo = default(ucpSegmentQueryInfo);
		ucpShapeFilter ucpShapeFilter = default(ucpShapeFilter);
		ucpShapeFilter.ucpShapeFilterAll();
		graphNode.m_position.y = (float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * -0.5f + 500f;
		ChipmunkProWrapper.ucpSpaceSegmentQueryFirst(graphNode.m_position, graphNode.m_position + new Vector3(0f, -10000f, 0f), 1f, ucpShapeFilter, ref ucpSegmentQueryInfo);
		GraphNode graphNode2 = graphNode;
		graphNode2.m_position.y = graphNode2.m_position.y + (-10000f * ucpSegmentQueryInfo.alpha + 90f);
		levelPlayerNode.m_position.y = (float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * -0.5f + 500f;
		ChipmunkProWrapper.ucpSpaceSegmentQueryFirst(levelPlayerNode.m_position, levelPlayerNode.m_position + new Vector3(0f, -10000f, 0f), 1f, ucpShapeFilter, ref ucpSegmentQueryInfo);
		LevelPlayerNode levelPlayerNode2 = levelPlayerNode;
		levelPlayerNode2.m_position.y = levelPlayerNode2.m_position.y + (-10000f * ucpSegmentQueryInfo.alpha + 90f);
		LevelManager.m_currentLevel.m_currentLayer.AddElement(levelPlayerNode);
		LevelManager.m_currentLevel.m_currentLayer.AddElement(graphNode);
		graphNode.Assemble();
		levelPlayerNode.Assemble();
		PsState.m_editorCameraPos = vector2 + Vector2.up * 200f + new Vector2(-200f, 0f);
		PsState.m_editorCameraZoom = 1500f;
		Debug.Log(string.Concat(new object[]
		{
			PsState.m_wizardPlayer,
			" ",
			PsState.m_wizardGameMode,
			" ",
			PsState.m_wizardDomeIndex
		}), null);
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00073908 File Offset: 0x00071D08
	public static Minigame Assemble()
	{
		Debug.LogWarning("DEFAULT MINIGAME SETTING DOME SIZE INDEX TO 0");
		Minigame minigame = LevelManager.NewLevel(new Minigame((int)(2048f * PsState.m_editorAreaSize.x) + 256, (int)(2048f * PsState.m_editorAreaSize.y) + 256)) as Minigame;
		minigame.m_settings.Add("environment", "default");
		int num = 0;
		foreach (PsUnlockable psUnlockable in PsMetagameData.m_gameAreas[0].m_items)
		{
			PsEditorItem psEditorItem = (PsEditorItem)psUnlockable;
			if (psEditorItem.m_unlocked)
			{
				if (psEditorItem.m_identifier == "AreaMedium")
				{
					num = 1;
				}
				if (psEditorItem.m_identifier == "AreaLarge")
				{
					num = 2;
					break;
				}
			}
		}
		PsState.m_wizardDomeIndex = num;
		Debug.LogWarning("NEW LEVEL DOME SIZE INDEX: " + num);
		minigame.m_settings.Add("domeSizeIndex", num);
		minigame.m_settings.Add("bgSeed", Main.m_gameTicks);
		return minigame;
	}
}
