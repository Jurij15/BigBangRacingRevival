using System;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Google.Developers
{
	// Token: 0x02000610 RID: 1552
	public class JavaObjWrapper
	{
		// Token: 0x06002D76 RID: 11638 RVA: 0x001C0F6F File Offset: 0x001BF36F
		protected JavaObjWrapper()
		{
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x001C0F82 File Offset: 0x001BF382
		public JavaObjWrapper(string clazzName)
		{
			this.raw = AndroidJNI.AllocObject(AndroidJNI.FindClass(clazzName));
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x001C0FA6 File Offset: 0x001BF3A6
		public JavaObjWrapper(IntPtr rawObject)
		{
			this.raw = rawObject;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x001C0FC0 File Offset: 0x001BF3C0
		public IntPtr RawObject
		{
			get
			{
				return this.raw;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x001C0FC8 File Offset: 0x001BF3C8
		public virtual IntPtr RawClass
		{
			get
			{
				if (this.cachedRawClass == IntPtr.Zero && this.raw != IntPtr.Zero)
				{
					this.cachedRawClass = AndroidJNI.GetObjectClass(this.raw);
				}
				return this.cachedRawClass;
			}
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x001C1018 File Offset: 0x001BF418
		public void CreateInstance(string clazzName, params object[] args)
		{
			if (this.raw != IntPtr.Zero)
			{
				throw new Exception("Java object already set");
			}
			IntPtr constructorID = AndroidJNIHelper.GetConstructorID(this.RawClass, args);
			jvalue[] array = JavaObjWrapper.ConstructArgArray(args);
			this.raw = AndroidJNI.NewObject(this.RawClass, constructorID, array);
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x001C106C File Offset: 0x001BF46C
		protected static jvalue[] ConstructArgArray(object[] theArgs)
		{
			object[] array = new object[theArgs.Length];
			for (int i = 0; i < theArgs.Length; i++)
			{
				if (theArgs[i] is JavaObjWrapper)
				{
					array[i] = ((JavaObjWrapper)theArgs[i]).raw;
				}
				else
				{
					array[i] = theArgs[i];
				}
			}
			jvalue[] array2 = AndroidJNIHelper.CreateJNIArgArray(array);
			for (int j = 0; j < theArgs.Length; j++)
			{
				if (theArgs[j] is JavaObjWrapper)
				{
					array2[j].l = ((JavaObjWrapper)theArgs[j]).raw;
				}
				else if (theArgs[j] is JavaInterfaceProxy)
				{
					IntPtr intPtr = AndroidJNIHelper.CreateJavaProxy((AndroidJavaProxy)theArgs[j]);
					array2[j].l = intPtr;
				}
			}
			if (array2.Length == 1)
			{
				for (int k = 0; k < array2.Length; k++)
				{
					Debug.Log(string.Concat(new object[]
					{
						"---- [",
						k,
						"] -- ",
						array2[k].l
					}));
				}
			}
			return array2;
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x001C1194 File Offset: 0x001BF594
		public static T StaticInvokeObjectCall<T>(string type, string name, string sig, params object[] args)
		{
			IntPtr intPtr = AndroidJNI.FindClass(type);
			IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr, name, sig);
			jvalue[] array = JavaObjWrapper.ConstructArgArray(args);
			IntPtr intPtr2 = AndroidJNI.CallStaticObjectMethod(intPtr, staticMethodID, array);
			ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { intPtr2.GetType() });
			if (constructor != null)
			{
				return (T)((object)constructor.Invoke(new object[] { intPtr2 }));
			}
			if (typeof(T).IsArray)
			{
				return AndroidJNIHelper.ConvertFromJNIArray<T>(intPtr2);
			}
			Debug.Log("Trying cast....");
			Type typeFromHandle = typeof(T);
			return (T)((object)Marshal.PtrToStructure(intPtr2, typeFromHandle));
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x001C124C File Offset: 0x001BF64C
		public static void StaticInvokeCallVoid(string type, string name, string sig, params object[] args)
		{
			IntPtr intPtr = AndroidJNI.FindClass(type);
			IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr, name, sig);
			jvalue[] array = JavaObjWrapper.ConstructArgArray(args);
			AndroidJNI.CallStaticVoidMethod(intPtr, staticMethodID, array);
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x001C1278 File Offset: 0x001BF678
		public static T GetStaticObjectField<T>(string clsName, string name, string sig)
		{
			IntPtr intPtr = AndroidJNI.FindClass(clsName);
			IntPtr staticFieldID = AndroidJNI.GetStaticFieldID(intPtr, name, sig);
			IntPtr staticObjectField = AndroidJNI.GetStaticObjectField(intPtr, staticFieldID);
			ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { staticObjectField.GetType() });
			if (constructor != null)
			{
				return (T)((object)constructor.Invoke(new object[] { staticObjectField }));
			}
			Type typeFromHandle = typeof(T);
			return (T)((object)Marshal.PtrToStructure(staticObjectField, typeFromHandle));
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x001C1300 File Offset: 0x001BF700
		public static int GetStaticIntField(string clsName, string name)
		{
			IntPtr intPtr = AndroidJNI.FindClass(clsName);
			IntPtr staticFieldID = AndroidJNI.GetStaticFieldID(intPtr, name, "I");
			return AndroidJNI.GetStaticIntField(intPtr, staticFieldID);
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x001C1328 File Offset: 0x001BF728
		public static string GetStaticStringField(string clsName, string name)
		{
			IntPtr intPtr = AndroidJNI.FindClass(clsName);
			IntPtr staticFieldID = AndroidJNI.GetStaticFieldID(intPtr, name, "Ljava/lang/String;");
			return AndroidJNI.GetStaticStringField(intPtr, staticFieldID);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x001C1350 File Offset: 0x001BF750
		public static float GetStaticFloatField(string clsName, string name)
		{
			IntPtr intPtr = AndroidJNI.FindClass(clsName);
			IntPtr staticFieldID = AndroidJNI.GetStaticFieldID(intPtr, name, "F");
			return AndroidJNI.GetStaticFloatField(intPtr, staticFieldID);
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x001C1378 File Offset: 0x001BF778
		public void InvokeCallVoid(string name, string sig, params object[] args)
		{
			IntPtr methodID = AndroidJNI.GetMethodID(this.RawClass, name, sig);
			jvalue[] array = JavaObjWrapper.ConstructArgArray(args);
			AndroidJNI.CallVoidMethod(this.raw, methodID, array);
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x001C13A8 File Offset: 0x001BF7A8
		public T InvokeCall<T>(string name, string sig, params object[] args)
		{
			Type typeFromHandle = typeof(T);
			IntPtr methodID = AndroidJNI.GetMethodID(this.RawClass, name, sig);
			jvalue[] array = JavaObjWrapper.ConstructArgArray(args);
			if (methodID == IntPtr.Zero)
			{
				Debug.LogError("Cannot get method for " + name);
				throw new Exception("Cannot get method for " + name);
			}
			if (typeFromHandle == typeof(bool))
			{
				return (T)((object)AndroidJNI.CallBooleanMethod(this.raw, methodID, array));
			}
			if (typeFromHandle == typeof(string))
			{
				return (T)((object)AndroidJNI.CallStringMethod(this.raw, methodID, array));
			}
			if (typeFromHandle == typeof(int))
			{
				return (T)((object)AndroidJNI.CallIntMethod(this.raw, methodID, array));
			}
			if (typeFromHandle == typeof(float))
			{
				return (T)((object)AndroidJNI.CallFloatMethod(this.raw, methodID, array));
			}
			if (typeFromHandle == typeof(double))
			{
				return (T)((object)AndroidJNI.CallDoubleMethod(this.raw, methodID, array));
			}
			if (typeFromHandle == typeof(byte))
			{
				return (T)((object)AndroidJNI.CallByteMethod(this.raw, methodID, array));
			}
			if (typeFromHandle == typeof(char))
			{
				return (T)((object)AndroidJNI.CallCharMethod(this.raw, methodID, array));
			}
			if (typeFromHandle == typeof(long))
			{
				return (T)((object)AndroidJNI.CallLongMethod(this.raw, methodID, array));
			}
			if (typeFromHandle == typeof(short))
			{
				return (T)((object)AndroidJNI.CallShortMethod(this.raw, methodID, array));
			}
			return this.InvokeObjectCall<T>(name, sig, args);
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x001C1574 File Offset: 0x001BF974
		public static T StaticInvokeCall<T>(string type, string name, string sig, params object[] args)
		{
			Type typeFromHandle = typeof(T);
			IntPtr intPtr = AndroidJNI.FindClass(type);
			IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr, name, sig);
			jvalue[] array = JavaObjWrapper.ConstructArgArray(args);
			if (typeFromHandle == typeof(bool))
			{
				return (T)((object)AndroidJNI.CallStaticBooleanMethod(intPtr, staticMethodID, array));
			}
			if (typeFromHandle == typeof(string))
			{
				return (T)((object)AndroidJNI.CallStaticStringMethod(intPtr, staticMethodID, array));
			}
			if (typeFromHandle == typeof(int))
			{
				return (T)((object)AndroidJNI.CallStaticIntMethod(intPtr, staticMethodID, array));
			}
			if (typeFromHandle == typeof(float))
			{
				return (T)((object)AndroidJNI.CallStaticFloatMethod(intPtr, staticMethodID, array));
			}
			if (typeFromHandle == typeof(double))
			{
				return (T)((object)AndroidJNI.CallStaticDoubleMethod(intPtr, staticMethodID, array));
			}
			if (typeFromHandle == typeof(byte))
			{
				return (T)((object)AndroidJNI.CallStaticByteMethod(intPtr, staticMethodID, array));
			}
			if (typeFromHandle == typeof(char))
			{
				return (T)((object)AndroidJNI.CallStaticCharMethod(intPtr, staticMethodID, array));
			}
			if (typeFromHandle == typeof(long))
			{
				return (T)((object)AndroidJNI.CallStaticLongMethod(intPtr, staticMethodID, array));
			}
			if (typeFromHandle == typeof(short))
			{
				return (T)((object)AndroidJNI.CallStaticShortMethod(intPtr, staticMethodID, array));
			}
			return JavaObjWrapper.StaticInvokeObjectCall<T>(type, name, sig, args);
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x001C16E4 File Offset: 0x001BFAE4
		public T InvokeObjectCall<T>(string name, string sig, params object[] theArgs)
		{
			IntPtr methodID = AndroidJNI.GetMethodID(this.RawClass, name, sig);
			jvalue[] array = JavaObjWrapper.ConstructArgArray(theArgs);
			IntPtr intPtr = AndroidJNI.CallObjectMethod(this.raw, methodID, array);
			if (intPtr.Equals(IntPtr.Zero))
			{
				return default(T);
			}
			ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { intPtr.GetType() });
			if (constructor != null)
			{
				return (T)((object)constructor.Invoke(new object[] { intPtr }));
			}
			Type typeFromHandle = typeof(T);
			return (T)((object)Marshal.PtrToStructure(intPtr, typeFromHandle));
		}

		// Token: 0x04003174 RID: 12660
		private IntPtr raw;

		// Token: 0x04003175 RID: 12661
		private IntPtr cachedRawClass = IntPtr.Zero;
	}
}
