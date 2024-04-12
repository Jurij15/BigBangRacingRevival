using System;
using UnityEngine;

// Token: 0x0200022A RID: 554
public class PsUserNameInputState : BasicState
{
	// Token: 0x06001035 RID: 4149 RVA: 0x00096E79 File Offset: 0x00095279
	public override void Enter(IStatedObject _parent)
	{
		PsMenuScene.m_lastIState = null;
		this.CreateUI();
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x00096E88 File Offset: 0x00095288
	public void PermissionCallback(bool _success)
	{
		bool flag = NoodlePermissionGranter.ShouldShowRequestPermissionRationale(NoodlePermissionGranter.AndroidPermission.WRITE_EXTERNAL_STORAGE);
		if (flag && !_success && !this.m_retried)
		{
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterLocalSavePrompt), null, null, null, true, true, InitialPage.Center, false, false, false);
			popup.SetAction("Continue", delegate
			{
				popup.Destroy();
				this.m_retried = true;
				NoodlePermissionGranter.GrantPermission(NoodlePermissionGranter.AndroidPermission.WRITE_EXTERNAL_STORAGE);
			});
		}
		else
		{
			PsPersistentData.CheckBackups(null);
			PlayerPrefsX.SetLocalSavePrompted(true);
		}
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x00096F0C File Offset: 0x0009530C
	private static int getSDKInt()
	{
		int @static;
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Build$VERSION"))
		{
			@static = androidJavaClass.GetStatic<int>("SDK_INT");
		}
		return @static;
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x00096F54 File Offset: 0x00095354
	private void CreateUI()
	{
		PsDialogue dialogueByIdentifier = PsMetagameData.GetDialogueByIdentifier("name_input_dialogue");
		PsMenuDialogueFlow psMenuDialogueFlow = new PsMenuDialogueFlow(dialogueByIdentifier, 0f, delegate
		{
			PsUIBaseState psUIBaseState = new PsUIBaseState(typeof(PsUICenterNewUser), null, null, null, false, InitialPage.Center);
			psUIBaseState.SetAction("Exit", delegate
			{
				PsMainMenuState.m_tweenIn = true;
				if (this.m_continueAction != null)
				{
					this.m_continueAction.Invoke();
				}
				else
				{
					Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(this.m_lastState);
				}
			});
			psUIBaseState.SetAction("Back", delegate
			{
				PsMainMenuState.m_tweenIn = true;
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(this.m_lastState);
			});
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUIBaseState);
		}, true, false);
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x00096F88 File Offset: 0x00095388
	public override void Execute()
	{
		if (PsPlanetManager.GetCurrentPlanet() != null && PsPlanetManager.GetCurrentPlanet().m_floatingNodeList != null)
		{
			foreach (PsFloatingPlanetNode psFloatingPlanetNode in PsPlanetManager.GetCurrentPlanet().m_floatingNodeList)
			{
				psFloatingPlanetNode.Update();
			}
		}
		base.Execute();
	}

	// Token: 0x040012FB RID: 4859
	private bool m_retried;

	// Token: 0x040012FC RID: 4860
	public IState m_lastState = new PsMainMenuState();

	// Token: 0x040012FD RID: 4861
	public Action m_continueAction;
}
