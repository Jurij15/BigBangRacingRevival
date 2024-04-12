using System;
using UnityEngine;

// Token: 0x02000202 RID: 514
public class PsGetDiamondsFlow : Flow
{
	// Token: 0x06000F05 RID: 3845 RVA: 0x0008EA08 File Offset: 0x0008CE08
	public PsGetDiamondsFlow(Action _proceed = null, Action _cancel = null, Action _customShopAction = null)
		: base(_proceed, _cancel, null)
	{
		this.m_customShopAction = _customShopAction;
		this.m_popup = new PsUIBasePopup(typeof(PsUICenterNotEnoughDiamonds), null, null, null, true, true, InitialPage.Center, false, false, false);
		this.m_popup.SetAction("EnterShop", new Action(this.UserProceededToPurchaseDiamonds));
		this.m_popup.SetAction("Exit", new Action(this.UserDeclinedDiamondPurchase));
		TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		if (PsMetagameManager.m_menuResourceView != null)
		{
			this.m_lastResourceView = PsMetagameManager.m_menuResourceView.SetLastView();
		}
		PsMetagameManager.ShowResources(null, true, false, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x0008EAE4 File Offset: 0x0008CEE4
	private void UserProceededToPurchaseDiamonds()
	{
		this.DestroyPopup();
		if (this.m_lastResourceView != null)
		{
			PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastResourceView);
		}
		if (this.m_customShopAction != null)
		{
			this.m_customShopAction.Invoke();
			if (this.Proceed != null)
			{
				this.Proceed.Invoke();
			}
		}
		else
		{
			new PsGetDiamondsFlow.ShopFlow(this, this.Proceed, this.Cancel);
		}
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x0008EB56 File Offset: 0x0008CF56
	public void DestroyPopup()
	{
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
			this.m_popup = null;
		}
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x0008EB75 File Offset: 0x0008CF75
	private void UserDeclinedDiamondPurchase()
	{
		this.DestroyPopup();
		if (this.m_lastResourceView != null)
		{
			PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastResourceView);
		}
		if (this.Cancel != null)
		{
			this.Cancel.Invoke();
		}
	}

	// Token: 0x040011E2 RID: 4578
	private PsUIBasePopup m_popup;

	// Token: 0x040011E3 RID: 4579
	private LastResourceView m_lastResourceView;

	// Token: 0x040011E4 RID: 4580
	private Action m_customShopAction;

	// Token: 0x02000203 RID: 515
	private class ShopFlow : Flow
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x0008EBB0 File Offset: 0x0008CFB0
		public ShopFlow(Flow _rootFlow, Action _proceed = null, Action _cancel = null)
			: base(_proceed, _cancel, _rootFlow)
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUICenterShopAll), typeof(PsUITopBackButton), null, null, true, true, InitialPage.Center, false, false, false);
			this.m_popup.SetAction("Exit", new Action(this.UserClosedTheShop));
			(this.m_popup.m_mainContent as PsUICenterShopAll).ScrollToGemShop();
			this.m_popup.Step();
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
			if (PsMetagameManager.m_menuResourceView != null)
			{
				this.m_lastResourceView = PsMetagameManager.m_menuResourceView.SetLastView();
			}
			PsMetagameManager.ShowResources((this.m_popup.m_topContent as PsUITopBackButton).m_camera, false, true, true, false, 0.03f, false, false, false);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0008ECA8 File Offset: 0x0008D0A8
		private void UserClosedTheShop()
		{
			this.m_popup.Destroy();
			this.m_popup = null;
			if (this.Cancel != null)
			{
				this.Cancel.Invoke();
			}
			if (this.m_lastResourceView != null)
			{
				PsMetagameManager.m_menuResourceView.ShowLastView(this.m_lastResourceView);
			}
			else
			{
				PsMetagameManager.HideResources();
			}
		}

		// Token: 0x040011E5 RID: 4581
		private PsUIBasePopup m_popup;

		// Token: 0x040011E6 RID: 4582
		private LastResourceView m_lastResourceView;
	}
}
