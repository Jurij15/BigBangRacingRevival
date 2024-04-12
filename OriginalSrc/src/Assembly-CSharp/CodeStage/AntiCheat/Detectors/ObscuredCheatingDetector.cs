using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeStage.AntiCheat.Detectors
{
	// Token: 0x0200047E RID: 1150
	[AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Obscured Cheating Detector")]
	public class ObscuredCheatingDetector : ActDetectorBase
	{
		// Token: 0x06001F5F RID: 8031 RVA: 0x0018132D File Offset: 0x0017F72D
		private ObscuredCheatingDetector()
		{
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00181361 File Offset: 0x0017F761
		public static void StartDetection()
		{
			if (ObscuredCheatingDetector.Instance != null)
			{
				ObscuredCheatingDetector.Instance.StartDetectionInternal(null);
			}
			else
			{
				Debug.LogError("[ACTk] Obscured Cheating Detector: can't be started since it doesn't exists in scene or not yet initialized!");
			}
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0018138D File Offset: 0x0017F78D
		public static void StartDetection(UnityAction callback)
		{
			ObscuredCheatingDetector.GetOrCreateInstance.StartDetectionInternal(callback);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0018139A File Offset: 0x0017F79A
		public static void StopDetection()
		{
			if (ObscuredCheatingDetector.Instance != null)
			{
				ObscuredCheatingDetector.Instance.StopDetectionInternal();
			}
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x001813B6 File Offset: 0x0017F7B6
		public static void Dispose()
		{
			if (ObscuredCheatingDetector.Instance != null)
			{
				ObscuredCheatingDetector.Instance.DisposeInternal();
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x001813D2 File Offset: 0x0017F7D2
		// (set) Token: 0x06001F65 RID: 8037 RVA: 0x001813D9 File Offset: 0x0017F7D9
		public static ObscuredCheatingDetector Instance { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x001813E4 File Offset: 0x0017F7E4
		private static ObscuredCheatingDetector GetOrCreateInstance
		{
			get
			{
				if (ObscuredCheatingDetector.Instance != null)
				{
					return ObscuredCheatingDetector.Instance;
				}
				if (ActDetectorBase.detectorsContainer == null)
				{
					ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
				}
				ObscuredCheatingDetector.Instance = ActDetectorBase.detectorsContainer.AddComponent<ObscuredCheatingDetector>();
				return ObscuredCheatingDetector.Instance;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0018143A File Offset: 0x0017F83A
		internal static bool IsRunning
		{
			get
			{
				return ObscuredCheatingDetector.Instance != null && ObscuredCheatingDetector.Instance.isRunning;
			}
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00181453 File Offset: 0x0017F853
		private void Awake()
		{
			ObscuredCheatingDetector.instancesInScene++;
			if (this.Init(ObscuredCheatingDetector.Instance, "Obscured Cheating Detector"))
			{
				ObscuredCheatingDetector.Instance = this;
			}
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0018147C File Offset: 0x0017F87C
		protected override void OnDestroy()
		{
			base.OnDestroy();
			ObscuredCheatingDetector.instancesInScene--;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00181490 File Offset: 0x0017F890
		private void OnLevelWasLoaded(int index)
		{
			if (ObscuredCheatingDetector.instancesInScene < 2)
			{
				if (!this.keepAlive)
				{
					this.DisposeInternal();
				}
			}
			else if (!this.keepAlive && ObscuredCheatingDetector.Instance != this)
			{
				this.DisposeInternal();
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x001814E0 File Offset: 0x0017F8E0
		private void StartDetectionInternal(UnityAction callback)
		{
			if (this.isRunning)
			{
				Debug.LogWarning("[ACTk] Obscured Cheating Detector: already running!", this);
				return;
			}
			if (!base.enabled)
			{
				Debug.LogWarning("[ACTk] Obscured Cheating Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", this);
				return;
			}
			if (callback != null && this.detectionEventHasListener)
			{
				Debug.LogWarning("[ACTk] Obscured Cheating Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", this);
			}
			if (callback == null && !this.detectionEventHasListener)
			{
				Debug.LogWarning("[ACTk] Obscured Cheating Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", this);
				base.enabled = false;
				return;
			}
			this.detectionAction = callback;
			this.started = true;
			this.isRunning = true;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00181570 File Offset: 0x0017F970
		protected override void StartDetectionAutomatically()
		{
			this.StartDetectionInternal(null);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x00181579 File Offset: 0x0017F979
		protected override void PauseDetector()
		{
			this.isRunning = false;
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00181582 File Offset: 0x0017F982
		protected override void ResumeDetector()
		{
			if (this.detectionAction == null && !this.detectionEventHasListener)
			{
				return;
			}
			this.isRunning = true;
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x001815A2 File Offset: 0x0017F9A2
		protected override void StopDetectionInternal()
		{
			if (!this.started)
			{
				return;
			}
			this.detectionAction = null;
			this.started = false;
			this.isRunning = false;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x001815C5 File Offset: 0x0017F9C5
		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (ObscuredCheatingDetector.Instance == this)
			{
				ObscuredCheatingDetector.Instance = null;
			}
		}

		// Token: 0x04002702 RID: 9986
		internal const string COMPONENT_NAME = "Obscured Cheating Detector";

		// Token: 0x04002703 RID: 9987
		internal const string FINAL_LOG_PREFIX = "[ACTk] Obscured Cheating Detector: ";

		// Token: 0x04002704 RID: 9988
		private static int instancesInScene;

		// Token: 0x04002705 RID: 9989
		[Tooltip("Max allowed difference between encrypted and fake values in ObscuredFloat. Increase in case of false positives.")]
		public float floatEpsilon = 0.0001f;

		// Token: 0x04002706 RID: 9990
		[Tooltip("Max allowed difference between encrypted and fake values in ObscuredVector2. Increase in case of false positives.")]
		public float vector2Epsilon = 0.1f;

		// Token: 0x04002707 RID: 9991
		[Tooltip("Max allowed difference between encrypted and fake values in ObscuredVector3. Increase in case of false positives.")]
		public float vector3Epsilon = 0.1f;

		// Token: 0x04002708 RID: 9992
		[Tooltip("Max allowed difference between encrypted and fake values in ObscuredQuaternion. Increase in case of false positives.")]
		public float quaternionEpsilon = 0.1f;
	}
}
