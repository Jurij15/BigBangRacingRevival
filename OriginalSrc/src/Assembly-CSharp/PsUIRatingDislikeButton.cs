using System;

// Token: 0x020003B6 RID: 950
public class PsUIRatingDislikeButton : PsUIRatingButton
{
	// Token: 0x06001B28 RID: 6952 RVA: 0x0012FDAB File Offset: 0x0012E1AB
	public PsUIRatingDislikeButton(UIComponent _parent, float _fillerwidth, float _fillerheight, string _spriteFrame = "menu_thumbs_down_off")
		: base(_parent, _spriteFrame, _fillerwidth, _fillerheight)
	{
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x0012FDB8 File Offset: 0x0012E1B8
	public override void ButtonPressed()
	{
		base.ButtonPressedAnimation(false);
	}
}
