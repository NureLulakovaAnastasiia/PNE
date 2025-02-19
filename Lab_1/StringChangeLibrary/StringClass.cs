using System.Reflection.Metadata.Ecma335;

namespace StringChangeLibrary
{

    //Замінити усі круглі дужки на квадратні.
    public class StringClass
    {

        public string ChangeBrackets(string value)
        {
            var res = string.Empty;

            if (!String.IsNullOrEmpty(value))
            {
                res = value.Replace('(', '[');
                res = res.Replace(')', ']');
            }

            return res;
        }
    }
}
