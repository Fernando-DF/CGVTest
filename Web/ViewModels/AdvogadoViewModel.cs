using Dominio;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.ViewModels
{
    public class AdvogadoViewModel
    {
        //Deixando o ID ser nulo aqui para a criação
        public int? Id { get; set; } 
        public string Nome { get; set; }
        public Senioridade Senioridade { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public IEnumerable<SelectListItem> Estados { get; set; }

    }
}