using System;

// Token: 0x0200052E RID: 1326
public enum LabelType
{
	// Token: 0x04002C83 RID: 11395
	None,
	// Token: 0x04002C84 RID: 11396
	FixedValue,
	// Token: 0x04002C85 RID: 11397
	Probability,
	// Token: 0x04002C86 RID: 11398
	Equal,
	// Token: 0x04002C87 RID: 11399
	NotEqual,
	// Token: 0x04002C88 RID: 11400
	Less,
	// Token: 0x04002C89 RID: 11401
	Greater,
	// Token: 0x04002C8A RID: 11402
	LessOrEqual,
	// Token: 0x04002C8B RID: 11403
	GreaterOrEqual,
	// Token: 0x04002C8C RID: 11404
	WithinRange,
	// Token: 0x04002C8D RID: 11405
	OutsideRange,
	// Token: 0x04002C8E RID: 11406
	Trigger,
	// Token: 0x04002C8F RID: 11407
	ReverseTrigger,
	// Token: 0x04002C90 RID: 11408
	Dice,
	// Token: 0x04002C91 RID: 11409
	ChangeValue,
	// Token: 0x04002C92 RID: 11410
	ChangeInterval,
	// Token: 0x04002C93 RID: 11411
	ChangeProbability,
	// Token: 0x04002C94 RID: 11412
	ChangeMultipler,
	// Token: 0x04002C95 RID: 11413
	ChangeCapacity
}
