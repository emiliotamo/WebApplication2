namespace WebApplication2
{
    public class Cervezas
    {
        public string Nombre { get; set; }

        public decimal Graduacion { get; set; }

        public string Pais { get; set; }

        public Cervezas(string nombre, decimal graduacion, string pais)
        {
            Nombre = nombre;
            Graduacion = graduacion;
            Pais = pais;
        }
    }
}