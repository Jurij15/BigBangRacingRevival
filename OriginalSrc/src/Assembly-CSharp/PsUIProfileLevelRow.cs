using System;
using System.Collections.Generic;

// Token: 0x0200031C RID: 796
public class PsUIProfileLevelRow : UIHorizontalList
{
	// Token: 0x06001775 RID: 6005 RVA: 0x000FEE78 File Offset: 0x000FD278
	public PsUIProfileLevelRow(UIComponent _parent, string _tag, int _itemsPerRow, int _rowIndex, PsMinigameMetaData[] _levels, Type _gameloopType, ref List<PsUIProfileLevelButton> _buttons, float _spacing = 0.02f, bool _showCreators = false, bool _claimable = false, bool _createSlots = false)
		: base(_parent, _tag)
	{
		this.SetHorizontalAlign(0f);
		this.SetSpacing(_spacing, RelativeTo.ParentWidth);
		this.RemoveDrawHandler();
		this.SetDepthOffset(-3f);
		int num = _levels.Length - _rowIndex * _itemsPerRow;
		float num2 = 1f - _spacing * (float)(_itemsPerRow - 1);
		for (int i = 0; i < _itemsPerRow; i++)
		{
			if (i + 1 > num)
			{
				if (!_createSlots)
				{
					break;
				}
				UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
				uicanvas.SetWidth(num2 / (float)_itemsPerRow, RelativeTo.ParentWidth);
				uicanvas.SetHeight(num2 / (float)_itemsPerRow * 0.775f, RelativeTo.ParentWidth);
				uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.EmptySpaceRectDrawhandler));
			}
			else
			{
				object[] array = new object[] { _levels[_rowIndex * _itemsPerRow + i] };
				PsGameLoop psGameLoop = Activator.CreateInstance(_gameloopType, array) as PsGameLoop;
				PsGameLoop psGameLoop2 = psGameLoop;
				PsUIProfileLevelButton psUIProfileLevelButton = new PsUIProfileLevelButton(this, psGameLoop2, true, _showCreators, _claimable);
				psUIProfileLevelButton.SetWidth(num2 / (float)_itemsPerRow, RelativeTo.ParentWidth);
				psUIProfileLevelButton.SetHeight(num2 / (float)_itemsPerRow * 0.775f, RelativeTo.ParentWidth);
				psUIProfileLevelButton.SetHorizontalAlign(0f);
				_buttons.Add(psUIProfileLevelButton);
			}
		}
	}
}
