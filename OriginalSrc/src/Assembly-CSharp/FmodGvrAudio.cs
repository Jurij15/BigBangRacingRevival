using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000730 RID: 1840
public static class FmodGvrAudio
{
	// Token: 0x06003540 RID: 13632 RVA: 0x001CF6C4 File Offset: 0x001CDAC4
	public static void UpdateAudioRoom(FmodGvrAudioRoom room, bool roomEnabled)
	{
		if (roomEnabled)
		{
			if (!FmodGvrAudio.enabledRooms.Contains(room))
			{
				FmodGvrAudio.enabledRooms.Add(room);
			}
		}
		else
		{
			FmodGvrAudio.enabledRooms.Remove(room);
		}
		if (FmodGvrAudio.enabledRooms.Count > 0)
		{
			FmodGvrAudioRoom fmodGvrAudioRoom = FmodGvrAudio.enabledRooms[FmodGvrAudio.enabledRooms.Count - 1];
			FmodGvrAudio.RoomProperties roomProperties = FmodGvrAudio.GetRoomProperties(fmodGvrAudioRoom);
			IntPtr intPtr = Marshal.AllocHGlobal(FmodGvrAudio.roomPropertiesSize);
			Marshal.StructureToPtr(roomProperties, intPtr, false);
			FmodGvrAudio.ListenerPlugin.setParameterData(FmodGvrAudio.roomPropertiesIndex, FmodGvrAudio.GetBytes(intPtr, FmodGvrAudio.roomPropertiesSize));
			Marshal.FreeHGlobal(intPtr);
		}
		else
		{
			FmodGvrAudio.ListenerPlugin.setParameterData(FmodGvrAudio.roomPropertiesIndex, FmodGvrAudio.GetBytes(IntPtr.Zero, 0));
		}
	}

	// Token: 0x06003541 RID: 13633 RVA: 0x001CF78C File Offset: 0x001CDB8C
	public static bool IsListenerInsideRoom(FmodGvrAudioRoom room)
	{
		VECTOR vector;
		RuntimeManager.LowlevelSystem.get3DListenerAttributes(0, ref FmodGvrAudio.listenerPositionFmod, ref vector, ref vector, ref vector);
		Vector3 vector2;
		vector2..ctor(FmodGvrAudio.listenerPositionFmod.x, FmodGvrAudio.listenerPositionFmod.y, FmodGvrAudio.listenerPositionFmod.z);
		Vector3 vector3 = vector2 - room.transform.position;
		Quaternion quaternion = Quaternion.Inverse(room.transform.rotation);
		FmodGvrAudio.bounds.size = Vector3.Scale(room.transform.lossyScale, room.size);
		return FmodGvrAudio.bounds.Contains(quaternion * vector3);
	}

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06003542 RID: 13634 RVA: 0x001CF829 File Offset: 0x001CDC29
	private static DSP ListenerPlugin
	{
		get
		{
			if (FmodGvrAudio.listenerPlugin == null)
			{
				FmodGvrAudio.listenerPlugin = FmodGvrAudio.Initialize();
			}
			return FmodGvrAudio.listenerPlugin;
		}
	}

	// Token: 0x06003543 RID: 13635 RVA: 0x001CF84A File Offset: 0x001CDC4A
	private static float ConvertAmplitudeFromDb(float db)
	{
		return Mathf.Pow(10f, 0.05f * db);
	}

	// Token: 0x06003544 RID: 13636 RVA: 0x001CF860 File Offset: 0x001CDC60
	private static void ConvertAudioTransformFromUnity(ref Vector3 position, ref Quaternion rotation)
	{
		Matrix4x4 matrix4x = Matrix4x4.TRS(position, rotation, Vector3.one);
		matrix4x = FmodGvrAudio.flipZ * matrix4x * FmodGvrAudio.flipZ;
		position = matrix4x.GetColumn(3);
		rotation = Quaternion.LookRotation(matrix4x.GetColumn(2), matrix4x.GetColumn(1));
	}

	// Token: 0x06003545 RID: 13637 RVA: 0x001CF8D4 File Offset: 0x001CDCD4
	private static byte[] GetBytes(IntPtr ptr, int length)
	{
		if (ptr != IntPtr.Zero)
		{
			byte[] array = new byte[length];
			Marshal.Copy(ptr, array, 0, length);
			return array;
		}
		return new byte[1];
	}

	// Token: 0x06003546 RID: 13638 RVA: 0x001CF90C File Offset: 0x001CDD0C
	private static FmodGvrAudio.RoomProperties GetRoomProperties(FmodGvrAudioRoom room)
	{
		Vector3 position = room.transform.position;
		Quaternion rotation = room.transform.rotation;
		Vector3 vector = Vector3.Scale(room.transform.lossyScale, room.size);
		FmodGvrAudio.ConvertAudioTransformFromUnity(ref position, ref rotation);
		FmodGvrAudio.RoomProperties roomProperties;
		roomProperties.positionX = position.x;
		roomProperties.positionY = position.y;
		roomProperties.positionZ = position.z;
		roomProperties.rotationX = rotation.x;
		roomProperties.rotationY = rotation.y;
		roomProperties.rotationZ = rotation.z;
		roomProperties.rotationW = rotation.w;
		roomProperties.dimensionsX = vector.x;
		roomProperties.dimensionsY = vector.y;
		roomProperties.dimensionsZ = vector.z;
		roomProperties.materialLeft = room.leftWall;
		roomProperties.materialRight = room.rightWall;
		roomProperties.materialBottom = room.floor;
		roomProperties.materialTop = room.ceiling;
		roomProperties.materialFront = room.frontWall;
		roomProperties.materialBack = room.backWall;
		roomProperties.reverbGain = FmodGvrAudio.ConvertAmplitudeFromDb(room.reverbGainDb);
		roomProperties.reverbTime = room.reverbTime;
		roomProperties.reverbBrightness = room.reverbBrightness;
		roomProperties.reflectionScalar = room.reflectivity;
		return roomProperties;
	}

	// Token: 0x06003547 RID: 13639 RVA: 0x001CFA68 File Offset: 0x001CDE68
	private static DSP Initialize()
	{
		int num = 0;
		Bank[] array = null;
		RuntimeManager.StudioSystem.getBankCount(ref num);
		RuntimeManager.StudioSystem.getBankList(ref array);
		for (int i = 0; i < num; i++)
		{
			int num2 = 0;
			Bus[] array2 = null;
			array[i].getBusCount(ref num2);
			array[i].getBusList(ref array2);
			RuntimeManager.StudioSystem.flushCommands();
			for (int j = 0; j < num2; j++)
			{
				string text = null;
				array2[j].getPath(ref text);
				RuntimeManager.StudioSystem.getBus(text, ref array2[j]);
				RuntimeManager.StudioSystem.flushCommands();
				ChannelGroup channelGroup = null;
				array2[j].getChannelGroup(ref channelGroup);
				RuntimeManager.StudioSystem.flushCommands();
				if (channelGroup != null)
				{
					int num3 = 0;
					DSP dsp = null;
					channelGroup.getNumDSPs(ref num3);
					for (int k = 0; k < num3; k++)
					{
						channelGroup.getDSP(k, ref dsp);
						StringBuilder stringBuilder = new StringBuilder(32);
						int num4 = 0;
						uint num5 = 0U;
						dsp.getInfo(stringBuilder, ref num5, ref num4, ref num4, ref num4);
						if (stringBuilder.ToString().Equals(FmodGvrAudio.listenerPluginName) && dsp.isValid())
						{
							return dsp;
						}
					}
				}
			}
		}
		Debug.LogError(FmodGvrAudio.listenerPluginName + " not found in the FMOD project.");
		return null;
	}

	// Token: 0x04003346 RID: 13126
	public const float maxGainDb = 24f;

	// Token: 0x04003347 RID: 13127
	public const float minGainDb = -24f;

	// Token: 0x04003348 RID: 13128
	public const float maxReverbBrightness = 1f;

	// Token: 0x04003349 RID: 13129
	public const float minReverbBrightness = -1f;

	// Token: 0x0400334A RID: 13130
	public const float maxReverbTime = 3f;

	// Token: 0x0400334B RID: 13131
	public const float maxReflectivity = 2f;

	// Token: 0x0400334C RID: 13132
	private static readonly Matrix4x4 flipZ = Matrix4x4.Scale(new Vector3(1f, 1f, -1f));

	// Token: 0x0400334D RID: 13133
	private static readonly string listenerPluginName = "Google GVR Listener";

	// Token: 0x0400334E RID: 13134
	private static readonly int roomPropertiesSize = Marshal.SizeOf(typeof(FmodGvrAudio.RoomProperties));

	// Token: 0x0400334F RID: 13135
	private static readonly int roomPropertiesIndex = 1;

	// Token: 0x04003350 RID: 13136
	private static Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

	// Token: 0x04003351 RID: 13137
	private static List<FmodGvrAudioRoom> enabledRooms = new List<FmodGvrAudioRoom>();

	// Token: 0x04003352 RID: 13138
	private static VECTOR listenerPositionFmod = default(VECTOR);

	// Token: 0x04003353 RID: 13139
	private static DSP listenerPlugin = null;

	// Token: 0x02000731 RID: 1841
	private struct RoomProperties
	{
		// Token: 0x04003354 RID: 13140
		public float positionX;

		// Token: 0x04003355 RID: 13141
		public float positionY;

		// Token: 0x04003356 RID: 13142
		public float positionZ;

		// Token: 0x04003357 RID: 13143
		public float rotationX;

		// Token: 0x04003358 RID: 13144
		public float rotationY;

		// Token: 0x04003359 RID: 13145
		public float rotationZ;

		// Token: 0x0400335A RID: 13146
		public float rotationW;

		// Token: 0x0400335B RID: 13147
		public float dimensionsX;

		// Token: 0x0400335C RID: 13148
		public float dimensionsY;

		// Token: 0x0400335D RID: 13149
		public float dimensionsZ;

		// Token: 0x0400335E RID: 13150
		public FmodGvrAudioRoom.SurfaceMaterial materialLeft;

		// Token: 0x0400335F RID: 13151
		public FmodGvrAudioRoom.SurfaceMaterial materialRight;

		// Token: 0x04003360 RID: 13152
		public FmodGvrAudioRoom.SurfaceMaterial materialBottom;

		// Token: 0x04003361 RID: 13153
		public FmodGvrAudioRoom.SurfaceMaterial materialTop;

		// Token: 0x04003362 RID: 13154
		public FmodGvrAudioRoom.SurfaceMaterial materialFront;

		// Token: 0x04003363 RID: 13155
		public FmodGvrAudioRoom.SurfaceMaterial materialBack;

		// Token: 0x04003364 RID: 13156
		public float reflectionScalar;

		// Token: 0x04003365 RID: 13157
		public float reverbGain;

		// Token: 0x04003366 RID: 13158
		public float reverbTime;

		// Token: 0x04003367 RID: 13159
		public float reverbBrightness;
	}
}
