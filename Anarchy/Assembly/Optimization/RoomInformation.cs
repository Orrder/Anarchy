﻿using Anarchy;
using System.Text;

namespace Optimization
{
    internal class RoomInformation
    {
        private const float UpdateInterval = 1f;

        private float updateTimer = UpdateInterval;
        private readonly Anarchy.Localization.Locale lang;

        public RoomInformation()
        {
            lang = new Anarchy.Localization.Locale(nameof(RoomInformation), true, ',');
            lang.Load();    
        }

        public void Update()
        {
            updateTimer -= UnityEngine.Time.unscaledDeltaTime;
            if(updateTimer <= 0f)
            {
                UpdateLabels();
                updateTimer = UpdateInterval;
            }
        }

        public void UpdateLabels()
        {
            var bld = new StringBuilder();
            bld.AppendLine(lang.Format("time", (IN_GAME_MAIN_CAMERA.GameType == GameType.Single ? (FengGameManagerMKII.FGM.Logic.RoundTime).ToString("F0") : (FengGameManagerMKII.FGM.Logic.ServerTime).ToString("F0"))));

            if (IN_GAME_MAIN_CAMERA.GameMode != GameMode.RACING)
            {
                bld.AppendLine(lang.Format("score", FengGameManagerMKII.FGM.Logic.HumanScore.ToString(), FengGameManagerMKII.FGM.Logic.TitanScore.ToString()));
                string difficulty = (IN_GAME_MAIN_CAMERA.Difficulty >= 0) ? ((IN_GAME_MAIN_CAMERA.Difficulty != 0) ? ((IN_GAME_MAIN_CAMERA.Difficulty != 1) ? lang["abnormal"] : lang["hard"]) : lang["normal"]) : lang["training"];
                bld.Append(FengGameManagerMKII.Level.Name);
                bld.Append(": ");
                bld.AppendLine(difficulty);
            }

            bld.AppendLine(lang.Format("cameraChange", InputManager.Settings[InputCode.CameraChange].ToString(), IN_GAME_MAIN_CAMERA.CameraMode.ToString()));
            if (PhotonNetwork.inRoom)
            {
                bld.AppendLine(lang.Format("room", PhotonNetwork.room.UIName.Split('/')[0].ToHTMLFormat()));
                bld.AppendLine(lang.Format("slots", PhotonNetwork.room.playerCount.ToString(), PhotonNetwork.room.maxPlayers.ToString().ToString()));
            }
            bld.AppendLine(lang.Format("fps", FengGameManagerMKII.FPS.FPS.ToString()));
            Labels.TopRight = "<color=#" + User.MainColor.Value + ">" + bld.ToString() + "</color>";
        }
    }
}
