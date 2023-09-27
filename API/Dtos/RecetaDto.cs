using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RecetaDto
    {
        public int Id { get; set; }
        public DateTime FechaReceta { get; set; }
        public string Detalle { get; set; }
    }
}