using System;

namespace AdMediation
{
	// Token: 0x020004A0 RID: 1184
	public class AdNetwork
	{
		// Token: 0x060021E3 RID: 8675 RVA: 0x00189E62 File Offset: 0x00188262
		public AdNetwork(string _name, int _priority, Action _initialize, Func<bool> _adsAvailable, Action<string, Action<AdResult>> _showAd, bool _enabled)
		{
			this.networkName = _name;
			this.adsAvailable = _adsAvailable;
			this.showAd = _showAd;
			this.m_priority = _priority;
			this.initialize = _initialize;
			this.m_enabled = _enabled;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x00189E97 File Offset: 0x00188297
		public bool enabled
		{
			get
			{
				return this.m_enabled;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x00189E9F File Offset: 0x0018829F
		public int priority
		{
			get
			{
				return this.m_priority;
			}
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x00189EA7 File Offset: 0x001882A7
		public void Enable(bool _value)
		{
			this.m_enabled = _value;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00189EB0 File Offset: 0x001882B0
		public void SetPriority(int _value)
		{
			this.m_priority = _value;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x00189EB9 File Offset: 0x001882B9
		public void SetConfig(NetworkConfig _cfg)
		{
			this.m_enabled = _cfg.enabled;
			this.m_priority = _cfg.priority;
		}

		// Token: 0x04002801 RID: 10241
		private bool m_enabled;

		// Token: 0x04002802 RID: 10242
		private int m_priority;

		// Token: 0x04002803 RID: 10243
		public Func<bool> adsAvailable;

		// Token: 0x04002804 RID: 10244
		public Action<string, Action<AdResult>> showAd;

		// Token: 0x04002805 RID: 10245
		public Action initialize;

		// Token: 0x04002806 RID: 10246
		public string networkName;
	}
}
