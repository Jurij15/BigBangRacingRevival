using System;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x02000606 RID: 1542
	public class Logger
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x001BFE21 File Offset: 0x001BE221
		// (set) Token: 0x06002D33 RID: 11571 RVA: 0x001BFE28 File Offset: 0x001BE228
		public static bool DebugLogEnabled
		{
			get
			{
				return Logger.debugLogEnabled;
			}
			set
			{
				Logger.debugLogEnabled = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x001BFE30 File Offset: 0x001BE230
		// (set) Token: 0x06002D35 RID: 11573 RVA: 0x001BFE37 File Offset: 0x001BE237
		public static bool WarningLogEnabled
		{
			get
			{
				return Logger.warningLogEnabled;
			}
			set
			{
				Logger.warningLogEnabled = value;
			}
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x001BFE40 File Offset: 0x001BE240
		public static void d(string msg)
		{
			if (Logger.debugLogEnabled)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					Debug.Log(Logger.ToLogMessage(string.Empty, "DEBUG", msg));
				});
			}
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x001BFE78 File Offset: 0x001BE278
		public static void w(string msg)
		{
			if (Logger.warningLogEnabled)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					Debug.LogWarning(Logger.ToLogMessage("!!!", "WARNING", msg));
				});
			}
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x001BFEB0 File Offset: 0x001BE2B0
		public static void e(string msg)
		{
			if (Logger.warningLogEnabled)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					Debug.LogWarning(Logger.ToLogMessage("***", "ERROR", msg));
				});
			}
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x001BFEE5 File Offset: 0x001BE2E5
		public static string describe(byte[] b)
		{
			return (b != null) ? ("byte[" + b.Length + "]") : "(null)";
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x001BFF10 File Offset: 0x001BE310
		private static string ToLogMessage(string prefix, string logType, string msg)
		{
			return string.Format("{0} [Play Games Plugin DLL] {1} {2}: {3}", new object[]
			{
				prefix,
				DateTime.Now.ToString("MM/dd/yy H:mm:ss zzz"),
				logType,
				msg
			});
		}

		// Token: 0x04003152 RID: 12626
		private static bool debugLogEnabled;

		// Token: 0x04003153 RID: 12627
		private static bool warningLogEnabled = true;
	}
}
