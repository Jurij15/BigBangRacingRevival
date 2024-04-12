using System;
using System.Diagnostics;
using System.Text;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

namespace CodeStage.AntiCheat.Examples
{
	// Token: 0x02000479 RID: 1145
	[AddComponentMenu("")]
	public class ObscuredPerformanceTests : MonoBehaviour
	{
		// Token: 0x06001F2B RID: 7979 RVA: 0x0017F90A File Offset: 0x0017DD0A
		private void Start()
		{
			base.Invoke("StartTests", 1f);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0017F91C File Offset: 0x0017DD1C
		private void StartTests()
		{
			this.logBuilder.Length = 0;
			this.logBuilder.AppendLine("[ACTk] <b>[ Performance tests ]</b>");
			if (this.boolTest)
			{
				this.TestBool();
			}
			if (this.byteTest)
			{
				this.TestByte();
			}
			if (this.shortTest)
			{
				this.TestShort();
			}
			if (this.ushortTest)
			{
				this.TestUShort();
			}
			if (this.intTest)
			{
				this.TestInt();
			}
			if (this.uintTest)
			{
				this.TestUInt();
			}
			if (this.longTest)
			{
				this.TestLong();
			}
			if (this.floatTest)
			{
				this.TestFloat();
			}
			if (this.doubleTest)
			{
				this.TestDouble();
			}
			if (this.stringTest)
			{
				this.TestString();
			}
			if (this.vector3Test)
			{
				this.TestVector3();
			}
			if (this.prefsTest)
			{
				this.TestPrefs();
			}
			Debug.Log(this.logBuilder, null);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0017FA20 File Offset: 0x0017DE20
		private void TestBool()
		{
			this.logBuilder.AppendLine("ObscuredBool vs bool, " + this.boolIterations + " iterations for read and write");
			ObscuredBool obscuredBool = true;
			bool flag = obscuredBool;
			bool flag2 = false;
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.boolIterations; i++)
			{
				flag2 = obscuredBool;
			}
			for (int j = 0; j < this.boolIterations; j++)
			{
				obscuredBool = flag2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredBool:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.boolIterations; k++)
			{
				flag2 = flag;
			}
			for (int l = 0; l < this.boolIterations; l++)
			{
				flag = flag2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("bool:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (flag2)
			{
			}
			if (obscuredBool)
			{
			}
			if (flag)
			{
			}
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0017FB6C File Offset: 0x0017DF6C
		private void TestByte()
		{
			this.logBuilder.AppendLine("ObscuredByte vs byte, " + this.byteIterations + " iterations for read and write");
			ObscuredByte obscuredByte = 100;
			byte b = obscuredByte;
			byte b2 = 0;
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.byteIterations; i++)
			{
				b2 = obscuredByte;
			}
			for (int j = 0; j < this.byteIterations; j++)
			{
				obscuredByte = b2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredByte:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.byteIterations; k++)
			{
				b2 = b;
			}
			for (int l = 0; l < this.byteIterations; l++)
			{
				b = b2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("byte:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (b2 != 0)
			{
			}
			if (obscuredByte != 0)
			{
			}
			if (b != 0)
			{
			}
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0017FCBC File Offset: 0x0017E0BC
		private void TestShort()
		{
			this.logBuilder.AppendLine("ObscuredShort vs short, " + this.shortIterations + " iterations for read and write");
			ObscuredShort obscuredShort = 100;
			short num = obscuredShort;
			short num2 = 0;
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.shortIterations; i++)
			{
				num2 = obscuredShort;
			}
			for (int j = 0; j < this.shortIterations; j++)
			{
				obscuredShort = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredShort:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.shortIterations; k++)
			{
				num2 = num;
			}
			for (int l = 0; l < this.shortIterations; l++)
			{
				num = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("short:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (num2 != 0)
			{
			}
			if (obscuredShort != 0)
			{
			}
			if (num != 0)
			{
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0017FE0C File Offset: 0x0017E20C
		private void TestUShort()
		{
			this.logBuilder.AppendLine("ObscuredUShort vs ushort, " + this.ushortIterations + " iterations for read and write");
			ObscuredUShort obscuredUShort = 100;
			ushort num = obscuredUShort;
			ushort num2 = 0;
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.ushortIterations; i++)
			{
				num2 = obscuredUShort;
			}
			for (int j = 0; j < this.ushortIterations; j++)
			{
				obscuredUShort = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredUShort:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.ushortIterations; k++)
			{
				num2 = num;
			}
			for (int l = 0; l < this.ushortIterations; l++)
			{
				num = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ushort:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (num2 != 0)
			{
			}
			if (obscuredUShort != 0)
			{
			}
			if (num != 0)
			{
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0017FF5C File Offset: 0x0017E35C
		private void TestDouble()
		{
			this.logBuilder.AppendLine("ObscuredDouble vs double, " + this.doubleIterations + " iterations for read and write");
			ObscuredDouble obscuredDouble = 100.0;
			double num = obscuredDouble;
			double num2 = 0.0;
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.doubleIterations; i++)
			{
				num2 = obscuredDouble;
			}
			for (int j = 0; j < this.doubleIterations; j++)
			{
				obscuredDouble = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredDouble:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.doubleIterations; k++)
			{
				num2 = num;
			}
			for (int l = 0; l < this.doubleIterations; l++)
			{
				num = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("double:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (num2 != 0.0)
			{
			}
			if (obscuredDouble != 0.0)
			{
			}
			if (num != 0.0)
			{
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x001800D4 File Offset: 0x0017E4D4
		private void TestFloat()
		{
			this.logBuilder.AppendLine("ObscuredFloat vs float, " + this.floatIterations + " iterations for read and write");
			ObscuredFloat obscuredFloat = 100f;
			float num = obscuredFloat;
			float num2 = 0f;
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.floatIterations; i++)
			{
				num2 = obscuredFloat;
			}
			for (int j = 0; j < this.floatIterations; j++)
			{
				obscuredFloat = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredFloat:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.floatIterations; k++)
			{
				num2 = num;
			}
			for (int l = 0; l < this.floatIterations; l++)
			{
				num = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("float:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (num2 != 0f)
			{
			}
			if (obscuredFloat != 0f)
			{
			}
			if (num != 0f)
			{
			}
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00180238 File Offset: 0x0017E638
		private void TestInt()
		{
			this.logBuilder.AppendLine("ObscuredInt vs int, " + this.intIterations + " iterations for read and write");
			ObscuredInt obscuredInt = 100;
			int num = obscuredInt;
			int num2 = 0;
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.intIterations; i++)
			{
				num2 = obscuredInt;
			}
			for (int j = 0; j < this.intIterations; j++)
			{
				obscuredInt = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredInt:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.intIterations; k++)
			{
				num2 = num;
			}
			for (int l = 0; l < this.intIterations; l++)
			{
				num = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("int:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (num2 != 0)
			{
			}
			if (obscuredInt != 0)
			{
			}
			if (num != 0)
			{
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00180388 File Offset: 0x0017E788
		private void TestLong()
		{
			this.logBuilder.AppendLine("ObscuredLong vs long, " + this.longIterations + " iterations for read and write");
			ObscuredLong obscuredLong = 100L;
			long num = obscuredLong;
			long num2 = 0L;
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.longIterations; i++)
			{
				num2 = obscuredLong;
			}
			for (int j = 0; j < this.longIterations; j++)
			{
				obscuredLong = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredLong:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.longIterations; k++)
			{
				num2 = num;
			}
			for (int l = 0; l < this.longIterations; l++)
			{
				num = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("long:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (num2 != 0L)
			{
			}
			if (obscuredLong != 0L)
			{
			}
			if (num != 0L)
			{
			}
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x001804E0 File Offset: 0x0017E8E0
		private void TestString()
		{
			this.logBuilder.AppendLine("ObscuredString vs string, " + this.stringIterations + " iterations for read and write");
			ObscuredString obscuredString = "abcd";
			string text = obscuredString;
			string text2 = string.Empty;
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.stringIterations; i++)
			{
				text2 = obscuredString;
			}
			for (int j = 0; j < this.stringIterations; j++)
			{
				obscuredString = text2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredString:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.stringIterations; k++)
			{
				text2 = text;
			}
			for (int l = 0; l < this.stringIterations; l++)
			{
				text = text2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("string:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (text2 != string.Empty)
			{
			}
			if (obscuredString != string.Empty)
			{
			}
			if (text != string.Empty)
			{
			}
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00180654 File Offset: 0x0017EA54
		private void TestUInt()
		{
			this.logBuilder.AppendLine("ObscuredUInt vs uint, " + this.uintIterations + " iterations for read and write");
			ObscuredUInt obscuredUInt = 100U;
			uint num = obscuredUInt;
			uint num2 = 0U;
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.uintIterations; i++)
			{
				num2 = obscuredUInt;
			}
			for (int j = 0; j < this.uintIterations; j++)
			{
				obscuredUInt = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredUInt:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.uintIterations; k++)
			{
				num2 = num;
			}
			for (int l = 0; l < this.uintIterations; l++)
			{
				num = num2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("uint:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (num2 != 0U)
			{
			}
			if (obscuredUInt != 0U)
			{
			}
			if (num != 0U)
			{
			}
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x001807A4 File Offset: 0x0017EBA4
		private void TestVector3()
		{
			this.logBuilder.AppendLine("ObscuredVector3 vs Vector3, " + this.vector3Iterations + " iterations for read and write");
			ObscuredVector3 obscuredVector = new Vector3(1f, 2f, 3f);
			Vector3 vector = obscuredVector;
			Vector3 vector2;
			vector2..ctor(0f, 0f, 0f);
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.vector3Iterations; i++)
			{
				vector2 = obscuredVector;
			}
			for (int j = 0; j < this.vector3Iterations; j++)
			{
				obscuredVector = vector2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredVector3:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.vector3Iterations; k++)
			{
				vector2 = vector;
			}
			for (int l = 0; l < this.vector3Iterations; l++)
			{
				vector = vector2;
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("Vector3:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			if (vector2 != Vector3.zero)
			{
			}
			if (obscuredVector != Vector3.zero)
			{
			}
			if (vector != Vector3.zero)
			{
			}
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00180930 File Offset: 0x0017ED30
		private void TestPrefs()
		{
			this.logBuilder.AppendLine("ObscuredPrefs vs PlayerPrefs, " + this.prefsIterations + " iterations for read and write");
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int i = 0; i < this.prefsIterations; i++)
			{
				ObscuredPrefs.SetInt("__a", 1);
				ObscuredPrefs.SetFloat("__b", 2f);
				ObscuredPrefs.SetString("__c", "3");
			}
			for (int j = 0; j < this.prefsIterations; j++)
			{
				ObscuredPrefs.GetInt("__a", 1);
				ObscuredPrefs.GetFloat("__b", 2f);
				ObscuredPrefs.GetString("__c", "3");
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("ObscuredPrefs:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			ObscuredPrefs.DeleteKey("__a");
			ObscuredPrefs.DeleteKey("__b");
			ObscuredPrefs.DeleteKey("__c");
			stopwatch.Reset();
			stopwatch.Start();
			for (int k = 0; k < this.prefsIterations; k++)
			{
				PlayerPrefs.SetInt("__a", 1);
				PlayerPrefs.SetFloat("__b", 2f);
				PlayerPrefs.SetString("__c", "3");
			}
			for (int l = 0; l < this.prefsIterations; l++)
			{
				PlayerPrefs.GetInt("__a", 1);
				PlayerPrefs.GetFloat("__b", 2f);
				PlayerPrefs.GetString("__c", "3");
			}
			stopwatch.Stop();
			this.logBuilder.AppendLine("PlayerPrefs:").AppendLine(stopwatch.ElapsedMilliseconds + " ms");
			PlayerPrefs.DeleteKey("__a");
			PlayerPrefs.DeleteKey("__b");
			PlayerPrefs.DeleteKey("__c");
		}

		// Token: 0x040026D3 RID: 9939
		public bool boolTest = true;

		// Token: 0x040026D4 RID: 9940
		public int boolIterations = 2500000;

		// Token: 0x040026D5 RID: 9941
		public bool byteTest = true;

		// Token: 0x040026D6 RID: 9942
		public int byteIterations = 2500000;

		// Token: 0x040026D7 RID: 9943
		public bool shortTest = true;

		// Token: 0x040026D8 RID: 9944
		public int shortIterations = 2500000;

		// Token: 0x040026D9 RID: 9945
		public bool ushortTest = true;

		// Token: 0x040026DA RID: 9946
		public int ushortIterations = 2500000;

		// Token: 0x040026DB RID: 9947
		public bool intTest = true;

		// Token: 0x040026DC RID: 9948
		public int intIterations = 2500000;

		// Token: 0x040026DD RID: 9949
		public bool uintTest = true;

		// Token: 0x040026DE RID: 9950
		public int uintIterations = 2500000;

		// Token: 0x040026DF RID: 9951
		public bool longTest = true;

		// Token: 0x040026E0 RID: 9952
		public int longIterations = 2500000;

		// Token: 0x040026E1 RID: 9953
		public bool floatTest = true;

		// Token: 0x040026E2 RID: 9954
		public int floatIterations = 2500000;

		// Token: 0x040026E3 RID: 9955
		public bool doubleTest = true;

		// Token: 0x040026E4 RID: 9956
		public int doubleIterations = 2500000;

		// Token: 0x040026E5 RID: 9957
		public bool stringTest = true;

		// Token: 0x040026E6 RID: 9958
		public int stringIterations = 250000;

		// Token: 0x040026E7 RID: 9959
		public bool vector3Test = true;

		// Token: 0x040026E8 RID: 9960
		public int vector3Iterations = 2500000;

		// Token: 0x040026E9 RID: 9961
		public bool prefsTest = true;

		// Token: 0x040026EA RID: 9962
		public int prefsIterations = 2500;

		// Token: 0x040026EB RID: 9963
		private readonly StringBuilder logBuilder = new StringBuilder();
	}
}
