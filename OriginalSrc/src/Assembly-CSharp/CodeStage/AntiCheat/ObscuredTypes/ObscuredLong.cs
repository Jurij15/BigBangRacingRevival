using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x0200048B RID: 1163
	[Serializable]
	public struct ObscuredLong : IEquatable<ObscuredLong>, IFormattable
	{
		// Token: 0x06002057 RID: 8279 RVA: 0x00185309 File Offset: 0x00183709
		private ObscuredLong(long value)
		{
			this.currentCryptoKey = ObscuredLong.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = 0L;
			this.inited = true;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0018532C File Offset: 0x0018372C
		public static void SetNewCryptoKey(long newKey)
		{
			ObscuredLong.cryptoKey = newKey;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x00185334 File Offset: 0x00183734
		public static long Encrypt(long value)
		{
			return ObscuredLong.Encrypt(value, 0L);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x0018533E File Offset: 0x0018373E
		public static long Decrypt(long value)
		{
			return ObscuredLong.Decrypt(value, 0L);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00185348 File Offset: 0x00183748
		public static long Encrypt(long value, long key)
		{
			if (key == 0L)
			{
				return value ^ ObscuredLong.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0018535D File Offset: 0x0018375D
		public static long Decrypt(long value, long key)
		{
			if (key == 0L)
			{
				return value ^ ObscuredLong.cryptoKey;
			}
			return value ^ key;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x00185372 File Offset: 0x00183772
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredLong.cryptoKey)
			{
				this.hiddenValue = ObscuredLong.Encrypt(this.InternalDecrypt(), ObscuredLong.cryptoKey);
				this.currentCryptoKey = ObscuredLong.cryptoKey;
			}
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x001853A8 File Offset: 0x001837A8
		public void RandomizeCryptoKey()
		{
			long num = this.InternalDecrypt();
			this.currentCryptoKey = (long)Random.seed;
			this.hiddenValue = ObscuredLong.Encrypt(num, this.currentCryptoKey);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x001853DA File Offset: 0x001837DA
		public long GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x001853E8 File Offset: 0x001837E8
		public void SetEncrypted(long encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00185410 File Offset: 0x00183810
		private long InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredLong.cryptoKey;
				this.hiddenValue = ObscuredLong.Encrypt(0L);
				this.fakeValue = 0L;
				this.inited = true;
			}
			long num = ObscuredLong.Decrypt(this.hiddenValue, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0L && num != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return num;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00185490 File Offset: 0x00183890
		public static implicit operator ObscuredLong(long value)
		{
			ObscuredLong obscuredLong = new ObscuredLong(ObscuredLong.Encrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredLong.fakeValue = value;
			}
			return obscuredLong;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x001854BD File Offset: 0x001838BD
		public static implicit operator long(ObscuredLong value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x001854C8 File Offset: 0x001838C8
		public static ObscuredLong operator ++(ObscuredLong input)
		{
			long num = input.InternalDecrypt() + 1L;
			input.hiddenValue = ObscuredLong.Encrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00185508 File Offset: 0x00183908
		public static ObscuredLong operator --(ObscuredLong input)
		{
			long num = input.InternalDecrypt() - 1L;
			input.hiddenValue = ObscuredLong.Encrypt(num, input.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning)
			{
				input.fakeValue = num;
			}
			return input;
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00185547 File Offset: 0x00183947
		public override bool Equals(object obj)
		{
			return obj is ObscuredLong && this.Equals((ObscuredLong)obj);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00185564 File Offset: 0x00183964
		public bool Equals(ObscuredLong obj)
		{
			if (this.currentCryptoKey == obj.currentCryptoKey)
			{
				return this.hiddenValue == obj.hiddenValue;
			}
			return ObscuredLong.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredLong.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x001855BC File Offset: 0x001839BC
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x001855E0 File Offset: 0x001839E0
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00185604 File Offset: 0x00183A04
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00185620 File Offset: 0x00183A20
		public string ToString(IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(provider);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0018563C File Offset: 0x00183A3C
		public string ToString(string format, IFormatProvider provider)
		{
			return this.InternalDecrypt().ToString(format, provider);
		}

		// Token: 0x04002789 RID: 10121
		private static long cryptoKey = 444442L;

		// Token: 0x0400278A RID: 10122
		[SerializeField]
		private long currentCryptoKey;

		// Token: 0x0400278B RID: 10123
		[SerializeField]
		private long hiddenValue;

		// Token: 0x0400278C RID: 10124
		[SerializeField]
		private long fakeValue;

		// Token: 0x0400278D RID: 10125
		[SerializeField]
		private bool inited;
	}
}
