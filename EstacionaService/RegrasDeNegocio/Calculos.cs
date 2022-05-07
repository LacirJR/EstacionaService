using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstacionaService.RegrasDeNegocio
{
    public class Calculos
    {
        public decimal CalcularFechamento(TimeSpan tempoNoPatio, string tipoVeiculo)
        {
            decimal valorAPagar = 0;

            if (tipoVeiculo != "MOTO")
                valorAPagar = 2m;

            if (tempoNoPatio.Ticks <= TimeSpan.Parse("00:15").Ticks)
            {
                valorAPagar += 3.50m;
                
            } else if (tempoNoPatio.Ticks > TimeSpan.Parse("00:15").Ticks && tempoNoPatio.Ticks <= TimeSpan.Parse("00:30").Ticks)
            {
                valorAPagar += 7m; 
            } else if(tempoNoPatio.Ticks > TimeSpan.Parse("00:30").Ticks && tempoNoPatio.Ticks <= TimeSpan.Parse("00:45").Ticks)
            {
                valorAPagar += 10.5m;
            }else if(tempoNoPatio.Ticks > TimeSpan.Parse("00:45").Ticks && tempoNoPatio.Ticks < TimeSpan.Parse("01:00").Ticks)
            {
                valorAPagar += 14m;
            }else
            {
                valorAPagar = ((decimal)tempoNoPatio.TotalHours) * 17.5m;
            }


            return valorAPagar;
        }

        public TimeSpan TempoGasto(DateTime entrada, DateTime saida)
        {
            DateTime data = DateTime.Now;
            TimeSpan tempoNoPatio = new TimeSpan(saida.Ticks - entrada.Ticks);



            return tempoNoPatio;
        }
    }
}
