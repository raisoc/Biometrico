using bo.gp.Recolector;
using System;
using System.Configuration;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            var key = "FechaCorte";
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string valor= null;
            if (config.AppSettings.Settings[key] != null)
                valor = config.AppSettings.Settings[key].Value;
            if (valor == null) { 
                config.AppSettings.Settings.Add(key, "31/01/2020 00:00:00");
                valor = config.AppSettings.Settings[key].Value;
            }
            var fechaCorte = DateTime.Parse(valor);
            Console.WriteLine("Fecha Corte:" + fechaCorte + "\n");
            config.AppSettings.Settings[key].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            config.Save(ConfigurationSaveMode.Modified);
            Manejador.RecoletarAsistencias(fechaCorte);
            Console.WriteLine( "Final\n");

        }
    }
}
