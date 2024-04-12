using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x02000609 RID: 1545
	public class PlayGamesHelperObject : MonoBehaviour
	{
		// Token: 0x06002D42 RID: 11586 RVA: 0x001C014C File Offset: 0x001BE54C
		public static void CreateObject()
		{
			if (PlayGamesHelperObject.instance != null)
			{
				return;
			}
			if (Application.isPlaying)
			{
				GameObject gameObject = new GameObject("PlayGames_QueueRunner");
				Object.DontDestroyOnLoad(gameObject);
				PlayGamesHelperObject.instance = gameObject.AddComponent<PlayGamesHelperObject>();
			}
			else
			{
				PlayGamesHelperObject.instance = new PlayGamesHelperObject();
				PlayGamesHelperObject.sIsDummy = true;
			}
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x001C01A5 File Offset: 0x001BE5A5
		public void Awake()
		{
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x001C01B2 File Offset: 0x001BE5B2
		public void OnDisable()
		{
			if (PlayGamesHelperObject.instance == this)
			{
				PlayGamesHelperObject.instance = null;
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x001C01CC File Offset: 0x001BE5CC
		public static void RunCoroutine(IEnumerator action)
		{
			if (PlayGamesHelperObject.instance != null)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					PlayGamesHelperObject.instance.StartCoroutine(action);
				});
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x001C0208 File Offset: 0x001BE608
		public static void RunOnGameThread(Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (PlayGamesHelperObject.sIsDummy)
			{
				return;
			}
			object obj = PlayGamesHelperObject.sQueue;
			lock (obj)
			{
				PlayGamesHelperObject.sQueue.Add(action);
				PlayGamesHelperObject.sQueueEmpty = false;
			}
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x001C026C File Offset: 0x001BE66C
		public void Update()
		{
			if (PlayGamesHelperObject.sIsDummy || PlayGamesHelperObject.sQueueEmpty)
			{
				return;
			}
			this.localQueue.Clear();
			object obj = PlayGamesHelperObject.sQueue;
			lock (obj)
			{
				this.localQueue.AddRange(PlayGamesHelperObject.sQueue);
				PlayGamesHelperObject.sQueue.Clear();
				PlayGamesHelperObject.sQueueEmpty = true;
			}
			for (int i = 0; i < this.localQueue.Count; i++)
			{
				this.localQueue[i].Invoke();
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x001C0314 File Offset: 0x001BE714
		public void OnApplicationFocus(bool focused)
		{
			foreach (Action<bool> action in PlayGamesHelperObject.sFocusCallbackList)
			{
				try
				{
					action.Invoke(focused);
				}
				catch (Exception ex)
				{
					Debug.LogError("Exception in OnApplicationFocus:" + ex.Message + "\n" + ex.StackTrace);
				}
			}
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x001C03A8 File Offset: 0x001BE7A8
		public void OnApplicationPause(bool paused)
		{
			foreach (Action<bool> action in PlayGamesHelperObject.sPauseCallbackList)
			{
				try
				{
					action.Invoke(paused);
				}
				catch (Exception ex)
				{
					Debug.LogError("Exception in OnApplicationPause:" + ex.Message + "\n" + ex.StackTrace);
				}
			}
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x001C043C File Offset: 0x001BE83C
		public static void AddFocusCallback(Action<bool> callback)
		{
			if (!PlayGamesHelperObject.sFocusCallbackList.Contains(callback))
			{
				PlayGamesHelperObject.sFocusCallbackList.Add(callback);
			}
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x001C0459 File Offset: 0x001BE859
		public static bool RemoveFocusCallback(Action<bool> callback)
		{
			return PlayGamesHelperObject.sFocusCallbackList.Remove(callback);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x001C0466 File Offset: 0x001BE866
		public static void AddPauseCallback(Action<bool> callback)
		{
			if (!PlayGamesHelperObject.sPauseCallbackList.Contains(callback))
			{
				PlayGamesHelperObject.sPauseCallbackList.Add(callback);
			}
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x001C0483 File Offset: 0x001BE883
		public static bool RemovePauseCallback(Action<bool> callback)
		{
			return PlayGamesHelperObject.sPauseCallbackList.Remove(callback);
		}

		// Token: 0x04003154 RID: 12628
		private static PlayGamesHelperObject instance = null;

		// Token: 0x04003155 RID: 12629
		private static bool sIsDummy = false;

		// Token: 0x04003156 RID: 12630
		private static List<Action> sQueue = new List<Action>();

		// Token: 0x04003157 RID: 12631
		private List<Action> localQueue = new List<Action>();

		// Token: 0x04003158 RID: 12632
		private static volatile bool sQueueEmpty = true;

		// Token: 0x04003159 RID: 12633
		private static List<Action<bool>> sPauseCallbackList = new List<Action<bool>>();

		// Token: 0x0400315A RID: 12634
		private static List<Action<bool>> sFocusCallbackList = new List<Action<bool>>();
	}
}
