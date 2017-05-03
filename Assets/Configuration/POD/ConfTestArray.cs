public class ConfTestArray  {

	[RefID(typeof(Config), "testStrDict")]
	public string name;
	public int serverid;
	[Require]
	public ConfTestArray1[] address_list = {};
}
