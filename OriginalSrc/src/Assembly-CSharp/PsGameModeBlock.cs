using System;

// Token: 0x0200011B RID: 283
public class PsGameModeBlock : PsGameModeBase
{
	// Token: 0x060007F8 RID: 2040 RVA: 0x000566A5 File Offset: 0x00054AA5
	public PsGameModeBlock(PsGameLoop _info)
		: base(PsGameMode.Race, _info)
	{
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x000566B0 File Offset: 0x00054AB0
	public override void CreateMusic()
	{
		Minigame minigame = LevelManager.m_currentLevel as Minigame;
		minigame.m_music = SoundS.AddComponent(minigame.m_environmentTC, "/Music/RacingMusic2", 1f, true);
		SoundS.PlaySound(minigame.m_music, true);
		SoundS.SetSoundParameter(minigame.m_music, "MusicState", 0f);
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00056705 File Offset: 0x00054B05
	public override void CreatePlayMenu(Action _restartAction, Action _pauseAction)
	{
		PsIngameMenu.CloseAll();
		PsIngameMenu.OpenController(false);
	}
}
