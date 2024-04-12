using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[Serializable]
public class Ghost : ISerializable
{
	// Token: 0x060003A5 RID: 933 RVA: 0x00035EA0 File Offset: 0x000342A0
	public Ghost(string _unitClass, Hashtable _upgradeValues, List<string> _characterVisualItems, List<string> _vehicleVisualItems, Dictionary<string, ObscuredInt> _vehicleUpgradeItems, bool _record = true)
	{
		this.m_unitClass = _unitClass;
		this.m_nodes = new Hashtable();
		this.m_upgradeValues = _upgradeValues;
		this.m_characterVisualItems = _characterVisualItems;
		this.m_vehicleVisualItems = _vehicleVisualItems;
		this.m_vehicleUpgradeItems = _vehicleUpgradeItems;
		this.m_frameSkip = 5;
		this.Init(_record);
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00035F20 File Offset: 0x00034320
	public Ghost(SerializationInfo info, StreamingContext ctxt)
	{
		this.Init(false);
		this.m_keyframeCount = (int)info.GetValue("keyframeCount", typeof(int));
		this.m_nodes = (Hashtable)info.GetValue("nodes", typeof(Hashtable));
		try
		{
			this.m_unitClass = info.GetString("unitClass");
		}
		catch
		{
			this.m_unitClass = string.Empty;
		}
		try
		{
			this.m_upgradeValues = (Hashtable)info.GetValue("upgradeValues", typeof(Hashtable));
		}
		catch
		{
			this.m_upgradeValues = null;
		}
		try
		{
			this.m_characterVisualItems = (List<string>)info.GetValue("characterVisualItems", typeof(List<string>));
		}
		catch
		{
			this.m_characterVisualItems = null;
		}
		try
		{
			this.m_vehicleVisualItems = (List<string>)info.GetValue("vehicleVisualItems", typeof(List<string>));
		}
		catch
		{
			this.m_vehicleVisualItems = null;
		}
		try
		{
			this.m_vehicleUpgradeItems = (Dictionary<string, ObscuredInt>)info.GetValue("vehicleUpgradeItems", typeof(Dictionary<string, ObscuredInt>));
		}
		catch
		{
			this.m_vehicleUpgradeItems = null;
		}
		try
		{
			this.m_events = (List<GhostEvent>)info.GetValue("events", typeof(List<GhostEvent>));
		}
		catch
		{
			this.m_events = new List<GhostEvent>();
		}
		try
		{
			this.m_frameSkip = (int)info.GetValue("frameSkip", typeof(int));
		}
		catch
		{
			this.m_frameSkip = 1;
		}
		try
		{
			this.m_lastFrameSkip = (int)info.GetValue("lastFrameSkip", typeof(int));
		}
		catch
		{
			this.m_lastFrameSkip = this.m_frameSkip;
		}
		Debug.Log(string.Concat(new object[] { "Ghost deserialized... keyframes: ", this.m_keyframeCount, " / frameSkip: ", this.m_frameSkip }), null);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000361D8 File Offset: 0x000345D8
	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("keyframeCount", this.m_keyframeCount);
		info.AddValue("nodes", this.m_nodes);
		info.AddValue("unitClass", this.m_unitClass);
		info.AddValue("upgradeValues", this.m_upgradeValues);
		info.AddValue("characterVisualItems", this.m_characterVisualItems);
		info.AddValue("vehicleVisualItems", this.m_vehicleVisualItems);
		info.AddValue("vehicleUpgradeItems", this.m_vehicleUpgradeItems);
		info.AddValue("frameSkip", this.m_frameSkip);
		info.AddValue("lastFrameSkip", this.m_lastFrameSkip);
		info.AddValue("events", this.m_events);
		Debug.Log("Ghost serialized... keyframes: " + this.m_keyframeCount, null);
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x000362AC File Offset: 0x000346AC
	public Ghost DeepCopy()
	{
		Ghost ghost;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			ghost = (Ghost)binaryFormatter.Deserialize(memoryStream);
		}
		return ghost;
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00036308 File Offset: 0x00034708
	public void ResetEvents()
	{
		if (this.m_events != null)
		{
			this.m_runTimeEvents = new List<GhostEvent>(this.m_events);
		}
	}

	// Token: 0x060003AA RID: 938 RVA: 0x00036328 File Offset: 0x00034728
	public void Init(bool _record)
	{
		this.m_recording = _record;
		this.m_playing = !_record;
		this.m_playbackTick = 0f;
		this.m_keyframeCount = 0;
		if (this.m_events != null)
		{
			this.m_runTimeEvents = new List<GhostEvent>(this.m_events);
		}
		if (_record)
		{
			Debug.Log("New ghost created for recording.", null);
		}
		else
		{
			Debug.Log("New playback ghost created.", null);
		}
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00036398 File Offset: 0x00034798
	public void AddNode(string _name, Transform _TC)
	{
		GhostNode ghostNode = new GhostNode(_name, _TC);
		this.m_nodes.Add(_name, ghostNode);
		Debug.Log("Node " + _name + " added to ghost.", null);
		ghostNode.AddKeyFrame();
	}

	// Token: 0x060003AC RID: 940 RVA: 0x000363D8 File Offset: 0x000347D8
	public void AddEvent(GhostEventType _type, int _tick)
	{
		GhostEvent ghostEvent = default(GhostEvent);
		ghostEvent.m_event = _type;
		ghostEvent.m_tick = _tick;
		if (this.m_events != null)
		{
			this.m_events.Add(ghostEvent);
		}
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00036414 File Offset: 0x00034814
	public void AddGoalFrame(int _tick)
	{
		if (this.m_keyframeCount * this.m_frameSkip < _tick)
		{
			this.m_lastFrameSkip = _tick - this.m_keyframeCount * this.m_frameSkip;
			IEnumerator enumerator = this.m_nodes.Values.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					GhostNode ghostNode = (GhostNode)obj;
					ghostNode.AddKeyFrame();
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
			this.m_keyframeCount++;
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x000364B4 File Offset: 0x000348B4
	public void SlowDownPlayback(float _seconds = 2f, float _amount = 0.3f, Action _callback = null)
	{
		this.m_slowDownSeconds = _seconds;
		this.m_slowDownAmount = _amount;
		this.m_slowdownCallback = _callback;
	}

	// Token: 0x060003AF RID: 943 RVA: 0x000364CB File Offset: 0x000348CB
	public void SpeedUpPlayback(float _seconds = 2f, float _amount = 1.3f)
	{
		this.m_speedUpSeconds = _seconds;
		this.m_speedUpAmount = _amount;
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x000364DC File Offset: 0x000348DC
	private float GetPlaybackSpeed(float _timeScale)
	{
		return Main.m_timeScale / (float)this.m_frameSkip;
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x000364F8 File Offset: 0x000348F8
	public virtual void UpdateRecording(bool _ignoreTimescale = false)
	{
		if (this.m_recording)
		{
			bool flag = Main.m_timeScale >= (float)this.m_frameSkip;
			if (!flag)
			{
				this.m_scaledTick += Main.m_timeScale;
				if (this.m_scaledTick >= (float)this.m_frameSkip)
				{
					flag = true;
					this.m_scaledTick -= (float)this.m_frameSkip;
				}
			}
			else
			{
				this.m_scaledTick = 0f;
			}
			if (flag)
			{
				IEnumerator enumerator = this.m_nodes.Values.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						GhostNode ghostNode = (GhostNode)obj;
						ghostNode.AddKeyFrame();
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = enumerator as IDisposable) != null)
					{
						disposable.Dispose();
					}
				}
				this.m_keyframeCount++;
			}
			if (this.m_keyframeCount >= 7200)
			{
				this.StopRecord();
			}
		}
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x000365F8 File Offset: 0x000349F8
	public virtual void UpdatePlayback(float _timeScale)
	{
		if (this.m_playing && _timeScale > 0f)
		{
			float num = Main.m_timeScale / (float)this.m_frameSkip * _timeScale;
			if (this.m_playbackTick > (float)(this.m_keyframeCount - 2))
			{
				num = Main.m_timeScale / (float)this.m_lastFrameSkip * _timeScale;
			}
			this.m_playbackTick = Mathf.Min(this.m_playbackTick + num, (float)(this.m_keyframeCount - 1));
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0003666B File Offset: 0x00034A6B
	public void StopRecord()
	{
		if (this.m_recording)
		{
			this.m_recording = false;
		}
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0003667F File Offset: 0x00034A7F
	public float GetPlaybackTick()
	{
		return this.m_playbackTick * (float)this.m_frameSkip;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x0003668F File Offset: 0x00034A8F
	public bool PlaybackEnded()
	{
		return this.m_playbackTick == (float)(this.m_keyframeCount - 1);
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x000366A2 File Offset: 0x00034AA2
	public Vector3 GetCurrentPosition(GhostNode _node)
	{
		return this.GetCurrentPosition(_node, this.m_playbackTick, -1);
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x000366B2 File Offset: 0x00034AB2
	public float GetCurrentRotation(GhostNode _node)
	{
		return this.GetCurrentRotation(_node, this.m_playbackTick);
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x000366C4 File Offset: 0x00034AC4
	public Vector2 GetCurrentVelocity(GhostNode _node, float _tick)
	{
		int num = Mathf.FloorToInt(_tick);
		int num2 = Mathf.CeilToInt(_tick);
		Vector2 keyFramePos = _node.GetKeyFramePos(num);
		Vector2 keyFramePos2 = _node.GetKeyFramePos(num2);
		return keyFramePos2 - keyFramePos;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x000366F8 File Offset: 0x00034AF8
	public Vector3 GetCurrentPosition(GhostNode _node, float _tick, int _clampIndex = -1)
	{
		float num = _tick - (float)Mathf.FloorToInt(_tick);
		if (num == 0f)
		{
			return _node.GetKeyFramePos(Mathf.FloorToInt(_tick));
		}
		int num2 = Mathf.FloorToInt(_tick);
		int num3 = Mathf.CeilToInt(_tick);
		Vector2 keyFramePos = _node.GetKeyFramePos(Mathf.Max(num2 - 1, 0));
		Vector2 keyFramePos2 = _node.GetKeyFramePos(num2);
		Vector2 keyFramePos3 = _node.GetKeyFramePos(num3);
		Vector2 keyFramePos4 = _node.GetKeyFramePos(Mathf.Min(num3 + 1, this.m_keyframeCount - 1));
		if (_clampIndex == 1)
		{
			return keyFramePos2;
		}
		if (_clampIndex == 2)
		{
			return keyFramePos3;
		}
		return ToolBox.PointOnBezierSpline(keyFramePos, keyFramePos2, keyFramePos3, keyFramePos4, num);
	}

	// Token: 0x060003BA RID: 954 RVA: 0x000367A8 File Offset: 0x00034BA8
	public float GetCurrentRotation(GhostNode _node, float _tick)
	{
		float num = _tick - (float)Mathf.FloorToInt(_tick);
		if (num != 0f)
		{
			int num2 = Mathf.FloorToInt(_tick);
			int num3 = Mathf.CeilToInt(_tick);
			float keyFrameRotation = _node.GetKeyFrameRotation(num2);
			float keyFrameRotation2 = _node.GetKeyFrameRotation(num3);
			float num4 = keyFrameRotation2 - keyFrameRotation;
			if (num4 > 180f)
			{
				num4 -= 360f;
			}
			else if (num4 < -180f)
			{
				num4 += 360f;
			}
			return keyFrameRotation + num4 * num;
		}
		return _node.GetKeyFrameRotation(Mathf.FloorToInt(_tick));
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00036834 File Offset: 0x00034C34
	public int GetNextKeyframe(string _nodeName, int _startKeyframe, float _distanceToTravel)
	{
		_distanceToTravel *= _distanceToTravel;
		GhostNode ghostNode = this.m_nodes[_nodeName] as GhostNode;
		int num = _startKeyframe;
		Vector2 keyFramePos = ghostNode.GetKeyFramePos(_startKeyframe);
		for (int i = _startKeyframe + 1; i < this.m_keyframeCount; i++)
		{
			Vector2 keyFramePos2 = ghostNode.GetKeyFramePos(i);
			float sqrMagnitude = (keyFramePos2 - keyFramePos).sqrMagnitude;
			if (sqrMagnitude >= _distanceToTravel)
			{
				num = i;
				break;
			}
		}
		return num;
	}

	// Token: 0x060003BC RID: 956 RVA: 0x000368B4 File Offset: 0x00034CB4
	public float GetPathWorldDistance(string _nodeName)
	{
		if (this.m_keyframeCount == 0)
		{
			return 0f;
		}
		GhostNode ghostNode = this.m_nodes[_nodeName] as GhostNode;
		float num = 0f;
		Vector2 vector = ghostNode.GetKeyFramePos(0);
		for (int i = 1; i < this.m_keyframeCount; i++)
		{
			Vector2 keyFramePos = ghostNode.GetKeyFramePos(i);
			num += (keyFramePos - vector).magnitude;
			vector = keyFramePos;
		}
		return num;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x0003692C File Offset: 0x00034D2C
	public List<GhostEventType> GetEventsOnTick(int _tick)
	{
		if (this.m_runTimeEvents != null && this.m_runTimeEvents.Count > 0)
		{
			List<GhostEventType> list = new List<GhostEventType>();
			int num = 0;
			for (int i = 0; i < this.m_runTimeEvents.Count; i++)
			{
				if (this.m_runTimeEvents[i].m_tick > _tick)
				{
					break;
				}
				list.Add(this.m_runTimeEvents[i].m_event);
				num++;
			}
			this.m_runTimeEvents.RemoveRange(0, num);
			return list;
		}
		return null;
	}

	// Token: 0x060003BE RID: 958 RVA: 0x000369CC File Offset: 0x00034DCC
	public List<int> GetEventGameTicks(GhostEventType _eventType)
	{
		List<int> list = new List<int>();
		if (this.m_events == null)
		{
			return list;
		}
		for (int i = 0; i < this.m_events.Count; i++)
		{
			if (this.m_events[i].m_event == _eventType)
			{
				list.Add(this.m_events[i].m_tick);
			}
		}
		return list;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00036A40 File Offset: 0x00034E40
	public List<int> GetRuntimeEventGameTicks(GhostEventType _eventType)
	{
		List<int> list = new List<int>();
		if (this.m_runTimeEvents == null)
		{
			return list;
		}
		for (int i = 0; i < this.m_runTimeEvents.Count; i++)
		{
			if (this.m_runTimeEvents[i].m_event == _eventType)
			{
				list.Add(this.m_runTimeEvents[i].m_tick);
			}
		}
		return list;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00036AB4 File Offset: 0x00034EB4
	public virtual void Destroy()
	{
		if (this.m_recording)
		{
			Debug.Log("Destroying recording ghost", null);
		}
		else
		{
			Debug.Log("Destroying playback ghost", null);
		}
		this.m_nodes.Clear();
		this.m_nodes = null;
		if (this.m_upgradeValues != null)
		{
			this.m_upgradeValues.Clear();
			this.m_upgradeValues = null;
		}
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00036B16 File Offset: 0x00034F16
	public static byte[] SerializeToBytes(Ghost _ghost)
	{
		return Ghost.SerializeToStream(_ghost).ToArray();
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00036B24 File Offset: 0x00034F24
	public static MemoryStream SerializeToStream(Ghost _ghost)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		MemoryStream memoryStream = new MemoryStream();
		binaryFormatter.Serialize(memoryStream, _ghost);
		return memoryStream;
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00036B48 File Offset: 0x00034F48
	public static Ghost DeSerializeFromBytes(byte[] _bytes)
	{
		Debug.Log("Deserializing ghost...", null);
		MemoryStream memoryStream = new MemoryStream(_bytes, 0, _bytes.Length, false, true);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		Ghost ghost = null;
		try
		{
			ghost = (Ghost)binaryFormatter.Deserialize(memoryStream);
		}
		catch (InvalidCastException ex)
		{
			Debug.LogWarning(ex.Message);
			ghost = null;
		}
		memoryStream.Close();
		return ghost;
	}

	// Token: 0x040004C1 RID: 1217
	public string m_name = "Unknown";

	// Token: 0x040004C2 RID: 1218
	public string m_unitClass;

	// Token: 0x040004C3 RID: 1219
	public bool m_recording;

	// Token: 0x040004C4 RID: 1220
	public bool m_playing;

	// Token: 0x040004C5 RID: 1221
	public Hashtable m_nodes;

	// Token: 0x040004C6 RID: 1222
	public float m_playbackTick;

	// Token: 0x040004C7 RID: 1223
	public int m_keyframeCount;

	// Token: 0x040004C8 RID: 1224
	public int m_frameSkip;

	// Token: 0x040004C9 RID: 1225
	public int m_lastFrameSkip;

	// Token: 0x040004CA RID: 1226
	public List<string> m_characterVisualItems;

	// Token: 0x040004CB RID: 1227
	public List<string> m_vehicleVisualItems;

	// Token: 0x040004CC RID: 1228
	public Dictionary<string, ObscuredInt> m_vehicleUpgradeItems;

	// Token: 0x040004CD RID: 1229
	public List<GhostEvent> m_events = new List<GhostEvent>();

	// Token: 0x040004CE RID: 1230
	public List<GhostEvent> m_runTimeEvents;

	// Token: 0x040004CF RID: 1231
	public Hashtable m_upgradeValues;

	// Token: 0x040004D0 RID: 1232
	public float m_slowDownSeconds;

	// Token: 0x040004D1 RID: 1233
	private float m_slowDownAmount = 0.3f;

	// Token: 0x040004D2 RID: 1234
	private Action m_slowdownCallback;

	// Token: 0x040004D3 RID: 1235
	public float m_speedUpSeconds;

	// Token: 0x040004D4 RID: 1236
	private float m_speedUpAmount = 1.2f;

	// Token: 0x040004D5 RID: 1237
	private float m_scaledTick;
}
