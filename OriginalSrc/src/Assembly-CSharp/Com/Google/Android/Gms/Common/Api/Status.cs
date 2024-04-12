using System;
using Google.Developers;

namespace Com.Google.Android.Gms.Common.Api
{
	// Token: 0x02000616 RID: 1558
	public class Status : JavaObjWrapper, Result
	{
		// Token: 0x06002DAC RID: 11692 RVA: 0x001C1B91 File Offset: 0x001BFF91
		public Status(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x001C1B9A File Offset: 0x001BFF9A
		public Status(int arg_int_1, string arg_string_2, object arg_object_3)
		{
			base.CreateInstance("com/google/android/gms/common/api/Status", new object[] { arg_int_1, arg_string_2, arg_object_3 });
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x001C1BC4 File Offset: 0x001BFFC4
		public Status(int arg_int_1, string arg_string_2)
		{
			base.CreateInstance("com/google/android/gms/common/api/Status", new object[] { arg_int_1, arg_string_2 });
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x001C1BEA File Offset: 0x001BFFEA
		public Status(int arg_int_1)
		{
			base.CreateInstance("com/google/android/gms/common/api/Status", new object[] { arg_int_1 });
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x001C1C0C File Offset: 0x001C000C
		public static object CREATOR
		{
			get
			{
				return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/common/api/Status", "CREATOR", "Landroid/os/Parcelable$Creator;");
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x001C1C22 File Offset: 0x001C0022
		public static string NULL
		{
			get
			{
				return JavaObjWrapper.GetStaticStringField("com/google/android/gms/common/api/Status", "NULL");
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x001C1C33 File Offset: 0x001C0033
		public static int CONTENTS_FILE_DESCRIPTOR
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/api/Status", "CONTENTS_FILE_DESCRIPTOR");
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x001C1C44 File Offset: 0x001C0044
		public static int PARCELABLE_WRITE_RETURN_VALUE
		{
			get
			{
				return JavaObjWrapper.GetStaticIntField("com/google/android/gms/common/api/Status", "PARCELABLE_WRITE_RETURN_VALUE");
			}
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x001C1C55 File Offset: 0x001C0055
		public bool equals(object arg_object_1)
		{
			return base.InvokeCall<bool>("equals", "(Ljava/lang/Object;)Z", new object[] { arg_object_1 });
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x001C1C71 File Offset: 0x001C0071
		public string toString()
		{
			return base.InvokeCall<string>("toString", "()Ljava/lang/String;", new object[0]);
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x001C1C89 File Offset: 0x001C0089
		public int hashCode()
		{
			return base.InvokeCall<int>("hashCode", "()I", new object[0]);
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x001C1CA1 File Offset: 0x001C00A1
		public bool isInterrupted()
		{
			return base.InvokeCall<bool>("isInterrupted", "()Z", new object[0]);
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x001C1CB9 File Offset: 0x001C00B9
		public Status getStatus()
		{
			return base.InvokeCall<Status>("getStatus", "()Lcom/google/android/gms/common/api/Status;", new object[0]);
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x001C1CD1 File Offset: 0x001C00D1
		public bool isCanceled()
		{
			return base.InvokeCall<bool>("isCanceled", "()Z", new object[0]);
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x001C1CE9 File Offset: 0x001C00E9
		public int describeContents()
		{
			return base.InvokeCall<int>("describeContents", "()I", new object[0]);
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x001C1D01 File Offset: 0x001C0101
		public object getResolution()
		{
			return base.InvokeCall<object>("getResolution", "()Landroid/app/PendingIntent;", new object[0]);
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x001C1D19 File Offset: 0x001C0119
		public int getStatusCode()
		{
			return base.InvokeCall<int>("getStatusCode", "()I", new object[0]);
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x001C1D31 File Offset: 0x001C0131
		public string getStatusMessage()
		{
			return base.InvokeCall<string>("getStatusMessage", "()Ljava/lang/String;", new object[0]);
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x001C1D49 File Offset: 0x001C0149
		public bool hasResolution()
		{
			return base.InvokeCall<bool>("hasResolution", "()Z", new object[0]);
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x001C1D61 File Offset: 0x001C0161
		public void startResolutionForResult(object arg_object_1, int arg_int_2)
		{
			base.InvokeCallVoid("startResolutionForResult", "(Landroid/app/Activity;I)V", new object[] { arg_object_1, arg_int_2 });
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x001C1D86 File Offset: 0x001C0186
		public void writeToParcel(object arg_object_1, int arg_int_2)
		{
			base.InvokeCallVoid("writeToParcel", "(Landroid/os/Parcel;I)V", new object[] { arg_object_1, arg_int_2 });
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x001C1DAB File Offset: 0x001C01AB
		public bool isSuccess()
		{
			return base.InvokeCall<bool>("isSuccess", "()Z", new object[0]);
		}

		// Token: 0x04003179 RID: 12665
		private const string CLASS_NAME = "com/google/android/gms/common/api/Status";
	}
}
