using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CodeStage.AntiCheat.Detectors
{
	// Token: 0x02000480 RID: 1152
	[AddComponentMenu("Code Stage/Anti-Cheat Toolkit/WallHack Detector")]
	public class WallHackDetector : ActDetectorBase
	{
		// Token: 0x06001F88 RID: 8072 RVA: 0x00181A98 File Offset: 0x0017FE98
		private WallHackDetector()
		{
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x00181B27 File Offset: 0x0017FF27
		// (set) Token: 0x06001F8A RID: 8074 RVA: 0x00181B30 File Offset: 0x0017FF30
		public bool CheckRigidbody
		{
			get
			{
				return this.checkRigidbody;
			}
			set
			{
				if (this.checkRigidbody == value || !Application.isPlaying || !base.enabled || !base.gameObject.activeSelf)
				{
					return;
				}
				this.checkRigidbody = value;
				if (!this.started)
				{
					return;
				}
				this.UpdateServiceContainer();
				if (this.checkRigidbody)
				{
					this.StartRigidModule();
				}
				else
				{
					this.StopRigidModule();
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x00181BA4 File Offset: 0x0017FFA4
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x00181BAC File Offset: 0x0017FFAC
		public bool CheckController
		{
			get
			{
				return this.checkController;
			}
			set
			{
				if (this.checkController == value || !Application.isPlaying || !base.enabled || !base.gameObject.activeSelf)
				{
					return;
				}
				this.checkController = value;
				if (!this.started)
				{
					return;
				}
				this.UpdateServiceContainer();
				if (this.checkController)
				{
					this.StartControllerModule();
				}
				else
				{
					this.StopControllerModule();
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x00181C20 File Offset: 0x00180020
		// (set) Token: 0x06001F8E RID: 8078 RVA: 0x00181C28 File Offset: 0x00180028
		public bool CheckWireframe
		{
			get
			{
				return this.checkWireframe;
			}
			set
			{
				if (this.checkWireframe == value || !Application.isPlaying || !base.enabled || !base.gameObject.activeSelf)
				{
					return;
				}
				this.checkWireframe = value;
				if (!this.started)
				{
					return;
				}
				this.UpdateServiceContainer();
				if (this.checkWireframe)
				{
					this.StartWireframeModule();
				}
				else
				{
					this.StopWireframeModule();
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x00181C9C File Offset: 0x0018009C
		// (set) Token: 0x06001F90 RID: 8080 RVA: 0x00181CA4 File Offset: 0x001800A4
		public bool CheckRaycast
		{
			get
			{
				return this.checkRaycast;
			}
			set
			{
				if (this.checkRaycast == value || !Application.isPlaying || !base.enabled || !base.gameObject.activeSelf)
				{
					return;
				}
				this.checkRaycast = value;
				if (!this.started)
				{
					return;
				}
				this.UpdateServiceContainer();
				if (this.checkRaycast)
				{
					this.StartRaycastModule();
				}
				else
				{
					this.StopRaycastModule();
				}
			}
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00181D18 File Offset: 0x00180118
		public static void StartDetection()
		{
			if (WallHackDetector.Instance != null)
			{
				WallHackDetector.Instance.StartDetectionInternal(null, WallHackDetector.Instance.spawnPosition, WallHackDetector.Instance.maxFalsePositives);
			}
			else
			{
				Debug.LogError("[ACTk] WallHack Detector: can't be started since it doesn't exists in scene or not yet initialized!");
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x00181D58 File Offset: 0x00180158
		public static void StartDetection(UnityAction callback)
		{
			WallHackDetector.StartDetection(callback, WallHackDetector.GetOrCreateInstance.spawnPosition);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x00181D6A File Offset: 0x0018016A
		public static void StartDetection(UnityAction callback, Vector3 spawnPosition)
		{
			WallHackDetector.StartDetection(callback, spawnPosition, WallHackDetector.GetOrCreateInstance.maxFalsePositives);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00181D7D File Offset: 0x0018017D
		public static void StartDetection(UnityAction callback, Vector3 spawnPosition, byte maxFalsePositives)
		{
			WallHackDetector.GetOrCreateInstance.StartDetectionInternal(callback, spawnPosition, maxFalsePositives);
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x00181D8C File Offset: 0x0018018C
		public static void StopDetection()
		{
			if (WallHackDetector.Instance != null)
			{
				WallHackDetector.Instance.StopDetectionInternal();
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x00181DA8 File Offset: 0x001801A8
		public static void Dispose()
		{
			if (WallHackDetector.Instance != null)
			{
				WallHackDetector.Instance.DisposeInternal();
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x00181DC4 File Offset: 0x001801C4
		// (set) Token: 0x06001F98 RID: 8088 RVA: 0x00181DCB File Offset: 0x001801CB
		public static WallHackDetector Instance { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x00181DD4 File Offset: 0x001801D4
		private static WallHackDetector GetOrCreateInstance
		{
			get
			{
				if (WallHackDetector.Instance != null)
				{
					return WallHackDetector.Instance;
				}
				if (ActDetectorBase.detectorsContainer == null)
				{
					ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
				}
				WallHackDetector.Instance = ActDetectorBase.detectorsContainer.AddComponent<WallHackDetector>();
				return WallHackDetector.Instance;
			}
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x00181E2A File Offset: 0x0018022A
		private void Awake()
		{
			WallHackDetector.instancesInScene++;
			if (this.Init(WallHackDetector.Instance, "WallHack Detector"))
			{
				WallHackDetector.Instance = this;
			}
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x00181E54 File Offset: 0x00180254
		protected override void OnDestroy()
		{
			base.OnDestroy();
			base.StopAllCoroutines();
			if (this.serviceContainer != null)
			{
				Object.Destroy(this.serviceContainer);
			}
			if (this.wfMaterial != null)
			{
				this.wfMaterial.mainTexture = null;
				this.wfMaterial.shader = null;
				this.wfMaterial = null;
				this.wfShader = null;
				this.shaderTexture = null;
				this.targetTexture = null;
				this.renderTexture.DiscardContents();
				this.renderTexture.Release();
				this.renderTexture = null;
			}
			WallHackDetector.instancesInScene--;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00181EF8 File Offset: 0x001802F8
		private void OnLevelWasLoaded(int index)
		{
			if (WallHackDetector.instancesInScene < 2)
			{
				if (!this.keepAlive)
				{
					this.DisposeInternal();
				}
			}
			else if (!this.keepAlive && WallHackDetector.Instance != this)
			{
				this.DisposeInternal();
			}
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00181F48 File Offset: 0x00180348
		private void FixedUpdate()
		{
			if (!this.isRunning || !this.checkRigidbody || this.rigidPlayer == null)
			{
				return;
			}
			if (this.rigidPlayer.transform.localPosition.z > 1f)
			{
				this.rigidbodyDetections += 1;
				if (!this.Detect())
				{
					this.StopRigidModule();
					this.StartRigidModule();
				}
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00181FC8 File Offset: 0x001803C8
		private void Update()
		{
			if (!this.isRunning || !this.checkController || this.charControllerPlayer == null)
			{
				return;
			}
			if (this.charControllerVelocity > 0f)
			{
				this.charControllerPlayer.Move(new Vector3(Random.Range(-0.002f, 0.002f), 0f, this.charControllerVelocity));
				if (this.charControllerPlayer.transform.localPosition.z > 1f)
				{
					this.controllerDetections += 1;
					if (!this.Detect())
					{
						this.StopControllerModule();
						this.StartControllerModule();
					}
				}
			}
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00182080 File Offset: 0x00180480
		private void StartDetectionInternal(UnityAction callback, Vector3 servicePosition, byte falsePositivesInRow)
		{
			if (this.isRunning)
			{
				Debug.LogWarning("[ACTk] WallHack Detector: already running!", this);
				return;
			}
			if (!base.enabled)
			{
				Debug.LogWarning("[ACTk] WallHack Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", this);
				return;
			}
			if (callback != null && this.detectionEventHasListener)
			{
				Debug.LogWarning("[ACTk] WallHack Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", this);
			}
			if (callback == null && !this.detectionEventHasListener)
			{
				Debug.LogWarning("[ACTk] WallHack Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", this);
				base.enabled = false;
				return;
			}
			this.detectionAction = callback;
			this.spawnPosition = servicePosition;
			this.maxFalsePositives = falsePositivesInRow;
			this.rigidbodyDetections = 0;
			this.controllerDetections = 0;
			this.wireframeDetections = 0;
			this.raycastDetections = 0;
			base.StartCoroutine(this.InitDetector());
			this.started = true;
			this.isRunning = true;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00182147 File Offset: 0x00180547
		protected override void StartDetectionAutomatically()
		{
			this.StartDetectionInternal(null, this.spawnPosition, this.maxFalsePositives);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x0018215C File Offset: 0x0018055C
		protected override void PauseDetector()
		{
			if (!this.isRunning)
			{
				return;
			}
			this.isRunning = false;
			this.StopRigidModule();
			this.StopControllerModule();
			this.StopWireframeModule();
			this.StopRaycastModule();
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0018218C File Offset: 0x0018058C
		protected override void ResumeDetector()
		{
			if (this.detectionAction == null && !this.detectionEventHasListener)
			{
				return;
			}
			this.isRunning = true;
			if (this.checkRigidbody)
			{
				this.StartRigidModule();
			}
			if (this.checkController)
			{
				this.StartControllerModule();
			}
			if (this.checkWireframe)
			{
				this.StartWireframeModule();
			}
			if (this.checkRaycast)
			{
				this.StartRaycastModule();
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x001821FB File Offset: 0x001805FB
		protected override void StopDetectionInternal()
		{
			if (!this.started)
			{
				return;
			}
			this.PauseDetector();
			this.detectionAction = null;
			this.isRunning = false;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0018221D File Offset: 0x0018061D
		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (WallHackDetector.Instance == this)
			{
				WallHackDetector.Instance = null;
			}
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0018223C File Offset: 0x0018063C
		private void UpdateServiceContainer()
		{
			if (base.enabled && base.gameObject.activeSelf)
			{
				if (this.whLayer == -1)
				{
					this.whLayer = LayerMask.NameToLayer("Ignore Raycast");
				}
				if (this.raycastMask == -1)
				{
					this.raycastMask = LayerMask.GetMask(new string[] { "Ignore Raycast" });
				}
				if (this.serviceContainer == null)
				{
					this.serviceContainer = new GameObject("[WH Detector Service]");
					this.serviceContainer.layer = this.whLayer;
					this.serviceContainer.transform.position = this.spawnPosition;
					Object.DontDestroyOnLoad(this.serviceContainer);
				}
				if ((this.checkRigidbody || this.checkController) && this.solidWall == null)
				{
					this.solidWall = new GameObject("SolidWall");
					this.solidWall.AddComponent<BoxCollider>();
					this.solidWall.layer = this.whLayer;
					this.solidWall.transform.parent = this.serviceContainer.transform;
					this.solidWall.transform.localScale = new Vector3(3f, 3f, 0.5f);
					this.solidWall.transform.localPosition = Vector3.zero;
				}
				else if (!this.checkRigidbody && !this.checkController && this.solidWall != null)
				{
					Object.Destroy(this.solidWall);
				}
				if (this.checkWireframe && this.wfCamera == null)
				{
					if (this.wfShader == null)
					{
						this.wfShader = Shader.Find("Hidden/ACTk/WallHackTexture");
					}
					if (this.wfShader == null)
					{
						Debug.LogError("[ACTk] WallHack Detector: can't find 'Hidden/ACTk/WallHackTexture' shader!\nPlease make sure you have it included at the Editor > Project Settings > Graphics.", this);
						this.checkWireframe = false;
					}
					else if (!this.wfShader.isSupported)
					{
						Debug.LogError("[ACTk] WallHack Detector: can't detect wireframe cheats on this platform!", this);
						this.checkWireframe = false;
					}
					else
					{
						if (this.wfColor1 == Color.black)
						{
							this.wfColor1 = WallHackDetector.GenerateColor();
							do
							{
								this.wfColor2 = WallHackDetector.GenerateColor();
							}
							while (WallHackDetector.ColorsSimilar(this.wfColor1, this.wfColor2, 10));
						}
						if (this.shaderTexture == null)
						{
							this.shaderTexture = new Texture2D(4, 4, 3, false);
							this.shaderTexture.filterMode = 0;
							Color[] array = new Color[16];
							for (int i = 0; i < 16; i++)
							{
								if (i < 8)
								{
									array[i] = this.wfColor1;
								}
								else
								{
									array[i] = this.wfColor2;
								}
							}
							this.shaderTexture.SetPixels(array, 0);
							this.shaderTexture.Apply();
						}
						if (this.renderTexture == null)
						{
							this.renderTexture = new RenderTexture(4, 4, 24, 0, 0);
							this.renderTexture.autoGenerateMips = false;
							this.renderTexture.filterMode = 0;
							this.renderTexture.Create();
						}
						if (this.targetTexture == null)
						{
							this.targetTexture = new Texture2D(4, 4, 3, false);
							this.targetTexture.filterMode = 0;
						}
						if (this.wfMaterial == null)
						{
							this.wfMaterial = new Material(this.wfShader);
							this.wfMaterial.mainTexture = this.shaderTexture;
						}
						if (this.foregroundRenderer == null)
						{
							GameObject gameObject = GameObject.CreatePrimitive(3);
							Object.Destroy(gameObject.GetComponent<BoxCollider>());
							gameObject.name = "WireframeFore";
							gameObject.layer = this.whLayer;
							gameObject.transform.parent = this.serviceContainer.transform;
							gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
							this.foregroundRenderer = gameObject.GetComponent<MeshRenderer>();
							this.foregroundRenderer.sharedMaterial = this.wfMaterial;
							this.foregroundRenderer.shadowCastingMode = 0;
							this.foregroundRenderer.receiveShadows = false;
							this.foregroundRenderer.enabled = false;
						}
						if (this.backgroundRenderer == null)
						{
							GameObject gameObject2 = GameObject.CreatePrimitive(5);
							Object.Destroy(gameObject2.GetComponent<MeshCollider>());
							gameObject2.name = "WireframeBack";
							gameObject2.layer = this.whLayer;
							gameObject2.transform.parent = this.serviceContainer.transform;
							gameObject2.transform.localPosition = new Vector3(0f, 0f, 1f);
							gameObject2.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
							this.backgroundRenderer = gameObject2.GetComponent<MeshRenderer>();
							this.backgroundRenderer.sharedMaterial = this.wfMaterial;
							this.backgroundRenderer.shadowCastingMode = 0;
							this.backgroundRenderer.receiveShadows = false;
							this.backgroundRenderer.enabled = false;
						}
						if (this.wfCamera == null)
						{
							this.wfCamera = new GameObject("WireframeCamera").AddComponent<Camera>();
							this.wfCamera.gameObject.layer = this.whLayer;
							this.wfCamera.transform.parent = this.serviceContainer.transform;
							this.wfCamera.transform.localPosition = new Vector3(0f, 0f, -1f);
							this.wfCamera.clearFlags = 2;
							this.wfCamera.backgroundColor = Color.black;
							this.wfCamera.orthographic = true;
							this.wfCamera.orthographicSize = 0.5f;
							this.wfCamera.nearClipPlane = 0.01f;
							this.wfCamera.farClipPlane = 2.1f;
							this.wfCamera.depth = 0f;
							this.wfCamera.renderingPath = 1;
							this.wfCamera.useOcclusionCulling = false;
							this.wfCamera.hdr = false;
							this.wfCamera.targetTexture = this.renderTexture;
							this.wfCamera.enabled = false;
						}
					}
				}
				else if (!this.checkWireframe && this.wfCamera != null)
				{
					Object.Destroy(this.foregroundRenderer.gameObject);
					Object.Destroy(this.backgroundRenderer.gameObject);
					this.wfCamera.targetTexture = null;
					Object.Destroy(this.wfCamera.gameObject);
				}
				if (this.checkRaycast && this.thinWall == null)
				{
					this.thinWall = GameObject.CreatePrimitive(4);
					this.thinWall.name = "ThinWall";
					this.thinWall.layer = this.whLayer;
					this.thinWall.transform.parent = this.serviceContainer.transform;
					this.thinWall.transform.localScale = new Vector3(0.2f, 1f, 0.2f);
					this.thinWall.transform.localRotation = Quaternion.Euler(270f, 0f, 0f);
					this.thinWall.transform.localPosition = new Vector3(0f, 0f, 1.4f);
					Object.Destroy(this.thinWall.GetComponent<Renderer>());
					Object.Destroy(this.thinWall.GetComponent<MeshFilter>());
				}
				else if (!this.checkRaycast && this.thinWall != null)
				{
					Object.Destroy(this.thinWall);
				}
			}
			else if (this.serviceContainer != null)
			{
				Object.Destroy(this.serviceContainer);
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00182A2C File Offset: 0x00180E2C
		private IEnumerator InitDetector()
		{
			yield return this.waitForEndOfFrame;
			this.UpdateServiceContainer();
			if (this.checkRigidbody)
			{
				this.StartRigidModule();
			}
			if (this.checkController)
			{
				this.StartControllerModule();
			}
			if (this.checkWireframe)
			{
				this.StartWireframeModule();
			}
			if (this.checkRaycast)
			{
				this.StartRaycastModule();
			}
			yield break;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00182A48 File Offset: 0x00180E48
		private void StartRigidModule()
		{
			if (!this.checkRigidbody)
			{
				this.StopRigidModule();
				this.UninitRigidModule();
				this.UpdateServiceContainer();
				return;
			}
			if (!this.rigidPlayer)
			{
				this.InitRigidModule();
			}
			if (this.rigidPlayer.transform.localPosition.z <= 1f && this.rigidbodyDetections > 0)
			{
				this.rigidbodyDetections = 0;
			}
			this.rigidPlayer.rotation = Quaternion.identity;
			this.rigidPlayer.angularVelocity = Vector3.zero;
			this.rigidPlayer.transform.localPosition = new Vector3(0.75f, 0f, -1f);
			this.rigidPlayer.velocity = this.rigidPlayerVelocity;
			base.Invoke("StartRigidModule", 4f);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00182B24 File Offset: 0x00180F24
		private void StartControllerModule()
		{
			if (!this.checkController)
			{
				this.StopControllerModule();
				this.UninitControllerModule();
				this.UpdateServiceContainer();
				return;
			}
			if (!this.charControllerPlayer)
			{
				this.InitControllerModule();
			}
			if (this.charControllerPlayer.transform.localPosition.z <= 1f && this.controllerDetections > 0)
			{
				this.controllerDetections = 0;
			}
			this.charControllerPlayer.transform.localPosition = new Vector3(-0.75f, 0f, -1f);
			this.charControllerVelocity = 0.01f;
			base.Invoke("StartControllerModule", 4f);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00182BD9 File Offset: 0x00180FD9
		private void StartWireframeModule()
		{
			if (!this.checkWireframe)
			{
				this.StopWireframeModule();
				this.UpdateServiceContainer();
				return;
			}
			if (!this.wireframeDetected)
			{
				base.Invoke("ShootWireframeModule", (float)this.wireframeDelay);
			}
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00182C10 File Offset: 0x00181010
		private void ShootWireframeModule()
		{
			base.StartCoroutine(this.CaptureFrame());
			base.Invoke("ShootWireframeModule", (float)this.wireframeDelay);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x00182C34 File Offset: 0x00181034
		private IEnumerator CaptureFrame()
		{
			this.wfCamera.enabled = true;
			yield return this.waitForEndOfFrame;
			this.foregroundRenderer.enabled = true;
			this.backgroundRenderer.enabled = true;
			RenderTexture previousActive = RenderTexture.active;
			RenderTexture.active = this.renderTexture;
			this.wfCamera.Render();
			this.foregroundRenderer.enabled = false;
			this.backgroundRenderer.enabled = false;
			while (!this.renderTexture.IsCreated())
			{
				yield return this.waitForEndOfFrame;
			}
			this.targetTexture.ReadPixels(new Rect(0f, 0f, 4f, 4f), 0, 0, false);
			this.targetTexture.Apply();
			RenderTexture.active = previousActive;
			if (this.wfCamera == null)
			{
				yield return null;
			}
			this.wfCamera.enabled = false;
			if (!(this.targetTexture.GetPixel(0, 3) != this.wfColor1) && !(this.targetTexture.GetPixel(0, 1) != this.wfColor2) && !(this.targetTexture.GetPixel(3, 3) != this.wfColor1) && !(this.targetTexture.GetPixel(3, 1) != this.wfColor2) && !(this.targetTexture.GetPixel(1, 3) != this.wfColor1) && !(this.targetTexture.GetPixel(2, 3) != this.wfColor1) && !(this.targetTexture.GetPixel(1, 1) != this.wfColor2) && !(this.targetTexture.GetPixel(2, 1) != this.wfColor2))
			{
				if (this.wireframeDetections > 0)
				{
					this.wireframeDetections = 0;
				}
			}
			else
			{
				this.wireframeDetections += 1;
				this.wireframeDetected = this.Detect();
			}
			yield return null;
			yield break;
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00182C4F File Offset: 0x0018104F
		private void StartRaycastModule()
		{
			if (!this.checkRaycast)
			{
				this.StopRaycastModule();
				this.UpdateServiceContainer();
				return;
			}
			base.Invoke("ShootRaycastModule", (float)this.raycastDelay);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00182C7C File Offset: 0x0018107C
		private void ShootRaycastModule()
		{
			if (Physics.Raycast(this.serviceContainer.transform.position, this.serviceContainer.transform.TransformDirection(Vector3.forward), 1.5f, this.raycastMask))
			{
				if (this.raycastDetections > 0)
				{
					this.raycastDetections = 0;
				}
			}
			else
			{
				this.raycastDetections += 1;
				if (this.Detect())
				{
					return;
				}
			}
			base.Invoke("ShootRaycastModule", (float)this.raycastDelay);
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x00182D08 File Offset: 0x00181108
		private void StopRigidModule()
		{
			if (this.rigidPlayer)
			{
				this.rigidPlayer.velocity = Vector3.zero;
			}
			base.CancelInvoke("StartRigidModule");
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x00182D35 File Offset: 0x00181135
		private void StopControllerModule()
		{
			if (this.charControllerPlayer)
			{
				this.charControllerVelocity = 0f;
			}
			base.CancelInvoke("StartControllerModule");
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00182D5D File Offset: 0x0018115D
		private void StopWireframeModule()
		{
			base.CancelInvoke("ShootWireframeModule");
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00182D6A File Offset: 0x0018116A
		private void StopRaycastModule()
		{
			base.CancelInvoke("ShootRaycastModule");
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00182D78 File Offset: 0x00181178
		private void InitRigidModule()
		{
			GameObject gameObject = new GameObject("RigidPlayer");
			gameObject.AddComponent<CapsuleCollider>().height = 2f;
			gameObject.layer = this.whLayer;
			gameObject.transform.parent = this.serviceContainer.transform;
			gameObject.transform.localPosition = new Vector3(0.75f, 0f, -1f);
			this.rigidPlayer = gameObject.AddComponent<Rigidbody>();
			this.rigidPlayer.useGravity = false;
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00182DFC File Offset: 0x001811FC
		private void InitControllerModule()
		{
			GameObject gameObject = new GameObject("ControlledPlayer");
			gameObject.AddComponent<CapsuleCollider>().height = 2f;
			gameObject.layer = this.whLayer;
			gameObject.transform.parent = this.serviceContainer.transform;
			gameObject.transform.localPosition = new Vector3(-0.75f, 0f, -1f);
			this.charControllerPlayer = gameObject.AddComponent<CharacterController>();
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00182E71 File Offset: 0x00181271
		private void UninitRigidModule()
		{
			if (!this.rigidPlayer)
			{
				return;
			}
			Object.Destroy(this.rigidPlayer.gameObject);
			this.rigidPlayer = null;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x00182E9B File Offset: 0x0018129B
		private void UninitControllerModule()
		{
			if (!this.charControllerPlayer)
			{
				return;
			}
			Object.Destroy(this.charControllerPlayer.gameObject);
			this.charControllerPlayer = null;
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x00182EC8 File Offset: 0x001812C8
		private bool Detect()
		{
			bool flag = false;
			if (this.controllerDetections > this.maxFalsePositives || this.rigidbodyDetections > this.maxFalsePositives || this.wireframeDetections > this.maxFalsePositives || this.raycastDetections > this.maxFalsePositives)
			{
				this.OnCheatingDetected();
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00182F24 File Offset: 0x00181324
		private static Color32 GenerateColor()
		{
			return new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), byte.MaxValue);
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00182F54 File Offset: 0x00181354
		private static bool ColorsSimilar(Color32 c1, Color32 c2, int tolerance)
		{
			return Math.Abs((int)(c1.r - c2.r)) < tolerance && Math.Abs((int)(c1.g - c2.g)) < tolerance && Math.Abs((int)(c1.b - c2.b)) < tolerance;
		}

		// Token: 0x04002719 RID: 10009
		internal const string COMPONENT_NAME = "WallHack Detector";

		// Token: 0x0400271A RID: 10010
		internal const string FINAL_LOG_PREFIX = "[ACTk] WallHack Detector: ";

		// Token: 0x0400271B RID: 10011
		private const string SERVICE_CONTAINER_NAME = "[WH Detector Service]";

		// Token: 0x0400271C RID: 10012
		private const string WIREFRAME_SHADER_NAME = "Hidden/ACTk/WallHackTexture";

		// Token: 0x0400271D RID: 10013
		private const int SHADER_TEXTURE_SIZE = 4;

		// Token: 0x0400271E RID: 10014
		private const int RENDER_TEXTURE_SIZE = 4;

		// Token: 0x0400271F RID: 10015
		private readonly Vector3 rigidPlayerVelocity = new Vector3(0f, 0f, 1f);

		// Token: 0x04002720 RID: 10016
		private static int instancesInScene;

		// Token: 0x04002721 RID: 10017
		private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

		// Token: 0x04002722 RID: 10018
		[SerializeField]
		[Tooltip("Check for the \"walk through the walls\" kind of cheats made via Rigidbody hacks?")]
		private bool checkRigidbody = true;

		// Token: 0x04002723 RID: 10019
		[SerializeField]
		[Tooltip("Check for the \"walk through the walls\" kind of cheats made via Character Controller hacks?")]
		private bool checkController = true;

		// Token: 0x04002724 RID: 10020
		[SerializeField]
		[Tooltip("Check for the \"see through the walls\" kind of cheats made via shader or driver hacks (wireframe, color alpha, etc.)?")]
		private bool checkWireframe = true;

		// Token: 0x04002725 RID: 10021
		[SerializeField]
		[Tooltip("Check for the \"shoot through the walls\" kind of cheats made via Raycast hacks?")]
		private bool checkRaycast = true;

		// Token: 0x04002726 RID: 10022
		[Tooltip("Delay between Wireframe module checks, from 1 up to 60 secs.")]
		[Range(1f, 60f)]
		public int wireframeDelay = 10;

		// Token: 0x04002727 RID: 10023
		[Tooltip("Delay between Raycast module checks, from 1 up to 60 secs.")]
		[Range(1f, 60f)]
		public int raycastDelay = 10;

		// Token: 0x04002728 RID: 10024
		[Tooltip("World position of the container for service objects within 3x3x3 cube (drawn as red wire cube in scene).")]
		public Vector3 spawnPosition;

		// Token: 0x04002729 RID: 10025
		[Tooltip("Maximum false positives in a row for each detection module before registering a wall hack.")]
		public byte maxFalsePositives = 3;

		// Token: 0x0400272A RID: 10026
		private GameObject serviceContainer;

		// Token: 0x0400272B RID: 10027
		private GameObject solidWall;

		// Token: 0x0400272C RID: 10028
		private GameObject thinWall;

		// Token: 0x0400272D RID: 10029
		private Camera wfCamera;

		// Token: 0x0400272E RID: 10030
		private MeshRenderer foregroundRenderer;

		// Token: 0x0400272F RID: 10031
		private MeshRenderer backgroundRenderer;

		// Token: 0x04002730 RID: 10032
		private Color wfColor1 = Color.black;

		// Token: 0x04002731 RID: 10033
		private Color wfColor2 = Color.black;

		// Token: 0x04002732 RID: 10034
		private Shader wfShader;

		// Token: 0x04002733 RID: 10035
		private Material wfMaterial;

		// Token: 0x04002734 RID: 10036
		private Texture2D shaderTexture;

		// Token: 0x04002735 RID: 10037
		private Texture2D targetTexture;

		// Token: 0x04002736 RID: 10038
		private RenderTexture renderTexture;

		// Token: 0x04002737 RID: 10039
		private int whLayer = -1;

		// Token: 0x04002738 RID: 10040
		private int raycastMask = -1;

		// Token: 0x04002739 RID: 10041
		private Rigidbody rigidPlayer;

		// Token: 0x0400273A RID: 10042
		private CharacterController charControllerPlayer;

		// Token: 0x0400273B RID: 10043
		private float charControllerVelocity;

		// Token: 0x0400273C RID: 10044
		private byte rigidbodyDetections;

		// Token: 0x0400273D RID: 10045
		private byte controllerDetections;

		// Token: 0x0400273E RID: 10046
		private byte wireframeDetections;

		// Token: 0x0400273F RID: 10047
		private byte raycastDetections;

		// Token: 0x04002740 RID: 10048
		private bool wireframeDetected;
	}
}
