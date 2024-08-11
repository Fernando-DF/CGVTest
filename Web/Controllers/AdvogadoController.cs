using Dominio;
using Repositorio.Implementacao;
using Repositorio.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class AdvogadoController : Controller
    {
        private readonly IAdvogadoRepositorio _repositorio;

        public AdvogadoController()
        {
            _repositorio = new AdvogadoRepositorio();
        }

        public ActionResult Index()
        {
            var advogados = _repositorio.ListarAdvogados();

            var advogadoViewModels = advogados.Select(x => new AdvogadoViewModel
            {
                Id = x.Id,
                Nome = x.Nome,
                Senioridade = (Senioridade)x.Senioridade,

                Endereco = new EnderecoViewModel
                {
                    Logradouro = x.Endereco.Logradouro,
                    Numero = x.Endereco.Numero,
                    Bairro = x.Endereco.Bairro,
                    Estado = x.Endereco.Estado,
                    Cep = x.Endereco.Cep,
                    Complemento = x.Endereco.Complemento
                }
            }).ToList();

            return View(advogadoViewModels);
        }

        public ActionResult Details(int id)
        {
            var advogado = _repositorio.ObterAdvogado(id);
            if (advogado == null) return HttpNotFound();
            return View(advogado);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var advogadoViewModel = new AdvogadoViewModel();

            advogadoViewModel.Estados = ObterEstados();

            return View(advogadoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdvogadoViewModel advogadoViewModel)
        {
            if (ModelState.IsValid)
            {
                var advogado = new Advogado
                {
                    Nome = advogadoViewModel.Nome,
                    Senioridade = advogadoViewModel.Senioridade,
                    Endereco = new Endereco
                    {
                        Logradouro = advogadoViewModel.Endereco.Logradouro,
                        Bairro = advogadoViewModel.Endereco.Bairro,
                        Estado = advogadoViewModel.Endereco.Estado,
                        Cep = advogadoViewModel.Endereco.Cep,
                        Numero = advogadoViewModel.Endereco.Numero,
                        Complemento = advogadoViewModel.Endereco.Complemento
                    }
                };

                _repositorio.IncluirAdvogado(advogado);

                return RedirectToAction("Index");
            }

            advogadoViewModel.Estados = ObterEstados();
            return View(advogadoViewModel);
        }

        public ActionResult Edit(int id)
        {
            var advogado = _repositorio.ObterAdvogado(id);
            if (advogado == null) return HttpNotFound();

            var advogadoViewModel = new AdvogadoViewModel() {
                Id = advogado.Id,
                Nome = advogado.Nome,
                Senioridade = (Senioridade)advogado.Senioridade,

                Endereco = new EnderecoViewModel
                {
                    Id = advogado.Endereco.Id,
                    Logradouro = advogado.Endereco.Logradouro,
                    Numero = advogado.Endereco.Numero,
                    Bairro = advogado.Endereco.Bairro,
                    Estado = advogado.Endereco.Estado,
                    Cep = advogado.Endereco.Cep,
                    Complemento = advogado.Endereco.Complemento
                }
            };

            advogadoViewModel.Estados = ObterEstados();

            return View(advogadoViewModel);
        }

        [HttpPost]
        public ActionResult Edit(Advogado advogado)
        {
            if (ModelState.IsValid)
            {
                _repositorio.AtualizarAdvogado(advogado);
                return RedirectToAction("Index");
            }
            return View(advogado);
        }

        public ActionResult Delete(int id)
        {
            var advogado = _repositorio.ObterAdvogado(id);
            if (advogado == null) return HttpNotFound();

            var advogadoViewModel = new AdvogadoViewModel()
            {
                Id = advogado.Id,
                Nome = advogado.Nome,
                Senioridade = (Senioridade)advogado.Senioridade,

                Endereco = new EnderecoViewModel
                {
                    Id = advogado.Endereco.Id,
                    Logradouro = advogado.Endereco.Logradouro,
                    Numero = advogado.Endereco.Numero,
                    Bairro = advogado.Endereco.Bairro,
                    Estado = advogado.Endereco.Estado,
                    Cep = advogado.Endereco.Cep,
                    Complemento = advogado.Endereco.Complemento
                }
            };

            return View(advogadoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repositorio.ExcluirAdvogado(id);
            return RedirectToAction("Index");
        }

        private List<SelectListItem> ObterEstados()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "SP", Text = "São Paulo" },
                new SelectListItem { Value = "RJ", Text = "Rio de Janeiro" },
                new SelectListItem { Value = "MG", Text = "Minas Gerais" },

            };
        }

    }
}