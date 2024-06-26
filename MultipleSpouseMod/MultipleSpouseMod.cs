using BepInEx;
using HarmonyLib;
using Wish;
using BepInEx.Logging;
using I2.Loc;

[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
public class MultipleSpouseMod : BaseUnityPlugin
{
    public const string pluginGuid = "syburo.sunhaven.multiplespousemod";
    public const string pluginName = "Multiple Spouse Mod";
    public const string pluginVersion = "0.1.0";
    private Harmony m_harmony = new Harmony(pluginGuid);
    public static ManualLogSource logger;

    public void Awake()
    {
        logger = this.Logger;
        logger.LogInfo((object)$"Plugin {pluginName} is loaded!");
        this.m_harmony.PatchAll();
    }

    [HarmonyPatch(typeof(NPCAI), "HandleWeddingRing")]
    class HarmonyPatch_NPCAI_HandleWeddingRing
    {
        private static string Postfix(string __result, out bool response, NPCAI __instance, ref string ____npcName)
        {
            response = false;
            bool flag = false;
            string str = "";
            float value;
            if (__instance.IsMarriedToPlayer())
            {
                flag = true;
                switch (____npcName)
                {
                    case "Wornhardt":
                        str = "";
                        break;
                    case "Zaria":
                        str = ScriptLocalization.RNPC_Zaria_DeclineProposal_01;
                        break;
                    case "Donovan":
                        str = "";
                        break;
                    case "Karish":
                        str = ScriptLocalization.RNPC_Karish_DeclineProposal_01;
                        break;
                    case "Claude":
                        str = "";
                        break;
                    case "Jun":
                        str = "";
                        break;
                    case "Vivi":
                        str = ScriptLocalization.RNPC_Vivi_DeclineProposal_01;
                        break;
                    case "Lynn":
                        str = ScriptLocalization.RNPC_Lynn_DeclineProposal_01;
                        break;
                    case "Liam":
                        str = "";
                        break;
                    case "Anne":
                        str = ScriptLocalization.RNPC_Anne_DeclineProposal_01;
                        break;
                    case "Miyeon":
                        str = ScriptLocalization.RNPC_Miyeon_DeclineProposal_01;
                        break;
                    case "Kitty":
                        str = "";
                        break;
                    case "Lucius":
                        str = ScriptLocalization.RNPC_Lucius_DeclineProposal_01;
                        break;
                    case "Shang":
                        str = ScriptLocalization.RNPC_Shang_DeclineProposal_01;
                        break;
                    case "Wesley":
                        str = ScriptLocalization.RNPC_Wesley_DeclineProposal_01;
                        break;
                    case "Darius":
                        str = "";
                        break;
                    case "Xyla":
                        str = "";
                        break;
                    case "Catherine":
                        str = "";
                        break;
                    case "Lucia":
                        str = "";
                        break;
                    case "Iris":
                        str = "";
                        break;
                    case "Kai":
                        str = ScriptLocalization.RNPC_Kai_DeclineProposal_01;
                        break;
                    case "Vaan":
                        str = "";
                        break;
                    case "Nathaniel":
                        str = "";
                        break;
                }
            }
            else if (
                !__instance.IsDatingPlayer() ||
                !SingletonBehaviour<GameSave>.Instance.GetProgressBoolCharacter(____npcName + " Cycle 14") ||
                !SingletonBehaviour<GameSave>.Instance.CurrentSave.characterData.Relationships.TryGetValue(____npcName, out value) ||
                value < 75f)
            {
                flag = true;
                switch (____npcName)
                {
                    case "Wornhardt":
                        str = "";
                        break;
                    case "Zaria":
                        str = ScriptLocalization.RNPC_Zaria_DeclineProposal_00;
                        break;
                    case "Donovan":
                        str = "";
                        break;
                    case "Karish":
                        str = ScriptLocalization.RNPC_Karish_DeclineProposal_00;
                        break;
                    case "Claude":
                        str = "";
                        break;
                    case "Jun":
                        str = "";
                        break;
                    case "Vivi":
                        str = ScriptLocalization.RNPC_Vivi_DeclineProposal_00;
                        break;
                    case "Lynn":
                        str = ScriptLocalization.RNPC_Lynn_DeclineProposal_00;
                        break;
                    case "Liam":
                        str = "";
                        break;
                    case "Anne":
                        str = ScriptLocalization.RNPC_Anne_DeclineProposal_00;
                        break;
                    case "Miyeon":
                        str = ScriptLocalization.RNPC_Miyeon_DeclineProposal_00;
                        break;
                    case "Kitty":
                        str = "";
                        break;
                    case "Lucius":
                        str = ScriptLocalization.RNPC_Lucius_DeclineProposal_00;
                        break;
                    case "Shang":
                        str = ScriptLocalization.RNPC_Shang_DeclineProposal_00;
                        break;
                    case "Wesley":
                        str = ScriptLocalization.RNPC_Wesley_DeclineProposal_00;
                        break;
                    case "Darius":
                        str = "";
                        break;
                    case "Xyla":
                        str = "";
                        break;
                    case "Catherine":
                        str = "";
                        break;
                    case "Lucia":
                        str = "";
                        break;
                    case "Iris":
                        str = "";
                        break;
                    case "Kai":
                        str = ScriptLocalization.RNPC_Kai_DeclineProposal_00;
                        break;
                    case "Vaan":
                        str = "";
                        break;
                    case "Nathaniel":
                        str = "";
                        break;
                }
            }
            if (Player.Instance.QuestList.HasQuest($"{__instance.OriginalName}MarriageQuest"))
            {
                flag = true;
                return "You are already engaged.";
            }
            if (flag)
            {
                return str + "[]" + ScriptLocalization.RNPC_Generic_DeclineProposal;
            }
            if (SingletonBehaviour<GameSave>.Instance.GetProgressBoolCharacter("Married"))
            {
                Player.Instance.Inventory.RemoveItem(6107, 1);
            }
            response = true;
            
            SingletonBehaviour<GameSave>.Instance.SetProgressBoolCharacter("EngagedToRNPC", value: true);
            switch (____npcName)
            {
                case "Wornhardt":
                    Player.Instance.QuestList.StartQuest("WornhardtMarriageQuest", false);
                    return ScriptLocalization.RNPC_Wornhardt_AcceptProposal;
                case "Zaria":
                    Player.Instance.QuestList.StartQuest("ZariaMarriageQuest", false);
                    return ScriptLocalization.RNPC_Zaria_AcceptProposal;
                case "Donovan":
                    Player.Instance.QuestList.StartQuest("DonovanMarriageQuest", false);
                    return ScriptLocalization.RNPC_Donovan_AcceptProposal;
                case "Karish":
                    Player.Instance.QuestList.StartQuest("KarishMarriageQuest", false);
                    return ScriptLocalization.RNPC_Karish_AcceptProposal;
                case "Claude":
                    Player.Instance.QuestList.StartQuest("ClaudeMarriageQuest", false);
                    return ScriptLocalization.RNPC_Claude_AcceptProposal;
                case "Jun":
                    Player.Instance.QuestList.StartQuest("JunMarriageQuest", false);
                    return ScriptLocalization.RNPC_Jun_AcceptProposal;
                case "Vivi":
                    Player.Instance.QuestList.StartQuest("ViviMarriageQuest", false);
                    return ScriptLocalization.RNPC_Vivi_AcceptProposal;
                case "Lynn":
                    Player.Instance.QuestList.StartQuest("LynnMarriageQuest", false);
                    return ScriptLocalization.RNPC_Lynn_AcceptProposal;
                case "Liam":
                    Player.Instance.QuestList.StartQuest("LiamMarriageQuest", false);
                    return ScriptLocalization.RNPC_Liam_AcceptProposal;
                case "Anne":
                    Player.Instance.QuestList.StartQuest("AnneMarriageQuest", false);
                    return ScriptLocalization.RNPC_Anne_AcceptProposal;
                case "Miyeon":
                    Player.Instance.QuestList.StartQuest("MiyeonMarriageQuest", false);
                    return ScriptLocalization.RNPC_Miyeon_AcceptProposal;
                case "Kitty":
                    Player.Instance.QuestList.StartQuest("KittyMarriageQuest", false);
                    return ScriptLocalization.RNPC_Kitty_AcceptProposal;
                case "Lucius":
                    Player.Instance.QuestList.StartQuest("LuciusMarriageQuest", false);
                    return ScriptLocalization.RNPC_Lucius_AcceptProposal;
                case "Shang":
                    Player.Instance.QuestList.StartQuest("ShangMarriageQuest", false);
                    return ScriptLocalization.RNPC_Shang_AcceptProposal;
                case "Wesley":
                    Player.Instance.QuestList.StartQuest("WesleyMarriageQuest", false);
                    return ScriptLocalization.RNPC_Wesley_AcceptProposal;
                case "Darius":
                    Player.Instance.QuestList.StartQuest("DariusMarriageQuest", false);
                    return ScriptLocalization.RNPC_Darius_AcceptProposal;
                case "Xyla":
                    Player.Instance.QuestList.StartQuest("XylaMarriageQuest", false);
                    return ScriptLocalization.RNPC_Xyla_AcceptProposal;
                case "Catherine":
                    Player.Instance.QuestList.StartQuest("CatherineMarriageQuest", false);
                    return ScriptLocalization.RNPC_Catherine_AcceptProposal;
                case "Lucia":
                    Player.Instance.QuestList.StartQuest("LuciaMarriageQuest", false);
                    return ScriptLocalization.RNPC_Lucia_AcceptProposal;
                case "Iris":
                    Player.Instance.QuestList.StartQuest("IrisMarriageQuest", false);
                    return ScriptLocalization.RNPC_Iris_AcceptProposal;
                case "Kai":
                    Player.Instance.QuestList.StartQuest("KaiMarriageQuest", false);
                    return ScriptLocalization.RNPC_Kai_AcceptProposal;
                case "Vaan":
                    Player.Instance.QuestList.StartQuest("VaanMarriageQuest", false);
                    return ScriptLocalization.RNPC_Vaan_AcceptProposal;
                case "Nathaniel":
                    Player.Instance.QuestList.StartQuest("NathanielMarriageQuest", false);
                    return ScriptLocalization.RNPC_Nathaniel_AcceptProposal;
                default:
                    return ScriptLocalization.RNPC_Generic_AcceptProposal;

            }
        }
    }
}
