using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000F5 RID: 245
public class EventGiftTrail : EventGiftComponent
{
	// Token: 0x06000569 RID: 1385 RVA: 0x00046C0A File Offset: 0x0004500A
	public EventGiftTrail(Dictionary<string, object> _dict)
	{
		if (_dict.ContainsKey("identifier"))
		{
			this.m_trailIdentifier = (string)_dict["identifier"];
		}
		else
		{
			Debug.LogError("Did not contain identifier");
		}
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00046C48 File Offset: 0x00045048
	public override void Claim(Hashtable _data)
	{
		for (int i = 0; i < PsState.m_vehicleTypes.Length; i++)
		{
			PsCustomisationManager.UnlockItem(PsState.m_vehicleTypes[i], this.m_trailIdentifier);
		}
		Hashtable updatedData = PsCustomisationManager.GetUpdatedData(PsUpgradeManager.GetUpdatedData(null));
		_data.Add("customisation", updatedData);
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00046C98 File Offset: 0x00045098
	private string GetPicture()
	{
		PsCustomisationItem itemByIdentifier = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(this.m_trailIdentifier);
		return itemByIdentifier.m_iconName;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00046CC8 File Offset: 0x000450C8
	public override string GetName()
	{
		PsCustomisationItem itemByIdentifier = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(this.m_trailIdentifier);
		return PsStrings.Get(itemByIdentifier.m_title);
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00046CFC File Offset: 0x000450FC
	public override void CreateUI(UIComponent _parent)
	{
		UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.GetPicture(), null), true, true);
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00046D34 File Offset: 0x00045134
	public override void EndAction(Action _callback)
	{
		PsUICenterGarage.m_startView = 1;
		PsMainMenuState.ChangeToGarageState(delegate
		{
			PsMainMenuState.ExitToMainMenuState();
			_callback.Invoke();
		});
	}

	// Token: 0x040006FF RID: 1791
	public string m_trailIdentifier;
}
