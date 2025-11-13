using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Proj_Filas_Acessos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcao;
            Cadastro gerenciador = new Cadastro();
            do
            {
                Console.WriteLine("\nMenu de opções:" +
                    "\n0. Finalizar processo" +
                    "\n1.Cadastrar ambiente" +
                    "\n2.Consultar ambiente" +
                    "\n3.Excluir ambiente" +
                    "\n4.Cadastrar usuario" +
                    "\n5.Consultar usuario" +
                    "\n6.Excluir usuario" +
                    "\n7.Conceder permissão de acesso ao usuario" + //(informar ambiente e usuário - vincular ambiente ao usuário)"
                    "\n8.Revogar permissão de acesso ao usuario" + //(informar ambiente e usuário - desvincular ambiente do usuário)"
                    "\n9.Registrar acesso" + //(informar o ambiente e o usuário - registrar o log respectivo)"
                    "\n10.Consultar logs de acesso" + //(informar o ambiente e listar os logs - filtrar por logs autorizados / negados" / todos));
                    "\nEscolha a opção desejada: ");
                string inputOpcao = Console.ReadLine();
                if (!int.TryParse(inputOpcao, out opcao))
                {
                    Console.WriteLine("Ocorreu um erro, verifique se digitou corretamente.\n");
                    continue;
                }
                switch (opcao)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine("\ndigite o ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int addID))
                        {
                            Console.WriteLine("ID inválido!\n");
                            break;
                        }
                        Console.WriteLine("digite o Nome: ");
                        string addNome = Console.ReadLine();
                        Console.WriteLine("digite o laboratorio: ");
                        string addLaboratorio = Console.ReadLine();

                        Medicamento novo = new Medicamento(addID, addNome, addLaboratorio);
                        farmacia.adicionar(novo);
                        break;
                    case 2:
                        Console.WriteLine("\ndigite o ID do medicamento");
                        if (!int.TryParse(Console.ReadLine(), out int idPesquisa))
                        {
                            Console.WriteLine("ID inválido!\n");
                            break;
                        }
                        Medicamento medicamentoPesquisado = farmacia.pesquisa(new Medicamento(idPesquisa));
                        if (medicamentoPesquisado != null && medicamentoPesquisado.Id != -1)

                        {
                            Console.WriteLine(medicamentoPesquisado.ToString());
                        }
                        else { Console.WriteLine("Medicamento não encontrado!\n"); }
                        break;
                    case 3:
                        Console.WriteLine("\nDigite o ID do medicamento: ");
                        if (!int.TryParse(Console.ReadLine(), out int idPesquisa2))
                        {
                            Console.WriteLine("ID inválido!\n");
                            break;
                        }
                        Medicamento medicamentoPesquisado2 = farmacia.pesquisa(new Medicamento(idPesquisa2));
                        if (medicamentoPesquisado2 != null && medicamentoPesquisado2.Id != -1)
                        {
                            Console.WriteLine(medicamentoPesquisado2.ToString());
                            if (medicamentoPesquisado2.Lotes.Count > 0)
                            {
                                Console.WriteLine("Lotes cadastrados:");
                                foreach (Lote lote in medicamentoPesquisado2.Lotes)
                                {
                                    Console.WriteLine("   " + lote.ToString());
                                }
                            }
                            else { Console.WriteLine("Nenhum lote cadastrado para este medicamento.\n"); }
                        }
                        else { Console.WriteLine("Medicamento não encontrado!\n"); }
                        break;
                    case 4:
                        Console.WriteLine("\nDigite o Id pra comprar : ");
                        if (!int.TryParse(Console.ReadLine(), out int IdComprar))
                        {
                            Console.WriteLine("ID inválido!\n");
                            break;
                        }
                        Medicamento medicamentoComprado = farmacia.pesquisa(new Medicamento(IdComprar));
                        if (medicamentoComprado != null && medicamentoComprado.Id != -1)
                        {
                            Console.WriteLine("Digite o ID do lote: ");
                            if (!int.TryParse(Console.ReadLine(), out int idLote))
                            {
                                Console.WriteLine("ID inválido!\n");
                                break;
                            }
                            Console.WriteLine("Digite a quantidade do lote: ");
                            if (!int.TryParse(Console.ReadLine(), out int qtdeLote))
                            {
                                Console.WriteLine("Quantidade inválida!\n");
                                return;
                            }
                            Lote novoLote = new Lote(idLote, qtdeLote, DateTime.Now);
                            medicamentoComprado.comprar(novoLote);
                            Console.WriteLine("Lote comprado com sucesso!\n");
                        }
                        else
                        {
                            Console.WriteLine("Medicamento não encontrado!\n");
                        }
                        break;
                    case 5:
                        Console.WriteLine("\nDigite o Id pra vender: ");
                        if (!int.TryParse(Console.ReadLine(), out int IdVender))
                        {
                            Console.WriteLine("ID inválido!\n");
                            break;
                        }
                        Medicamento medicamentoVendido = farmacia.pesquisa(new Medicamento(IdVender));
                        if (medicamentoVendido != null && medicamentoVendido.Id != -1)
                        {
                            Console.WriteLine("Digite a quantidade a vender: ");
                            if (!int.TryParse(Console.ReadLine(), out int qtdeVender))
                            {
                                Console.WriteLine("Quantidade inválida!\n");
                                return;
                            }
                            if (medicamentoVendido.vender(qtdeVender))
                            {
                                Console.WriteLine("Venda realizada com sucesso!\n");
                            }
                            else { Console.WriteLine("Quantidade indisponível para venda!\n"); }
                        }
                        else { Console.WriteLine("Medicamento não encontrado!\n"); }
                        break;
                    case 6:
                        Console.WriteLine("\nLista de medicamentos cadastrados:\n");
                        foreach (Medicamento med in farmacia.ListaMedicamentos)
                        {
                            Console.WriteLine(med.ToString());
                        }
                        break;
                    default:
                        Console.WriteLine("\nOcorreu um erro, verifique se digitou corretamente.\n");
                        break;
                }
            } while (opcao != 0);
        }
    }
}
