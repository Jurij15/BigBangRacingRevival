using System;
using UnityEngine;

// Token: 0x0200055C RID: 1372
public class SceneManager
{
	// Token: 0x06002809 RID: 10249 RVA: 0x001AAF04 File Offset: 0x001A9304
	public void ChangeScene(IScene _scene, ILoadingScene _loadingScene = null)
	{
		this.m_assetUnloader = null;
		this.m_changeToScene = _scene;
		if (_loadingScene != null)
		{
			if (this.m_loadingScene != null)
			{
				if (this.m_oldLoadingScene != null)
				{
					this.m_oldLoadingScene.Destroy();
					this.m_oldLoadingScene = null;
				}
				this.m_oldLoadingScene = this.m_loadingScene;
			}
			this.m_loadingScene = _loadingScene;
			if (!TouchAreaS.m_disabled)
			{
				TouchAreaS.Disable();
			}
			this.m_loadingScene.ToScene = this.m_changeToScene;
			this.m_loadingScene.FromScene = this.m_currentScene;
		}
		this.m_currentSceneRemoved = false;
		this.m_sceneChanged = false;
		Main.m_currentGame.m_currentScene = _scene;
	}

	// Token: 0x0600280A RID: 10250 RVA: 0x001AAFAC File Offset: 0x001A93AC
	public bool StopSceneChange()
	{
		if (this.m_loadingScene != null)
		{
			if (this.m_changeToScene != null)
			{
				this.m_changeToScene.Destroy();
				this.m_changeToScene = null;
			}
			Main.m_currentGame.m_currentScene = this.m_loadingScene.FromScene;
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState());
			this.m_loadingScene.Destroy();
			this.m_loadingScene = null;
			EntityManager.Update();
			if (TouchAreaS.m_disabled)
			{
				TouchAreaS.Enable();
			}
		}
		return false;
	}

	// Token: 0x0600280B RID: 10251 RVA: 0x001AB045 File Offset: 0x001A9445
	public IScene GetCurrentScene()
	{
		return this.m_currentScene;
	}

	// Token: 0x0600280C RID: 10252 RVA: 0x001AB050 File Offset: 0x001A9450
	private void DestroyCurrentScene()
	{
		if (this.m_currentScene != null)
		{
			this.m_currentScene.Destroy();
			this.m_currentScene = null;
			EntityManager.Update();
		}
		if (this.m_oldLoadingScene != null)
		{
			this.m_oldLoadingScene.Destroy();
			this.m_oldLoadingScene = null;
		}
	}

	// Token: 0x0600280D RID: 10253 RVA: 0x001AB09C File Offset: 0x001A949C
	private void StartAssetUnload()
	{
		if (this.m_assetUnloader == null)
		{
			Debug.Log("Unloading all assets and running GC.", null);
			GC.Collect();
			GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load("UnusedResourceUnloader") as GameObject);
			gameObject.name = "UNUSED RESOURCE UNLOADER";
			this.m_assetUnloader = gameObject.GetComponent<UnloadUnusedAssets>();
		}
		else
		{
			Debug.LogError("FAIL");
		}
	}

	// Token: 0x0600280E RID: 10254 RVA: 0x001AB105 File Offset: 0x001A9505
	private bool isAssetUnloadComplete()
	{
		return this.m_assetUnloader.isDone;
	}

	// Token: 0x0600280F RID: 10255 RVA: 0x001AB112 File Offset: 0x001A9512
	private void RemoveAssetUnloader()
	{
		Object.Destroy(this.m_assetUnloader.gameObject);
		this.m_assetUnloader = null;
	}

	// Token: 0x06002810 RID: 10256 RVA: 0x001AB12C File Offset: 0x001A952C
	public void UpdateLogic()
	{
		if (this.m_changeToScene != null)
		{
			if (this.m_loadingScene != null)
			{
				this.m_loadingScene.Load();
				this.m_changeToScene = null;
			}
			else if (this.m_assetUnloader == null)
			{
				this.DestroyCurrentScene();
				this.StartAssetUnload();
			}
			else if (this.isAssetUnloadComplete())
			{
				this.RemoveAssetUnloader();
				this.m_currentScene = this.m_changeToScene;
				this.m_currentScene.Load();
				if (TouchAreaS.m_disabled)
				{
					TouchAreaS.Enable();
				}
				GC.Collect();
				this.m_changeToScene = null;
			}
		}
		if (this.m_loadingScene != null && this.m_loadingScene.InitComplete)
		{
			if (!this.m_sceneChanged)
			{
				if (this.m_loadingScene.IntroComplete() && !this.m_currentSceneRemoved)
				{
					this.DestroyCurrentScene();
					this.StartAssetUnload();
					this.m_currentSceneRemoved = true;
				}
				if (this.m_currentSceneRemoved)
				{
					if (this.m_assetUnloader != null && this.isAssetUnloadComplete())
					{
						this.RemoveAssetUnloader();
						this.m_loadingScene.ToScene.Load();
						if (TouchAreaS.m_disabled)
						{
							TouchAreaS.Enable();
						}
						GC.Collect();
					}
					if (this.m_loadingScene.ToScene.m_initComplete)
					{
						this.m_currentScene = this.m_loadingScene.ToScene;
						this.m_sceneChanged = true;
						this.m_loadingScene.StartOutro();
					}
				}
			}
			this.m_loadingScene.Update();
			if (this.m_loadingScene.OutroComplete())
			{
				this.m_loadingScene.Destroy();
				this.m_loadingScene = null;
			}
		}
		if (this.m_currentScene != null && this.m_currentScene.m_initComplete)
		{
			this.m_currentScene.Update();
		}
	}

	// Token: 0x04002D75 RID: 11637
	public IScene m_currentScene;

	// Token: 0x04002D76 RID: 11638
	public IScene m_changeToScene;

	// Token: 0x04002D77 RID: 11639
	public ILoadingScene m_loadingScene;

	// Token: 0x04002D78 RID: 11640
	public bool m_sceneChanged;

	// Token: 0x04002D79 RID: 11641
	public bool m_currentSceneRemoved;

	// Token: 0x04002D7A RID: 11642
	public ILoadingScene m_oldLoadingScene;

	// Token: 0x04002D7B RID: 11643
	public UnloadUnusedAssets m_assetUnloader;
}
