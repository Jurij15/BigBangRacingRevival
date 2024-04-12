using System;

// Token: 0x0200013B RID: 315
public class PsLeagueData
{
	// Token: 0x06000969 RID: 2409 RVA: 0x00064D81 File Offset: 0x00063181
	public PsLeagueData(int _rank, StringID _nameEnum, int _trophyLimit, string _bannerSprite, StringID _splittedNameEnum)
	{
		this.m_rank = _rank;
		this.m_trophyLimit = _trophyLimit;
		this.m_bannerSprite = _bannerSprite;
		this.m_nameEnum = _nameEnum;
		this.m_splittedNameEnum = _splittedNameEnum;
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600096A RID: 2410 RVA: 0x00064DAE File Offset: 0x000631AE
	public string m_name
	{
		get
		{
			return PsStrings.Get(this.m_nameEnum);
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600096B RID: 2411 RVA: 0x00064DBB File Offset: 0x000631BB
	public string m_splittedName
	{
		get
		{
			return PsStrings.Get(this.m_splittedNameEnum);
		}
	}

	// Token: 0x040008EA RID: 2282
	public int m_rank;

	// Token: 0x040008EB RID: 2283
	public int m_trophyLimit;

	// Token: 0x040008EC RID: 2284
	public string m_bannerSprite;

	// Token: 0x040008ED RID: 2285
	public StringID m_nameEnum;

	// Token: 0x040008EE RID: 2286
	public StringID m_splittedNameEnum;
}
