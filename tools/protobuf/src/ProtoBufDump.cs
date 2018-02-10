using System;
using System.IO;
using Google.Protobuf;
using UF.Config;

public class ProtoBufDump {

	private static int Main(string[] args)
	{
		if (args.Length != 2)
		{
			Console.Error.WriteLine("Usage: Google.Protobuf.JsonDump <descriptor type name> <input data>");
			Console.Error.WriteLine("The descriptor type name is the fully-qualified message name,");
			Console.Error.WriteLine("including assembly e.g. ProjectNamespace.Message,Company.Project");
			return 1;
		}
		Type type = Type.GetType(args[0]);
		if (type == null)
		{
			Console.Error.WriteLine("Unable to load type {0}.", args[0]);
			return 1;
		}
		if (!typeof(IMessage).IsAssignableFrom(type))
		{
			Console.Error.WriteLine("Type {0} doesn't implement IMessage.", args[0]);
			return 1;
		}
		IMessage message = (IMessage) Activator.CreateInstance(type);

		using (var writer = File.CreateText(args[1]))
		{
			JsonFormatter.Settings settings = new JsonFormatter.Settings(true); 
			JsonFormatter formatter = new JsonFormatter(settings);
			formatter.Format(message, writer);
		}
		// using (var input = File.OpenRead(args[1]))
		// {
		// 	message.MergeFrom(input);
		// }
		// Console.WriteLine(message);
		return 0;
	}
}
