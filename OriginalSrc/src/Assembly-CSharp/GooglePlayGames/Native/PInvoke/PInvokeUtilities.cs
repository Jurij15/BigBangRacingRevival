using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000706 RID: 1798
	internal static class PInvokeUtilities
	{
		// Token: 0x06003401 RID: 13313 RVA: 0x001CCF8A File Offset: 0x001CB38A
		internal static HandleRef CheckNonNull(HandleRef reference)
		{
			if (PInvokeUtilities.IsNull(reference))
			{
				throw new InvalidOperationException();
			}
			return reference;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x001CCF9E File Offset: 0x001CB39E
		internal static bool IsNull(HandleRef reference)
		{
			return PInvokeUtilities.IsNull(HandleRef.ToIntPtr(reference));
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x001CCFAB File Offset: 0x001CB3AB
		internal static bool IsNull(IntPtr pointer)
		{
			return pointer.Equals(IntPtr.Zero);
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x001CCFC4 File Offset: 0x001CB3C4
		internal static DateTime FromMillisSinceUnixEpoch(long millisSinceEpoch)
		{
			return PInvokeUtilities.UnixEpoch.Add(TimeSpan.FromMilliseconds((double)millisSinceEpoch));
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x001CCFE8 File Offset: 0x001CB3E8
		internal static string OutParamsToString(PInvokeUtilities.OutStringMethod outStringMethod)
		{
			UIntPtr uintPtr = outStringMethod(null, UIntPtr.Zero);
			if (uintPtr.Equals(UIntPtr.Zero))
			{
				return null;
			}
			string text = null;
			try
			{
				byte[] array = new byte[uintPtr.ToUInt32()];
				outStringMethod(array, uintPtr);
				text = Encoding.UTF8.GetString(array, 0, (int)(uintPtr.ToUInt32() - 1U));
			}
			catch (Exception ex)
			{
				Debug.LogError("Exception creating string from char array: " + ex);
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x001CD084 File Offset: 0x001CB484
		internal static T[] OutParamsToArray<T>(PInvokeUtilities.OutMethod<T> outMethod)
		{
			UIntPtr uintPtr = outMethod(null, UIntPtr.Zero);
			if (uintPtr.Equals(UIntPtr.Zero))
			{
				return new T[0];
			}
			T[] array = new T[uintPtr.ToUInt64()];
			outMethod(array, uintPtr);
			return array;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x001CD0DC File Offset: 0x001CB4DC
		internal static IEnumerable<T> ToEnumerable<T>(UIntPtr size, Func<UIntPtr, T> getElement)
		{
			for (ulong i = 0UL; i < size.ToUInt64(); i += 1UL)
			{
				yield return getElement.Invoke(new UIntPtr(i));
			}
			yield break;
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x001CD106 File Offset: 0x001CB506
		internal static IEnumerator<T> ToEnumerator<T>(UIntPtr size, Func<UIntPtr, T> getElement)
		{
			return PInvokeUtilities.ToEnumerable<T>(size, getElement).GetEnumerator();
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x001CD114 File Offset: 0x001CB514
		internal static UIntPtr ArrayToSizeT<T>(T[] array)
		{
			if (array == null)
			{
				return UIntPtr.Zero;
			}
			return new UIntPtr((ulong)((long)array.Length));
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x001CD12C File Offset: 0x001CB52C
		internal static long ToMilliseconds(TimeSpan span)
		{
			double totalMilliseconds = span.TotalMilliseconds;
			if (totalMilliseconds > 9.223372036854776E+18)
			{
				return long.MaxValue;
			}
			if (totalMilliseconds < -9.223372036854776E+18)
			{
				return long.MinValue;
			}
			return Convert.ToInt64(totalMilliseconds);
		}

		// Token: 0x040032EE RID: 13038
		private static readonly DateTime UnixEpoch = DateTime.SpecifyKind(new DateTime(1970, 1, 1), 1);

		// Token: 0x02000707 RID: 1799
		// (Invoke) Token: 0x0600340D RID: 13325
		internal delegate UIntPtr OutStringMethod([In] [Out] byte[] out_bytes, UIntPtr out_size);

		// Token: 0x02000708 RID: 1800
		// (Invoke) Token: 0x06003411 RID: 13329
		internal delegate UIntPtr OutMethod<T>([In] [Out] T[] out_bytes, UIntPtr out_size);
	}
}
