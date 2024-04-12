using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006D7 RID: 1751
	internal abstract class BaseReferenceHolder : IDisposable
	{
		// Token: 0x06003262 RID: 12898 RVA: 0x001C9B52 File Offset: 0x001C7F52
		public BaseReferenceHolder(IntPtr pointer)
		{
			this.mSelfPointer = PInvokeUtilities.CheckNonNull(new HandleRef(this, pointer));
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x001C9B6C File Offset: 0x001C7F6C
		protected bool IsDisposed()
		{
			return PInvokeUtilities.IsNull(this.mSelfPointer);
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x001C9B79 File Offset: 0x001C7F79
		protected HandleRef SelfPtr()
		{
			if (this.IsDisposed())
			{
				throw new InvalidOperationException("Attempted to use object after it was cleaned up");
			}
			return this.mSelfPointer;
		}

		// Token: 0x06003265 RID: 12901
		protected abstract void CallDispose(HandleRef selfPointer);

		// Token: 0x06003266 RID: 12902 RVA: 0x001C9B98 File Offset: 0x001C7F98
		~BaseReferenceHolder()
		{
			this.Dispose(true);
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x001C9BC8 File Offset: 0x001C7FC8
		public void Dispose()
		{
			this.Dispose(false);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x001C9BD8 File Offset: 0x001C7FD8
		internal IntPtr AsPointer()
		{
			return this.SelfPtr().Handle;
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x001C9BF4 File Offset: 0x001C7FF4
		private void Dispose(bool fromFinalizer)
		{
			if ((fromFinalizer || !BaseReferenceHolder._refs.ContainsKey(this.mSelfPointer)) && !PInvokeUtilities.IsNull(this.mSelfPointer))
			{
				this.CallDispose(this.mSelfPointer);
				this.mSelfPointer = new HandleRef(this, IntPtr.Zero);
			}
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x001C9C49 File Offset: 0x001C8049
		internal void ReferToMe()
		{
			BaseReferenceHolder._refs[this.SelfPtr()] = this;
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x001C9C5C File Offset: 0x001C805C
		internal void ForgetMe()
		{
			if (BaseReferenceHolder._refs.ContainsKey(this.SelfPtr()))
			{
				BaseReferenceHolder._refs.Remove(this.SelfPtr());
				this.Dispose(false);
			}
		}

		// Token: 0x040032C2 RID: 12994
		private static Dictionary<HandleRef, BaseReferenceHolder> _refs = new Dictionary<HandleRef, BaseReferenceHolder>();

		// Token: 0x040032C3 RID: 12995
		private HandleRef mSelfPointer;
	}
}
