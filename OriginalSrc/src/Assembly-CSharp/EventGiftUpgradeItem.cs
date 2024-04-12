using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000F9 RID: 249
public class EventGiftUpgradeItem : EventGiftComponent
{
	// Token: 0x0600057F RID: 1407 RVA: 0x00047354 File Offset: 0x00045754
	public EventGiftUpgradeItem(Dictionary<string, object> _dict)
	{
		if (_dict.ContainsKey("identifier"))
		{
			try
			{
				this.m_rarity = (PsRarity)Enum.Parse(typeof(PsRarity), (string)_dict["identifier"]);
			}
			catch
			{
				Debug.LogError("parsing failed, string was: " + (string)_dict["identifier"]);
			}
		}
		else
		{
			Debug.LogError("Did not contain type");
		}
		if (_dict.ContainsKey("amount"))
		{
			this.m_maxCount = Convert.ToInt32(_dict["amount"]);
		}
		else
		{
			Debug.LogError("Did not contain amount");
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00047420 File Offset: 0x00045820
	private void GenerateItem()
	{
		List<PsUpgradeItem> list = new List<PsUpgradeItem>();
		PsUpgradeData vehicleUpgradeData = PsUpgradeManager.GetVehicleUpgradeData(typeof(OffroadCar));
		for (int i = 0; i <= PsMetagameManager.m_playerStats.carRank; i++)
		{
			list.AddRange(vehicleUpgradeData.GetUpgradeItemsByTier(i + 1));
		}
		if (PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)))
		{
			PsUpgradeData vehicleUpgradeData2 = PsUpgradeManager.GetVehicleUpgradeData(typeof(Motorcycle));
			for (int j = 0; j <= PsMetagameManager.m_playerStats.mcRank; j++)
			{
				list.AddRange(vehicleUpgradeData2.GetUpgradeItemsByTier(j + 1));
			}
		}
		GachaMachine<string> gachaMachine = new GachaMachine<string>();
		for (int k = 0; k < list.Count; k++)
		{
			if (list[k].m_rarity == this.m_rarity)
			{
				gachaMachine.AddItem(list[k].m_identifier, 1f, -1);
			}
		}
		this.m_identifier = gachaMachine.GetItem(true);
		if (this.m_identifier.StartsWith("Car"))
		{
			this.m_item = PsUpgradeManager.GetUpgradeItem(typeof(OffroadCar), this.m_identifier);
		}
		else
		{
			this.m_item = PsUpgradeManager.GetUpgradeItem(typeof(Motorcycle), this.m_identifier);
		}
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00047574 File Offset: 0x00045974
	public override void Claim(Hashtable _data)
	{
		this.GenerateItem();
		PsUpgradeManager.IncreaseResources(this.m_identifier, this.m_maxCount);
		Hashtable updatedData = PsCustomisationManager.GetUpdatedData(PsUpgradeManager.GetUpdatedData(null));
		_data.Add("customisation", updatedData);
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x000475B0 File Offset: 0x000459B0
	public override string GetName()
	{
		return PsStrings.Get(this.m_item.m_title);
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x000475C4 File Offset: 0x000459C4
	public override void CreateUI(UIComponent _parent)
	{
		PsUIUpgradeView.UpgradeItemInfo upgradeItemInfo = new PsUIUpgradeView.UpgradeItemInfo(_parent, this.m_item, false, null, "revealCardFront", false);
		upgradeItemInfo.SetWidth(1f, RelativeTo.ParentWidth);
		UICanvas uicanvas = new UICanvas(upgradeItemInfo, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.22f, RelativeTo.ParentHeight);
		uicanvas.SetRogue();
		uicanvas.SetVerticalAlign(0f);
		uicanvas.RemoveDrawHandler();
		string text = "+" + this.m_maxCount;
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), false, "ffffff", "000000");
	}

	// Token: 0x04000705 RID: 1797
	public PsRarity m_rarity;

	// Token: 0x04000706 RID: 1798
	public int m_maxCount;

	// Token: 0x04000707 RID: 1799
	public string m_identifier;

	// Token: 0x04000708 RID: 1800
	public PsUpgradeItem m_item;
}
