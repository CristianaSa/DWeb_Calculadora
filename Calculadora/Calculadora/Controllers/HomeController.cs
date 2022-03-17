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
        public IActionResult Index(string botao, string visor, string operando, string operacao, string limpaVisor)
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

                    //Atribuir ao visor o algarismo selecionado
                    if (visor != "0" && limpaVisor != "sim") visor = visor + botao;
                    else { visor = botao; }

                    limpaVisor = "nao";

                    break;
                case "-":
                case "+":
                case "x":
                case ":":
                    if (!string.IsNullOrEmpty(operacao)) { 
              
                        double numOperando = double.Parse(operando);
                        double numVisor = double.Parse(visor);
                        switch (operacao)
                        {
                            case "+":
                                visor = (numOperando + numVisor).ToString();
                                break;
                            case "-":
                                visor = (numOperando - numVisor).ToString();
                                break;
                            case "x":
                                visor = (numOperando * numVisor).ToString();
                                break;
                            case ":":
                                visor = (numOperando / numVisor).ToString();
                                    break;
                 
                            default:
                                break;
                        }

                        operacao = botao;
                        operando = visor;
                        limpaVisor = "sim";

                    }
 
                    break;
                case ",":

                    if (limpaVisor != "sim") {
                        visor = "0,";
                        limpaVisor = "nao";

                    }
                    else { 
                    //verifica se o visor é diferente de "0" e se não contém virgula
                    if (visor != "0" && !visor.Contains(",")) visor = visor + ",";
                        //if (visor == "0" && !visor.Contains(",")) visor = "0,"; 
                    }
                    break;
                case "=":
                   //converte a string para float (numero real)
                    double num1 = double.Parse(operando);
                    double num2 = double.Parse(visor);
                   
                    //operaçoes
                    if (operacao == "+") visor = (num1 + num2).ToString();
                    if (operacao == "-") visor = (num1 - num2).ToString();
                    if (operacao == "x") visor = (num1 * num2).ToString();
                    if (operacao == ":") visor = (num1 / num2).ToString();
                    break;
                case "+/-":
                    //multiplica o valor do visor por -1
                    visor = (double.Parse(visor) * -1).ToString(); 
                    
                    break;
                case "C":
                    //limpar o ecrã
                    visor = "0";
                    operando ="";
                    operacao="";
                    limpaVisor = "nao";
                    break;
                default:
                    break;
            }

            //Preparar os dados a serem enviados para a View
            ViewBag.Visor = visor;
            ViewBag.Operando = operando;
            ViewBag.Operacao = operacao;
            ViewBag.LimpaVisor =limpaVisor;
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