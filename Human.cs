using System;
using System.Runtime.Serialization;

[DataContract]
public class Human
{
    [DataMember]
	private string FirstName {  get; set; }
	[DataMember]
	private char MiddleInitial {  get; set; }
	[DataMember]
	private string LastName { get; set; }
	[DataMember]
	private short DOB {  get; set; }
	
	public Human(string fn, char mi, string ln, short dob)
	{
		FirstName = fn;
		MiddleInitial = mi;
		LastName = ln;
		DOB = dob;
	}

	public override string ToString()
	{
		return FirstName + " " + MiddleInitial + " " + LastName
			+ ", Age " + (DateTime.Now.Year - DOB);
	}

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
		FirstName = info.GetString("FirstName");
		MiddleInitial = info.GetChar("MiddleInitial");
		LastName = info.GetString("LastName");
		DOB = info.GetInt16("DOB");
    }
}
