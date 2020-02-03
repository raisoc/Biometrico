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
            var keyDB = "DB";
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string valor = null;
            string valorDB = null;
            if (config.AppSettings.Settings[key] != null)
                valor = config.AppSettings.Settings[key].Value;
            if (config.AppSettings.Settings[keyDB] != null)
                valorDB = config.AppSettings.Settings[keyDB].Value;
            if (valor == null)
            {
                config.AppSettings.Settings.Add(key, "31/01/2020 00:00:00");
                valor = config.AppSettings.Settings[key].Value;
            }
            if (valorDB == null)
            {
                config.AppSettings.Settings.Add(keyDB, "2");
                valorDB = config.AppSettings.Settings[keyDB].Value;
            }
            var fechaCorte = DateTime.Parse(valor);
            Console.WriteLine("Fecha Corte:" + fechaCorte + "\n");
            config.AppSettings.Settings[key].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            config.Save(ConfigurationSaveMode.Modified);
            switch (valorDB)
            {
                case "1":
                    {
                        /*
                         * Recoleta y Almacena al sistema DB Mysql
                         */
                        Manejador.RecoletarAsistencias(fechaCorte); 
                    }
                    break;
                case "2":
                    {
                        /*
                         * Recoleta tomando configuraion Dispositivo de SQL-Server y Almacena al sistema DB Mysql y SQL-Server
                         */
                        Manejador.RecoletarAsistenciasDB2(fechaCorte);
                    }
                    break;
                case "3":
                    {
                        /*
                       * Recoleta tomando configuraion Dispositivo de MySQL y Almacena al sistema DB Mysql y SQL-Server
                       */
                        Manejador.RecoletarAsistenciasDB3(fechaCorte);
                    }
                    break;
                default:
                    {
                        Manejador.RecoletarAsistencias(fechaCorte);
                    }
                    break;
            }
            Console.WriteLine("Final\n");
        }
    }
}
