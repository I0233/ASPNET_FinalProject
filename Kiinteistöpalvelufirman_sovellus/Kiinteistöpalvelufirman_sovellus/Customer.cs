using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Kiinteistöpalvelufirman_sovellus
{
    class Customer
    {
       public static string sähköposti{ get; set; }
       public static string etunimi { get; set; }
       public static string sukunimi { get; set; }
       public static string salasana { get; set; }
       public static string vahvistettu_salasana { get; set; }
       public static string puhelinnumero { get; set; }
       public static string validation_msg;

       public static bool isNumeric(string input)
       {
           int result;
           return int.TryParse(input,out result);
       }

       public static void Validate(string sähköposti, string etunimi, string sukunimi, string salasana, string vahvistettu_salasana, string puhelinnumero)
       {
           Customer.sähköposti = sähköposti;
           Customer.etunimi = etunimi;
           Customer.sukunimi = sukunimi;
           Customer.salasana = salasana;
           Customer.vahvistettu_salasana = vahvistettu_salasana;
           Customer.puhelinnumero = puhelinnumero;

           if (!Regex.IsMatch(sähköposti, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
           {
               validation_msg += "Sähköposti ei kelpaa";
           }
           if (isNumeric(etunimi))
           {
               validation_msg += " Etunimi ei saa sisältää numeroita eikä symboleja";
           }
           if (isNumeric(sukunimi))
           {
               validation_msg += " Sukunimi ei saa sisältää numeroita eikä symboleja";
           }
           if (salasana != vahvistettu_salasana)
           {
               validation_msg += " Salasanat eivät täsmä";
           }
           if (!puhelinnumero.Any(char.IsDigit))
           {
               validation_msg += " Puhelinnumerossa pitäisi olla ainoastaan numeroita";
           }
       }
        
    }
}
