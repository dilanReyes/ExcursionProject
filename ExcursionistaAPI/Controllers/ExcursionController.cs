using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ExcursionistaAPI.Controllers
{
    [Route("api/excursion")]
    [ApiController]
    public class ExcursionController : ControllerBase
    {
        private static readonly List<Elemento> Elementos = new()
        {
            new Elemento { Nombre = "E1", Peso = 5, Calorias = 3 },
            new Elemento { Nombre = "E2", Peso = 3, Calorias = 5 },
            new Elemento { Nombre = "E3", Peso = 5, Calorias = 2 },
            new Elemento { Nombre = "E4", Peso = 1, Calorias = 8 },
            new Elemento { Nombre = "E5", Peso = 2, Calorias = 3 }
        };

        [HttpPost("seleccionar")]
        public IActionResult SeleccionarElementos([FromBody] SeleccionRequest request)
        {
            if (request == null)
                return BadRequest(new { mensaje = "Datos de entrada inválidos." });

            var seleccionados = ObtenerElementosOptimos(Elementos, request.CaloriasMinimas, request.PesoMaximo);

            return Ok(new
            {
                mensaje = seleccionados.Any() ? "Elementos seleccionados con éxito." : "No se encontraron elementos óptimos.",
                elementos = seleccionados
            });
        }

        private static List<Elemento> ObtenerElementosOptimos(List<Elemento> elementos, int caloriasMinimas, int pesoMaximo)
        {
            var combinaciones = new List<List<Elemento>>();

            for (int i = 0; i < (1 << elementos.Count); i++)
            {
                var seleccionados = new List<Elemento>();
                int pesoTotal = 0, caloriasTotal = 0;

                for (int j = 0; j < elementos.Count; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        seleccionados.Add(elementos[j]);
                        pesoTotal += elementos[j].Peso;
                        caloriasTotal += elementos[j].Calorias;
                    }
                }

                if (pesoTotal <= pesoMaximo && caloriasTotal >= caloriasMinimas)
                    combinaciones.Add(seleccionados);
            }

            return combinaciones.OrderBy(lista => lista.Sum(e => e.Peso)).FirstOrDefault() ?? new List<Elemento>();
        }
    }

    public class SeleccionRequest
    {
        public int CaloriasMinimas { get; set; }
        public int PesoMaximo { get; set; }
    }

    public class Elemento
    {
        public string Nombre { get; set; } = string.Empty;
        public int Peso { get; set; }
        public int Calorias { get; set; }
    }
}
