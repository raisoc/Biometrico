using bo.gp.DB.Models;
using Macadores.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bo.gp.Recolector
{
    public class DispositivoInf
    {
        public string  NameDispositivo { get; set; }
        public string MAC { get; set; }
        public string IP { get; set; }
        public ICollection<Machine> machinesInf { get; set; }
    }
}
