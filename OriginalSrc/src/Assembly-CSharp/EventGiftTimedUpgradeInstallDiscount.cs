using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000102 RID: 258
public class EventGiftTimedUpgradeInstallDiscount : EventGiftTimed
{
	// Token: 0x060005A2 RID: 1442 RVA: 0x00047BF4 File Offset: 0x00045FF4
	public EventGiftTimedUpgradeInstallDiscount(Dictionary<string, object> _dict)
	{
		this.m_timedType = EventGiftTimedType.upgrade50discount;
		if (_dict.ContainsKey("amount"))
		{
			int num = Convert.ToInt32(_dict["amount"]);
			num = Mathf.Min(100, Mathf.Max(0, num));
			if (num != 1)
			{
				this.m_discount = (float)num / 100f;
			}
		}
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x00047C60 File Offset: 0x00046060
	public float GetPriceMultiplier()
	{
		return 1f - this.m_discount;
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00047C7C File Offset: 0x0004607C
	public string GetDiscountTag()
	{
		return "-" + this.m_discount * 100f + "%";
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00047CAB File Offset: 0x000460AB
	public override string GetName()
	{
		return PsStrings.Get(StringID.GIFT_TYPE_UPGRADE_DAY);
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x00047CB8 File Offset: 0x000460B8
	public override void CreateUI(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, this.GetDiscountTag(), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FEA700", "#cc350c");
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00047D04 File Offset: 0x00046104
	public override void EndAction(Action _callback)
	{
		PsMainMenuState.ChangeToGarageState(delegate
		{
			PsMainMenuState.ExitToMainMenuState();
			_callback.Invoke();
		});
	}

	// Token: 0x04000710 RID: 1808
	private float m_discount = 0.5f;
}
