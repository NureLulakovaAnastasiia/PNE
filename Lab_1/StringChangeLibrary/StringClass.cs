using System.Reflection.Metadata.Ecma335;

namespace StringChangeLibrary
{
    //Замінити усі круглі дужки на квадратні.
    //Клас з реалізацією обчислень
    public class StringClass
    {

        //метод для заміни дужок
        public string ChangeBrackets(string value)
        {
            var res = string.Empty;

            //модифікація строки
            if (!String.IsNullOrEmpty(value))
            {
                res = value.Replace('(', '[');
                res = res.Replace(')', ']');
            }

            return res;
        }
    }
}