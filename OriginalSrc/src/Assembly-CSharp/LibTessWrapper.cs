using System;
using System.Runtime.InteropServices;

// Token: 0x020004F3 RID: 1267
internal static class LibTessWrapper
{
	// Token: 0x06002480 RID: 9344
	[DllImport("chipmunk")]
	public static extern IntPtr tessNewTess(IntPtr alloc);

	// Token: 0x06002481 RID: 9345
	[DllImport("chipmunk")]
	public static extern void tessDeleteTess(IntPtr tess);

	// Token: 0x06002482 RID: 9346
	[DllImport("chipmunk")]
	public static extern void tessAddContour(IntPtr tess, int size, float[] pointer, int stride, int count);

	// Token: 0x06002483 RID: 9347
	[DllImport("chipmunk")]
	public static extern int tessTesselate(IntPtr tess, int windingRule, int elementType, int polySize, int vertexSize, float[] normals);

	// Token: 0x06002484 RID: 9348
	[DllImport("chipmunk")]
	public static extern int tessGetVertexCount(IntPtr tess);

	// Token: 0x06002485 RID: 9349
	[DllImport("chipmunk")]
	public static extern void wTessGetVertices(IntPtr tess, float[] array, int vertexSize);

	// Token: 0x06002486 RID: 9350
	[DllImport("chipmunk")]
	public static extern void wTessGetVertexIndices(IntPtr tess, int[] array);

	// Token: 0x06002487 RID: 9351
	[DllImport("chipmunk")]
	public static extern int tessGetElementCount(IntPtr tess);

	// Token: 0x06002488 RID: 9352
	[DllImport("chipmunk")]
	public static extern void wTessGetElements(IntPtr tess, int[] array, int elementSize);

	// Token: 0x04002A5D RID: 10845
	private const string lookFrom = "chipmunk";

	// Token: 0x020004F4 RID: 1268
	public enum WindingRule
	{
		// Token: 0x04002A5F RID: 10847
		EvenOdd,
		// Token: 0x04002A60 RID: 10848
		NonZero,
		// Token: 0x04002A61 RID: 10849
		Positive,
		// Token: 0x04002A62 RID: 10850
		Negative,
		// Token: 0x04002A63 RID: 10851
		AbsGeqTwo
	}

	// Token: 0x020004F5 RID: 1269
	public enum ElementType
	{
		// Token: 0x04002A65 RID: 10853
		Polygons,
		// Token: 0x04002A66 RID: 10854
		ConnectedPolygons,
		// Token: 0x04002A67 RID: 10855
		BoundaryContours
	}
}
