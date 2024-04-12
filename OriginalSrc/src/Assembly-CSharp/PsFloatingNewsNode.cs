using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class PsFloatingNewsNode : PsFloatingPlanetNode
{
	// Token: 0x06000881 RID: 2177 RVA: 0x0005E980 File Offset: 0x0005CD80
	public PsFloatingNewsNode(PsTimedEventLoop _loop, bool _tutorial = false, bool _hasTimer = false)
		: base(_loop, _tutorial)
	{
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0005E98A File Offset: 0x0005CD8A
	public override void CreateBase()
	{
		base.CreateBase();
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0005E992 File Offset: 0x0005CD92
	public override void SetPrefabName()
	{
		this.m_prefabName = this.GetPrefabByPopupType((this.m_loop as PsGameLoopNews).m_eventMessage.eventType);
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0005E9B8 File Offset: 0x0005CDB8
	private string GetPrefabByPopupType(string _type)
	{
		if (_type != null)
		{
			if (_type == "CreatorChallenge")
			{
				return "FloatingBuilderHatPrefab";
			}
			if (_type == "Gift")
			{
				return "FloatingWinterPresentPrefab";
			}
			if (_type == "Tournament")
			{
				return "FloatingPlatformTournamentPrefab";
			}
		}
		return "FloatingSatellitePrefab";
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x0005EA18 File Offset: 0x0005CE18
	public override void CustomizeGameobject()
	{
		int messageId = (this.m_loop as PsGameLoopNews).m_eventMessage.messageId;
		VisualsFloatingWinterPresent component = this.m_prefab.p_gameObject.GetComponent<VisualsFloatingWinterPresent>();
		if (component != null)
		{
			if ((this.m_loop as PsGameLoopNews).m_eventMessage.giftContent != null && (this.m_loop as PsGameLoopNews).m_eventMessage.giftContent.texture != -1)
			{
				component.SetPresentColor((this.m_loop as PsGameLoopNews).m_eventMessage.giftContent.texture);
			}
			else
			{
				int num = messageId % 7;
				component.SetPresentColor(num);
			}
		}
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0005EAC4 File Offset: 0x0005CEC4
	public override void CreateUI()
	{
		if (this.m_numberText == null)
		{
			Frame frame = this.m_planetNodeSpriteSheet.m_atlas.GetFrame("menu_timer_background", null);
			this.bgSpriteC = SpriteS.AddComponent(this.m_domeNumberTC, frame, this.m_planetNodeSpriteSheet);
			SpriteS.SetColor(this.bgSpriteC, new Color(1f, 1f, 1f, 0.6f));
			this.m_numberText = TextMeshS.AddComponent(this.m_domeNumberTC, Vector3.forward * -0.5f, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 5f, 2f, 30f, Align.Center, Align.Middle, this.m_loop.m_planet.m_planetCamera, this.m_loop.m_levelNumber.ToString());
			string bannerString = (this.m_loop as PsGameLoopNews).GetBannerString();
			this.m_numberText.m_textMesh.characterSize = 0.65f;
			TextMeshS.SetText(this.m_numberText, bannerString, false);
			SpriteS.SetDimensions(this.bgSpriteC, this.m_numberText.m_renderer.bounds.size.x * 1.25f, this.m_numberText.m_renderer.bounds.size.y * 0.65f);
			this.m_numberText.m_renderer.material.shader = Shader.Find("Framework/FontShaderFg");
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0005EC37 File Offset: 0x0005D037
	public override void UpdateBanner()
	{
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0005EC39 File Offset: 0x0005D039
	public override void Update()
	{
		base.Update();
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x0005EC41 File Offset: 0x0005D041
	public override string GetTransformName()
	{
		return "FloaterFreshAndFree";
	}
}
