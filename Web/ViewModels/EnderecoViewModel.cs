namespace Web.ViewModels
{
    public class EnderecoViewModel
    {
        //Deixando o ID ser nulo aqui para a criação
        public int? Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}