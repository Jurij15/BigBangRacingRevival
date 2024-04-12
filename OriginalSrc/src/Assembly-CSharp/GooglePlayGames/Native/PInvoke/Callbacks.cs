using System;
using System.Collections;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006D8 RID: 1752
	internal static class Callbacks
	{
		// Token: 0x0600326D RID: 12909 RVA: 0x001C9E5C File Offset: 0x001C825C
		internal static IntPtr ToIntPtr<T>(Action<T> callback, Func<IntPtr, T> conversionFunction) where T : BaseReferenceHolder
		{
			Action<IntPtr> action = delegate(IntPtr result)
			{
				using (T t = conversionFunction.Invoke(result))
				{
					if (callback != null)
					{
						callback.Invoke(t);
					}
				}
			};
			return Callbacks.ToIntPtr(action);
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x001C9E90 File Offset: 0x001C8290
		internal static IntPtr ToIntPtr<T, P>(Action<T, P> callback, Func<IntPtr, P> conversionFunction) where P : BaseReferenceHolder
		{
			Action<T, IntPtr> action = delegate(T param1, IntPtr param2)
			{
				using (P p = conversionFunction.Invoke(param2))
				{
					if (callback != null)
					{
						callback.Invoke(param1, p);
					}
				}
			};
			return Callbacks.ToIntPtr(action);
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x001C9EC4 File Offset: 0x001C82C4
		internal static IntPtr ToIntPtr(Delegate callback)
		{
			if (callback == null)
			{
				return IntPtr.Zero;
			}
			GCHandle gchandle = GCHandle.Alloc(callback);
			return GCHandle.ToIntPtr(gchandle);
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x001C9EEA File Offset: 0x001C82EA
		internal static T IntPtrToTempCallback<T>(IntPtr handle) where T : class
		{
			return Callbacks.IntPtrToCallback<T>(handle, true);
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x001C9EF4 File Offset: 0x001C82F4
		private static T IntPtrToCallback<T>(IntPtr handle, bool unpinHandle) where T : class
		{
			if (PInvokeUtilities.IsNull(handle))
			{
				return (T)((object)null);
			}
			GCHandle gchandle = GCHandle.FromIntPtr(handle);
			T t;
			try
			{
				t = (T)((object)gchandle.Target);
			}
			catch (InvalidCastException ex)
			{
				Logger.e(string.Concat(new object[]
				{
					"GC Handle pointed to unexpected type: ",
					gchandle.Target.ToString(),
					". Expected ",
					typeof(T)
				}));
				throw ex;
			}
			finally
			{
				if (unpinHandle)
				{
					gchandle.Free();
				}
			}
			return t;
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x001C9F98 File Offset: 0x001C8398
		internal static T IntPtrToPermanentCallback<T>(IntPtr handle) where T : class
		{
			return Callbacks.IntPtrToCallback<T>(handle, false);
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x001C9FA4 File Offset: 0x001C83A4
		[MonoPInvokeCallback(typeof(Callbacks.ShowUICallbackInternal))]
		internal static void InternalShowUICallback(CommonErrorStatus.UIStatus status, IntPtr data)
		{
			Logger.d("Showing UI Internal callback: " + status);
			Action<CommonErrorStatus.UIStatus> action = Callbacks.IntPtrToTempCallback<Action<CommonErrorStatus.UIStatus>>(data);
			try
			{
				action.Invoke(status);
			}
			catch (Exception ex)
			{
				Logger.e("Error encountered executing InternalShowAllUICallback. Smothering to avoid passing exception into Native: " + ex);
			}
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x001CA000 File Offset: 0x001C8400
		internal static void PerformInternalCallback(string callbackName, Callbacks.Type callbackType, IntPtr response, IntPtr userData)
		{
			Logger.d("Entering internal callback for " + callbackName);
			Action<IntPtr> action = ((callbackType != Callbacks.Type.Permanent) ? Callbacks.IntPtrToTempCallback<Action<IntPtr>>(userData) : Callbacks.IntPtrToPermanentCallback<Action<IntPtr>>(userData));
			if (action == null)
			{
				return;
			}
			try
			{
				action.Invoke(response);
			}
			catch (Exception ex)
			{
				Logger.e(string.Concat(new object[] { "Error encountered executing ", callbackName, ". Smothering to avoid passing exception into Native: ", ex }));
			}
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x001CA088 File Offset: 0x001C8488
		internal static void PerformInternalCallback<T>(string callbackName, Callbacks.Type callbackType, T param1, IntPtr param2, IntPtr userData)
		{
			Logger.d("Entering internal callback for " + callbackName);
			Action<T, IntPtr> action = null;
			try
			{
				action = ((callbackType != Callbacks.Type.Permanent) ? Callbacks.IntPtrToTempCallback<Action<T, IntPtr>>(userData) : Callbacks.IntPtrToPermanentCallback<Action<T, IntPtr>>(userData));
			}
			catch (Exception ex)
			{
				Logger.e(string.Concat(new object[] { "Error encountered converting ", callbackName, ". Smothering to avoid passing exception into Native: ", ex }));
				return;
			}
			Logger.d("Internal Callback converted to action");
			if (action == null)
			{
				return;
			}
			try
			{
				action.Invoke(param1, param2);
			}
			catch (Exception ex2)
			{
				Logger.e(string.Concat(new object[] { "Error encountered executing ", callbackName, ". Smothering to avoid passing exception into Native: ", ex2 }));
			}
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x001CA15C File Offset: 0x001C855C
		internal static Action<T> AsOnGameThreadCallback<T>(Action<T> toInvokeOnGameThread)
		{
			return delegate(T result)
			{
				if (toInvokeOnGameThread == null)
				{
					return;
				}
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					toInvokeOnGameThread.Invoke(result);
				});
			};
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x001CA184 File Offset: 0x001C8584
		internal static Action<T1, T2> AsOnGameThreadCallback<T1, T2>(Action<T1, T2> toInvokeOnGameThread)
		{
			return delegate(T1 result1, T2 result2)
			{
				if (toInvokeOnGameThread == null)
				{
					return;
				}
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					toInvokeOnGameThread.Invoke(result1, result2);
				});
			};
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x001CA1AA File Offset: 0x001C85AA
		internal static void AsCoroutine(IEnumerator routine)
		{
			PlayGamesHelperObject.RunCoroutine(routine);
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x001CA1B4 File Offset: 0x001C85B4
		internal static byte[] IntPtrAndSizeToByteArray(IntPtr data, UIntPtr dataLength)
		{
			if (dataLength.ToUInt64() == 0UL)
			{
				return null;
			}
			byte[] array = new byte[dataLength.ToUInt32()];
			Marshal.Copy(data, array, 0, (int)dataLength.ToUInt32());
			return array;
		}

		// Token: 0x040032C4 RID: 12996
		internal static readonly Action<CommonErrorStatus.UIStatus> NoopUICallback = delegate(CommonErrorStatus.UIStatus status)
		{
			Logger.d("Received UI callback: " + status);
		};

		// Token: 0x020006D9 RID: 1753
		// (Invoke) Token: 0x0600327D RID: 12925
		internal delegate void ShowUICallbackInternal(CommonErrorStatus.UIStatus status, IntPtr data);

		// Token: 0x020006DA RID: 1754
		internal enum Type
		{
			// Token: 0x040032C6 RID: 12998
			Permanent,
			// Token: 0x040032C7 RID: 12999
			Temporary
		}
	}
}
