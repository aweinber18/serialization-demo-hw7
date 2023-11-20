// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Serialization;

public class Program
{
    public void XmlSerializationDemo()
    {
        IList<Human> humans = new List<Human>() {
            new Human("Avraham", 'E', "Weinberg", 2000),
             new Human("Yonatan", 'S', "Katz", 2000),
              new Human("Mark", 'Y', "Kimmel", 2000),
               new Human("Dov", 'B', "Kimmel", 2000),
        };


        var dataContractSerializer = new DataContractSerializer(typeof(List<Human>));
        using (var stream = new FileStream("humans-serialized.dat", FileMode.Create))
        {
            var then = DateTime.Now;
            dataContractSerializer.WriteObject(stream, humans);
            Console.WriteLine("Object Serialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.");
        }

        humans = null;

        using (var stream = new FileStream("humans-serialized.dat", FileMode.Open))
        {
            var then = DateTime.Now;
            humans = (List<Human>) dataContractSerializer.ReadObject(stream);
            Console.WriteLine("Object Deserialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.\n");
        }

        foreach (var human in humans)
            Console.WriteLine(human.ToString());
    }

    public void BinarySerializationDemo()
    {
        IList<Car> cars = new List<Car>() {
            new Car("Honda", "CRV", 2012),
             new Car("Toyota", "Sienna", 2004),
              new Car("Ford", "F-150", 1993),
        };

        var then = DateTime.Now;
        var binaryFormatter = new BinaryFormatter();
        var stream = File.Open("cars-serialized.dat", FileMode.Create);
        binaryFormatter.Serialize(stream, cars);
        stream.Close();
        Console.WriteLine("Object Serialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.");

        cars = null;

        then = DateTime.Now;
        stream = File.Open("cars-serialized.dat", FileMode.Open);
        binaryFormatter = new BinaryFormatter();
        cars = (List<Car>) binaryFormatter.Deserialize(stream);
        stream.Close(); 
        Console.WriteLine("Object Deserialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.\n");

        foreach (var car in cars)
            Console.WriteLine(car.ToString());
    }

    public void JSONSerializationDemo()
    {
        IList<Car> cars = new List<Car>() {
            new Car("Honda", "CRV", 2012),
             new Car("Toyota", "Sienna", 2004),
              new Car("Ford", "F-150", 1993),
        };

        var then = DateTime.Now;
        var filename = "cars-serialized.json";
        File.WriteAllText(filename, JsonSerializer.Serialize(cars));
        Console.WriteLine("Object Serialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.");

        cars = null;

        then = DateTime.Now;
        cars = JsonSerializer.Deserialize<List<Car>>(File.ReadAllText(filename));
        Console.WriteLine("Object Deserialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.\n");

        foreach (var car in cars)
            Console.WriteLine(car.ToString());
    }


    public static void Main(string[] args)
    {
        var program = new Program();
        program.JSONSerializationDemo();
    }
}
