using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

[assembly: CommandClass(typeof(AutoCADStarter.StartPluginCommand))]

namespace AutoCADStarter
{
    using RocketPlugin.UI;

    /// <summary>
    /// Класс, отвечающий за запуск плагнина из AutoCAD.
    /// </summary>
    public class StartPluginCommand
    {
        /// <summary>
        /// Команда для запуска плагина.
        /// </summary>
        [CommandMethod("StartRocketModelPlugin", CommandFlags.Modal)]
        public void StartCommand()
        {
            Application.ShowModalDialog(new MainForm());
        }
    }

}
