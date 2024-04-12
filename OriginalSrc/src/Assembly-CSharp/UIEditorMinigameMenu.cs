using System;
using UnityEngine;

// Token: 0x02000391 RID: 913
public class UIEditorMinigameMenu : UICanvas
{
	// Token: 0x06001A43 RID: 6723 RVA: 0x00125193 File Offset: 0x00123593
	public UIEditorMinigameMenu(UIComponent _parent, string _tag)
		: base(_parent, false, _tag, null, string.Empty)
	{
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(0.14f, RelativeTo.ParentHeight);
		this.SetVerticalAlign(1f);
		this.RemoveDrawHandler();
		this.Update();
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x001251D4 File Offset: 0x001235D4
	public override void Step()
	{
		if (this.m_freeWizardButton.m_hit)
		{
			SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
			EditorBaseState.RemoveTransformGizmo();
			if (PsState.m_freeWizardLastSelectedItemCategory == string.Empty)
			{
				PsState.m_freeWizardLastSelectedItemCategory = UIEditorSelectorNavigator.WIZARD_CATEGORIES[0];
			}
			if (PsState.m_freeWizardLastSelectedItemCategory == UIEditorSelectorNavigator.WIZARD_CATEGORIES[0])
			{
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorPlayer(EditorSelectorContext.FREE_WIZARD));
			}
			else if (PsState.m_freeWizardLastSelectedItemCategory == UIEditorSelectorNavigator.WIZARD_CATEGORIES[1])
			{
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorGameMode(EditorSelectorContext.FREE_WIZARD));
			}
			else
			{
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new EditorSelectorArea(EditorSelectorContext.FREE_WIZARD));
			}
		}
		base.Step();
	}

	// Token: 0x04001CCE RID: 7374
	public UIRectSpriteButton m_freeWizardButton;
}
