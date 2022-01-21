using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

[assembly: CommandClass(typeof(AutoCADConnector.AutoCADConnector))]

namespace AutoCADConnector
{
    using RocketPlugin.BL;
    using RocketPlugin.Builder;
    using RocketPlugin.UI;

    using Microsoft.VisualBasic.Devices;

    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Класс, отвечающий за запуск плагина из AutoCAD.
    /// </summary>
    public class AutoCADConnector
    {
        /// <summary>
        /// Команда для запуска плагина.
        /// </summary>
        [CommandMethod("StartRocketModelPlugin", CommandFlags.Modal)]
        public void StartCommand()
        {
            Application.ShowModelessDialog(new MainForm());
        }

        /// <summary>
        /// Команда для запуска нагрузочного тестированя 
        /// построения модели с параметрами по умолчанию.
        /// </summary>
        [CommandMethod("StressTest", CommandFlags.Session)]
        public void StressTest()
        {
            var gearParameters = new RocketParameters();
            var builder = new RocketBuilder(gearParameters);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var streamWriter = new StreamWriter($"log.txt", true);
            var currentProcess = Process.GetCurrentProcess();
            var count = 0;

            while (count < 60000)
            {
                builder.Build();
                var computerInfo = new ComputerInfo();
                var usedMemory = (computerInfo.TotalPhysicalMemory - computerInfo.AvailablePhysicalMemory) *
                                 0.000000000931322574615478515625;
                streamWriter.WriteLine(
                    $"{++count}\t{stopWatch.ElapsedMilliseconds}\t{usedMemory}");
                streamWriter.Flush();
            }
        }
    }
}
