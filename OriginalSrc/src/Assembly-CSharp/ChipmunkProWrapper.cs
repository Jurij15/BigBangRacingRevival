using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x020004F2 RID: 1266
internal static class ChipmunkProWrapper
{
	// Token: 0x06002374 RID: 9076
	[DllImport("chipmunk")]
	public static extern void ucpInitialize();

	// Token: 0x06002375 RID: 9077
	[DllImport("chipmunk")]
	public static extern void ucpFree();

	// Token: 0x06002376 RID: 9078
	[DllImport("chipmunk")]
	public static extern int ucpGetTotalBodyCount();

	// Token: 0x06002377 RID: 9079
	[DllImport("chipmunk")]
	public static extern int ucpGetTotalConstraintCount();

	// Token: 0x06002378 RID: 9080
	[DllImport("chipmunk")]
	public static extern IntPtr ucpAddBody(float mass, float inertia, int componentIndex);

	// Token: 0x06002379 RID: 9081
	[DllImport("chipmunk")]
	public static extern IntPtr ucpAddKinematicBody(int componentIndex);

	// Token: 0x0600237A RID: 9082
	[DllImport("chipmunk")]
	public static extern IntPtr ucpAddStaticBody(int componentIndex);

	// Token: 0x0600237B RID: 9083
	[DllImport("chipmunk")]
	public static extern IntPtr ucpAddConstraint(IntPtr constraint, int componentIndex);

	// Token: 0x0600237C RID: 9084
	[DllImport("chipmunk")]
	public static extern int ucpRemoveBody(IntPtr body, [In] [Out] ucpConstraintData[] results, int maxResults);

	// Token: 0x0600237D RID: 9085
	[DllImport("chipmunk")]
	public static extern void ucpRemoveShape(IntPtr shape);

	// Token: 0x0600237E RID: 9086
	[DllImport("chipmunk")]
	public static extern void ucpRemoveConstraint(IntPtr constraint);

	// Token: 0x0600237F RID: 9087
	[DllImport("chipmunk")]
	public static extern int ucpRemoveConstraintsFromBody(IntPtr body, [In] [Out] ucpConstraintData[] results, int maxResults);

	// Token: 0x06002380 RID: 9088
	[DllImport("chipmunk")]
	public static extern bool ucpBodyHasCustomProperties(IntPtr body);

	// Token: 0x06002381 RID: 9089
	[DllImport("chipmunk")]
	public static extern float ucpBodyGetAngularDamp(IntPtr body);

	// Token: 0x06002382 RID: 9090
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyGetGravity(IntPtr body);

	// Token: 0x06002383 RID: 9091
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyGetLinearDamp(IntPtr body);

	// Token: 0x06002384 RID: 9092
	[DllImport("chipmunk")]
	public static extern void ucpBodySetAngularDamp(IntPtr body, float angularDamp);

	// Token: 0x06002385 RID: 9093
	[DllImport("chipmunk")]
	public static extern void ucpBodySetGravity(IntPtr body, Vector2 gravity);

	// Token: 0x06002386 RID: 9094
	[DllImport("chipmunk")]
	public static extern void ucpBodySetLinearDamp(IntPtr body, Vector2 linearDamp);

	// Token: 0x06002387 RID: 9095
	[DllImport("chipmunk")]
	public static extern void ucpSpaceAddCollisionHandler(ucpCollisionType collisionTypeA, ucpCollisionType collisionTypeB, bool trackBegin, bool trackPersist, bool trackSeparate);

	// Token: 0x06002388 RID: 9096
	[DllImport("chipmunk")]
	public static extern void ucpSpaceRemoveCollisionHandler(ucpCollisionType collisionTypeA, ucpCollisionType collisionTypeB);

	// Token: 0x06002389 RID: 9097
	[DllImport("chipmunk")]
	public static extern int ucpGetBeginCollisionCount();

	// Token: 0x0600238A RID: 9098
	[DllImport("chipmunk")]
	public static extern int ucpGetPersistCollisionCount();

	// Token: 0x0600238B RID: 9099
	[DllImport("chipmunk")]
	public static extern int ucpGetSeparateCollisionCount();

	// Token: 0x0600238C RID: 9100
	[DllImport("chipmunk")]
	public static extern int ucpGetBeginCollisions([In] [Out] ucpCollisionPair[] results, int maxResults);

	// Token: 0x0600238D RID: 9101
	[DllImport("chipmunk")]
	public static extern int ucpGetPersistCollisions([In] [Out] ucpCollisionPair[] results, int maxResults);

	// Token: 0x0600238E RID: 9102
	[DllImport("chipmunk")]
	public static extern int ucpGetSeparateCollisions([In] [Out] ucpCollisionPair[] results, int maxResults);

	// Token: 0x0600238F RID: 9103
	[DllImport("chipmunk")]
	public static extern IntPtr ucpGetSpace();

	// Token: 0x06002390 RID: 9104
	[DllImport("chipmunk")]
	public static extern int ucpHastySpaceGetThreads();

	// Token: 0x06002391 RID: 9105
	[DllImport("chipmunk")]
	public static extern void ucpHastySpaceSetThreads(int threads);

	// Token: 0x06002392 RID: 9106
	[DllImport("chipmunk")]
	public static extern IntPtr ucpSpaceGetStaticBody();

	// Token: 0x06002393 RID: 9107
	[DllImport("chipmunk")]
	public static extern float ucpSpaceGetCollisionBias();

	// Token: 0x06002394 RID: 9108
	[DllImport("chipmunk")]
	public static extern float ucpSpaceGetCollisionPersistence();

	// Token: 0x06002395 RID: 9109
	[DllImport("chipmunk")]
	public static extern float ucpSpaceGetCollisionSlop();

	// Token: 0x06002396 RID: 9110
	[DllImport("chipmunk")]
	public static extern float ucpSpaceGetDamping();

	// Token: 0x06002397 RID: 9111
	[DllImport("chipmunk")]
	public static extern Vector2 ucpSpaceGetGravity();

	// Token: 0x06002398 RID: 9112
	[DllImport("chipmunk")]
	public static extern float ucpSpaceGetIdleSpeedThreshold();

	// Token: 0x06002399 RID: 9113
	[DllImport("chipmunk")]
	public static extern int ucpSpaceGetIterations();

	// Token: 0x0600239A RID: 9114
	[DllImport("chipmunk")]
	public static extern float ucpSpaceGetSleepTimeThreshold();

	// Token: 0x0600239B RID: 9115
	[DllImport("chipmunk")]
	public static extern void ucpSpaceSetCollisionBias(float value);

	// Token: 0x0600239C RID: 9116
	[DllImport("chipmunk")]
	public static extern void ucpSpaceSetCollisionPersistence(uint value);

	// Token: 0x0600239D RID: 9117
	[DllImport("chipmunk")]
	public static extern void ucpSpaceSetCollisionSlop(float value);

	// Token: 0x0600239E RID: 9118
	[DllImport("chipmunk")]
	public static extern void ucpSpaceSetDamping(float value);

	// Token: 0x0600239F RID: 9119
	[DllImport("chipmunk")]
	public static extern void ucpSpaceSetGravity(Vector2 value);

	// Token: 0x060023A0 RID: 9120
	[DllImport("chipmunk")]
	public static extern void ucpSpaceSetIdleSpeedThreshold(float value);

	// Token: 0x060023A1 RID: 9121
	[DllImport("chipmunk")]
	public static extern void ucpSpaceSetIterations(int value);

	// Token: 0x060023A2 RID: 9122
	[DllImport("chipmunk")]
	public static extern void ucpSpaceSetSleepTimeThreshold(float value);

	// Token: 0x060023A3 RID: 9123
	[DllImport("chipmunk")]
	public static extern void ucpSpaceStep(float dt);

	// Token: 0x060023A4 RID: 9124
	[DllImport("chipmunk")]
	public static extern void ucpClearCollisionLists();

	// Token: 0x060023A5 RID: 9125
	[DllImport("chipmunk")]
	public static extern void ucpBodyUpdatePosition(IntPtr body, float dt);

	// Token: 0x060023A6 RID: 9126
	[DllImport("chipmunk")]
	public static extern void ucpBodyUpdateVelocity(IntPtr body, Vector2 gravity, float damping, float dt);

	// Token: 0x060023A7 RID: 9127
	[DllImport("chipmunk")]
	public static extern cpBB ucpShapeUpdate(IntPtr shape, Vector2 pos, Vector2 rot);

	// Token: 0x060023A8 RID: 9128
	[DllImport("chipmunk")]
	public static extern bool ucpSpaceContainsBody(IntPtr body);

	// Token: 0x060023A9 RID: 9129
	[DllImport("chipmunk")]
	public static extern bool ucpSpaceContainsConstraint(IntPtr constraint);

	// Token: 0x060023AA RID: 9130
	[DllImport("chipmunk")]
	public static extern bool ucpSpaceContainsShape(IntPtr shape);

	// Token: 0x060023AB RID: 9131
	[DllImport("chipmunk")]
	public static extern void ucpSpaceReindexShape(IntPtr shape);

	// Token: 0x060023AC RID: 9132
	[DllImport("chipmunk")]
	public static extern void ucpSpaceReindexShapesForBody(IntPtr body);

	// Token: 0x060023AD RID: 9133
	[DllImport("chipmunk")]
	public static extern void ucpSpaceReindexStatic();

	// Token: 0x060023AE RID: 9134
	[DllImport("chipmunk")]
	public static extern float ucpAreaForCircle(float r1, float r2);

	// Token: 0x060023AF RID: 9135
	[DllImport("chipmunk")]
	public static extern float ucpAreaForPoly(int numVerts, Vector2[] verts, float r);

	// Token: 0x060023B0 RID: 9136
	[DllImport("chipmunk")]
	public static extern float ucpAreaForSegment(Vector2 a, Vector2 b, float r);

	// Token: 0x060023B1 RID: 9137
	[DllImport("chipmunk")]
	public static extern Vector2 ucpCenteroidForPoly(int numVerts, Vector2[] verts);

	// Token: 0x060023B2 RID: 9138
	[DllImport("chipmunk")]
	public static extern float ucpMomentForBox(float m, float width, float height);

	// Token: 0x060023B3 RID: 9139
	[DllImport("chipmunk")]
	public static extern float ucpMomentForBox2(float m, cpBB box);

	// Token: 0x060023B4 RID: 9140
	[DllImport("chipmunk")]
	public static extern float ucpMomentForCircle(float m, float r1, float r2, Vector2 offset);

	// Token: 0x060023B5 RID: 9141
	[DllImport("chipmunk")]
	public static extern float ucpMomentForSegment(float m, Vector2 a, Vector2 b, float r);

	// Token: 0x060023B6 RID: 9142
	[DllImport("chipmunk")]
	public static extern float ucpMomentForPoly(float m, int numVerts, Vector2[] verts, Vector2 offset, float r);

	// Token: 0x060023B7 RID: 9143
	[DllImport("chipmunk")]
	public static extern int ucpConvexHull(int count, [In] [Out] Vector2[] verts, float tol);

	// Token: 0x060023B8 RID: 9144
	[DllImport("chipmunk")]
	public static extern IntPtr ucpSpaceAddBody(IntPtr body);

	// Token: 0x060023B9 RID: 9145
	[DllImport("chipmunk")]
	public static extern IntPtr ucpSpaceAddConstraint(IntPtr constraint);

	// Token: 0x060023BA RID: 9146
	[DllImport("chipmunk")]
	public static extern IntPtr ucpSpaceAddShape(IntPtr shape);

	// Token: 0x060023BB RID: 9147
	[DllImport("chipmunk")]
	public static extern void ucpSpaceRemoveBody(IntPtr body);

	// Token: 0x060023BC RID: 9148
	[DllImport("chipmunk")]
	public static extern void ucpSpaceRemoveConstraint(IntPtr constraint);

	// Token: 0x060023BD RID: 9149
	[DllImport("chipmunk")]
	public static extern void ucpSpaceRemoveShape(IntPtr shape);

	// Token: 0x060023BE RID: 9150
	[DllImport("chipmunk")]
	public static extern void ucpBodyActivate(IntPtr body);

	// Token: 0x060023BF RID: 9151
	[DllImport("chipmunk")]
	public static extern void ucpBodySleep(IntPtr body);

	// Token: 0x060023C0 RID: 9152
	[DllImport("chipmunk")]
	public static extern void ucpBodySleepWithGroup(IntPtr body, IntPtr group);

	// Token: 0x060023C1 RID: 9153
	[DllImport("chipmunk")]
	public static extern void ucpBodyActivateStatic(IntPtr body, IntPtr filter);

	// Token: 0x060023C2 RID: 9154
	[DllImport("chipmunk")]
	public static extern void ucpBodyApplyForceAtLocalPoint(IntPtr body, Vector2 f, Vector2 r);

	// Token: 0x060023C3 RID: 9155
	[DllImport("chipmunk")]
	public static extern void ucpBodyApplyImpulseAtLocalPoint(IntPtr body, Vector2 f, Vector2 r);

	// Token: 0x060023C4 RID: 9156
	[DllImport("chipmunk")]
	public static extern void ucpBodyApplyForceAtWorldPoint(IntPtr body, Vector2 f, Vector2 r);

	// Token: 0x060023C5 RID: 9157
	[DllImport("chipmunk")]
	public static extern void ucpBodyApplyImpulseAtWorldPoint(IntPtr body, Vector2 f, Vector2 r);

	// Token: 0x060023C6 RID: 9158
	[DllImport("chipmunk")]
	public static extern float ucpBodyGetAngle(IntPtr body);

	// Token: 0x060023C7 RID: 9159
	[DllImport("chipmunk")]
	public static extern float ucpBodyGetAngVel(IntPtr body);

	// Token: 0x060023C8 RID: 9160
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyGetForce(IntPtr body);

	// Token: 0x060023C9 RID: 9161
	[DllImport("chipmunk")]
	public static extern float ucpBodyGetMass(IntPtr body);

	// Token: 0x060023CA RID: 9162
	[DllImport("chipmunk")]
	public static extern float ucpBodyGetMoment(IntPtr body);

	// Token: 0x060023CB RID: 9163
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyGetPos(IntPtr body);

	// Token: 0x060023CC RID: 9164
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyGetRot(IntPtr body);

	// Token: 0x060023CD RID: 9165
	[DllImport("chipmunk")]
	public static extern float ucpBodyGetTorque(IntPtr body);

	// Token: 0x060023CE RID: 9166
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyGetVel(IntPtr body);

	// Token: 0x060023CF RID: 9167
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyGetVelAtLocalPoint(IntPtr body, Vector2 point);

	// Token: 0x060023D0 RID: 9168
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyGetVelAtWorldPoint(IntPtr body, Vector2 point);

	// Token: 0x060023D1 RID: 9169
	[DllImport("chipmunk")]
	public static extern bool ucpBodyIsSleeping(IntPtr body);

	// Token: 0x060023D2 RID: 9170
	[DllImport("chipmunk")]
	public static extern ucpBodyType ucpBodyGetType(IntPtr body);

	// Token: 0x060023D3 RID: 9171
	[DllImport("chipmunk")]
	public static extern float ucpBodyKineticEnergy(IntPtr body);

	// Token: 0x060023D4 RID: 9172
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyLocal2World(IntPtr body, Vector2 v);

	// Token: 0x060023D5 RID: 9173
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyWorld2Local(IntPtr body, Vector2 v);

	// Token: 0x060023D6 RID: 9174
	[DllImport("chipmunk")]
	public static extern void ucpBodySetAngle(IntPtr body, float a);

	// Token: 0x060023D7 RID: 9175
	[DllImport("chipmunk")]
	public static extern void ucpBodySetAngVel(IntPtr body, float a);

	// Token: 0x060023D8 RID: 9176
	[DllImport("chipmunk")]
	public static extern void ucpBodySetForce(IntPtr body, Vector2 value);

	// Token: 0x060023D9 RID: 9177
	[DllImport("chipmunk")]
	public static extern void ucpBodySetMass(IntPtr body, float m);

	// Token: 0x060023DA RID: 9178
	[DllImport("chipmunk")]
	public static extern void ucpBodySetMoment(IntPtr body, float i);

	// Token: 0x060023DB RID: 9179
	[DllImport("chipmunk")]
	public static extern void ucpBodySetPos(IntPtr body, Vector2 pos);

	// Token: 0x060023DC RID: 9180
	[DllImport("chipmunk")]
	public static extern void ucpBodySetTorque(IntPtr body, float value);

	// Token: 0x060023DD RID: 9181
	[DllImport("chipmunk")]
	public static extern void ucpBodySetVel(IntPtr body, Vector2 value);

	// Token: 0x060023DE RID: 9182
	[DllImport("chipmunk")]
	public static extern void ucpBodySetType(IntPtr body, ucpBodyType type);

	// Token: 0x060023DF RID: 9183
	[DllImport("chipmunk")]
	public static extern float ucpBodyGetScale(IntPtr body);

	// Token: 0x060023E0 RID: 9184
	[DllImport("chipmunk")]
	public static extern void ucpBodySetScale(IntPtr body, float value);

	// Token: 0x060023E1 RID: 9185
	[DllImport("chipmunk")]
	public static extern Vector2 ucpBodyGetForceVectorSum(IntPtr body);

	// Token: 0x060023E2 RID: 9186
	[DllImport("chipmunk")]
	public static extern float ucpBodyGetForceMagnitudeSum(IntPtr body);

	// Token: 0x060023E3 RID: 9187
	[DllImport("chipmunk")]
	public static extern cpBB ucpShapeGetBB(IntPtr shape);

	// Token: 0x060023E4 RID: 9188
	[DllImport("chipmunk")]
	public static extern IntPtr ucpShapeGetBody(IntPtr shape);

	// Token: 0x060023E5 RID: 9189
	[DllImport("chipmunk")]
	public static extern uint ucpShapeGetCollisionType(IntPtr shape);

	// Token: 0x060023E6 RID: 9190
	[DllImport("chipmunk")]
	public static extern float ucpShapeGetElasticity(IntPtr shape);

	// Token: 0x060023E7 RID: 9191
	[DllImport("chipmunk")]
	public static extern float ucpShapeGetFriction(IntPtr shape);

	// Token: 0x060023E8 RID: 9192
	[DllImport("chipmunk")]
	public static extern bool ucpShapeGetSensor(IntPtr shape);

	// Token: 0x060023E9 RID: 9193
	[DllImport("chipmunk")]
	public static extern Vector2 ucpShapeGetSurfaceVelocity(IntPtr shape);

	// Token: 0x060023EA RID: 9194
	[DllImport("chipmunk")]
	public static extern void ucpShapeSetCollisionType(IntPtr shape, ucpCollisionType value);

	// Token: 0x060023EB RID: 9195
	[DllImport("chipmunk")]
	public static extern void ucpShapeSetElasticity(IntPtr shape, float value);

	// Token: 0x060023EC RID: 9196
	[DllImport("chipmunk")]
	public static extern void ucpShapeSetFriction(IntPtr shape, float value);

	// Token: 0x060023ED RID: 9197
	[DllImport("chipmunk")]
	public static extern void ucpShapeSetFilter(IntPtr shape, ucpShapeFilter filter);

	// Token: 0x060023EE RID: 9198
	[DllImport("chipmunk")]
	public static extern ucpShapeFilter ucpShapeGetFilter(IntPtr shape);

	// Token: 0x060023EF RID: 9199
	[DllImport("chipmunk")]
	public static extern void ucpShapeSetSensor(IntPtr shape, bool value);

	// Token: 0x060023F0 RID: 9200
	[DllImport("chipmunk")]
	public static extern void ucpShapeSetSurfaceVelocity(IntPtr shape, Vector2 value);

	// Token: 0x060023F1 RID: 9201
	[DllImport("chipmunk")]
	public static extern cpBB ucpShapeCacheBB(IntPtr shape);

	// Token: 0x060023F2 RID: 9202
	[DllImport("chipmunk")]
	public static extern IntPtr ucpCircleShapeNew(IntPtr body, float radius, Vector2 offset, ucpCollisionType collisionType);

	// Token: 0x060023F3 RID: 9203
	[DllImport("chipmunk")]
	public static extern float ucpCircleShapeGetRadius(IntPtr shape);

	// Token: 0x060023F4 RID: 9204
	[DllImport("chipmunk")]
	public static extern Vector2 ucpCircleShapeGetOffset(IntPtr shape);

	// Token: 0x060023F5 RID: 9205
	[DllImport("chipmunk")]
	public static extern void ucpCircleShapeSetRadius(IntPtr shape, float radius);

	// Token: 0x060023F6 RID: 9206
	[DllImport("chipmunk")]
	public static extern void ucpCircleShapeSetOffset(IntPtr shape, Vector2 offset);

	// Token: 0x060023F7 RID: 9207
	[DllImport("chipmunk")]
	public static extern IntPtr ucpSegmentShapeNew(IntPtr body, Vector2 a, Vector2 b, float radius, ucpCollisionType collisionType);

	// Token: 0x060023F8 RID: 9208
	[DllImport("chipmunk")]
	public static extern Vector2 ucpSegmentShapeGetA(IntPtr shape);

	// Token: 0x060023F9 RID: 9209
	[DllImport("chipmunk")]
	public static extern Vector2 ucpSegmentShapeGetB(IntPtr shape);

	// Token: 0x060023FA RID: 9210
	[DllImport("chipmunk")]
	public static extern Vector2 ucpSegmentShapeGetNormal(IntPtr shape);

	// Token: 0x060023FB RID: 9211
	[DllImport("chipmunk")]
	public static extern float ucpSegmentShapeGetRadius(IntPtr shape);

	// Token: 0x060023FC RID: 9212
	[DllImport("chipmunk")]
	public static extern void ucpSegmentShapeSetEndpoints(IntPtr shape, Vector2 a, Vector2 b);

	// Token: 0x060023FD RID: 9213
	[DllImport("chipmunk")]
	public static extern void ucpSegmentShapeSetRadius(IntPtr shape, float radius);

	// Token: 0x060023FE RID: 9214
	[DllImport("chipmunk")]
	public static extern void ucpSegmentShapeSetNeighbors(IntPtr shape, Vector2 prev, Vector2 next);

	// Token: 0x060023FF RID: 9215
	[DllImport("chipmunk")]
	public static extern void ucpSegmentShapeSetPrevNeighborTangent(IntPtr shape, Vector2 tangent);

	// Token: 0x06002400 RID: 9216
	[DllImport("chipmunk")]
	public static extern void ucpSegmentShapeSetNextNeighborTangent(IntPtr shape, Vector2 tangent);

	// Token: 0x06002401 RID: 9217
	[DllImport("chipmunk")]
	public static extern IntPtr ucpPolyShapeNew(IntPtr body, int numVerts, Vector2[] verts, ucpTransform transform, float radius, ucpCollisionType collisionType);

	// Token: 0x06002402 RID: 9218
	[DllImport("chipmunk")]
	public static extern int ucpPolyShapeGetNumVerts(IntPtr shape);

	// Token: 0x06002403 RID: 9219
	[DllImport("chipmunk")]
	public static extern Vector2 ucpPolyShapeGetVert(IntPtr shape, int idx);

	// Token: 0x06002404 RID: 9220
	[DllImport("chipmunk")]
	public static extern void ucpPolyShapeSetVerts(IntPtr shape, int numVerts, Vector2[] verts, ucpTransform transform);

	// Token: 0x06002405 RID: 9221
	[DllImport("chipmunk")]
	public static extern IntPtr ucpConstraintGetA(IntPtr constraint);

	// Token: 0x06002406 RID: 9222
	[DllImport("chipmunk")]
	public static extern IntPtr ucpConstraintGetB(IntPtr constraint);

	// Token: 0x06002407 RID: 9223
	[DllImport("chipmunk")]
	public static extern float ucpConstraintGetErrorBias(IntPtr constraint);

	// Token: 0x06002408 RID: 9224
	[DllImport("chipmunk")]
	public static extern float ucpConstraintGetImpulse(IntPtr constraint);

	// Token: 0x06002409 RID: 9225
	[DllImport("chipmunk")]
	public static extern float ucpConstraintGetMaxBias(IntPtr constraint);

	// Token: 0x0600240A RID: 9226
	[DllImport("chipmunk")]
	public static extern float ucpConstraintGetMaxForce(IntPtr constraint);

	// Token: 0x0600240B RID: 9227
	[DllImport("chipmunk")]
	public static extern void ucpConstraintSetErrorBias(IntPtr constraint, float value);

	// Token: 0x0600240C RID: 9228
	[DllImport("chipmunk")]
	public static extern void ucpConstraintSetMaxBias(IntPtr constraint, float value);

	// Token: 0x0600240D RID: 9229
	[DllImport("chipmunk")]
	public static extern void ucpConstraintSetMaxForce(IntPtr constraint, float value);

	// Token: 0x0600240E RID: 9230
	[DllImport("chipmunk")]
	public static extern IntPtr ucpDampedRotarySpringNew(IntPtr a, IntPtr b, float restAngle, float stiffness, float damping);

	// Token: 0x0600240F RID: 9231
	[DllImport("chipmunk")]
	public static extern float ucpDampedRotarySpringGetDamping(IntPtr constraint);

	// Token: 0x06002410 RID: 9232
	[DllImport("chipmunk")]
	public static extern float ucpDampedRotarySpringGetRestAngle(IntPtr constraint);

	// Token: 0x06002411 RID: 9233
	[DllImport("chipmunk")]
	public static extern float ucpDampedRotarySpringGetStiffness(IntPtr constraint);

	// Token: 0x06002412 RID: 9234
	[DllImport("chipmunk")]
	public static extern void ucpDampedRotarySpringSetDamping(IntPtr constraint, float value);

	// Token: 0x06002413 RID: 9235
	[DllImport("chipmunk")]
	public static extern void ucpDampedRotarySpringSetRestAngle(IntPtr constraint, float value);

	// Token: 0x06002414 RID: 9236
	[DllImport("chipmunk")]
	public static extern void ucpDampedRotarySpringSetStiffness(IntPtr constraint, float value);

	// Token: 0x06002415 RID: 9237
	[DllImport("chipmunk")]
	public static extern IntPtr ucpDampedSpringNew(IntPtr a, IntPtr b, Vector2 anchr1, Vector2 anchr2, float restLength, float stiffness, float damping);

	// Token: 0x06002416 RID: 9238
	[DllImport("chipmunk")]
	public static extern Vector2 ucpDampedSpringGetAnchr1(IntPtr constraint);

	// Token: 0x06002417 RID: 9239
	[DllImport("chipmunk")]
	public static extern Vector2 ucpDampedSpringGetAnchr2(IntPtr constraint);

	// Token: 0x06002418 RID: 9240
	[DllImport("chipmunk")]
	public static extern float ucpDampedSpringGetDamping(IntPtr constraint);

	// Token: 0x06002419 RID: 9241
	[DllImport("chipmunk")]
	public static extern float ucpDampedSpringGetRestLength(IntPtr constraint);

	// Token: 0x0600241A RID: 9242
	[DllImport("chipmunk")]
	public static extern float ucpDampedSpringGetStiffness(IntPtr constraint);

	// Token: 0x0600241B RID: 9243
	[DllImport("chipmunk")]
	public static extern void ucpDampedSpringSetAnchr1(IntPtr constraint, Vector2 value);

	// Token: 0x0600241C RID: 9244
	[DllImport("chipmunk")]
	public static extern void ucpDampedSpringSetAnchr2(IntPtr constraint, Vector2 value);

	// Token: 0x0600241D RID: 9245
	[DllImport("chipmunk")]
	public static extern void ucpDampedSpringSetDamping(IntPtr constraint, float value);

	// Token: 0x0600241E RID: 9246
	[DllImport("chipmunk")]
	public static extern void ucpDampedSpringSetRestLength(IntPtr constraint, float value);

	// Token: 0x0600241F RID: 9247
	[DllImport("chipmunk")]
	public static extern void ucpDampedSpringSetStiffness(IntPtr constraint, float value);

	// Token: 0x06002420 RID: 9248
	[DllImport("chipmunk")]
	public static extern IntPtr ucpGearJointNew(IntPtr a, IntPtr b, float phase, float ratio);

	// Token: 0x06002421 RID: 9249
	[DllImport("chipmunk")]
	public static extern float ucpGearJointGetPhase(IntPtr constraint);

	// Token: 0x06002422 RID: 9250
	[DllImport("chipmunk")]
	public static extern float ucpGearJointGetRatio(IntPtr constraint);

	// Token: 0x06002423 RID: 9251
	[DllImport("chipmunk")]
	public static extern void ucpGearJointSetPhase(IntPtr constraint, float value);

	// Token: 0x06002424 RID: 9252
	[DllImport("chipmunk")]
	public static extern void ucpGearJointSetRatio(IntPtr constraint, float value);

	// Token: 0x06002425 RID: 9253
	[DllImport("chipmunk")]
	public static extern IntPtr ucpGrooveJointNew(IntPtr a, IntPtr b, Vector2 groove_a, Vector2 groobe_b, Vector2 anchr2);

	// Token: 0x06002426 RID: 9254
	[DllImport("chipmunk")]
	public static extern Vector2 ucpGrooveJointGetAnchr2(IntPtr constraint);

	// Token: 0x06002427 RID: 9255
	[DllImport("chipmunk")]
	public static extern Vector2 ucpGrooveJointGetGrooveA(IntPtr constraint);

	// Token: 0x06002428 RID: 9256
	[DllImport("chipmunk")]
	public static extern Vector2 ucpGrooveJointGetGrooveB(IntPtr constraint);

	// Token: 0x06002429 RID: 9257
	[DllImport("chipmunk")]
	public static extern void ucpGrooveJointSetAnchr2(IntPtr constraint, Vector2 value);

	// Token: 0x0600242A RID: 9258
	[DllImport("chipmunk")]
	public static extern void ucpGrooveJointSetGrooveA(IntPtr constraint, Vector2 value);

	// Token: 0x0600242B RID: 9259
	[DllImport("chipmunk")]
	public static extern void ucpGrooveJointSetGrooveB(IntPtr constraint, Vector2 value);

	// Token: 0x0600242C RID: 9260
	[DllImport("chipmunk")]
	public static extern IntPtr ucpPinJointNew(IntPtr a, IntPtr b, Vector2 anchr1, Vector2 anchr2);

	// Token: 0x0600242D RID: 9261
	[DllImport("chipmunk")]
	public static extern Vector2 ucpPinJointGetAnchr1(IntPtr constraint);

	// Token: 0x0600242E RID: 9262
	[DllImport("chipmunk")]
	public static extern Vector2 ucpPinJointGetAnchr2(IntPtr constraint);

	// Token: 0x0600242F RID: 9263
	[DllImport("chipmunk")]
	public static extern float ucpPinJointGetDist(IntPtr constraint);

	// Token: 0x06002430 RID: 9264
	[DllImport("chipmunk")]
	public static extern void ucpPinJointSetAnchr1(IntPtr constraint, Vector2 value);

	// Token: 0x06002431 RID: 9265
	[DllImport("chipmunk")]
	public static extern void ucpPinJointSetAnchr2(IntPtr constraint, Vector2 value);

	// Token: 0x06002432 RID: 9266
	[DllImport("chipmunk")]
	public static extern void ucpPinJointSetDist(IntPtr constraint, float value);

	// Token: 0x06002433 RID: 9267
	[DllImport("chipmunk")]
	public static extern IntPtr ucpPivotJointNew(IntPtr a, IntPtr b, Vector2 pivot);

	// Token: 0x06002434 RID: 9268
	[DllImport("chipmunk")]
	public static extern IntPtr ucpPivotJointNew2(IntPtr a, IntPtr b, Vector2 anchr1, Vector2 anchr2);

	// Token: 0x06002435 RID: 9269
	[DllImport("chipmunk")]
	public static extern Vector2 ucpPivotJointGetAnchr1(IntPtr constraint);

	// Token: 0x06002436 RID: 9270
	[DllImport("chipmunk")]
	public static extern Vector2 ucpPivotJointGetAnchr2(IntPtr constraint);

	// Token: 0x06002437 RID: 9271
	[DllImport("chipmunk")]
	public static extern void ucpPivotJointSetAnchr1(IntPtr constraint, Vector2 value);

	// Token: 0x06002438 RID: 9272
	[DllImport("chipmunk")]
	public static extern void ucpPivotJointSetAnchr2(IntPtr constraint, Vector2 value);

	// Token: 0x06002439 RID: 9273
	[DllImport("chipmunk")]
	public static extern IntPtr ucpRatchetJointNew(IntPtr a, IntPtr b, float phase, float ratchet);

	// Token: 0x0600243A RID: 9274
	[DllImport("chipmunk")]
	public static extern float ucpRatchetJointGetAngle(IntPtr constraint);

	// Token: 0x0600243B RID: 9275
	[DllImport("chipmunk")]
	public static extern float ucpRatchetJointGetPhase(IntPtr constraint);

	// Token: 0x0600243C RID: 9276
	[DllImport("chipmunk")]
	public static extern float ucpRatchetJointGetRatchet(IntPtr constraint);

	// Token: 0x0600243D RID: 9277
	[DllImport("chipmunk")]
	public static extern void ucpRatchetJointSetAngle(IntPtr constraint, float value);

	// Token: 0x0600243E RID: 9278
	[DllImport("chipmunk")]
	public static extern void ucpRatchetJointSetPhase(IntPtr constraint, float value);

	// Token: 0x0600243F RID: 9279
	[DllImport("chipmunk")]
	public static extern void ucpRatchetJointSetRatchet(IntPtr constraint, float value);

	// Token: 0x06002440 RID: 9280
	[DllImport("chipmunk")]
	public static extern IntPtr ucpRotaryLimitJointNew(IntPtr a, IntPtr b, float min, float max);

	// Token: 0x06002441 RID: 9281
	[DllImport("chipmunk")]
	public static extern float ucpRotaryLimitJointGetMax(IntPtr constraint);

	// Token: 0x06002442 RID: 9282
	[DllImport("chipmunk")]
	public static extern float ucpRotaryLimitJointGetMin(IntPtr constraint);

	// Token: 0x06002443 RID: 9283
	[DllImport("chipmunk")]
	public static extern void ucpRotaryLimitJointSetMax(IntPtr constraint, float value);

	// Token: 0x06002444 RID: 9284
	[DllImport("chipmunk")]
	public static extern void ucpRotaryLimitJointSetMin(IntPtr constraint, float value);

	// Token: 0x06002445 RID: 9285
	[DllImport("chipmunk")]
	public static extern IntPtr ucpSimpleMotorNew(IntPtr a, IntPtr b, float rate);

	// Token: 0x06002446 RID: 9286
	[DllImport("chipmunk")]
	public static extern float ucpSimpleMotorGetRate(IntPtr constraint);

	// Token: 0x06002447 RID: 9287
	[DllImport("chipmunk")]
	public static extern void ucpSimpleMotorSetRate(IntPtr constraint, float value);

	// Token: 0x06002448 RID: 9288
	[DllImport("chipmunk")]
	public static extern IntPtr ucpSlideJointNew(IntPtr a, IntPtr b, Vector2 anchr1, Vector2 anchr2, float min, float max);

	// Token: 0x06002449 RID: 9289
	[DllImport("chipmunk")]
	public static extern Vector2 ucpSlideJointGetAnchr1(IntPtr constraint);

	// Token: 0x0600244A RID: 9290
	[DllImport("chipmunk")]
	public static extern Vector2 ucpSlideJointGetAnchr2(IntPtr constraint);

	// Token: 0x0600244B RID: 9291
	[DllImport("chipmunk")]
	public static extern float ucpSlideJointGetMax(IntPtr constraint);

	// Token: 0x0600244C RID: 9292
	[DllImport("chipmunk")]
	public static extern float ucpSlideJointGetMin(IntPtr constraint);

	// Token: 0x0600244D RID: 9293
	[DllImport("chipmunk")]
	public static extern void ucpSlideJointSetAnchr1(IntPtr constraint, Vector2 value);

	// Token: 0x0600244E RID: 9294
	[DllImport("chipmunk")]
	public static extern void ucpSlideJointSetAnchr2(IntPtr constraint, Vector2 value);

	// Token: 0x0600244F RID: 9295
	[DllImport("chipmunk")]
	public static extern void ucpSlideJointSetMax(IntPtr constraint, float value);

	// Token: 0x06002450 RID: 9296
	[DllImport("chipmunk")]
	public static extern void ucpSlideJointSetMin(IntPtr constraint, float value);

	// Token: 0x06002451 RID: 9297
	[DllImport("chipmunk")]
	public static extern cpBB ucpBBNew(float l, float b, float r, float t);

	// Token: 0x06002452 RID: 9298
	[DllImport("chipmunk")]
	public static extern cpBB ucpBBNewForCircle(Vector2 p, float r);

	// Token: 0x06002453 RID: 9299
	[DllImport("chipmunk")]
	public static extern cpBB ucpBBExpand(cpBB bb, Vector2 v);

	// Token: 0x06002454 RID: 9300
	[DllImport("chipmunk")]
	public static extern cpBB ucpBBMerge(cpBB a, cpBB b);

	// Token: 0x06002455 RID: 9301
	[DllImport("chipmunk")]
	public static extern float ucpBBMergedArea(cpBB a, cpBB b);

	// Token: 0x06002456 RID: 9302
	[DllImport("chipmunk")]
	public static extern bool ucpBBContainsBB(cpBB bb, cpBB other);

	// Token: 0x06002457 RID: 9303
	[DllImport("chipmunk")]
	public static extern bool ucpBBContainsVect(cpBB bb, Vector2 v);

	// Token: 0x06002458 RID: 9304
	[DllImport("chipmunk")]
	public static extern bool ucpBBIntersects(cpBB a, cpBB b);

	// Token: 0x06002459 RID: 9305
	[DllImport("chipmunk")]
	public static extern bool ucpBBIntersectsSegment(cpBB bb, Vector2 a, Vector2 b);

	// Token: 0x0600245A RID: 9306
	[DllImport("chipmunk")]
	public static extern float ucpBBSegmentQuery(cpBB bb, Vector2 a, Vector2 b);

	// Token: 0x0600245B RID: 9307
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvadd(Vector2 v1, Vector2 v2);

	// Token: 0x0600245C RID: 9308
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvclamp(Vector2 v, float len);

	// Token: 0x0600245D RID: 9309
	[DllImport("chipmunk")]
	public static extern float ucpvcross(Vector2 v1, Vector2 v2);

	// Token: 0x0600245E RID: 9310
	[DllImport("chipmunk")]
	public static extern float ucpvdist(Vector2 v1, Vector2 v2);

	// Token: 0x0600245F RID: 9311
	[DllImport("chipmunk")]
	public static extern float ucpvdistsq(Vector2 v1, Vector2 v2);

	// Token: 0x06002460 RID: 9312
	[DllImport("chipmunk")]
	public static extern float ucpvdot(Vector2 v1, Vector2 v2);

	// Token: 0x06002461 RID: 9313
	[DllImport("chipmunk")]
	public static extern bool ucpveql(Vector2 v1, Vector2 v2);

	// Token: 0x06002462 RID: 9314
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvforangle(float a);

	// Token: 0x06002463 RID: 9315
	[DllImport("chipmunk")]
	public static extern float ucpvlength(Vector2 v);

	// Token: 0x06002464 RID: 9316
	[DllImport("chipmunk")]
	public static extern float ucpvlengthsq(Vector2 v);

	// Token: 0x06002465 RID: 9317
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvlerp(Vector2 v1, Vector2 v2, float t);

	// Token: 0x06002466 RID: 9318
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvlerpconst(Vector2 v1, Vector2 v2, float d);

	// Token: 0x06002467 RID: 9319
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvmult(Vector2 v, float s);

	// Token: 0x06002468 RID: 9320
	[DllImport("chipmunk")]
	public static extern bool ucpvnear(Vector2 v1, Vector2 v2, float dist);

	// Token: 0x06002469 RID: 9321
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvneg(Vector2 v);

	// Token: 0x0600246A RID: 9322
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvnormalize_safe(Vector2 v);

	// Token: 0x0600246B RID: 9323
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvperp(Vector2 v);

	// Token: 0x0600246C RID: 9324
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvproject(Vector2 v1, Vector2 v2);

	// Token: 0x0600246D RID: 9325
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvrotate(Vector2 v1, Vector2 v2);

	// Token: 0x0600246E RID: 9326
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvrperp(Vector2 v);

	// Token: 0x0600246F RID: 9327
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvslerp(Vector2 v1, Vector2 v2, float t);

	// Token: 0x06002470 RID: 9328
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvslerpconst(Vector2 v1, Vector2 v2, float a);

	// Token: 0x06002471 RID: 9329
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvsub(Vector2 v1, Vector2 v2);

	// Token: 0x06002472 RID: 9330
	[DllImport("chipmunk")]
	public static extern float ucpvtoangle(Vector2 v);

	// Token: 0x06002473 RID: 9331
	[DllImport("chipmunk")]
	public static extern Vector2 ucpvunrotate(Vector2 v1, Vector2 v2);

	// Token: 0x06002474 RID: 9332
	[DllImport("chipmunk")]
	public static extern float ucpShapePointQuery(IntPtr shape, Vector2 p, ref ucpPointQueryInfo info);

	// Token: 0x06002475 RID: 9333
	[DllImport("chipmunk")]
	public static extern bool ucpShapeSegmentQuery(IntPtr shape, Vector2 a, Vector2 b, float radius, ref ucpSegmentQueryInfo info);

	// Token: 0x06002476 RID: 9334
	[DllImport("chipmunk")]
	public static extern int ucpSpacePointQuery(Vector2 point, float maxDistance, ucpShapeFilter filter, [In] [Out] ucpPointQueryInfo[] results, int maxResults);

	// Token: 0x06002477 RID: 9335
	[DllImport("chipmunk")]
	public static extern int ucpSpaceSegmentQuery(Vector2 start, Vector2 end, float radius, ucpShapeFilter filter, [In] [Out] ucpSegmentQueryInfo[] results, int maxResults);

	// Token: 0x06002478 RID: 9336
	[DllImport("chipmunk")]
	public static extern IntPtr ucpSpacePointQueryNearest(Vector2 point, float maxDistance, ucpShapeFilter filter, ref ucpPointQueryInfo info);

	// Token: 0x06002479 RID: 9337
	[DllImport("chipmunk")]
	public static extern IntPtr ucpSpaceSegmentQueryFirst(Vector2 start, Vector2 end, float radius, ucpShapeFilter filter, ref ucpSegmentQueryInfo info);

	// Token: 0x0600247A RID: 9338
	[DllImport("chipmunk")]
	public static extern bool ucpSpaceShapeQuery(IntPtr shape, [In] [Out] ucpPointQueryInfo[] results, int maxResults);

	// Token: 0x0600247B RID: 9339
	[DllImport("chipmunk")]
	public static extern int ucpGetBodyComponentIndexByBody(IntPtr body);

	// Token: 0x0600247C RID: 9340
	[DllImport("chipmunk")]
	public static extern int ucpGetBodyComponentIndexByShape(IntPtr shape);

	// Token: 0x0600247D RID: 9341 RVA: 0x001933D4 File Offset: 0x001917D4
	public static void ucpShapeSetLayers(IntPtr shape, uint value)
	{
		ucpShapeFilter ucpShapeFilter = ChipmunkProWrapper.ucpShapeGetFilter(shape);
		ucpShapeFilter.categories = value;
		ucpShapeFilter.mask = value;
		ChipmunkProWrapper.ucpShapeSetFilter(shape, ucpShapeFilter);
	}

	// Token: 0x0600247E RID: 9342 RVA: 0x00193400 File Offset: 0x00191800
	public static void ucpShapeSetGroup(IntPtr shape, uint value)
	{
		ucpShapeFilter ucpShapeFilter = ChipmunkProWrapper.ucpShapeGetFilter(shape);
		ucpShapeFilter.group = value;
		ChipmunkProWrapper.ucpShapeSetFilter(shape, ucpShapeFilter);
	}

	// Token: 0x0600247F RID: 9343 RVA: 0x00193423 File Offset: 0x00191823
	public static void ucpBodyResetForces(IntPtr body)
	{
		ChipmunkProWrapper.ucpBodySetForce(body, Vector2.zero);
		ChipmunkProWrapper.ucpBodySetTorque(body, 0f);
	}

	// Token: 0x04002A5C RID: 10844
	private const string lookFrom = "chipmunk";
}
