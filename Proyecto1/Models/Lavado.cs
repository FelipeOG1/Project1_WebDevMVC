namespace Proyecto1.Models
{
    public enum EstadoLavado
    {
        EnProceso,
        Facturado,
        agendado
    }

   


     public abstract class Lavado
    {

        public int Id { get; }
        public string Tipo { get; set; }
        
        public List<string> Beneficios { get; set; }

        public int Precio { get; set; }

        public EstadoLavado Estado { get; set; }



        public abstract void agendarLavado();

        public abstract void elimininarLavado();

        public abstract void actualizarLavado();

        public abstract void buscarServicioLavado();


        
      
    }


    public class LavadoDeluxe : Lavado
    {

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