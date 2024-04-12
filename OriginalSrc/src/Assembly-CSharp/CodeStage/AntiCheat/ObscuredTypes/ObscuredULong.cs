using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000495 RID: 1173
	[Serializable]
	public struct ObscuredULong : IEquatable<ObscuredULong>, IFormattable
	{
		// Token: 0x06002136 RID: 8502 RVA: 0x00187E85 File Offset: 0x00186285
		private ObscuredULong(ulong value)
		{
			this.currentCryptoKey = ObscuredULong.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0UL;
			this.inited = true;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00187EA8 File Offset: 0x001862A8
		public static void SetNewCryptoKey(ulong newKey)
		{
			ObscuredULong.cryptoKey = newKey;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00187EB0 File Offset: 0x001862B0
		public static ulong Encrypt(ulong value)
		{
			return ObscuredULong.Encrypt(value, 0UL);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00187EBA File Offset: 0x001862BA
		public static ulong Decrypt(ulong value)
		{
			return ObscuredULong.Decrypt(value, 0UL);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x00187EC4 File Offset: 0x001862C4
		public static ulong Encrypt(ulong value, ulong key)
		{
			if (key == 0UL)
			{
				return value ^ ObscuredULong.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00187ED9 File Offset: 0x001862D9
		public static ulong Decrypt(ulong value, ulong key)
		{
			if (key == 0UL)
			{
				return value ^ ObscuredULong.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00187EEE File Offset: 0x001862EE
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredULong.cryptoKey)
			{
				this.hiddenValue = ObscuredULong.Encrypt(this.InternalDecrypt(), ObscuredULong.cryptoKey);
				this.currentCryptoKey = ObscuredULong.cryptoKey;
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x00187F24 File Offset: 0x00186324
		public void RandomizeCryptoKey()
		{
			ulong num = this.InternalDecrypt();
			this.currentCryptoKey = (ulong)((long)Random.seed);
			this.hiddenValue = ObscuredULong.Encrypt(num, this.currentCryptoKey);
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00187F56 File Offset: 0x00186356
		public ulong GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00187F64 File Offset: 0x00186364
		public void SetEncrypted(ulong encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00187F8C File Offset: 0x0018638C
		private ulong InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredULong.cryptoKey;
				this.hiddenValue = ObscuredULong.Encrypt(0UL);
				this.fakeValue = 0UL;
				this.inited = true;
			}
			ulong num = ObscuredULong.Decrypt(this.hiddenValue, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0UL && num != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return num;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0018800C File Offset: 0x0018640C
		public static implicit operator ObscuredULong(ulong value)
		{
			ObscuredULong obscuredULong = new ObscuredULong(ObscuredULong.Encrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredULong.fakeValue = value;
			}
			return obscuredULong;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x00188039 File Offset: 0x00186439
		public static implicit operator ulong(ObscuredULong value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00188044 File Offset: 0x00186444
		public static ObscuredULong operator ++(ObscuredULong input)
		{
			ulong num = input.InternalDecrypt() + 1UL;
			input.hiddenValue = ObscuredULong.Encrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00188084 File Offset: 0x00186484
		public static ObscuredULong operator --(ObscuredULong input)
		{
			ulong num = input.InternalDecrypt() - 1UL;
			input.hiddenValue = ObscuredULong.Encrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x001880C3 File Offset: 0x001864C3
		public override bool Equals(object obj)
		{
			return obj is ObscuredULong && this.Equals((ObscuredULong)obj);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x001880E0 File Offset: 0x001864E0
		public bool Equals(ObscuredULong obj)
		{
			if (this.currentCryptoKey == obj.currentCryptoKey)
			{
				return this.hiddenValue == obj.hiddenValue;
			}
			return ObscuredULong.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredULong.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x00188138 File Offset: 0x00186538
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0018815C File Offset: 0x0018655C
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00188180 File Offset: 0x00186580
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0018819C File Offset: 0x0018659C
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x001881B8 File Offset: 0x001865B8
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x040027CD RID: 10189
		private static ulong cryptoKey = 444443UL;

		// Token: 0x040027CE RID: 10190
		private ulong currentCryptoKey;

		// Token: 0x040027CF RID: 10191
		private ulong hiddenValue;

		// Token: 0x040027D0 RID: 10192
		private ulong fakeValue;

		// Token: 0x040027D1 RID: 10193
		private bool inited;
	}
}
