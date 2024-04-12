using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeStage.AntiCheat.Detectors
{
	// Token: 0x0200047B RID: 1147
	[AddComponentMenu("")]
	public abstract class ActDetectorBase : MonoBehaviour
	{
		// Token: 0x06001F3B RID: 7995 RVA: 0x00180B44 File Offset: 0x0017EF44
		private void Start()
		{
			if (ActDetectorBase.detectorsContainer == null && base.gameObject.name == "Anti-Cheat Toolkit Detectors")
			{
				ActDetectorBase.detectorsContainer = base.gameObject;
			}
			if (this.autoStart && !this.started)
			{
				this.StartDetectionAutomatically();
			}
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00180BA2 File Offset: 0x0017EFA2
		private void OnEnable()
		{
			if (!this.started || (!this.detectionEventHasListener && this.detectionAction == null))
			{
				return;
			}
			this.ResumeDetector();
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00180BCC File Offset: 0x0017EFCC
		private void OnDisable()
		{
			if (!this.started)
			{
				return;
			}
			this.PauseDetector();
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00180BE0 File Offset: 0x0017EFE0
		private void OnApplicationQuit()
		{
			this.DisposeInternal();
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00180BE8 File Offset: 0x0017EFE8
		protected virtual void OnDestroy()
		{
			this.StopDetectionInternal();
			if (base.transform.childCount == 0 && base.GetComponentsInChildren<Component>().Length <= 2)
			{
				Object.Destroy(base.gameObject);
			}
			else if (base.name == "Anti-Cheat Toolkit Detectors" && base.GetComponentsInChildren<ActDetectorBase>().Length <= 1)
			{
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00180C57 File Offset: 0x0017F057
		protected virtual bool Init(ActDetectorBase instance, string detectorName)
		{
			if (instance != null && instance != this && instance.keepAlive)
			{
				Object.Destroy(this);
				return false;
			}
			Object.DontDestroyOnLoad(base.gameObject);
			return true;
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00180C90 File Offset: 0x0017F090
		protected virtual void DisposeInternal()
		{
			Object.Destroy(this);
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00180C98 File Offset: 0x0017F098
		internal virtual void OnCheatingDetected()
		{
			if (this.detectionAction != null)
			{
				this.detectionAction.Invoke();
			}
			if (this.detectionEventHasListener)
			{
				this.detectionEvent.Invoke();
			}
			if (this.autoDispose)
			{
				this.DisposeInternal();
			}
			else
			{
				this.StopDetectionInternal();
			}
		}

		// Token: 0x06001F43 RID: 8003
		protected abstract void StartDetectionAutomatically();

		// Token: 0x06001F44 RID: 8004
		protected abstract void StopDetectionInternal();

		// Token: 0x06001F45 RID: 8005
		protected abstract void PauseDetector();

		// Token: 0x06001F46 RID: 8006
		protected abstract void ResumeDetector();

		// Token: 0x040026ED RID: 9965
		protected const string CONTAINER_NAME = "Anti-Cheat Toolkit Detectors";

		// Token: 0x040026EE RID: 9966
		protected const string MENU_PATH = "Code Stage/Anti-Cheat Toolkit/";

		// Token: 0x040026EF RID: 9967
		protected const string GAME_OBJECT_MENU_PATH = "GameObject/Create Other/Code Stage/Anti-Cheat Toolkit/";

		// Token: 0x040026F0 RID: 9968
		protected static GameObject detectorsContainer;

		// Token: 0x040026F1 RID: 9969
		[Tooltip("Automatically start detector. Detection Event will be called on detection.")]
		public bool autoStart = true;

		// Token: 0x040026F2 RID: 9970
		[Tooltip("Detector will survive new level (scene) load if checked.")]
		public bool keepAlive = true;

		// Token: 0x040026F3 RID: 9971
		[Tooltip("Automatically dispose Detector after firing callback.")]
		public bool autoDispose = true;

		// Token: 0x040026F4 RID: 9972
		[SerializeField]
		protected UnityEvent detectionEvent;

		// Token: 0x040026F5 RID: 9973
		protected UnityAction detectionAction;

		// Token: 0x040026F6 RID: 9974
		[SerializeField]
		protected bool detectionEventHasListener;

		// Token: 0x040026F7 RID: 9975
		protected bool isRunning;

		// Token: 0x040026F8 RID: 9976
		protected bool started;
	}
}
