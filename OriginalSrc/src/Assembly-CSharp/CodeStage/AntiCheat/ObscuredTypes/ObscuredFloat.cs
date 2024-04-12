using System;
using System.Runtime.InteropServices;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000488 RID: 1160
	[Serializable]
	public struct ObscuredFloat : IEquatable<ObscuredFloat>, IFormattable
	{
		// Token: 0x06002026 RID: 8230 RVA: 0x00184AC6 File Offset: 0x00182EC6
		private ObscuredFloat(byte[] value)
		{
			this.currentCryptoKey = ObscuredFloat.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0f;
			this.inited = true;
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00184AEC File Offset: 0x00182EEC
		public static void SetNewCryptoKey(int newKey)
		{
			ObscuredFloat.cryptoKey = newKey;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00184AF4 File Offset: 0x00182EF4
		public static int Encrypt(float value)
		{
			return ObscuredFloat.Encrypt(value, ObscuredFloat.cryptoKey);
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00184B04 File Offset: 0x00182F04
		public static int Encrypt(float value, int key)
		{
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.f = value;
			floatIntBytesUnion.i ^= key;
			return floatIntBytesUnion.i;
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00184B38 File Offset: 0x00182F38
		private static byte[] InternalEncrypt(float value)
		{
			return ObscuredFloat.InternalEncrypt(value, 0);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00184B44 File Offset: 0x00182F44
		private static byte[] InternalEncrypt(float value, int key)
		{
			int num = key;
			if (num == 0)
			{
				num = ObscuredFloat.cryptoKey;
			}
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.f = value;
			floatIntBytesUnion.i ^= num;
			return new byte[] { floatIntBytesUnion.b1, floatIntBytesUnion.b2, floatIntBytesUnion.b3, floatIntBytesUnion.b4 };
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00184BAD File Offset: 0x00182FAD
		public static float Decrypt(int value)
		{
			return ObscuredFloat.Decrypt(value, ObscuredFloat.cryptoKey);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x00184BBC File Offset: 0x00182FBC
		public static float Decrypt(int value, int key)
		{
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.i = value ^ key;
			return floatIntBytesUnion.f;
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00184BE2 File Offset: 0x00182FE2
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredFloat.cryptoKey)
			{
				this.hiddenValue = ObscuredFloat.InternalEncrypt(this.InternalDecrypt(), ObscuredFloat.cryptoKey);
				this.currentCryptoKey = ObscuredFloat.cryptoKey;
			}
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00184C18 File Offset: 0x00183018
		public void RandomizeCryptoKey()
		{
			float num = this.InternalDecrypt();
			this.currentCryptoKey = Random.seed;
			this.hiddenValue = ObscuredFloat.InternalEncrypt(num, this.currentCryptoKey);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00184C4C File Offset: 0x0018304C
		public int GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.b1 = this.hiddenValue[0];
			floatIntBytesUnion.b2 = this.hiddenValue[1];
			floatIntBytesUnion.b3 = this.hiddenValue[2];
			floatIntBytesUnion.b4 = this.hiddenValue[3];
			return floatIntBytesUnion.i;
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00184CAC File Offset: 0x001830AC
		public void SetEncrypted(int encrypted)
		{
			this.inited = true;
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.i = encrypted;
			this.hiddenValue = new byte[] { floatIntBytesUnion.b1, floatIntBytesUnion.b2, floatIntBytesUnion.b3, floatIntBytesUnion.b4 };
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00184D1C File Offset: 0x0018311C
		private float InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredFloat.cryptoKey;
				this.hiddenValue = ObscuredFloat.InternalEncrypt(0f);
				this.fakeValue = 0f;
				this.inited = true;
			}
			ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = default(ObscuredFloat.FloatIntBytesUnion);
			floatIntBytesUnion.b1 = this.hiddenValue[0];
			floatIntBytesUnion.b2 = this.hiddenValue[1];
			floatIntBytesUnion.b3 = this.hiddenValue[2];
			floatIntBytesUnion.b4 = this.hiddenValue[3];
			floatIntBytesUnion.i ^= this.currentCryptoKey;
			float f = floatIntBytesUnion.f;
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0f && Math.Abs(f - this.fakeValue) > ObscuredCheatingDetector.Instance.floatEpsilon)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return f;
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00184E04 File Offset: 0x00183204
		public static implicit operator ObscuredFloat(float value)
		{
			ObscuredFloat obscuredFloat = new ObscuredFloat(ObscuredFloat.InternalEncrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredFloat.fakeValue = value;
			}
			return obscuredFloat;
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00184E31 File Offset: 0x00183231
		public static implicit operator float(ObscuredFloat value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00184E3C File Offset: 0x0018323C
		public static ObscuredFloat operator ++(ObscuredFloat input)
		{
			float num = input.InternalDecrypt() + 1f;
			input.hiddenValue = ObscuredFloat.InternalEncrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00184E80 File Offset: 0x00183280
		public static ObscuredFloat operator --(ObscuredFloat input)
		{
			float num = input.InternalDecrypt() - 1f;
			input.hiddenValue = ObscuredFloat.InternalEncrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00184EC2 File Offset: 0x001832C2
		public override bool Equals(object obj)
		{
			return obj is ObscuredFloat && this.Equals((ObscuredFloat)obj);
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00184EE0 File Offset: 0x001832E0
		public bool Equals(ObscuredFloat obj)
		{
			double num = (double)obj.InternalDecrypt();
			double num2 = (double)this.InternalDecrypt();
			return num.Equals(num2);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00184F08 File Offset: 0x00183308
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00184F2C File Offset: 0x0018332C
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00184F50 File Offset: 0x00183350
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00184F6C File Offset: 0x0018336C
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00184F88 File Offset: 0x00183388
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04002779 RID: 10105
		private static int cryptoKey = 230887;

		// Token: 0x0400277A RID: 10106
		[SerializeField]
		private int currentCryptoKey;

		// Token: 0x0400277B RID: 10107
		[SerializeField]
		private byte[] hiddenValue;

		// Token: 0x0400277C RID: 10108
		[SerializeField]
		private float fakeValue;

		// Token: 0x0400277D RID: 10109
		[SerializeField]
		private bool inited;

		// Token: 0x02000489 RID: 1161
		[StructLayout(2)]
		private struct FloatIntBytesUnion
		{
			// Token: 0x0400277E RID: 10110
			[FieldOffset(0)]
			public float f;

			// Token: 0x0400277F RID: 10111
			[FieldOffset(0)]
			public int i;

			// Token: 0x04002780 RID: 10112
			[FieldOffset(0)]
			public byte b1;

			// Token: 0x04002781 RID: 10113
			[FieldOffset(1)]
			public byte b2;

			// Token: 0x04002782 RID: 10114
			[FieldOffset(2)]
			public byte b3;

			// Token: 0x04002783 RID: 10115
			[FieldOffset(3)]
			public byte b4;
		}
	}
}
