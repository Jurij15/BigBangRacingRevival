using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000491 RID: 1169
	[Serializable]
	public struct ObscuredSByte : IEquatable<ObscuredSByte>, IFormattable
	{
		// Token: 0x060020DB RID: 8411 RVA: 0x00187072 File Offset: 0x00185472
		private ObscuredSByte(sbyte value)
		{
			this.currentCryptoKey = ObscuredSByte.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0;
			this.inited = true;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x00187094 File Offset: 0x00185494
		public static void SetNewCryptoKey(sbyte newKey)
		{
			ObscuredSByte.cryptoKey = newKey;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0018709C File Offset: 0x0018549C
		public static sbyte EncryptDecrypt(sbyte value)
		{
			return ObscuredSByte.EncryptDecrypt(value, 0);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x001870A5 File Offset: 0x001854A5
		public static sbyte EncryptDecrypt(sbyte value, sbyte key)
		{
			if ((int)key == 0)
			{
				return (sbyte)((int)value ^ (int)ObscuredSByte.cryptoKey);
			}
			return (sbyte)((int)value ^ (int)key);
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x001870BF File Offset: 0x001854BF
		public void ApplyNewCryptoKey()
		{
			if ((int)this.currentCryptoKey != (int)ObscuredSByte.cryptoKey)
			{
				this.hiddenValue = ObscuredSByte.EncryptDecrypt(this.InternalDecrypt(), ObscuredSByte.cryptoKey);
				this.currentCryptoKey = ObscuredSByte.cryptoKey;
			}
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x001870F4 File Offset: 0x001854F4
		public void RandomizeCryptoKey()
		{
			sbyte b = this.InternalDecrypt();
			this.currentCryptoKey = (sbyte)(Random.seed >> 24);
			this.hiddenValue = ObscuredSByte.EncryptDecrypt(b, this.currentCryptoKey);
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00187129 File Offset: 0x00185529
		public sbyte GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x00187137 File Offset: 0x00185537
		public void SetEncrypted(sbyte encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00187160 File Offset: 0x00185560
		private sbyte InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredSByte.cryptoKey;
				this.hiddenValue = ObscuredSByte.EncryptDecrypt(0);
				this.fakeValue = 0;
				this.inited = true;
			}
			sbyte b = ObscuredSByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && (int)this.fakeValue != 0 && (int)b != (int)this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return b;
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x001871E0 File Offset: 0x001855E0
		public static implicit operator ObscuredSByte(sbyte value)
		{
			ObscuredSByte obscuredSByte = new ObscuredSByte(ObscuredSByte.EncryptDecrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredSByte.fakeValue = value;
			}
			return obscuredSByte;
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x0018720D File Offset: 0x0018560D
		public static implicit operator sbyte(ObscuredSByte value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x00187218 File Offset: 0x00185618
		public static ObscuredSByte operator ++(ObscuredSByte input)
		{
			sbyte b = (sbyte)((int)input.InternalDecrypt() + 1);
			input.hiddenValue = ObscuredSByte.EncryptDecrypt(b, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = b;
			}
			return input;
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00187258 File Offset: 0x00185658
		public static ObscuredSByte operator --(ObscuredSByte input)
		{
			sbyte b = (sbyte)((int)input.InternalDecrypt() - 1);
			input.hiddenValue = ObscuredSByte.EncryptDecrypt(b, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = b;
			}
			return input;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x00187298 File Offset: 0x00185698
		public override bool Equals(object obj)
		{
			return obj is ObscuredSByte && this.Equals((ObscuredSByte)obj);
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x001872B4 File Offset: 0x001856B4
		public bool Equals(ObscuredSByte obj)
		{
			if ((int)this.currentCryptoKey == (int)obj.currentCryptoKey)
			{
				return (int)this.hiddenValue == (int)obj.hiddenValue;
			}
			return (int)ObscuredSByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int)ObscuredSByte.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00187310 File Offset: 0x00185710
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00187334 File Offset: 0x00185734
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00187350 File Offset: 0x00185750
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00187374 File Offset: 0x00185774
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00187390 File Offset: 0x00185790
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x040027B9 RID: 10169
		private static sbyte cryptoKey = 112;

		// Token: 0x040027BA RID: 10170
		private sbyte currentCryptoKey;

		// Token: 0x040027BB RID: 10171
		private sbyte hiddenValue;

		// Token: 0x040027BC RID: 10172
		private sbyte fakeValue;

		// Token: 0x040027BD RID: 10173
		private bool inited;
	}
}
