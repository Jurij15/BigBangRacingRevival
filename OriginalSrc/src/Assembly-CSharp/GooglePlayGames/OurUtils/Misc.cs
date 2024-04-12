using System;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x02000607 RID: 1543
	public static class Misc
	{
		// Token: 0x06002D3C RID: 11580 RVA: 0x001BFFC4 File Offset: 0x001BE3C4
		public static bool BuffersAreIdentical(byte[] a, byte[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x001C0018 File Offset: 0x001BE418
		public static byte[] GetSubsetBytes(byte[] array, int offset, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0 || offset >= array.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length < 0 || array.Length - offset < length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (offset == 0 && length == array.Length)
			{
				return array;
			}
			byte[] array2 = new byte[length];
			Array.Copy(array, offset, array2, 0, length);
			return array2;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x001C0091 File Offset: 0x001BE491
		public static T CheckNotNull<T>(T value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			return value;
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x001C00A5 File Offset: 0x001BE4A5
		public static T CheckNotNull<T>(T value, string paramName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(paramName);
			}
			return value;
		}
	}
}
