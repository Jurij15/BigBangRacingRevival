using System;

// Token: 0x0200042A RID: 1066
public class MinigameSearchParametres
{
	// Token: 0x06001D90 RID: 7568 RVA: 0x0015187A File Offset: 0x0014FC7A
	public MinigameSearchParametres(string _playerUnitFilter = null, string[] _items = null, PsGameMode _gameMode = PsGameMode.Any, string[] _features = null, PsGameDifficulty _difficulty = PsGameDifficulty.Any)
	{
		this.m_playerUnitFilter = _playerUnitFilter;
		this.m_items = _items;
		this.m_gameMode = _gameMode;
		this.m_features = _features;
		this.m_difficulty = _difficulty;
	}

	// Token: 0x04002067 RID: 8295
	public string m_playerUnitFilter;

	// Token: 0x04002068 RID: 8296
	public string[] m_items;

	// Token: 0x04002069 RID: 8297
	public PsGameMode m_gameMode;

	// Token: 0x0400206A RID: 8298
	public string[] m_features;

	// Token: 0x0400206B RID: 8299
	public PsGameDifficulty m_difficulty;
}
