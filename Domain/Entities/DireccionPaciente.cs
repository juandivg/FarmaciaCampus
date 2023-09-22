using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class DireccionPaciente:BaseEntity
    {
        public int Calle { get; set; }
        public int Carrera { get; set; }
        public string Detalles { get; set; }
        public int IdBarriofk { get; set; }
        public Barrio Barrio { get; set; }
        public ICollection<Paciente> pacientes {get;set;}
    }
