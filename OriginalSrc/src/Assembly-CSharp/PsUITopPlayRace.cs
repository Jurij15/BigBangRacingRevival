using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200035B RID: 859
public class PsUITopPlayRace : UICanvas, IPlayMenu
{
	// Token: 0x06001902 RID: 6402 RVA: 0x0010FA78 File Offset: 0x0010DE78
	public PsUITopPlayRace(Action _restartAction = null, Action _pauseAction = null)
		: base(null, false, "TopContent", null, string.Empty)
	{
		this.m_restartAction = _restartAction;
		this.m_pauseAction = _pauseAction;
		this.m_race = PsState.m_activeGameLoop.m_gameMode as PsGameModeRace;
		int num = Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks);
		float num2 = HighScores.TicksToTime(num);
		this.m_medalTimes = new float[]
		{
			this.m_race.m_oneMedalTime,
			this.m_race.m_twoMedalTime,
			this.m_race.m_threeMedalTime
		};
		Debug.Log(string.Concat(new object[]
		{
			num2,
			" ",
			this.m_race.m_threeMedalTime,
			" ",
			this.m_race.m_twoMedalTime,
			" ",
			this.m_race.m_oneMedalTime
		}), null);
		if (num2 < this.m_race.m_threeMedalTime)
		{
			this.m_starCount = 3;
		}
		else if (num2 < this.m_race.m_twoMedalTime)
		{
			this.m_starCount = 2;
		}
		else if (num2 < this.m_race.m_oneMedalTime)
		{
			this.m_starCount = 1;
		}
		else
		{
			this.m_starCount = 0;
		}
		this.RemoveDrawHandler();
		this.m_leftArea = new UIVerticalList(this, "UpperLeft");
		this.m_leftArea.SetMargins(0.01f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetMargins(-0.01f, 0.025f, 0.01f, 0.025f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetSpacing(0.01f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetAlign(0f, 1f);
		this.m_leftArea.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_leftArea, "hList");
		uihorizontalList.SetSpacing(0.425f, RelativeTo.ScreenHeight);
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ResourceLabelBackground));
		uihorizontalList.SetHeight(0.12f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.03f, -0.055f, 0f, 0f, RelativeTo.ScreenHeight);
		UIComponent uicomponent = new UIComponent(uihorizontalList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetSize(0f, 0f, RelativeTo.ScreenHeight);
		uicomponent.SetMargins(0.275f, 0f, 0f, 0f, RelativeTo.ScreenShortest);
		uicomponent.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(uicomponent, false, "TrophyCanvas", null, string.Empty);
		uicanvas.SetHeight(0.15f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.3f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		this.m_stars = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.GetStarFrame(), null), true, true);
		this.m_timer = new UIText(uihorizontalList, false, string.Empty, HighScores.TicksToTimeString(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks)), PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.05f, RelativeTo.ScreenHeight, null, null);
		this.m_timer.m_tmc.m_horizontalAlign = Align.Right;
		this.m_timer.SetVerticalAlign(0.35f);
		this.CreateCoinArea();
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList2.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList2.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList2.SetAlign(1f, 1f);
		uihorizontalList2.RemoveDrawHandler();
		this.CreateRestartArea(uihorizontalList2);
		this.Update();
		this.CreateExplodingStars();
		this.Step();
	}

	// Token: 0x06001903 RID: 6403 RVA: 0x0010FE10 File Offset: 0x0010E210
	public virtual string GetStarFrame()
	{
		return "menu_mode_star_0";
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x0010FE18 File Offset: 0x0010E218
	public virtual void CreateExplodingStars()
	{
		float num = 0.16f * (float)Screen.height;
		float num2 = 0f * this.m_stars.m_actualHeight;
		this.m_starList = new List<UIStarInflate>();
		for (int i = 0; i < this.m_starCount; i++)
		{
			float num3 = this.m_stars.m_actualWidth * 0.296f;
			float num4 = this.m_stars.m_actualWidth * 0.292f;
			float num5 = this.m_stars.m_actualHeight * 0.0155f;
			Vector3 vector = this.m_stars.m_TC.transform.position + new Vector3(2f, 1f, 0f) + new Vector3((float)i * num3 - num4, (float)i * num5, (float)i * -1f - 1f);
			TransformC transformC = TransformS.AddComponent(this.m_TC.p_entity, "InflatingStar", vector);
			PrefabC prefabC = PrefabS.AddComponent(transformC, Vector3.zero, ResourceManager.GetGameObject(RESOURCE.UIStarInflatePrefab_GameObject));
			prefabC.p_gameObject.transform.Rotate(0f, 180f, 0f);
			prefabC.p_gameObject.transform.localScale = new Vector3(num / 100f, num / 100f, 1f);
			PrefabS.SetCamera(prefabC, this.m_camera);
			this.m_starList.Add((UIStarInflate)prefabC.p_gameObject.GetComponent(typeof(UIStarInflate)));
		}
	}

	// Token: 0x06001905 RID: 6405 RVA: 0x0010FFA4 File Offset: 0x0010E3A4
	public virtual void CreateRestartArea(UIComponent _parent)
	{
		this.m_restartButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
		this.m_pauseButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_pauseButton.SetSound("/UI/PauseGame");
		this.m_pauseButton.SetIcon("hud_icon_pause", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_pauseButton.SetOrangeColors(true);
	}

	// Token: 0x06001906 RID: 6406 RVA: 0x00110064 File Offset: 0x0010E464
	public virtual void CreateCoinArea()
	{
		PsMetagameManager.ShowResources(this.m_camera, false, true, false, false, 0.15f, false, false, false);
	}

	// Token: 0x06001907 RID: 6407 RVA: 0x00110088 File Offset: 0x0010E488
	public virtual void UpdateTimer()
	{
		int num = Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks);
		float num2 = HighScores.TicksToTime(num);
		float num3 = 5f;
		for (int i = 0; i < this.m_starList.Count; i++)
		{
			if (num2 > this.m_medalTimes[i] - num3 && num2 < this.m_medalTimes[i])
			{
				float num4 = (num2 - (this.m_medalTimes[i] - num3)) / num3;
				this.m_starList[i].SetInflationAmount(num4);
				if (i == this.m_starList.Count - 1)
				{
					if (this.m_popTimer == null)
					{
						float num5 = 0.05f;
						this.m_popTimer = new UIText(this, false, "popTimer", Mathf.CeilToInt(this.m_medalTimes[i] - num2).ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), num5, RelativeTo.ScreenHeight, null, null);
						Vector3 position = this.m_starList[i].transform.position;
						this.m_popTimer.SetDepthOffset(-25f);
						this.m_popTimer.Update();
						this.m_popTimer.m_TC.transform.position = new Vector3(position.x, position.y - this.m_popTimer.m_actualHeight / 6f, this.m_popTimer.m_TC.transform.position.z);
					}
					else
					{
						this.m_popTimer.SetText(Mathf.CeilToInt(this.m_medalTimes[i] - num2).ToString());
					}
				}
			}
			else if (num2 > this.m_medalTimes[i] && !this.m_starList[i].exploded)
			{
				this.m_starList[i].SetInflationAmount(1f);
				if (i > 0 && num2 > this.m_medalTimes[i - 1] - num3)
				{
					this.m_popTimer.SetText(Mathf.CeilToInt(this.m_medalTimes[i - 1] - num2).ToString());
					Vector3 position2 = this.m_starList[i - 1].transform.position;
					this.m_popTimer.m_TC.transform.position = new Vector3(position2.x, position2.y - this.m_popTimer.m_actualHeight / 6f, this.m_popTimer.m_TC.transform.position.z);
				}
				else
				{
					this.m_popTimer.Destroy();
					this.m_popTimer = null;
				}
				this.m_starList.RemoveAt(i);
			}
		}
		this.m_timer.SetText(HighScores.TicksToTimeString(num));
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x00110360 File Offset: 0x0010E760
	public override void Step()
	{
		this.UpdateTimer();
		if (this.m_restartButton.m_hit)
		{
			if (this.m_restartAction != null)
			{
				this.m_restartAction.Invoke();
			}
		}
		else if (this.m_pauseButton.m_hit && this.m_pauseAction != null)
		{
			this.m_pauseAction.Invoke();
		}
		base.Step();
	}

	// Token: 0x06001909 RID: 6409 RVA: 0x001103CA File Offset: 0x0010E7CA
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x0600190A RID: 6410 RVA: 0x001103D2 File Offset: 0x0010E7D2
	public void ApplyLeftySettings()
	{
	}

	// Token: 0x04001B8A RID: 7050
	protected PsUIGenericButton m_restartButton;

	// Token: 0x04001B8B RID: 7051
	protected PsUIGenericButton m_pauseButton;

	// Token: 0x04001B8C RID: 7052
	private Action m_restartAction;

	// Token: 0x04001B8D RID: 7053
	private Action m_pauseAction;

	// Token: 0x04001B8E RID: 7054
	private UIVerticalList m_leftArea;

	// Token: 0x04001B8F RID: 7055
	private int m_starCount;

	// Token: 0x04001B90 RID: 7056
	protected UIFittedSprite m_stars;

	// Token: 0x04001B91 RID: 7057
	protected UIText m_timer;

	// Token: 0x04001B92 RID: 7058
	protected UIText m_popTimer;

	// Token: 0x04001B93 RID: 7059
	private PsGameModeRace m_race;

	// Token: 0x04001B94 RID: 7060
	private string m_frameName;

	// Token: 0x04001B95 RID: 7061
	private List<UIStarInflate> m_starList;

	// Token: 0x04001B96 RID: 7062
	protected float[] m_medalTimes;
}
