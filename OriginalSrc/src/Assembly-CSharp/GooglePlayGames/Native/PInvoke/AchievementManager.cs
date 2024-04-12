using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006D2 RID: 1746
	internal class AchievementManager
	{
		// Token: 0x0600323F RID: 12863 RVA: 0x001C99A7 File Offset: 0x001C7DA7
		internal AchievementManager(GameServices services)
		{
			this.mServices = Misc.CheckNotNull<GameServices>(services);
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x001C99BB File Offset: 0x001C7DBB
		internal void ShowAllUI(Action<CommonErrorStatus.UIStatus> callback)
		{
			Misc.CheckNotNull<Action<CommonErrorStatus.UIStatus>>(callback);
			GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_ShowAllUI(this.mServices.AsHandle(), new GooglePlayGames.Native.Cwrapper.AchievementManager.ShowAllUICallback(Callbacks.InternalShowUICallback), Callbacks.ToIntPtr(callback));
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x001C99F8 File Offset: 0x001C7DF8
		internal void FetchAll(Action<GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse> callback)
		{
			Misc.CheckNotNull<Action<GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse>>(callback);
			GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAll(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, new GooglePlayGames.Native.Cwrapper.AchievementManager.FetchAllCallback(GooglePlayGames.Native.PInvoke.AchievementManager.InternalFetchAllCallback), Callbacks.ToIntPtr<GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse>(callback, new Func<IntPtr, GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse>(GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse.FromPointer)));
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x001C9A5D File Offset: 0x001C7E5D
		[MonoPInvokeCallback(typeof(GooglePlayGames.Native.Cwrapper.AchievementManager.FetchAllCallback))]
		private static void InternalFetchAllCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("AchievementManager#InternalFetchAllCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x001C9A6C File Offset: 0x001C7E6C
		internal void Fetch(string achId, Action<GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse> callback)
		{
			Misc.CheckNotNull<string>(achId);
			Misc.CheckNotNull<Action<GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse>>(callback);
			GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_Fetch(this.mServices.AsHandle(), Types.DataSource.CACHE_OR_NETWORK, achId, new GooglePlayGames.Native.Cwrapper.AchievementManager.FetchCallback(GooglePlayGames.Native.PInvoke.AchievementManager.InternalFetchCallback), Callbacks.ToIntPtr<GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse>(callback, new Func<IntPtr, GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse>(GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse.FromPointer)));
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x001C9AD9 File Offset: 0x001C7ED9
		[MonoPInvokeCallback(typeof(GooglePlayGames.Native.Cwrapper.AchievementManager.FetchCallback))]
		private static void InternalFetchCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("AchievementManager#InternalFetchCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x001C9AE8 File Offset: 0x001C7EE8
		internal void Increment(string achievementId, uint numSteps)
		{
			Misc.CheckNotNull<string>(achievementId);
			GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_Increment(this.mServices.AsHandle(), achievementId, numSteps);
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x001C9B03 File Offset: 0x001C7F03
		internal void SetStepsAtLeast(string achivementId, uint numSteps)
		{
			Misc.CheckNotNull<string>(achivementId);
			GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_SetStepsAtLeast(this.mServices.AsHandle(), achivementId, numSteps);
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x001C9B1E File Offset: 0x001C7F1E
		internal void Reveal(string achievementId)
		{
			Misc.CheckNotNull<string>(achievementId);
			GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_Reveal(this.mServices.AsHandle(), achievementId);
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x001C9B38 File Offset: 0x001C7F38
		internal void Unlock(string achievementId)
		{
			Misc.CheckNotNull<string>(achievementId);
			GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_Unlock(this.mServices.AsHandle(), achievementId);
		}

		// Token: 0x040032BB RID: 12987
		private readonly GameServices mServices;

		// Token: 0x020006D3 RID: 1747
		internal class FetchResponse : BaseReferenceHolder
		{
			// Token: 0x06003249 RID: 12873 RVA: 0x001C9C97 File Offset: 0x001C8097
			internal FetchResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x0600324A RID: 12874 RVA: 0x001C9CA0 File Offset: 0x001C80A0
			internal CommonErrorStatus.ResponseStatus Status()
			{
				return GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x0600324B RID: 12875 RVA: 0x001C9CB0 File Offset: 0x001C80B0
			internal NativeAchievement Achievement()
			{
				IntPtr intPtr = GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchResponse_GetData(base.SelfPtr());
				return new NativeAchievement(intPtr);
			}

			// Token: 0x0600324C RID: 12876 RVA: 0x001C9CCF File Offset: 0x001C80CF
			protected override void CallDispose(HandleRef selfPointer)
			{
				GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchResponse_Dispose(selfPointer);
			}

			// Token: 0x0600324D RID: 12877 RVA: 0x001C9CD7 File Offset: 0x001C80D7
			internal static GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new GooglePlayGames.Native.PInvoke.AchievementManager.FetchResponse(pointer);
			}
		}

		// Token: 0x020006D4 RID: 1748
		internal class FetchAllResponse : BaseReferenceHolder, IEnumerable<NativeAchievement>, IEnumerable
		{
			// Token: 0x0600324E RID: 12878 RVA: 0x001C9CFD File Offset: 0x001C80FD
			internal FetchAllResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x0600324F RID: 12879 RVA: 0x001C9D06 File Offset: 0x001C8106
			internal CommonErrorStatus.ResponseStatus Status()
			{
				return GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAllResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x06003250 RID: 12880 RVA: 0x001C9D13 File Offset: 0x001C8113
			private UIntPtr Length()
			{
				return GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAllResponse_GetData_Length(base.SelfPtr());
			}

			// Token: 0x06003251 RID: 12881 RVA: 0x001C9D20 File Offset: 0x001C8120
			private NativeAchievement GetElement(UIntPtr index)
			{
				if (index.ToUInt64() >= this.Length().ToUInt64())
				{
					throw new ArgumentOutOfRangeException();
				}
				return new NativeAchievement(GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAllResponse_GetData_GetElement(base.SelfPtr(), index));
			}

			// Token: 0x06003252 RID: 12882 RVA: 0x001C9D5E File Offset: 0x001C815E
			public IEnumerator<NativeAchievement> GetEnumerator()
			{
				return PInvokeUtilities.ToEnumerator<NativeAchievement>(GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAllResponse_GetData_Length(base.SelfPtr()), (UIntPtr index) => this.GetElement(index));
			}

			// Token: 0x06003253 RID: 12883 RVA: 0x001C9D7C File Offset: 0x001C817C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003254 RID: 12884 RVA: 0x001C9D84 File Offset: 0x001C8184
			protected override void CallDispose(HandleRef selfPointer)
			{
				GooglePlayGames.Native.Cwrapper.AchievementManager.AchievementManager_FetchAllResponse_Dispose(selfPointer);
			}

			// Token: 0x06003255 RID: 12885 RVA: 0x001C9D8C File Offset: 0x001C818C
			internal static GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new GooglePlayGames.Native.PInvoke.AchievementManager.FetchAllResponse(pointer);
			}
		}
	}
}
