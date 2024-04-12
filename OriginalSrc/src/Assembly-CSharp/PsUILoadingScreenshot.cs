using System;
using UnityEngine;

// Token: 0x0200024D RID: 589
public class PsUILoadingScreenshot : PsUIScreenshot
{
	// Token: 0x060011D2 RID: 4562 RVA: 0x000AF158 File Offset: 0x000AD558
	public PsUILoadingScreenshot(UIComponent _parent, bool _touchable, string _tag, Vector2 _offset, PsGameLoop _minigameInfo, bool _loadImmediately = true, bool _createBorder = true, float _cornerSize = 0.045f, bool _useTutorialTexture = false)
		: base(_parent, _touchable, _tag, _offset, _minigameInfo, _loadImmediately, _createBorder, _cornerSize, _useTutorialTexture)
	{
		this.m_screenTC = TransformS.AddComponent(this.m_TC.p_entity, Vector3.zero);
		TransformS.ParentComponent(this.m_screenTC, this.m_TC);
		TransformS.SetPosition(this.m_screenTC, Vector3.zero);
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x000AF1B8 File Offset: 0x000AD5B8
	public override void DrawHandler(UIComponent _c)
	{
		this.m_screenTC.transform.localScale = Vector3.one;
		_c.m_TC.transform.localScale = Vector3.one;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, false);
		if (this.m_texture != null)
		{
			this.m_material.mainTexture = this.m_texture;
		}
		if (this.m_flickMat == null)
		{
			this.m_flickMat = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material));
			this.m_flickMat.shader = Shader.Find("WOE/Unlit/ColorUnlit");
			this.m_flickMat.mainTexture = ResourceManager.GetTexture(RESOURCE.TVNoisePausePattern_Texture2D);
		}
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.3f, 10, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth * 0.985f, _c.m_actualHeight * 0.985f, _c.m_actualHeight * 0.3f * 0.98f, 10, Vector2.zero);
		UVRect uvrect = new UVRect(0f, 0.55f - _c.m_actualHeight / _c.m_actualWidth / 2f, 1f, _c.m_actualHeight / _c.m_actualWidth);
		for (int i = 0; i < roundedRect.Length; i++)
		{
			roundedRect[i] = Quaternion.Euler(new Vector3(0f, 0f, -1.5f)) * roundedRect[i];
		}
		for (int j = 0; j < roundedRect2.Length; j++)
		{
			roundedRect2[j] = Quaternion.Euler(new Vector3(0f, 0f, -1.5f)) * roundedRect2[j];
		}
		uint num = DebugDraw.HexToUint("#0156b2");
		uint num2 = DebugDraw.HexToUint("#2e8599");
		uint num3 = DebugDraw.HexToUint("#1a64a8");
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_screenTC, Vector3.forward * 1f + Vector3.up * 2.5f, roundedRect, uint.MaxValue, uint.MaxValue, this.m_material, this.m_camera, string.Empty, uvrect);
		TransformS.SetScale(this.m_screenTC, Vector3.zero);
		if (this.m_flickMat != null)
		{
			PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 5f, roundedRect2, DebugDraw.ColorToUInt(Color.black), DebugDraw.ColorToUInt(Color.black), this.m_flickMat, this.m_camera, string.Empty, uvrect);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, roundedRect2, (float)Screen.height * 0.01f, DebugDraw.UIntToColor(num), DebugDraw.UIntToColor(num2), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f + Vector3.up * 2.5f, roundedRect, (float)Screen.height * 0.008f, DebugDraw.HexToColor("#41acee"), DebugDraw.HexToColor("#86d9f9"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f + Vector3.up * 2.5f, roundedRect, (float)Screen.height * 0.03f, new Color(1f, 1f, 1f, 0.3f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Inside, true);
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x000AF590 File Offset: 0x000AD990
	protected override void HandleScreenshot(byte[] _bytes)
	{
		base.HandleScreenshot(_bytes);
		this.m_material.mainTexture = this.m_texture;
		TweenC tweenC = TweenS.AddTransformTween(this.m_screenTC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.zero, Vector3.one, 0.3f, 0f, false, true);
		TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
		{
			if (Main.m_currentGame.m_sceneManager.m_loadingScene is PsRacingLoadingScene)
			{
				(Main.m_currentGame.m_sceneManager.m_loadingScene as PsRacingLoadingScene).m_screenShotTweened = true;
			}
		});
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x000AF5FC File Offset: 0x000AD9FC
	public override void Step()
	{
		if (this.m_flickMat != null)
		{
			Vector2 mainTextureOffset = this.m_flickMat.mainTextureOffset;
			mainTextureOffset.y += Random.Range(-0.2f, 0.2f);
			this.m_flickMat.mainTextureOffset = mainTextureOffset;
		}
		base.Step();
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x000AF655 File Offset: 0x000ADA55
	public override void Destroy()
	{
		if (this.m_flickMat != null)
		{
			Object.Destroy(this.m_flickMat);
		}
		this.m_flickMat = null;
		base.Destroy();
	}

	// Token: 0x040014CF RID: 5327
	private Material m_flickMat;

	// Token: 0x040014D0 RID: 5328
	public TransformC m_screenTC;
}
