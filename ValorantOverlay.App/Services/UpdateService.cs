using AutoUpdaterDotNET;
using System.Text.Json;
using ValorantOverlay.App.Models;

namespace ValorantOverlay.App.Services
{
    public interface IUpdateService
    {
        void Start();
    }

    public class UpdateService : IUpdateService
    {
        const string UPDATE_URL = "https://dl.dropboxusercontent.com/s/2h50ctzn973cx6r/updator.json?dl=0";

        public UpdateService()
        {
            AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
        }

        public void Start() => AutoUpdater.Start(UPDATE_URL);

        private void AutoUpdaterOnParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
        {
            var updateInfo = JsonSerializer.Deserialize<UpdateInfo>(args.RemoteData);
            args.UpdateInfo = new UpdateInfoEventArgs
            {
                CurrentVersion = updateInfo.Version,
                ChangelogURL = updateInfo.Changelog,
                DownloadURL = updateInfo.Url,
                Mandatory = new Mandatory
                {
                    Value = updateInfo.Mandatory?.Value ?? false,
                    UpdateMode = (Mode)(updateInfo.Mandatory?.UpdateMode ?? (int)Mode.Normal),
                    MinimumVersion = updateInfo.Mandatory?.MinimumVersion
                }
            };

        }
    }
}
