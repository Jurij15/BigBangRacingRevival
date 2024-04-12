using System;
using System.Reflection;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Native
{
	// Token: 0x020006B8 RID: 1720
	internal static class JavaUtils
	{
		// Token: 0x06003112 RID: 12562 RVA: 0x001C29B3 File Offset: 0x001C0DB3
		internal static AndroidJavaObject JavaObjectFromPointer(IntPtr jobject)
		{
			if (jobject == IntPtr.Zero)
			{
				return null;
			}
			return (AndroidJavaObject)JavaUtils.IntPtrConstructor.Invoke(new object[] { jobject });
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x001C29E8 File Offset: 0x001C0DE8
		internal static AndroidJavaObject NullSafeCall(this AndroidJavaObject target, string methodName, params object[] args)
		{
			AndroidJavaObject androidJavaObject;
			try
			{
				androidJavaObject = target.Call<AndroidJavaObject>(methodName, args);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("null"))
				{
					androidJavaObject = null;
				}
				else
				{
					Logger.w("CallObjectMethod exception: " + ex);
					androidJavaObject = null;
				}
			}
			return androidJavaObject;
		}

		// Token: 0x0400325C RID: 12892
		private static ConstructorInfo IntPtrConstructor = typeof(AndroidJavaObject).GetConstructor(36, null, new Type[] { typeof(IntPtr) }, null);
	}
}
