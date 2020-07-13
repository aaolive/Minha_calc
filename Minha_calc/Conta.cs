using System;
using System.Collections.Generic;
using System.Text;

namespace Minha_calc
{
    class Conta
    {
        // variaveis
        bool temSoma = false;
        bool temSubtracao = false;
        bool temMultiplicacao = false;
        bool temDivisao = false;

        // variavel para indicar se a conta esta resolvida
        bool aContaEstaResolvida = false;

        // variavel de conta de exemplo
        static string contaPraFazer = "12+14*10";
        string contaSendoResolvida = "";
        string resultadoDaConta = "0.0";

        string[] arrayDeNumeros = contaPraFazer.Split('*', '+', '-', '/');

        private void ExibirArray()
        {
            foreach(var n in arrayDeNumeros)
            {
                Console.WriteLine(n);
            }
        }
        
        // metodo para identificar que operação existe na conta.
        public void ProcurarOperacao()
        {
            temSoma = false;
            temSubtracao = false;
            temMultiplicacao = false;
            temDivisao = false;

            foreach(char ch in contaPraFazer)
            {
                if (ch == '+')
                    temSoma = true;
                else if (ch == '-')
                    temDivisao = true;
                else if (ch == '*')
                    temMultiplicacao = true;
                else if (ch == '/')
                    temDivisao = true;
            }
        }

        // metodo para ver se a conta está resolvida
        public void VerificarSeAContaEstaResolvida()
        {
            if ((temSoma == false) && (temSubtracao == false) && (temMultiplicacao == false) && (temDivisao == false))
                aContaEstaResolvida = true;
        }

        // metodo para mostrar a conta respondida parcialmente
        public void MostrarAContaResolvidaEmParte()
        {
            Console.Write("Conta sendo resolvida: {0}", contaPraFazer);
        }

        // metodo para resolver a conta
        public void ResolverConta()
        {
            // variavel da classe de calculo usado para soma, subtracao, etc.
            Calculos calc = new Calculos();

            // executo o metodo para saber que operacoes existem.
            ProcurarOperacao();

            // repito até resolver a conta
            while (aContaEstaResolvida == false)
            {
                //se tiver multiplicao ou divivisao faço primeiro, depois adição e subtração
                if (temMultiplicacao == true)
                {
                    // variaveis para busca dos numeros durante a resolução da conta e para o sinal
                    int indiceDoNumeroNaConta = 0;
                    string numero1 = "0";
                    string numero2 = "0";
                    int indiceDoSinalDeMultiplicacaoNaConta = contaPraFazer.IndexOf('*');

                    // procuro o numero com menor indice que vem antes do sinal de multiplicacao,
                    //quando o indice se tornar maior é o numero depois do sinal
                    foreach (string numero in arrayDeNumeros)
                    {
                        indiceDoNumeroNaConta = contaPraFazer.IndexOf(numero);
                        if (indiceDoNumeroNaConta < indiceDoSinalDeMultiplicacaoNaConta)
                        {
                            numero1 = numero;
                        }
                        else if (indiceDoNumeroNaConta > indiceDoSinalDeMultiplicacaoNaConta)
                        {
                            numero2 = numero;
                            break;
                        }
                    }
                    // recebe o resultado da multiplicação
                    double resultado = calc.Multiplicar(double.Parse(numero1), double.Parse(numero2));

                    // guardo a conta que fiz
                    string contaDeMultplicacaoFeita = numero1 + "*" + numero2;
                    // guardo o indice da conta feita para inserir o resultado no lugar.
                    int indiceDaContaDeMultiplicaoNaExpressao = contaPraFazer.IndexOf(contaDeMultplicacaoFeita);
                    // insiro na posicao da conta que fiz o resultado e,
                    // depois removo a conta que fiz da expressão...
                    contaPraFazer = contaPraFazer.Insert(indiceDaContaDeMultiplicaoNaExpressao, resultado.ToString());
                    // pego o indice da conta novamente porque mudou depois de inserir o resultado
                    indiceDaContaDeMultiplicaoNaExpressao = contaPraFazer.IndexOf(contaDeMultplicacaoFeita);
                    // recebe o tamanho da string para remover
                    int tamanhoDaConta = contaDeMultplicacaoFeita.Length;
                    // agora removo a conta feita
                    contaPraFazer = contaPraFazer.Remove(indiceDaContaDeMultiplicaoNaExpressao, tamanhoDaConta);
                    // vejo novamente se há operacoes para fazer e se tiver continuo resolvendo
                    ProcurarOperacao();
                    VerificarSeAContaEstaResolvida();
                    if (aContaEstaResolvida == false)
                    {
                        // separo os numeros de novo na expressao
                        arrayDeNumeros = contaPraFazer.Split('*', '+', '-', '/');
                    }
                }
                else if (temDivisao == true)
                {
                    // variaveis para busca dos numeros durante a resolução da conta e para o sinal
                    int indiceDoNumeroNaConta = 0;
                    string numero1 = "0";
                    string numero2 = "0";
                    int indiceDoSinalDeDivisaoNaConta = contaPraFazer.IndexOf('/');

                    // procuro o numero com menor indice que vem antes do sinal de divisão,
                    //quando o indice se tornar maior é o numero depois do sinal
                    foreach (string numero in arrayDeNumeros)
                    {
                        indiceDoNumeroNaConta = contaPraFazer.IndexOf(numero);
                        if (indiceDoNumeroNaConta < indiceDoSinalDeDivisaoNaConta)
                        {
                            numero1 = numero;
                        }
                        else if (indiceDoNumeroNaConta > indiceDoSinalDeDivisaoNaConta)
                        {
                            numero2 = numero;
                            break;
                        }
                    }
                    // recebe o resultado da divisão
                    double resultado = calc.Dividir(double.Parse(numero1), double.Parse(numero2));

                    // guardo a conta que fiz
                    string contaDeDivisaoFeita = numero1 + "/" + numero2;
                    // guardo o indice da conta feita para inserir o resultado no lugar.
                    int indiceDaContaDeDivisaoNaExpressao = contaPraFazer.IndexOf(contaDeDivisaoFeita);
                    // insiro na posicao da conta que fiz o resultado e,
                    // depois removo a conta que fiz da expressão...
                    contaPraFazer = contaPraFazer.Insert(indiceDaContaDeDivisaoNaExpressao, resultado.ToString());
                    // pego o indice da conta novamente porque mudou depois de inserir o resultado
                    indiceDaContaDeDivisaoNaExpressao = contaPraFazer.IndexOf(contaDeDivisaoFeita);
                    // recebe o tamanho da string para remover
                    int tamanhoDaConta = contaDeDivisaoFeita.Length;
                    // agora removo a conta feita
                    contaPraFazer = contaPraFazer.Remove(indiceDaContaDeDivisaoNaExpressao, tamanhoDaConta);
                    // vejo novamente se há operacoes para fazer e se tiver continuo resolvendo
                    ProcurarOperacao();
                    VerificarSeAContaEstaResolvida();
                    if (aContaEstaResolvida == false)
                    {
                        // separo os numeros de novo na expressao
                        arrayDeNumeros = contaPraFazer.Split('*', '+', '-', '/');
                    }
                }
                else if (temSoma == true)
                {
                    // variaveis para busca dos numeros durante a resolução da conta e para o sinal
                    int indiceDoNumeroNaConta = 0;
                    string numero1 = "0";
                    string numero2 = "0";
                    int indiceDoSinalDeSomaNaConta = contaPraFazer.IndexOf('+');

                    // procuro o numero com menor indice que vem antes do sinal de adição,
                    //quando o indice se tornar maior é o numero depois do sinal
                    foreach (string numero in arrayDeNumeros)
                    {
                        indiceDoNumeroNaConta = contaPraFazer.IndexOf(numero);
                        if (indiceDoNumeroNaConta < indiceDoSinalDeSomaNaConta)
                        {
                            numero1 = numero;
                        }
                        else if (indiceDoNumeroNaConta > indiceDoSinalDeSomaNaConta)
                        {
                            numero2 = numero;
                            break;
                        }
                    }
                    // recebe o resultado da adição
                    double resultado = calc.Somar(double.Parse(numero1), double.Parse(numero2));

                    // guardo a conta que fiz
                    string contaDeSomaFeita = numero1 + "+" + numero2;
                    // guardo o indice da conta feita para inserir o resultado no lugar.
                    int indiceDaContaDeSomaNaExpressao = contaPraFazer.IndexOf(contaDeSomaFeita);
                    // insiro na posicao da conta que fiz o resultado e,
                    // depois removo a conta que fiz da expressão...
                    contaPraFazer = contaPraFazer.Insert(indiceDaContaDeSomaNaExpressao, resultado.ToString());
                    // pego o indice da conta novamente porque mudou depois de inserir o resultado
                    indiceDaContaDeSomaNaExpressao = contaPraFazer.IndexOf(contaDeSomaFeita);
                    // recebe o tamanho da string para remover
                    int tamanhoDaConta = contaDeSomaFeita.Length;
                    // agora removo a conta feita
                    contaPraFazer = contaPraFazer.Remove(indiceDaContaDeSomaNaExpressao, tamanhoDaConta);
                    // vejo novamente se há operacoes para fazer e se tiver continuo resolvendo
                    ProcurarOperacao();
                    VerificarSeAContaEstaResolvida();
                    if (aContaEstaResolvida == false)
                    {
                        // separo os numeros de novo na expressao
                        arrayDeNumeros = contaPraFazer.Split('*', '+', '-', '/');
                    }
                }
                else if (temSubtracao == true)
                {
                    // variaveis para busca dos numeros durante a resolução da conta e para o sinal
                    int indiceDoNumeroNaConta = 0;
                    string numero1 = "0";
                    string numero2 = "0";
                    int indiceDoSinalDeSubtracaoNaConta = contaPraFazer.IndexOf('-');

                    // procuro o numero com menor indice que vem antes do sinal de subtracao,
                    //quando o indice se tornar maior é o numero depois do sinal
                    foreach (string numero in arrayDeNumeros)
                    {
                        indiceDoNumeroNaConta = contaPraFazer.IndexOf(numero);
                        if (indiceDoNumeroNaConta < indiceDoSinalDeSubtracaoNaConta)
                        {
                            numero1 = numero;
                        }
                        else if (indiceDoNumeroNaConta > indiceDoSinalDeSubtracaoNaConta)
                        {
                            numero2 = numero;
                            break;
                        }
                    }
                    // recebe o resultado da conta
                    double resultado = calc.Subtrair(double.Parse(numero1), double.Parse(numero2));

                    // guardo a conta que fiz
                    string contaDeSubtracaoFeita = numero1 + "-" + numero2;
                    // guardo o indice da conta feita para inserir o resultado no lugar.
                    int indiceDaContaDeSubtracaoNaExpressao = contaPraFazer.IndexOf(contaDeSubtracaoFeita);
                    // insiro na posicao da conta que fiz o resultado e,
                    // depois removo a conta que fiz da expressão...
                    contaPraFazer = contaPraFazer.Insert(indiceDaContaDeSubtracaoNaExpressao, resultado.ToString());
                    // pego o indice da conta novamente porque mudou depois de inserir o resultado
                    indiceDaContaDeSubtracaoNaExpressao = contaPraFazer.IndexOf(contaDeSubtracaoFeita);
                    // recebe o tamanho da string para remover
                    int tamanhoDaConta = contaDeSubtracaoFeita.Length;
                    // agora removo a conta feita
                    contaPraFazer = contaPraFazer.Remove(indiceDaContaDeSubtracaoNaExpressao, tamanhoDaConta);
                    // vejo novamente se há operacoes para fazer e se tiver continuo resolvendo
                    ProcurarOperacao();
                    VerificarSeAContaEstaResolvida();
                    if (aContaEstaResolvida == false)
                    {
                        // separo os numeros de novo na expressao
                        arrayDeNumeros = contaPraFazer.Split('*', '+', '-', '/');
                    }
                }
            }
            
        }
    }
}
