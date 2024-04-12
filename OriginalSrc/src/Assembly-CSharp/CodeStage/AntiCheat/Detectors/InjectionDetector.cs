using System;
using System.IO;
using System.Reflection;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.Events;

namespace CodeStage.AntiCheat.Detectors
{
	// Token: 0x0200047C RID: 1148
	[AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Injection Detector")]
	public class InjectionDetector : ActDetectorBase
	{
		// Token: 0x06001F47 RID: 8007 RVA: 0x00180CED File Offset: 0x0017F0ED
		private InjectionDetector()
		{
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00180CF5 File Offset: 0x0017F0F5
		public static void StartDetection()
		{
			if (InjectionDetector.Instance != null)
			{
				InjectionDetector.Instance.StartDetectionInternal(null);
			}
			else
			{
				Debug.Log("[ACTk] Injection Detector: can't be started since it doesn't exists in scene or not yet initialized!", null);
			}
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00180D22 File Offset: 0x0017F122
		public static void StartDetection(UnityAction callback)
		{
			InjectionDetector.GetOrCreateInstance.StartDetectionInternal(callback);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00180D2F File Offset: 0x0017F12F
		public static void StopDetection()
		{
			if (InjectionDetector.Instance != null)
			{
				InjectionDetector.Instance.StopDetectionInternal();
			}
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00180D4B File Offset: 0x0017F14B
		public static void Dispose()
		{
			if (InjectionDetector.Instance != null)
			{
				InjectionDetector.Instance.DisposeInternal();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00180D67 File Offset: 0x0017F167
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x00180D6E File Offset: 0x0017F16E
		public static InjectionDetector Instance { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x00180D78 File Offset: 0x0017F178
		private static InjectionDetector GetOrCreateInstance
		{
			get
			{
				if (InjectionDetector.Instance != null)
				{
					return InjectionDetector.Instance;
				}
				if (ActDetectorBase.detectorsContainer == null)
				{
					ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
				}
				InjectionDetector.Instance = ActDetectorBase.detectorsContainer.AddComponent<InjectionDetector>();
				return InjectionDetector.Instance;
			}
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00180DCE File Offset: 0x0017F1CE
		private void Awake()
		{
			InjectionDetector.instancesInScene++;
			if (this.Init(InjectionDetector.Instance, "Injection Detector"))
			{
				InjectionDetector.Instance = this;
			}
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00180DF7 File Offset: 0x0017F1F7
		protected override void OnDestroy()
		{
			base.OnDestroy();
			InjectionDetector.instancesInScene--;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00180E0C File Offset: 0x0017F20C
		private void OnLevelWasLoaded(int index)
		{
			if (InjectionDetector.instancesInScene < 2)
			{
				if (!this.keepAlive)
				{
					this.DisposeInternal();
				}
			}
			else if (!this.keepAlive && InjectionDetector.Instance != this)
			{
				this.DisposeInternal();
			}
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x00180E5C File Offset: 0x0017F25C
		private void StartDetectionInternal(UnityAction callback)
		{
			if (this.isRunning)
			{
				Debug.LogWarning("[ACTk] Injection Detector: already running!", this);
				return;
			}
			if (!base.enabled)
			{
				Debug.LogWarning("[ACTk] Injection Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", this);
				return;
			}
			if (callback != null && this.detectionEventHasListener)
			{
				Debug.LogWarning("[ACTk] Injection Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", this);
			}
			if (callback == null && !this.detectionEventHasListener)
			{
				Debug.LogWarning("[ACTk] Injection Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", this);
				base.enabled = false;
				return;
			}
			this.detectionAction = callback;
			this.started = true;
			this.isRunning = true;
			if (this.allowedAssemblies == null)
			{
				this.LoadAndParseAllowedAssemblies();
			}
			if (this.signaturesAreNotGenuine)
			{
				this.OnCheatingDetected();
				return;
			}
			if (!this.FindInjectionInCurrentAssemblies())
			{
				AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
			}
			else
			{
				this.OnCheatingDetected();
			}
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x00180F3B File Offset: 0x0017F33B
		protected override void StartDetectionAutomatically()
		{
			this.StartDetectionInternal(null);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00180F44 File Offset: 0x0017F344
		protected override void PauseDetector()
		{
			this.isRunning = false;
			AppDomain.CurrentDomain.AssemblyLoad -= new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00180F63 File Offset: 0x0017F363
		protected override void ResumeDetector()
		{
			if (this.detectionAction == null && !this.detectionEventHasListener)
			{
				return;
			}
			this.isRunning = true;
			AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00180F99 File Offset: 0x0017F399
		protected override void StopDetectionInternal()
		{
			if (!this.started)
			{
				return;
			}
			AppDomain.CurrentDomain.AssemblyLoad -= new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
			this.detectionAction = null;
			this.started = false;
			this.isRunning = false;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00180FD2 File Offset: 0x0017F3D2
		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (InjectionDetector.Instance == this)
			{
				InjectionDetector.Instance = null;
			}
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00180FF0 File Offset: 0x0017F3F0
		private void OnNewAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
		{
			if (!this.AssemblyAllowed(args.LoadedAssembly))
			{
				this.OnCheatingDetected();
			}
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0018100C File Offset: 0x0017F40C
		private bool FindInjectionInCurrentAssemblies()
		{
			bool flag = false;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			if (assemblies.Length == 0)
			{
				flag = true;
			}
			else
			{
				foreach (Assembly assembly in assemblies)
				{
					if (!this.AssemblyAllowed(assembly))
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x00181068 File Offset: 0x0017F468
		private bool AssemblyAllowed(Assembly ass)
		{
			string name = ass.GetName().Name;
			int assemblyHash = this.GetAssemblyHash(ass);
			bool flag = false;
			for (int i = 0; i < this.allowedAssemblies.Length; i++)
			{
				InjectionDetector.AllowedAssembly allowedAssembly = this.allowedAssemblies[i];
				if (allowedAssembly.name == name && Array.IndexOf<int>(allowedAssembly.hashes, assemblyHash) != -1)
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x001810DC File Offset: 0x0017F4DC
		private void LoadAndParseAllowedAssemblies()
		{
			TextAsset textAsset = (TextAsset)Resources.Load("fndid", typeof(TextAsset));
			if (textAsset == null)
			{
				this.signaturesAreNotGenuine = true;
				return;
			}
			string[] array = new string[] { ":" };
			MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			int num = binaryReader.ReadInt32();
			this.allowedAssemblies = new InjectionDetector.AllowedAssembly[num];
			for (int i = 0; i < num; i++)
			{
				string text = binaryReader.ReadString();
				text = ObscuredString.EncryptDecrypt(text, "Elina");
				string[] array2 = text.Split(array, 1);
				int num2 = array2.Length;
				if (num2 <= 1)
				{
					this.signaturesAreNotGenuine = true;
					binaryReader.Close();
					memoryStream.Close();
					return;
				}
				string text2 = array2[0];
				int[] array3 = new int[num2 - 1];
				for (int j = 1; j < num2; j++)
				{
					array3[j - 1] = int.Parse(array2[j]);
				}
				this.allowedAssemblies[i] = new InjectionDetector.AllowedAssembly(text2, array3);
			}
			binaryReader.Close();
			memoryStream.Close();
			Resources.UnloadAsset(textAsset);
			this.hexTable = new string[256];
			for (int k = 0; k < 256; k++)
			{
				this.hexTable[k] = k.ToString("x2");
			}
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0018124C File Offset: 0x0017F64C
		private int GetAssemblyHash(Assembly ass)
		{
			AssemblyName name = ass.GetName();
			byte[] publicKeyToken = name.GetPublicKeyToken();
			string text;
			if (publicKeyToken.Length >= 8)
			{
				text = name.Name + this.PublicKeyTokenToString(publicKeyToken);
			}
			else
			{
				text = name.Name;
			}
			int num = 0;
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				num += (int)text.get_Chars(i);
				num += num << 10;
				num ^= num >> 6;
			}
			num += num << 3;
			num ^= num >> 11;
			return num + (num << 15);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x001812E0 File Offset: 0x0017F6E0
		private string PublicKeyTokenToString(byte[] bytes)
		{
			string text = string.Empty;
			for (int i = 0; i < 8; i++)
			{
				text += this.hexTable[(int)bytes[i]];
			}
			return text;
		}

		// Token: 0x040026F9 RID: 9977
		internal const string COMPONENT_NAME = "Injection Detector";

		// Token: 0x040026FA RID: 9978
		internal const string FINAL_LOG_PREFIX = "[ACTk] Injection Detector: ";

		// Token: 0x040026FB RID: 9979
		private static int instancesInScene;

		// Token: 0x040026FC RID: 9980
		private bool signaturesAreNotGenuine;

		// Token: 0x040026FD RID: 9981
		private InjectionDetector.AllowedAssembly[] allowedAssemblies;

		// Token: 0x040026FE RID: 9982
		private string[] hexTable;

		// Token: 0x0200047D RID: 1149
		private class AllowedAssembly
		{
			// Token: 0x06001F5E RID: 8030 RVA: 0x00181317 File Offset: 0x0017F717
			public AllowedAssembly(string name, int[] hashes)
			{
				this.name = name;
				this.hashes = hashes;
			}

			// Token: 0x04002700 RID: 9984
			public readonly string name;

			// Token: 0x04002701 RID: 9985
			public readonly int[] hashes;
		}
	}
}
