using System;
using System.Collections.Generic;
using System.Text;

namespace Macadores.Models
{
   public  class Machine
    {
        public int MachineNumber { get; set; }
        public int IndRegID { get; set; }
        public string DateTimeRecord { get; set; }
       

        public DateTime DateOnlyRecord
        {
            get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("yyyy-MM-dd")); }
        }
        public DateTime TimeOnlyRecord
        {
            get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("hh:mm:ss tt")); }
        }
        public DateTime TimeEvento
        {
            get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("dd/MM-yyyy hh:mm:ss tt")); }
        }
        public Int64 LongTimeOnlyRecord
        {
            get {

                var data = DateTime.Now;
                var dataUTC = DateTime.UtcNow; ; ;
                var difmin = (data - dataUTC).TotalMinutes;
                var reg = DateTime.Parse(this.DateTimeRecord).AddHours(4);
                var fechaStar = new DateTime(1970, 1, 1);
                var fecr = reg.AddMinutes(difmin);
                var diferencia = (fecr - fechaStar).TotalSeconds;

                return  (Int64)diferencia;
                }
        }
        public override string ToString()
        {
            return  this.MachineNumber + " | " + this.IndRegID + " | " + this.DateTimeRecord + " | " +  this.LongTimeOnlyRecord;
        }
    }
}
