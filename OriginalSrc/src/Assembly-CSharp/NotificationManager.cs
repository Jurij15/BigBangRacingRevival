using System;
using System.Collections.Generic;

// Token: 0x02000544 RID: 1348
public static class NotificationManager
{
	// Token: 0x060027A8 RID: 10152 RVA: 0x001AA4D3 File Offset: 0x001A88D3
	public static void Initialize()
	{
		NotificationManager.m_notificationQueue = new List<Notification>();
	}

	// Token: 0x060027A9 RID: 10153 RVA: 0x001AA4DF File Offset: 0x001A88DF
	public static void AddNotification(Notification _notification)
	{
		NotificationManager.m_notificationQueue.Add(_notification);
	}

	// Token: 0x060027AA RID: 10154 RVA: 0x001AA4EC File Offset: 0x001A88EC
	public static void RemoveNotification(Notification _notification)
	{
		NotificationManager.m_notificationQueue.Remove(_notification);
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x001AA4FA File Offset: 0x001A88FA
	public static void Pause()
	{
		NotificationManager.m_paused = true;
	}

	// Token: 0x060027AC RID: 10156 RVA: 0x001AA502 File Offset: 0x001A8902
	public static void Resume()
	{
		NotificationManager.m_paused = false;
		NotificationManager.m_lastNotificationEnded = Main.m_gameTimeSinceAppStarted + 1.0;
	}

	// Token: 0x060027AD RID: 10157 RVA: 0x001AA520 File Offset: 0x001A8920
	public static void Update()
	{
		if (NotificationManager.m_paused && NotificationManager.m_currentNotification == null)
		{
			return;
		}
		if (NotificationManager.m_notificationQueue.Count > 0)
		{
			if (NotificationManager.m_currentNotification == null && NotificationManager.m_lastNotificationEnded + NotificationManager.m_waitBetween < Main.m_gameTimeSinceAppStarted)
			{
				NotificationManager.m_currentNotification = NotificationManager.m_notificationQueue[0];
				NotificationManager.m_currentNotification.Start();
			}
			if (NotificationManager.m_currentNotification != null && NotificationManager.m_currentNotification.m_started + NotificationManager.m_currentNotification.m_duration < Main.m_gameTimeSinceAppStarted)
			{
				NotificationManager.m_currentNotification.End();
				NotificationManager.m_currentNotification = null;
				NotificationManager.m_lastNotificationEnded = Main.m_gameTimeSinceAppStarted;
			}
		}
	}

	// Token: 0x04002D12 RID: 11538
	private static List<Notification> m_notificationQueue;

	// Token: 0x04002D13 RID: 11539
	private static Notification m_currentNotification;

	// Token: 0x04002D14 RID: 11540
	private static bool m_paused;

	// Token: 0x04002D15 RID: 11541
	private static double m_lastNotificationEnded;

	// Token: 0x04002D16 RID: 11542
	public static double m_waitBetween = 0.25;
}
