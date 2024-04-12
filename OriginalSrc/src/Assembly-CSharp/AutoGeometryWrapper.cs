using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x020004E8 RID: 1256
internal static class AutoGeometryWrapper
{
	// Token: 0x06002353 RID: 9043
	[DllImport("chipmunk")]
	public static extern IntPtr agSimpleSamplerNew(int width, int height, cpBB outputRect, [In] [Out] byte[] data);

	// Token: 0x06002354 RID: 9044
	[DllImport("chipmunk")]
	public static extern void agSimpleSamplerFree(IntPtr sampler);

	// Token: 0x06002355 RID: 9045
	[DllImport("chipmunk")]
	public static extern void agSimpleSamplerSetProperties(IntPtr sampler, float marchTreshold);

	// Token: 0x06002356 RID: 9046
	[DllImport("chipmunk")]
	public static extern IntPtr agCachedTileGetPolylineSet(IntPtr tile);

	// Token: 0x06002357 RID: 9047
	[DllImport("chipmunk")]
	public static extern cpBB agCachedTileGetBB(IntPtr tile);

	// Token: 0x06002358 RID: 9048
	[DllImport("chipmunk")]
	public static extern void agCachedTileSetDirty(IntPtr tile, bool dirty);

	// Token: 0x06002359 RID: 9049
	[DllImport("chipmunk")]
	public static extern int agPolylineSetGetLineCount(IntPtr set);

	// Token: 0x0600235A RID: 9050
	[DllImport("chipmunk")]
	public static extern void agPolylineSetGetLines(IntPtr set, IntPtr[] polylineArray);

	// Token: 0x0600235B RID: 9051
	[DllImport("chipmunk")]
	public static extern int agPolylineGetVertCount(IntPtr polyline);

	// Token: 0x0600235C RID: 9052
	[DllImport("chipmunk")]
	public static extern void agPolyLineGetVertices(IntPtr polyline, [In] [Out] Vector2[] vertexArray);

	// Token: 0x0600235D RID: 9053
	[DllImport("chipmunk")]
	public static extern bool agPolylineIsLooped(IntPtr polyline);

	// Token: 0x0600235E RID: 9054
	[DllImport("chipmunk")]
	public static extern IntPtr agBasicTileCacheNew(IntPtr sampler, IntPtr space, float tileSize, int maxTiles, float samplesPerTile, bool extendedDirtyList);

	// Token: 0x0600235F RID: 9055
	[DllImport("chipmunk")]
	public static extern void agBasicTileCacheFree(IntPtr cache);

	// Token: 0x06002360 RID: 9056
	[DllImport("chipmunk")]
	public static extern void agBasicTileCacheResetCache(IntPtr cache);

	// Token: 0x06002361 RID: 9057
	[DllImport("chipmunk")]
	public static extern void agBasicTileCacheMarkDirtyRect(IntPtr cache, cpBB bounds);

	// Token: 0x06002362 RID: 9058
	[DllImport("chipmunk")]
	public static extern void agBasicTileCacheEnsureRect(IntPtr cache, cpBB bounds);

	// Token: 0x06002363 RID: 9059
	[DllImport("chipmunk")]
	public static extern int agBasicTileCacheGetTileCountInRect(IntPtr cache, cpBB bounds);

	// Token: 0x06002364 RID: 9060
	[DllImport("chipmunk")]
	public static extern void agBasicTileCacheGetTilesInRect(IntPtr cache, cpBB bounds, IntPtr[] tileListArray);

	// Token: 0x06002365 RID: 9061
	[DllImport("chipmunk")]
	public static extern void agBasicTileCacheSetOffsets(IntPtr cache, Vector2 worldOffset, Vector2 tileOffset);

	// Token: 0x06002366 RID: 9062
	[DllImport("chipmunk")]
	public static extern void agBasicTileCacheSetMarchProperties(IntPtr cache, bool marchHard, float simplifyTreshold);

	// Token: 0x06002367 RID: 9063
	[DllImport("chipmunk")]
	public static extern void agBasicTileCacheSetSegmentProperties(IntPtr cache, float segmentRadius, float segmentFriction, float segmentElasticity, ucpShapeFilter segmentFilter, uint segmentCollisionType);

	// Token: 0x06002368 RID: 9064
	[DllImport("chipmunk")]
	public static extern int agBasicTileCacheGetDirtyTileCount(IntPtr cache);

	// Token: 0x06002369 RID: 9065
	[DllImport("chipmunk")]
	public static extern void agBasicTileCacheGetDirtyTileList(IntPtr cache, IntPtr[] tileListArray);

	// Token: 0x04002A2F RID: 10799
	private const string lookFrom = "chipmunk";
}
