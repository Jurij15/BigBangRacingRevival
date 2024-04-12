using System;
using UnityEngine;

// Token: 0x02000228 RID: 552
public class PsCheckBackupsState : BasicState
{
	// Token: 0x06001020 RID: 4128 RVA: 0x000969B8 File Offset: 0x00094DB8
	public override void Enter(IStatedObject _parent)
	{
		PsMenuScene.m_lastIState = null;
		Debug.LogError("Backups: Entered CHECK BACK UP STATE");
		int sdkint = PsCheckBackupsState.getSDKInt();
		bool flag = sdkint > 23 && NoodlePermissionGranter.ShouldShowRequestPermissionRationale(NoodlePermissionGranter.AndroidPermission.WRITE_EXTERNAL_STORAGE);
		bool flag2 = sdkint <= 23 || NoodlePermissionGranter.HasPermission(NoodlePermissionGranter.AndroidPermission.WRITE_EXTERNAL_STORAGE);
		Debug.LogError(string.Concat(new object[]
		{
			"Backups: Android: APILEVLE: ",
			sdkint,
			", askAgain: ",
			flag.ToString(),
			", permissionGranted: ",
			flag2.ToString()
		}));
		if (!flag2 || flag)
		{
			NoodlePermissionGranter.PermissionRequestCallback = new Action<bool>(this.PermissionCallback);
			NoodlePermissionGranter.GrantPermission(NoodlePermissionGranter.AndroidPermission.WRITE_EXTERNAL_STORAGE);
		}
		else
		{
			this.CheckBackups();
		}
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x00096A81 File Offset: 0x00094E81
	private void CheckBackups()
	{
		PlayerPrefsX.SetLocalSavePrompted(true);
		PsPersistentData.CheckBackups(delegate
		{
			PsMainMenuState.m_tweenIn = true;
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsMainMenuState());
		});
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x00096AAC File Offset: 0x00094EAC
	private static int getSDKInt()
	{
		int @static;
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Build$VERSION"))
		{
			@static = androidJavaClass.GetStatic<int>("SDK_INT");
		}
		return @static;
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x00096AF4 File Offset: 0x00094EF4
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
			this.CheckBackups();
		}
	}

	// Token: 0x040012EF RID: 4847
	private bool m_retried;
}
