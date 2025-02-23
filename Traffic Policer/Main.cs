using System.IO;
using System.Reflection;

namespace Traffic_Policer
{
    internal class Main : Plugin
    {
        public Main()
        {
            Game.LogTrivial("Creating Traffic Policer.Main.");
            Functions.OnOnDutyStateChanged += Functions_OnOnDutyStateChanged;

            // Ensure dependency checks are up to date.
            Albo1125.Common.UpdateChecker.VerifyXmlNodeExists(PluginName, FileID, DownloadURL, Path);
            Albo1125.Common.DependencyChecker.RegisterPluginForDependencyChecks(PluginName);
            Game.LogTrivial("Done with Traffic Policer.Main.");
        }

        public override void Finally()
        {
            foreach (Vehicle veh in TrafficStopAssist.PlayerVehicles.ToArray())
            {
                if (veh.Exists())
                {
                    veh.LockStatus = VehicleLockStatus.Unlocked;
                }
            }
        }

        public override void Initialize()
        {
            Game.LogTrivial($"Traffic Policer {Assembly.GetExecutingAssembly().GetName().Version} developed by Albo1125 has been initialised.");
            Game.LogTrivial("Go on duty to start Traffic Policer - Traffic Policer.Initialise done.");
        }

        static void Functions_OnOnDutyStateChanged(bool onDuty)
        {
            Game.LogTrivial($"In traffic policer duty event handler: {onDuty}");
            if (onDuty)
            {
                Albo1125.Common.UpdateChecker.InitialiseUpdateCheckingProcess();
                if (Albo1125.Common.DependencyChecker.DependencyCheckMain(PluginName, Albo1125CommonVer, MinimumRPHVersion, MadeForGTAVersion, MadeForLSPDFRVersion, RAGENativeUIVersion, AudioFilesToCheckFor))
                {                    
                    Albo1125.Common.DependencyChecker.CheckIfThereAreNoConflictingFiles("Traffic Policer", ConflictingFiles);
                    TrafficPolicerHandler.Initialise();                   
                }              
            }
        }
    }
}
