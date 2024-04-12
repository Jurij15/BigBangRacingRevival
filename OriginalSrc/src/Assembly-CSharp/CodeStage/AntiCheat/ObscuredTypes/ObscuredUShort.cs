using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000496 RID: 1174
	[Serializable]
	public struct ObscuredUShort : IEquatable<ObscuredUShort>, IFormattable
	{
		// Token: 0x0600214D RID: 8525 RVA: 0x001881E2 File Offset: 0x001865E2
		private ObscuredUShort(ushort value)
		{
			this.currentCryptoKey = ObscuredUShort.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0;
			this.inited = true;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00188204 File Offset: 0x00186604
		public static void SetNewCryptoKey(ushort newKey)
		{
			ObscuredUShort.cryptoKey = newKey;
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0018820C File Offset: 0x0018660C
		public static ushort EncryptDecrypt(ushort value)
		{
			return ObscuredUShort.EncryptDecrypt(value, 0);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00188215 File Offset: 0x00186615
		public static ushort EncryptDecrypt(ushort value, ushort key)
		{
			if (key == 0)
			{
				return value ^ ObscuredUShort.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0018822A File Offset: 0x0018662A
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredUShort.cryptoKey)
			{
				this.hiddenValue = ObscuredUShort.EncryptDecrypt(this.InternalDecrypt(), ObscuredUShort.cryptoKey);
				this.currentCryptoKey = ObscuredUShort.cryptoKey;
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00188260 File Offset: 0x00186660
		public void RandomizeCryptoKey()
		{
			ushort num = this.InternalDecrypt();
			this.currentCryptoKey = (ushort)Random.seed;
			this.hiddenValue = ObscuredUShort.EncryptDecrypt(num, this.currentCryptoKey);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00188292 File Offset: 0x00186692
		public ushort GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x001882A0 File Offset: 0x001866A0
		public void SetEncrypted(ushort encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x001882C8 File Offset: 0x001866C8
		private ushort InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredUShort.cryptoKey;
				this.hiddenValue = ObscuredUShort.EncryptDecrypt(0);
				this.fakeValue = 0;
				this.inited = true;
			}
			ushort num = ObscuredUShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0 && num != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return num;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00188344 File Offset: 0x00186744
		public static implicit operator ObscuredUShort(ushort value)
		{
			ObscuredUShort obscuredUShort = new ObscuredUShort(ObscuredUShort.EncryptDecrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredUShort.fakeValue = value;
			}
			return obscuredUShort;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00188371 File Offset: 0x00186771
		public static implicit operator ushort(ObscuredUShort value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0018837C File Offset: 0x0018677C
		public static ObscuredUShort operator ++(ObscuredUShort input)
		{
			ushort num = input.InternalDecrypt() + 1;
			input.hiddenValue = ObscuredUShort.EncryptDecrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x001883BC File Offset: 0x001867BC
		public static ObscuredUShort operator --(ObscuredUShort input)
		{
			ushort num = input.InternalDecrypt() - 1;
			input.hiddenValue = ObscuredUShort.EncryptDecrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x001883FB File Offset: 0x001867FB
		public override bool Equals(object obj)
		{
			return obj is ObscuredUShort && this.Equals((ObscuredUShort)obj);
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x00188418 File Offset: 0x00186818
		public bool Equals(ObscuredUShort obj)
		{
			if (this.currentCryptoKey == obj.currentCryptoKey)
			{
				return this.hiddenValue == obj.hiddenValue;
			}
			return ObscuredUShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredUShort.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00188470 File Offset: 0x00186870
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00188494 File Offset: 0x00186894
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x001884B0 File Offset: 0x001868B0
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x001884D4 File Offset: 0x001868D4
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x001884F0 File Offset: 0x001868F0
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x040027D2 RID: 10194
		private static ushort cryptoKey = 224;

		// Token: 0x040027D3 RID: 10195
		private ushort currentCryptoKey;

		// Token: 0x040027D4 RID: 10196
		private ushort hiddenValue;

		// Token: 0x040027D5 RID: 10197
		private ushort fakeValue;

		// Token: 0x040027D6 RID: 10198
		private bool inited;
	}
}
