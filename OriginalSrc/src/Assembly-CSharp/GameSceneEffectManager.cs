using System;
using UnityEngine;

// Token: 0x0200020B RID: 523
public static class GameSceneEffectManager
{
	// Token: 0x06000F31 RID: 3889 RVA: 0x000908B8 File Offset: 0x0008ECB8
	public static void Initialize(Camera _camera)
	{
		GameSceneEffectManager.m_camera = _camera;
		GameSceneEffectManager.m_tvTube = _camera.gameObject.AddComponent<OLDTVTube>();
		GameSceneEffectManager.m_tvTube.tvMaterialTube = ResourceManager.GetMaterial(RESOURCE.RetroTVTube_Material);
		GameSceneEffectManager.m_tvTube.tvMaterialTube.SetTexture("_Mask", ResourceManager.GetTexture(RESOURCE.RoundedCornerMask_Texture2D));
		GameSceneEffectManager.m_tvTube.radialDistortion = 0.5f;
		GameSceneEffectManager.m_tvTube.tvMaterialTube.SetTexture("_ChromaticAberrationTex", ResourceManager.GetTexture(RESOURCE.TVAberration_Texture2D));
		GameSceneEffectManager.m_tvTube.tvMaterialTube.SetTexture("_NoiseTex", ResourceManager.GetTexture(RESOURCE.TVNoise_Texture2D));
		GameSceneEffectManager.m_tvTube.tvMaterialTube.SetTexture("_StaticTex", ResourceManager.GetTexture(RESOURCE.TVPixel_Texture2D));
		GameSceneEffectManager.m_tvTube.tvMaterialTube.SetTexture("_StaticMask", ResourceManager.GetTexture(RESOURCE.TVNoisePausePattern_Texture2D));
		GameSceneEffectManager.m_tvTube.tvMaterialTube.SetFloat("_ScreenHorizontal", 0f);
		GameSceneEffectManager.m_tvTube.noiseMagnetude = 0.1f;
		GameSceneEffectManager.m_tvTube.chromaticAberrationMagnetude = 0.1f;
		GameSceneEffectManager.m_tvTube.staticMagnetude = 0.25f;
		GameSceneEffectManager.m_tvTube.screenSaturation = 1f;
		GameSceneEffectManager.m_tvTube.enabled = false;
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x000909F4 File Offset: 0x0008EDF4
	public static void StartMusic(string _sound, bool _signalNoisePlaying)
	{
		if (!PsState.m_muteMusic)
		{
			GameSceneEffectManager.RemoveMusic();
			TransformC transformC = EntityManager.AddEntityWithTC();
			GameSceneEffectManager.m_music = SoundS.AddComponent(transformC, _sound, 1f, true);
			SoundS.PlaySound(GameSceneEffectManager.m_music, true);
		}
		GameSceneEffectManager.m_signalNoiseMusicPlaying = _signalNoisePlaying;
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x00090A39 File Offset: 0x0008EE39
	public static void RemoveMusic()
	{
		if (GameSceneEffectManager.m_music != null)
		{
			EntityManager.RemoveEntity(GameSceneEffectManager.m_music.p_entity);
			GameSceneEffectManager.m_music = null;
		}
		GameSceneEffectManager.m_signalNoiseMusicPlaying = false;
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x00090A60 File Offset: 0x0008EE60
	public static void CreateTVChannels(string _materialResource, int _numberOfImagesInChannelStrip)
	{
		GameSceneEffectManager.m_numberOfImagesInChannelStrip = _numberOfImagesInChannelStrip;
		GameSceneEffectManager.m_flickMat = ResourceManager.GetMaterial(_materialResource);
		Vector2[] rect = DebugDraw.GetRect(134.4f, 89.6f, Vector2.zero);
		UVRect uvrect = new UVRect(0f, 0f, 1f / (float)GameSceneEffectManager.m_numberOfImagesInChannelStrip, 1f);
		GameSceneEffectManager.m_utilityEntity = EntityManager.AddEntity();
		GameSceneEffectManager.m_utilityEntity.m_persistent = true;
		TransformC transformC = TransformS.AddComponent(GameSceneEffectManager.m_utilityEntity);
		transformC.transform.parent = GameSceneEffectManager.m_camera.transform;
		transformC.transform.localPosition = Vector3.forward * 100f;
		transformC.transform.localRotation = Quaternion.identity;
		GameSceneEffectManager.m_flickPC = PrefabS.CreateFlatPrefabComponentsFromVectorArray(transformC, Vector3.zero, rect, DebugDraw.ColorToUInt(Color.white), DebugDraw.ColorToUInt(Color.white), GameSceneEffectManager.m_flickMat, GameSceneEffectManager.m_camera, string.Empty, uvrect);
		GameSceneEffectManager.m_flickPC.p_gameObject.layer = 10;
		GameSceneEffectManager.DrawMainLayerOn();
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x00090B5E File Offset: 0x0008EF5E
	public static void DrawMainLayerOn()
	{
		GameSceneEffectManager.m_camera.cullingMask |= 256;
		GameSceneEffectManager.m_camera.cullingMask |= 512;
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x00090B8C File Offset: 0x0008EF8C
	public static void DrawMainLayerOff()
	{
		GameSceneEffectManager.m_camera.cullingMask &= -257;
		GameSceneEffectManager.m_camera.cullingMask &= -513;
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00090BBA File Offset: 0x0008EFBA
	public static void Enable()
	{
		GameSceneEffectManager.m_tvTube.enabled = true;
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x00090BC7 File Offset: 0x0008EFC7
	public static void Disable()
	{
		GameSceneEffectManager.m_tvTube.enabled = false;
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x00090BD4 File Offset: 0x0008EFD4
	public static void Destroy()
	{
		Object.Destroy(GameSceneEffectManager.m_tvTube);
		GameSceneEffectManager.m_tvTube = null;
		GameSceneEffectManager.m_flickMat = null;
		GameSceneEffectManager.m_flickPC = null;
		GameSceneEffectManager.DestroyCrew();
		EntityManager.RemoveEntity(GameSceneEffectManager.m_utilityEntity);
		GameSceneEffectManager.m_utilityEntity = null;
		GameSceneEffectManager.RemoveMusic();
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x00090C0C File Offset: 0x0008F00C
	public static void SetChannel(int _channel)
	{
		if (_channel == -1)
		{
			GameSceneEffectManager.DrawMainLayerOn();
			GameSceneEffectManager.m_flickPC.p_gameObject.GetComponent<Renderer>().enabled = false;
		}
		else
		{
			GameSceneEffectManager.DrawMainLayerOff();
			GameSceneEffectManager.m_flickPC.p_gameObject.GetComponent<Renderer>().enabled = true;
		}
		Vector2 textureOffset = GameSceneEffectManager.m_flickMat.GetTextureOffset("_MainTex");
		GameSceneEffectManager.m_flickMat.SetTextureOffset("_MainTex", new Vector2((float)_channel * (1f / (float)GameSceneEffectManager.m_numberOfImagesInChannelStrip), textureOffset.y));
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x00090C93 File Offset: 0x0008F093
	public static void SetChannelRoll(bool _on)
	{
		if (_on)
		{
			GameSceneEffectManager.m_channelRollSpeed = -0.1f;
		}
		else
		{
			GameSceneEffectManager.m_channelRollStop = 0f;
		}
		GameSceneEffectManager.m_channelRoll = _on;
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x00090CBA File Offset: 0x0008F0BA
	public static void SetEffects(bool _on)
	{
		GameSceneEffectManager.m_effectsOn = _on;
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00090CC4 File Offset: 0x0008F0C4
	public static void Update()
	{
		Vector2 textureOffset = GameSceneEffectManager.m_flickMat.GetTextureOffset("_MainTex");
		Vector2 textureOffset2 = GameSceneEffectManager.m_tvTube.tvMaterialTube.GetTextureOffset("_StaticMask");
		if (!GameSceneEffectManager.m_channelRoll)
		{
			GameSceneEffectManager.m_channelRollSpeed = (GameSceneEffectManager.m_channelRollStop - textureOffset.y) * 0.1f;
		}
		GameSceneEffectManager.m_flickMat.SetTextureOffset("_MainTex", new Vector2(textureOffset.x, ToolBox.getRolledValue(textureOffset.y + GameSceneEffectManager.m_channelRollSpeed, 0f, 1f)));
		GameSceneEffectManager.m_staticRollSpeed = 0.005f + Mathf.Sin(Main.m_resettingGameTime * Mathf.Sin(Main.m_resettingGameTime * 0.0618f)) * 0.01f;
		GameSceneEffectManager.m_tvTube.tvMaterialTube.SetTextureOffset("_StaticMask", new Vector2(textureOffset2.x, ToolBox.getRolledValue(textureOffset2.y + GameSceneEffectManager.m_staticRollSpeed, 0f, 1f)));
		if (GameSceneEffectManager.m_effectsOn)
		{
			GameSceneEffectManager.m_effectMultipler += (1f - GameSceneEffectManager.m_effectMultipler) * 0.05f;
		}
		else
		{
			GameSceneEffectManager.m_effectMultipler *= 0.95f;
		}
		GameSceneEffectManager.m_tvTube.noiseMagnetude = 0.1f * GameSceneEffectManager.m_effectMultipler + 0.1f;
		GameSceneEffectManager.m_tvTube.chromaticAberrationMagnetude = 0.1f * GameSceneEffectManager.m_effectMultipler + 0.025f;
		GameSceneEffectManager.m_tvTube.staticMagnetude = 0.125f * GameSceneEffectManager.m_effectMultipler + 0.0125f;
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x00090E43 File Offset: 0x0008F243
	public static void DestroyCrew()
	{
		if (GameSceneEffectManager.m_crew != null)
		{
			GameSceneEffectManager.m_crew.Destroy();
			GameSceneEffectManager.m_crew = null;
		}
		if (GameSceneEffectManager.m_bubble != null)
		{
			GameSceneEffectManager.m_bubble.Destroy();
			GameSceneEffectManager.m_bubble = null;
		}
		GameSceneEffectManager.DrawMainLayerOn();
	}

	// Token: 0x04001208 RID: 4616
	private static Camera m_camera;

	// Token: 0x04001209 RID: 4617
	public static OLDTVTube m_tvTube;

	// Token: 0x0400120A RID: 4618
	private static Material m_flickMat;

	// Token: 0x0400120B RID: 4619
	private static PrefabC m_flickPC;

	// Token: 0x0400120C RID: 4620
	private static int m_numberOfImagesInChannelStrip;

	// Token: 0x0400120D RID: 4621
	private static bool m_channelRoll = true;

	// Token: 0x0400120E RID: 4622
	private static float m_channelRollSpeed = 0.005f;

	// Token: 0x0400120F RID: 4623
	private static float m_channelRollStop = 1f;

	// Token: 0x04001210 RID: 4624
	private static float m_staticRollSpeed = 0.01f;

	// Token: 0x04001211 RID: 4625
	private static float m_noisePeak;

	// Token: 0x04001212 RID: 4626
	private static float m_colorSaturation;

	// Token: 0x04001213 RID: 4627
	private static float m_colorShiftPeak;

	// Token: 0x04001214 RID: 4628
	private static float m_effectMultipler = 1f;

	// Token: 0x04001215 RID: 4629
	private static bool m_effectsOn = true;

	// Token: 0x04001216 RID: 4630
	private static bool m_saturationRoll = true;

	// Token: 0x04001217 RID: 4631
	public static Entity m_utilityEntity;

	// Token: 0x04001218 RID: 4632
	public static SoundC m_music;

	// Token: 0x04001219 RID: 4633
	public static Action m_endSequenceCallback;

	// Token: 0x0400121A RID: 4634
	public static PsDialogue m_dialogue;

	// Token: 0x0400121B RID: 4635
	public static PsUICrewMemberBase m_crew;

	// Token: 0x0400121C RID: 4636
	public static PsUIFixedDialogueSpeechBubble m_bubble;

	// Token: 0x0400121D RID: 4637
	public static bool m_signalNoiseMusicPlaying;
}
