using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos;
    public class CompraxProductosDto
    {
        public int Id { get; set; }
        public  DateTime Fecha { get; set; }
        public List<ProductoDto> Productos { get; set; }
    }
