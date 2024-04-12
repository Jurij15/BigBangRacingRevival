using System;
using System.Runtime.InteropServices;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000486 RID: 1158
	[Serializable]
	public struct ObscuredDouble : IEquatable<ObscuredDouble>, IFormattable
	{
		// Token: 0x0600200D RID: 8205 RVA: 0x00184502 File Offset: 0x00182902
		private ObscuredDouble(byte[] value)
		{
			this.currentCryptoKey = ObscuredDouble.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0.0;
			this.inited = true;
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0018452C File Offset: 0x0018292C
		public static void SetNewCryptoKey(long newKey)
		{
			ObscuredDouble.cryptoKey = newKey;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00184534 File Offset: 0x00182934
		public static long Encrypt(double value)
		{
			return ObscuredDouble.Encrypt(value, ObscuredDouble.cryptoKey);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00184544 File Offset: 0x00182944
		public static long Encrypt(double value, long key)
		{
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.d = value;
			doubleLongBytesUnion.l ^= key;
			return doubleLongBytesUnion.l;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00184578 File Offset: 0x00182978
		private static byte[] InternalEncrypt(double value)
		{
			return ObscuredDouble.InternalEncrypt(value, 0L);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00184584 File Offset: 0x00182984
		private static byte[] InternalEncrypt(double value, long key)
		{
			long num = key;
			if (num == 0L)
			{
				num = ObscuredDouble.cryptoKey;
			}
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.d = value;
			doubleLongBytesUnion.l ^= num;
			return new byte[] { doubleLongBytesUnion.b1, doubleLongBytesUnion.b2, doubleLongBytesUnion.b3, doubleLongBytesUnion.b4, doubleLongBytesUnion.b5, doubleLongBytesUnion.b6, doubleLongBytesUnion.b7, doubleLongBytesUnion.b8 };
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00184617 File Offset: 0x00182A17
		public static double Decrypt(long value)
		{
			return ObscuredDouble.Decrypt(value, ObscuredDouble.cryptoKey);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00184624 File Offset: 0x00182A24
		public static double Decrypt(long value, long key)
		{
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.l = value ^ key;
			return doubleLongBytesUnion.d;
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0018464A File Offset: 0x00182A4A
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredDouble.cryptoKey)
			{
				this.hiddenValue = ObscuredDouble.InternalEncrypt(this.InternalDecrypt(), ObscuredDouble.cryptoKey);
				this.currentCryptoKey = ObscuredDouble.cryptoKey;
			}
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00184680 File Offset: 0x00182A80
		public void RandomizeCryptoKey()
		{
			double num = this.InternalDecrypt();
			this.currentCryptoKey = (long)Random.seed;
			this.hiddenValue = ObscuredDouble.InternalEncrypt(num, this.currentCryptoKey);
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x001846B4 File Offset: 0x00182AB4
		public long GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.b1 = this.hiddenValue[0];
			doubleLongBytesUnion.b2 = this.hiddenValue[1];
			doubleLongBytesUnion.b3 = this.hiddenValue[2];
			doubleLongBytesUnion.b4 = this.hiddenValue[3];
			doubleLongBytesUnion.b5 = this.hiddenValue[4];
			doubleLongBytesUnion.b6 = this.hiddenValue[5];
			doubleLongBytesUnion.b7 = this.hiddenValue[6];
			doubleLongBytesUnion.b8 = this.hiddenValue[7];
			return doubleLongBytesUnion.l;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00184750 File Offset: 0x00182B50
		public void SetEncrypted(long encrypted)
		{
			this.inited = true;
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.l = encrypted;
			this.hiddenValue = new byte[] { doubleLongBytesUnion.b1, doubleLongBytesUnion.b2, doubleLongBytesUnion.b3, doubleLongBytesUnion.b4, doubleLongBytesUnion.b5, doubleLongBytesUnion.b6, doubleLongBytesUnion.b7, doubleLongBytesUnion.b8 };
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x001847E8 File Offset: 0x00182BE8
		private double InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredDouble.cryptoKey;
				this.hiddenValue = ObscuredDouble.InternalEncrypt(0.0);
				this.fakeValue = 0.0;
				this.inited = true;
			}
			ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = default(ObscuredDouble.DoubleLongBytesUnion);
			doubleLongBytesUnion.b1 = this.hiddenValue[0];
			doubleLongBytesUnion.b2 = this.hiddenValue[1];
			doubleLongBytesUnion.b3 = this.hiddenValue[2];
			doubleLongBytesUnion.b4 = this.hiddenValue[3];
			doubleLongBytesUnion.b5 = this.hiddenValue[4];
			doubleLongBytesUnion.b6 = this.hiddenValue[5];
			doubleLongBytesUnion.b7 = this.hiddenValue[6];
			doubleLongBytesUnion.b8 = this.hiddenValue[7];
			doubleLongBytesUnion.l ^= this.currentCryptoKey;
			double d = doubleLongBytesUnion.d;
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0.0 && Math.Abs(d - this.fakeValue) > 1E-06)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return d;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00184918 File Offset: 0x00182D18
		public static implicit operator ObscuredDouble(double value)
		{
			ObscuredDouble obscuredDouble = new ObscuredDouble(ObscuredDouble.InternalEncrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredDouble.fakeValue = value;
			}
			return obscuredDouble;
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00184945 File Offset: 0x00182D45
		public static implicit operator double(ObscuredDouble value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00184950 File Offset: 0x00182D50
		public static ObscuredDouble operator ++(ObscuredDouble input)
		{
			double num = input.InternalDecrypt() + 1.0;
			input.hiddenValue = ObscuredDouble.InternalEncrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00184998 File Offset: 0x00182D98
		public static ObscuredDouble operator --(ObscuredDouble input)
		{
			double num = input.InternalDecrypt() - 1.0;
			input.hiddenValue = ObscuredDouble.InternalEncrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x001849E0 File Offset: 0x00182DE0
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x00184A04 File Offset: 0x00182E04
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x00184A20 File Offset: 0x00182E20
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x00184A3C File Offset: 0x00182E3C
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x00184A59 File Offset: 0x00182E59
		public override bool Equals(object obj)
		{
			return obj is ObscuredDouble && this.Equals((ObscuredDouble)obj);
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x00184A74 File Offset: 0x00182E74
		public bool Equals(ObscuredDouble obj)
		{
			return obj.InternalDecrypt().Equals(this.InternalDecrypt());
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00184A98 File Offset: 0x00182E98
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x0400276A RID: 10090
		private static long cryptoKey = 210987L;

		// Token: 0x0400276B RID: 10091
		[SerializeField]
		private long currentCryptoKey;

		// Token: 0x0400276C RID: 10092
		[SerializeField]
		private byte[] hiddenValue;

		// Token: 0x0400276D RID: 10093
		[SerializeField]
		private double fakeValue;

		// Token: 0x0400276E RID: 10094
		[SerializeField]
		private bool inited;

		// Token: 0x02000487 RID: 1159
		[StructLayout(2)]
		private struct DoubleLongBytesUnion
		{
			// Token: 0x0400276F RID: 10095
			[FieldOffset(0)]
			public double d;

			// Token: 0x04002770 RID: 10096
			[FieldOffset(0)]
			public long l;

			// Token: 0x04002771 RID: 10097
			[FieldOffset(0)]
			public byte b1;

			// Token: 0x04002772 RID: 10098
			[FieldOffset(1)]
			public byte b2;

			// Token: 0x04002773 RID: 10099
			[FieldOffset(2)]
			public byte b3;

			// Token: 0x04002774 RID: 10100
			[FieldOffset(3)]
			public byte b4;

			// Token: 0x04002775 RID: 10101
			[FieldOffset(4)]
			public byte b5;

			// Token: 0x04002776 RID: 10102
			[FieldOffset(5)]
			public byte b6;

			// Token: 0x04002777 RID: 10103
			[FieldOffset(6)]
			public byte b7;

			// Token: 0x04002778 RID: 10104
			[FieldOffset(7)]
			public byte b8;
		}
	}
}
