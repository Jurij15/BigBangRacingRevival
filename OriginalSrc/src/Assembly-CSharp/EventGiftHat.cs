using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000F6 RID: 246
public class EventGiftHat : EventGiftComponent
{
	// Token: 0x0600056F RID: 1391 RVA: 0x00046D7F File Offset: 0x0004517F
	public EventGiftHat(Dictionary<string, object> _dict)
	{
		if (_dict.ContainsKey("identifier"))
		{
			this.m_hatIdentifier = (string)_dict["identifier"];
		}
		else
		{
			Debug.LogError("Did not contain identifier");
		}
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x00046DBC File Offset: 0x000451BC
	public override void Claim(Hashtable _data)
	{
		for (int i = 0; i < PsState.m_vehicleTypes.Length; i++)
		{
			PsCustomisationManager.UnlockItem(PsState.m_vehicleTypes[i], this.m_hatIdentifier);
		}
		Hashtable updatedData = PsCustomisationManager.GetUpdatedData(PsUpgradeManager.GetUpdatedData(null));
		_data.Add("customisation", updatedData);
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00046E0C File Offset: 0x0004520C
	private string GetPicture()
	{
		PsCustomisationItem itemByIdentifier = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(this.m_hatIdentifier);
		return itemByIdentifier.m_iconName;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00046E3C File Offset: 0x0004523C
	public override string GetName()
	{
		PsCustomisationItem itemByIdentifier = PsCustomisationManager.GetVehicleCustomisationData(typeof(OffroadCar)).GetItemByIdentifier(this.m_hatIdentifier);
		return PsStrings.Get(itemByIdentifier.m_title);
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00046E70 File Offset: 0x00045270
	public override void CreateUI(UIComponent _parent)
	{
		UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.GetPicture(), null), true, true);
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00046EA8 File Offset: 0x000452A8
	public override void EndAction(Action _callback)
	{
		PsUICenterGarage.m_startView = 0;
		PsMainMenuState.ChangeToGarageState(delegate
		{
			PsMainMenuState.ExitToMainMenuState();
			_callback.Invoke();
		});
	}

	// Token: 0x04000700 RID: 1792
	public string m_hatIdentifier;
}
