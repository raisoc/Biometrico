using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace bo.gp.DB.Models
{
    [Table("FingerprintEvents")]
    public class FingerprintEvent
    {
        /// <summary>
        /// El ID del registro (autonumérico)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID del usuario (usualmente el carnet de identidad)
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// El nombre de la computadora donde se realizo el marcado.
        /// </summary>
        public string ComputerName { get; set; }

        /// <summary>
        /// El MAC address de la computadora donde se realizo el marcado.
        /// </summary>
        public string PhysicalAddress { get; set; }

        /// <summary>
        /// El IP del sensor (normalmente el IP del API de marcado.
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Fecha en que se creó el registro en el equipo donde se realizó el marcado En UTC.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Fecha en la que se realizó la consolidación a la tabla del Sistema de Control de Personal En UTC.
        /// </summary>
        public DateTime? ConsolidatedOn { get; set; }

        /// <summary>
        /// Indica si el resitro ha sido consolidado.
        /// </summary>
        [Column("Consolidated", TypeName = "bit")]
        public bool Consolidated { get; set; }
        public override string ToString()
        {
         
         return $"{string.Format("{0,-20}", ComputerName)}|{string.Format("{0,-20}", PhysicalAddress)}|{string.Format("{0,-15}", IP)}|{string.Format("{0,-10}", UserId.ToString())}|{CreatedOn}|";

        }
    }
}
