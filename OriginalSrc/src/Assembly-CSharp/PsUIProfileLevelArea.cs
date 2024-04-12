using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000315 RID: 789
public class PsUIProfileLevelArea : UIScrollableCanvas
{
	// Token: 0x0600174A RID: 5962 RVA: 0x000FB504 File Offset: 0x000F9904
	public PsUIProfileLevelArea(UIComponent _parent, int _itemsPerRow, cpBB _margins = default(cpBB), RelativeTo _relativeTo = RelativeTo.ScreenHeight, int _minimumRowCount = -1, string _optionalTitle = null)
		: base(_parent, "levelArea")
	{
		if (!string.IsNullOrEmpty(_optionalTitle))
		{
			this.m_hasTitle = true;
		}
		this.m_itemsPerRow = _itemsPerRow;
		this.m_minimumRowCount = _minimumRowCount;
		this.m_buttons = new List<PsUIProfileLevelButton>();
		this.m_maxScrollInertialX = 0f;
		this.SetVerticalAlign(1f);
		this.RemoveDrawHandler();
		this.m_levelList = new UIVerticalList(this, "levelList");
		this.m_levelList.SetVerticalAlign(1f);
		this.m_levelList.SetHorizontalAlign(1f);
		this.m_levelList.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_levelList.SetMargins(_margins, _relativeTo);
		this.m_levelList.RemoveDrawHandler();
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x000FB5C1 File Offset: 0x000F99C1
	public void AddTitleComponent(UIComponent _title)
	{
		_title.Parent(this.m_levelList);
		this.m_hasTitle = true;
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x000FB5D8 File Offset: 0x000F99D8
	public void PopulateContent(PsMinigameMetaData[] _levels, Type _gameloopType, string _noLevelsTexts = "User has no levels", float _spacing = 0.02f, bool _createSlots = false, bool _showCreators = false, bool _claimable = false)
	{
		this.m_levelList.DestroyChildren((!this.m_hasTitle) ? 0 : 1);
		this.m_levelList.SetSpacing(_spacing, RelativeTo.ParentWidth);
		this.m_buttons.Clear();
		if (_levels == null)
		{
			PsUILoadingAnimation psUILoadingAnimation = new PsUILoadingAnimation(this.m_levelList, false);
			psUILoadingAnimation.SetDepthOffset(-3f);
		}
		else if (_levels.Length > 0)
		{
			int num = Mathf.CeilToInt((float)_levels.Length / (float)this.m_itemsPerRow);
			for (int i = 0; i < num; i++)
			{
				int num2 = _levels.Length - i * this.m_itemsPerRow;
				UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_levelList, string.Empty);
				uihorizontalList.SetHorizontalAlign(0f);
				uihorizontalList.SetSpacing(_spacing, RelativeTo.ParentWidth);
				uihorizontalList.RemoveDrawHandler();
				uihorizontalList.SetDepthOffset(-3f);
				float num3 = 1f - _spacing * (float)(this.m_itemsPerRow - 1);
				for (int j = 0; j < this.m_itemsPerRow; j++)
				{
					if (j + 1 > num2)
					{
						if (!_createSlots)
						{
							break;
						}
						UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
						uicanvas.SetWidth(num3 / (float)this.m_itemsPerRow, RelativeTo.ParentWidth);
						uicanvas.SetHeight(num3 / (float)this.m_itemsPerRow * 0.775f, RelativeTo.ParentWidth);
						uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.EmptySpaceRectDrawhandler));
					}
					else
					{
						object[] array = new object[] { _levels[i * this.m_itemsPerRow + j] };
						PsGameLoop psGameLoop = Activator.CreateInstance(_gameloopType, array) as PsGameLoop;
						UIHorizontalList uihorizontalList2 = uihorizontalList;
						PsGameLoop psGameLoop2 = psGameLoop;
						PsUIProfileLevelButton psUIProfileLevelButton = new PsUIProfileLevelButton(uihorizontalList2, psGameLoop2, true, _showCreators, _claimable);
						psUIProfileLevelButton.SetWidth(num3 / (float)this.m_itemsPerRow, RelativeTo.ParentWidth);
						psUIProfileLevelButton.SetHeight(num3 / (float)this.m_itemsPerRow * 0.775f, RelativeTo.ParentWidth);
						psUIProfileLevelButton.SetHorizontalAlign(0f);
						this.m_buttons.Add(psUIProfileLevelButton);
					}
				}
			}
			if (num < this.m_minimumRowCount)
			{
				int num4 = this.m_minimumRowCount - num;
				for (int k = 0; k < num4; k++)
				{
					UIHorizontalList uihorizontalList3 = new UIHorizontalList(this.m_levelList, string.Empty);
					uihorizontalList3.SetHorizontalAlign(0f);
					uihorizontalList3.SetSpacing(_spacing, RelativeTo.ParentWidth);
					uihorizontalList3.RemoveDrawHandler();
					uihorizontalList3.SetDepthOffset(-3f);
					float num5 = 1f - _spacing * (float)(this.m_itemsPerRow - 1);
					for (int l = 0; l < this.m_itemsPerRow; l++)
					{
						UICanvas uicanvas2 = new UICanvas(uihorizontalList3, false, string.Empty, null, string.Empty);
						uicanvas2.SetWidth(num5 / (float)this.m_itemsPerRow, RelativeTo.ParentWidth);
						uicanvas2.SetHeight(num5 / (float)this.m_itemsPerRow * 0.775f, RelativeTo.ParentWidth);
						uicanvas2.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.EmptySpaceRectDrawhandler));
					}
				}
			}
		}
		else if (_createSlots && this.m_minimumRowCount > 0)
		{
			for (int m = 0; m < this.m_minimumRowCount; m++)
			{
				UIHorizontalList uihorizontalList4 = new UIHorizontalList(this.m_levelList, string.Empty);
				uihorizontalList4.SetHorizontalAlign(0f);
				uihorizontalList4.SetSpacing(_spacing, RelativeTo.ParentWidth);
				uihorizontalList4.RemoveDrawHandler();
				uihorizontalList4.SetDepthOffset(-3f);
				float num6 = 1f - _spacing * (float)(this.m_itemsPerRow - 1);
				for (int n = 0; n < this.m_itemsPerRow; n++)
				{
					UICanvas uicanvas3 = new UICanvas(uihorizontalList4, false, string.Empty, null, string.Empty);
					uicanvas3.SetWidth(num6 / (float)this.m_itemsPerRow, RelativeTo.ParentWidth);
					uicanvas3.SetHeight(num6 / (float)this.m_itemsPerRow * 0.775f, RelativeTo.ParentWidth);
					uicanvas3.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.EmptySpaceRectDrawhandler));
				}
			}
		}
		else
		{
			UITextbox uitextbox = new UITextbox(this.m_levelList, false, string.Empty, _noLevelsTexts, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0385f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
			uitextbox.SetDepthOffset(-3f);
		}
	}

	// Token: 0x04001A0D RID: 6669
	private int m_itemsPerRow;

	// Token: 0x04001A0E RID: 6670
	public UIVerticalList m_levelList;

	// Token: 0x04001A0F RID: 6671
	public List<PsUIProfileLevelButton> m_buttons;

	// Token: 0x04001A10 RID: 6672
	private int m_minimumRowCount;

	// Token: 0x04001A11 RID: 6673
	private bool m_hasTitle;
}
