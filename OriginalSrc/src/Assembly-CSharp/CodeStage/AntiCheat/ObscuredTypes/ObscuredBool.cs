using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000481 RID: 1153
	[Serializable]
	public struct ObscuredBool : IEquatable<ObscuredBool>
	{
		// Token: 0x06001FB9 RID: 8121 RVA: 0x00183442 File Offset: 0x00181842
		private ObscuredBool(int value)
		{
			this.currentCryptoKey = ObscuredBool.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = false;
			this.fakeValueChanged = false;
			this.inited = true;
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0018346B File Offset: 0x0018186B
		public static void SetNewCryptoKey(byte newKey)
		{
			ObscuredBool.cryptoKey = newKey;
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00183473 File Offset: 0x00181873
		public static int Encrypt(bool value)
		{
			return ObscuredBool.Encrypt(value, 0);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0018347C File Offset: 0x0018187C
		public static int Encrypt(bool value, byte key)
		{
			if (key == 0)
			{
				key = ObscuredBool.cryptoKey;
			}
			int num = ((!value) ? 181 : 213);
			return num ^ (int)key;
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x001834B1 File Offset: 0x001818B1
		public static bool Decrypt(int value)
		{
			return ObscuredBool.Decrypt(value, 0);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x001834BA File Offset: 0x001818BA
		public static bool Decrypt(int value, byte key)
		{
			if (key == 0)
			{
				key = ObscuredBool.cryptoKey;
			}
			value ^= (int)key;
			return value != 181;
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x001834D9 File Offset: 0x001818D9
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredBool.cryptoKey)
			{
				this.hiddenValue = ObscuredBool.Encrypt(this.InternalDecrypt(), ObscuredBool.cryptoKey);
				this.currentCryptoKey = ObscuredBool.cryptoKey;
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0018350C File Offset: 0x0018190C
		public void RandomizeCryptoKey()
		{
			bool flag = this.InternalDecrypt();
			this.currentCryptoKey = (byte)(Random.seed >> 24);
			this.hiddenValue = ObscuredBool.Encrypt(flag, this.currentCryptoKey);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00183541 File Offset: 0x00181941
		public int GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0018354F File Offset: 0x0018194F
		public void SetEncrypted(int encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
				this.fakeValueChanged = true;
			}
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0018357C File Offset: 0x0018197C
		private bool InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredBool.cryptoKey;
				this.hiddenValue = ObscuredBool.Encrypt(false);
				this.fakeValue = false;
				this.fakeValueChanged = true;
				this.inited = true;
			}
			int num = this.hiddenValue;
			num ^= (int)this.currentCryptoKey;
			bool flag = num != 181;
			if (ObscuredCheatingDetector.IsRunning && this.fakeValueChanged && flag != this.fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return flag;
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00183608 File Offset: 0x00181A08
		public static implicit operator ObscuredBool(bool value)
		{
			ObscuredBool obscuredBool = new ObscuredBool(ObscuredBool.Encrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredBool.fakeValue = value;
				obscuredBool.fakeValueChanged = true;
			}
			return obscuredBool;
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0018363D File Offset: 0x00181A3D
		public static implicit operator bool(ObscuredBool value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x00183646 File Offset: 0x00181A46
		public override bool Equals(object obj)
		{
			return obj is ObscuredBool && this.Equals((ObscuredBool)obj);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x00183664 File Offset: 0x00181A64
		public bool Equals(ObscuredBool obj)
		{
			if (this.currentCryptoKey == obj.currentCryptoKey)
			{
				return this.hiddenValue == obj.hiddenValue;
			}
			return ObscuredBool.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredBool.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x001836BC File Offset: 0x00181ABC
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x001836E0 File Offset: 0x00181AE0
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x04002742 RID: 10050
		private static byte cryptoKey = 215;

		// Token: 0x04002743 RID: 10051
		[SerializeField]
		private byte currentCryptoKey;

		// Token: 0x04002744 RID: 10052
		[SerializeField]
		private int hiddenValue;

		// Token: 0x04002745 RID: 10053
		[SerializeField]
		private bool fakeValue;

		// Token: 0x04002746 RID: 10054
		[SerializeField]
		private bool fakeValueChanged;

		// Token: 0x04002747 RID: 10055
		[SerializeField]
		private bool inited;
	}
}
