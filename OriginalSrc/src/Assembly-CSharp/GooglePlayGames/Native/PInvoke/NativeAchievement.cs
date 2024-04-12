using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006EA RID: 1770
	internal class NativeAchievement : BaseReferenceHolder
	{
		// Token: 0x060032F4 RID: 13044 RVA: 0x001CB437 File Offset: 0x001C9837
		internal NativeAchievement(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x001CB440 File Offset: 0x001C9840
		internal uint CurrentSteps()
		{
			return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_CurrentSteps(base.SelfPtr());
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x001CB44D File Offset: 0x001C984D
		internal string Description()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Description(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x001CB460 File Offset: 0x001C9860
		internal string Id()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Id(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x001CB473 File Offset: 0x001C9873
		internal string Name()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Name(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x001CB486 File Offset: 0x001C9886
		internal Types.AchievementState State()
		{
			return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_State(base.SelfPtr());
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x001CB493 File Offset: 0x001C9893
		internal uint TotalSteps()
		{
			return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_TotalSteps(base.SelfPtr());
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x001CB4A0 File Offset: 0x001C98A0
		internal Types.AchievementType Type()
		{
			return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Type(base.SelfPtr());
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x001CB4AD File Offset: 0x001C98AD
		internal ulong LastModifiedTime()
		{
			if (GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Valid(base.SelfPtr()))
			{
				return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_LastModifiedTime(base.SelfPtr());
			}
			return 0UL;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x001CB4CD File Offset: 0x001C98CD
		internal ulong getXP()
		{
			return GooglePlayGames.Native.Cwrapper.Achievement.Achievement_XP(base.SelfPtr());
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x001CB4DA File Offset: 0x001C98DA
		internal string getRevealedImageUrl()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => GooglePlayGames.Native.Cwrapper.Achievement.Achievement_RevealedIconUrl(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x001CB4ED File Offset: 0x001C98ED
		internal string getUnlockedImageUrl()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => GooglePlayGames.Native.Cwrapper.Achievement.Achievement_UnlockedIconUrl(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x001CB500 File Offset: 0x001C9900
		protected override void CallDispose(HandleRef selfPointer)
		{
			GooglePlayGames.Native.Cwrapper.Achievement.Achievement_Dispose(selfPointer);
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x001CB508 File Offset: 0x001C9908
		internal GooglePlayGames.BasicApi.Achievement AsAchievement()
		{
			GooglePlayGames.BasicApi.Achievement achievement = new GooglePlayGames.BasicApi.Achievement();
			achievement.Id = this.Id();
			achievement.Name = this.Name();
			achievement.Description = this.Description();
			DateTime dateTime;
			dateTime..ctor(1970, 1, 1, 0, 0, 0, 0, 1);
			ulong num = this.LastModifiedTime();
			if (num == 18446744073709551615UL)
			{
				num = 0UL;
			}
			achievement.LastModifiedTime = dateTime.AddMilliseconds(num);
			achievement.Points = this.getXP();
			achievement.RevealedImageUrl = this.getRevealedImageUrl();
			achievement.UnlockedImageUrl = this.getUnlockedImageUrl();
			if (this.Type() == Types.AchievementType.INCREMENTAL)
			{
				achievement.IsIncremental = true;
				achievement.CurrentSteps = (int)this.CurrentSteps();
				achievement.TotalSteps = (int)this.TotalSteps();
			}
			achievement.IsRevealed = this.State() == Types.AchievementState.REVEALED || this.State() == Types.AchievementState.UNLOCKED;
			achievement.IsUnlocked = this.State() == Types.AchievementState.UNLOCKED;
			return achievement;
		}

		// Token: 0x040032DD RID: 13021
		private const ulong MinusOne = 18446744073709551615UL;
	}
}
