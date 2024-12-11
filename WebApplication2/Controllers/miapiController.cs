﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class miapiController : ControllerBase
    {
        private static readonly List<Cervezas> Lista = new List<Cervezas>
    {
        new Cervezas("Estrella Galicia", 3.0m, "España"),
        new Cervezas("Mahou", 4.5m, "España"),
        new Cervezas("Heineken", 3.5m, "Holanda"),
        new Cervezas("Guinness", 3.3m, "Irlanda")
    };

        private static readonly string[] Paises = new[]
        {
        "España", "Holanda", "Irlanda", "Alemania", "EEUU"
    };
        private static readonly String[] NombreCerveza = new[]
       {
            "Estrella Galicia", "Mahou", "Heineken", "Guinness", "Budweiser"
        };

        private readonly ILogger<miapiController> _logger;

        public miapiController(ILogger<miapiController> logger)
        {
            _logger = logger;
        }

        [HttpGet("cervezas")]
        public IActionResult GetCervezas()
        {
            _logger.LogInformation("Obteniendo la lista de cervezas.");
            return Ok(Lista);
        }
        [HttpPost]
        public ActionResult<Cervezas> Post([FromBody] Cervezas nuevaCerveza)
        {
            if (nuevaCerveza == null || string.IsNullOrWhiteSpace(nuevaCerveza.Nombre))
            {
                return BadRequest(new { mensaje = "Los datos de la cerveza no son válidos." });
            }

            if (string.IsNullOrEmpty(nuevaCerveza.Pais))
            {
                nuevaCerveza.Pais = "Países Bajos";
            }

            if (nuevaCerveza.Graduacion <= 0)
            {
                nuevaCerveza.Graduacion = 5.0m; // Valor predeterminado
            }

            Lista.Add(nuevaCerveza);

            return CreatedAtAction(nameof(GetCervezas), new { nombre = nuevaCerveza.Nombre }, nuevaCerveza);
        }

        [HttpPut("cervezas/{nombreActual}")]
        public IActionResult ActualizarNombre(string nombreActual, [FromBody] string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                return BadRequest(new { mensaje = "El nuevo nombre no puede estar vacío." });
            }

            var cerveza = Lista.Find(c => c.Nombre.Equals(nombreActual, StringComparison.OrdinalIgnoreCase));
            if (cerveza == null)
            {
                return NotFound(new { mensaje = $"Cerveza con nombre '{nombreActual}' no encontrada." });
            }

            cerveza.Nombre = nuevoNombre;

            return Ok(new { mensaje = "Nombre actualizado exitosamente.", cerveza });
        }

        [HttpDelete("cervezas/{nombre}")]
        public IActionResult EliminarCerveza(string nombre)
        {
            var cerveza = Lista.Find(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (cerveza == null)
            {
                return NotFound(new { mensaje = $"Cerveza con nombre '{nombre}' no encontrada." });
            }

            Lista.Remove(cerveza);

            return Ok(new { mensaje = "Cerveza eliminada exitosamente.", cerveza });
        }
    }
}

