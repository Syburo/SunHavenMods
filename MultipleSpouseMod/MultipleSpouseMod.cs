using BepInEx;
using HarmonyLib;
using Wish;
using BepInEx.Logging;
using I2.Loc;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using System.Reflection;
using System.Linq;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

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
        private static bool Prefix(ref string __result, out bool response, NPCAI __instance, ref string ____npcName)
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
            foreach (var QuestName in Player.Instance.QuestList.questLog)
            {
                if (QuestName.Key.Contains("MarriageQuest"))
                {
                    flag = true;
                    __result = "You are already engaged.";
                    Player.Instance.Inventory.AddItem(6107, 1, false);
                    return false;
                }
            }
            if (flag)
            {
                __result = str + "[]" + ScriptLocalization.RNPC_Generic_DeclineProposal;
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
                    __result = ScriptLocalization.RNPC_Wornhardt_AcceptProposal;
                    return false;
                case "Zaria":
                    Player.Instance.QuestList.StartQuest("ZariaMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Zaria_AcceptProposal;
                    return false;
                case "Donovan":
                    Player.Instance.QuestList.StartQuest("DonovanMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Donovan_AcceptProposal;
                    return false;
                case "Karish":
                    Player.Instance.QuestList.StartQuest("KarishMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Karish_AcceptProposal;
                    return false;
                case "Claude":
                    Player.Instance.QuestList.StartQuest("ClaudeMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Claude_AcceptProposal;
                    return false;
                case "Jun":
                    Player.Instance.QuestList.StartQuest("JunMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Jun_AcceptProposal;
                    return false;
                case "Vivi":
                    Player.Instance.QuestList.StartQuest("ViviMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Vivi_AcceptProposal;
                    return false;
                case "Lynn":
                    Player.Instance.QuestList.StartQuest("LynnMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Lynn_AcceptProposal;
                    return false;
                case "Liam":
                    Player.Instance.QuestList.StartQuest("LiamMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Liam_AcceptProposal;
                    return false;
                case "Anne":
                    Player.Instance.QuestList.StartQuest("AnneMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Anne_AcceptProposal;
                    return false;
                case "Miyeon":
                    Player.Instance.QuestList.StartQuest("MiyeonMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Miyeon_AcceptProposal;
                    return false;
                case "Kitty":
                    Player.Instance.QuestList.StartQuest("KittyMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Kitty_AcceptProposal;
                    return false;
                case "Lucius":
                    Player.Instance.QuestList.StartQuest("LuciusMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Lucius_AcceptProposal;
                    return false;
                case "Shang":
                    Player.Instance.QuestList.StartQuest("ShangMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Shang_AcceptProposal;
                    return false;
                case "Wesley":
                    Player.Instance.QuestList.StartQuest("WesleyMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Wesley_AcceptProposal;
                    return false;
                case "Darius":
                    Player.Instance.QuestList.StartQuest("DariusMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Darius_AcceptProposal;
                    return false;
                case "Xyla":
                    Player.Instance.QuestList.StartQuest("XylaMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Xyla_AcceptProposal;
                    return false;
                case "Catherine":
                    Player.Instance.QuestList.StartQuest("CatherineMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Catherine_AcceptProposal;
                    return false;
                case "Lucia":
                    Player.Instance.QuestList.StartQuest("LuciaMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Lucia_AcceptProposal;
                    return false;
                case "Iris":
                    Player.Instance.QuestList.StartQuest("IrisMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Iris_AcceptProposal;
                    return false;
                case "Kai":
                    Player.Instance.QuestList.StartQuest("KaiMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Kai_AcceptProposal;
                    return false;
                case "Vaan":
                    Player.Instance.QuestList.StartQuest("VaanMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Vaan_AcceptProposal;
                    return false;
                case "Nathaniel":
                    Player.Instance.QuestList.StartQuest("NathanielMarriageQuest", false);
                    __result = ScriptLocalization.RNPC_Nathaniel_AcceptProposal;
                    return false;
                default:
                    __result = ScriptLocalization.RNPC_Generic_AcceptProposal;
                    return false;

            }
        }
    }

    [HarmonyPatch(typeof(Player), "RequestSleep")]
    class HarmonyPatch_Player_RequestSleep
    {
        private static bool Prefix(bool isMarriageBed = false, MarriageOvernightCutscene marriageOvernightCutscene = null, bool isCutsceneComplete = false)
        {
            if (isMarriageBed && !isCutsceneComplete)
            {
                string sleep = "Early";
                if (SingletonBehaviour<DayCycle>.Instance.Time.Hour >= 18 || SingletonBehaviour<DayCycle>.Instance.Time.Hour < 6)
                {
                    sleep = "Sleep";
                }

                LocalizedDialogueTree localizedDialogueTree0 = new LocalizedDialogueTree
                {
                    npc = null
                };


                string name0 = "Bed";
                DialogueNode dialogueNode0 = new DialogueNode();
                dialogueNode0.dialogueText = new List<string>
                {
                    "What do you want to do?"
                };

                Dictionary<int, Response> dictionary0 = new Dictionary<int, Response>();

                Response response01 = new Response();
                response01.responseText = (() => "Sleep");
                response01.action = delegate ()
                {
                    localizedDialogueTree0.Talk(sleep, true, null);
                };
                dictionary0.Add(0, response01);

                Response response02 = new Response();
                response02.responseText = (() => "Change main spouse");
                response02.action = delegate ()
                {
                    localizedDialogueTree0.Talk("ChangeMainSpouse1", true, null);
                };
                dictionary0.Add(1, response02);
                dialogueNode0.responses = dictionary0;

                Response response03 = new Response();
                response03.responseText = (() => "Nothing");
                response03.action = delegate ()
                {
                    DialogueController.Instance.CancelDialogue();
                };
                dictionary0.Add(2, response03);
                dialogueNode0.responses = dictionary0;
                localizedDialogueTree0.AddNode(name0, dialogueNode0);


                string name1 = "Sleep";
                DialogueNode dialogueNode1 = new DialogueNode();
                dialogueNode1.dialogueText = new List<string>
                {
                    ScriptLocalization.SleepRequestSpouse
                };
                Dictionary<int, Response> dictionary1 = new Dictionary<int, Response>();

                Response response04 = new Response();
                response04.responseText = (() => ScriptLocalization.Yes);
                response04.action = delegate ()
                {
                    marriageOvernightCutscene.Begin();
                    DialogueController.Instance.CancelDialogue();
                };
                dictionary1.Add(0, response04);

                Response response05 = new Response();
                response05.responseText = (() => ScriptLocalization.No);
                response05.action = delegate ()
                {
                    DialogueController.Instance.CancelDialogue();
                };
                dictionary1.Add(1, response05);

                dialogueNode1.responses = dictionary1;
                localizedDialogueTree0.AddNode(name1, dialogueNode1);


                string name2 = "Early";
                DialogueNode dialogueNode2 = new DialogueNode();
                dialogueNode2.dialogueText = new List<string>
                {
                    ScriptLocalization.TooEarlyToSleep
                };
                localizedDialogueTree0.AddNode(name2, dialogueNode2);


                string name3 = "ChangeMainSpouse";

                List<string> spouses = new List<string>();
                Dictionary<int, Response> dictionary2 = new Dictionary<int, Response>();
                int i = 0;
                int page = 1;
                string page_name;

                foreach (var npcai in SingletonBehaviour<NPCManager>.Instance._npcs.Values.Where(npcai => SingletonBehaviour<GameSave>.Instance.GetProgressBoolCharacter("MarriedTo" + npcai.OriginalName)))
                {
                    spouses.Add(npcai.OriginalName);
                }
                foreach (var name in spouses)
                {
                    if (i == 3)
                    {
                        DialogueNode dialogueNode3 = new DialogueNode();
                        dialogueNode3.dialogueText = new List<string>
                            {
                                "Who will be the main spouse?"
                            };
                        page_name = name3 + page;
                        page += 1;
                        string next_page_name = name3 + page;

                        Response next_page = new Response();
                        next_page.responseText = (() => "Next");
                        next_page.action = delegate ()
                        {
                            localizedDialogueTree0.Talk(next_page_name, true, null);
                        };
                        dictionary2.Add(i, next_page);
                        dialogueNode3.responses = dictionary2;
                        localizedDialogueTree0.AddNode(page_name, dialogueNode3);


                        i = 0;
                        dictionary2 = new Dictionary<int, Response>();
                    }

                    Response response = new Response();
                    response.responseText = (() => name);
                    response.action = delegate ()
                    {
                        string current_spouse = SingletonBehaviour<GameSave>.Instance.GetProgressStringCharacter("MarriedWith");

                        SingletonBehaviour<GameSave>.Instance.SetProgressBoolWorld(current_spouse + "MarriedWalkPath", false, true);
                        NPCAI realNPC = SingletonBehaviour<NPCManager>.Instance.GetRealNPC(current_spouse);
                        realNPC.GeneratePath();
                        SingletonBehaviour<NPCManager>.Instance.StartNPCPath(realNPC);

                        SingletonBehaviour<GameSave>.Instance.SetProgressStringCharacter("MarriedWith", name);
                        localizedDialogueTree0.Talk("Reenter", true, null);
                    };
                    dictionary2.Add(i, response);
                    i += 1;

                }

                DialogueNode dialogueNode4 = new DialogueNode();
                dialogueNode4.dialogueText = new List<string>
                {
                    "Who will be the main spouse?"
                };
                page_name = name3 + page;

                Response nevermind = new Response();
                nevermind.responseText = (() => "Nevermind");
                nevermind.action = delegate ()
                {
                    DialogueController.Instance.CancelDialogue();
                };
                dictionary2.Add(i, nevermind);
                dialogueNode4.responses = dictionary2;
                localizedDialogueTree0.AddNode(page_name, dialogueNode4);

                string name4 = "Reenter";
                DialogueNode dialogueNode5 = new DialogueNode();
                dialogueNode5.dialogueText = new List<string>
                {
                    "Re-enter the house to apply changes."
                };
                localizedDialogueTree0.AddNode(name4, dialogueNode5);

                localizedDialogueTree0.Talk("Bed", true, null);
                return true;
            }
            else
            {
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(LocalizedDialogueTree), "BernardHandleDivorce")]
    class HarmonyPatch_Player_BernardHandleDivorce
    {
        private static LocalizedDialogueTree HandleConfirm(LocalizedDialogueTree localizedDialogueTree0, string name, string new_main)
        {
            string name1 = "ConfirmDivorce";
            DialogueNode dialogueNode2 = new DialogueNode();
            dialogueNode2.dialogueText = new List<string>
            {
                $"Are you sure you want to divorce {name}?"
            };
            Dictionary<int, Response> dictionary1 = new Dictionary<int, Response>();

            Response response01 = new Response();
            response01.responseText = (() => ScriptLocalization.Yes);
            response01.action = delegate ()
            {
                Utilities.UnlockAcheivement(131);
                if (SingletonBehaviour<GameSave>.Instance.GetProgressStringCharacter("MarriedWith") == name)
                {
                    SingletonBehaviour<GameSave>.Instance.SetProgressStringCharacter("MarriedWith", new_main);
                }
                SingletonBehaviour<GameSave>.Instance.SetProgressBoolCharacter("MarriedTo" + name, false);
                NPCAI realNPC = SingletonBehaviour<NPCManager>.Instance.GetRealNPC(name);
                realNPC.GeneratePath();
                SingletonBehaviour<NPCManager>.Instance.StartNPCPath(realNPC);
                GameSave.CurrentCharacter.Relationships[name] = 40f;
                SingletonBehaviour<NPCManager>.Instance.GetRealNPC(name).GenerateCycle(false);
            };
            dictionary1.Add(0, response01);

            Response response05 = new Response();
            response05.responseText = (() => ScriptLocalization.No);
            response05.action = delegate ()
            {
                DialogueController.Instance.CancelDialogue();
            };
            dictionary1.Add(1, response05);

            dialogueNode2.responses = dictionary1;
            localizedDialogueTree0.AddNode(name1, dialogueNode2);
            return localizedDialogueTree0;
        }
        private static bool Prefix()
        {
            LocalizedDialogueTree localizedDialogueTree0 = new LocalizedDialogueTree
            {
                npc = null
            };

            string name0 = "Divorce";

            List<string> spouses = new List<string>();
            Dictionary<int, Response> dictionary0 = new Dictionary<int, Response>();
            int i = 0;
            int page = 1;
            string page_name;
            int count = 0;
            string new_main;

            foreach (var npcai in SingletonBehaviour<NPCManager>.Instance._npcs.Values.Where(npcai => SingletonBehaviour<GameSave>.Instance.GetProgressBoolCharacter("MarriedTo" + npcai.OriginalName)))
            {
                spouses.Add(npcai.OriginalName);
                count += 1;
            }
            if (count <= 1)
            {
                return true;
            }
            foreach (var name in spouses)
            {
                if (i == 3)
                {
                    DialogueNode dialogueNode0 = new DialogueNode();
                    dialogueNode0.dialogueText = new List<string>
                    {
                        "Who do you want to divorce?"
                    };
                    page_name = name0 + page;
                    page += 1;
                    string next_page_name = name0 + page;

                    Response next_page = new Response();
                    next_page.responseText = (() => "Next");
                    next_page.action = delegate ()
                    {
                        localizedDialogueTree0.Talk(next_page_name, true, null);
                    };
                    dictionary0.Add(i, next_page);
                    dialogueNode0.responses = dictionary0;
                    localizedDialogueTree0.AddNode(page_name, dialogueNode0);


                    i = 0;
                    dictionary0 = new Dictionary<int, Response>();
                }

                Response response = new Response();
                response.responseText = (() => name);
                response.action = delegate ()
                {
                    if (spouses[0] == name)
                    {
                        new_main = spouses[1];
                    } else
                    {
                        new_main = spouses[0];
                    }
                    
                    localizedDialogueTree0 = HandleConfirm(localizedDialogueTree0, name, new_main);
                    localizedDialogueTree0.Talk("ConfirmDivorce", true, null);
                };
                dictionary0.Add(i, response);
                i += 1;

            }

            DialogueNode dialogueNode1 = new DialogueNode();
            dialogueNode1.dialogueText = new List<string>
            {
                "Who do you want to divorce?"
            };
            page_name = name0 + page;

            Response nevermind = new Response();
            nevermind.responseText = (() => "Nevermind");
            nevermind.action = delegate ()
            {
                DialogueController.Instance.CancelDialogue();
            };
            dictionary0.Add(i, nevermind);
            dialogueNode1.responses = dictionary0;
            localizedDialogueTree0.AddNode(page_name, dialogueNode1);

            localizedDialogueTree0.Talk("Divorce1", true, null);

            return false;
        }
    }
}
