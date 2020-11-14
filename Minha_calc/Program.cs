using System;

namespace Minha_calc
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Hello World!");
            Calculos cal = new Calculos();

            Conta cont = new Conta();
            cont.ExibirArray();

            int contador = 3;
            string num = "";
            num += contador;

            int dinheiro = 2;
            int dinheiro2 = dinheiro;
            dinheiro2++;
            Console.WriteLine("dinheiro 1: {0}, dinheiro 2: {1}", dinheiro, dinheiro2);

            char vogal = 'i';
            switch (vogal)
            {
                case 'a':
                    Console.WriteLine("letra " + vogal);
                    break;
                    var n = 20;
                    var m = 221;
                case 'e':
                    Console.WriteLine("letra " + vogal);
                    break;
                default:
                    Console.WriteLine("nenhuma vogal");
                    break;
            }
            */

            /*int[] numarray = new int[10];
            Console.WriteLine(numarray.Length);
            Console.WriteLine(numarray[1]);
            
            string nome = "Anderson de Assis";
            Console.WriteLine(nome.Length);
            int inddoS = nome.LastIndexOf("de");
            nome = nome.Insert(inddoS, "bb");
            Console.WriteLine(nome);
            nome = nome.Replace("Anderson", "Andrea");
            Console.WriteLine(nome);*/

            // exemplo da classe conta
            Conta contaExemplo = new Conta();
            //contaExemplo.ResolverConta();
            //contaExemplo.MostrarAContaResolvidaEmParte();

            // exemplo de conta com regex
            contaExemplo.ResolverContaComRegexII();

        }
    }
}
