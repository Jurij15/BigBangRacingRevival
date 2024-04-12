using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000493 RID: 1171
	[Serializable]
	public sealed class ObscuredString
	{
		// Token: 0x06002105 RID: 8453 RVA: 0x001876DD File Offset: 0x00185ADD
		private ObscuredString()
		{
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x001876E5 File Offset: 0x00185AE5
		private ObscuredString(byte[] value)
		{
			this.currentCryptoKey = ObscuredString.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = null;
			this.inited = true;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x0018770D File Offset: 0x00185B0D
		public static void SetNewCryptoKey(string newKey)
		{
			ObscuredString.cryptoKey = newKey;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00187715 File Offset: 0x00185B15
		public static string EncryptDecrypt(string value)
		{
			return ObscuredString.EncryptDecrypt(value, string.Empty);
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x00187724 File Offset: 0x00185B24
		public static string EncryptDecrypt(string value, string key)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(key))
			{
				key = ObscuredString.cryptoKey;
			}
			int length = key.Length;
			int length2 = value.Length;
			char[] array = new char[length2];
			for (int i = 0; i < length2; i++)
			{
				array[i] = value.get_Chars(i) ^ key.get_Chars(i % length);
			}
			return new string(array);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00187796 File Offset: 0x00185B96
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredString.cryptoKey)
			{
				this.hiddenValue = ObscuredString.InternalEncrypt(this.InternalDecrypt());
				this.currentCryptoKey = ObscuredString.cryptoKey;
			}
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x001877CC File Offset: 0x00185BCC
		public void RandomizeCryptoKey()
		{
			string text = this.InternalDecrypt();
			this.currentCryptoKey = Random.seed.ToString();
			this.hiddenValue = ObscuredString.InternalEncrypt(text, this.currentCryptoKey);
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0018780B File Offset: 0x00185C0B
		public string GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return ObscuredString.GetString(this.hiddenValue);
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x0018781E File Offset: 0x00185C1E
		public void SetEncrypted(string encrypted)
		{
			this.inited = true;
			this.hiddenValue = ObscuredString.GetBytes(encrypted);
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x00187849 File Offset: 0x00185C49
		private static byte[] InternalEncrypt(string value)
		{
			return ObscuredString.InternalEncrypt(value, ObscuredString.cryptoKey);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00187856 File Offset: 0x00185C56
		private static byte[] InternalEncrypt(string value, string key)
		{
			return ObscuredString.GetBytes(ObscuredString.EncryptDecrypt(value, key));
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x00187864 File Offset: 0x00185C64
		private string InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredString.cryptoKey;
				this.hiddenValue = ObscuredString.InternalEncrypt(string.Empty);
				this.fakeValue = string.Empty;
				this.inited = true;
			}
			string text = this.currentCryptoKey;
			if (string.IsNullOrEmpty(text))
			{
				text = ObscuredString.cryptoKey;
			}
			string text2 = ObscuredString.EncryptDecrypt(ObscuredString.GetString(this.hiddenValue), text);
			if (ObscuredCheatingDetector.IsRunning && !string.IsNullOrEmpty(this.fakeValue) && text2 != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return text2;
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0018790C File Offset: 0x00185D0C
		public static implicit operator ObscuredString(string value)
		{
			if (value == null)
			{
				return null;
			}
			ObscuredString obscuredString = new ObscuredString(ObscuredString.InternalEncrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredString.fakeValue = value;
			}
			return obscuredString;
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0018793F File Offset: 0x00185D3F
		public static implicit operator string(ObscuredString value)
		{
			if (value == null)
			{
				return null;
			}
			return value.InternalDecrypt();
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x00187955 File Offset: 0x00185D55
		public override string ToString()
		{
			return this.InternalDecrypt();
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x00187960 File Offset: 0x00185D60
		public static bool operator ==(ObscuredString a, ObscuredString b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.currentCryptoKey == b.currentCryptoKey)
			{
				return ObscuredString.ArraysEquals(a.hiddenValue, b.hiddenValue);
			}
			return string.Equals(a.InternalDecrypt(), b.InternalDecrypt());
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x001879C2 File Offset: 0x00185DC2
		public static bool operator !=(ObscuredString a, ObscuredString b)
		{
			return !(a == b);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x001879CE File Offset: 0x00185DCE
		public override bool Equals(object obj)
		{
			return obj is ObscuredString && this.Equals((ObscuredString)obj);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x001879EC File Offset: 0x00185DEC
		public bool Equals(ObscuredString value)
		{
			if (value == null)
			{
				return false;
			}
			if (this.currentCryptoKey == value.currentCryptoKey)
			{
				return ObscuredString.ArraysEquals(this.hiddenValue, value.hiddenValue);
			}
			return string.Equals(this.InternalDecrypt(), value.InternalDecrypt());
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00187A40 File Offset: 0x00185E40
		public bool Equals(ObscuredString value, StringComparison comparisonType)
		{
			return !(value == null) && string.Equals(this.InternalDecrypt(), value.InternalDecrypt(), comparisonType);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x00187A62 File Offset: 0x00185E62
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x00187A70 File Offset: 0x00185E70
		private static byte[] GetBytes(string str)
		{
			byte[] array = new byte[str.Length * 2];
			Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x00187AA0 File Offset: 0x00185EA0
		private static string GetString(byte[] bytes)
		{
			char[] array = new char[bytes.Length / 2];
			Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
			return new string(array);
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x00187ACC File Offset: 0x00185ECC
		private static bool ArraysEquals(byte[] a1, byte[] a2)
		{
			if (a1 == a2)
			{
				return true;
			}
			if (a1 == null || a2 == null)
			{
				return false;
			}
			if (a1.Length != a2.Length)
			{
				return false;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				if (a1[i] != a2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040027C3 RID: 10179
		private static string cryptoKey = "4441";

		// Token: 0x040027C4 RID: 10180
		[SerializeField]
		private string currentCryptoKey;

		// Token: 0x040027C5 RID: 10181
		[SerializeField]
		private byte[] hiddenValue;

		// Token: 0x040027C6 RID: 10182
		[SerializeField]
		private string fakeValue;

		// Token: 0x040027C7 RID: 10183
		[SerializeField]
		private bool inited;
	}
}
