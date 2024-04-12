using System;
using System.Text;
using CodeStage.AntiCheat.Utils;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x0200048C RID: 1164
	public static class ObscuredPrefs
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x00185666 File Offset: 0x00183A66
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x00185686 File Offset: 0x00183A86
		public static string DeviceId
		{
			get
			{
				if (string.IsNullOrEmpty(ObscuredPrefs.deviceId))
				{
					ObscuredPrefs.deviceId = ObscuredPrefs.GetDeviceId();
				}
				return ObscuredPrefs.deviceId;
			}
			set
			{
				ObscuredPrefs.deviceId = value;
				ObscuredPrefs.deviceIdHash = ObscuredPrefs.CalculateChecksum(ObscuredPrefs.deviceId);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x0018569D File Offset: 0x00183A9D
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x001856BD File Offset: 0x00183ABD
		[Obsolete("This property is obsolete, please use DeviceId instead.")]
		internal static string DeviceID
		{
			get
			{
				if (string.IsNullOrEmpty(ObscuredPrefs.deviceId))
				{
					ObscuredPrefs.deviceId = ObscuredPrefs.GetDeviceId();
				}
				return ObscuredPrefs.deviceId;
			}
			set
			{
				ObscuredPrefs.deviceId = value;
				ObscuredPrefs.deviceIdHash = ObscuredPrefs.CalculateChecksum(ObscuredPrefs.deviceId);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x001856D4 File Offset: 0x00183AD4
		private static uint DeviceIdHash
		{
			get
			{
				if (ObscuredPrefs.deviceIdHash == 0U)
				{
					ObscuredPrefs.deviceIdHash = ObscuredPrefs.CalculateChecksum(ObscuredPrefs.DeviceId);
				}
				return ObscuredPrefs.deviceIdHash;
			}
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x001856F4 File Offset: 0x00183AF4
		public static void ForceLockToDeviceInit()
		{
			if (string.IsNullOrEmpty(ObscuredPrefs.deviceId))
			{
				ObscuredPrefs.deviceId = ObscuredPrefs.GetDeviceId();
				ObscuredPrefs.deviceIdHash = ObscuredPrefs.CalculateChecksum(ObscuredPrefs.deviceId);
			}
			else
			{
				Debug.LogWarning("[ACTk] ObscuredPrefs.ForceLockToDeviceInit() is called, but device ID is already obtained!");
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x0018572D File Offset: 0x00183B2D
		public static void SetNewCryptoKey(string newKey)
		{
			ObscuredPrefs.encryptionKey = newKey;
			ObscuredPrefs.deviceIdHash = ObscuredPrefs.CalculateChecksum(ObscuredPrefs.deviceId);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00185744 File Offset: 0x00183B44
		public static void SetInt(string key, int value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptIntValue(key, value));
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00185758 File Offset: 0x00183B58
		public static int GetInt(string key)
		{
			return ObscuredPrefs.GetInt(key, 0);
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x00185764 File Offset: 0x00183B64
		public static int GetInt(string key, int defaultValue)
		{
			string text = ObscuredPrefs.EncryptKey(key);
			if (!PlayerPrefs.HasKey(text) && PlayerPrefs.HasKey(key))
			{
				int @int = PlayerPrefs.GetInt(key, defaultValue);
				if (!ObscuredPrefs.preservePlayerPrefs)
				{
					ObscuredPrefs.SetInt(key, @int);
					PlayerPrefs.DeleteKey(key);
				}
				return @int;
			}
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, text);
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptIntValue(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x001857D8 File Offset: 0x00183BD8
		private static string EncryptIntValue(string key, int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Int);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x001857F4 File Offset: 0x00183BF4
		private static int DecryptIntValue(string key, string encryptedInput, int defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				int num;
				int.TryParse(text, ref num);
				ObscuredPrefs.SetInt(key, num);
				return num;
			}
			else
			{
				byte[] array = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array == null)
				{
					return defaultValue;
				}
				return BitConverter.ToInt32(array, 0);
			}
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x00185853 File Offset: 0x00183C53
		public static void SetUInt(string key, uint value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptUIntValue(key, value));
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x00185867 File Offset: 0x00183C67
		public static uint GetUInt(string key)
		{
			return ObscuredPrefs.GetUInt(key, 0U);
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x00185870 File Offset: 0x00183C70
		public static uint GetUInt(string key, uint defaultValue)
		{
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptUIntValue(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x001858A8 File Offset: 0x00183CA8
		private static string EncryptUIntValue(string key, uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.UInt);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x001858C8 File Offset: 0x00183CC8
		private static uint DecryptUIntValue(string key, string encryptedInput, uint defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				uint num;
				uint.TryParse(text, ref num);
				ObscuredPrefs.SetUInt(key, num);
				return num;
			}
			else
			{
				byte[] array = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array == null)
				{
					return defaultValue;
				}
				return BitConverter.ToUInt32(array, 0);
			}
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00185927 File Offset: 0x00183D27
		public static void SetString(string key, string value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptStringValue(key, value));
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0018593B File Offset: 0x00183D3B
		public static string GetString(string key)
		{
			return ObscuredPrefs.GetString(key, string.Empty);
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00185948 File Offset: 0x00183D48
		public static string GetString(string key, string defaultValue)
		{
			string text = ObscuredPrefs.EncryptKey(key);
			if (!PlayerPrefs.HasKey(text) && PlayerPrefs.HasKey(key))
			{
				string @string = PlayerPrefs.GetString(key, defaultValue);
				if (!ObscuredPrefs.preservePlayerPrefs)
				{
					ObscuredPrefs.SetString(key, @string);
					PlayerPrefs.DeleteKey(key);
				}
				return @string;
			}
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, text);
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptStringValue(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x001859BC File Offset: 0x00183DBC
		private static string EncryptStringValue(string key, string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.String);
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x001859E0 File Offset: 0x00183DE0
		private static string DecryptStringValue(string key, string encryptedInput, string defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				ObscuredPrefs.SetString(key, text);
				return text;
			}
			else
			{
				byte[] array = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array == null)
				{
					return defaultValue;
				}
				return Encoding.UTF8.GetString(array, 0, array.Length);
			}
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00185A3E File Offset: 0x00183E3E
		public static void SetFloat(string key, float value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptFloatValue(key, value));
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00185A52 File Offset: 0x00183E52
		public static float GetFloat(string key)
		{
			return ObscuredPrefs.GetFloat(key, 0f);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00185A60 File Offset: 0x00183E60
		public static float GetFloat(string key, float defaultValue)
		{
			string text = ObscuredPrefs.EncryptKey(key);
			if (!PlayerPrefs.HasKey(text) && PlayerPrefs.HasKey(key))
			{
				float @float = PlayerPrefs.GetFloat(key, defaultValue);
				if (!ObscuredPrefs.preservePlayerPrefs)
				{
					ObscuredPrefs.SetFloat(key, @float);
					PlayerPrefs.DeleteKey(key);
				}
				return @float;
			}
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, text);
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptFloatValue(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00185AD4 File Offset: 0x00183ED4
		private static string EncryptFloatValue(string key, float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Float);
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00185AF4 File Offset: 0x00183EF4
		private static float DecryptFloatValue(string key, string encryptedInput, float defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				float num;
				float.TryParse(text, ref num);
				ObscuredPrefs.SetFloat(key, num);
				return num;
			}
			else
			{
				byte[] array = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array == null)
				{
					return defaultValue;
				}
				return BitConverter.ToSingle(array, 0);
			}
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00185B53 File Offset: 0x00183F53
		public static void SetDouble(string key, double value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptDoubleValue(key, value));
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00185B67 File Offset: 0x00183F67
		public static double GetDouble(string key)
		{
			return ObscuredPrefs.GetDouble(key, 0.0);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00185B78 File Offset: 0x00183F78
		public static double GetDouble(string key, double defaultValue)
		{
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptDoubleValue(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00185BB0 File Offset: 0x00183FB0
		private static string EncryptDoubleValue(string key, double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Double);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00185BD0 File Offset: 0x00183FD0
		private static double DecryptDoubleValue(string key, string encryptedInput, double defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				double num;
				double.TryParse(text, ref num);
				ObscuredPrefs.SetDouble(key, num);
				return num;
			}
			else
			{
				byte[] array = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array == null)
				{
					return defaultValue;
				}
				return BitConverter.ToDouble(array, 0);
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00185C2F File Offset: 0x0018402F
		public static void SetLong(string key, long value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptLongValue(key, value));
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00185C43 File Offset: 0x00184043
		public static long GetLong(string key)
		{
			return ObscuredPrefs.GetLong(key, 0L);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00185C50 File Offset: 0x00184050
		public static long GetLong(string key, long defaultValue)
		{
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptLongValue(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x00185C88 File Offset: 0x00184088
		private static string EncryptLongValue(string key, long value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Long);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00185CA8 File Offset: 0x001840A8
		private static long DecryptLongValue(string key, string encryptedInput, long defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				long num;
				long.TryParse(text, ref num);
				ObscuredPrefs.SetLong(key, num);
				return num;
			}
			else
			{
				byte[] array = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array == null)
				{
					return defaultValue;
				}
				return BitConverter.ToInt64(array, 0);
			}
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x00185D07 File Offset: 0x00184107
		public static void SetBool(string key, bool value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptBoolValue(key, value));
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00185D1B File Offset: 0x0018411B
		public static bool GetBool(string key)
		{
			return ObscuredPrefs.GetBool(key, false);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00185D24 File Offset: 0x00184124
		public static bool GetBool(string key, bool defaultValue)
		{
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptBoolValue(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x00185D5C File Offset: 0x0018415C
		private static string EncryptBoolValue(string key, bool value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Bool);
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x00185D7C File Offset: 0x0018417C
		private static bool DecryptBoolValue(string key, string encryptedInput, bool defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				int num;
				int.TryParse(text, ref num);
				ObscuredPrefs.SetBool(key, num == 1);
				return num == 1;
			}
			else
			{
				byte[] array = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array == null)
				{
					return defaultValue;
				}
				return BitConverter.ToBoolean(array, 0);
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00185DE1 File Offset: 0x001841E1
		public static void SetByteArray(string key, byte[] value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptByteArrayValue(key, value));
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00185DF5 File Offset: 0x001841F5
		public static byte[] GetByteArray(string key)
		{
			return ObscuredPrefs.GetByteArray(key, 0, 0);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00185E00 File Offset: 0x00184200
		public static byte[] GetByteArray(string key, byte defaultValue, int defaultLength)
		{
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
			if (encryptedPrefsString == "{not_found}")
			{
				return ObscuredPrefs.ConstructByteArray(defaultValue, defaultLength);
			}
			return ObscuredPrefs.DecryptByteArrayValue(key, encryptedPrefsString, defaultValue, defaultLength);
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x00185E3B File Offset: 0x0018423B
		private static string EncryptByteArrayValue(string key, byte[] value)
		{
			return ObscuredPrefs.EncryptData(key, value, ObscuredPrefs.DataType.ByteArray);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00185E48 File Offset: 0x00184248
		private static byte[] DecryptByteArrayValue(string key, string encryptedInput, byte defaultValue, int defaultLength)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return ObscuredPrefs.ConstructByteArray(defaultValue, defaultLength);
				}
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				ObscuredPrefs.SetByteArray(key, bytes);
				return bytes;
			}
			else
			{
				byte[] array = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array == null)
				{
					return ObscuredPrefs.ConstructByteArray(defaultValue, defaultLength);
				}
				return array;
			}
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x00185EB0 File Offset: 0x001842B0
		private static byte[] ConstructByteArray(byte value, int length)
		{
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = value;
			}
			return array;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x00185EDB File Offset: 0x001842DB
		public static void SetVector2(string key, Vector2 value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptVector2Value(key, value));
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00185EEF File Offset: 0x001842EF
		public static Vector2 GetVector2(string key)
		{
			return ObscuredPrefs.GetVector2(key, Vector2.zero);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00185EFC File Offset: 0x001842FC
		public static Vector2 GetVector2(string key, Vector2 defaultValue)
		{
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptVector2Value(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00185F34 File Offset: 0x00184334
		private static string EncryptVector2Value(string key, Vector2 value)
		{
			byte[] array = new byte[8];
			Buffer.BlockCopy(BitConverter.GetBytes(value.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(value.y), 0, array, 4, 4);
			return ObscuredPrefs.EncryptData(key, array, ObscuredPrefs.DataType.Vector2);
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00185F7C File Offset: 0x0018437C
		private static Vector2 DecryptVector2Value(string key, string encryptedInput, Vector2 defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				string[] array = text.Split(new char[] { "|".get_Chars(0) });
				float num;
				float.TryParse(array[0], ref num);
				float num2;
				float.TryParse(array[1], ref num2);
				Vector2 vector;
				vector..ctor(num, num2);
				ObscuredPrefs.SetVector2(key, vector);
				return vector;
			}
			else
			{
				byte[] array2 = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array2 == null)
				{
					return defaultValue;
				}
				Vector2 vector2;
				vector2.x = BitConverter.ToSingle(array2, 0);
				vector2.y = BitConverter.ToSingle(array2, 4);
				return vector2;
			}
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00186027 File Offset: 0x00184427
		public static void SetVector3(string key, Vector3 value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptVector3Value(key, value));
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x0018603B File Offset: 0x0018443B
		public static Vector3 GetVector3(string key)
		{
			return ObscuredPrefs.GetVector3(key, Vector3.zero);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00186048 File Offset: 0x00184448
		public static Vector3 GetVector3(string key, Vector3 defaultValue)
		{
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptVector3Value(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00186080 File Offset: 0x00184480
		private static string EncryptVector3Value(string key, Vector3 value)
		{
			byte[] array = new byte[12];
			Buffer.BlockCopy(BitConverter.GetBytes(value.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(value.y), 0, array, 4, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(value.z), 0, array, 8, 4);
			return ObscuredPrefs.EncryptData(key, array, ObscuredPrefs.DataType.Vector3);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x001860E0 File Offset: 0x001844E0
		private static Vector3 DecryptVector3Value(string key, string encryptedInput, Vector3 defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				string[] array = text.Split(new char[] { "|".get_Chars(0) });
				float num;
				float.TryParse(array[0], ref num);
				float num2;
				float.TryParse(array[1], ref num2);
				float num3;
				float.TryParse(array[2], ref num3);
				Vector3 vector;
				vector..ctor(num, num2, num3);
				ObscuredPrefs.SetVector3(key, vector);
				return vector;
			}
			else
			{
				byte[] array2 = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array2 == null)
				{
					return defaultValue;
				}
				Vector3 vector2;
				vector2.x = BitConverter.ToSingle(array2, 0);
				vector2.y = BitConverter.ToSingle(array2, 4);
				vector2.z = BitConverter.ToSingle(array2, 8);
				return vector2;
			}
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x001861A7 File Offset: 0x001845A7
		public static void SetQuaternion(string key, Quaternion value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptQuaternionValue(key, value));
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x001861BB File Offset: 0x001845BB
		public static Quaternion GetQuaternion(string key)
		{
			return ObscuredPrefs.GetQuaternion(key, Quaternion.identity);
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x001861C8 File Offset: 0x001845C8
		public static Quaternion GetQuaternion(string key, Quaternion defaultValue)
		{
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptQuaternionValue(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00186200 File Offset: 0x00184600
		private static string EncryptQuaternionValue(string key, Quaternion value)
		{
			byte[] array = new byte[16];
			Buffer.BlockCopy(BitConverter.GetBytes(value.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(value.y), 0, array, 4, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(value.z), 0, array, 8, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(value.w), 0, array, 12, 4);
			return ObscuredPrefs.EncryptData(key, array, ObscuredPrefs.DataType.Quaternion);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00186274 File Offset: 0x00184674
		private static Quaternion DecryptQuaternionValue(string key, string encryptedInput, Quaternion defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				string[] array = text.Split(new char[] { "|".get_Chars(0) });
				float num;
				float.TryParse(array[0], ref num);
				float num2;
				float.TryParse(array[1], ref num2);
				float num3;
				float.TryParse(array[2], ref num3);
				float num4;
				float.TryParse(array[3], ref num4);
				Quaternion quaternion;
				quaternion..ctor(num, num2, num3, num4);
				ObscuredPrefs.SetQuaternion(key, quaternion);
				return quaternion;
			}
			else
			{
				byte[] array2 = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array2 == null)
				{
					return defaultValue;
				}
				Quaternion quaternion2;
				quaternion2.x = BitConverter.ToSingle(array2, 0);
				quaternion2.y = BitConverter.ToSingle(array2, 4);
				quaternion2.z = BitConverter.ToSingle(array2, 8);
				quaternion2.w = BitConverter.ToSingle(array2, 12);
				return quaternion2;
			}
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x00186358 File Offset: 0x00184758
		public static void SetColor(string key, Color32 value)
		{
			uint num = (uint)(((int)value.a << 24) | ((int)value.r << 16) | ((int)value.g << 8) | (int)value.b);
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptColorValue(key, num));
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0018639F File Offset: 0x0018479F
		public static Color32 GetColor(string key)
		{
			return ObscuredPrefs.GetColor(key, new Color32(0, 0, 0, 1));
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x001863B0 File Offset: 0x001847B0
		public static Color32 GetColor(string key, Color32 defaultValue)
		{
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
			if (encryptedPrefsString == "{not_found}")
			{
				return defaultValue;
			}
			uint num = ObscuredPrefs.DecryptUIntValue(key, encryptedPrefsString, 16777216U);
			byte b = (byte)(num >> 24);
			byte b2 = (byte)(num >> 16);
			byte b3 = (byte)(num >> 8);
			byte b4 = (byte)(num >> 0);
			return new Color32(b2, b3, b4, b);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0018640C File Offset: 0x0018480C
		private static string EncryptColorValue(string key, uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Color);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00186429 File Offset: 0x00184829
		public static void SetRect(string key, Rect value)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptRectValue(key, value));
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0018643D File Offset: 0x0018483D
		public static Rect GetRect(string key)
		{
			return ObscuredPrefs.GetRect(key, new Rect(0f, 0f, 0f, 0f));
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00186460 File Offset: 0x00184860
		public static Rect GetRect(string key, Rect defaultValue)
		{
			string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
			return (!(encryptedPrefsString == "{not_found}")) ? ObscuredPrefs.DecryptRectValue(key, encryptedPrefsString, defaultValue) : defaultValue;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x00186498 File Offset: 0x00184898
		private static string EncryptRectValue(string key, Rect value)
		{
			byte[] array = new byte[16];
			Buffer.BlockCopy(BitConverter.GetBytes(value.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(value.y), 0, array, 4, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(value.width), 0, array, 8, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(value.height), 0, array, 12, 4);
			return ObscuredPrefs.EncryptData(key, array, ObscuredPrefs.DataType.Rect);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0018650C File Offset: 0x0018490C
		private static Rect DecryptRectValue(string key, string encryptedInput, Rect defaultValue)
		{
			if (encryptedInput.IndexOf(':') > -1)
			{
				string text = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
				if (text == string.Empty)
				{
					return defaultValue;
				}
				string[] array = text.Split(new char[] { "|".get_Chars(0) });
				float num;
				float.TryParse(array[0], ref num);
				float num2;
				float.TryParse(array[1], ref num2);
				float num3;
				float.TryParse(array[2], ref num3);
				float num4;
				float.TryParse(array[3], ref num4);
				Rect rect;
				rect..ctor(num, num2, num3, num4);
				ObscuredPrefs.SetRect(key, rect);
				return rect;
			}
			else
			{
				byte[] array2 = ObscuredPrefs.DecryptData(key, encryptedInput);
				if (array2 == null)
				{
					return defaultValue;
				}
				Rect rect2 = default(Rect);
				rect2.x = BitConverter.ToSingle(array2, 0);
				rect2.y = BitConverter.ToSingle(array2, 4);
				rect2.width = BitConverter.ToSingle(array2, 8);
				rect2.height = BitConverter.ToSingle(array2, 12);
				return rect2;
			}
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x001865F8 File Offset: 0x001849F8
		public static void SetRawValue(string key, string encryptedValue)
		{
			PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), encryptedValue);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00186608 File Offset: 0x00184A08
		public static string GetRawValue(string key)
		{
			string text = ObscuredPrefs.EncryptKey(key);
			return PlayerPrefs.GetString(text);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00186622 File Offset: 0x00184A22
		public static bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key) || PlayerPrefs.HasKey(ObscuredPrefs.EncryptKey(key));
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0018663D File Offset: 0x00184A3D
		public static void DeleteKey(string key)
		{
			PlayerPrefs.DeleteKey(ObscuredPrefs.EncryptKey(key));
			if (!ObscuredPrefs.preservePlayerPrefs)
			{
				PlayerPrefs.DeleteKey(key);
			}
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x0018665A File Offset: 0x00184A5A
		public static void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x00186661 File Offset: 0x00184A61
		public static void Save()
		{
			PlayerPrefs.Save();
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x00186668 File Offset: 0x00184A68
		private static string GetEncryptedPrefsString(string key, string encryptedKey)
		{
			string @string = PlayerPrefs.GetString(encryptedKey, "{not_found}");
			if (@string == "{not_found}" && PlayerPrefs.HasKey(key))
			{
				Debug.LogWarning("[ACTk] Are you trying to read regular PlayerPrefs data using ObscuredPrefs (key = " + key + ")?");
			}
			return @string;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x001866B2 File Offset: 0x00184AB2
		private static string EncryptKey(string key)
		{
			key = ObscuredString.EncryptDecrypt(key, ObscuredPrefs.encryptionKey);
			key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
			return key;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x001866D4 File Offset: 0x00184AD4
		private static string EncryptData(string key, byte[] cleanBytes, ObscuredPrefs.DataType type)
		{
			int num = cleanBytes.Length;
			byte[] array = ObscuredPrefs.EncryptDecryptBytes(cleanBytes, num, key + ObscuredPrefs.encryptionKey);
			uint num2 = xxHash.CalculateHash(cleanBytes, num, 0U);
			byte[] array2 = new byte[]
			{
				(byte)(num2 & 255U),
				(byte)((num2 >> 8) & 255U),
				(byte)((num2 >> 16) & 255U),
				(byte)((num2 >> 24) & 255U)
			};
			byte[] array3 = null;
			int num3;
			if (ObscuredPrefs.lockToDevice != ObscuredPrefs.DeviceLockLevel.None)
			{
				num3 = num + 11;
				uint num4 = ObscuredPrefs.DeviceIdHash;
				array3 = new byte[]
				{
					(byte)(num4 & 255U),
					(byte)((num4 >> 8) & 255U),
					(byte)((num4 >> 16) & 255U),
					(byte)((num4 >> 24) & 255U)
				};
			}
			else
			{
				num3 = num + 7;
			}
			byte[] array4 = new byte[num3];
			Buffer.BlockCopy(array, 0, array4, 0, num);
			if (array3 != null)
			{
				Buffer.BlockCopy(array3, 0, array4, num, 4);
			}
			array4[num3 - 7] = (byte)type;
			array4[num3 - 6] = 2;
			array4[num3 - 5] = (byte)ObscuredPrefs.lockToDevice;
			Buffer.BlockCopy(array2, 0, array4, num3 - 4, 4);
			return Convert.ToBase64String(array4);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x001867FC File Offset: 0x00184BFC
		private static byte[] DecryptData(string key, string encryptedInput)
		{
			byte[] array;
			try
			{
				array = Convert.FromBase64String(encryptedInput);
			}
			catch (Exception)
			{
				ObscuredPrefs.SavesTampered();
				return null;
			}
			if (array.Length <= 0)
			{
				ObscuredPrefs.SavesTampered();
				return null;
			}
			int num = array.Length;
			byte b = array[num - 6];
			if (b != 2)
			{
				ObscuredPrefs.SavesTampered();
				return null;
			}
			ObscuredPrefs.DeviceLockLevel deviceLockLevel = (ObscuredPrefs.DeviceLockLevel)array[num - 5];
			byte[] array2 = new byte[4];
			Buffer.BlockCopy(array, num - 4, array2, 0, 4);
			uint num2 = (uint)((int)array2[0] | ((int)array2[1] << 8) | ((int)array2[2] << 16) | ((int)array2[3] << 24));
			uint num3 = 0U;
			int num4;
			if (deviceLockLevel != ObscuredPrefs.DeviceLockLevel.None)
			{
				num4 = num - 11;
				if (ObscuredPrefs.lockToDevice != ObscuredPrefs.DeviceLockLevel.None)
				{
					byte[] array3 = new byte[4];
					Buffer.BlockCopy(array, num4, array3, 0, 4);
					num3 = (uint)((int)array3[0] | ((int)array3[1] << 8) | ((int)array3[2] << 16) | ((int)array3[3] << 24));
				}
			}
			else
			{
				num4 = num - 7;
			}
			byte[] array4 = new byte[num4];
			Buffer.BlockCopy(array, 0, array4, 0, num4);
			byte[] array5 = ObscuredPrefs.EncryptDecryptBytes(array4, num4, key + ObscuredPrefs.encryptionKey);
			uint num5 = xxHash.CalculateHash(array5, num4, 0U);
			if (num5 != num2)
			{
				ObscuredPrefs.SavesTampered();
				return null;
			}
			if (ObscuredPrefs.lockToDevice == ObscuredPrefs.DeviceLockLevel.Strict && num3 == 0U && !ObscuredPrefs.emergencyMode && !ObscuredPrefs.readForeignSaves)
			{
				return null;
			}
			if (num3 != 0U && !ObscuredPrefs.emergencyMode)
			{
				uint num6 = ObscuredPrefs.DeviceIdHash;
				if (num3 != num6)
				{
					ObscuredPrefs.PossibleForeignSavesDetected();
					if (!ObscuredPrefs.readForeignSaves)
					{
						return null;
					}
				}
			}
			return array5;
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00186990 File Offset: 0x00184D90
		private static uint CalculateChecksum(string input)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(input + ObscuredPrefs.encryptionKey);
			return xxHash.CalculateHash(bytes, bytes.Length, 0U);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x001869BF File Offset: 0x00184DBF
		private static void SavesTampered()
		{
			if (ObscuredPrefs.onAlterationDetected != null)
			{
				ObscuredPrefs.onAlterationDetected.Invoke();
				ObscuredPrefs.onAlterationDetected = null;
			}
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x001869DB File Offset: 0x00184DDB
		private static void PossibleForeignSavesDetected()
		{
			if (ObscuredPrefs.onPossibleForeignSavesDetected != null && !ObscuredPrefs.foreignSavesReported)
			{
				ObscuredPrefs.foreignSavesReported = true;
				ObscuredPrefs.onPossibleForeignSavesDetected.Invoke();
			}
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00186A04 File Offset: 0x00184E04
		private static string GetDeviceId()
		{
			string text = string.Empty;
			if (string.IsNullOrEmpty(text))
			{
				text = Metrics.GetMetricsGUID();
			}
			return text;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x00186A2C File Offset: 0x00184E2C
		private static byte[] EncryptDecryptBytes(byte[] bytes, int dataLength, string key)
		{
			int length = key.Length;
			byte[] array = new byte[dataLength];
			for (int i = 0; i < dataLength; i++)
			{
				array[i] = (byte)((char)bytes[i] ^ key.get_Chars(i % length));
			}
			return array;
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x00186A6C File Offset: 0x00184E6C
		private static string DeprecatedDecryptValue(string value)
		{
			string[] array = value.Split(new char[] { ':' });
			if (array.Length < 2)
			{
				ObscuredPrefs.SavesTampered();
				return string.Empty;
			}
			string text = array[0];
			string text2 = array[1];
			byte[] array2;
			try
			{
				array2 = Convert.FromBase64String(text);
			}
			catch
			{
				ObscuredPrefs.SavesTampered();
				return string.Empty;
			}
			string @string = Encoding.UTF8.GetString(array2, 0, array2.Length);
			string text3 = ObscuredString.EncryptDecrypt(@string, ObscuredPrefs.encryptionKey);
			if (array.Length == 3)
			{
				if (text2 != ObscuredPrefs.DeprecatedCalculateChecksum(text + ObscuredPrefs.DeprecatedDeviceId))
				{
					ObscuredPrefs.SavesTampered();
				}
			}
			else if (array.Length == 2)
			{
				if (text2 != ObscuredPrefs.DeprecatedCalculateChecksum(text))
				{
					ObscuredPrefs.SavesTampered();
				}
			}
			else
			{
				ObscuredPrefs.SavesTampered();
			}
			if (ObscuredPrefs.lockToDevice != ObscuredPrefs.DeviceLockLevel.None && !ObscuredPrefs.emergencyMode)
			{
				if (array.Length >= 3)
				{
					string text4 = array[2];
					if (text4 != ObscuredPrefs.DeprecatedDeviceId)
					{
						if (!ObscuredPrefs.readForeignSaves)
						{
							text3 = string.Empty;
						}
						ObscuredPrefs.PossibleForeignSavesDetected();
					}
				}
				else if (ObscuredPrefs.lockToDevice == ObscuredPrefs.DeviceLockLevel.Strict)
				{
					if (!ObscuredPrefs.readForeignSaves)
					{
						text3 = string.Empty;
					}
					ObscuredPrefs.PossibleForeignSavesDetected();
				}
				else if (text2 != ObscuredPrefs.DeprecatedCalculateChecksum(text))
				{
					if (!ObscuredPrefs.readForeignSaves)
					{
						text3 = string.Empty;
					}
					ObscuredPrefs.PossibleForeignSavesDetected();
				}
			}
			return text3;
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x00186BEC File Offset: 0x00184FEC
		private static string DeprecatedCalculateChecksum(string input)
		{
			int num = 0;
			byte[] bytes = Encoding.UTF8.GetBytes(input + ObscuredPrefs.encryptionKey);
			int num2 = bytes.Length;
			int num3 = ObscuredPrefs.encryptionKey.Length ^ 64;
			for (int i = 0; i < num2; i++)
			{
				byte b = bytes[i];
				num += (int)b + (int)b * (i + num3) % 3;
			}
			return num.ToString("X2");
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x00186C5A File Offset: 0x0018505A
		private static string DeprecatedDeviceId
		{
			get
			{
				if (string.IsNullOrEmpty(ObscuredPrefs.deprecatedDeviceId))
				{
					ObscuredPrefs.deprecatedDeviceId = ObscuredPrefs.DeprecatedCalculateChecksum(ObscuredPrefs.DeviceId);
				}
				return ObscuredPrefs.deprecatedDeviceId;
			}
		}

		// Token: 0x0400278E RID: 10126
		private const byte VERSION = 2;

		// Token: 0x0400278F RID: 10127
		private const string RAW_NOT_FOUND = "{not_found}";

		// Token: 0x04002790 RID: 10128
		private const string DATA_SEPARATOR = "|";

		// Token: 0x04002791 RID: 10129
		private static string encryptionKey = "e806f6";

		// Token: 0x04002792 RID: 10130
		private static bool foreignSavesReported;

		// Token: 0x04002793 RID: 10131
		private static string deviceId;

		// Token: 0x04002794 RID: 10132
		private static uint deviceIdHash;

		// Token: 0x04002795 RID: 10133
		public static Action onAlterationDetected;

		// Token: 0x04002796 RID: 10134
		public static bool preservePlayerPrefs;

		// Token: 0x04002797 RID: 10135
		public static Action onPossibleForeignSavesDetected;

		// Token: 0x04002798 RID: 10136
		public static ObscuredPrefs.DeviceLockLevel lockToDevice;

		// Token: 0x04002799 RID: 10137
		public static bool readForeignSaves;

		// Token: 0x0400279A RID: 10138
		public static bool emergencyMode;

		// Token: 0x0400279B RID: 10139
		private const char DEPRECATED_RAW_SEPARATOR = ':';

		// Token: 0x0400279C RID: 10140
		private static string deprecatedDeviceId;

		// Token: 0x0200048D RID: 1165
		private enum DataType : byte
		{
			// Token: 0x0400279E RID: 10142
			Int = 5,
			// Token: 0x0400279F RID: 10143
			UInt = 10,
			// Token: 0x040027A0 RID: 10144
			String = 15,
			// Token: 0x040027A1 RID: 10145
			Float = 20,
			// Token: 0x040027A2 RID: 10146
			Double = 25,
			// Token: 0x040027A3 RID: 10147
			Long = 30,
			// Token: 0x040027A4 RID: 10148
			Bool = 35,
			// Token: 0x040027A5 RID: 10149
			ByteArray = 40,
			// Token: 0x040027A6 RID: 10150
			Vector2 = 45,
			// Token: 0x040027A7 RID: 10151
			Vector3 = 50,
			// Token: 0x040027A8 RID: 10152
			Quaternion = 55,
			// Token: 0x040027A9 RID: 10153
			Color = 60,
			// Token: 0x040027AA RID: 10154
			Rect = 65
		}

		// Token: 0x0200048E RID: 1166
		public enum DeviceLockLevel : byte
		{
			// Token: 0x040027AC RID: 10156
			None,
			// Token: 0x040027AD RID: 10157
			Soft,
			// Token: 0x040027AE RID: 10158
			Strict
		}
	}
}
