using System;

// Token: 0x020004C6 RID: 1222
public class ChipmunkBodyShape
{
	// Token: 0x060022BE RID: 8894 RVA: 0x00190C20 File Offset: 0x0018F020
	public ChipmunkBodyShape(IntPtr _shapePtr, uint _layers, string _tag)
	{
		this.shapePtr = _shapePtr;
		this.layerMask = _layers;
		this.tag = _tag;
	}

	// Token: 0x040028E3 RID: 10467
	public IntPtr shapePtr;

	// Token: 0x040028E4 RID: 10468
	public uint layerMask;

	// Token: 0x040028E5 RID: 10469
	public string tag;
}
