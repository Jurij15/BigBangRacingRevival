using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000482 RID: 1154
	[Serializable]
	public struct ObscuredByte : IEquatable<ObscuredByte>, IFormattable
	{
		// Token: 0x06001FCB RID: 8139 RVA: 0x0018370D File Offset: 0x00181B0D
		private ObscuredByte(byte value)
		{
			this.currentCryptoKey = ObscuredByte.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0;
			this.inited = true;
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x0018372F File Offset: 0x00181B2F
		public static void SetNewCryptoKey(byte newKey)
		{
			ObscuredByte.cryptoKey = newKey;
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x00183737 File Offset: 0x00181B37
		public static byte EncryptDecrypt(byte value)
		{
			return ObscuredByte.EncryptDecrypt(value, 0);
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x00183740 File Offset: 0x00181B40
		public static byte EncryptDecrypt(byte value, byte key)
		{
			if (key == 0)
			{
				return value ^ ObscuredByte.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x00183755 File Offset: 0x00181B55
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredByte.cryptoKey)
			{
				this.hiddenValue = ObscuredByte.EncryptDecrypt(this.InternalDecrypt(), ObscuredByte.cryptoKey);
				this.currentCryptoKey = ObscuredByte.cryptoKey;
			}
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x00183788 File Offset: 0x00181B88
		public void RandomizeCryptoKey()
		{
			byte b = this.InternalDecrypt();
			this.currentCryptoKey = (byte)(Random.seed >> 24);
			this.hiddenValue = ObscuredByte.EncryptDecrypt(b, this.currentCryptoKey);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x001837BD File Offset: 0x00181BBD
		public byte GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x001837CB File Offset: 0x00181BCB
		public void SetEncrypted(byte encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x001837F4 File Offset: 0x00181BF4
		private byte InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredByte.cryptoKey;
				this.hiddenValue = ObscuredByte.EncryptDecrypt(0);
				this.fakeValue = 0;
				this.inited = true;
			}
			byte b = ObscuredByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0 && b != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return b;
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x00183870 File Offset: 0x00181C70
		public static implicit operator ObscuredByte(byte value)
		{
			ObscuredByte obscuredByte = new ObscuredByte(ObscuredByte.EncryptDecrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredByte.fakeValue = value;
			}
			return obscuredByte;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0018389D File Offset: 0x00181C9D
		public static implicit operator byte(ObscuredByte value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x001838A8 File Offset: 0x00181CA8
		public static ObscuredByte operator ++(ObscuredByte input)
		{
			byte b = input.InternalDecrypt() + 1;
			input.hiddenValue = ObscuredByte.EncryptDecrypt(b, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = b;
			}
			return input;
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x001838E8 File Offset: 0x00181CE8
		public static ObscuredByte operator --(ObscuredByte input)
		{
			byte b = input.InternalDecrypt() - 1;
			input.hiddenValue = ObscuredByte.EncryptDecrypt(b, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = b;
			}
			return input;
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00183927 File Offset: 0x00181D27
		public override bool Equals(object obj)
		{
			return obj is ObscuredByte && this.Equals((ObscuredByte)obj);
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x00183944 File Offset: 0x00181D44
		public bool Equals(ObscuredByte obj)
		{
			if (this.currentCryptoKey == obj.currentCryptoKey)
			{
				return this.hiddenValue == obj.hiddenValue;
			}
			return ObscuredByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredByte.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0018399C File Offset: 0x00181D9C
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x001839C0 File Offset: 0x00181DC0
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x001839DC File Offset: 0x00181DDC
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x00183A00 File Offset: 0x00181E00
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x00183A1C File Offset: 0x00181E1C
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04002748 RID: 10056
		private static byte cryptoKey = 244;

		// Token: 0x04002749 RID: 10057
		private byte currentCryptoKey;

		// Token: 0x0400274A RID: 10058
		private byte hiddenValue;

		// Token: 0x0400274B RID: 10059
		private byte fakeValue;

		// Token: 0x0400274C RID: 10060
		private bool inited;
	}
}
