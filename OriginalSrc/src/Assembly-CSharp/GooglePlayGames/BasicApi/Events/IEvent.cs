using System;

namespace GooglePlayGames.BasicApi.Events
{
	// Token: 0x020005CF RID: 1487
	public interface IEvent
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06002B37 RID: 11063
		string Id { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06002B38 RID: 11064
		string Name { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06002B39 RID: 11065
		string Description { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06002B3A RID: 11066
		string ImageUrl { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06002B3B RID: 11067
		ulong CurrentCount { get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06002B3C RID: 11068
		EventVisibility Visibility { get; }
	}
}
