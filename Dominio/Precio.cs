namespace Dominio
{
    public class Precio
    {
        public int PrecioID {get;set;}
        public decimal PrecioActual {get;set;}
        public decimal Promocion {get;set;}
        public int CursoId {get;set;}
        
        public Curso Curso {get;set;}
    }
}