using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000492 RID: 1170
	[Serializable]
	public struct ObscuredShort : IEquatable<ObscuredShort>, IFormattable
	{
		// Token: 0x060020F0 RID: 8432 RVA: 0x001873B6 File Offset: 0x001857B6
		private ObscuredShort(short value)
		{
			this.currentCryptoKey = ObscuredShort.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0;
			this.inited = true;
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x001873D8 File Offset: 0x001857D8
		public static void SetNewCryptoKey(short newKey)
		{
			ObscuredShort.cryptoKey = newKey;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x001873E0 File Offset: 0x001857E0
		public static short EncryptDecrypt(short value)
		{
			return ObscuredShort.EncryptDecrypt(value, 0);
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x001873E9 File Offset: 0x001857E9
		public static short EncryptDecrypt(short value, short key)
		{
			if (key == 0)
			{
				return value ^ ObscuredShort.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x001873FE File Offset: 0x001857FE
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredShort.cryptoKey)
			{
				this.hiddenValue = ObscuredShort.EncryptDecrypt(this.InternalDecrypt(), ObscuredShort.cryptoKey);
				this.currentCryptoKey = ObscuredShort.cryptoKey;
			}
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x00187434 File Offset: 0x00185834
		public void RandomizeCryptoKey()
		{
			short num = this.InternalDecrypt();
			this.currentCryptoKey = (short)Random.seed;
			this.hiddenValue = ObscuredShort.EncryptDecrypt(num, this.currentCryptoKey);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x00187466 File Offset: 0x00185866
		public short GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x00187474 File Offset: 0x00185874
		public void SetEncrypted(short encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0018749C File Offset: 0x0018589C
		private short InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredShort.cryptoKey;
				this.hiddenValue = ObscuredShort.EncryptDecrypt(0);
				this.fakeValue = 0;
				this.inited = true;
			}
			short num = ObscuredShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0 && num != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return num;
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x00187518 File Offset: 0x00185918
		public static implicit operator ObscuredShort(short value)
		{
			ObscuredShort obscuredShort = new ObscuredShort(ObscuredShort.EncryptDecrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredShort.fakeValue = value;
			}
			return obscuredShort;
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00187545 File Offset: 0x00185945
		public static implicit operator short(ObscuredShort value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00187550 File Offset: 0x00185950
		public static ObscuredShort operator ++(ObscuredShort input)
		{
			short num = input.InternalDecrypt() + 1;
			input.hiddenValue = ObscuredShort.EncryptDecrypt(num);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00187588 File Offset: 0x00185988
		public static ObscuredShort operator --(ObscuredShort input)
		{
			short num = input.InternalDecrypt() - 1;
			input.hiddenValue = ObscuredShort.EncryptDecrypt(num);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x001875C0 File Offset: 0x001859C0
		public override bool Equals(object obj)
		{
			return obj is ObscuredShort && this.Equals((ObscuredShort)obj);
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x001875DC File Offset: 0x001859DC
		public bool Equals(ObscuredShort obj)
		{
			if (this.currentCryptoKey == obj.currentCryptoKey)
			{
				return this.hiddenValue == obj.hiddenValue;
			}
			return ObscuredShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredShort.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00187634 File Offset: 0x00185A34
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00187658 File Offset: 0x00185A58
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00187674 File Offset: 0x00185A74
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00187698 File Offset: 0x00185A98
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x001876B4 File Offset: 0x00185AB4
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x040027BE RID: 10174
		private static short cryptoKey = 214;

		// Token: 0x040027BF RID: 10175
		private short currentCryptoKey;

		// Token: 0x040027C0 RID: 10176
		private short hiddenValue;

		// Token: 0x040027C1 RID: 10177
		private short fakeValue;

		// Token: 0x040027C2 RID: 10178
		private bool inited;
	}
}
