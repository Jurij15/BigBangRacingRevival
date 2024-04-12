using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x02000221 RID: 545
[Serializable]
public class MetagameNodeData : ISerializable
{
	// Token: 0x06000FAD RID: 4013 RVA: 0x00093F38 File Offset: 0x00092338
	public MetagameNodeData(string _uniqueName = "")
	{
		this.m_nodeDataType = MetagameNodeDataType.Undefined;
		this.m_dataIdentifier = string.Empty;
		this.m_dialogues = new List<PsDialogue>();
		this.m_sidePathLevels = null;
		this.m_unlockableType = PsUnlockableType.Undefined;
		this.m_unlockableName = _uniqueName;
		this.m_unlockableDescription = string.Empty;
		this.m_unlockableIcon = string.Empty;
		this.m_unlockableGraphNodeClass = string.Empty;
		this.m_unlockableRentName = string.Empty;
		this.m_unlockableRentButton = string.Empty;
		this.m_unlockableMaxCount = 0;
		this.m_unlockableComplexity = 0;
		this.m_unlockableItemLevel = 0;
		this.m_unlockableGachaProbability = 1f;
		this.m_unlockableRarity = PsRarity.Common;
		this.m_unlockableCurrency = PsCurrency.None;
		this.m_unlockablePrice = 0;
		this.m_unlockableUpgradeValues = new Hashtable();
		this.m_unlockableUpgradeSteps = 10;
		this.m_unlockableUpgradePrices = new string[this.m_unlockableUpgradeSteps];
		this.m_levelMinigameMetaData = null;
		this.m_levelMinigameId = string.Empty;
		this.m_levelDifficulty = PsGameDifficulty.Any;
		this.m_levelPlayerUnit = "Any";
		this.m_levelSubgenre = PsSubgenre.Any;
		this.m_levelGameMode = PsGameMode.Any;
		this.m_levelItems = new List<string>();
		this.m_levelMedalTimes = new string[3];
		this.m_blockType = PsPathBlockType.Boss;
		this.m_blockRequiredStars = 0;
		this.m_blockBolts = 0;
		this.m_blockCoins = 0;
		this.m_blockDiamonds = 0;
		this.m_blockKeys = 0;
		this.m_blockUnlocked = string.Empty;
		this.m_blockWillUnlock = string.Empty;
		this.m_blockChestType = PsPathBlockChestType.Silver;
		this.m_dialogueTrigger = PsNodeEventTrigger.Manual;
		this.m_dialogueCharacter = PsDialogueCharacter.Scientist;
		this.m_dialogueCharacterPosition = PsDialogueCharacterPosition.Right;
		this.m_dialogueText = "What say you?";
		this.m_dialogueProceed = "Yes!";
		this.m_dialogueCancel = "No!";
		this.m_dialogueTextLocalized = StringID.EMPTY;
		this.m_levelTemplateDomeSize = PsGameArea.Any;
		this.m_levelTemplateMinigames = new List<string>();
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x000940F0 File Offset: 0x000924F0
	public MetagameNodeData(SerializationInfo info, StreamingContext ctxt)
	{
		try
		{
			this.m_nodeDataType = (MetagameNodeDataType)Enum.Parse(typeof(MetagameNodeDataType), (string)info.GetValue("geNodeType", typeof(string)));
		}
		catch
		{
			this.m_nodeDataType = MetagameNodeDataType.Undefined;
		}
		this.m_dialogues = new List<PsDialogue>();
		if (this.m_nodeDataType == MetagameNodeDataType.Unlock)
		{
			this.m_dataIdentifier = (string)info.GetValue("itemClass", typeof(string));
			this.m_unlockableName = (string)info.GetValue("itemName", typeof(string));
			this.m_unlockableDescription = (string)info.GetValue("itemDescription", typeof(string));
			this.m_unlockableIcon = (string)info.GetValue("itemIcon", typeof(string));
		}
		if (this.m_nodeDataType == MetagameNodeDataType.UnlockableCategory)
		{
			this.m_unlockableType = (PsUnlockableType)Enum.Parse(typeof(PsUnlockableType), (string)info.GetValue("itemType", typeof(string)));
			this.m_unlockableName = (string)info.GetValue("itemName", typeof(string));
			this.m_unlockableDescription = (string)info.GetValue("itemDescription", typeof(string));
			this.m_unlockableIcon = (string)info.GetValue("itemIcon", typeof(string));
		}
		if (this.m_nodeDataType == MetagameNodeDataType.Unlockable)
		{
			this.m_dataIdentifier = (string)info.GetValue("itemClass", typeof(string));
			this.m_unlockableName = (string)info.GetValue("itemName", typeof(string));
			this.m_unlockableDescription = (string)info.GetValue("itemDescription", typeof(string));
			this.m_unlockableIcon = (string)info.GetValue("itemIcon", typeof(string));
			this.m_unlockableGraphNodeClass = (string)info.GetValue("itemNodeType", typeof(string));
			this.m_unlockableMaxCount = (int)info.GetValue("itemMaxCount", typeof(int));
			this.m_unlockableComplexity = (int)info.GetValue("itemComplexity", typeof(int));
			try
			{
				this.m_unlockableItemLevel = (int)info.GetValue("itemLevel", typeof(int));
			}
			catch
			{
				this.m_unlockableItemLevel = 0;
			}
			try
			{
				this.m_unlockableGachaProbability = (float)info.GetValue("itemGachaProbability", typeof(float));
			}
			catch
			{
				this.m_unlockableGachaProbability = 1f;
			}
			try
			{
				this.m_unlockableRarity = (PsRarity)Enum.Parse(typeof(PsRarity), (string)info.GetValue("itemRarity", typeof(string)));
			}
			catch
			{
				this.m_unlockableRarity = PsRarity.Common;
			}
			try
			{
				this.m_unlockableCurrency = (PsCurrency)Enum.Parse(typeof(PsCurrency), (string)info.GetValue("itemCurrency", typeof(string)));
			}
			catch
			{
				this.m_unlockableCurrency = PsCurrency.None;
			}
			try
			{
				this.m_unlockablePrice = (int)info.GetValue("itemPrice", typeof(int));
			}
			catch
			{
				this.m_unlockablePrice = 0;
			}
		}
		if (this.m_nodeDataType == MetagameNodeDataType.UnlockableSimple)
		{
			this.m_dataIdentifier = (string)info.GetValue("itemClass", typeof(string));
			this.m_unlockableName = (string)info.GetValue("itemName", typeof(string));
			this.m_unlockableDescription = (string)info.GetValue("itemDescription", typeof(string));
			this.m_unlockableIcon = (string)info.GetValue("itemIcon", typeof(string));
			try
			{
				this.m_unlockableItemLevel = (int)info.GetValue("itemLevel", typeof(int));
			}
			catch
			{
				this.m_unlockableItemLevel = 0;
			}
		}
		if (this.m_nodeDataType == MetagameNodeDataType.UnlockableUpgradeable)
		{
			this.m_dataIdentifier = (string)info.GetValue("itemClass", typeof(string));
			this.m_unlockableName = (string)info.GetValue("itemName", typeof(string));
			this.m_unlockableDescription = (string)info.GetValue("itemDescription", typeof(string));
			this.m_unlockableIcon = (string)info.GetValue("itemIcon", typeof(string));
			this.m_unlockableGraphNodeClass = (string)info.GetValue("itemNodeType", typeof(string));
			this.m_unlockableMaxCount = (int)info.GetValue("itemMaxCount", typeof(int));
			this.m_unlockableComplexity = (int)info.GetValue("itemComplexity", typeof(int));
			try
			{
				this.m_unlockableItemLevel = (int)info.GetValue("itemLevel", typeof(int));
			}
			catch
			{
				this.m_unlockableItemLevel = 0;
			}
			this.m_unlockableUpgradeValues = (Hashtable)info.GetValue("vehicleUpgradeValues", typeof(Hashtable));
			this.m_unlockableUpgradeSteps = (int)info.GetValue("vehicleUpgradeSteps", typeof(int));
			this.m_unlockableUpgradePrices = (string[])info.GetValue("vehicleUpgradePrices", typeof(string[]));
			try
			{
				this.m_unlockableRentName = (string)info.GetValue("rentName", typeof(string));
			}
			catch
			{
				this.m_unlockableRentName = "Rentzal";
			}
			try
			{
				this.m_unlockableRentButton = (string)info.GetValue("rentButton", typeof(string));
			}
			catch
			{
				this.m_unlockableRentButton = "Rentz this";
			}
		}
		if (this.m_nodeDataType == MetagameNodeDataType.Level)
		{
			try
			{
				this.m_levelMinigameId = (string)info.GetValue("levelMinigameId", typeof(string));
			}
			catch
			{
				this.m_levelMinigameId = string.Empty;
			}
			this.m_levelDifficulty = (PsGameDifficulty)Enum.Parse(typeof(PsGameDifficulty), (string)info.GetValue("levelDifficulty", typeof(string)));
			this.m_levelPlayerUnit = (string)info.GetValue("levelPlayerUnit", typeof(string));
			this.m_levelSubgenre = (PsSubgenre)Enum.Parse(typeof(PsSubgenre), (string)info.GetValue("levelSubgenre", typeof(string)));
			this.m_levelGameMode = (PsGameMode)Enum.Parse(typeof(PsGameMode), (string)info.GetValue("levelGameMode", typeof(string)));
			this.m_levelItems = new List<string>((string[])info.GetValue("levelItems", typeof(string[])));
			try
			{
				this.m_levelMedalTimes = (string[])info.GetValue("levelMedalTimes", typeof(string[]));
			}
			catch
			{
				this.m_levelMedalTimes = new string[3];
			}
		}
		if (this.m_nodeDataType == MetagameNodeDataType.EditorPuzzle)
		{
			try
			{
				this.m_levelMinigameId = (string)info.GetValue("levelMinigameId", typeof(string));
			}
			catch
			{
				this.m_levelMinigameId = string.Empty;
			}
			this.m_levelItems = new List<string>((string[])info.GetValue("puzzleLimitedItems", typeof(string[])));
		}
		if (this.m_nodeDataType == MetagameNodeDataType.VersusRace)
		{
			try
			{
				this.m_levelMinigameId = (string)info.GetValue("levelMinigameId", typeof(string));
			}
			catch
			{
				this.m_levelMinigameId = string.Empty;
			}
		}
		if (this.m_nodeDataType == MetagameNodeDataType.Signal)
		{
			try
			{
				this.m_levelMinigameId = (string)info.GetValue("levelMinigameId", typeof(string));
			}
			catch
			{
				this.m_levelMinigameId = string.Empty;
			}
			this.m_blockUnlocked = info.GetString("blockUnlocked");
			try
			{
				this.m_blockWillUnlock = info.GetString("blockWillUnlock");
			}
			catch
			{
				this.m_blockWillUnlock = string.Empty;
			}
		}
		if (this.m_nodeDataType == MetagameNodeDataType.Block)
		{
			try
			{
				this.m_levelMinigameId = (string)info.GetValue("levelMinigameId", typeof(string));
			}
			catch
			{
				this.m_levelMinigameId = string.Empty;
			}
			this.m_blockRequiredStars = info.GetInt32("blockRequiredStars");
			try
			{
				this.m_blockType = (PsPathBlockType)Enum.Parse(typeof(PsPathBlockType), info.GetString("blockType"));
			}
			catch
			{
				this.m_blockType = PsPathBlockType.Chest;
			}
			this.m_blockBolts = info.GetInt32("blockBolts");
			this.m_blockCoins = info.GetInt32("blockCoins");
			this.m_blockDiamonds = info.GetInt32("blockDiamonds");
			try
			{
				this.m_blockKeys = info.GetInt32("blockKeys");
			}
			catch
			{
				this.m_blockKeys = 0;
			}
			this.m_blockUnlocked = info.GetString("blockUnlocked");
			try
			{
				this.m_blockWillUnlock = info.GetString("blockWillUnlock");
			}
			catch
			{
				this.m_blockWillUnlock = string.Empty;
			}
			try
			{
				this.m_dataIdentifier = info.GetString("blockRewardCue");
			}
			catch
			{
				this.m_dataIdentifier = string.Empty;
			}
			try
			{
				this.m_blockChestType = (PsPathBlockChestType)Enum.Parse(typeof(PsPathBlockChestType), info.GetString("blockChestType"));
			}
			catch
			{
				this.m_blockChestType = PsPathBlockChestType.Silver;
			}
		}
		if (this.m_nodeDataType == MetagameNodeDataType.Dialogue)
		{
			this.m_dataIdentifier = (string)info.GetValue("dialogueIdentifier", typeof(string));
			try
			{
				this.m_dialogueTrigger = (PsNodeEventTrigger)Enum.Parse(typeof(PsNodeEventTrigger), (string)info.GetValue("dialogueTrigger", typeof(string)));
			}
			catch
			{
				this.m_dialogueTrigger = PsNodeEventTrigger.MinigameLose;
			}
			this.m_dialogueCharacter = (PsDialogueCharacter)Enum.Parse(typeof(PsDialogueCharacter), (string)info.GetValue("dialogueCharacter", typeof(string)));
			this.m_dialogueCharacterPosition = (PsDialogueCharacterPosition)Enum.Parse(typeof(PsDialogueCharacterPosition), (string)info.GetValue("dialogueCharacterPosition", typeof(string)));
			this.m_dialogueText = (string)info.GetValue("dialogueText", typeof(string));
			this.m_dialogueProceed = (string)info.GetValue("dialogueProceed", typeof(string));
			this.m_dialogueCancel = (string)info.GetValue("dialogueCancel", typeof(string));
			try
			{
				this.m_dialogueTextLocalized = (StringID)Enum.Parse(typeof(StringID), info.GetString("dialogueTextLocalized"));
			}
			catch
			{
				this.m_dialogueTextLocalized = StringID.EMPTY;
			}
		}
		if (this.m_nodeDataType == MetagameNodeDataType.LevelTemplate)
		{
			this.m_dataIdentifier = (string)info.GetValue("levelTemplateIdentifier", typeof(string));
			this.m_levelPlayerUnit = (string)info.GetValue("levelPlayerUnit", typeof(string));
			this.m_levelGameMode = (PsGameMode)Enum.Parse(typeof(PsGameMode), (string)info.GetValue("levelGameMode", typeof(string)));
			this.m_levelTemplateDomeSize = (PsGameArea)Enum.Parse(typeof(PsGameArea), (string)info.GetValue("levelTemplateDomeSize", typeof(string)));
			this.m_levelItems = new List<string>((string[])info.GetValue("levelTemplateItems", typeof(string[])));
			this.m_levelTemplateMinigames = new List<string>((string[])info.GetValue("levelTemplateMinigames", typeof(string[])));
		}
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x00094FEC File Offset: 0x000933EC
	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("geNodeType", this.m_nodeDataType.ToString());
		if (this.m_nodeDataType == MetagameNodeDataType.Unlock)
		{
			info.AddValue("itemClass", this.m_dataIdentifier);
			info.AddValue("itemName", this.m_unlockableName);
			info.AddValue("itemDescription", this.m_unlockableDescription);
			info.AddValue("itemIcon", this.m_unlockableIcon);
		}
		if (this.m_nodeDataType == MetagameNodeDataType.UnlockableCategory)
		{
			info.AddValue("itemType", this.m_unlockableType.ToString());
			info.AddValue("itemName", this.m_unlockableName);
			info.AddValue("itemDescription", this.m_unlockableDescription);
			info.AddValue("itemIcon", this.m_unlockableIcon);
		}
		if (this.m_nodeDataType == MetagameNodeDataType.Unlockable)
		{
			info.AddValue("itemClass", this.m_dataIdentifier);
			info.AddValue("itemName", this.m_unlockableName);
			info.AddValue("itemDescription", this.m_unlockableDescription);
			info.AddValue("itemIcon", this.m_unlockableIcon);
			info.AddValue("itemNodeType", this.m_unlockableGraphNodeClass);
			info.AddValue("itemMaxCount", this.m_unlockableMaxCount);
			info.AddValue("itemComplexity", this.m_unlockableComplexity);
			info.AddValue("itemLevel", this.m_unlockableItemLevel);
			info.AddValue("itemGachaProbability", this.m_unlockableGachaProbability);
			info.AddValue("itemRarity", this.m_unlockableRarity.ToString());
			info.AddValue("itemCurrency", this.m_unlockableCurrency.ToString());
			info.AddValue("itemPrice", this.m_unlockablePrice);
		}
		if (this.m_nodeDataType == MetagameNodeDataType.UnlockableSimple)
		{
			info.AddValue("itemClass", this.m_dataIdentifier);
			info.AddValue("itemName", this.m_unlockableName);
			info.AddValue("itemDescription", this.m_unlockableDescription);
			info.AddValue("itemIcon", this.m_unlockableIcon);
			info.AddValue("itemLevel", this.m_unlockableItemLevel);
		}
		if (this.m_nodeDataType == MetagameNodeDataType.UnlockableUpgradeable)
		{
			info.AddValue("itemClass", this.m_dataIdentifier);
			info.AddValue("itemName", this.m_unlockableName);
			info.AddValue("itemDescription", this.m_unlockableDescription);
			info.AddValue("itemIcon", this.m_unlockableIcon);
			info.AddValue("itemNodeType", this.m_unlockableGraphNodeClass);
			info.AddValue("itemMaxCount", this.m_unlockableMaxCount);
			info.AddValue("itemComplexity", this.m_unlockableComplexity);
			info.AddValue("itemLevel", this.m_unlockableItemLevel);
			info.AddValue("vehicleUpgradeValues", this.m_unlockableUpgradeValues);
			info.AddValue("vehicleUpgradeSteps", this.m_unlockableUpgradeSteps);
			info.AddValue("vehicleUpgradePrices", this.m_unlockableUpgradePrices);
			info.AddValue("rentName", this.m_unlockableRentName);
			info.AddValue("rentButton", this.m_unlockableRentButton);
		}
		if (this.m_nodeDataType == MetagameNodeDataType.Level)
		{
			if (this.m_levelMinigameMetaData != null)
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameMetaData.id);
			}
			else
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameId);
			}
			info.AddValue("levelDifficulty", this.m_levelDifficulty.ToString());
			info.AddValue("levelPlayerUnit", this.m_levelPlayerUnit);
			info.AddValue("levelSubgenre", this.m_levelSubgenre.ToString());
			info.AddValue("levelGameMode", this.m_levelGameMode.ToString());
			info.AddValue("levelItems", this.m_levelItems.ToArray());
			info.AddValue("levelMedalTimes", this.m_levelMedalTimes);
		}
		if (this.m_nodeDataType == MetagameNodeDataType.EditorPuzzle)
		{
			if (this.m_levelMinigameMetaData != null)
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameMetaData.id);
			}
			else
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameId);
			}
			info.AddValue("puzzleLimitedItems", this.m_levelItems.ToArray());
		}
		if (this.m_nodeDataType == MetagameNodeDataType.Signal)
		{
			if (this.m_levelMinigameMetaData != null)
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameMetaData.id);
			}
			else
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameId);
			}
			info.AddValue("blockUnlocked", this.m_blockUnlocked);
			info.AddValue("blockWillUnlock", this.m_blockWillUnlock);
		}
		if (this.m_nodeDataType == MetagameNodeDataType.VersusRace)
		{
			if (this.m_levelMinigameMetaData != null)
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameMetaData.id);
			}
			else
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameId);
			}
		}
		if (this.m_nodeDataType == MetagameNodeDataType.Block)
		{
			if (this.m_levelMinigameMetaData != null)
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameMetaData.id);
			}
			else
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameId);
			}
			info.AddValue("blockRequiredStars", this.m_blockRequiredStars);
			info.AddValue("blockType", this.m_blockType.ToString());
			info.AddValue("blockBolts", this.m_blockBolts);
			info.AddValue("blockCoins", this.m_blockCoins);
			info.AddValue("blockDiamonds", this.m_blockDiamonds);
			info.AddValue("blockKeys", this.m_blockKeys);
			info.AddValue("blockUnlocked", this.m_blockUnlocked);
			info.AddValue("blockWillUnlock", this.m_blockWillUnlock);
			info.AddValue("blockRewardCue", this.m_dataIdentifier);
			info.AddValue("blockChestType", this.m_blockChestType.ToString());
		}
		if (this.m_nodeDataType == MetagameNodeDataType.Dialogue)
		{
			info.AddValue("dialogueIdentifier", this.m_dataIdentifier);
			info.AddValue("dialogueTrigger", this.m_dialogueTrigger.ToString());
			info.AddValue("dialogueCharacter", this.m_dialogueCharacter.ToString());
			info.AddValue("dialogueCharacterPosition", this.m_dialogueCharacterPosition.ToString());
			info.AddValue("dialogueText", this.m_dialogueText);
			info.AddValue("dialogueProceed", this.m_dialogueProceed);
			info.AddValue("dialogueCancel", this.m_dialogueCancel);
			info.AddValue("dialogueTextLocalized", this.m_dialogueTextLocalized.ToString());
		}
		if (this.m_nodeDataType == MetagameNodeDataType.LevelTemplate)
		{
			info.AddValue("levelTemplateIdentifier", this.m_dataIdentifier);
			if (this.m_levelMinigameMetaData != null)
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameMetaData.id);
			}
			else
			{
				info.AddValue("levelMinigameId", this.m_levelMinigameId);
			}
			info.AddValue("levelPlayerUnit", this.m_levelPlayerUnit);
			info.AddValue("levelGameMode", this.m_levelGameMode.ToString());
			info.AddValue("levelTemplateDomeSize", this.m_levelTemplateDomeSize.ToString());
			info.AddValue("levelTemplateItems", this.m_levelItems.ToArray());
			info.AddValue("levelTemplateMinigames", this.m_levelTemplateMinigames.ToArray());
		}
	}

	// Token: 0x0400126C RID: 4716
	public MetagameNodeDataType m_nodeDataType;

	// Token: 0x0400126D RID: 4717
	public string m_dataIdentifier;

	// Token: 0x0400126E RID: 4718
	public List<PsDialogue> m_dialogues;

	// Token: 0x0400126F RID: 4719
	public List<MetagameNodeData> m_sidePathLevels;

	// Token: 0x04001270 RID: 4720
	public byte[] m_levelMinigameBytes;

	// Token: 0x04001271 RID: 4721
	public PsUnlockableType m_unlockableType;

	// Token: 0x04001272 RID: 4722
	public string m_unlockableName;

	// Token: 0x04001273 RID: 4723
	public string m_unlockableDescription;

	// Token: 0x04001274 RID: 4724
	public string m_unlockableIcon;

	// Token: 0x04001275 RID: 4725
	public string m_unlockableGraphNodeClass;

	// Token: 0x04001276 RID: 4726
	public string m_unlockableRentName;

	// Token: 0x04001277 RID: 4727
	public string m_unlockableRentButton;

	// Token: 0x04001278 RID: 4728
	public int m_unlockableMaxCount;

	// Token: 0x04001279 RID: 4729
	public int m_unlockableComplexity;

	// Token: 0x0400127A RID: 4730
	public int m_unlockableOrderIndex;

	// Token: 0x0400127B RID: 4731
	public int m_unlockableItemLevel;

	// Token: 0x0400127C RID: 4732
	public float m_unlockableGachaProbability;

	// Token: 0x0400127D RID: 4733
	public PsRarity m_unlockableRarity;

	// Token: 0x0400127E RID: 4734
	public PsCurrency m_unlockableCurrency;

	// Token: 0x0400127F RID: 4735
	public int m_unlockablePrice;

	// Token: 0x04001280 RID: 4736
	public Hashtable m_unlockableUpgradeValues;

	// Token: 0x04001281 RID: 4737
	public int m_unlockableUpgradeSteps;

	// Token: 0x04001282 RID: 4738
	public string[] m_unlockableUpgradePrices;

	// Token: 0x04001283 RID: 4739
	public PsGameDifficulty m_levelDifficulty;

	// Token: 0x04001284 RID: 4740
	public string m_levelPlayerUnit;

	// Token: 0x04001285 RID: 4741
	public PsSubgenre m_levelSubgenre;

	// Token: 0x04001286 RID: 4742
	public PsGameMode m_levelGameMode;

	// Token: 0x04001287 RID: 4743
	public List<string> m_levelItems;

	// Token: 0x04001288 RID: 4744
	public PsMinigameMetaData m_levelMinigameMetaData;

	// Token: 0x04001289 RID: 4745
	public string m_levelMinigameId;

	// Token: 0x0400128A RID: 4746
	public string[] m_levelMedalTimes;

	// Token: 0x0400128B RID: 4747
	public int m_blockRequiredStars;

	// Token: 0x0400128C RID: 4748
	public PsPathBlockType m_blockType;

	// Token: 0x0400128D RID: 4749
	public int m_blockCoins;

	// Token: 0x0400128E RID: 4750
	public int m_blockDiamonds;

	// Token: 0x0400128F RID: 4751
	public int m_blockBolts;

	// Token: 0x04001290 RID: 4752
	public int m_blockKeys;

	// Token: 0x04001291 RID: 4753
	public string m_blockUnlocked;

	// Token: 0x04001292 RID: 4754
	public string m_blockWillUnlock;

	// Token: 0x04001293 RID: 4755
	public PsPathBlockChestType m_blockChestType;

	// Token: 0x04001294 RID: 4756
	public PsNodeEventTrigger m_dialogueTrigger;

	// Token: 0x04001295 RID: 4757
	public string m_eventDialogueIdentifier;

	// Token: 0x04001296 RID: 4758
	public PsDialogueCharacter m_dialogueCharacter;

	// Token: 0x04001297 RID: 4759
	public PsDialogueCharacterPosition m_dialogueCharacterPosition;

	// Token: 0x04001298 RID: 4760
	public string m_dialogueText;

	// Token: 0x04001299 RID: 4761
	public string m_dialogueProceed;

	// Token: 0x0400129A RID: 4762
	public string m_dialogueCancel;

	// Token: 0x0400129B RID: 4763
	public string m_dialogueUnlockIdentifier;

	// Token: 0x0400129C RID: 4764
	public StringID m_dialogueTextLocalized;

	// Token: 0x0400129D RID: 4765
	public PsGameArea m_levelTemplateDomeSize;

	// Token: 0x0400129E RID: 4766
	public List<string> m_levelTemplateMinigames;
}
