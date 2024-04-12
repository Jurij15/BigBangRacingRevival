using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000F8 RID: 248
public class EventGiftEditorItem : EventGiftComponent
{
	// Token: 0x0600057A RID: 1402 RVA: 0x00047100 File Offset: 0x00045500
	public EventGiftEditorItem(Dictionary<string, object> _dict)
	{
		if (_dict.ContainsKey("identifier"))
		{
			this.m_identifier = (string)_dict["identifier"];
		}
		else
		{
			Debug.LogError("Did not contain identifier");
		}
		if (_dict.ContainsKey("amount"))
		{
			this.m_amount = Convert.ToInt32(_dict["amount"]);
		}
		else
		{
			Debug.LogError("Did not contain amount");
		}
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0004717D File Offset: 0x0004557D
	public override void Claim(Hashtable _data)
	{
		PsMetagameManager.m_playerStats.CumulateEditorResources(this.m_identifier, this.m_amount);
		_data.Add("editorResources", PsMetagameManager.m_playerStats.GetUpdatedEditorResources());
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x000471AC File Offset: 0x000455AC
	private PsEditorItem GetEditorItem()
	{
		if (this.m_item != null)
		{
			return this.m_item;
		}
		for (int i = 0; i < PsMetagameData.m_units.Count - 1; i++)
		{
			for (int j = 0; j < PsMetagameData.m_units[i].m_items.Count; j++)
			{
				if (PsMetagameData.m_units[i].m_items[j].m_identifier == this.m_identifier)
				{
					this.m_item = PsMetagameData.m_units[i].m_items[j] as PsEditorItem;
				}
			}
		}
		return this.m_item;
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00047260 File Offset: 0x00045660
	public override void CreateUI(UIComponent _parent)
	{
		PsEditorItem editorItem = this.GetEditorItem();
		if (editorItem == null)
		{
			return;
		}
		PsUIEditorItemCard psUIEditorItemCard = new PsUIEditorItemCard(_parent, editorItem, false, false);
		psUIEditorItemCard.SetItemCount(0);
		psUIEditorItemCard.RemoveTouchAreas();
		UICanvas uicanvas = new UICanvas(psUIEditorItemCard, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.22f, RelativeTo.ParentHeight);
		uicanvas.SetRogue();
		uicanvas.SetVerticalAlign(0f);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetMargins(0f, 0f, 0.06f, -0.15f, RelativeTo.ParentHeight);
		string text = "+" + this.m_amount;
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, text, PsFontManager.GetFont(PsFonts.HurmeBold), false, "ffffff", "000000");
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00047328 File Offset: 0x00045728
	public override string GetName()
	{
		PsEditorItem editorItem = this.GetEditorItem();
		if (editorItem == null)
		{
			return "NULL";
		}
		return PsStrings.Get(editorItem.m_name);
	}

	// Token: 0x04000702 RID: 1794
	public string m_identifier;

	// Token: 0x04000703 RID: 1795
	public int m_amount;

	// Token: 0x04000704 RID: 1796
	public PsEditorItem m_item;
}
