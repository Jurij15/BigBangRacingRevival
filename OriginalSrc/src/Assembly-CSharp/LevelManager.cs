using System;

// Token: 0x02000542 RID: 1346
public static class LevelManager
{
	// Token: 0x06002796 RID: 10134 RVA: 0x001AA0E6 File Offset: 0x001A84E6
	public static uint GetUniqueId()
	{
		return LevelManager.m_uniqueID += 1U;
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x001AA0F8 File Offset: 0x001A84F8
	public static void LoadLevel(byte[] _bytes, bool _assemble = true)
	{
		LevelManager.m_uniqueID = 0U;
		if (LevelManager.m_currentLevel != null)
		{
			LevelManager.DestroyCurrentLevel();
		}
		EntityManager.Update();
		Level level = LevelSerializer.DeSerializeLevelFromBytes(_bytes);
		if (level != null)
		{
			LevelManager.m_currentLevel = level;
			LevelManager.m_currentLevel.m_currentLayer = LevelManager.m_currentLevel.m_layers[0];
			LevelManager.m_currentLevel.m_currentLayerIndex = 0;
			if (_assemble)
			{
				LevelManager.AssembleCurrentLevel();
			}
		}
	}

	// Token: 0x06002798 RID: 10136 RVA: 0x001AA162 File Offset: 0x001A8562
	public static void AssembleCurrentLevel()
	{
		if (LevelManager.m_currentLevel != null)
		{
			LevelManager.m_currentLevel.ClearItems();
			LevelManager.m_currentLevel.m_currentLayer.Initialize();
			LevelManager.m_currentLevel.m_currentLayer.Assemble();
			LevelManager.m_currentLevel.SetLayerItems();
		}
	}

	// Token: 0x06002799 RID: 10137 RVA: 0x001AA1A0 File Offset: 0x001A85A0
	public static void ClearCurrentLevel(bool _isReset)
	{
		if (LevelManager.m_currentLevel != null)
		{
			LevelManager.m_currentLevel.ClearItems();
			for (int i = 0; i < LevelManager.m_currentLevel.m_layers.Count; i++)
			{
				LevelManager.m_currentLevel.m_layers[i].Clear(_isReset);
			}
		}
	}

	// Token: 0x0600279A RID: 10138 RVA: 0x001AA1F8 File Offset: 0x001A85F8
	public static void ResetCurrentLevel()
	{
		if (LevelManager.m_currentLevel != null)
		{
			LevelManager.ClearCurrentLevel(true);
			LevelManager.m_currentLevel.m_currentLayerIndex = 0;
			LevelManager.m_currentLevel.m_currentLayer = LevelManager.m_currentLevel.m_layers[0];
			EntityManager.Update();
			LevelManager.AssembleCurrentLevel();
		}
	}

	// Token: 0x0600279B RID: 10139 RVA: 0x001AA244 File Offset: 0x001A8644
	public static void DestroyCurrentLevel()
	{
		if (LevelManager.m_currentLevel != null)
		{
			LevelManager.m_currentLevel.Destroy();
		}
		LevelManager.m_currentLevel = null;
	}

	// Token: 0x0600279C RID: 10140 RVA: 0x001AA260 File Offset: 0x001A8660
	public static Level NewLevel(int _width, int _height)
	{
		if (LevelManager.m_currentLevel != null)
		{
			LevelManager.DestroyCurrentLevel();
		}
		Level level = new Level(_width, _height);
		LevelManager.m_currentLevel = level;
		return level;
	}

	// Token: 0x0600279D RID: 10141 RVA: 0x001AA28B File Offset: 0x001A868B
	public static Level NewLevel(Level _level)
	{
		if (LevelManager.m_currentLevel != null)
		{
			LevelManager.DestroyCurrentLevel();
		}
		LevelManager.m_currentLevel = _level;
		return _level;
	}

	// Token: 0x0600279E RID: 10142 RVA: 0x001AA2A3 File Offset: 0x001A86A3
	public static void Update()
	{
		if (LevelManager.m_currentLevel != null)
		{
			LevelManager.m_currentLevel.Update();
		}
	}

	// Token: 0x04002D10 RID: 11536
	public static Level m_currentLevel;

	// Token: 0x04002D11 RID: 11537
	public static uint m_uniqueID;
}
