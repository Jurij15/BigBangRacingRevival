using System;
using Google.Developers;

namespace Com.Google.Android.Gms.Common
{
	// Token: 0x02000617 RID: 1559
	public class ConnectionResult : JavaObjWrapper
	{
		// Token: 0x06002DC2 RID: 11714 RVA: 0x001C1DC3 File Offset: 0x001C01C3
		public ConnectionResult(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x001C1DCC File Offset: 0x001C01CC
		public ConnectionResult(int arg_int_1, object arg_object_2, string arg_string_3)
		{
			base.CreateInstance("com/google/android/gms/common/ConnectionResult", new object[] { arg_int_1, arg_object_2, arg_string_3 });
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x001C1DF6 File Offset: 0x001C01F6
		public ConnectionResult(int arg_int_1, object arg_object_2)
		{
			base.CreateInstance("com/google/android/gms/common/ConnectionResult", new object[] { arg_int_1, arg_object_2 });
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x001C1E1C File Offset: 0x001C021C
		public ConnectionResult(int arg_int_1)
		{
			base.CreateInstance("com/google/android/gms/common/ConnectionResult", new object[] { arg_int_1 });
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x001C1E3E File Offset: 0x001C023E
		public static int SUCCESS
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "SUCCESS");
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x001C1E4F File Offset: 0x001C024F
		public static int SERVICE_MISSING
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "SERVICE_MISSING");
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x001C1E60 File Offset: 0x001C0260
		public static int SERVICE_VERSION_UPDATE_REQUIRED
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "SERVICE_VERSION_UPDATE_REQUIRED");
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x001C1E71 File Offset: 0x001C0271
		public static int SERVICE_DISABLED
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "SERVICE_DISABLED");
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x001C1E82 File Offset: 0x001C0282
		public static int SIGN_IN_REQUIRED
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "SIGN_IN_REQUIRED");
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06002DCB RID: 11723 RVA: 0x001C1E93 File Offset: 0x001C0293
		public static int INVALID_ACCOUNT
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "INVALID_ACCOUNT");
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x001C1EA4 File Offset: 0x001C02A4
		public static int RESOLUTION_REQUIRED
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "RESOLUTION_REQUIRED");
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x001C1EB5 File Offset: 0x001C02B5
		public static int NETWORK_ERROR
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "NETWORK_ERROR");
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x001C1EC6 File Offset: 0x001C02C6
		public static int INTERNAL_ERROR
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "INTERNAL_ERROR");
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x001C1ED7 File Offset: 0x001C02D7
		public static int SERVICE_INVALID
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "SERVICE_INVALID");
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x001C1EE8 File Offset: 0x001C02E8
		public static int DEVELOPER_ERROR
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "DEVELOPER_ERROR");
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x001C1EF9 File Offset: 0x001C02F9
		public static int LICENSE_CHECK_FAILED
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "LICENSE_CHECK_FAILED");
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x001C1F0A File Offset: 0x001C030A
		public static int CANCELED
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "CANCELED");
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x001C1F1B File Offset: 0x001C031B
		public static int TIMEOUT
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "TIMEOUT");
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x001C1F2C File Offset: 0x001C032C
		public static int INTERRUPTED
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "INTERRUPTED");
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x001C1F3D File Offset: 0x001C033D
		public static int API_UNAVAILABLE
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "API_UNAVAILABLE");
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x001C1F4E File Offset: 0x001C034E
		public static int SIGN_IN_FAILED
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "SIGN_IN_FAILED");
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x001C1F5F File Offset: 0x001C035F
		public static int SERVICE_UPDATING
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "SERVICE_UPDATING");
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x001C1F70 File Offset: 0x001C0370
		public static int SERVICE_MISSING_PERMISSION
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "SERVICE_MISSING_PERMISSION");
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x001C1F81 File Offset: 0x001C0381
		public static int DRIVE_EXTERNAL_STORAGE_REQUIRED
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "DRIVE_EXTERNAL_STORAGE_REQUIRED");
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x001C1F92 File Offset: 0x001C0392
		public static object CREATOR
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/common/ConnectionResult", "CREATOR", "Landroid/os/Parcelable$Creator;");
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06002DDB RID: 11739 RVA: 0x001C1FA8 File Offset: 0x001C03A8
		public static string NULL
		{
			get
			{
				return JavaObjWrapper.GetStaticStringField("com/google/android/gms/common/ConnectionResult", "NULL");
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x001C1FB9 File Offset: 0x001C03B9
		public static int CONTENTS_FILE_DESCRIPTOR
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "CONTENTS_FILE_DESCRIPTOR");
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x001C1FCA File Offset: 0x001C03CA
		public static int PARCELABLE_WRITE_RETURN_VALUE
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/ConnectionResult", "PARCELABLE_WRITE_RETURN_VALUE");
			}
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x001C1FDB File Offset: 0x001C03DB
		public bool equals(object arg_object_1)
		{
			return base.InvokeCall<bool>("equals", "(Ljava/lang/Object;)Z", new object[] { arg_object_1 });
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x001C1FF7 File Offset: 0x001C03F7
		public string toString()
		{
			return base.InvokeCall<string>("toString", "()Ljava/lang/String;", new object[0]);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x001C200F File Offset: 0x001C040F
		public int hashCode()
		{
			return base.InvokeCall<int>("hashCode", "()I", new object[0]);
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x001C2027 File Offset: 0x001C0427
		public int describeContents()
		{
			return base.InvokeCall<int>("describeContents", "()I", new object[0]);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x001C203F File Offset: 0x001C043F
		public object getResolution()
		{
			return base.InvokeCall<object>("getResolution", "()Landroid/app/PendingIntent;", new object[0]);
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x001C2057 File Offset: 0x001C0457
		public bool hasResolution()
		{
			return base.InvokeCall<bool>("hasResolution", "()Z", new object[0]);
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x001C206F File Offset: 0x001C046F
		public void startResolutionForResult(object arg_object_1, int arg_int_2)
		{
			base.InvokeCallVoid("startResolutionForResult", "(Landroid/app/Activity;I)V", new object[] { arg_object_1, arg_int_2 });
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x001C2094 File Offset: 0x001C0494
		public void writeToParcel(object arg_object_1, int arg_int_2)
		{
			base.InvokeCallVoid("writeToParcel", "(Landroid/os/Parcel;I)V", new object[] { arg_object_1, arg_int_2 });
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x001C20B9 File Offset: 0x001C04B9
		public int getErrorCode()
		{
			return base.InvokeCall<int>("getErrorCode", "()I", new object[0]);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x001C20D1 File Offset: 0x001C04D1
		public string getErrorMessage()
		{
			return base.InvokeCall<string>("getErrorMessage", "()Ljava/lang/String;", new object[0]);
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x001C20E9 File Offset: 0x001C04E9
		public bool isSuccess()
		{
			return base.InvokeCall<bool>("isSuccess", "()Z", new object[0]);
		}

		// Token: 0x0400317A RID: 12666
		private const string CLASS_NAME = "com/google/android/gms/common/ConnectionResult";
	}
}
