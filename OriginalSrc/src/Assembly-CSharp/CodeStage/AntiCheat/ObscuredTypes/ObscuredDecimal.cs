using System;
using System.Runtime.InteropServices;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000484 RID: 1156
	[Serializable]
	public struct ObscuredDecimal : IEquatable<ObscuredDecimal>, IFormattable
	{
		// Token: 0x06001FF3 RID: 8179 RVA: 0x00183D41 File Offset: 0x00182141
		private ObscuredDecimal(byte[] value)
		{
			this.currentCryptoKey = ObscuredDecimal.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0m;
			this.inited = true;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00183D68 File Offset: 0x00182168
		public static void SetNewCryptoKey(long newKey)
		{
			ObscuredDecimal.cryptoKey = newKey;
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x00183D70 File Offset: 0x00182170
		public static decimal Encrypt(decimal value)
		{
			return ObscuredDecimal.Encrypt(value, ObscuredDecimal.cryptoKey);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x00183D80 File Offset: 0x00182180
		public static decimal Encrypt(decimal value, long key)
		{
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.d = value;
			decimalLongBytesUnion.l1 ^= key;
			decimalLongBytesUnion.l2 ^= key;
			return decimalLongBytesUnion.d;
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00183DC4 File Offset: 0x001821C4
		private static byte[] InternalEncrypt(decimal value)
		{
			return ObscuredDecimal.InternalEncrypt(value, 0L);
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00183DD0 File Offset: 0x001821D0
		private static byte[] InternalEncrypt(decimal value, long key)
		{
			long num = key;
			if (num == 0L)
			{
				num = ObscuredDecimal.cryptoKey;
			}
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.d = value;
			decimalLongBytesUnion.l1 ^= num;
			decimalLongBytesUnion.l2 ^= num;
			return new byte[]
			{
				decimalLongBytesUnion.b1, decimalLongBytesUnion.b2, decimalLongBytesUnion.b3, decimalLongBytesUnion.b4, decimalLongBytesUnion.b5, decimalLongBytesUnion.b6, decimalLongBytesUnion.b7, decimalLongBytesUnion.b8, decimalLongBytesUnion.b9, decimalLongBytesUnion.b10,
				decimalLongBytesUnion.b11, decimalLongBytesUnion.b12, decimalLongBytesUnion.b13, decimalLongBytesUnion.b14, decimalLongBytesUnion.b15, decimalLongBytesUnion.b16
			};
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00183ECB File Offset: 0x001822CB
		public static decimal Decrypt(decimal value)
		{
			return ObscuredDecimal.Decrypt(value, ObscuredDecimal.cryptoKey);
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00183ED8 File Offset: 0x001822D8
		public static decimal Decrypt(decimal value, long key)
		{
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.d = value;
			decimalLongBytesUnion.l1 ^= key;
			decimalLongBytesUnion.l2 ^= key;
			return decimalLongBytesUnion.d;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00183F1C File Offset: 0x0018231C
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredDecimal.cryptoKey)
			{
				this.hiddenValue = ObscuredDecimal.InternalEncrypt(this.InternalDecrypt(), ObscuredDecimal.cryptoKey);
				this.currentCryptoKey = ObscuredDecimal.cryptoKey;
			}
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x00183F50 File Offset: 0x00182350
		public void RandomizeCryptoKey()
		{
			decimal num = this.InternalDecrypt();
			this.currentCryptoKey = (long)Random.seed;
			this.hiddenValue = ObscuredDecimal.InternalEncrypt(num, this.currentCryptoKey);
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x00183F84 File Offset: 0x00182384
		public decimal GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.b1 = this.hiddenValue[0];
			decimalLongBytesUnion.b2 = this.hiddenValue[1];
			decimalLongBytesUnion.b3 = this.hiddenValue[2];
			decimalLongBytesUnion.b4 = this.hiddenValue[3];
			decimalLongBytesUnion.b5 = this.hiddenValue[4];
			decimalLongBytesUnion.b6 = this.hiddenValue[5];
			decimalLongBytesUnion.b7 = this.hiddenValue[6];
			decimalLongBytesUnion.b8 = this.hiddenValue[7];
			decimalLongBytesUnion.b9 = this.hiddenValue[8];
			decimalLongBytesUnion.b10 = this.hiddenValue[9];
			decimalLongBytesUnion.b11 = this.hiddenValue[10];
			decimalLongBytesUnion.b12 = this.hiddenValue[11];
			decimalLongBytesUnion.b13 = this.hiddenValue[12];
			decimalLongBytesUnion.b14 = this.hiddenValue[13];
			decimalLongBytesUnion.b15 = this.hiddenValue[14];
			decimalLongBytesUnion.b16 = this.hiddenValue[15];
			return decimalLongBytesUnion.d;
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x001840A0 File Offset: 0x001824A0
		public void SetEncrypted(decimal encrypted)
		{
			this.inited = true;
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.d = encrypted;
			this.hiddenValue = new byte[]
			{
				decimalLongBytesUnion.b1, decimalLongBytesUnion.b2, decimalLongBytesUnion.b3, decimalLongBytesUnion.b4, decimalLongBytesUnion.b5, decimalLongBytesUnion.b6, decimalLongBytesUnion.b7, decimalLongBytesUnion.b8, decimalLongBytesUnion.b9, decimalLongBytesUnion.b10,
				decimalLongBytesUnion.b11, decimalLongBytesUnion.b12, decimalLongBytesUnion.b13, decimalLongBytesUnion.b14, decimalLongBytesUnion.b15, decimalLongBytesUnion.b16
			};
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x00184190 File Offset: 0x00182590
		private decimal InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredDecimal.cryptoKey;
				this.hiddenValue = ObscuredDecimal.InternalEncrypt(0m);
				this.fakeValue = 0m;
				this.inited = true;
			}
			ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = default(ObscuredDecimal.DecimalLongBytesUnion);
			decimalLongBytesUnion.b1 = this.hiddenValue[0];
			decimalLongBytesUnion.b2 = this.hiddenValue[1];
			decimalLongBytesUnion.b3 = this.hiddenValue[2];
			decimalLongBytesUnion.b4 = this.hiddenValue[3];
			decimalLongBytesUnion.b5 = this.hiddenValue[4];
			decimalLongBytesUnion.b6 = this.hiddenValue[5];
			decimalLongBytesUnion.b7 = this.hiddenValue[6];
			decimalLongBytesUnion.b8 = this.hiddenValue[7];
			decimalLongBytesUnion.b9 = this.hiddenValue[8];
			decimalLongBytesUnion.b10 = this.hiddenValue[9];
			decimalLongBytesUnion.b11 = this.hiddenValue[10];
			decimalLongBytesUnion.b12 = this.hiddenValue[11];
			decimalLongBytesUnion.b13 = this.hiddenValue[12];
			decimalLongBytesUnion.b14 = this.hiddenValue[13];
			decimalLongBytesUnion.b15 = this.hiddenValue[14];
			decimalLongBytesUnion.b16 = this.hiddenValue[15];
			decimalLongBytesUnion.l1 ^= this.currentCryptoKey;
			decimalLongBytesUnion.l2 ^= this.currentCryptoKey;
			decimal d = decimalLongBytesUnion.d;
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0m && d != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return d;
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00184344 File Offset: 0x00182744
		public static implicit operator ObscuredDecimal(decimal value)
		{
			ObscuredDecimal obscuredDecimal = new ObscuredDecimal(ObscuredDecimal.InternalEncrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredDecimal.fakeValue = value;
			}
			return obscuredDecimal;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00184371 File Offset: 0x00182771
		public static implicit operator decimal(ObscuredDecimal value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x0018437A File Offset: 0x0018277A
		public static explicit operator ObscuredDecimal(ObscuredFloat f)
		{
			return (decimal)f;
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x0018438C File Offset: 0x0018278C
		public static ObscuredDecimal operator ++(ObscuredDecimal input)
		{
			decimal num = input.InternalDecrypt() + 1m;
			input.hiddenValue = ObscuredDecimal.InternalEncrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x001843D4 File Offset: 0x001827D4
		public static ObscuredDecimal operator --(ObscuredDecimal input)
		{
			decimal num = input.InternalDecrypt() - 1m;
			input.hiddenValue = ObscuredDecimal.InternalEncrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x0018441C File Offset: 0x0018281C
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00184440 File Offset: 0x00182840
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0018445C File Offset: 0x0018285C
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00184478 File Offset: 0x00182878
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00184495 File Offset: 0x00182895
		public override bool Equals(object obj)
		{
			return obj is ObscuredDecimal && this.Equals((ObscuredDecimal)obj);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x001844B0 File Offset: 0x001828B0
		public bool Equals(ObscuredDecimal obj)
		{
			return obj.InternalDecrypt().Equals(this.InternalDecrypt());
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x001844D4 File Offset: 0x001828D4
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x04002752 RID: 10066
		private static long cryptoKey = 209208L;

		// Token: 0x04002753 RID: 10067
		private long currentCryptoKey;

		// Token: 0x04002754 RID: 10068
		private byte[] hiddenValue;

		// Token: 0x04002755 RID: 10069
		private decimal fakeValue;

		// Token: 0x04002756 RID: 10070
		private bool inited;

		// Token: 0x02000485 RID: 1157
		[StructLayout(2)]
		private struct DecimalLongBytesUnion
		{
			// Token: 0x04002757 RID: 10071
			[FieldOffset(0)]
			public decimal d;

			// Token: 0x04002758 RID: 10072
			[FieldOffset(0)]
			public long l1;

			// Token: 0x04002759 RID: 10073
			[FieldOffset(8)]
			public long l2;

			// Token: 0x0400275A RID: 10074
			[FieldOffset(0)]
			public byte b1;

			// Token: 0x0400275B RID: 10075
			[FieldOffset(1)]
			public byte b2;

			// Token: 0x0400275C RID: 10076
			[FieldOffset(2)]
			public byte b3;

			// Token: 0x0400275D RID: 10077
			[FieldOffset(3)]
			public byte b4;

			// Token: 0x0400275E RID: 10078
			[FieldOffset(4)]
			public byte b5;

			// Token: 0x0400275F RID: 10079
			[FieldOffset(5)]
			public byte b6;

			// Token: 0x04002760 RID: 10080
			[FieldOffset(6)]
			public byte b7;

			// Token: 0x04002761 RID: 10081
			[FieldOffset(7)]
			public byte b8;

			// Token: 0x04002762 RID: 10082
			[FieldOffset(8)]
			public byte b9;

			// Token: 0x04002763 RID: 10083
			[FieldOffset(9)]
			public byte b10;

			// Token: 0x04002764 RID: 10084
			[FieldOffset(10)]
			public byte b11;

			// Token: 0x04002765 RID: 10085
			[FieldOffset(11)]
			public byte b12;

			// Token: 0x04002766 RID: 10086
			[FieldOffset(12)]
			public byte b13;

			// Token: 0x04002767 RID: 10087
			[FieldOffset(13)]
			public byte b14;

			// Token: 0x04002768 RID: 10088
			[FieldOffset(14)]
			public byte b15;

			// Token: 0x04002769 RID: 10089
			[FieldOffset(15)]
			public byte b16;
		}
	}
}
