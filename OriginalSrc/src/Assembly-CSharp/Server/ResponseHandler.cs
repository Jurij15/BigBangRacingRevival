using System;

namespace Server
{
	// Token: 0x02000438 RID: 1080
	public class ResponseHandler<T>
	{
		// Token: 0x06001E06 RID: 7686 RVA: 0x001559BC File Offset: 0x00153DBC
		public ResponseHandler(Action<T> _okCallback, Func<HttpC, T> _parser)
		{
			this.m_okCallback = _okCallback;
			this.m_parser = _parser;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x001559D4 File Offset: 0x00153DD4
		public void RequestOk(HttpC req)
		{
			T t = this.m_parser.Invoke(req);
			this.m_okCallback.Invoke(t);
		}

		// Token: 0x040020AC RID: 8364
		private Action<T> m_okCallback;

		// Token: 0x040020AD RID: 8365
		private Func<HttpC, T> m_parser;
	}
}
