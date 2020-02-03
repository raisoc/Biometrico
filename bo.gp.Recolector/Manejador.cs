using bo.gp.DB;
using bo.gp.DB.Contexts;
using bo.gp.DB.Models;
using Macadores.Models;
using Macadores.ZK;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace bo.gp.Recolector
{
   public static class  Manejador
    {
        public static void RecoletarAsistencias(DateTime fechacorte)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            var pathOrigen = configuration.GetSection("Ruta").Value;

            List<DispositivoInf> dispositivoInfs = new List<DispositivoInf>();
            var dispositivos = GetDispositivos(Conector.MySql);
            foreach (var dispositivo in dispositivos)
            {
                var dispositivoInf = GetDispositivoInf(dispositivo, fechacorte);
                dispositivoInfs.Add(dispositivoInf);
            }
            var isregistroTxt = RegistrarEventosTxt(dispositivoInfs, pathOrigen);
            if (isregistroTxt)
            {
                var lstMySqlNoregistro = RegistrarEventoDB(dispositivoInfs, Conector.MySql);
                if (lstMySqlNoregistro.Count > 0)
                    RegistrarEventosTxtNoDB(lstMySqlNoregistro, pathOrigen, Conector.MySql);
            }
            else
            {
                Console.WriteLine("No registro Text");
            }
        }
        public static void RecoletarAsistenciasDB2(DateTime fechacorte)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            var pathOrigen = configuration.GetSection("Ruta").Value;

            List<DispositivoInf> dispositivoInfs = new List<DispositivoInf>();
            var dispositivos = GetDispositivos(Conector.SqlServer);
            foreach (var dispositivo in dispositivos)
            {
                var dispositivoInf = GetDispositivoInf(dispositivo, fechacorte);
                dispositivoInfs.Add(dispositivoInf);
            }
            var isregistroTxt = RegistrarEventosTxt(dispositivoInfs, pathOrigen);
            if (isregistroTxt)
            {
                var lstSqlNoregistro = RegistrarEventoDB(dispositivoInfs, Conector.SqlServer);
                var lstMySqlNoregistro = RegistrarEventoDB(dispositivoInfs, Conector.MySql);
                if (lstSqlNoregistro.Count > 0)
                    RegistrarEventosTxtNoDB(lstSqlNoregistro, pathOrigen, Conector.SqlServer);
                if (lstMySqlNoregistro.Count > 0)
                    RegistrarEventosTxtNoDB(lstMySqlNoregistro, pathOrigen, Conector.MySql);
            }
            else
            {
                Console.WriteLine("No registro Text");
            }
        }
        public static void RecoletarAsistenciasDB3(DateTime fechacorte)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            var pathOrigen = configuration.GetSection("Ruta").Value;

            List<DispositivoInf> dispositivoInfs = new List<DispositivoInf>();
            var dispositivos = GetDispositivos(Conector.MySql);
            foreach (var dispositivo in dispositivos)
            {
                var dispositivoInf = GetDispositivoInf(dispositivo, fechacorte);
                dispositivoInfs.Add(dispositivoInf);
            }
            var isregistroTxt = RegistrarEventosTxt(dispositivoInfs, pathOrigen);
            if (isregistroTxt)
            {
                var lstSqlNoregistro = RegistrarEventoDB(dispositivoInfs, Conector.SqlServer);
                var lstMySqlNoregistro = RegistrarEventoDB(dispositivoInfs, Conector.MySql);
                if (lstSqlNoregistro.Count > 0)
                    RegistrarEventosTxtNoDB(lstSqlNoregistro, pathOrigen, Conector.SqlServer);
                if (lstMySqlNoregistro.Count > 0)
                    RegistrarEventosTxtNoDB(lstMySqlNoregistro, pathOrigen, Conector.MySql);
            }
            else
            {
                Console.WriteLine("No registro Text");
            }
        }



        //private static ICollection<Machine> GetMachinesInf(Dispositivo dispositivo)
        //{
        //    ZKLib zk = new ZKLib();
        //    var ipDispositivo = dispositivo.DireccionIP.Trim();
        //    var puerto = int.Parse(dispositivo.Puerto.Trim());
        //    Manipulador manipulator = new Manipulador();
        //    var isConetado = zk.Connect_Net(ipDispositivo, puerto);
        //    if (isConetado)
        //    {
        //        ICollection<Machine> lstMachineInfo = manipulator.GetLogData(zk, 1);
        //        zk.Disconnect();
        //        return lstMachineInfo;
        //    }
        //    return new List<Machine>();
        //}

        private static DispositivoInf GetDispositivoInf(Dispositivo dispositivo, DateTime fechacorte)
        {

            var dipositivoInf = new DispositivoInf();
            ZKLib zk = new ZKLib();
            var ipDispositivo = dispositivo.DireccionIP.Trim();
            var puerto = int.Parse(dispositivo.Puerto.Trim());
            Manipulador manipulator = new Manipulador();
            var isConetado = zk.Connect_Net(ipDispositivo, puerto);
            if (isConetado)
            {
                var mac = manipulator.GetDeviceMAC(zk, 1).Replace(":","");
                var ip = manipulator.GetDeviceIP(zk, 1);
                var serial = manipulator.GetSerialNumber(zk, 1);
                ICollection<Machine> lstMachineInfo = manipulator.GetLogData(zk, 1,fechacorte);
                zk.Disconnect();
                return new DispositivoInf()
                {
                    IP = ip,
                    MAC = mac,
                    NameDispositivo = serial,
                    machinesInf = lstMachineInfo,
                };
            }
            return dipositivoInf;
        }

        private static ICollection<Dispositivo> GetDispositivos(Conector conector)
        {
            var lst = new List<Dispositivo>();
            using (var context = new DbContextFactory().CreateDbContext(conector))
            {
                lst = context.Dispositivos.Where(o => o.Cod_Estado.Equals("H")).ToList();
            }
            return lst;
        }
        private static List<FingerprintEvent> RegistrarEventoDB(List<DispositivoInf> dispositivoInfs,Conector conector)
        {
            List<FingerprintEvent> norgistroDB = new List<FingerprintEvent>();
            foreach (var dispositivo in dispositivoInfs)
            {
                foreach (var m in dispositivo.machinesInf)
                {
                    using (var context = new DbContextFactory().CreateDbContext(conector))
                    {
                        
                        FingerprintEvent itemEvent1 = null;
                        try
                        {
                            itemEvent1 = new FingerprintEvent
                            {
                                ComputerName = dispositivo.NameDispositivo,
                                PhysicalAddress = dispositivo.MAC,
                                IP = dispositivo.IP,
                                UserId = m.IndRegID,
                                CreatedOn = m.TimeEvento.AddHours(4),
                                Consolidated = false,
                                ConsolidatedOn = null
                            };
                           context.FingerprintEvents.Add(itemEvent1);
                           context.SaveChanges();
                            int id = itemEvent1.Id;

                        }
                        catch (Exception e)
                        {
                            context.Entry(itemEvent1).Reload();
                            norgistroDB.Add(itemEvent1);

                        }
                        
                    }
                }

            }
            return norgistroDB;
        }
        private static bool RegistrarEventosTxtNoDB(List<FingerprintEvent> norgistroDB, string pathOrigen,Conector conector)
        {
            string path = pathOrigen + @"\NoRegistro_"+conector+"_Hora_" + DateTime.Now.ToString("hh-mm tt") + @".txt";
            try
            {
                using (StreamWriter fs = File.CreateText(path))
                {
                  
                        foreach (var m in norgistroDB)
                        {
                            var itemEvent1 = new FingerprintEvent
                            {
                                ComputerName = m.ComputerName,
                                PhysicalAddress = m.PhysicalAddress,
                                IP = m.IP,
                                UserId = m.UserId,
                                CreatedOn = m.CreatedOn,
                                Consolidated = false,
                                ConsolidatedOn = null
                            };
                            char[] info = new UTF8Encoding(true).GetChars(new UTF8Encoding(true).GetBytes(itemEvent1.ToString()));
                            fs.WriteLine(info, 0, info.Length);

                        }

                    

                }
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        private static bool RegistrarEventosTxt( List<DispositivoInf> dispositivoInfs,string pathOrigen)
        {
            string path = pathOrigen + @"\Hora_" + DateTime.Now.ToString("hh-mm tt") + @".txt";
            try
            {
                using (StreamWriter fs = File.CreateText(path))
                {
                    foreach (var dispositivo in dispositivoInfs)
                    {
                        foreach (var m in dispositivo.machinesInf)
                        {
                            var itemEvent1 = new FingerprintEvent
                            {
                                ComputerName = dispositivo.NameDispositivo,
                                PhysicalAddress = dispositivo.MAC,
                                IP = dispositivo.IP,
                                UserId = m.IndRegID,
                                CreatedOn = m.TimeEvento.AddHours(4),
                                Consolidated = false,
                                ConsolidatedOn = null
                            };
                            BS bS = new BS()
                            {
                                UserID = m.IndRegID,
                                eventTime = m.LongTimeOnlyRecord,
                                IP = dispositivo.IP,
                                Code = m.IndRegID.ToString(),
                            };
                            char[] info = new UTF8Encoding(true).GetChars(new UTF8Encoding(true).GetBytes(bS.ToString()+ itemEvent1.ToString()));
                            fs.WriteLine(info, 0, info.Length);

                        }

                    }
                    
                }
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
      
    }
}
