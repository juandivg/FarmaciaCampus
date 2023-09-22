using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Paciente:BaseEntity
    {
        public string NombrePaciente { get; set; }
        public string cedula { get; set; } 
        public string Correo { get; set; }   
        public int IdDireccionPac { get; set; }    
        public DireccionPaciente DireccionPaciente { get; set; }
        public ICollection<Venta> Ventas {get; set;}
        public ICollection<TelefonoPaciente> TelefonoPacientes { get; set; }
        public ICollection<Receta> Recetas { get; set; }
        
    }
