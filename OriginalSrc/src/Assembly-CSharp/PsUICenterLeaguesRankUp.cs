using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A2 RID: 930
public class PsUICenterLeaguesRankUp : PsUICenterLeagues
{
	// Token: 0x06001A89 RID: 6793 RVA: 0x00127CD0 File Offset: 0x001260D0
	public PsUICenterLeaguesRankUp(UIComponent _parent)
		: base(_parent)
	{
		SoundS.PlaySingleShot("/Ingame/Events/League_ScreenAppear", Vector3.zero, 1f);
		UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_playerLeagueBanner, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(PsMetagameData.m_leagueData[PsMetagameData.GetCurrentLeagueIndex() - 1].m_bannerSprite, null), true, true);
		uifittedSprite.SetDepthOffset(-1f);
		TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 0f, 0.3f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			this.TweenHighlightUI();
			this.TweenToLeague();
		});
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x00127D7A File Offset: 0x0012617A
	protected override void CustomUpdate()
	{
		this.UpdateHighlightSize(true);
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x00127D84 File Offset: 0x00126184
	public void TweenHighlightUI()
	{
		float y = this.m_leagueInfos[this.m_currentRank - 1].m_TC.transform.localPosition.y;
		float y2 = this.m_leagueInfos[this.m_currentRank].m_TC.transform.localPosition.y;
		Vector3 startAlpha = new Vector3(0.255f, 0.255f, 0.255f);
		Vector3 endAlpha = new Vector3(0.47f, 0.47f, 0.47f);
		TweenC tweenC = TweenS.AddTransformTween(this.m_highlightUI.m_TC, TweenedProperty.Alpha, TweenStyle.CubicInOut, startAlpha, endAlpha, this.toLeagueDuration / 2f - 0.45f, 0f, false);
		tweenC.alphaPrefabs = true;
		TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _c)
		{
			this.UpdateHighlightSize(false);
			this.SetStartAlphas(endAlpha);
			this.m_highlightTween = TweenS.AddTransformTween(this.m_highlightUI.m_TC, TweenedProperty.Alpha, TweenStyle.CubicInOut, endAlpha, startAlpha, this.toLeagueDuration / 2f - 0.45f, 0.9f, false);
			this.m_highlightTween.alphaPrefabs = true;
		});
	}

	// Token: 0x06001A8C RID: 6796 RVA: 0x00127E7C File Offset: 0x0012627C
	private void SetStartAlphas(Vector3 _start)
	{
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Prefab, this.m_highlightUI.m_TC.p_entity);
		for (int i = 0; i < componentsByEntity.Count; i++)
		{
			PrefabC prefabC = componentsByEntity[i] as PrefabC;
			if (prefabC.p_gameObject != null)
			{
				Renderer component = prefabC.p_gameObject.GetComponent<Renderer>();
				if (component != null && component.material != null)
				{
					Color color = component.material.color;
					color.a = _start.x;
					component.material.color = color;
				}
			}
		}
	}

	// Token: 0x06001A8D RID: 6797 RVA: 0x00127F28 File Offset: 0x00126328
	protected override void HighlightStep()
	{
		float num = this.m_leagueInfos[this.m_currentRank - 1].m_TC.transform.localPosition.y;
		if (this.m_highlightTween != null)
		{
			num = this.m_leagueInfos[this.m_currentRank].m_TC.transform.localPosition.y;
		}
		this.m_highlightUI.m_TC.transform.position = new Vector3(this.m_highlightUI.m_TC.transform.position.x, this.m_scrollArea.m_scrollTC.transform.position.y * -1f + num, -5f);
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x00127FF8 File Offset: 0x001263F8
	protected override void SetScrollPosition()
	{
		int num = this.m_leagueInfos.IndexOf(this.m_currentLeagueInfo);
		num = ((num <= 0) ? 0 : (num - 1));
		this.m_scrollArea.SetScrollPositionToChild(this.m_leagueInfos[num]);
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x0012803F File Offset: 0x0012643F
	protected override PsUILeagueInfo CreateLeagueInfo(UIComponent _parent, int _index)
	{
		return new PsUILeagueInfo(_parent, _index, true);
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x0012804C File Offset: 0x0012644C
	public void TweenToLeague()
	{
		Vector3 vector;
		vector..ctor(0f, this.m_currentLeagueInfo.m_TC.transform.position.y);
		TweenC tweenC = TweenS.AddTransformTween(this.m_scrollArea.m_scrollTC, TweenedProperty.Position, TweenStyle.CubicInOut, vector, this.toLeagueDuration, 0f, false);
		TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
		{
			TweenS.RemoveComponent(c);
			this.RankUpEffect();
		});
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x001280B4 File Offset: 0x001264B4
	public void RankUpEffect()
	{
		int num = this.m_leagueInfos.IndexOf(this.m_currentLeagueInfo);
		int num2 = ((num <= 0) ? 0 : (num - 1));
		this.m_currentLeagueInfo.RankUp();
		this.PlayerBannerTween(null);
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x001280F8 File Offset: 0x001264F8
	private void PlayerBannerTween(TweenC _tweenC = null)
	{
		TweenC tweenC = TweenS.AddTransformTween(this.m_playerLeagueBanner.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.one, new Vector3(1.275f, 1.275f, 1f), 0.2f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
		{
			this.PlayerBannerFlip();
		});
		SoundS.PlaySingleShot("/Ingame/Events/League_Banner", Vector3.zero, 1f);
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x00128164 File Offset: 0x00126564
	private void PlayerBannerFlip()
	{
		TweenC tweenC = TweenS.AddTransformTween(this.m_playerLeagueBanner.m_TC, TweenedProperty.Rotation, TweenStyle.CubicIn, new Vector3(90f, 0f, 0f), 0.25f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC ev)
		{
			this.m_playerLeagueBanner.m_childs[this.m_playerLeagueBanner.m_childs.Count - 1].Destroy();
			TweenC tweenC2 = TweenS.AddTransformTween(this.m_playerLeagueBanner.m_TC, TweenedProperty.Rotation, TweenStyle.CubicIn, new Vector3(0f, 0f, 0f), 0.25f, 0f, true);
			TweenS.AddTweenEndEventListener(tweenC2, delegate(TweenC te)
			{
				TweenS.AddTransformTween(this.m_playerLeagueBanner.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.one, 0.2f, 0f, true);
			});
		});
	}

	// Token: 0x04001D0F RID: 7439
	private float m_highlightOffset;

	// Token: 0x04001D10 RID: 7440
	private TweenC m_highlightTween;

	// Token: 0x04001D11 RID: 7441
	private float toLeagueDuration = 2f;
}
