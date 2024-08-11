using Dominio;
using System.Collections.Generic;

namespace Repositorio.Interface
{
    public interface IAdvogadoRepositorio
    {
        List<Advogado> ListarAdvogados();
        Advogado ObterAdvogado(int id);
        void IncluirAdvogado(Advogado advogado);
        void AtualizarAdvogado(Advogado advogado);
        void ExcluirAdvogado(int id);
    }
}
