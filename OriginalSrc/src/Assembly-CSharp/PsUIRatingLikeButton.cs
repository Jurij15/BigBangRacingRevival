using System;

// Token: 0x020003B4 RID: 948
public class PsUIRatingLikeButton : PsUIRatingButton
{
	// Token: 0x06001B25 RID: 6949 RVA: 0x0012FD88 File Offset: 0x0012E188
	public PsUIRatingLikeButton(UIComponent _parent, float _fillerwidth, float _fillerheight, string _spriteFrame = "menu_thumbs_up_off")
		: base(_parent, _spriteFrame, _fillerwidth, _fillerheight)
	{
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x0012FD95 File Offset: 0x0012E195
	public override void ButtonPressed()
	{
		base.ButtonPressedAnimation(true);
	}
}
