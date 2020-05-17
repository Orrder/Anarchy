﻿namespace Anarchy.Commands.Chat
{
    internal class ASORacingCommand : ChatCommand
    {
        public ASORacingCommand() : base("asoracing", true, true, false)
        {

        }

        public override bool Execute(string[] args)
        {
            if(FengGameManagerMKII.FGM.logic.Mode != GameMode.RACING)
            {
                chatMessage = Lang["notRacingMode"];
                return false;
            }
            GameModes.AsoRacing.State = !GameModes.AsoRacing.State;
            chatMessage = Lang["asorace" + GameModes.AsoRacing.State.ToString()];
            SendLocalizedText("asorace" + GameModes.AsoRacing.State.ToString(), null);
            return true;
        }
    }
}
