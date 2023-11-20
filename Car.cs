using System;
using System.Runtime.Serialization;

[Serializable]
[DataContract]
public class Car
{
	[DataMember]
	public string Company {  get; set; }
	[DataMember]
	public string Model { get; set; }
	[DataMember]
	public short Year { get; set; }
	public Car(string company, string model, short year)
	{
		Company = company;
		Model = model;
		Year = year;
	}

	public override string ToString()
	{
		return Company + " " + Model + " , " + Year;
	}

    protected Car(SerializationInfo info, StreamingContext context)
    {
		Company = info.GetString("Company");
		Model = info.GetString("Model");
		Year = info.GetInt16("Year");
    }
}
