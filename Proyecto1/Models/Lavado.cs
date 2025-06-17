namespace Proyecto1.Models
{
    public enum EstadoLavado
    {
        EnProceso,
        Facturado,
        Agendado
    }

     public abstract class Lavado
    {

        public int Id { get; }
        public string Tipo { get; set; }
        
        public List<string> Beneficios { get; set; }

        public int Precio { get; set; }

        public EstadoLavado Estado { get; set; }



        

        protected Lavado(int id, string tipo, List<string> beneficios, int precio, EstadoLavado estado)
        {
            Id = id;
            Tipo = tipo;
            Beneficios = beneficios ?? new List<string>();
            Precio = precio;
            Estado = estado;
        }



        public abstract void agendarLavado();


        

        public abstract void elimininarLavado();

        public abstract void actualizarLavado();

        public abstract void buscarServicioLavado();
   
      
    }

    public class LavadoDeluxe : Lavado
    {

        public LavadoDeluxe(int id,EstadoLavado estado)
        : base(id, "Básico", new List<string> { "Aspirado", "Lavado exterior" }, 20000, estado)
        {
        }

        public override void agendarLavado()
        {

            throw new NotFiniteNumberException();
           
        }
        public override void actualizarLavado()
        {
            throw new NotImplementedException();
        }

        public override void buscarServicioLavado()
        {
            throw new NotImplementedException();
        }

        public override void elimininarLavado()
        {
            throw new NotImplementedException();
        }

    }

    


    




    

            
} 