using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using MateriaHelper;
using SlotHelper;
using ItemHelper;
using System;
using Memory;

//SUMMARY
// Created for use with ePSXe v 2.0.5
// Offsets can be changed in gData.cs for other versions of ePSXe
// Cannot guarentee portability to other emulators . . . DuckStation is x64 so that would NOT work

///LICENSE INFORMATION
/// I hold all rights for this creative work. If you use this and distribute you must also include a source.
/// As well as credit to this trainer.
/// Copyright xCENTx 2021

namespace FinalFantasy7_Tutorial
{
    public partial class Main : Form
    {
        public const string PROCESSNAME = "ePSXe.exe";
        bool gameRunning;
        Mem m = new Mem();

        //Dictionary for storing Player Data (Battle Screen)
        Dictionary<string, string> c1Data = new Dictionary<string, string>();
        Dictionary<string, string> c2Data = new Dictionary<string, string>();
        Dictionary<string, string> c3Data = new Dictionary<string, string>();
        
        //Could use some comments
        #region COMBO BOX DATA

        #region ITEMS
        private void ItemSlot_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var ItemList = new List<Items>()
            {
                new Items() { ID = "0x00", Name = "POTION" },
                new Items() { ID = "0x01", Name = "Hi-Potion" },
                new Items() { ID = "0x02", Name = "X-Potion" },
                new Items() { ID = "0x03", Name = "Ether" },
                new Items() { ID = "0x04", Name = "Turbo Ether" },
                new Items() { ID = "0x05", Name = "Elixer" },
                new Items() { ID = "0x06", Name = "Megalixer" },
                new Items() { ID = "0x07", Name = "Phoenix Down" },
                new Items() { ID = "0x08", Name = "Antidote" },
                new Items() { ID = "0x09", Name = "Soft" },
                new Items() { ID = "0x0A", Name = "Maiden's Kiss" },
                new Items() { ID = "0x0B", Name = "Cornucopia" },
                new Items() { ID = "0x0C", Name = "Echo Screen" },
                new Items() { ID = "0x0D", Name = "Hyper" },
                new Items() { ID = "0x0E", Name = "Tranquilizer" },
                new Items() { ID = "0x0F", Name = "Remedy" },
                new Items() { ID = "0x10", Name = "Smoke Bomb" },
                new Items() { ID = "0x11", Name = "Speed Drink" },
                new Items() { ID = "0x12", Name = "Hero Drink" },
                new Items() { ID = "0x13", Name = "Vaccine" },
                new Items() { ID = "0x14", Name = "Grenade" },
                new Items() { ID = "0x15", Name = "Shrapnel" }
            };
            ItemType_comboBox.DataSource = ItemList;
            ItemType_comboBox.ValueMember = "ID";
            ItemType_comboBox.DisplayMember = "Name";
        }
        #endregion

        #region MATERIA
        private void MateriaSlot_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var List2 = new List<Materia>()
            {
                new Materia() { ID = "00", Name = "MP PLUS" }
            };
            MateriaType_comboBox.DataSource = List2;
            MateriaType_comboBox.ValueMember = "ID";       // This assigns a vlue to our Selection
            MateriaType_comboBox.DisplayMember = "Name";   // This is the name shown in the combobox
        }


        #endregion

        #endregion

        //Could use some comments
        #region MAIN FORM
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ItemSlot_comboBox.Items.Clear();
            var ItemList = new List<ItemSlots>()
            {
                new ItemSlots() { ID = gData.ITEMS.SLOT1, Name = "SLOT 1" },
                new ItemSlots() { ID = gData.ITEMS.SLOT2, Name = "SLOT 2" },
                new ItemSlots() { ID = gData.ITEMS.SLOT3, Name = "SLOT 3" },
                new ItemSlots() { ID = gData.ITEMS.SLOT4, Name = "SLOT 4" },
                new ItemSlots() { ID = gData.ITEMS.SLOT5, Name = "SLOT 5" },
                new ItemSlots() { ID = gData.ITEMS.SLOT6, Name = "SLOT 6" },
                new ItemSlots() { ID = gData.ITEMS.SLOT7, Name = "SLOT 7" },
                new ItemSlots() { ID = gData.ITEMS.SLOT8, Name = "SLOT 8" },
                new ItemSlots() { ID = gData.ITEMS.SLOT9, Name = "SLOT 9" },
                new ItemSlots() { ID = gData.ITEMS.SLOT10, Name = "SLOT 10" }
            };
            ItemSlot_comboBox.DataSource = ItemList;
            ItemSlot_comboBox.ValueMember = "ID";
            ItemSlot_comboBox.DisplayMember = "Name";

            //Nothing has been done with this yet
            MateriaSlot_comboBox.Items.Clear();
            var MateriaList = new List<MateriaSlots>()
            {
                new MateriaSlots() { ID = "01", Name = "SLOT 1" },
                new MateriaSlots() { ID = "02", Name = "SLOT 2" },
                new MateriaSlots() { ID = "03", Name = "SLOT 3" },
                new MateriaSlots() { ID = "04", Name = "SLOT 4" },
                new MateriaSlots() { ID = "05", Name = "SLOT 5" },
                new MateriaSlots() { ID = "06", Name = "SLOT 6" },
                new MateriaSlots() { ID = "07", Name = "SLOT 7" },
                new MateriaSlots() { ID = "08", Name = "SLOT 8" },
                new MateriaSlots() { ID = "09", Name = "SLOT 9" },
                new MateriaSlots() { ID = "10", Name = "SLOT 10" }
            };
            MateriaSlot_comboBox.DataSource = MateriaList;
            MateriaSlot_comboBox.ValueMember = "ID";
            MateriaSlot_comboBox.DisplayMember = "Name";

        }

        #region TIMERS
        private void ProcessTimer_Tick(object sender, EventArgs e)
        {
            int PID = m.GetProcIdFromName(PROCESSNAME);
            if (PID > 0)
            {
                m.OpenProcess(PID);
                ProcessIndicator.BackColor = Color.FromArgb(0, 169, 0);
                gameRunning = true;
                return;
            }
            ProcessIndicator.BackColor = Color.FromArgb(169, 0, 0);
            gameRunning = false;
        }

        private void MemoryTimer_Tick(object sender, EventArgs e)
        {
            //Check if the game is running , if not running we won't proceed
            if (!gameRunning)
            {
                return;
            }

            GENERAL();
            BATTLESCREEN();
            AWARDSCREEN();
        }
        #endregion

        #endregion

        //Completed , could use some comments
        #region GENERAL

        private void GENERAL()
        {
            SaveAnywhere();
            StartMenu();
        }

        private void SaveAnywhere()
        {
            //Check if the game is running , if not running we won't proceed
            if (!gameRunning)
            {
                return;
            }

            //See if checkbox is checked , if so continue
            if (SaveAnywhere_Checkbox.Checked)
            {
                //Check the value before proceeding , if the value is already what we want then there is no need to continue
                if (m.ReadByte(gData.GENERAL.SAVE_ANYWHERE_ADDRESS) == 3)
                {
                    m.WriteMemory(gData.GENERAL.SAVE_ANYWHERE_ADDRESS, "byte", "0");
                }
            }
            else if (m.ReadByte(gData.GENERAL.SAVE_ANYWHERE_ADDRESS) == 0)
            {
                m.WriteMemory(gData.GENERAL.SAVE_ANYWHERE_ADDRESS, "byte", "3");
            }
        }

        private void StartMenu()
        {
            //Check if the game is running , if not running we won't proceed
            if (!gameRunning)
            { return; }
            
            //See if checkbox is checked , if so continue
            if (StartMenu_Checkbox.Checked)
            {
                //Check the value before proceeding , if the value is already what we want then there is no need to continue
                if (m.ReadByte(gData.GENERAL.START_MENU_ADDRESS) == 0xFB)
                {
                    m.WriteMemory(gData.GENERAL.START_MENU_ADDRESS, "byte", "0xFF");
                }
            }
            else if (m.ReadByte(gData.GENERAL.START_MENU_ADDRESS) == 0xFF)
            {
                m.WriteMemory(gData.GENERAL.START_MENU_ADDRESS, "byte", "0xFB");
            }
        }

        private void SendGil_Button_Click(object sender, EventArgs e)
        {
            //Check if the game is running , if not running we won't proceed
            if (!gameRunning)
            {
                return;
            }

            //We need to define some variables 
            var GIL = GIL_TextBox.Text;

            //We need to make sure the extbox is not empty
            if (GIL_TextBox.Text != "")
            {
                //Write
                m.WriteMemory(gData.GENERAL.GIL_ADDRESS, "int", GIL.ToString());
            }
        }

        private void SendTime_Button_Click(object sender, EventArgs e)
        {
            //Check if the game is running , if not running we won't proceed
            if (!gameRunning)
            {
                return;
            }

            //We need to define some variables 
            var TIME = Time_TextBox.Text;

            //We need to make sure the textbox is not empty 
            if (Time_TextBox.Text != "")
            {
                //Write
                m.WriteMemory(gData.GENERAL.GAME_TIME_ADDRESS, "int", TIME.ToString());
            }
        }

        #endregion

        //Completed , could use some comments
        #region ITEMS
        private void SendItem_button_Click(object sender, EventArgs e)
        {
            if (!gameRunning)
            {
                return;
            }

            if (ItemQty_textBox.Text != "")
            {
                string SLOT_ADDRESS = ItemSlot_comboBox.SelectedValue.ToString();

                if (SLOT_ADDRESS == gData.ITEMS.SLOT1)
                {
                    var QTY_ADDRESS = gData.ITEMS.SLOT1_QTY;
                    var ITEM = ItemType_comboBox.SelectedValue;
                    var QTY = Convert.ToInt32(ItemQty_textBox.Text);
                    var VALUE = QTY * 2;
                    m.WriteMemory(SLOT_ADDRESS.ToString(), "Byte", ITEM.ToString());
                    m.WriteMemory(QTY_ADDRESS.ToString(), "Byte", VALUE.ToString("X"));
                }

                if (SLOT_ADDRESS == gData.ITEMS.SLOT2)
                {
                    var QTY_ADDRESS = gData.ITEMS.SLOT2_QTY;
                    var ITEM = ItemType_comboBox.SelectedValue;
                    var QTY = Convert.ToInt32(ItemQty_textBox.Text);
                    var VALUE = QTY * 2;
                    m.WriteMemory(SLOT_ADDRESS.ToString(), "Byte", ITEM.ToString());
                    m.WriteMemory(QTY_ADDRESS.ToString(), "Byte", VALUE.ToString("X"));
                }

                if (SLOT_ADDRESS == gData.ITEMS.SLOT3)
                {
                    var QTY_ADDRESS = gData.ITEMS.SLOT3_QTY;
                    var ITEM = ItemType_comboBox.SelectedValue;
                    var QTY = Convert.ToInt32(ItemQty_textBox.Text);
                    var VALUE = QTY * 2;
                    m.WriteMemory(SLOT_ADDRESS.ToString(), "Byte", ITEM.ToString());
                    m.WriteMemory(QTY_ADDRESS.ToString(), "Byte", VALUE.ToString("X"));
                }

                if (SLOT_ADDRESS == gData.ITEMS.SLOT4)
                {
                    var QTY_ADDRESS = gData.ITEMS.SLOT4_QTY;
                    var ITEM = ItemType_comboBox.SelectedValue;
                    var QTY = Convert.ToInt32(ItemQty_textBox.Text);
                    var VALUE = QTY * 2;
                    m.WriteMemory(SLOT_ADDRESS.ToString(), "Byte", ITEM.ToString());
                    m.WriteMemory(QTY_ADDRESS.ToString(), "Byte", VALUE.ToString("X"));
                }

                if (SLOT_ADDRESS == gData.ITEMS.SLOT5)
                {
                    var QTY_ADDRESS = gData.ITEMS.SLOT5_QTY;
                    var ITEM = ItemType_comboBox.SelectedValue;
                    var QTY = Convert.ToInt32(ItemQty_textBox.Text);
                    var VALUE = QTY * 2;
                    m.WriteMemory(SLOT_ADDRESS.ToString(), "Byte", ITEM.ToString());
                    m.WriteMemory(QTY_ADDRESS.ToString(), "Byte", VALUE.ToString("X"));
                }

                if (SLOT_ADDRESS == gData.ITEMS.SLOT6)
                {
                    var QTY_ADDRESS = gData.ITEMS.SLOT6_QTY;
                    var ITEM = ItemType_comboBox.SelectedValue;
                    var QTY = Convert.ToInt32(ItemQty_textBox.Text);
                    var VALUE = QTY * 2;
                    m.WriteMemory(SLOT_ADDRESS.ToString(), "Byte", ITEM.ToString());
                    m.WriteMemory(QTY_ADDRESS.ToString(), "Byte", VALUE.ToString("X"));
                }

                if (SLOT_ADDRESS == gData.ITEMS.SLOT7)
                {
                    var QTY_ADDRESS = gData.ITEMS.SLOT7_QTY;
                    var ITEM = ItemType_comboBox.SelectedValue;
                    var QTY = Convert.ToInt32(ItemQty_textBox.Text);
                    var VALUE = QTY * 2;
                    m.WriteMemory(SLOT_ADDRESS.ToString(), "Byte", ITEM.ToString());
                    m.WriteMemory(QTY_ADDRESS.ToString(), "Byte", VALUE.ToString("X"));
                }

                if (SLOT_ADDRESS == gData.ITEMS.SLOT8)
                {
                    var QTY_ADDRESS = gData.ITEMS.SLOT8_QTY;
                    var ITEM = ItemType_comboBox.SelectedValue;
                    var QTY = Convert.ToInt32(ItemQty_textBox.Text);
                    var VALUE = QTY * 2;
                    m.WriteMemory(SLOT_ADDRESS.ToString(), "Byte", ITEM.ToString());
                    m.WriteMemory(QTY_ADDRESS.ToString(), "Byte", VALUE.ToString("X"));
                }

                if (SLOT_ADDRESS == gData.ITEMS.SLOT9)
                {
                    var QTY_ADDRESS = gData.ITEMS.SLOT9_QTY;
                    var ITEM = ItemType_comboBox.SelectedValue;
                    var QTY = Convert.ToInt32(ItemQty_textBox.Text);
                    var VALUE = QTY * 2;
                    m.WriteMemory(SLOT_ADDRESS.ToString(), "Byte", ITEM.ToString());
                    m.WriteMemory(QTY_ADDRESS.ToString(), "Byte", VALUE.ToString("X"));
                }

                if (SLOT_ADDRESS == gData.ITEMS.SLOT10)
                {
                    var QTY_ADDRESS = gData.ITEMS.SLOT10_QTY;
                    var ITEM = ItemType_comboBox.SelectedValue;
                    var QTY = Convert.ToInt32(ItemQty_textBox.Text);
                    var VALUE = QTY * 2;
                    m.WriteMemory(SLOT_ADDRESS.ToString(), "Byte", ITEM.ToString());
                    m.WriteMemory(QTY_ADDRESS.ToString(), "Byte", VALUE.ToString("X"));
                }
            }
        }
        #endregion

        //This is still empty
        #region MATERIA

        private void SendMateria_button_Click(object sender, EventArgs e)
        {

        }

        #endregion

        //Nothing done with this quite yet
        #region CHARACTERS

        #endregion

        //Missing Character 3 info
        #region BATTLE SCREEN
        bool Character1HP = false;
        bool Character1MP = false;
        bool Character1LIMIT = false;
        bool Character1TIMER = false;

        bool Character2HP = false;
        bool Character2MP = false;
        bool Character2LIMIT = false;
        bool Character2TIMER = false;

        bool Character3HP = false;
        bool Character3MP = false;
        bool Character3LIMIT = false;
        bool Character3TIMER = false;
        
        private void BATTLESCREEN()
        {
            Character1();
            Character2();
            //Character3();
        }

        private void Character1()
        {
            if (!gameRunning)
            {
                return;
            }
            c1BATTLE_HP();
            c1BATTLE_MP();
            c1LIMIT();
            c1TIMER();
        }

        private void Character2()
        {
            c2BATTLE_HP();
            c2BATTLE_MP();
            c2LIMIT();
            c2TIMER();
        }

        /// Not ready yet
        private void Character3()
        {
            //c3BATTLE_HP();
            //c3BATTLE_MP();
            //c3LIMIT();
            //c3TIMER();
        }

        #region CHARACTER 1 FUNCTIONS
        private void c1BATTLE_HP()
        {
            if (!gameRunning)
            {
                return;
            }

            if (Battle1HP_checkBox.Checked)
            {
                //This is where things get kind of complicated
                //We need to establish what our default values are
                //For this we can create a dictionary and store data with a "key"

                //The very first thing we must do
                //Since this is a checkbox we need to check if we have already frozen our values and stored the data to a dictionary.
                if (!Character1HP)
                {
                    //Start off by creating variables for the data we want stored for later
                    var CurrentHP = m.ReadInt(gData.BATTLESCREEN.Battle1HP);
                    var MaxHP = m.ReadInt(gData.BATTLESCREEN.c1MAX_HP);

                    //Now we can add this data
                    //DictionaryName.Add("Key", variable)
                    c1Data.Add("cHP", CurrentHP.ToString());
                    c1Data.Add("mHP", MaxHP.ToString());

                    //Set and Freeze Value
                    m.FreezeValue(gData.BATTLESCREEN.Battle1HP, "int", MaxHP.ToString());
                    m.FreezeValue(gData.BATTLESCREEN.c1MAX_HP, "int", MaxHP.ToString());

                    //Lets create a boolean so we know this is active ...
                    //we can later use this to unfreeze the value and return to default
                    Character1HP = true;
                }
            }
            else if (Character1HP)
            {
                //Grab our stored data from the dictionary
                var CurrentHP = c1Data["cHP"];
                var MaxHP = c1Data["mHP"];

                //Now we want to unfreeze the frozen values
                m.UnfreezeValue(gData.BATTLESCREEN.Battle1HP);
                m.UnfreezeValue(gData.BATTLESCREEN.c1MAX_HP);

                //Now we want to restore default data
                m.WriteMemory(gData.BATTLESCREEN.Battle1HP, "int", CurrentHP);
                m.WriteMemory(gData.BATTLESCREEN.c1MAX_HP, "int", MaxHP);

                //Finally we can remove the stored keys and set our boolean to false
                c1Data.Remove("cHP");
                c1Data.Remove("mHP");
                Character1HP = false;
            }
        }

        private void c1BATTLE_MP()
        {
            if (!gameRunning)
            {
                return;
            }

            if (Battle1MP_checkBox.Checked)
            {
                if (!Character1MP)
                {
                    var CurrentMP = m.Read2Byte(gData.BATTLESCREEN.Battle1MP);
                    c1Data.Add("cMP", CurrentMP.ToString());
                    m.FreezeValue(gData.BATTLESCREEN.Battle1MP, "2bytes", CurrentMP.ToString());
                    Character1MP = true;
                }
            }
            else if (Character1MP)
            {
                var CurrentMP = c1Data["cMP"];
                m.UnfreezeValue(gData.BATTLESCREEN.Battle1MP);
                m.WriteMemory(gData.BATTLESCREEN.Battle1MP, "2bytes", CurrentMP);
                c1Data.Remove("cMP");
                Character1MP = false;
            }
        }

        private void c1LIMIT()
        {
            if (!gameRunning)
            {
                return;
            }

            if (Battle1Limit_checkBox.Checked)
            {
                if (!Character1LIMIT)
                {
                    var CurrentLIMIT = m.Read2Byte(gData.BATTLESCREEN.Battle1LIMIT);
                    c1Data.Add("cLIMIT", CurrentLIMIT.ToString());
                    m.FreezeValue(gData.BATTLESCREEN.Battle1LIMIT, "2bytes", "255");
                    Character1LIMIT = true;
                }
            }
            else if (Character1LIMIT)
            {
                var value = c1Data["cLIMIT"];
                m.UnfreezeValue(gData.BATTLESCREEN.Battle1LIMIT);
                m.WriteMemory(gData.BATTLESCREEN.Battle1LIMIT, "2bytes", value);
                c1Data.Remove("cLIMIT");
                Character1LIMIT = false;
            }
        }

        private void c1TIMER()
        {
            if (!gameRunning)
            {
                return;
            }

            if (Battle1Time_checkBox.Checked)
            {
                if (!Character1TIMER)
                {
                    var CurrentTime = m.ReadByte(gData.BATTLESCREEN.Battle1AtkTimer);
                    c1Data.Add("cTIMER", CurrentTime.ToString());
                    m.FreezeValue(gData.BATTLESCREEN.Battle1AtkTimer, "2bytes", "255");
                    Character1TIMER = true;
                }
            }
            else if (Character1TIMER)
            {
                var value = c1Data["cTIMER"];
                m.UnfreezeValue(gData.BATTLESCREEN.Battle1AtkTimer);
                m.WriteMemory(gData.BATTLESCREEN.Battle1AtkTimer, "2bytes", value);
                c1Data.Remove("cTIMER");
                Character1TIMER = false;
            }
        }
        #endregion

        #region CHARACTER 2 FUNCTIONS
     
        private void c2BATTLE_HP()
        {
            if (!gameRunning)
            {
                return;
            }

            if (Battle2HP_checkBox.Checked)
            {
                if (!Character2HP)
                {
                    var CurrentHP = m.ReadInt(gData.BATTLESCREEN.Battle2HP);
                    var MaxHP = m.ReadInt(gData.BATTLESCREEN.c2MAX_HP);
                    c2Data.Add("cHP", CurrentHP.ToString());
                    c2Data.Add("mHP", MaxHP.ToString());
                    m.FreezeValue(gData.BATTLESCREEN.Battle2HP, "int", MaxHP.ToString());
                    m.FreezeValue(gData.BATTLESCREEN.c2MAX_HP, "int", MaxHP.ToString());
                    Character2HP = true;
                }
            }
            else if (Character2HP)
            {
                var CurrentHP = c2Data["cHP"];
                var MaxHP = c2Data["mHP"];
                m.UnfreezeValue(gData.BATTLESCREEN.Battle2HP);
                m.UnfreezeValue(gData.BATTLESCREEN.c2MAX_HP);
                m.WriteMemory(gData.BATTLESCREEN.Battle2HP, "int", CurrentHP);
                m.WriteMemory(gData.BATTLESCREEN.c2MAX_HP, "int", MaxHP);
                c2Data.Remove("cHP");
                c2Data.Remove("mHP");
                Character2HP = false;
            }
        }
      
        private void c2BATTLE_MP()
        {
            if (!gameRunning)
            {
                return;
            }

            if (Battle2MP_checkBox.Checked)
            {
                if (!Character2MP)
                {
                    var CurrentMP = m.Read2Byte(gData.BATTLESCREEN.Battle2MP);
                    c2Data.Add("cMP", CurrentMP.ToString());
                    m.FreezeValue(gData.BATTLESCREEN.Battle2MP, "2Bytes", CurrentMP.ToString());
                    Character2MP = true;
                }
            }
            else if (Character2MP)
            {
                var CurrentMP = c2Data["cMP"];
                m.UnfreezeValue(gData.BATTLESCREEN.Battle2MP);
                m.WriteMemory(gData.BATTLESCREEN.Battle2MP, "2Bytes", CurrentMP);
                c2Data.Remove("cMP");
                Character2MP = false;
            }
        }
     
        private void c2LIMIT()
        {
            if (!gameRunning)
            {
                return;
            }

            if (Battle2LIMIT_checkBox.Checked)
            {
                if (!Character2LIMIT)
                {
                    var CurrentLIMIT = m.Read2Byte(gData.BATTLESCREEN.Battle2LIMIT);
                    c2Data.Add("cLIMIT", CurrentLIMIT.ToString());
                    m.FreezeValue(gData.BATTLESCREEN.Battle2LIMIT, "2bytes", "255");
                    Character2LIMIT = true;
                }
            }
            else if (Character2LIMIT)
            {
                var value = c2Data["cLIMIT"];
                m.UnfreezeValue(gData.BATTLESCREEN.Battle2LIMIT);
                m.WriteMemory(gData.BATTLESCREEN.Battle2LIMIT, "2bytes", value);
                c2Data.Remove("cLIMIT");
                Character2LIMIT = false;
            }
        }
     
        private void c2TIMER()
        {
            if (!gameRunning)
            {
                return;
            }

            if (Battle2TIMER_checkBox.Checked)
            {
                if (!Character2TIMER)
                {
                    var CurrentTime = m.ReadByte(gData.BATTLESCREEN.Battle2AtkTimer);
                    c2Data.Add("cTIMER", CurrentTime.ToString());
                    m.FreezeValue(gData.BATTLESCREEN.Battle2AtkTimer, "2bytes", "255");
                    Character2TIMER = true;
                }
            }
            else if (Character2TIMER)
            {
                var value = c2Data["cTIMER"];
                m.UnfreezeValue(gData.BATTLESCREEN.Battle2AtkTimer);
                m.WriteMemory(gData.BATTLESCREEN.Battle2AtkTimer, "2bytes", value);
                c2Data.Remove("cTIMER");
                Character2TIMER = false;
            }
        }
        #endregion

        #region CHARACTER 3 FUNCTIONS
      
        private void c3BATTLE_HP()
        {

        }
      
        private void c3BATTLE_MP()
        {

        }
      
        private void c3LIMIT()
        {

        }
       
        private void c3TIMER()
        {

        }

        #endregion

        #endregion

        #region AWARD SCREEN
        bool AwardXP = false;
        bool AwardAP = false;
        bool AwardGIL = false;

        private void AWARDSCREEN()
        {
            FreezeXP();
            FreezeAP();
            FreezeGIL();
        }

        private void FreezeXP()
        {
            if (!gameRunning)
            {
                return;
            }

            //Determine if the checkbox is checked
            if (FreezeXP_checkBox.Checked)
            {
                // determine if we have already frozen the value
                // Also make sure text box is not empty
                if ((!AwardXP) && (AwardXP_textBox.Text != ""))
                {
                    //Define our variable
                    var value = AwardXP_textBox.Text;

                    //Freeze the value
                    m.FreezeValue(gData.AWARDSCREEN.XP, "int", value.ToString());
                    
                    //Trigger our boolean so we don't continue this thread
                    AwardXP = true;
                }
            }
            //If Checkbox is not checked AND we have activated our cheat
            // Basically this is what will happen when we uncheck the box , but we only want this event to transpire when we are done freezing our memory
            // is true , then continue
            else if (AwardXP)
            {
                //Unfreeze the value
                m.UnfreezeValue(gData.AWARDSCREEN.XP);
                
                //Trigger our boolean to be false
                AwardXP = false;
            }
        }

        private void FreezeAP()
        {
            if (!gameRunning)
            {
                return;
            }

            //Determine if the checkbox is checked
            if (FreezeAP_checkBox.Checked)
            {
                // determine if we have already frozen the value
                // Also make sure text box is not empty
                if ((!AwardAP) && (AwardAP_textBox.Text != ""))
                {
                    //Define our variable
                    var value = AwardAP_textBox.Text;

                    //Freeze the value
                    m.FreezeValue(gData.AWARDSCREEN.AP, "int", value.ToString());

                    //Trigger our boolean so we don't continue this thread
                    AwardAP = true;
                }
            }
            //If Checkbox is not checked AND we have activated our cheat
            // Basically this is what will happen when we uncheck the box , but we only want this event to transpire when we are done freezing our memory
            // is true , then continue
            else if (AwardAP)
            {
                //Unfreeze the value
                m.UnfreezeValue(gData.AWARDSCREEN.AP);

                //Trigger our boolean to be false
                AwardAP = false;
            }
        }

        private void FreezeGIL()
        {
            if (!gameRunning)
            {
                return;
            }

            //Determine if the checkbox is checked
            if (FreezeGIL_checkBox.Checked)
            {
                // determine if we have already frozen the value
                // Also make sure text box is not empty
                if ((!AwardGIL) && (AwardGIL_textBox.Text != ""))
                {
                    //Define our variable
                    var value = AwardGIL_textBox.Text;

                    //Freeze the value
                    m.FreezeValue(gData.AWARDSCREEN.GIL, "int", value.ToString());
                    
                    //Trigger our boolean so we dont repeat this again and again
                    AwardGIL = true;
                }
            }
            //If Checkbox is not checked AND we have activated our cheat
            // Basically this is what will happen when we uncheck the box , but we only want this event to transpire when we are done freezing our memory
            // is true , then continue
            else if (AwardGIL)
            {
                m.UnfreezeValue(gData.AWARDSCREEN.GIL);

                //Trigger our boolean to be false
                AwardGIL = false;
            }
        }

        private void AwardGIL_button_Click(object sender, EventArgs e)
        {
            var GIL = AwardGIL_textBox.Text;

            if (AwardGIL_textBox.Text != "")
            {
                m.WriteMemory(gData.AWARDSCREEN.GIL, "int", GIL);
            }
        }

        #endregion

    }
}
