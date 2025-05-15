using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebSuper.Models;

namespace WebSuper.Pages.Produtos
{
    public class DetalharModel : PageModel
    {
        public Produto produto { get; set; }
        public IActionResult OnGet(int id)
        {
            //Carregando a lista de produtos do arquivo TXT
            var produtos = CarregarProdutos();

            //Usar o método da classe LIST FirstOrDefault para encontrar o produto usando o ID e o LIN -> expressão lambda

            produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return RedirectToPage("/Produtos/Index");
            }
            else
            {
                return Page();
            }
        }
        public List<Produto> CarregarProdutos()
        {
            var produtos = new List<Produto>();

            if (System.IO.File.Exists("produtos.txt"))
            {
                var linhas = System.IO.File.ReadAllLines("produtos.txt");
                foreach (var linha in linhas)
                {
                    var dados = linha.Split(';');
                    var produto = new Produto()
                    {
                        Id = int.Parse(dados[0]),
                        Nome  = dados[1],
                        Preco = double.Parse(dados[2])
                    };
                    produtos.Add(produto);
                }
            }
            return produtos;
        }
    }
}
