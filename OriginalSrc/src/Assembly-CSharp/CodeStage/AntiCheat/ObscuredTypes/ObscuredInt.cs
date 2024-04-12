using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x0200048A RID: 1162
	[Serializable]
	public struct ObscuredInt : IEquatable<ObscuredInt>, IFormattable
	{
		// Token: 0x0600203F RID: 8255 RVA: 0x00184FB1 File Offset: 0x001833B1
		private ObscuredInt(int value)
		{
			this.currentCryptoKey = ObscuredInt.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0;
			this.inited = true;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x00184FD3 File Offset: 0x001833D3
		public static void SetNewCryptoKey(int newKey)
		{
			ObscuredInt.cryptoKey = newKey;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x00184FDB File Offset: 0x001833DB
		public static int Encrypt(int value)
		{
			return ObscuredInt.Encrypt(value, 0);
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00184FE4 File Offset: 0x001833E4
		public static int Encrypt(int value, int key)
		{
			if (key == 0)
			{
				return value ^ ObscuredInt.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x00184FF7 File Offset: 0x001833F7
		public static int Decrypt(int value)
		{
			return ObscuredInt.Decrypt(value, 0);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00185000 File Offset: 0x00183400
		public static int Decrypt(int value, int key)
		{
			if (key == 0)
			{
				return value ^ ObscuredInt.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x00185013 File Offset: 0x00183413
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredInt.cryptoKey)
			{
				this.hiddenValue = ObscuredInt.Encrypt(this.InternalDecrypt(), ObscuredInt.cryptoKey);
				this.currentCryptoKey = ObscuredInt.cryptoKey;
			}
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00185046 File Offset: 0x00183446
		public void RandomizeCryptoKey()
		{
			this.hiddenValue = this.InternalDecrypt();
			this.currentCryptoKey = Random.seed;
			this.hiddenValue = ObscuredInt.Encrypt(this.hiddenValue, this.currentCryptoKey);
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00185076 File Offset: 0x00183476
		public int GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00185084 File Offset: 0x00183484
		public void SetEncrypted(int encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x001850AC File Offset: 0x001834AC
		private int InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredInt.cryptoKey;
				this.hiddenValue = ObscuredInt.Encrypt(0);
				this.fakeValue = 0;
				this.inited = true;
			}
			int num = ObscuredInt.Decrypt(this.hiddenValue, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0 && num != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return num;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00185128 File Offset: 0x00183528
		public static implicit operator ObscuredInt(int value)
		{
			ObscuredInt obscuredInt = new ObscuredInt(ObscuredInt.Encrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredInt.fakeValue = value;
			}
			return obscuredInt;
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00185155 File Offset: 0x00183555
		public static implicit operator int(ObscuredInt value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0018515E File Offset: 0x0018355E
		public static explicit operator ObscuredUInt(ObscuredInt value)
		{
			return (uint)value.InternalDecrypt();
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0018516C File Offset: 0x0018356C
		public static ObscuredInt operator ++(ObscuredInt input)
		{
			int num = input.InternalDecrypt() + 1;
			input.hiddenValue = ObscuredInt.Encrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x001851AC File Offset: 0x001835AC
		public static ObscuredInt operator --(ObscuredInt input)
		{
			int num = input.InternalDecrypt() - 1;
			input.hiddenValue = ObscuredInt.Encrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x001851EA File Offset: 0x001835EA
		public override bool Equals(object obj)
		{
			return obj is ObscuredInt && this.Equals((ObscuredInt)obj);
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00185208 File Offset: 0x00183608
		public bool Equals(ObscuredInt obj)
		{
			if (this.currentCryptoKey == obj.currentCryptoKey)
			{
				return this.hiddenValue == obj.hiddenValue;
			}
			return ObscuredInt.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredInt.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00185260 File Offset: 0x00183660
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00185284 File Offset: 0x00183684
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x001852A8 File Offset: 0x001836A8
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x001852C4 File Offset: 0x001836C4
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x001852E0 File Offset: 0x001836E0
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04002784 RID: 10116
		private static int cryptoKey = 444444;

		// Token: 0x04002785 RID: 10117
		[SerializeField]
		private int currentCryptoKey;

		// Token: 0x04002786 RID: 10118
		[SerializeField]
		private int hiddenValue;

		// Token: 0x04002787 RID: 10119
		[SerializeField]
		private int fakeValue;

		// Token: 0x04002788 RID: 10120
		[SerializeField]
		private bool inited;
	}
}
