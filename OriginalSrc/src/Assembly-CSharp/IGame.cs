using System;

// Token: 0x020004E5 RID: 1253
public interface IGame
{
	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x0600233C RID: 9020
	// (set) Token: 0x0600233D RID: 9021
	string m_projectCode { get; set; }

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x0600233E RID: 9022
	// (set) Token: 0x0600233F RID: 9023
	IScene m_currentScene { get; set; }

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06002340 RID: 9024
	// (set) Token: 0x06002341 RID: 9025
	SceneManager m_sceneManager { get; set; }

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06002342 RID: 9026
	// (set) Token: 0x06002343 RID: 9027
	IState m_currentState { get; set; }

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06002344 RID: 9028
	// (set) Token: 0x06002345 RID: 9029
	StateMachine m_stateMachine { get; set; }

	// Token: 0x06002346 RID: 9030
	void RemoveComponent(IComponent _c);

	// Token: 0x06002347 RID: 9031
	void Initialize(IScene _scene = null);

	// Token: 0x06002348 RID: 9032
	void Update();

	// Token: 0x06002349 RID: 9033
	void OnApplicationPause(bool _pauseStatus);

	// Token: 0x0600234A RID: 9034
	void OnApplicationFocus(bool _focusStatus);

	// Token: 0x0600234B RID: 9035
	void LateUpdate();
}
