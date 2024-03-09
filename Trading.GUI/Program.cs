using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Trading.Library;
using Trading.Library.Data;
namespace Trading.GUI
{
    public class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();
            string connectionString = "Data Source=C:\\Users\\44734\\source\\NEA\\Trading-App\\Company Database.db;Mode=ReadWrite;";
            Database db = new Database(connectionString);
            Application.Run(new Menu(db));
        }
    }
}