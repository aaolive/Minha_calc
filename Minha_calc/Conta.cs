using System;
using System.Globalization;
using System.Text.RegularExpressions;

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

        // variaveis da conta de exemplo
        static string contaPraFazer = "(10*2)";
        string contaSendoResolvida = "";
        string resultadoDaConta = "0.0";

        // array que recebe os numeros da expressao matemantica
        string[] arrayDeNumeros = contaPraFazer.Split('+', '-', '*', '/');

        private void ExibirArray()
        {
            foreach (var n in arrayDeNumeros)
            {
                Console.WriteLine(n);
            }
        }

        // metodo para identificar que operação existe na conta.
        public void ProcurarOperacao()
        {
            // define tudo para falso antes de procurar
            temSoma = false;
            temSubtracao = false;
            temMultiplicacao = false;
            temDivisao = false;

            foreach (char ch in contaPraFazer)
            {
                if (ch == '+')
                    temSoma = true;
                else if (ch == '-')
                    temSubtracao = true;
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
        private void MostrarAContaResolvidaEmParte()
        {
            Console.Write("Conta sendo resolvida: {0}", contaPraFazer);
        }

        // metodo para resolver a conta
        public void ResolverConta()
        {
            // variavel da classe de calculo usado para soma, subtracao, etc.
            Calculos calculo = new Calculos();

            // executo o metodo para saber que operacoes existem.
            ProcurarOperacao();

            // pego os numeros
            arrayDeNumeros = contaPraFazer.Split('+', '-', '*', '/');

            // repito até resolver a conta
            while (aContaEstaResolvida == false)
            {
                //se tiver multiplicao ou divivisao faço primeiro, depois adição e subtração
                if (temMultiplicacao == true)
                {
                    // variaveis para busca dos numeros durante a resolução da conta e para o sinal
                    string numero1 = "0";
                    string numero2 = "0";
                    bool jaTenhoNumero1 = false;
                    bool jaTenhoNumero2 = false;

                    // repasso no arrayDeNumeros para colocar nas variaveis
                    foreach (string numero in arrayDeNumeros)
                    {
                        // coloca os numeros nas variaveis numero 1 e numero 2
                        if (!jaTenhoNumero1)
                        {
                            numero1 = numero;
                            jaTenhoNumero1 = true;
                        }
                        else if (!jaTenhoNumero2)
                        {
                            numero2 = numero;
                            jaTenhoNumero2 = true;
                            string contaDeMultiplicacaoProcurada = numero1 + "*" + numero2;
                            // recebe a resposta se essa string existe na contaPraFazer, se -1 reatribuo a variavel numero2
                            int respostaSeAchouContaMultiplicacao = contaPraFazer.IndexOf(contaDeMultiplicacaoProcurada);
                            if (respostaSeAchouContaMultiplicacao == -1)
                            {
                                numero1 = numero2;
                                jaTenhoNumero2 = false;
                            }
                        }
                    }
                    // recebe o resultado da multiplicação
                    double resultado = calculo.Multiplicar(double.Parse(numero1), double.Parse(numero2));

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
                }
                else if (temDivisao == true)
                {
                    // variaveis para busca dos numeros durante a resolução da conta e para o sinal                    
                    string numero1 = "0";
                    string numero2 = "0";
                    bool jaTenhoNumero1 = false;
                    bool jaTenhoNumero2 = false;                    

                    // repasso no arrayDeNumeros para colocar nas variaveis
                    foreach (string numero in arrayDeNumeros)
                    {
                        // coloca os numeros nas variaveis numero 1 e numero 2
                        if (!jaTenhoNumero1)
                        {
                            numero1 = numero;
                            jaTenhoNumero1 = true;
                        }
                        else if (!jaTenhoNumero2)
                        {
                            numero2 = numero;
                            jaTenhoNumero2 = true;
                            string contaDeDivisaoProcurada = numero1 + "/" + numero2;
                            // recebe a resposta se essa string existe na contaPraFazer, se -1 reatribuo a variavel numero2
                            int respostaSeAchouContaDivisao = contaPraFazer.IndexOf(contaDeDivisaoProcurada);
                            if (respostaSeAchouContaDivisao == -1)
                            {
                                numero1 = numero2;
                                jaTenhoNumero2 = false;
                            }
                        }
                    }

                    // recebe o resultado da divisão
                    double resultado = calculo.Dividir(double.Parse(numero1), double.Parse(numero2));

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
                }
                else if (temSoma == true)
                {
                    // variaveis para busca dos numeros durante a resolução da conta e para o sinal
                    string numero1 = "0";
                    string numero2 = "0";
                    bool jaTenhoNumero1 = false;
                    bool jaTenhoNumero2 = false;

                    // repasso no arrayDeNumeros para colocar nas variaveis
                    foreach (string numero in arrayDeNumeros)
                    {
                        // coloca os numeros nas variaveis numero 1 e numero 2
                        if (!jaTenhoNumero1)
                        {
                            numero1 = numero;
                            jaTenhoNumero1 = true;
                        }
                        else if (!jaTenhoNumero2)
                        {
                            numero2 = numero;
                            jaTenhoNumero2 = true;
                            string contaDeSomaProcurada = numero1 + "+" + numero2;
                            // recebe a resposta se essa string existe na contaPraFazer, se -1 reatribuo a variavel numero2
                            int respostaSeAchouContaSoma = contaPraFazer.IndexOf(contaDeSomaProcurada);
                            if (respostaSeAchouContaSoma == -1)
                            {
                                numero1 = numero2;
                                jaTenhoNumero2 = false;
                            }
                        }
                    }

                    // recebe o resultado da adição
                    double resultado = calculo.Somar(double.Parse(numero1), double.Parse(numero2));

                    // guardo a conta que fiz
                    string contaDeSomaFeita = numero1 + "+" + numero2;

                    // guardo o indice da conta feita para inserir o resultado no lugar.
                    int indiceDaContaDeAdicaoNaExpressao = contaPraFazer.IndexOf(contaDeSomaFeita);

                    // insiro na posicao da conta que fiz o resultado e,
                    // depois removo a conta que fiz da expressão...
                    contaPraFazer = contaPraFazer.Insert(indiceDaContaDeAdicaoNaExpressao, resultado.ToString());

                    // pego o indice da conta novamente porque mudou depois de inserir o resultado
                    indiceDaContaDeAdicaoNaExpressao = contaPraFazer.IndexOf(contaDeSomaFeita);

                    // recebe o tamanho da string para remover
                    int tamanhoDaConta = contaDeSomaFeita.Length;

                    // agora removo a conta feita
                    contaPraFazer = contaPraFazer.Remove(indiceDaContaDeAdicaoNaExpressao, tamanhoDaConta);
                }
                else if (temSubtracao == true)
                {
                    // variaveis para busca dos numeros durante a resolução da conta e para o sinal                    
                    string numero1 = "0";
                    string numero2 = "0";
                    bool jaTenhoNumero1 = false;
                    bool jaTenhoNumero2 = false;

                    // repasso no arrayDeNumeros para colocar nas variaveis
                    foreach (string numero in arrayDeNumeros)
                    {                        
                        // coloca os numeros nas variaveis numero 1 e numero 2
                        if (!jaTenhoNumero1)
                        {
                            numero1 = numero;
                            jaTenhoNumero1 = true;
                        }
                        else if (!jaTenhoNumero2)
                        {
                            numero2 = numero;
                            jaTenhoNumero2 = true;
                            string contaDeSubtracaoProcurada = numero1 + "-" + numero2;
                            // recebe a resposta se essa string existe na contaPraFazer, se -1 reatribuo a variavel numero2
                            int respostaSeAchouContaSubtracao = contaPraFazer.IndexOf(contaDeSubtracaoProcurada);
                            if (respostaSeAchouContaSubtracao == -1)
                            {
                                numero1 = numero2;
                                jaTenhoNumero2 = false;
                            }
                        }
                    }
                    // recebe o resultado da subtracao
                    double resultado = calculo.Subtrair(double.Parse(numero1), double.Parse(numero2));

                    // guardo a conta que fiz
                    string contaDeSutracaoFeita = numero1 + "-" + numero2;

                    // guardo o indice da conta feita para inserir o resultado no lugar.
                    int indiceDaContaDeSubtracaoNaExpressao = contaPraFazer.IndexOf(contaDeSutracaoFeita);

                    // insiro na posicao da conta que fiz o resultado e,
                    //depois removo a conta que fiz da expressão...
                    contaPraFazer = contaPraFazer.Insert(indiceDaContaDeSubtracaoNaExpressao, resultado.ToString());

                    // pego o indice da conta novamente porque mudou depois de inserir o resultado
                    indiceDaContaDeSubtracaoNaExpressao = contaPraFazer.IndexOf(contaDeSutracaoFeita);

                    // recebe o tamanho da string para remover
                    int tamanhoDaConta = contaDeSutracaoFeita.Length;

                    // agora removo a conta feita
                    contaPraFazer = contaPraFazer.Remove(indiceDaContaDeSubtracaoNaExpressao, tamanhoDaConta);
                }

                // verificando de novo a conta
                ProcurarOperacao();
                VerificarSeAContaEstaResolvida();

                // se a conta nao esta resolvida separo os numero na string novamente
                if (aContaEstaResolvida == false)
                {                    
                    arrayDeNumeros = contaPraFazer.Split('+', '-', '*', '/');
                }
            }

            // exibe o resultado da conta
            resultadoDaConta = contaPraFazer;
            Console.WriteLine("Resultado da conta: {0}", resultadoDaConta);
        }

        // variáveis de padrões de conta com regex
        Regex regexDeContaDeSoma = new Regex(@"(\+?\d+[\.\d]*|\-?\d+[\.\d]*)(\+)(\+?\d+[\.\d]*|\-?\d+[\.\d]*)");
        Regex regexDeContaDeSubtrair = new Regex(@"(\+?\d+[\.\d]*|\-?\d+[\.\d]*)(\-)(\+?\d+[\.\d]*|\-?\d+[\.\d]*)");
        Regex regexDeContaDeMultiplicar = new Regex(@"(\+?\d+[\.\d]*|\-?\d+[\.\d]*)(\*)(\+?\d+[\.\d]*|\-?\d+[\.\d]*)");
        Regex regexDeContaDeDivisao = new Regex(@"(\+?\d+[\.\d]*|\-?\d+[\.\d]*)(\/)(\+?\d+[\.\d]*|\-?\d+[\.\d]*)");
        // variável de regex utilizando parentesis, buscando conta entre os parentesis.
        Regex regexDeContaDeSomaComParentesis = new Regex(@"(\(?\+?\d+[\.\d]*|\(?\-?\d+[\.\d]*)(\+)(\+?\d+[\.\d]*\)?|\-?\d+[\.\d]*\)?)");
        Regex regexDeContaDeSubtrairComParentesis = new Regex(@"(\(?\+?\d+[\.\d]*|\(?\-?\d+[\.\d]*)(\-)(\+?\d+[\.\d]*\)?|\-?\d+[\.\d]*\)?)");
        Regex regexDeContaDeMultiplicarComParentesis = new Regex(@"(\(?\+?\d+[\.\d]*|\(?\-?\d+[\.\d]*)(\*)(\+?\d+[\.\d]*\)?|\-?\d+[\.\d]*\)?)");
        Regex regexDeContaDeDividirComParentesis = new Regex(@"(\(?\+?\d+[\.\d]*|\(?\-?\d+[\.\d]*)(\/)(\+?\d+[\.\d]*\)?|\-?\d+[\.\d]*\)?)");

        // método para buscar por operacao de conta utilizando regex
        public void ProcuraOperacaoComRegex()
        {
            // define tudo para falso antes de procurar
            temSoma = false;
            temSubtracao = false;
            temMultiplicacao = false;
            temDivisao = false;

            // aqui define para verdadeiro se tiver soma, subtacao, etc.
            try
            {               
                if (regexDeContaDeSoma.IsMatch(contaPraFazer))
                {
                    temSoma = true;
                }
                else if (regexDeContaDeSubtrair.IsMatch(contaPraFazer))
                {
                    temSubtracao = true;
                }
                else if (regexDeContaDeMultiplicar.IsMatch(contaPraFazer))
                {
                    temMultiplicacao = true;
                }
                else if (regexDeContaDeDivisao.IsMatch(contaPraFazer))
                {
                    temDivisao = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("A conta não é um número.");
            }
        }

        // método para resolver conta com regex
        public void ResolverContaComRegex()
        {
            // variavel da classe de calculo com metodos para calcular
            Calculos calculos = new Calculos();

            // verifico se tem operação para fazer e verifico se a conta está resolvida
            ProcuraOperacaoComRegex();
            VerificarSeAContaEstaResolvida();

            // Aqui repito enquanto a conta não estiver resolvida
            while (aContaEstaResolvida != true)
            {
                // se tiver conta crio objeto match com as correspondecias.
                // aqui resolve multiplicacao
                if (temMultiplicacao == true)
                {
                    // variavel contendo a conta que correspondeu com o padrão
                    Match matchDeMultiplicacao = regexDeContaDeMultiplicar.Match(contaPraFazer);

                    // repito enquanto tiver conta para fazer
                    while (matchDeMultiplicacao.Success)
                    {
                        // variaves para receber os numeros do objeto match
                        var numero1 = 0.0;
                        var numero2 = 0.0;
                        // tento converter os numeros, se der erro mostro um aviso
                        try
                        {
                            numero1 = double.Parse(matchDeMultiplicacao.Groups[1].Value, CultureInfo.InvariantCulture);
                            numero2 = double.Parse(matchDeMultiplicacao.Groups[3].Value, CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Erro ao tentar converter os números. {e}");
                        }

                        // chamo a função de multiplicar
                        var resultado = calculos.Multiplicar(numero1, numero2);

                        // pego o indice da conta que fiz para inserir o resultado no lugar
                        // e retiro a conta que fiz
                        var indiceDaContaDeMultuplicacao = matchDeMultiplicacao.Index;
                        contaPraFazer = regexDeContaDeMultiplicar.Replace(contaPraFazer, resultado.ToString(CultureInfo.InvariantCulture), 1, indiceDaContaDeMultuplicacao);

                        // pesquiso na conta por outras contas
                        matchDeMultiplicacao = regexDeContaDeMultiplicar.Match(contaPraFazer);
                    }


                }
                // resolve divisão
                else if (temDivisao == true)
                {
                    // variavel contendo a conta que correspondeu com o padrão
                    Match matchDeDivisao = regexDeContaDeDivisao.Match(contaPraFazer);

                    // repito enquanto tiver conta para fazer
                    while (matchDeDivisao.Success)
                    {
                        // variaves para receber os numeros do objeto match
                        var numero1 = 0.0;
                        var numero2 = 0.0;
                        // tento converter os numeros, se der erro mostro um aviso
                        try
                        {
                            numero1 = double.Parse(matchDeDivisao.Groups[1].Value, CultureInfo.InvariantCulture);
                            numero2 = double.Parse(matchDeDivisao.Groups[3].Value, CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Erro ao tentar converter os números. {e}");
                        }

                        // chamo a função de dividir
                        var resultado = calculos.Dividir(numero1, numero2);

                        // pego o indice da conta que fiz para inserir o resultado no lugar
                        // e retiro a conta que fiz
                        var indiceDaContaDeDivisao = matchDeDivisao.Index;
                        contaPraFazer = regexDeContaDeDivisao.Replace(contaPraFazer, resultado.ToString(CultureInfo.InvariantCulture), 1, indiceDaContaDeDivisao);

                        // pesquiso na conta por outras contas
                        matchDeDivisao = regexDeContaDeDivisao.Match(contaPraFazer);
                    }
                }
                // faz a adicao
                else if (temSoma == true)
                {
                    // variavel contendo a conta que correspondeu com o padrão
                    Match matchDeSoma = regexDeContaDeSoma.Match(contaPraFazer);

                    // repito enquanto tiver conta para fazer
                    while (matchDeSoma.Success)
                    {
                        // variaves para receber os numeros do objeto match
                        var numero1 = 0.0;
                        var numero2 = 0.0;
                        // tento converter os numeros, se der erro mostro um aviso
                        try
                        {
                            numero1 = double.Parse(matchDeSoma.Groups[1].Value, CultureInfo.InvariantCulture);
                            numero2 = double.Parse(matchDeSoma.Groups[3].Value, CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Erro ao tentar converter os números. {e}");
                        }

                        // chamo a função de soma
                        var resultado = calculos.Somar(numero1, numero2);

                        // pego o indice da conta que fiz para inserir o resultado no lugar
                        // e retiro a conta que fiz
                        var indiceDaContaDeSoma = matchDeSoma.Index;
                        contaPraFazer = regexDeContaDeSoma.Replace(contaPraFazer, resultado.ToString(CultureInfo.InvariantCulture), 1, indiceDaContaDeSoma);

                        // pesquiso na conta por outras contas
                        matchDeSoma = regexDeContaDeSoma.Match(contaPraFazer);
                    }
                }
                // faz subtracao
                else if (temSubtracao == true)
                {
                    // variavel contendo a conta que correspondeu com o padrão
                    Match matchDeSubtracao = regexDeContaDeSubtrair.Match(contaPraFazer);

                    // repito enquanto tiver conta para fazer
                    while (matchDeSubtracao.Success)
                    {
                        // variaves para receber os numeros do objeto match
                        var numero1 = 0.0;
                        var numero2 = 0.0;
                        // tento converter os numeros, se der erro mostro um aviso
                        try
                        {
                            numero1 = double.Parse(matchDeSubtracao.Groups[1].Value, CultureInfo.InvariantCulture);
                            numero2 = double.Parse(matchDeSubtracao.Groups[3].Value, CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Erro ao tentar converter os números. {e}");
                        }

                        // chamo a função de subtrair
                        var resultado = calculos.Subtrair(numero1, numero2);

                        // pego o indice da conta que fiz para inserir o resultado no lugar
                        // e retiro a conta que fiz
                        var indiceDaContaDeSubtracao = matchDeSubtracao.Index;
                        contaPraFazer = regexDeContaDeSubtrair.Replace(contaPraFazer, resultado.ToString(CultureInfo.InvariantCulture), 1, indiceDaContaDeSubtracao);

                        // pesquiso na conta por outras contas
                        matchDeSubtracao = regexDeContaDeSubtrair.Match(contaPraFazer);
                    }
                }

                // chamo os métodos para ver operações na conta e se está terminada.
                ProcuraOperacaoComRegex();
                VerificarSeAContaEstaResolvida();
            }            

            //exibe resultado da multiplicacao
            resultadoDaConta = contaPraFazer;
            Console.WriteLine($"Resultado: {resultadoDaConta}");
        }
        
        // tentativa de resolver com os parentesis I
        public void ResolverContaComRegexII()
        {
            // variavel da classe de calculo com métodos para calcular
            Calculos calculos = new Calculos();

            // verifico se tem operação para fazer e verifico se a conta está resolvida
            ProcuraOperacaoComRegex();
            VerificarSeAContaEstaResolvida();

            // Aqui repito enquanto a conta não estiver resolvida
            while (aContaEstaResolvida != true)
            {
                // se tiver conta crio objeto match com as correspondecias.
                // aqui resolve multiplicacao
                if (temMultiplicacao == true)
                {
                    // variavel contendo a conta que correspondeu com o padrão
                    Match matchDeMultiplicacaoComParenteses = regexDeContaDeMultiplicarComParentesis.Match(contaPraFazer);

                    // a conta de multiplicacao capturada
                    string contaComParenteses = matchDeMultiplicacaoComParenteses.Value;

                    // variavel p/ guardar a posição dos parênteses
                    int posicaoAbreParentese, posicaoFechaParentese;

                    // repito enquanto tiver conta para fazer
                    while (matchDeMultiplicacaoComParenteses.Success)
                    {
                        // pego a conta sem os parenteses
                        Match contaDentroDosParenteses = regexDeContaDeMultiplicar.Match(contaComParenteses);

                        // variaves para receber os numeros do objeto match
                        var numero1 = 0.0;
                        var numero2 = 0.0;

                        // tento converter os numeros, se der erro mostro um aviso
                        try
                        {
                            numero1 = double.Parse(contaDentroDosParenteses.Groups[1].Value, CultureInfo.InvariantCulture);
                            numero2 = double.Parse(contaDentroDosParenteses.Groups[3].Value, CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Erro ao tentar converter os números. {e}");
                        }

                        // chamo a função de multiplicar
                        var resultado = calculos.Multiplicar(numero1, numero2);

                        // pego o indice da conta que fiz para inserir o resultado no lugar
                        // e retiro a conta que fiz
                        var indiceDaContaDeMultiplicacaoComParenteses = matchDeMultiplicacaoComParenteses.Index;
                        contaPraFazer = regexDeContaDeMultiplicarComParentesis.Replace(contaPraFazer, resultado.ToString(CultureInfo.InvariantCulture), 1, indiceDaContaDeMultiplicacaoComParenteses);

                        // pesquiso na conta por outras contas
                        matchDeMultiplicacaoComParenteses = regexDeContaDeMultiplicarComParentesis.Match(contaPraFazer);
                    }


                }
                // resolve divisão
                else if (temDivisao == true)
                {
                    // variavel contendo a conta que correspondeu com o padrão
                    Match matchDeDivisao = regexDeContaDeDivisao.Match(contaPraFazer);

                    // repito enquanto tiver conta para fazer
                    while (matchDeDivisao.Success)
                    {
                        // variaves para receber os numeros do objeto match
                        var numero1 = 0.0;
                        var numero2 = 0.0;
                        // tento converter os numeros, se der erro mostro um aviso
                        try
                        {
                            numero1 = double.Parse(matchDeDivisao.Groups[1].Value, CultureInfo.InvariantCulture);
                            numero2 = double.Parse(matchDeDivisao.Groups[3].Value, CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Erro ao tentar converter os números. {e}");
                        }

                        // chamo a função de dividir
                        var resultado = calculos.Dividir(numero1, numero2);

                        // pego o indice da conta que fiz para inserir o resultado no lugar
                        // e retiro a conta que fiz
                        var indiceDaContaDeDivisao = matchDeDivisao.Index;
                        contaPraFazer = regexDeContaDeDivisao.Replace(contaPraFazer, resultado.ToString(CultureInfo.InvariantCulture), 1, indiceDaContaDeDivisao);

                        // pesquiso na conta por outras contas
                        matchDeDivisao = regexDeContaDeDivisao.Match(contaPraFazer);
                    }
                }
                // faz a adicao
                else if (temSoma == true)
                {
                    // variavel contendo a conta que correspondeu com o padrão
                    Match matchDeSoma = regexDeContaDeSoma.Match(contaPraFazer);

                    // repito enquanto tiver conta para fazer
                    while (matchDeSoma.Success)
                    {
                        // variaves para receber os numeros do objeto match
                        var numero1 = 0.0;
                        var numero2 = 0.0;
                        // tento converter os numeros, se der erro mostro um aviso
                        try
                        {
                            numero1 = double.Parse(matchDeSoma.Groups[1].Value, CultureInfo.InvariantCulture);
                            numero2 = double.Parse(matchDeSoma.Groups[3].Value, CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Erro ao tentar converter os números. {e}");
                        }

                        // chamo a função de soma
                        var resultado = calculos.Somar(numero1, numero2);

                        // pego o indice da conta que fiz para inserir o resultado no lugar
                        // e retiro a conta que fiz
                        var indiceDaContaDeSoma = matchDeSoma.Index;
                        contaPraFazer = regexDeContaDeSoma.Replace(contaPraFazer, resultado.ToString(CultureInfo.InvariantCulture), 1, indiceDaContaDeSoma);

                        // pesquiso na conta por outras contas
                        matchDeSoma = regexDeContaDeSoma.Match(contaPraFazer);
                    }
                }
                // faz subtracao
                else if (temSubtracao == true)
                {
                    // variavel contendo a conta que correspondeu com o padrão
                    Match matchDeSubtracao = regexDeContaDeSubtrair.Match(contaPraFazer);

                    // repito enquanto tiver conta para fazer
                    while (matchDeSubtracao.Success)
                    {
                        // variaves para receber os numeros do objeto match
                        var numero1 = 0.0;
                        var numero2 = 0.0;
                        // tento converter os numeros, se der erro mostro um aviso
                        try
                        {
                            numero1 = double.Parse(matchDeSubtracao.Groups[1].Value, CultureInfo.InvariantCulture);
                            numero2 = double.Parse(matchDeSubtracao.Groups[3].Value, CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Erro ao tentar converter os números. {e}");
                        }

                        // chamo a função de subtrair
                        var resultado = calculos.Subtrair(numero1, numero2);

                        // pego o indice da conta que fiz para inserir o resultado no lugar
                        // e retiro a conta que fiz
                        var indiceDaContaDeSubtracao = matchDeSubtracao.Index;
                        contaPraFazer = regexDeContaDeSubtrair.Replace(contaPraFazer, resultado.ToString(CultureInfo.InvariantCulture), 1, indiceDaContaDeSubtracao);

                        // pesquiso na conta por outras contas
                        matchDeSubtracao = regexDeContaDeSubtrair.Match(contaPraFazer);
                    }
                }

                // chamo os métodos para ver operações na conta e se está terminada.
                ProcuraOperacaoComRegex();
                VerificarSeAContaEstaResolvida();
            }

            //exibe resultado da multiplicacao
            resultadoDaConta = contaPraFazer;
            Console.WriteLine($"Resultado: {resultadoDaConta}");
        }
    }
}
