using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006F0 RID: 1776
	internal class NativeEvent : BaseReferenceHolder, IEvent
	{
		// Token: 0x06003330 RID: 13104 RVA: 0x001CBA18 File Offset: 0x001C9E18
		internal NativeEvent(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x001CBA21 File Offset: 0x001C9E21
		public string Id
		{
			get
			{
				return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => Event.Event_Id(base.SelfPtr(), out_string, out_size));
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x001CBA34 File Offset: 0x001C9E34
		public string Name
		{
			get
			{
				return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => Event.Event_Name(base.SelfPtr(), out_string, out_size));
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x001CBA47 File Offset: 0x001C9E47
		public string Description
		{
			get
			{
				return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => Event.Event_Description(base.SelfPtr(), out_string, out_size));
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x001CBA5A File Offset: 0x001C9E5A
		public string ImageUrl
		{
			get
			{
				return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => Event.Event_ImageUrl(base.SelfPtr(), out_string, out_size));
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x001CBA6D File Offset: 0x001C9E6D
		public ulong CurrentCount
		{
			get
			{
				return Event.Event_Count(base.SelfPtr());
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x001CBA7C File Offset: 0x001C9E7C
		public EventVisibility Visibility
		{
			get
			{
				Types.EventVisibility eventVisibility = Event.Event_Visibility(base.SelfPtr());
				if (eventVisibility == Types.EventVisibility.HIDDEN)
				{
					return EventVisibility.Hidden;
				}
				if (eventVisibility != Types.EventVisibility.REVEALED)
				{
					throw new InvalidOperationException("Unknown visibility: " + eventVisibility);
				}
				return EventVisibility.Revealed;
			}
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x001CBAC1 File Offset: 0x001C9EC1
		protected override void CallDispose(HandleRef selfPointer)
		{
			Event.Event_Dispose(selfPointer);
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x001CBACC File Offset: 0x001C9ECC
		public override string ToString()
		{
			if (base.IsDisposed())
			{
				return "[NativeEvent: DELETED]";
			}
			return string.Format("[NativeEvent: Id={0}, Name={1}, Description={2}, ImageUrl={3}, CurrentCount={4}, Visibility={5}]", new object[] { this.Id, this.Name, this.Description, this.ImageUrl, this.CurrentCount, this.Visibility });
		}
	}
}
