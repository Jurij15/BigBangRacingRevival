using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Google.Developers;
using UnityEngine;

namespace Com.Google.Android.Gms.Common.Api
{
	// Token: 0x02000615 RID: 1557
	public abstract class ResultCallbackProxy<R> : JavaInterfaceProxy, ResultCallback<R> where R : Result
	{
		// Token: 0x06002DA8 RID: 11688 RVA: 0x001C0825 File Offset: 0x001BEC25
		public ResultCallbackProxy()
			: base("com/google/android/gms/common/api/ResultCallback")
		{
		}

		// Token: 0x06002DA9 RID: 11689
		public abstract void OnResult(R arg_Result_1);

		// Token: 0x06002DAA RID: 11690 RVA: 0x001C0832 File Offset: 0x001BEC32
		public void onResult(R arg_Result_1)
		{
			this.OnResult(arg_Result_1);
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x001C083C File Offset: 0x001BEC3C
		public void onResult(AndroidJavaObject arg_Result_1)
		{
			IntPtr rawObject = arg_Result_1.GetRawObject();
			ConstructorInfo constructor = typeof(R).GetConstructor(new Type[] { rawObject.GetType() });
			R r;
			if (constructor != null)
			{
				r = (R)((object)constructor.Invoke(new object[] { rawObject }));
			}
			else
			{
				ConstructorInfo constructor2 = typeof(R).GetConstructor(new Type[0]);
				r = (R)((object)constructor2.Invoke(new object[0]));
				Marshal.PtrToStructure(rawObject, r);
			}
			this.OnResult(r);
		}

		// Token: 0x04003178 RID: 12664
		private const string CLASS_NAME = "com/google/android/gms/common/api/ResultCallback";
	}
}
