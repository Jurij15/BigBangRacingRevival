using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006F9 RID: 1785
	internal class NativeScorePage : BaseReferenceHolder
	{
		// Token: 0x06003380 RID: 13184 RVA: 0x001CC12C File Offset: 0x001CA52C
		internal NativeScorePage(IntPtr selfPtr)
			: base(selfPtr)
		{
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x001CC135 File Offset: 0x001CA535
		protected override void CallDispose(HandleRef selfPointer)
		{
			ScorePage.ScorePage_Dispose(selfPointer);
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x001CC13D File Offset: 0x001CA53D
		internal Types.LeaderboardCollection GetCollection()
		{
			return ScorePage.ScorePage_Collection(base.SelfPtr());
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x001CC14A File Offset: 0x001CA54A
		private UIntPtr Length()
		{
			return ScorePage.ScorePage_Entries_Length(base.SelfPtr());
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x001CC158 File Offset: 0x001CA558
		private NativeScoreEntry GetElement(UIntPtr index)
		{
			if (index.ToUInt64() >= this.Length().ToUInt64())
			{
				throw new ArgumentOutOfRangeException();
			}
			return new NativeScoreEntry(ScorePage.ScorePage_Entries_GetElement(base.SelfPtr(), index));
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x001CC196 File Offset: 0x001CA596
		public IEnumerator<NativeScoreEntry> GetEnumerator()
		{
			return PInvokeUtilities.ToEnumerator<NativeScoreEntry>(ScorePage.ScorePage_Entries_Length(base.SelfPtr()), (UIntPtr index) => this.GetElement(index));
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x001CC1B4 File Offset: 0x001CA5B4
		internal bool HasNextScorePage()
		{
			return ScorePage.ScorePage_HasNextScorePage(base.SelfPtr());
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x001CC1C1 File Offset: 0x001CA5C1
		internal bool HasPrevScorePage()
		{
			return ScorePage.ScorePage_HasPreviousScorePage(base.SelfPtr());
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x001CC1CE File Offset: 0x001CA5CE
		internal NativeScorePageToken GetNextScorePageToken()
		{
			return new NativeScorePageToken(ScorePage.ScorePage_NextScorePageToken(base.SelfPtr()));
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x001CC1E0 File Offset: 0x001CA5E0
		internal NativeScorePageToken GetPreviousScorePageToken()
		{
			return new NativeScorePageToken(ScorePage.ScorePage_PreviousScorePageToken(base.SelfPtr()));
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x001CC1F2 File Offset: 0x001CA5F2
		internal bool Valid()
		{
			return ScorePage.ScorePage_Valid(base.SelfPtr());
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x001CC1FF File Offset: 0x001CA5FF
		internal Types.LeaderboardTimeSpan GetTimeSpan()
		{
			return ScorePage.ScorePage_TimeSpan(base.SelfPtr());
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x001CC20C File Offset: 0x001CA60C
		internal Types.LeaderboardStart GetStart()
		{
			return ScorePage.ScorePage_Start(base.SelfPtr());
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x001CC219 File Offset: 0x001CA619
		internal string GetLeaderboardId()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => ScorePage.ScorePage_LeaderboardId(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x001CC22C File Offset: 0x001CA62C
		internal static NativeScorePage FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new NativeScorePage(pointer);
		}
	}
}
