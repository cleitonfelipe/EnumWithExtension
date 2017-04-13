using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PostEnumExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            string entrada = Veiculos.Carro.ToString();
            string resultado = string.Empty;
            switch (entrada)
            {
                case "Carro":
                    resultado = Veiculos.Carro.GetStringValue();
                    break;
                case "Aviao":
                    resultado = Veiculos.Aviao.GetStringValue();
                    break;
                case "Moto":
                    resultado = Veiculos.Moto.GetStringValue();
                    break;
                default:
                    resultado = "Sem valor";
                    break;
            }
            Console.WriteLine(resultado);
            Console.ReadKey();
        }
    }

    public enum Veiculos
    {
        [StringValue("Onix 4 Portas 1.4 Flex")]
        Carro,
        [StringValue("Airbus A380")]
        Aviao,
        [StringValue("Honda CB 300")]
        Moto
    }

    public static class EnumExtension
    {
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());

            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }

    public sealed class StringValueAttribute : Attribute
    {

        #region Properties

        public string StringValue { get; set; }

        #endregion

        #region Constructor

        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        #endregion

    }

    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
