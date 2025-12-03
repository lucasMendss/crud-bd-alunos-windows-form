using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CadAluno
{
    public static class Validacoes
    {
        public static bool ValidarCPF(string cpf)
        {
            int soma = 0, resto, digito;
            //retiro caracteres ". / -"
            cpf = cpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            //verifico se sobrou 11 caracteres
            if (cpf.Length != 11)
                return false;

            //verificação do primeiro dígito
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }
            //verifico o resto da divisao da soma por 11
            resto = soma % 11;
            //se resto = 0 ou resto = 1 --> digito = 0
            if (resto < 2)
            {
                digito = 0;
            }
            else
            {
                digito = 11 - resto;
            }
            if (digito.ToString() != cpf[9].ToString())
                return false;
            //se chegou até aqui verifico o segundo dígito
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }
            //verifico o resto da divisao da soma por 11
            resto = soma % 11;
            //se resto = 0 ou resto = 1 --> digito = 0
            if (resto < 2)
            {
                digito = 0;
            }
            else
            {
                digito = 11 - resto;
            }
            //verificando dígito
            if (digito.ToString() != cpf[10].ToString())
                return false;
            else
                return true;
        }

        public static bool ValidarRGDeSP(string rg)
        {
            int soma = 0, resto;
            string digito;
            //retiro caracteres ". / -"
            rg = rg.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            //verifico se sobrou 11 caracteres
            if (rg.Length != 9)
                return false;
            //multiplico os termos
            for (int i = 0; i < 8; i++)
            {
                soma += int.Parse(rg[i].ToString()) * (2 + i);
            }
            resto = soma % 11;
            //verificando dígito
            if (resto == 10)
                digito = "X";
            else if (resto == 0)
                digito = "0";
            else
                digito = (11 - resto).ToString();

            if (digito == rg[8].ToString())
                return true;
            else
                return false;
        }
        public static bool ValidarProntuario(string prontuario)
        {
            if (prontuario.Length != 9) return false;      
            string numeros = prontuario.Substring(2, 6);  //separo os números sem o dígito
            string digito = prontuario.Substring(8, 1).ToUpper();
            string digitoCalculado;
            int num, resto, digitoVerificador;
            int[] pesos = { 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            for (int i = 0; i < pesos.Length; i++)
            {
                num = int.Parse(numeros[i].ToString());
                soma += num * pesos[i];
            }

            resto = soma % 11;
            digitoVerificador = 11 - resto;

            if (digitoVerificador == 10)
                digitoCalculado = "X";
            else if (digitoVerificador == 11)
                digitoCalculado = "1";
            else
                digitoCalculado = digitoVerificador.ToString();

            if (digito == digitoCalculado)
                return true;
            else
                return false;
        }
        public static bool ValidarNome(string nome)
        {
            nome = nome.Replace(".", "").Replace("-", "").Replace("/", "");
            if (nome.Length == 0) { return false; }

            return true;
        }
        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            // A pragmatic regex for most web applications
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
