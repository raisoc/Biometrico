using System.ComponentModel.DataAnnotations.Schema;

namespace bo.gp.DB.Models
{
	[Table("Dispositivo")]
	public class Dispositivo
	{
		public string DireccionIP { get; set; }
		public string Puerto { get; set; }
		public string Cod_Estado { get; set; }
		public string Tipo { get; set; }
		public string TipoId { get; set; }
		public string Comentario { get; set; }
		public string Param { get; set; }

		public override string ToString()
		{
			return this.DireccionIP +" | " + this.Puerto + " | " + this.Comentario + " | " +Cod_Estado;
		}

	}
}
