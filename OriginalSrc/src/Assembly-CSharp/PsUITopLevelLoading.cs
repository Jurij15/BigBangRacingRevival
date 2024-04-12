using System;
using UnityEngine;

// Token: 0x020003BA RID: 954
public class PsUITopLevelLoading : UICanvas
{
	// Token: 0x06001B37 RID: 6967 RVA: 0x00130178 File Offset: 0x0012E578
	public PsUITopLevelLoading(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.RemoveDrawHandler();
		this.CreateLeftArea();
		this.CreateHeader(false);
		this.m_infoBar = new PsUIInfoBar(this, string.Empty, false);
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x001301B4 File Offset: 0x0012E5B4
	protected virtual void CreateLeftArea()
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetSound("/UI/ButtonBack");
		this.m_exitButton.SetOrangeColors(true);
	}

	// Token: 0x06001B39 RID: 6969 RVA: 0x0013025C File Offset: 0x0012E65C
	private void CreateHeader(bool _update = true)
	{
		if (PsState.m_activeGameLoop == null)
		{
			return;
		}
		if (PsState.m_activeGameLoop.m_minigameMetaData != null)
		{
			this.m_header = new PsUILevelHeader(this);
			this.m_header.SetVerticalAlign(0.98f);
			if (_update)
			{
				this.Update();
			}
			return;
		}
		if (!PsState.m_activeGameLoop.m_loadingMetaData)
		{
			PsState.m_activeGameLoop.LoadMinigameMetaData(delegate
			{
				this.CreateHeader(true);
			});
			Debug.LogWarning("start loading minigame metadata, adding callback");
			return;
		}
		PsGameLoop activeGameLoop = PsState.m_activeGameLoop;
		activeGameLoop.m_loadMetadataCallback = (Action)Delegate.Combine(activeGameLoop.m_loadMetadataCallback, delegate
		{
			this.CreateHeader(true);
		});
		Debug.LogWarning("still loading minigame metadata, adding callback");
	}

	// Token: 0x06001B3A RID: 6970 RVA: 0x0013030C File Offset: 0x0012E70C
	public override void Update()
	{
		base.Update();
		if (!this.m_animationDone && this.m_header != null)
		{
			TweenS.AddTransformTween(this.m_header.m_TC, TweenedProperty.Position, TweenStyle.BackOut, this.m_header.m_TC.transform.localPosition + new Vector3(0f, 120f, 0f), this.m_header.m_TC.transform.localPosition, 0.3f, 0f, true);
			this.m_animationDone = true;
		}
	}

	// Token: 0x06001B3B RID: 6971 RVA: 0x001303A0 File Offset: 0x0012E7A0
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && this.m_exitButton.m_TAC.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x06001B3C RID: 6972 RVA: 0x00130444 File Offset: 0x0012E844
	public void Close()
	{
		TweenS.AddTransformTween(this.m_header.m_TC, TweenedProperty.Position, TweenStyle.Linear, this.m_header.m_TC.transform.localPosition, this.m_header.m_TC.transform.localPosition + new Vector3(0f, 120f, 0f), 0.2f, 0f, true);
		this.m_infoBar.TweenAway();
	}

	// Token: 0x06001B3D RID: 6973 RVA: 0x001304C0 File Offset: 0x0012E8C0
	public void SetBackButtonActive(bool flag)
	{
		if (this.m_exitButton != null)
		{
			if (flag)
			{
				this.m_exitButton.SetOrangeColors(true);
				this.m_exitButton.m_TAC.m_active = true;
			}
			else
			{
				this.m_exitButton.SetGrayColors();
				this.m_exitButton.m_TAC.m_active = false;
			}
			this.m_exitButton.Update();
		}
	}

	// Token: 0x04001DA3 RID: 7587
	private PsUIGenericButton m_exitButton;

	// Token: 0x04001DA4 RID: 7588
	private PsUILevelHeader m_header;

	// Token: 0x04001DA5 RID: 7589
	private PsUIInfoBar m_infoBar;

	// Token: 0x04001DA6 RID: 7590
	private bool m_animationDone;
}
