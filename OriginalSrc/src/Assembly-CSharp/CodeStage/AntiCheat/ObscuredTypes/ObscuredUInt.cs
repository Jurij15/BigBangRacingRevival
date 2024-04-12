using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000494 RID: 1172
	[Serializable]
	public struct ObscuredUInt : IEquatable<ObscuredUInt>, IFormattable
	{
		// Token: 0x0600211E RID: 8478 RVA: 0x00187B2B File Offset: 0x00185F2B
		private ObscuredUInt(uint value)
		{
			this.currentCryptoKey = ObscuredUInt.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0U;
			this.inited = true;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00187B4D File Offset: 0x00185F4D
		public static void SetNewCryptoKey(uint newKey)
		{
			ObscuredUInt.cryptoKey = newKey;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00187B55 File Offset: 0x00185F55
		public static uint Encrypt(uint value)
		{
			return ObscuredUInt.Encrypt(value, 0U);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00187B5E File Offset: 0x00185F5E
		public static uint Decrypt(uint value)
		{
			return ObscuredUInt.Decrypt(value, 0U);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00187B67 File Offset: 0x00185F67
		public static uint Encrypt(uint value, uint key)
		{
			if (key == 0U)
			{
				return value ^ ObscuredUInt.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x00187B7A File Offset: 0x00185F7A
		public static uint Decrypt(uint value, uint key)
		{
			if (key == 0U)
			{
				return value ^ ObscuredUInt.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00187B8D File Offset: 0x00185F8D
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredUInt.cryptoKey)
			{
				this.hiddenValue = ObscuredUInt.Encrypt(this.InternalDecrypt(), ObscuredUInt.cryptoKey);
				this.currentCryptoKey = ObscuredUInt.cryptoKey;
			}
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00187BC0 File Offset: 0x00185FC0
		public void RandomizeCryptoKey()
		{
			uint num = this.InternalDecrypt();
			this.currentCryptoKey = (uint)Random.seed;
			this.hiddenValue = ObscuredUInt.Encrypt(num, this.currentCryptoKey);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00187BF1 File Offset: 0x00185FF1
		public uint GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x00187BFF File Offset: 0x00185FFF
		public void SetEncrypted(uint encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00187C28 File Offset: 0x00186028
		private uint InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredUInt.cryptoKey;
				this.hiddenValue = ObscuredUInt.Encrypt(0U);
				this.fakeValue = 0U;
				this.inited = true;
			}
			uint num = ObscuredUInt.Decrypt(this.hiddenValue, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0U && num != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return num;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00187CA4 File Offset: 0x001860A4
		public static implicit operator ObscuredUInt(uint value)
		{
			ObscuredUInt obscuredUInt = new ObscuredUInt(ObscuredUInt.Encrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredUInt.fakeValue = value;
			}
			return obscuredUInt;
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00187CD1 File Offset: 0x001860D1
		public static implicit operator uint(ObscuredUInt value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00187CDA File Offset: 0x001860DA
		public static explicit operator ObscuredInt(ObscuredUInt value)
		{
			return (int)value.InternalDecrypt();
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00187CE8 File Offset: 0x001860E8
		public static ObscuredUInt operator ++(ObscuredUInt input)
		{
			uint num = input.InternalDecrypt() + 1U;
			input.hiddenValue = ObscuredUInt.Encrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00187D28 File Offset: 0x00186128
		public static ObscuredUInt operator --(ObscuredUInt input)
		{
			uint num = input.InternalDecrypt() - 1U;
			input.hiddenValue = ObscuredUInt.Encrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x00187D66 File Offset: 0x00186166
		public override bool Equals(object obj)
		{
			return obj is ObscuredUInt && this.Equals((ObscuredUInt)obj);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x00187D84 File Offset: 0x00186184
		public bool Equals(ObscuredUInt obj)
		{
			if (this.currentCryptoKey == obj.currentCryptoKey)
			{
				return this.hiddenValue == obj.hiddenValue;
			}
			return ObscuredUInt.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredUInt.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x00187DDC File Offset: 0x001861DC
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x00187E00 File Offset: 0x00186200
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x00187E1C File Offset: 0x0018621C
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00187E40 File Offset: 0x00186240
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x00187E5C File Offset: 0x0018625C
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x040027C8 RID: 10184
		private static uint cryptoKey = 240513U;

		// Token: 0x040027C9 RID: 10185
		private uint currentCryptoKey;

		// Token: 0x040027CA RID: 10186
		private uint hiddenValue;

		// Token: 0x040027CB RID: 10187
		private uint fakeValue;

		// Token: 0x040027CC RID: 10188
		private bool inited;
	}
}
