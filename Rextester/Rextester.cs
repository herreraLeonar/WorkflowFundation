using System;
using System.Reflection;

namespace WorkflowNet
{
    class Rextester
    {
        public string prop1 { get; set; }
        public string prop2 { get; set; }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Rextester objClass1 = new Rextester { prop1 = "value1", prop2 = "value2" };

            GenericPropertyFinder<Rextester> objGenericPropertyFinder = new GenericPropertyFinder<Rextester>();
            objGenericPropertyFinder.PrintTModelPropertyAndValue(objClass1);
            Console.ReadLine();
        }
    }
    public class GenericPropertyFinder<TModel> where TModel : class
    {
        public void PrintTModelPropertyAndValue(TModel tmodelObj)
        {
            Type tModelType = tmodelObj.GetType();

            PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();

            foreach (PropertyInfo property in arrayPropertyInfos)
            {
                Console.WriteLine("Name of property is\t:\t" + property.Name);
                Console.WriteLine("Value of property is\t:\t" + property.GetValue(tmodelObj).ToString());
                Console.WriteLine(Environment.NewLine);
            }
        }
    }
}