using System;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class PsPlanetLevelNode : PsPlanetNode
{
	// Token: 0x060008AF RID: 2223 RVA: 0x0005F1C6 File Offset: 0x0005D5C6
	public PsPlanetLevelNode(PsGameLoop _loop, bool _tutorial = false)
		: base(_loop, _tutorial)
	{
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0005F1D0 File Offset: 0x0005D5D0
	public override void CreatePrefab()
	{
		if (this.m_prefab != null)
		{
			PrefabS.RemoveComponent(this.m_prefab, true);
			this.m_prefab = null;
		}
		GameObject gameObject;
		if (this.m_loop.m_path != this.m_loop.m_planet.GetMainPath())
		{
			if (this.m_loop.m_minigameSearchParametres.m_difficulty == PsGameDifficulty.Impossible)
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.PathButtonInsanePrefab_GameObject);
			}
			else if (this.m_loop.m_minigameSearchParametres.m_gameMode == PsGameMode.StarCollect)
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.PathButtonFindStarsPrefab_GameObject);
			}
			else
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.PathButtonRacePrefab_GameObject);
			}
		}
		else
		{
			gameObject = ResourceManager.GetGameObject(RESOURCE.PathButtonLargePrefab_GameObject);
		}
		this.m_prefab = PrefabS.AddComponent(this.m_tc, Vector3.zero, gameObject, "LevelNode", true, false);
		PrefabS.SetCamera(this.m_prefab.p_gameObject, this.m_loop.m_planet.m_planetCamera);
		this.m_checkpointEffect = this.m_prefab.p_gameObject.GetComponent<EffectCheckpoint>();
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0005F2D8 File Offset: 0x0005D6D8
	public override string GetTransformName()
	{
		if (this.m_loop.m_minigameSearchParametres.m_gameMode == PsGameMode.StarCollect)
		{
			return "PlanetNodeStarCollect" + this.m_loop.m_nodeId;
		}
		return "PlanetNode" + this.m_loop.m_nodeId;
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0005F330 File Offset: 0x0005D730
	public override void SetClaimed()
	{
		base.SetClaimed();
		this.CreateUI();
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0005F340 File Offset: 0x0005D740
	public override void CreateUI()
	{
		base.CreateUI();
		if (!this.m_numberCreated || !this.m_uiCreated)
		{
			if (this.m_loop.m_path.m_startNodeId == 0 && !this.m_numberCreated)
			{
				this.m_numberText = TextMeshS.AddComponent(this.m_numberTC, Vector3.zero, PsFontManager.GetFont(PsFonts.KGSecondChances), 2f, 2f, 34f, Align.Center, Align.Middle, this.m_loop.m_planet.m_planetCamera, this.m_loop.m_nodeId.ToString());
				TextMeshS.SetText(this.m_numberText, this.m_loop.m_levelNumber.ToString(), true);
				this.m_numberText.m_textMesh.characterSize = 1f;
				this.m_numberText.m_renderer.material.shader = Shader.Find("Framework/FontShader");
				this.m_fontColor = DebugDraw.HexToColor("#2b8698");
				this.m_numberText.m_renderer.material.color = this.m_fontColor;
				this.m_numberCreated = true;
				this.m_updateUI = true;
			}
			if ((this.m_loop.m_unlocked && !this.m_inactiveSet && !this.m_uiCreated) || (!this.m_loop.m_unlocked && this.m_loop.m_nodeId == this.m_loop.m_path.m_currentNodeId && !this.m_inactiveSet && !this.m_uiCreated && this.m_loop.m_path.GetPathType() == PsPlanetPathType.main))
			{
				Frame frame = this.m_planetNodeSpriteSheet.m_atlas.GetFrame("menu_map_pieces_small_" + this.m_loop.m_scoreOld, null);
				SpriteC spriteC = SpriteS.AddComponent(this.m_centerStarTC, frame, this.m_planetNodeSpriteSheet);
				SpriteS.SetDimensions(spriteC, 4f * (frame.width / frame.height), 4f);
				SpriteS.SetColor(spriteC, Color.white);
				this.m_uiCreated = true;
				this.m_updateUI = true;
			}
			if (this.m_inactiveSet)
			{
				this.m_inactiveSet = false;
			}
			if (this.m_updateUI)
			{
				this.Update();
			}
		}
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0005F585 File Offset: 0x0005D985
	public override void SetActive()
	{
		base.SetActive();
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x0005F58D File Offset: 0x0005D98D
	public override void SetInactive()
	{
		base.SetInactive();
		this.m_inactiveSet = true;
		this.CreateUI();
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0005F5A2 File Offset: 0x0005D9A2
	public override void Activate()
	{
		base.Activate();
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0005F5AC File Offset: 0x0005D9AC
	private void CreateSpeechBubble()
	{
		if (this.m_loop == null)
		{
			return;
		}
		if (this.m_loop.m_minigameMetaData == null && !this.m_loop.m_loadingMetaData)
		{
			this.m_loop.LoadMinigameMetaData(new Action(this.CreateSpeechBubble));
			Debug.LogWarning("start loading minigame metadata, adding callback");
			return;
		}
		if (this.m_loop.m_minigameMetaData == null)
		{
			PsGameLoop loop = this.m_loop;
			loop.m_loadMetadataCallback = (Action)Delegate.Combine(loop.m_loadMetadataCallback, new Action(this.CreateSpeechBubble));
			Debug.LogWarning("still loading minigame metadata, adding callback");
			return;
		}
		Camera planetCamera = PsPlanetManager.GetCurrentPlanet().m_planetCamera;
		Frame frame = this.m_planetNodeSpriteSheet.m_atlas.GetFrame("menu_gamebanner_mini", null);
		float num = 0.4f;
		float num2 = 9f;
		float num3 = frame.width * num / frame.height * num2 * 0.94f;
		this.m_bubbleEntity = EntityManager.AddEntity();
		this.m_bubbleScaleTC = TransformS.AddComponent(this.m_bubbleEntity, "bubbleScaleTC");
		TransformS.ParentComponent(this.m_bubbleScaleTC, this.m_unlockTC, new Vector3(0f, -10f, 0f));
		TransformS.SetRotation(this.m_bubbleScaleTC, Vector3.zero);
		TransformC transformC = TransformS.AddComponent(this.m_bubbleEntity, "bubbleTC");
		TransformS.ParentComponent(transformC, this.m_bubbleScaleTC);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(num3, num2 * (1f - num), 0.1f * num2, 8, Vector2.zero);
		Vector2[] array = new Vector2[roundedRect.Length + 3];
		Array.Copy(roundedRect, 0, array, 0, 8);
		array[8] = new Vector2(roundedRect[8].x + 3f, roundedRect[8].y);
		array[9] = new Vector2(roundedRect[8].x + 0.7f, roundedRect[8].y - 2f);
		array[10] = new Vector2(roundedRect[8].x + 1f, roundedRect[8].y);
		Array.Copy(roundedRect, 8, array, 11, roundedRect.Length - 8);
		PrefabS.CreatePathPrefabComponentFromVectorArray(transformC, new Vector3(0.15f, -0.15f, 0.3f), array, 0.7f, new Color(0f, 0f, 0f, 0.3f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), planetCamera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(transformC, Vector3.forward * 0.1f, array, 0.25f, DebugDraw.HexToColor("#fffec6"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), planetCamera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(transformC, Vector3.zero, array, DebugDraw.HexToUint("#fffec6"), DebugDraw.HexToUint("#fffec6"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), planetCamera, string.Empty, null);
		SpriteC spriteC = SpriteS.AddComponent(transformC, frame, this.m_planetNodeSpriteSheet);
		SpriteS.SetDimensions(spriteC, num2 * num * (frame.width / frame.height), num2 * num);
		SpriteS.SetColor(spriteC, Color.white);
		SpriteS.SetOffset(spriteC, new Vector3(num3 / 2f * 0.02f, num2 * (1f - num) / 2f + spriteC.height / 2f * 0.5f, -0.2f), 0f);
		TextMeshC textMeshC = TextMeshS.AddComponent(transformC, new Vector3(-num3 / 2f * 0.96f, 5.1f, -0.4f), PsFontManager.GetFont(PsFonts.KGSecondChances), spriteC.width, spriteC.height, 22f, Align.Left, Align.Top, planetCamera, string.Empty);
		TextMeshS.SetText(textMeshC, this.m_loop.GetName(), false);
		textMeshC.m_textMesh.characterSize = 1f;
		textMeshC.m_renderer.material.shader = Shader.Find("Framework/FontShaderFg");
		textMeshC.m_textMesh.GetComponent<Renderer>().material.SetColor("_Color", DebugDraw.HexToColor("ffffff"));
		textMeshC.m_textMesh.fontStyle = 2;
		TextMeshC textMeshC2 = TextMeshS.AddComponent(transformC, new Vector3(-num3 / 2f * 0.96f + 0.06f, 5.02f, -0.3f), PsFontManager.GetFont(PsFonts.KGSecondChances), spriteC.width, spriteC.height, 22f, Align.Left, Align.Top, planetCamera, string.Empty);
		TextMeshS.SetText(textMeshC2, this.m_loop.GetName(), false);
		textMeshC2.m_textMesh.characterSize = 1f;
		textMeshC2.m_renderer.material.shader = Shader.Find("Framework/FontShaderFg");
		textMeshC2.m_textMesh.GetComponent<Renderer>().material.SetColor("_Color", DebugDraw.HexToColor("000000"));
		textMeshC2.m_textMesh.fontStyle = 2;
		Frame frame2 = null;
		switch (this.m_loop.GetDifficulty())
		{
		case PsGameDifficulty.Easy:
			frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_difficulty_casual", null);
			break;
		case PsGameDifficulty.Tricky:
			frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_difficulty_tricky", null);
			break;
		case PsGameDifficulty.Insane:
			frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_difficulty_hardcore", null);
			break;
		case PsGameDifficulty.Impossible:
			frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_difficulty_impossible", null);
			break;
		}
		if (frame2 != null)
		{
			SpriteC spriteC2 = SpriteS.AddComponent(transformC, frame2, this.m_planetNodeSpriteSheet);
			SpriteS.SetDimensions(spriteC2, num2 * 0.5f * (frame2.width / frame2.height), num2 * 0.5f);
			SpriteS.SetColor(spriteC2, Color.white);
			SpriteS.SetOffset(spriteC2, new Vector3(num3 / 2f * 0.9f, num2 * (1f - num) / 2f + spriteC.height / 2f * 0.5f, -0.3f), 0f);
		}
		this.m_profileImage = new PsUIProfileImage(null, false, string.Empty, this.m_loop.GetFacebookId(), this.m_loop.GetGamecenterId(), -1, true, true, false, 0.1f, 0.06f, "fff9e6", null, false, true);
		this.m_profileImage.SetSize(num2 * (1f - num) * 0.79f, num2 * (1f - num) * 0.79f, RelativeTo.World);
		this.m_profileImage.SetCamera(planetCamera, true, false);
		this.m_profileImage.Update();
		TransformS.ParentComponent(this.m_profileImage.m_TC, transformC);
		TransformS.SetPosition(this.m_profileImage.m_TC, new Vector3(-num3 / 2f + this.m_profileImage.m_actualWidth / 2f * 1.04f, -num2 * 0.03f, -0.1f));
		TransformS.SetRotation(this.m_profileImage.m_TC, Vector3.forward * 4f);
		string text = this.m_loop.GetDescription();
		TextMeshC textMeshC3 = TextMeshS.AddComponent(transformC, new Vector3(-num3 / 3.7f, 1.4f, -0.1f), PsFontManager.GetFont(PsFonts.HurmeRegular), 300f, spriteC.height, 15f, Align.Left, Align.Top, planetCamera, string.Empty);
		text = TextMeshS.WrapText(textMeshC3, text, num3 * 7.5f, 2, true);
		TextMeshS.SetText(textMeshC3, text, false);
		textMeshC3.m_textMesh.characterSize = 1f;
		textMeshC3.m_renderer.material.shader = Shader.Find("Framework/FontShaderFg");
		textMeshC3.m_textMesh.GetComponent<Renderer>().material.SetColor("_Color", DebugDraw.HexToColor("000000"));
		SpriteS.ConvertSpritesToPrefabComponent(transformC, true);
		transformC.forceRotation = true;
		TransformS.SetRotation(transformC, planetCamera.transform.rotation.eulerAngles);
		TransformS.SetPosition(transformC, new Vector3(10.5f, 8f, 0f));
		TransformS.SetScale(this.m_bubbleScaleTC, Vector3.zero);
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0005FDD0 File Offset: 0x0005E1D0
	private void BubbleDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.1f * _c.m_actualHeight, 8, Vector2.zero);
		Vector2[] array = new Vector2[roundedRect.Length + 2];
		Array.Copy(roundedRect, 0, array, 0, 8);
		array[8] = new Vector2(roundedRect[8].x + 2f, roundedRect[8].y);
		array[9] = new Vector2(roundedRect[8].x + 1f, roundedRect[8].y - 2f);
		Array.Copy(roundedRect, 8, array, 10, roundedRect.Length - 8);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(0.2f, -0.2f, 0.3f), array, 0.4f, DebugDraw.HexToColor("#666666"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.2f, -0.2f, 0.2f), array, DebugDraw.HexToUint("#666666"), DebugDraw.HexToUint("#666666"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0.1f, array, 0.25f, DebugDraw.HexToColor("#fffec6"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, DebugDraw.HexToUint("#fffec6"), DebugDraw.HexToUint("#fffec6"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0005FFA4 File Offset: 0x0005E3A4
	public override void Update()
	{
		base.Update();
		if (this.m_bubbleScaleTC != null)
		{
			float num = Vector3.Dot(this.m_tc.transform.position.normalized, Vector3.up);
			TransformS.SetPosition(this.m_unlockTC, new Vector3(0f, 14f + -6f * (1f - num), -10f));
			if (num > 0.9f || this.m_hideUI)
			{
				this.m_bubbleScaleTC.transform.localScale = Vector3.Lerp(this.m_bubbleScaleTC.transform.localScale, Vector3.zero, 0.3f);
			}
			else
			{
				this.m_bubbleScaleTC.transform.localScale = Vector3.Lerp(this.m_bubbleScaleTC.transform.localScale, Vector3.one, 0.3f);
			}
		}
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0006008C File Offset: 0x0005E48C
	public override void Reveal()
	{
		base.Reveal();
		if (this.m_loop != null)
		{
			int num = this.m_loop.m_nodeId - this.m_loop.m_path.GetPrevBlockId(this.m_loop.m_nodeId);
			if (this.m_loop.m_path.m_startNodeId == 0)
			{
				SoundS.PlaySingleShotWithParameter("/Metagame/LevelAppear", Vector3.zero, "NodeNumber", (float)num, 1f);
			}
		}
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00060102 File Offset: 0x0005E502
	public override void Activate2(TimerC _c)
	{
		base.Activate2(_c);
		if (this.m_loop.m_path.m_startNodeId == 0)
		{
			SoundS.PlaySingleShot("/Metagame/LevelHighlight", Vector3.zero, 1f);
		}
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00060134 File Offset: 0x0005E534
	public override void Destroy()
	{
		if (this.m_bubbleEntity != null)
		{
			EntityManager.RemoveEntity(this.m_bubbleEntity);
			this.m_bubbleEntity = null;
		}
		if (this.m_profileImage != null)
		{
			this.m_profileImage.Destroy();
			this.m_profileImage = null;
		}
		base.Destroy();
	}

	// Token: 0x0400082D RID: 2093
	private UITextbox m_bubbleTextBox;

	// Token: 0x0400082E RID: 2094
	private UIVerticalList m_imageArea;

	// Token: 0x0400082F RID: 2095
	private PsUIProfileImage m_profileImage;

	// Token: 0x04000830 RID: 2096
	private TransformC m_bubbleScaleTC;

	// Token: 0x04000831 RID: 2097
	private bool m_inactiveSet;

	// Token: 0x04000832 RID: 2098
	private bool m_numberCreated;

	// Token: 0x04000833 RID: 2099
	private bool m_uiCreated;

	// Token: 0x04000834 RID: 2100
	protected bool m_updateUI;

	// Token: 0x04000835 RID: 2101
	public bool m_createFlyingNode;

	// Token: 0x04000836 RID: 2102
	private Entity m_bubbleEntity;
}
