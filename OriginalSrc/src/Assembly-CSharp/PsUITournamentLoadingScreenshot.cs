using System;
using UnityEngine;

// Token: 0x0200025E RID: 606
public class PsUITournamentLoadingScreenshot : PsUIScreenshot
{
	// Token: 0x06001237 RID: 4663 RVA: 0x000B3E88 File Offset: 0x000B2288
	public PsUITournamentLoadingScreenshot(UIComponent _parent, bool _touchable, string _tag, Vector2 _offset, PsGameLoop _minigameInfo, bool _loadImmediately = true, bool _createBorder = true, float _cornerSize = 0.045f, bool _useTutorialTexture = false)
		: base(_parent, _touchable, _tag, _offset, _minigameInfo, _loadImmediately, _createBorder, _cornerSize, _useTutorialTexture)
	{
		this.m_screenTC = TransformS.AddComponent(this.m_TC.p_entity, Vector3.zero);
		TransformS.ParentComponent(this.m_screenTC, this.m_TC);
		TransformS.SetPosition(this.m_screenTC, Vector3.zero);
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x000B3EE8 File Offset: 0x000B22E8
	public override void DrawHandler(UIComponent _c)
	{
		float num = _c.m_actualWidth * 0.095f;
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
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Vector2[] rect2 = DebugDraw.GetRect(_c.m_actualWidth * 0.985f, _c.m_actualHeight * 0.985f, Vector2.zero);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.02f, 15, Vector2.zero);
		UVRect uvrect = new UVRect(0f, 0.55f - _c.m_actualHeight / _c.m_actualWidth / 2f, 1f, _c.m_actualHeight / _c.m_actualWidth);
		Vector2[] array = rect;
		int num2 = 1;
		array[num2].x = array[num2].x + num;
		Vector2[] array2 = rect;
		int num3 = 3;
		array2[num3].x = array2[num3].x - num;
		Vector2[] array3 = rect2;
		int num4 = 1;
		array3[num4].x = array3[num4].x + num;
		Vector2[] array4 = rect2;
		int num5 = 3;
		array4[num5].x = array4[num5].x - num;
		for (int i = 0; i < roundedRect.Length - 1; i++)
		{
			Vector2 vector = roundedRect[i];
			if (vector.x < 0f && vector.y < 0f)
			{
				Vector2[] array5 = roundedRect;
				int num6 = i;
				array5[num6].x = array5[num6].x + num;
			}
			else if (vector.x > 0f && vector.y > 0f)
			{
				Vector2[] array6 = roundedRect;
				int num7 = i;
				array6[num7].x = array6[num7].x - num;
			}
		}
		uint num8 = DebugDraw.HexToUint("#BB0712");
		uint num9 = DebugDraw.HexToUint("#AB0811");
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_screenTC, Vector3.forward * 1f + Vector3.up * 2.5f, rect, uint.MaxValue, uint.MaxValue, this.m_material, this.m_camera, string.Empty, uvrect);
		TransformS.SetScale(this.m_screenTC, Vector3.zero);
		if (this.m_flickMat != null)
		{
			PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 5f, rect2, DebugDraw.ColorToUInt(Color.black), DebugDraw.ColorToUInt(Color.black), this.m_flickMat, this.m_camera, string.Empty, uvrect);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, rect2, (float)Screen.height * 0.01f, DebugDraw.UIntToColor(num8), DebugDraw.UIntToColor(num9), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		}
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f + Vector3.up * 2.5f, roundedRect, (float)Screen.height * 0.009f, DebugDraw.HexToColor("#BB0712"), DebugDraw.HexToColor("#AB0811"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f + Vector3.up * 2.5f, rect, (float)Screen.height * 0.03f, new Color(0f, 0f, 0f, 1f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Inside, true);
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x000B4304 File Offset: 0x000B2704
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

	// Token: 0x0600123A RID: 4666 RVA: 0x000B4370 File Offset: 0x000B2770
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

	// Token: 0x0600123B RID: 4667 RVA: 0x000B43C9 File Offset: 0x000B27C9
	public override void Destroy()
	{
		if (this.m_flickMat != null)
		{
			Object.Destroy(this.m_flickMat);
		}
		this.m_flickMat = null;
		base.Destroy();
	}

	// Token: 0x04001567 RID: 5479
	private Material m_flickMat;

	// Token: 0x04001568 RID: 5480
	public TransformC m_screenTC;
}
