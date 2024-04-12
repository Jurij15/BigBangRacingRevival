using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x020004F8 RID: 1272
public static class CameraS
{
	// Token: 0x06002496 RID: 9366 RVA: 0x001936E8 File Offset: 0x00191AE8
	public static void Initialize()
	{
		CameraS.m_screenWidth = Screen.width;
		CameraS.m_screenHeight = Screen.height;
		Entity entity = EntityManager.AddEntity();
		entity.m_persistent = true;
		CameraS.cameraRootEntity = entity;
		if (CameraS.m_debugDraw)
		{
			CameraS.m_debugTC = TransformS.AddComponent(entity, "Main Camera Debug Transform");
			CameraS.m_debugTC2 = TransformS.AddComponent(entity, "Main Camera Debug Transform");
		}
		CameraS.m_mainCameraTC = TransformS.AddComponent(entity, "Main Camera Transform");
		CameraS.m_mainCameraRotateTC = TransformS.AddComponent(entity, "Main Camera Rotate Transform");
		CameraS.m_mainCamera = Camera.main;
		CameraS.m_mainCamera.depth = 1f;
		CameraS.m_mainCamera.targetTexture = null;
		CameraS.m_mainCamera.cullingMask = CameraS.m_mainCameraCullingMask;
		CameraS.m_mainCamera.gameObject.layer = CameraS.m_mainCameraLayer;
		CameraS.m_mainCamera.fieldOfView = CameraS.m_mainCameraFov;
		CameraS.m_mainCamera.farClipPlane = CameraS.m_mainCameraFarClip;
		CameraS.m_mainCamera.nearClipPlane = CameraS.m_mainCameraNearClip;
		CameraS.m_mainCamera.fieldOfView = CameraS.m_mainCameraFov;
		CameraS.m_mainCamera.backgroundColor = Color.black;
		for (int i = 0; i < CameraS.m_mainCamera.transform.childCount; i++)
		{
			Transform child = CameraS.m_mainCamera.transform.GetChild(i);
			child.gameObject.layer = CameraS.m_mainCamera.gameObject.layer;
		}
		CameraS.m_mainCamera.transform.parent = CameraS.m_mainCameraTC.transform;
		CameraS.m_mainCameraTC.transform.parent = CameraS.m_mainCameraRotateTC.transform;
		CameraS.m_mainCamera.transform.localPosition = Vector3.forward * -CameraS.m_mainCameraDistance;
		CameraS.m_mainCamera.transform.localRotation = Quaternion.identity;
		CameraS.m_mainCameraRotateTC.transform.localPosition = Vector3.zero;
		GameObject gameObject = new GameObject("UI Camera");
		CameraS.m_uiCamera = gameObject.AddComponent<Camera>();
		CameraS.m_uiCamera.orthographic = true;
		CameraS.m_uiCamera.orthographicSize = (float)Screen.height * 0.5f;
		CameraS.m_uiCamera.depth = 2f;
		CameraS.m_uiCamera.cullingMask = 1 << CameraS.m_uiCameraLayer;
		CameraS.m_uiCamera.gameObject.layer = CameraS.m_uiCameraLayer;
		CameraS.m_uiCamera.nearClipPlane = CameraS.m_uiCameraNearClip;
		CameraS.m_uiCamera.farClipPlane = CameraS.m_uiCameraFarClip;
		CameraS.m_uiCamera.gameObject.transform.position = new Vector3(0f, 0f, -500f);
		CameraS.m_uiCamera.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		CameraS.m_uiCamera.clearFlags = 3;
		CameraS.m_cameraTargetComponents = new DynamicArray<CameraTargetC>(100, 0.5f, 0.25f, 0.5f);
		CameraS.m_mainCameraFrame = ChipmunkProWrapper.ucpBBNew((float)Screen.width * -0.5f, (float)Screen.height * -0.5f, (float)Screen.width * 0.5f, (float)Screen.height * 0.5f);
		CameraS.m_cameras = new List<Camera>();
		CameraS.m_cameras.Add(CameraS.m_mainCamera);
		CameraS.CreateRenderTextureCamera();
		CameraS.m_cameras.Add(CameraS.m_uiCamera);
		CameraS.m_overlayCameras = new List<Camera>();
		CameraS.m_mainCameraRenderTextureDirty = false;
		CameraS.m_mainCameraRenderTexture = new RenderTexture(Screen.width, Screen.height, 0, 4);
		CameraS.m_mainCameraRenderTexture.useMipMap = false;
		CameraS.m_mainCameraRenderTexture.anisoLevel = 0;
	}

	// Token: 0x06002497 RID: 9367 RVA: 0x00193A64 File Offset: 0x00191E64
	public static void CreateRenderTextureCamera()
	{
		GameObject gameObject = new GameObject("Render Texture Camera");
		CameraS.m_renderTextureViewCamera = gameObject.AddComponent<Camera>();
		CameraS.m_renderTextureViewCamera.clearFlags = 2;
		CameraS.m_renderTextureViewCamera.backgroundColor = Color.black;
		CameraS.m_renderTextureViewCamera.gameObject.transform.position = new Vector3(0f, 0f, -500f);
		CameraS.m_renderTextureViewCamera.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		CameraS.m_renderTextureViewCamera.depth = 0f;
		CameraS.m_renderTextureViewCamera.cullingMask = 1 << CameraS.m_renderTextureViewCameraLayer;
		CameraS.m_renderTextureViewCamera.orthographic = true;
		CameraS.m_renderTextureViewCamera.orthographicSize = (float)Screen.height * 0.5f;
		CameraS.m_renderTextureViewCamera.nearClipPlane = CameraS.m_uiCameraNearClip;
		CameraS.m_renderTextureViewCamera.farClipPlane = CameraS.m_uiCameraFarClip;
		CameraS.m_cameras.Add(CameraS.m_renderTextureViewCamera);
		CameraS.m_renderTexture = new RenderTexture(Screen.width, Screen.height, 0, 4);
		CameraS.m_renderTexture.useMipMap = false;
		CameraS.m_renderTexture.anisoLevel = 0;
		CameraS.m_renderTextureViewCanvas = GameObject.CreatePrimitive(5);
		CameraS.m_renderTextureViewCanvas.name = "RenderTextureViewCanvas";
		CameraS.m_renderTextureViewCanvas.layer = CameraS.m_renderTextureViewCameraLayer;
		CameraS.m_renderTextureViewCanvas.transform.parent = CameraS.m_renderTextureViewCamera.transform;
		Vector3 vector;
		vector..ctor((float)Screen.width, (float)Screen.height, 0f);
		CameraS.m_renderTextureViewCanvas.transform.localScale = vector;
		CameraS.m_renderTextureViewCanvas.transform.localPosition += Vector3.forward * 2f;
		CameraS.m_renderTextureViewCanvas.GetComponent<Collider>().enabled = false;
		CameraS.m_renderTextureViewCamera.enabled = false;
		Renderer component = CameraS.m_renderTextureViewCamera.transform.GetChild(0).gameObject.GetComponent<Renderer>();
		component.useLightProbes = false;
		component.receiveShadows = false;
		component.shadowCastingMode = 0;
		component.material = new Material(Shader.Find("Framework/ColorUnlitTransparent"));
		CameraS.m_renderTextureViewMaterial = component.material;
	}

	// Token: 0x06002498 RID: 9368 RVA: 0x00193C8C File Offset: 0x0019208C
	public static RenderTexture RenderCameraToRenderTexture(Camera _camera)
	{
		_camera.targetTexture = CameraS.m_renderTexture;
		RenderTexture.active = _camera.targetTexture;
		_camera.Render();
		_camera.targetTexture = null;
		RenderTexture.active = null;
		return CameraS.m_renderTexture;
	}

	// Token: 0x06002499 RID: 9369 RVA: 0x00193CBC File Offset: 0x001920BC
	public static void EnableRenderTextureView()
	{
		CameraS.m_renderTextureViewCamera.enabled = true;
		CameraS.m_renderTextureViewCanvas.GetComponent<MeshRenderer>().enabled = true;
	}

	// Token: 0x0600249A RID: 9370 RVA: 0x00193CD9 File Offset: 0x001920D9
	public static void DisableRenderTextureView()
	{
		CameraS.m_renderTextureViewCamera.enabled = false;
		CameraS.m_renderTextureViewCanvas.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x0600249B RID: 9371 RVA: 0x00193CF8 File Offset: 0x001920F8
	public static void StoreTexture()
	{
		if (CameraS.m_renderTextureViewCamera != null)
		{
			if (CameraS.m_blur != null && CameraS.m_blur.enabled && CameraS.m_renderTextureViewCamera.enabled && CameraS.m_blur.storedTex != null && CameraS.m_createTex)
			{
				CameraS.m_storedRenderTexture.m_storedRT = new Texture2D(CameraS.m_blur.storedTex.width, CameraS.m_blur.storedTex.height, 3, false, true);
				CameraS.m_storedRenderTexture.m_storedRT.wrapMode = 1;
				RenderTexture.active = CameraS.m_blur.storedTex;
				CameraS.m_storedRenderTexture.m_storedRT.ReadPixels(new Rect(0f, 0f, (float)CameraS.m_blur.storedTex.width, (float)CameraS.m_blur.storedTex.height), 0, 0);
				CameraS.m_storedRenderTexture.m_storedRT.Apply();
				RenderTexture.active = null;
				CameraS.m_createTex = false;
			}
			else if (CameraS.m_renderTextureViewCamera.enabled && CameraS.m_createTex)
			{
				CameraS.m_storedRenderTexture.m_storedRT = new Texture2D(CameraS.m_renderTextureViewMaterial.mainTexture.width, CameraS.m_renderTextureViewMaterial.mainTexture.height, 3, false, true);
				CameraS.m_storedRenderTexture.m_storedRT.wrapMode = 1;
				CameraS.m_storedRenderTexture.m_storedRT.ReadPixels(new Rect(0f, 0f, (float)CameraS.m_renderTextureViewMaterial.mainTexture.width, (float)CameraS.m_renderTextureViewMaterial.mainTexture.height), 0, 0);
				CameraS.m_storedRenderTexture.m_storedRT.Apply();
				RenderTexture.active = null;
				CameraS.m_createTex = false;
			}
		}
	}

	// Token: 0x0600249C RID: 9372 RVA: 0x00193EC8 File Offset: 0x001922C8
	public static void ApplyTexture()
	{
		if (CameraS.m_renderTextureViewCamera != null && CameraS.m_renderTextureViewCamera.enabled && CameraS.m_storedRenderTexture.m_storedRT != null)
		{
			CameraS.m_renderTextureViewMaterial.mainTexture = CameraS.m_storedRenderTexture.m_storedRT;
			if (CameraS.m_blur != null)
			{
				Graphics.Blit(CameraS.m_storedRenderTexture.m_storedRT, CameraS.m_blur.storedTex);
			}
		}
	}

	// Token: 0x0600249D RID: 9373 RVA: 0x00193F46 File Offset: 0x00192346
	public static void CreateBlur(Action _blurDoneCallback = null)
	{
		if (CameraS.m_cameraForBlur == null)
		{
			CameraS.m_cameraForBlur = CameraS.AddCamera("CameraForBlur", true, 3);
		}
		CameraS.CreateBlur(CameraS.m_cameraForBlur, _blurDoneCallback);
	}

	// Token: 0x0600249E RID: 9374 RVA: 0x00193F74 File Offset: 0x00192374
	public static void CreateBlur(Camera _camera, Action _blurDoneCallback = null)
	{
		CameraS.m_createTex = true;
		CameraS.m_currentCamera = _camera;
		if (CameraS.m_blur != null && CameraS.m_blur.enabled)
		{
			CameraS.m_blur.enabled = false;
		}
		CameraS.m_blur = _camera.GetComponent<BlurOptimizedWOE>();
		if (CameraS.m_blur == null)
		{
			CameraS.m_blur = _camera.gameObject.AddComponent<BlurOptimizedWOE>();
			CameraS.m_blur.blurShader = Shader.Find("Hidden/FastBlur");
			CameraS.m_blur.done = false;
			CameraS.m_blur.update = false;
			CameraS.m_blur.blurSize = 0f;
			CameraS.m_blur.enabled = false;
			CameraS.m_updateBlur = (CameraS.m_blurFadeIn = (CameraS.m_blurFadeOut = false));
		}
		if (!CameraS.m_blur.enabled)
		{
			CameraS.m_blurDoneCallback = _blurDoneCallback;
			CameraS.m_blur.blurSize = 0f;
			CameraS.m_blur.downsample = 2;
			CameraS.m_blur.enabled = true;
			CameraS.m_blurFadeIn = true;
			CameraS.m_blurFadeOut = false;
			CameraS.m_updateBlur = true;
			CameraS.m_blur.done = false;
			CameraS.m_blur.update = true;
		}
	}

	// Token: 0x0600249F RID: 9375 RVA: 0x0019409C File Offset: 0x0019249C
	public static void UpdateBlur()
	{
		if (CameraS.m_renderTextureViewCamera != null && CameraS.m_blur != null && CameraS.m_blur.storedTex != null && !CameraS.m_blur.storedTex.IsCreated())
		{
			CameraS.m_blur.storedTex.Create();
		}
		if (CameraS.m_blur != null && CameraS.m_updateBlur)
		{
			if (CameraS.m_blurFadeIn && !CameraS.m_blurFadeOut)
			{
				CameraS.m_blur.blurSize += 0.25f;
				if (CameraS.m_blur.blurSize >= 2f)
				{
					CameraS.m_blur.blurSize = 2f;
					CameraS.m_blurFadeIn = false;
					CameraS.m_updateBlur = false;
					CameraS.m_blur.update = false;
					if (CameraS.m_blurDoneCallback != null)
					{
						CameraS.m_blurDoneCallback.Invoke();
					}
					CameraS.m_blur.done = true;
					CameraS.m_renderTextureViewCamera.enabled = true;
					CameraS.m_renderTextureViewMaterial.mainTexture = CameraS.m_blur.storedTex;
					for (int i = 0; i < CameraS.m_cameras.Count; i++)
					{
						if (!(CameraS.m_cameras[i] == CameraS.m_renderTextureViewCamera) && CameraS.m_currentCamera.depth >= CameraS.m_cameras[i].depth && CameraS.m_cameras[i].enabled)
						{
							CameraS.m_cameras[i].enabled = false;
							if (CameraS.m_blurDisabledCameras == null)
							{
								CameraS.m_blurDisabledCameras = new List<Camera>();
							}
							CameraS.m_blurDisabledCameras.Add(CameraS.m_cameras[i]);
						}
					}
				}
			}
			if (CameraS.m_blurFadeOut)
			{
				CameraS.m_blur.blurSize -= 0.25f;
				if (CameraS.m_blur.blurSize <= 0f)
				{
					CameraS.m_blur.blurSize = 0f;
					CameraS.m_blurFadeOut = false;
					CameraS.m_updateBlur = false;
					CameraS.DestroyBlur();
				}
			}
		}
	}

	// Token: 0x060024A0 RID: 9376 RVA: 0x001942BC File Offset: 0x001926BC
	public static void RemoveBlur()
	{
		if (CameraS.m_blur != null)
		{
			if (CameraS.m_blurDisabledCameras != null)
			{
				for (int i = CameraS.m_blurDisabledCameras.Count - 1; i >= 0; i--)
				{
					if (CameraS.m_blurDisabledCameras[i] != null)
					{
						CameraS.m_blurDisabledCameras[i].enabled = true;
					}
				}
				CameraS.m_blurDisabledCameras.Clear();
			}
			if (CameraS.m_cameraForBlur != null)
			{
				CameraS.RemoveCamera(CameraS.m_cameraForBlur);
				CameraS.m_cameraForBlur = null;
			}
			CameraS.m_blurFadeOut = true;
			CameraS.m_updateBlur = true;
			CameraS.m_blur.update = true;
			CameraS.m_blur.done = false;
		}
		CameraS.m_renderTextureViewCamera.enabled = false;
	}

	// Token: 0x060024A1 RID: 9377 RVA: 0x0019437E File Offset: 0x0019277E
	private static void DestroyBlur()
	{
		if (CameraS.m_blur != null)
		{
			CameraS.m_blur.enabled = false;
		}
		CameraS.m_createTex = false;
	}

	// Token: 0x060024A2 RID: 9378 RVA: 0x001943A1 File Offset: 0x001927A1
	public static void ShakeMainCamera(float _amount, float _duration, int _tickInterval = 2)
	{
		CameraS.m_mainCamShakeAmount = _amount;
		CameraS.m_mainCamShakeTime = _duration;
		CameraS.m_mainCamShakeTickInterval = _tickInterval;
	}

	// Token: 0x060024A3 RID: 9379 RVA: 0x001943B8 File Offset: 0x001927B8
	public static void SnapMainCameraFrame()
	{
		cpBB cpBB = CameraS.CalculateMergedTargetBB();
		if (!cpBB.isNull())
		{
			CameraS.m_mainCameraFrame = cpBB;
			CameraS.m_mainCameraRotation = Vector3.zero;
		}
		CameraS.m_mainCamShakeTime = 0f;
	}

	// Token: 0x060024A4 RID: 9380 RVA: 0x001943F4 File Offset: 0x001927F4
	public static void SnapMainCameraPos(Vector2 _position)
	{
		CameraS.m_mainCameraPosition = _position;
		CameraS.m_mainCameraRotateTC.transform.position = CameraS.m_mainCameraPosition;
		CameraS.m_mainCameraTC.transform.localPosition = Vector3.zero;
		CameraS.m_mainCameraFrame = CameraS.CenterBB(_position, CameraS.m_mainCameraFrame);
		CameraS.m_mainCameraRotation = Vector3.zero;
	}

	// Token: 0x060024A5 RID: 9381 RVA: 0x00194450 File Offset: 0x00192850
	public static void RenderMainCameraToTexture(Shader _replacementShader)
	{
		Color backgroundColor = CameraS.m_mainCamera.backgroundColor;
		CameraS.m_mainCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
		CameraS.m_mainCamera.targetTexture = CameraS.m_mainCameraRenderTexture;
		CameraS.m_mainCamera.RenderWithShader(_replacementShader, "RenderType");
		CameraS.m_mainCamera.targetTexture = null;
		CameraS.m_mainCamera.backgroundColor = backgroundColor;
		CameraS.m_mainCameraRenderTextureDirty = true;
	}

	// Token: 0x060024A6 RID: 9382 RVA: 0x001944C8 File Offset: 0x001928C8
	public static void ClearMainCameraRenderTexture()
	{
		if (CameraS.m_mainCameraRenderTextureDirty)
		{
			CameraS.m_mainCameraRenderTextureDirty = false;
			Color backgroundColor = CameraS.m_mainCamera.backgroundColor;
			int cullingMask = CameraS.m_mainCamera.cullingMask;
			CameraS.m_mainCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
			CameraS.m_mainCamera.cullingMask = 0;
			CameraS.m_mainCamera.targetTexture = CameraS.m_mainCameraRenderTexture;
			CameraS.m_mainCamera.Render();
			CameraS.m_mainCamera.targetTexture = null;
			CameraS.m_mainCamera.backgroundColor = backgroundColor;
			CameraS.m_mainCamera.cullingMask = cullingMask;
		}
	}

	// Token: 0x060024A7 RID: 9383 RVA: 0x00194564 File Offset: 0x00192964
	public static Camera AddCamera(string _name, bool _ortographic, CameraClearFlags _clearFlags = 3)
	{
		GameObject gameObject = new GameObject(_name);
		Camera camera = gameObject.AddComponent<Camera>();
		camera.depth = 1f;
		camera.gameObject.transform.position = new Vector3(0f, 0f, -500f);
		camera.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		camera.clearFlags = _clearFlags;
		CameraLayer cameraLayer = CameraS.m_cameraLayers.AddItem();
		int num = CameraS.m_otherCamerasStartLayer + cameraLayer.m_index;
		if (_ortographic)
		{
			camera.orthographic = _ortographic;
			camera.orthographicSize = (float)Screen.height * 0.5f;
			camera.cullingMask = 1 << num;
			camera.gameObject.layer = num;
			camera.nearClipPlane = CameraS.m_uiCameraNearClip;
			camera.farClipPlane = CameraS.m_uiCameraFarClip;
			CameraS.m_cameras.Add(camera);
		}
		else
		{
			camera.orthographic = false;
			camera.cullingMask = 1 << num;
			camera.gameObject.layer = num;
			camera.nearClipPlane = CameraS.m_mainCameraNearClip;
			camera.farClipPlane = CameraS.m_mainCameraFarClip;
			for (int i = 0; i < CameraS.m_cameras.Count; i++)
			{
				if (CameraS.m_cameras[i] == CameraS.m_uiCamera)
				{
					CameraS.m_cameras.Insert(i, camera);
					break;
				}
			}
		}
		for (int j = 0; j < CameraS.m_cameras.Count; j++)
		{
			CameraS.m_cameras[j].depth = (float)j;
		}
		CameraS.ReorderOverlayCameras();
		return camera;
	}

	// Token: 0x060024A8 RID: 9384 RVA: 0x00194708 File Offset: 0x00192B08
	public static void SetAsOverlayCamera(Camera _camera)
	{
		if (CameraS.m_overlayCameras.Contains(_camera))
		{
			return;
		}
		CameraS.m_overlayCameras.Add(_camera);
		CameraS.ReorderOverlayCameras();
	}

	// Token: 0x060024A9 RID: 9385 RVA: 0x0019472C File Offset: 0x00192B2C
	private static void ReorderOverlayCameras()
	{
		for (int i = 0; i < CameraS.m_overlayCameras.Count; i++)
		{
			if (CameraS.m_cameras.Remove(CameraS.m_overlayCameras[i]))
			{
				CameraS.m_cameras.Add(CameraS.m_overlayCameras[i]);
			}
		}
		for (int j = 0; j < CameraS.m_cameras.Count; j++)
		{
			CameraS.m_cameras[j].depth = (float)j;
		}
	}

	// Token: 0x060024AA RID: 9386 RVA: 0x001947B0 File Offset: 0x00192BB0
	public static void RemoveFromOverlayCameras(Camera _camera)
	{
		CameraS.m_overlayCameras.Remove(_camera);
	}

	// Token: 0x060024AB RID: 9387 RVA: 0x001947C0 File Offset: 0x00192BC0
	public static bool IsInFront(Camera _camera, bool _ignoreOverlayCameras = true)
	{
		int num = CameraS.m_cameras.IndexOf(_camera);
		if (_ignoreOverlayCameras || CameraS.m_overlayCameras.Count == 0)
		{
			return num == CameraS.m_cameras.Count - 1;
		}
		return num == CameraS.m_cameras.Count - 1 - CameraS.m_overlayCameras.Count;
	}

	// Token: 0x060024AC RID: 9388 RVA: 0x00194818 File Offset: 0x00192C18
	public static bool MoveToBehindOther(Camera _camera, Camera _otherCamera)
	{
		if (CameraS.m_cameras.Contains(_otherCamera) && CameraS.m_cameras.Contains(_camera) && !CameraS.m_overlayCameras.Contains(_camera) && !CameraS.m_overlayCameras.Contains(_otherCamera))
		{
			CameraS.m_cameras.Remove(_camera);
			CameraS.m_cameras.Insert(CameraS.m_cameras.IndexOf(_otherCamera), _camera);
			for (int i = 0; i < CameraS.m_cameras.Count; i++)
			{
				CameraS.m_cameras[i].depth = (float)i;
			}
			return true;
		}
		return false;
	}

	// Token: 0x060024AD RID: 9389 RVA: 0x001948B8 File Offset: 0x00192CB8
	public static void BringToFront(Camera _camera, bool _updateCameraDepth = true)
	{
		if (CameraS.m_cameras.Remove(_camera))
		{
			CameraS.m_cameras.Add(_camera);
			if (CameraS.m_overlayCameras.Remove(_camera))
			{
				CameraS.m_overlayCameras.Add(_camera);
			}
			if (_updateCameraDepth)
			{
				for (int i = 0; i < CameraS.m_cameras.Count; i++)
				{
					CameraS.m_cameras[i].depth = (float)i;
				}
			}
			if (!CameraS.m_overlayCameras.Contains(_camera))
			{
				CameraS.ReorderOverlayCameras();
			}
		}
	}

	// Token: 0x060024AE RID: 9390 RVA: 0x00194944 File Offset: 0x00192D44
	public static bool MoveToFront(Camera _camera)
	{
		if (CameraS.m_cameras.Remove(_camera))
		{
			CameraS.m_cameras.Add(_camera);
			if (CameraS.m_overlayCameras.Remove(_camera))
			{
				CameraS.m_overlayCameras.Add(_camera);
			}
			for (int i = 0; i < CameraS.m_cameras.Count; i++)
			{
				CameraS.m_cameras[i].depth = (float)i;
			}
			if (!CameraS.m_overlayCameras.Contains(_camera))
			{
				CameraS.ReorderOverlayCameras();
			}
			return true;
		}
		return false;
	}

	// Token: 0x060024AF RID: 9391 RVA: 0x001949CC File Offset: 0x00192DCC
	public static void RemoveCamera(Camera _camera)
	{
		if (_camera != null && _camera != CameraS.m_uiCamera && _camera != CameraS.m_mainCamera)
		{
			if (CameraS.m_overlayCameras.Contains(_camera))
			{
				CameraS.m_overlayCameras.Remove(_camera);
			}
			CameraS.m_cameraLayers.RemoveItem(_camera.gameObject.layer - CameraS.m_otherCamerasStartLayer);
			CameraS.m_cameras.Remove(_camera);
			Object.DestroyImmediate(_camera.gameObject);
		}
	}

	// Token: 0x060024B0 RID: 9392 RVA: 0x00194A54 File Offset: 0x00192E54
	public static CameraTargetC AddTargetComponent(TransformC _tc, float _width, float _height, Vector2 _offset)
	{
		CameraTargetC cameraTargetC = CameraS.m_cameraTargetComponents.AddItem();
		cameraTargetC.targetTC = _tc;
		cameraTargetC.frameTC = TransformS.AddComponent(CameraS.cameraRootEntity, "Camera Target Transform", _tc.transform.position);
		cameraTargetC.frameTC.forceRotation = true;
		cameraTargetC.frameTC.forceScale = true;
		cameraTargetC.offset = _offset;
		cameraTargetC.lastFrameTCPos = _tc.transform.position;
		cameraTargetC.lastTargetTCPos = _tc.transform.position;
		cameraTargetC.framePrefab = null;
		cameraTargetC.translatedCenter = Vector2.zero;
		cameraTargetC.framePeek = Vector2.zero;
		CameraS.SetTargetBB(cameraTargetC, _width, _height);
		EntityManager.AddComponentToEntity(_tc.p_entity, cameraTargetC);
		return cameraTargetC;
	}

	// Token: 0x060024B1 RID: 9393 RVA: 0x00194B08 File Offset: 0x00192F08
	public static void ResetFramePosition(CameraTargetC _ct)
	{
		_ct.frameTC.transform.position = _ct.targetTC.transform.position;
		_ct.lastFrameTCPos = _ct.frameTC.transform.position;
		_ct.lastTargetTCPos = _ct.targetTC.transform.position;
		_ct.translatedCenter = Vector2.zero;
		_ct.secondaryTargetTransMult = 1f;
	}

	// Token: 0x060024B2 RID: 9394 RVA: 0x00194B78 File Offset: 0x00192F78
	public static void SetTargetBB(CameraTargetC _ct, float _width, float _height)
	{
		if (CameraS.DEBUG)
		{
			if (_ct.framePrefab != null)
			{
				PrefabS.RemoveComponent(_ct.framePrefab, true);
			}
			_ct.framePrefab = PrefabS.CreateRect(_ct.frameTC, _ct.offset + new Vector3(0f, 0f, 2.5f), _width, _height, new Color(0f, 1f, 0f, 0.2f), ResourceManager.GetMaterial(RESOURCE.DebugMaterial_Material), CameraS.m_mainCamera);
		}
		_ct.targetFrame = ChipmunkProWrapper.ucpBBNew(_width * -0.5f, _height * -0.5f, _width * 0.5f, _height * 0.5f);
	}

	// Token: 0x060024B3 RID: 9395 RVA: 0x00194C2C File Offset: 0x0019302C
	public static void RemoveAllTargetComponents()
	{
		while (CameraS.m_cameraTargetComponents.m_aliveCount > 0)
		{
			CameraTargetC cameraTargetC = CameraS.m_cameraTargetComponents.m_array[CameraS.m_cameraTargetComponents.m_aliveIndices[0]];
			CameraS.RemoveTargetComponent(cameraTargetC);
		}
	}

	// Token: 0x060024B4 RID: 9396 RVA: 0x00194C6C File Offset: 0x0019306C
	public static void RemoveTargetComponent(CameraTargetC _c)
	{
		if (_c.framePrefab != null)
		{
			PrefabS.RemoveComponent(_c.framePrefab, true);
			_c.framePrefab = null;
		}
		if (_c.frameTC != null)
		{
			TransformS.RemoveComponent(_c.frameTC);
			_c.frameTC = null;
		}
		EntityManager.RemoveComponentFromEntity(_c);
		CameraS.m_cameraTargetComponents.RemoveItem(_c);
	}

	// Token: 0x060024B5 RID: 9397 RVA: 0x00194CC8 File Offset: 0x001930C8
	public static cpBB TransformBB(cpBB _bb, Vector2 _pos, float _scale)
	{
		_bb.l = _pos.x + _bb.l * _scale;
		_bb.r = _pos.x + _bb.r * _scale;
		_bb.b = _pos.y + _bb.b * _scale;
		_bb.t = _pos.y + _bb.t * _scale;
		return _bb;
	}

	// Token: 0x060024B6 RID: 9398 RVA: 0x00194D38 File Offset: 0x00193138
	public static cpBB CenterBB(Vector2 _pos, cpBB _bb)
	{
		cpBB cpBB = default(cpBB);
		float num = (_bb.r - _bb.l) * 0.5f;
		float num2 = (_bb.t - _bb.b) * 0.5f;
		cpBB.l = _pos.x - num;
		cpBB.r = _pos.x + num;
		cpBB.t = _pos.y + num2;
		cpBB.b = _pos.y - num2;
		return cpBB;
	}

	// Token: 0x060024B7 RID: 9399 RVA: 0x00194DBC File Offset: 0x001931BC
	private static cpBB ExpandBBToDirection(cpBB _bb, float _x, float _y)
	{
		if (_x < 0f)
		{
			_bb.l += _x;
		}
		else if (_x > 0f)
		{
			_bb.r += _x;
		}
		if (_y < 0f)
		{
			_bb.b += _y;
		}
		else if (_y > 0f)
		{
			_bb.t += _y;
		}
		return _bb;
	}

	// Token: 0x060024B8 RID: 9400 RVA: 0x00194E3C File Offset: 0x0019323C
	private static cpBB ScaleBB(cpBB _bb, float _scale)
	{
		float num = (_bb.r - _bb.l) * 0.5f;
		float num2 = (_bb.t - _bb.b) * 0.5f;
		Vector2 vector;
		vector..ctor(_bb.l + num, _bb.b + num2);
		_bb.l = vector.x - num * _scale;
		_bb.r = vector.x + num * _scale;
		_bb.b = vector.y - num2 * _scale;
		_bb.t = vector.y + num2 * _scale;
		return _bb;
	}

	// Token: 0x060024B9 RID: 9401 RVA: 0x00194ED8 File Offset: 0x001932D8
	private static cpBB ClampBB(cpBB _bb, cpBB _clamp)
	{
		_bb.b = Mathf.Max(_bb.b, _clamp.b);
		_bb.t = Mathf.Min(_bb.t, _clamp.t);
		_bb.l = Mathf.Max(_bb.l, _clamp.l);
		_bb.r = Mathf.Min(_bb.r, _clamp.r);
		return _bb;
	}

	// Token: 0x060024BA RID: 9402 RVA: 0x00194F50 File Offset: 0x00193350
	private static cpBB ContainBB(cpBB _bb, cpBB _clamp)
	{
		float num = Mathf.Max(0f, _clamp.b - _bb.b);
		float num2 = Mathf.Max(0f, _bb.t - _clamp.t);
		float num3 = Mathf.Max(0f, _bb.r - _clamp.r);
		float num4 = Mathf.Max(0f, _clamp.l - _bb.l);
		_bb.b = Mathf.Max(_bb.b, _clamp.b) + num2;
		_bb.t = Mathf.Min(_bb.t, _clamp.t) + num;
		_bb.l = Mathf.Max(_bb.l, _clamp.l) + num3;
		_bb.r = Mathf.Min(_bb.r, _clamp.r) + num4;
		return _bb;
	}

	// Token: 0x060024BB RID: 9403 RVA: 0x0019504E File Offset: 0x0019344E
	private static float GetBBWidth(cpBB _bb)
	{
		return _bb.r - _bb.l;
	}

	// Token: 0x060024BC RID: 9404 RVA: 0x0019505F File Offset: 0x0019345F
	private static float GetBBHeight(cpBB _bb)
	{
		return _bb.t - _bb.b;
	}

	// Token: 0x060024BD RID: 9405 RVA: 0x00195070 File Offset: 0x00193470
	private static Vector2 GetBBCenter(cpBB _bb)
	{
		float num = (_bb.r - _bb.l) * 0.5f;
		float num2 = (_bb.t - _bb.b) * 0.5f;
		return new Vector2(_bb.l + num, _bb.b + num2);
	}

	// Token: 0x060024BE RID: 9406 RVA: 0x001950C0 File Offset: 0x001934C0
	private static cpBB CalculateMergedTargetBB()
	{
		int num = 0;
		int num2 = 0;
		cpBB cpBB = default(cpBB);
		CameraS.m_combinedTargetsVelocity = Vector2.zero;
		Vector2 primaryTargetsCenter = CameraS.GetPrimaryTargetsCenter();
		int aliveCount = CameraS.m_cameraTargetComponents.m_aliveCount;
		for (int i = 0; i < aliveCount; i++)
		{
			CameraTargetC cameraTargetC = CameraS.m_cameraTargetComponents.m_array[CameraS.m_cameraTargetComponents.m_aliveIndices[i]];
			bool flag = true;
			if (cameraTargetC.activeRadius < 3.4028235E+38f)
			{
				float magnitude = (primaryTargetsCenter - cameraTargetC.frameTC.transform.position).magnitude;
				if (magnitude > cameraTargetC.activeRadius)
				{
					cameraTargetC.secondaryTargetTransMult += 1f / (float)Main.m_logicFPS;
				}
				else
				{
					cameraTargetC.secondaryTargetTransMult -= 1f / (float)Main.m_logicFPS;
					float positionBetween = ToolBox.getPositionBetween(magnitude, cameraTargetC.activeRadius * 0.75f, cameraTargetC.activeRadius);
					cameraTargetC.secondaryTargetTransMult = Mathf.Min(cameraTargetC.secondaryTargetTransMult, positionBetween);
				}
				if (cameraTargetC.frameTC.transform.position.y < CameraS.m_cameraTargetLimits.b || cameraTargetC.frameTC.transform.position.y > CameraS.m_cameraTargetLimits.t || cameraTargetC.frameTC.transform.position.x < CameraS.m_cameraTargetLimits.l || cameraTargetC.frameTC.transform.position.x > CameraS.m_cameraTargetLimits.r)
				{
					flag = false;
				}
				cameraTargetC.secondaryTargetTransMult = ToolBox.limitBetween(cameraTargetC.secondaryTargetTransMult, 0f, 1f);
				if (cameraTargetC.secondaryTargetTransMult == 1f)
				{
					flag = false;
					cameraTargetC.frameTC.transform.position = cameraTargetC.targetTC.transform.position;
					cameraTargetC.lastTargetTCPos = cameraTargetC.targetTC.transform.position;
					cameraTargetC.lastFrameTCPos = cameraTargetC.targetTC.transform.position;
				}
			}
			if (cameraTargetC.m_active && flag)
			{
				if (cameraTargetC.frameSlopRadiusMinMax != Vector2.zero)
				{
					float x = cameraTargetC.frameSlopRadiusMinMax.x;
					float y = cameraTargetC.frameSlopRadiusMinMax.y;
					Vector2 vector = cameraTargetC.frameTC.transform.position - cameraTargetC.targetTC.transform.position;
					float magnitude2 = vector.magnitude;
					if (magnitude2 > x)
					{
						float positionBetween2 = ToolBox.getPositionBetween(magnitude2, x, y);
						TransformS.Move(cameraTargetC.frameTC, -vector.normalized * positionBetween2 * (magnitude2 - x));
					}
				}
				else
				{
					TransformS.SetPosition(cameraTargetC.frameTC, cameraTargetC.targetTC.transform.position);
				}
				Vector3 vector2 = Vector3.zero;
				if (cameraTargetC.activeRadius < 3.4028235E+38f && primaryTargetsCenter != Vector2.zero)
				{
					vector2 = (primaryTargetsCenter - cameraTargetC.frameTC.transform.position) * cameraTargetC.secondaryTargetTransMult;
				}
				cpBB cpBB2 = CameraS.TransformBB(cameraTargetC.targetFrame, cameraTargetC.frameTC.transform.position + cameraTargetC.offset + vector2, 1f);
				Vector3 vector3 = cameraTargetC.frameTC.transform.position - cameraTargetC.lastFrameTCPos;
				Vector3 vector4 = cameraTargetC.targetTC.transform.position - cameraTargetC.lastTargetTCPos;
				if (cameraTargetC.frameGrowVelocityMultiplier != Vector2.zero)
				{
					Vector2 vector5;
					vector5..ctor(vector3.x * cameraTargetC.frameGrowVelocityMultiplier.x, vector3.y * cameraTargetC.frameGrowVelocityMultiplier.y);
					cpBB2 = CameraS.ExpandBBToDirection(cpBB2, vector5.x, vector5.y);
				}
				if (cameraTargetC.framePosVelocityMultiplier != Vector2.zero)
				{
					Vector2 vector6;
					vector6..ctor(vector3.x * cameraTargetC.framePosVelocityMultiplier.x, vector3.y * cameraTargetC.framePosVelocityMultiplier.y);
					cpBB2 = CameraS.TransformBB(cpBB2, new Vector3(vector6.x, vector6.y, 0f), 1f);
				}
				if (cameraTargetC.framePeekShiftMultiplier > 0f)
				{
					cameraTargetC.framePeek += vector4 * cameraTargetC.framePeekShiftMultiplier;
					if (Mathf.Abs(cameraTargetC.framePeek.x) > cameraTargetC.framePeekShiftMax.x)
					{
						cameraTargetC.framePeek.x = cameraTargetC.framePeekShiftMax.x * Mathf.Sign(cameraTargetC.framePeek.x);
					}
					if (Mathf.Abs(cameraTargetC.framePeek.y) > cameraTargetC.framePeekShiftMax.y)
					{
						cameraTargetC.framePeek.y = cameraTargetC.framePeekShiftMax.y * Mathf.Sign(cameraTargetC.framePeek.y);
					}
					cpBB2 = CameraS.TransformBB(cpBB2, cameraTargetC.framePeek, 1f);
				}
				if (cameraTargetC.frameScaleVelocityMultiplier > 0f)
				{
					cpBB2 = CameraS.ScaleBB(cpBB2, 1f + vector3.magnitude * cameraTargetC.frameScaleVelocityMultiplier);
				}
				cpBB2 = CameraS.ClampBB(cpBB2, cameraTargetC.frameWorldBounds);
				cameraTargetC.translatedCenter = CameraS.GetBBCenter(cpBB2);
				if (num == 0)
				{
					cpBB = cpBB2;
				}
				else
				{
					cpBB = ChipmunkProWrapper.ucpBBMerge(cpBB, cpBB2);
				}
				if (cameraTargetC.activeRadius == 3.4028235E+38f)
				{
					CameraS.m_combinedTargetsVelocity += vector3;
					num2++;
				}
				cameraTargetC.lastFrameTCPos = cameraTargetC.frameTC.transform.position;
				cameraTargetC.lastTargetTCPos = cameraTargetC.targetTC.transform.position;
				num++;
			}
		}
		if (num2 > 0)
		{
			CameraS.m_combinedTargetsVelocity /= (float)num2;
		}
		return cpBB;
	}

	// Token: 0x060024BF RID: 9407 RVA: 0x0019574E File Offset: 0x00193B4E
	public static void SetGlobalWorldBounds(float _l, float _r, float _t, float _b)
	{
		CameraS.m_globalWorldBounds = new cpBB(_l, _b, _r, _t);
	}

	// Token: 0x060024C0 RID: 9408 RVA: 0x0019575E File Offset: 0x00193B5E
	public static bool isPrimaryTarget(CameraTargetC _target)
	{
		return _target.activeRadius == float.MaxValue;
	}

	// Token: 0x060024C1 RID: 9409 RVA: 0x00195770 File Offset: 0x00193B70
	public static Vector2 GetPrimaryTargetsCenter()
	{
		Vector2 vector = Vector2.zero;
		int num = 0;
		int aliveCount = CameraS.m_cameraTargetComponents.m_aliveCount;
		for (int i = 0; i < aliveCount; i++)
		{
			CameraTargetC cameraTargetC = CameraS.m_cameraTargetComponents.m_array[CameraS.m_cameraTargetComponents.m_aliveIndices[i]];
			if (cameraTargetC.m_active && cameraTargetC.activeRadius == 3.4028235E+38f)
			{
				if (cameraTargetC.translatedCenter != Vector2.zero)
				{
					vector += cameraTargetC.translatedCenter;
				}
				else
				{
					vector += cameraTargetC.frameTC.transform.position;
				}
				num++;
			}
		}
		if (num > 0)
		{
			vector /= (float)num;
		}
		return vector;
	}

	// Token: 0x060024C2 RID: 9410 RVA: 0x00195834 File Offset: 0x00193C34
	private static float GetCameraInterpolationSpeed()
	{
		float num = 0f;
		int aliveCount = CameraS.m_cameraTargetComponents.m_aliveCount;
		for (int i = 0; i < aliveCount; i++)
		{
			CameraTargetC cameraTargetC = CameraS.m_cameraTargetComponents.m_array[CameraS.m_cameraTargetComponents.m_aliveIndices[i]];
			if (cameraTargetC.m_active && cameraTargetC.interpolateSpeed > num)
			{
				num = cameraTargetC.interpolateSpeed;
			}
		}
		if (num == 0f)
		{
			num = 1f;
		}
		return num;
	}

	// Token: 0x060024C3 RID: 9411 RVA: 0x001958AC File Offset: 0x00193CAC
	private static Vector2 GetCameraVelocityAngleChange()
	{
		Vector2 vector = default(Vector2);
		int aliveCount = CameraS.m_cameraTargetComponents.m_aliveCount;
		for (int i = 0; i < aliveCount; i++)
		{
			CameraTargetC cameraTargetC = CameraS.m_cameraTargetComponents.m_array[CameraS.m_cameraTargetComponents.m_aliveIndices[i]];
			if (cameraTargetC.m_active)
			{
				if (cameraTargetC.velAngleChangeMult.x > vector.x)
				{
					vector.x = cameraTargetC.velAngleChangeMult.x;
				}
				if (cameraTargetC.velAngleChangeMult.y > vector.y)
				{
					vector.y = cameraTargetC.velAngleChangeMult.y;
				}
			}
		}
		return vector;
	}

	// Token: 0x060024C4 RID: 9412 RVA: 0x00195954 File Offset: 0x00193D54
	private static Vector2 GetCameraAngleLimit()
	{
		Vector2 vector;
		vector..ctor(999f, 999f);
		int aliveCount = CameraS.m_cameraTargetComponents.m_aliveCount;
		for (int i = 0; i < aliveCount; i++)
		{
			CameraTargetC cameraTargetC = CameraS.m_cameraTargetComponents.m_array[CameraS.m_cameraTargetComponents.m_aliveIndices[i]];
			if (cameraTargetC.m_active)
			{
				if (cameraTargetC.angleLimits.x < vector.x)
				{
					vector.x = cameraTargetC.angleLimits.x;
				}
				if (cameraTargetC.angleLimits.y < vector.y)
				{
					vector.y = cameraTargetC.angleLimits.y;
				}
			}
		}
		return vector;
	}

	// Token: 0x060024C5 RID: 9413 RVA: 0x00195A08 File Offset: 0x00193E08
	private static cpBB InterpolateBBs(cpBB _from, cpBB _to, float _delta)
	{
		return new cpBB
		{
			b = _from.b + (_to.b - _from.b) * _delta,
			l = _from.l + (_to.l - _from.l) * _delta,
			r = _from.r + (_to.r - _from.r) * _delta,
			t = _from.t + (_to.t - _from.t) * _delta
		};
	}

	// Token: 0x060024C6 RID: 9414 RVA: 0x00195AA0 File Offset: 0x00193EA0
	public static void Update()
	{
		if (CameraS.m_storedRenderTexture != null)
		{
			CameraS.m_storedRenderTexture.Update();
		}
		CameraS.m_zoomMultipler *= CameraS.m_zoomNeutralizer;
		Vector3 vector = -CameraS.m_mainCamera.ScreenToWorldPoint(new Vector3((float)Screen.width * 0.5f + 1f, (float)Screen.height * 0.5f, CameraS.m_mainCamera.transform.position.z));
		CameraS.m_mainCameraDistanceMultipler = 1f / (vector + CameraS.m_mainCamera.transform.position).x;
		if (CameraS.m_finalTargetPrefab != null)
		{
			PrefabS.RemoveComponent(CameraS.m_finalTargetPrefab, true);
			CameraS.m_finalTargetPrefab = null;
		}
		if (CameraS.m_updateComponents)
		{
			cpBB cpBB = CameraS.CalculateMergedTargetBB();
			if (!cpBB.isNull())
			{
				if (CameraS.DEBUG)
				{
					CameraS.m_finalTargetPrefab = PrefabS.CreateRect(CameraS.m_mainCameraRotateTC, Vector3.zero, CameraS.GetBBWidth(cpBB), CameraS.GetBBHeight(cpBB), new Color(1f, 0f, 0f, 0.2f), ResourceManager.GetMaterial(RESOURCE.DebugMaterial_Material), CameraS.m_mainCamera);
				}
				float cameraInterpolationSpeed = CameraS.GetCameraInterpolationSpeed();
				CameraS.m_mainCameraFrame = CameraS.InterpolateBBs(CameraS.m_mainCameraFrame, cpBB, cameraInterpolationSpeed);
				float num = (CameraS.m_mainCameraFrame.r - CameraS.m_mainCameraFrame.l) * 0.5f;
				float num2 = (CameraS.m_mainCameraFrame.t - CameraS.m_mainCameraFrame.b) * 0.5f;
				CameraS.m_mainCameraPosition = new Vector3(CameraS.m_mainCameraFrame.l + num, CameraS.m_mainCameraFrame.b + num2, 0f) + CameraS.m_mainCameraPositionOffset;
				float num3 = (float)Screen.width / (float)Screen.height;
				float num4 = -(1f / Mathf.Tan(CameraS.m_mainCamera.fieldOfView * 0.5f * 0.017453292f)) * num2;
				float num5 = -(1f / Mathf.Tan(CameraS.m_mainCamera.fieldOfView * num3 * 0.5f * 0.017453292f)) * num;
				TransformS.SetPosition(CameraS.m_mainCameraRotateTC, CameraS.m_mainCameraPosition);
				CameraS.m_mainCamera.transform.localPosition = Vector3.forward * (Mathf.Min(num4, num5) * (CameraS.m_zoomMultipler + 1f) + CameraS.m_mainCameraZoomOffset);
				Vector2 cameraVelocityAngleChange = CameraS.GetCameraVelocityAngleChange();
				if (cameraVelocityAngleChange != Vector2.zero)
				{
					Vector2 cameraAngleLimit = CameraS.GetCameraAngleLimit();
					Vector3 zero = Vector3.zero;
					zero.y = cameraVelocityAngleChange.y * CameraS.m_combinedTargetsVelocity.x;
					zero.y = ToolBox.limitBetween(zero.y, -cameraAngleLimit.y, cameraAngleLimit.y);
					zero.x = -cameraVelocityAngleChange.x * CameraS.m_combinedTargetsVelocity.y;
					zero.x = ToolBox.limitBetween(zero.x, -cameraAngleLimit.x, cameraAngleLimit.x);
					CameraS.m_mainCameraRotation += (zero - CameraS.m_mainCameraRotation) * cameraInterpolationSpeed;
					TransformS.SetRotation(CameraS.m_mainCameraRotateTC, CameraS.m_mainCameraRotation + CameraS.m_mainCameraRotationOffset);
				}
				else
				{
					CameraS.m_mainCameraRotation += (CameraS.m_mainCameraRotationOffset - CameraS.m_mainCameraRotation) * cameraInterpolationSpeed;
					TransformS.SetRotation(CameraS.m_mainCameraRotateTC, CameraS.m_mainCameraRotation);
				}
				if (CameraS.m_mainCamShakeTime > 0f)
				{
					if (Main.m_gameTicks % CameraS.m_mainCamShakeTickInterval == 0)
					{
						CameraS.m_mainCamCurrentShakeAmount = new Vector3(Random.Range(-CameraS.m_mainCamShakeAmount, CameraS.m_mainCamShakeAmount), Random.Range(-CameraS.m_mainCamShakeAmount, CameraS.m_mainCamShakeAmount), Random.Range(-CameraS.m_mainCamShakeAmount, 0f));
					}
					else
					{
						CameraS.m_mainCamShakeAmount *= 0.8f;
					}
					CameraS.m_mainCamera.transform.localPosition += CameraS.m_mainCamCurrentShakeAmount;
					CameraS.m_mainCamShakeTime -= Main.m_gameDeltaTime;
				}
			}
		}
		CameraS.m_cameraTargetComponents.Update();
	}

	// Token: 0x04002A6E RID: 10862
	public static bool DEBUG = false;

	// Token: 0x04002A6F RID: 10863
	public static DynamicArray<CameraLayer> m_cameraLayers = new DynamicArray<CameraLayer>(16, 0f, 0f, 1f);

	// Token: 0x04002A70 RID: 10864
	public static List<Camera> m_cameras;

	// Token: 0x04002A71 RID: 10865
	private static List<Camera> m_overlayCameras;

	// Token: 0x04002A72 RID: 10866
	private static bool m_debugDraw = false;

	// Token: 0x04002A73 RID: 10867
	public static Camera m_mainCamera;

	// Token: 0x04002A74 RID: 10868
	public static float m_mainCameraDistanceMultipler;

	// Token: 0x04002A75 RID: 10869
	public static float m_mainCameraFov = 40f;

	// Token: 0x04002A76 RID: 10870
	public static float m_mainCameraDistance = 500f;

	// Token: 0x04002A77 RID: 10871
	public static Vector3 m_mainCameraRotation = Vector3.zero;

	// Token: 0x04002A78 RID: 10872
	public static Vector3 m_mainCameraPosition = Vector3.zero;

	// Token: 0x04002A79 RID: 10873
	public static float m_mainCameraNearClip = 30f;

	// Token: 0x04002A7A RID: 10874
	public static float m_mainCameraFarClip = 80000f;

	// Token: 0x04002A7B RID: 10875
	public static cpBB m_mainCameraFrame;

	// Token: 0x04002A7C RID: 10876
	public static RenderTexture m_mainCameraRenderTexture;

	// Token: 0x04002A7D RID: 10877
	public static bool m_mainCameraRenderTextureDirty;

	// Token: 0x04002A7E RID: 10878
	public static int m_mainCameraLayer = 8;

	// Token: 0x04002A7F RID: 10879
	public static int m_mainCameraCullingMask = 1792;

	// Token: 0x04002A80 RID: 10880
	public static TransformC m_mainCameraTC;

	// Token: 0x04002A81 RID: 10881
	public static TransformC m_mainCameraRotateTC;

	// Token: 0x04002A82 RID: 10882
	public static float m_mainCamShakeTime;

	// Token: 0x04002A83 RID: 10883
	public static float m_mainCamShakeAmount;

	// Token: 0x04002A84 RID: 10884
	public static int m_mainCamShakeTickInterval;

	// Token: 0x04002A85 RID: 10885
	private static Vector3 m_mainCamCurrentShakeAmount;

	// Token: 0x04002A86 RID: 10886
	public static Camera m_uiCamera;

	// Token: 0x04002A87 RID: 10887
	public static float m_uiCameraNearClip = 1f;

	// Token: 0x04002A88 RID: 10888
	public static float m_uiCameraFarClip = 1000f;

	// Token: 0x04002A89 RID: 10889
	public static int m_uiCameraLayer = 15;

	// Token: 0x04002A8A RID: 10890
	public static RenderTexture m_renderTexture;

	// Token: 0x04002A8B RID: 10891
	public static StoredRenderTexture m_storedRenderTexture = new StoredRenderTexture();

	// Token: 0x04002A8C RID: 10892
	public static Camera m_renderTextureViewCamera;

	// Token: 0x04002A8D RID: 10893
	public static int m_renderTextureViewCameraLayer = 12;

	// Token: 0x04002A8E RID: 10894
	public static Material m_renderTextureViewMaterial;

	// Token: 0x04002A8F RID: 10895
	public static int m_otherCamerasStartLayer = 16;

	// Token: 0x04002A90 RID: 10896
	public static DynamicArray<CameraTargetC> m_cameraTargetComponents;

	// Token: 0x04002A91 RID: 10897
	public static cpBB m_cameraTargetLimits = new cpBB(float.MinValue, float.MinValue, float.MaxValue, float.MaxValue);

	// Token: 0x04002A92 RID: 10898
	public static Vector3 m_combinedTargetsVelocity;

	// Token: 0x04002A93 RID: 10899
	public static TransformC m_debugTC;

	// Token: 0x04002A94 RID: 10900
	public static TransformC m_debugTC2;

	// Token: 0x04002A95 RID: 10901
	public static int m_screenWidth = 0;

	// Token: 0x04002A96 RID: 10902
	public static int m_screenHeight = 0;

	// Token: 0x04002A97 RID: 10903
	public static bool m_updateComponents = true;

	// Token: 0x04002A98 RID: 10904
	private static Entity cameraRootEntity;

	// Token: 0x04002A99 RID: 10905
	public static GameObject m_renderTextureViewCanvas;

	// Token: 0x04002A9A RID: 10906
	private static BlurOptimizedWOE m_blur;

	// Token: 0x04002A9B RID: 10907
	private static bool m_updateBlur;

	// Token: 0x04002A9C RID: 10908
	private static bool m_blurFadeIn;

	// Token: 0x04002A9D RID: 10909
	private static bool m_blurFadeOut;

	// Token: 0x04002A9E RID: 10910
	private static Camera m_currentCamera;

	// Token: 0x04002A9F RID: 10911
	public static List<Camera> m_blurDisabledCameras;

	// Token: 0x04002AA0 RID: 10912
	private static Camera m_cameraForBlur;

	// Token: 0x04002AA1 RID: 10913
	private static Action m_blurDoneCallback;

	// Token: 0x04002AA2 RID: 10914
	private static bool m_createTex = false;

	// Token: 0x04002AA3 RID: 10915
	private static PrefabC m_finalTargetPrefab;

	// Token: 0x04002AA4 RID: 10916
	public static cpBB m_globalWorldBounds;

	// Token: 0x04002AA5 RID: 10917
	public static float m_zoomMultipler = 1f;

	// Token: 0x04002AA6 RID: 10918
	public static float m_zoomNeutralizer = 0.98f;

	// Token: 0x04002AA7 RID: 10919
	public static Vector3 m_mainCameraRotationOffset = Vector3.zero;

	// Token: 0x04002AA8 RID: 10920
	public static Vector3 m_mainCameraPositionOffset = Vector3.zero;

	// Token: 0x04002AA9 RID: 10921
	public static float m_mainCameraZoomOffset = 0f;
}
