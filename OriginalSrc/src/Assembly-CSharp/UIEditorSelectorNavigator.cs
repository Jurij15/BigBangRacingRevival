using System;
using UnityEngine;

// Token: 0x02000395 RID: 917
public class UIEditorSelectorNavigator : UIHorizontalList
{
	// Token: 0x06001A54 RID: 6740 RVA: 0x00125ED8 File Offset: 0x001242D8
	public UIEditorSelectorNavigator(UIComponent _parent, string _title, string _description, string _tag, EditorSelectorContext _context, bool _createBackButton, UIFittedText _notificationAmount)
		: base(_parent, _tag)
	{
		this.m_context = _context;
		this.m_notificationAmount = _notificationAmount;
		if (_context != EditorSelectorContext.ITEM_SELECT)
		{
			if (_context == EditorSelectorContext.FREE_WIZARD)
			{
				this.m_categories = UIEditorSelectorNavigator.WIZARD_CATEGORIES;
				this.m_buttonColors = new Color[]
				{
					PsColors.objectCatButtonBlue,
					PsColors.objectCatButtonGrey,
					PsColors.objectCatButtonGreen
				};
			}
		}
		else
		{
			int num = ((!PsState.m_adminMode) ? (PsMetagameData.m_units.Count - 1) : PsMetagameData.m_units.Count);
			this.m_categories = new string[num];
			this.m_buttonColors = new Color[]
			{
				PsColors.objectCatButtonBlue,
				PsColors.objectCatButtonGreen,
				PsColors.objectCatButtonRed
			};
			for (int i = 0; i < this.m_categories.Length; i++)
			{
				PsUnlockable psUnlockable = PsMetagameData.m_units[i];
				this.m_categories[i] = psUnlockable.m_name;
			}
		}
		this.SetAlign(0f, 1f);
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		if (_createBackButton)
		{
			this.m_backButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_backButton.SetOrangeColors(true);
			this.m_backButton.SetIcon("hud_icon_back", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		}
		UIVerticalList uiverticalList = new UIVerticalList(this, "rightColumn");
		uiverticalList.SetMargins(0.1f, 0f, 0.1f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, "Title", string.Concat(new string[]
		{
			"<color=#",
			ToolBox.ColorToHex(PsColors.menuLimeGreen),
			">",
			_title.ToUpper(),
			"</color>"
		}), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.07f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(0f);
		if (_description != string.Empty)
		{
			UIText uitext2 = new UIText(uiverticalList, false, "Description", _description, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.03f, RelativeTo.ScreenHeight, null, null);
			uitext2.SetHorizontalAlign(0f);
		}
		if (this.m_categories != null)
		{
			UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList, "buttonList");
			uihorizontalList2.SetHorizontalAlign(0f);
			uihorizontalList2.SetMargins(-0.025f, 0f, 0.035f, 0f, RelativeTo.ScreenHeight);
			uihorizontalList2.SetSpacing(0.01f, RelativeTo.ParentHeight);
			uihorizontalList2.RemoveDrawHandler();
			this.m_categoryButtons = new PsUIGenericButton[this.m_categories.Length];
			for (int j = 0; j < this.m_categoryButtons.Length; j++)
			{
				Color color = Color.yellow;
				bool flag = false;
				if (this.m_context == EditorSelectorContext.ITEM_SELECT)
				{
					if (PsState.m_editorLastSelectedItemCategory == this.m_categories[j])
					{
						flag = true;
					}
					if (j < this.m_buttonColors.Length)
					{
						color = this.m_buttonColors[j];
					}
				}
				else if (this.m_context == EditorSelectorContext.FREE_WIZARD)
				{
					if (PsState.m_freeWizardLastSelectedItemCategory == this.m_categories[j])
					{
						flag = true;
					}
					color = this.m_buttonColors[0];
				}
				this.m_categoryButtons[j] = this.AddCategoryButton(uihorizontalList2, PsStrings.Get(this.m_categories[j]), flag, color);
			}
		}
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x001262B8 File Offset: 0x001246B8
	public PsUIGenericButton AddCategoryButton(UIComponent _parent, string _title, bool _highlight, Color _color)
	{
		PsUIGenericButton psUIGenericButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetSubTabBlueColors(!_highlight);
		psUIGenericButton.SetTextWithMinWidth(_title, 0.03f, 0.2f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		return psUIGenericButton;
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x00126300 File Offset: 0x00124700
	public override void Step()
	{
		if (this.m_backButton != null && this.m_backButton.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			SoundS.PlaySingleShot("/UI/ButtonBack", Vector3.zero, 1f);
			if (this.m_context == EditorSelectorContext.START_WIZARD)
			{
				Main.m_currentGame.m_sceneManager.ChangeScene(new PsMenuScene("MenuScene", false), new FadeLoadingScene(Color.black, true, 0.25f));
			}
			else
			{
				Main.m_currentGame.m_sceneManager.m_currentScene.m_stateMachine.ChangeState(new EditorBaseState());
			}
		}
		if (this.m_categoryButtons != null)
		{
			for (int i = 0; i < this.m_categoryButtons.Length; i++)
			{
				if (this.m_categoryButtons[i].m_hit)
				{
					TouchAreaS.CancelAllTouches(null);
					if (this.m_context == EditorSelectorContext.ITEM_SELECT)
					{
						this.OpenItemCategory(this.m_categories[i]);
					}
					else if (this.m_context == EditorSelectorContext.FREE_WIZARD)
					{
						this.OpenWizardCategory(this.m_categories[i]);
					}
				}
			}
		}
		base.Step();
	}

	// Token: 0x06001A57 RID: 6743 RVA: 0x00126415 File Offset: 0x00124815
	public void OpenItemCategory(string _category)
	{
		PsState.m_editorLastSelectedItemCategory = _category;
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorItem(_category, this.m_notificationAmount));
	}

	// Token: 0x06001A58 RID: 6744 RVA: 0x00126440 File Offset: 0x00124840
	public void OpenWizardCategory(string _category)
	{
		PsState.m_freeWizardLastSelectedItemCategory = _category;
		if (_category == UIEditorSelectorNavigator.WIZARD_CATEGORIES[0])
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorPlayer(this.m_context));
		}
		else if (_category == UIEditorSelectorNavigator.WIZARD_CATEGORIES[1])
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorGameMode(this.m_context));
		}
		else if (_category == UIEditorSelectorNavigator.WIZARD_CATEGORIES[2])
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorArea(this.m_context));
		}
	}

	// Token: 0x04001CDC RID: 7388
	public static string[] WIZARD_CATEGORIES = new string[] { "EDITOR_GUI_VEHICLE", "EDITOR_GUI_GAMEMODE_HEADER", "EDITOR_GUI_DOME_SIZE_HEADER" };

	// Token: 0x04001CDD RID: 7389
	public Color[] m_buttonColors;

	// Token: 0x04001CDE RID: 7390
	public PsUIGenericButton m_backButton;

	// Token: 0x04001CDF RID: 7391
	public PsUIGenericButton[] m_categoryButtons;

	// Token: 0x04001CE0 RID: 7392
	public UITextButton[] m_parentCategoryButtons;

	// Token: 0x04001CE1 RID: 7393
	private string[] m_categories;

	// Token: 0x04001CE2 RID: 7394
	private EditorSelectorContext m_context;

	// Token: 0x04001CE3 RID: 7395
	private UIFittedText m_notificationAmount;
}
