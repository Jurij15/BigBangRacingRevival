using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeStage.AntiCheat.Detectors
{
	// Token: 0x0200047F RID: 1151
	[AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Speed Hack Detector")]
	public class SpeedHackDetector : ActDetectorBase
	{
		// Token: 0x06001F71 RID: 8049 RVA: 0x001815E3 File Offset: 0x0017F9E3
		private SpeedHackDetector()
		{
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00181608 File Offset: 0x0017FA08
		public static void StartDetection()
		{
			if (SpeedHackDetector.Instance != null)
			{
				SpeedHackDetector.Instance.StartDetectionInternal(null, SpeedHackDetector.Instance.interval, SpeedHackDetector.Instance.maxFalsePositives, SpeedHackDetector.Instance.coolDown);
			}
			else
			{
				Debug.LogError("[ACTk] Speed Hack Detector: can't be started since it doesn't exists in scene or not yet initialized!");
			}
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0018165D File Offset: 0x0017FA5D
		public static void StartDetection(UnityAction callback)
		{
			SpeedHackDetector.StartDetection(callback, SpeedHackDetector.GetOrCreateInstance.interval);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0018166F File Offset: 0x0017FA6F
		public static void StartDetection(UnityAction callback, float interval)
		{
			SpeedHackDetector.StartDetection(callback, interval, SpeedHackDetector.GetOrCreateInstance.maxFalsePositives);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x00181682 File Offset: 0x0017FA82
		public static void StartDetection(UnityAction callback, float interval, byte maxFalsePositives)
		{
			SpeedHackDetector.StartDetection(callback, interval, maxFalsePositives, SpeedHackDetector.GetOrCreateInstance.coolDown);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00181696 File Offset: 0x0017FA96
		public static void StartDetection(UnityAction callback, float interval, byte maxFalsePositives, int coolDown)
		{
			SpeedHackDetector.GetOrCreateInstance.StartDetectionInternal(callback, interval, maxFalsePositives, coolDown);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x001816A6 File Offset: 0x0017FAA6
		public static void StopDetection()
		{
			if (SpeedHackDetector.Instance != null)
			{
				SpeedHackDetector.Instance.StopDetectionInternal();
			}
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x001816C2 File Offset: 0x0017FAC2
		public static void Dispose()
		{
			if (SpeedHackDetector.Instance != null)
			{
				SpeedHackDetector.Instance.DisposeInternal();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x001816DE File Offset: 0x0017FADE
		// (set) Token: 0x06001F7A RID: 8058 RVA: 0x001816E5 File Offset: 0x0017FAE5
		public static SpeedHackDetector Instance { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x001816F0 File Offset: 0x0017FAF0
		private static SpeedHackDetector GetOrCreateInstance
		{
			get
			{
				if (SpeedHackDetector.Instance != null)
				{
					return SpeedHackDetector.Instance;
				}
				if (ActDetectorBase.detectorsContainer == null)
				{
					ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
				}
				SpeedHackDetector.Instance = ActDetectorBase.detectorsContainer.AddComponent<SpeedHackDetector>();
				return SpeedHackDetector.Instance;
			}
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00181746 File Offset: 0x0017FB46
		private void Awake()
		{
			SpeedHackDetector.instancesInScene++;
			if (this.Init(SpeedHackDetector.Instance, "Speed Hack Detector"))
			{
				SpeedHackDetector.Instance = this;
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0018176F File Offset: 0x0017FB6F
		protected override void OnDestroy()
		{
			base.OnDestroy();
			SpeedHackDetector.instancesInScene--;
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x00181784 File Offset: 0x0017FB84
		private void OnLevelWasLoaded(int index)
		{
			if (SpeedHackDetector.instancesInScene < 2)
			{
				if (!this.keepAlive)
				{
					this.DisposeInternal();
				}
			}
			else if (!this.keepAlive && SpeedHackDetector.Instance != this)
			{
				this.DisposeInternal();
			}
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x001817D3 File Offset: 0x0017FBD3
		private void OnApplicationPause(bool pause)
		{
			if (!pause)
			{
				this.ResetStartTicks();
			}
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x001817E4 File Offset: 0x0017FBE4
		private void Update()
		{
			if (!this.isRunning)
			{
				return;
			}
			long ticks = DateTime.UtcNow.Ticks;
			long num = ticks - this.prevTicks;
			if (num < 0L || num > 10000000L)
			{
				this.ResetStartTicks();
				return;
			}
			this.prevTicks = ticks;
			long num2 = (long)(this.interval * 10000000f);
			if (ticks - this.prevIntervalTicks >= num2)
			{
				long num3 = (long)Environment.TickCount * 10000L;
				if (Mathf.Abs((float)(num3 - this.vulnerableTicksOnStart - (ticks - this.ticksOnStart))) > 5000000f)
				{
					this.currentFalsePositives += 1;
					if (this.currentFalsePositives > this.maxFalsePositives)
					{
						this.OnCheatingDetected();
					}
					else
					{
						this.currentCooldownShots = 0;
						this.ResetStartTicks();
					}
				}
				else if (this.currentFalsePositives > 0 && this.coolDown > 0)
				{
					this.currentCooldownShots++;
					if (this.currentCooldownShots >= this.coolDown)
					{
						this.currentFalsePositives = 0;
					}
				}
				this.prevIntervalTicks = ticks;
			}
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0018190C File Offset: 0x0017FD0C
		private void StartDetectionInternal(UnityAction callback, float checkInterval, byte falsePositives, int shotsTillCooldown)
		{
			if (this.isRunning)
			{
				Debug.LogWarning("[ACTk] Speed Hack Detector: already running!", this);
				return;
			}
			if (!base.enabled)
			{
				Debug.LogWarning("[ACTk] Speed Hack Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", this);
				return;
			}
			if (callback != null && this.detectionEventHasListener)
			{
				Debug.LogWarning("[ACTk] Speed Hack Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", this);
			}
			if (callback == null && !this.detectionEventHasListener)
			{
				Debug.LogWarning("[ACTk] Speed Hack Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", this);
				base.enabled = false;
				return;
			}
			this.detectionAction = callback;
			this.interval = checkInterval;
			this.maxFalsePositives = falsePositives;
			this.coolDown = shotsTillCooldown;
			this.ResetStartTicks();
			this.currentFalsePositives = 0;
			this.currentCooldownShots = 0;
			this.started = true;
			this.isRunning = true;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x001819C6 File Offset: 0x0017FDC6
		protected override void StartDetectionAutomatically()
		{
			this.StartDetectionInternal(null, this.interval, this.maxFalsePositives, this.coolDown);
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x001819E1 File Offset: 0x0017FDE1
		protected override void PauseDetector()
		{
			this.isRunning = false;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x001819EA File Offset: 0x0017FDEA
		protected override void ResumeDetector()
		{
			if (this.detectionAction == null && !this.detectionEventHasListener)
			{
				return;
			}
			this.isRunning = true;
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00181A0A File Offset: 0x0017FE0A
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

		// Token: 0x06001F86 RID: 8070 RVA: 0x00181A2D File Offset: 0x0017FE2D
		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (SpeedHackDetector.Instance == this)
			{
				SpeedHackDetector.Instance = null;
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00181A4C File Offset: 0x0017FE4C
		private void ResetStartTicks()
		{
			this.ticksOnStart = DateTime.UtcNow.Ticks;
			this.vulnerableTicksOnStart = (long)Environment.TickCount * 10000L;
			this.prevTicks = this.ticksOnStart;
			this.prevIntervalTicks = this.ticksOnStart;
		}

		// Token: 0x0400270A RID: 9994
		internal const string COMPONENT_NAME = "Speed Hack Detector";

		// Token: 0x0400270B RID: 9995
		internal const string FINAL_LOG_PREFIX = "[ACTk] Speed Hack Detector: ";

		// Token: 0x0400270C RID: 9996
		private const long TICKS_PER_SECOND = 10000000L;

		// Token: 0x0400270D RID: 9997
		private const int THRESHOLD = 5000000;

		// Token: 0x0400270E RID: 9998
		private static int instancesInScene;

		// Token: 0x0400270F RID: 9999
		[Tooltip("Time (in seconds) between detector checks.")]
		public float interval = 1f;

		// Token: 0x04002710 RID: 10000
		[Tooltip("Maximum false positives count allowed before registering speed hack.")]
		public byte maxFalsePositives = 3;

		// Token: 0x04002711 RID: 10001
		[Tooltip("Amount of sequential successful checks before clearing internal false positives counter.\nSet 0 to disable Cool Down feature.")]
		public int coolDown = 30;

		// Token: 0x04002712 RID: 10002
		private byte currentFalsePositives;

		// Token: 0x04002713 RID: 10003
		private int currentCooldownShots;

		// Token: 0x04002714 RID: 10004
		private long ticksOnStart;

		// Token: 0x04002715 RID: 10005
		private long vulnerableTicksOnStart;

		// Token: 0x04002716 RID: 10006
		private long prevTicks;

		// Token: 0x04002717 RID: 10007
		private long prevIntervalTicks;
	}
}
