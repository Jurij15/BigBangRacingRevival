using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000483 RID: 1155
	[Serializable]
	public struct ObscuredChar : IEquatable<ObscuredChar>
	{
		// Token: 0x06001FE0 RID: 8160 RVA: 0x00183A45 File Offset: 0x00181E45
		private ObscuredChar(char value)
		{
			this.currentCryptoKey = ObscuredChar.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = '\0';
			this.inited = true;
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x00183A67 File Offset: 0x00181E67
		public static void SetNewCryptoKey(char newKey)
		{
			ObscuredChar.cryptoKey = newKey;
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x00183A6F File Offset: 0x00181E6F
		public static char EncryptDecrypt(char value)
		{
			return ObscuredChar.EncryptDecrypt(value, '\0');
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x00183A78 File Offset: 0x00181E78
		public static char EncryptDecrypt(char value, char key)
		{
			if (key == '\0')
			{
				return value ^ ObscuredChar.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x00183A8D File Offset: 0x00181E8D
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredChar.cryptoKey)
			{
				this.hiddenValue = ObscuredChar.EncryptDecrypt(this.InternalDecrypt(), ObscuredChar.cryptoKey);
				this.currentCryptoKey = ObscuredChar.cryptoKey;
			}
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x00183AC0 File Offset: 0x00181EC0
		public void RandomizeCryptoKey()
		{
			char c = this.InternalDecrypt();
			this.currentCryptoKey = (char)(Random.seed >> 24);
			this.hiddenValue = ObscuredChar.EncryptDecrypt(c, this.currentCryptoKey);
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x00183AF5 File Offset: 0x00181EF5
		public char GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x00183B03 File Offset: 0x00181F03
		public void SetEncrypted(char encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x00183B2C File Offset: 0x00181F2C
		private char InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredChar.cryptoKey;
				this.hiddenValue = ObscuredChar.EncryptDecrypt('\0');
				this.fakeValue = '\0';
				this.inited = true;
			}
			char c = ObscuredChar.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != '\0' && c != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return c;
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x00183BA8 File Offset: 0x00181FA8
		public static implicit operator ObscuredChar(char value)
		{
			ObscuredChar obscuredChar = new ObscuredChar(ObscuredChar.EncryptDecrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredChar.fakeValue = value;
			}
			return obscuredChar;
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x00183BD5 File Offset: 0x00181FD5
		public static implicit operator char(ObscuredChar value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x00183BE0 File Offset: 0x00181FE0
		public static ObscuredChar operator ++(ObscuredChar input)
		{
			char c = input.InternalDecrypt() + '\u0001';
			input.hiddenValue = ObscuredChar.EncryptDecrypt(c, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = c;
			}
			return input;
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x00183C20 File Offset: 0x00182020
		public static ObscuredChar operator --(ObscuredChar input)
		{
			char c = input.InternalDecrypt() - '\u0001';
			input.hiddenValue = ObscuredChar.EncryptDecrypt(c, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = c;
			}
			return input;
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x00183C5F File Offset: 0x0018205F
		public override bool Equals(object obj)
		{
			return obj is ObscuredChar && this.Equals((ObscuredChar)obj);
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x00183C7C File Offset: 0x0018207C
		public bool Equals(ObscuredChar obj)
		{
			if (this.currentCryptoKey == obj.currentCryptoKey)
			{
				return this.hiddenValue == obj.hiddenValue;
			}
			return ObscuredChar.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredChar.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x00183CD4 File Offset: 0x001820D4
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00183CF8 File Offset: 0x001820F8
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00183D14 File Offset: 0x00182114
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x0400274D RID: 10061
		private static char cryptoKey = '—';

		// Token: 0x0400274E RID: 10062
		private char currentCryptoKey;

		// Token: 0x0400274F RID: 10063
		private char hiddenValue;

		// Token: 0x04002750 RID: 10064
		private char fakeValue;

		// Token: 0x04002751 RID: 10065
		private bool inited;
	}
}
