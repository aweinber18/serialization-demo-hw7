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
        IList<Car> cars = new List<Car>() {
            new Car("Honda", "CRV", 2012),
             new Car("Toyota", "Sienna", 2004),
              new Car("Ford", "F-150", 1993),
        };


        var then = DateTime.Now;
        var dataContractSerializer = new DataContractSerializer(typeof(List<Car>));
        using (var stream = new FileStream("cars-serialized.xml", FileMode.Create))
        {
            dataContractSerializer.WriteObject(stream, cars);
            Console.WriteLine("XML Method: Object Serialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.");
        }

        cars = null;

        then = DateTime.Now;
        using (var stream = new FileStream("cars-serialized.xml", FileMode.Open))
        {
            cars = (List<Car>) dataContractSerializer.ReadObject(stream);
            Console.WriteLine("Object Deserialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.");
        }

        foreach (var car in cars)
            Console.WriteLine(car.ToString()); Console.WriteLine();
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
        var stream = File.Open("cars-serialized.bin", FileMode.Create);
        binaryFormatter.Serialize(stream, cars);
        stream.Close();
        Console.WriteLine("Binary Method: Object Serialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.");

        cars = null;

        then = DateTime.Now;
        stream = File.Open("cars-serialized.bin", FileMode.Open);
        binaryFormatter = new BinaryFormatter();
        cars = (List<Car>) binaryFormatter.Deserialize(stream);
        stream.Close(); 
        Console.WriteLine("Object Deserialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.");

        foreach (var car in cars)
            Console.WriteLine(car.ToString()); Console.WriteLine();
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
        Console.WriteLine("JSON Method: Object Serialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.");

        cars = null;

        then = DateTime.Now;
        cars = JsonSerializer.Deserialize<List<Car>>(File.ReadAllText(filename));
        Console.WriteLine("Object Deserialized in " + (DateTime.Now.Millisecond - then.Millisecond) + "ms.");

        foreach (var car in cars)
            Console.WriteLine(car.ToString()); Console.WriteLine();
    }


    public static void Main(string[] args)
    {
        var program = new Program();
        program.XmlSerializationDemo();
        program.JSONSerializationDemo();
        program.BinarySerializationDemo();
    }
}
