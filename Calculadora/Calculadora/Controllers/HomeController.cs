using Calculadora.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet] // Esta anotação é facultativa
        public IActionResult Index()
        {
            //Inicializar a calculadora
            ViewBag.Visor = "0";
            return View();
        }

        [HttpPost] //Esta anotação já é obrigatória, para o método 'escutar'
        public IActionResult Index(string botao, string visor, string visorDados, string operacao)
        {
           switch (botao)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    //Caso exista uma operacao que não seja virgula, limpa o visor
                    if (operacao != null && operacao != ",") visor = "";
                    //Atribuir ao visor o algarismo selecionado
                    if (visor != "0") visor = visor + botao;
                    else visor = botao;
                    break;
                case "-":
                case "+":
                case "x":
                case ":":
                    // guardar operacao
                    operacao = botao;
                    //guardar o numero que está no visor num outro input para receber o novo
                    visorDados = visor;
                    break;
                case ",":
                    operacao = botao;
                    //verifica se o visor é diferente de "0" e se não contém virgula
                    if (visor != "0" && visor.Contains(operacao) == false) visor = visor + botao;
                    break;
                case "=":
                   //converte a string para float (numero real)
                    float num1 = float.Parse(visorDados);
                    float num2 = float.Parse(visor);
                   
                    //operaçoes
                    if (operacao == "+") visor = (num1 + num2).ToString();
                    if (operacao == "-") visor = (num1 - num2).ToString();
                    if (operacao == "x") visor = (num1 * num2).ToString();
                    if (operacao == ":") visor = (num1 / num2).ToString();
                    break;
                case "+/-":
                    //multiplica o valor do visor por -1
                    visor = (float.Parse(visor) * -1).ToString();  
                    break;
                case "C":
                    //limpar o ecrã
                    visor = "0";
                    break;
                default:
                    break;
            }

            //Preparar os dados a serem enviados para a View
            ViewBag.Visor = visor;
            ViewBag.VisorDados = visorDados;
            ViewBag.Operacao = operacao;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}