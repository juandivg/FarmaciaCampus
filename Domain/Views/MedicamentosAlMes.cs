using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Views;
public class MedicamentosAlMes
{
    public string Mes { get; set; }
    public ICollection<Producto> Productos { get; set; }
}
