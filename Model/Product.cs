
namespace Model
{
    public class Product
    {
        private string codigo;
        private string qtde;
        private string codigoLote;

        public string CodigoLote
        {
            get { return codigoLote; }
            set { codigoLote = value; }
        }


        public string Qtde
        {
            get { return qtde; }
            set { qtde = value; }
        }

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
    }
}
