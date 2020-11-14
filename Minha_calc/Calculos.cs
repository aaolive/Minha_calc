using System;
using System.Collections.Generic;
using System.Text;

namespace Minha_calc
{
    class Calculos
    {
        // somar
        public double Somar(double numero1, double numero2)
        {
            double resultadoSoma = numero1 + numero2;
            return resultadoSoma;
        }

        //subtrair
        public double Subtrair(double numero1, double numero2)
        {
            double resultadoSubtracao = numero1 - numero2;
            return resultadoSubtracao;
        }

        // multiplicar
        public double Multiplicar(double numero1, double numero2)
        {
            double resultadoMultiplicacao = numero1 * numero2;
            return resultadoMultiplicacao;
        }

        // dividir
        public double Dividir(double numero1, double numero2)
        {
            try
            {
                if (numero2 == 0)
                {
                    throw new Exception();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Houve um erro: {0}", e);
                Console.WriteLine("Não é possível dividir por 0.");
            }
            
            double resultadoDivisao = numero1 / numero2;
            return resultadoDivisao;
        }
    }
}
