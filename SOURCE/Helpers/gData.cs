namespace gData
{
    class GENERAL
    {
        public const string GIL_ADDRESS = "ePSXe.exe+B1F280";               //4Bytes
        public const string GAME_TIME_ADDRESS = "ePSXe.exe+B1F284";         //4Bytes
        public const string SAVE_ANYWHERE_ADDRESS = "ePSXe.exe+B1F2C7";     //Byte
        public const string START_MENU_ADDRESS = "ePSXe.exe+B1F2C4";        //Byte
    }

    class ITEMS
    {
        public const string SLOT1 = "ePSXe.exe+B1EC00";
        public const string SLOT1_QTY = "ePSXe.exe+B1EC01";

        public const string SLOT2 = "ePSXe.exe+B1EC02";
        public const string SLOT2_QTY = "ePSXe.exe+B1EC03";
        
        public const string SLOT3 = "ePSXe.exe+B1EC04";
        public const string SLOT3_QTY = "ePSXe.exe+B1EC05";
        
        public const string SLOT4 = "ePSXe.exe+B1EC06";
        public const string SLOT4_QTY = "ePSXe.exe+B1EC07";
        
        public const string SLOT5 = "ePSXe.exe+B1EC08";
        public const string SLOT5_QTY = "ePSXe.exe+B1EC09";
        
        public const string SLOT6 = "ePSXe.exe+B1EC0A";
        public const string SLOT6_QTY = "ePSXe.exe+B1EC0B";
        
        public const string SLOT7 = "ePSXe.exe+B1EC0C";
        public const string SLOT7_QTY = "ePSXe.exe+B1EC0D";
        
        public const string SLOT8 = "ePSXe.exe+B1EC0E";
        public const string SLOT8_QTY = "ePSXe.exe+B1EC0F";
        
        public const string SLOT9 = "ePSXe.exe+B1EC10";
        public const string SLOT9_QTY = "ePSXe.exe+B1EC11";
        
        public const string SLOT10 = "ePSXe.exe+B1EC12";
        public const string SLOT10_QTY = "ePSXe.exe+B1EC13";
    }

    class MATERIA
    {

    }

    class CHARACTERS
    {

    }

    class BATTLESCREEN
    {
        //Character Slot 1
        public const string c1MAX_HP = "ePSXe.exe+B7A430";                  //4Bytes
        public const string Battle1HP = "ePSXe.exe+B7A42C";                 //4Bytes
        public const string Battle1MP = "ePSXe.exe+B7A428";                 //2Bytes
        public const string Battle1LIMIT = "ePSXe.exe+B77E88";              //2Bytes
        public const string Battle1AtkTimer = "ePSXe.exe+B77BDD";           //Byte

        //Character Slot 2
        public const string c2MAX_HP = "ePSXe.exe+B7A498";                  //4Bytes
        public const string Battle2HP = "ePSXe.exe+B7A494";                 //4Bytes
        public const string Battle2MP = "ePSXe.exe+B7A490";                 //4Bytes
        public const string Battle2LIMIT = "ePSXe.exe+B77EBC";              //2Bytes
        public const string Battle2AtkTimer = "ePSXe.exe+B77C21";           //Byte

        //Character Slot 3 (Not Done Yet)
    }

    class AWARDSCREEN
    {
        public const string XP = "ePSXe.exe+B1F7F8";
        public const string AP = "ePSXe.exe+B1F7FC";
        public const string GIL = "ePSXe.exe+B1F800";
    }
}
