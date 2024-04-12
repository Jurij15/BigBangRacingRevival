using System;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

// Token: 0x0200014A RID: 330
public static class Tournaments
{
	// Token: 0x06000B0E RID: 2830 RVA: 0x0006F058 File Offset: 0x0006D458
	public static int GetCoinPrize(int _position, int _participants)
	{
		_position--;
		EventMessage eventMessage = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage;
		int prizeCoins = eventMessage.tournament.prizeCoins;
		int num = ((Tournaments.m_totalShares.Count <= _participants) ? Tournaments.m_totalShares[Tournaments.m_totalShares.Count - 1] : Tournaments.m_totalShares[_participants]);
		int roomNitroPot = eventMessage.tournament.roomNitroPot;
		int num2;
		if (_position >= 0 && _position < Tournaments.m_rewardShares.Count)
		{
			num2 = (int)((float)Tournaments.m_rewardShares[_position] / (float)num * (float)(_participants * prizeCoins + roomNitroPot));
		}
		else
		{
			int num3 = (int)((float)Tournaments.m_rewardShares[Tournaments.m_rewardShares.Count - 1] / (float)num * (float)(_participants * prizeCoins + roomNitroPot));
			int num4 = num3 / 4;
			if (_position == Tournaments.m_totalShares.Count)
			{
				num2 = num4;
			}
			else
			{
				float num5 = (float)(_position - Tournaments.m_totalShares.Count) / (float)(_participants - Tournaments.m_totalShares.Count);
				num2 = (int)((float)num4 - num5 * (float)_participants);
			}
		}
		return Math.Max(num2, 1);
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x0006F17D File Offset: 0x0006D57D
	public static int GetTotalPot(int _globalParticipants, int _prizeCoins, int _globalNitroPot)
	{
		return _globalParticipants * _prizeCoins + _globalNitroPot;
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x0006F184 File Offset: 0x0006D584
	public static GachaType GetGachaReward(int _position, int _participants)
	{
		float num = (float)_position / (float)_participants;
		GachaType gachaType = GachaType.COMMON;
		if (num <= 0.25f)
		{
			gachaType = GachaType.GOLD;
		}
		else if (num <= 0.5f)
		{
			gachaType = GachaType.SILVER;
		}
		else if (num <= 0.75f)
		{
			gachaType = GachaType.BRONZE;
		}
		if (_participants < 25)
		{
			if (_position == 1)
			{
				gachaType = GachaType.RARE;
			}
		}
		else if (_participants < 50)
		{
			if (_position == 1)
			{
				gachaType = GachaType.EPIC;
			}
			else if (_position == 2)
			{
				gachaType = GachaType.RARE;
			}
		}
		else if (_position < 4)
		{
			if (_position == 1)
			{
				gachaType = GachaType.SUPER;
			}
			else if (_position == 2)
			{
				gachaType = GachaType.EPIC;
			}
			else if (_position == 3)
			{
				gachaType = GachaType.RARE;
			}
		}
		return gachaType;
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x0006F234 File Offset: 0x0006D634
	public static void ParseTournamentRewardShares(List<object> _rewardShares)
	{
		Tournaments.m_rewardShares = new List<ObscuredInt>();
		Tournaments.m_totalShares = new List<int>();
		int num = 0;
		foreach (object obj in _rewardShares)
		{
			int num2 = Convert.ToInt32(obj);
			Tournaments.m_rewardShares.Add(num2);
			num += num2;
			Tournaments.m_totalShares.Add(num);
		}
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x0006F2C0 File Offset: 0x0006D6C0
	public static void ParseTournamentNitroPrices(TournamentInfo _info, Dictionary<string, object> _eventData)
	{
		if (_eventData.ContainsKey("nitroPack1Nitros"))
		{
			_info.nitroPack1Nitros = Convert.ToInt32(_eventData["nitroPack1Nitros"]);
		}
		else
		{
			_info.nitroPack1Nitros = 10;
		}
		if (_eventData.ContainsKey("nitroPack1Gems"))
		{
			_info.nitroPack1Gems = Convert.ToInt32(_eventData["nitroPack1Gems"]);
		}
		else
		{
			_info.nitroPack1Gems = 20;
		}
		if (_eventData.ContainsKey("nitroPack2Nitros"))
		{
			_info.nitroPack2Nitros = Convert.ToInt32(_eventData["nitroPack2Nitros"]);
		}
		else
		{
			_info.nitroPack2Nitros = 50;
		}
		if (_eventData.ContainsKey("nitroPack2Gems"))
		{
			_info.nitroPack2Gems = Convert.ToInt32(_eventData["nitroPack2Gems"]);
		}
		else
		{
			_info.nitroPack2Gems = 90;
		}
		if (_eventData.ContainsKey("nitroAdPackNitros"))
		{
			_info.nitroAdPackNitros = Convert.ToInt32(_eventData["nitroAdPackNitros"]);
		}
		else
		{
			_info.nitroAdPackNitros = 5;
		}
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x0006F3CC File Offset: 0x0006D7CC
	// Note: this type is marked as 'beforefieldinit'.
	static Tournaments()
	{
		List<ObscuredInt> list = new List<ObscuredInt>();
		list.Add(10000);
		list.Add(5000);
		list.Add(2500);
		list.Add(1000);
		list.Add(900);
		list.Add(810);
		list.Add(729);
		list.Add(656);
		list.Add(590);
		list.Add(531);
		list.Add(478);
		list.Add(430);
		list.Add(387);
		list.Add(348);
		list.Add(313);
		list.Add(282);
		list.Add(254);
		list.Add(229);
		list.Add(206);
		list.Add(185);
		list.Add(167);
		list.Add(150);
		list.Add(135);
		list.Add(122);
		list.Add(110);
		Tournaments.m_rewardShares = list;
		List<int> list2 = new List<int>();
		list2.Add(10000);
		list2.Add(15000);
		list2.Add(17500);
		list2.Add(18500);
		list2.Add(19400);
		list2.Add(20210);
		list2.Add(20939);
		list2.Add(21595);
		list2.Add(22185);
		list2.Add(22716);
		list2.Add(23194);
		list2.Add(23624);
		list2.Add(24011);
		list2.Add(24359);
		list2.Add(24672);
		list2.Add(24954);
		list2.Add(25208);
		list2.Add(25437);
		list2.Add(25643);
		list2.Add(25828);
		list2.Add(25995);
		list2.Add(26145);
		list2.Add(26280);
		list2.Add(26402);
		list2.Add(26512);
		Tournaments.m_totalShares = list2;
	}

	// Token: 0x04000A20 RID: 2592
	public static List<ObscuredInt> m_rewardShares;

	// Token: 0x04000A21 RID: 2593
	private static List<int> m_totalShares;
}
