using System;
using System.Globalization;

namespace NumeroExtenso
{
	public class Program
	{
        public static void Main(string[] args)
        {
            try
            {
                decimal valor = 45785.95M;
                Console.WriteLine(ToExtenso(valor, true));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        public static string ToExtenso(decimal valor, bool maiuscula)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema!";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += EscreverParte(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : string.Empty);

                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                                valor_por_extenso += " DE";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                                valor_por_extenso += " DE";
                            else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                                valor_por_extenso += " DE";

                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " REAL";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " REAIS";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " E ";
                    }

                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " CENTAVO";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " CENTAVOS";
                }

                valor_por_extenso += ".";

                if (maiuscula)
                    return valor_por_extenso;
                else
                    return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor_por_extenso.ToLower());
            }
        }

        public static string EscreverParte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                    valor *= 100;

                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                _ = (a == 1) 
                    ? montagem += (b + c == 0) ? "CEM" : "CENTO"
                    : (a == 2)
                        ? montagem += "DUZENTOS"
                        : (a == 3)
                            ? montagem += "TREZENTOS"
                            : (a == 4)
                                ? montagem += "QUATROCENTOS"
                                : (a == 5)
                                    ? montagem += "QUINHENTOS"
                                    : (a == 6)
                                        ? montagem += "SEISCENTOS"
                                        : (a == 7)
                                            ? montagem += "SETECENTOS"
                                            : (a == 8)
                                                ? montagem += "OITOCENTOS"
                                                : (a == 9)
                                                    ? montagem += "NOVECENTOS"
                                                    : montagem += "";

                _ = (b == 1)
                    ? montagem += EscreverDezena(a, c)
                    : (b == 2) 
                        ? montagem += ((a > 0) ? " E " : string.Empty) + "VINTE"
                        : (b == 3) 
                            ? montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA"
                            : (b == 4) 
                                ? montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA"
                                : (b == 5) 
                                    ? montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA"
                                    : (b == 6) 
                                        ? montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA"
                                        : (b == 7) 
                                            ? montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA"
                                            : (b == 8) 
                                                ? montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA"
                                                : (b == 9) 
                                                    ? montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA"
                                                    : montagem += "";

                _ = (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) ? montagem += " E " : montagem += "";

                if (strValor.Substring(1, 1) != "1")
                    _ = (c == 1)
                        ? montagem += "UM"
                        : (c == 2)
                            ? montagem += "DOIS"
                            : (c == 3)
                                ? montagem += "TRÊS"
                                : (c == 4)
                                    ? montagem += "QUATRO"
                                    : (c == 5)
                                        ? montagem += "CINCO"
                                        : (c == 6)
                                            ? montagem += "SEIS"
                                            : (c == 7)
                                                ? montagem += "SETE"
                                                : (c == 8)
                                                    ? montagem += "OITO"
                                                    : (c == 9)
                                                        ? montagem += "NOVE"
                                                        : montagem += "";
                return montagem;
            }
        }

        public static string EscreverDezena(int a, int c)
        {
            string retorno = string.Empty;

            _ = (c == 0)
                 ? retorno += ((a > 0) ? " E " : string.Empty) + "DEZ"
                 : (c == 1)
                     ? retorno += ((a > 0) ? " E " : string.Empty) + "ONZE"
                     : (c == 2)
                         ? retorno += ((a > 0) ? " E " : string.Empty) + "DOZE"
                         : (c == 3)
                             ? retorno += ((a > 0) ? " E " : string.Empty) + "TREZE"
                             : (c == 4)
                                 ? retorno += ((a > 0) ? " E " : string.Empty) + "QUATORZE"
                                 : (c == 5)
                                     ? retorno += ((a > 0) ? " E " : string.Empty) + "QUINZE"
                                     : (c == 6)
                                         ? retorno += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS"
                                         : (c == 7)
                                             ? retorno += ((a > 0) ? " E " : string.Empty) + "DEZESSETE"
                                             : (c == 8)
                                                 ? retorno += ((a > 0) ? " E " : string.Empty) + "DEZOITO"
                                                 : (c == 9)
                                                     ? retorno += ((a > 0) ? " E " : string.Empty) + "DEZENOVE"
                                                     : retorno += "";
            return retorno;
        }
    }
}
