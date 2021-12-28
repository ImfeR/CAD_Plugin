﻿using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

[assembly: CommandClass(typeof(AutoCADConnector.StartPluginCommand))]

namespace AutoCADConnector
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
            Application.ShowModelessDialog(new MainForm());
        }
    }

}