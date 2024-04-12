using System;
using CodeStage.AntiCheat.ObscuredTypes;

// Token: 0x02000023 RID: 35
public class Booster
{
	// Token: 0x060000F7 RID: 247 RVA: 0x0000C06D File Offset: 0x0000A46D
	public Booster(bool _free)
	{
		this.m_free = _free;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000C07C File Offset: 0x0000A47C
	public void SetBoosterButton(PsUIBoosterButton _button)
	{
		this.m_boosterButton = _button;
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x0000C088 File Offset: 0x0000A488
	public virtual void Use(Vehicle _vehicle)
	{
		if (!PsMetagameManager.IsTimedGiftActive(EventGiftTimedType.unlimitedNitros))
		{
			if (PsState.m_activeGameLoop is PsGameLoopTournament)
			{
				PsMetagameManager.m_playerStats.CumulateTournamentBoosters(-1);
			}
			else
			{
				PsMetagameManager.m_playerStats.CumulateBoosters(-1);
			}
		}
		Minigame activeMinigame = PsState.m_activeMinigame;
		activeMinigame.m_usedBoosters = ObscuredInt.op_Increment(activeMinigame.m_usedBoosters);
		this.m_boosterButton.UseBoost();
		this.m_used = true;
		PsState.m_activeGameLoop.m_boosterUsed = true;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0000C0FC File Offset: 0x0000A4FC
	public static bool IsBoosterAllowed()
	{
		return PsState.m_activeGameLoop is PsGameLoopTournament || PsState.m_activeGameLoop is PsGameLoopAdventureBattle || (PlayerPrefsX.GetOffroadRacing() && PsState.m_activeGameLoop.m_context != PsMinigameContext.Fresh && PsState.m_activeGameLoop.m_context != PsMinigameContext.Editor);
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0000C158 File Offset: 0x0000A558
	protected static bool IsBoosterAvailable()
	{
		if (PsState.m_activeGameLoop is PsGameLoopTournament)
		{
			return PsMetagameManager.m_playerStats.tournamentBoosters > 0;
		}
		return PsMetagameManager.m_playerStats.boosters > 0;
	}

	// Token: 0x040000B9 RID: 185
	public PsUIBoosterButton m_boosterButton;

	// Token: 0x040000BA RID: 186
	public bool m_free;

	// Token: 0x040000BB RID: 187
	public bool m_used;

	// Token: 0x040000BC RID: 188
	public bool m_available;
}
