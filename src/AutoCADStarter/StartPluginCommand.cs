using Autodesk.AutoCAD.Runtime;

[assembly: CommandClass(typeof(AutoCADStarter.StartPluginCommand))]

namespace AutoCADStarter
{
    public class StartPluginCommand
    {
        [CommandMethod("StartRocketModelPlugin", CommandFlags.Modal)]
        public void StartCommand()
        {
            // TODO: Вызов UI.
        }
    }

}
