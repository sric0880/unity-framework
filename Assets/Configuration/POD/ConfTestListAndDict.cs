using System.Collections.Generic;

public class ConfTestListAndDict {

	[Require]
	public List<string> stars;
	public List<int> levels;
	[Require]
	public Dictionary<int, string> names;
	public Dictionary<string, int> heroes;
	public List<int> listofHeros;
	[RefID(typeof(Config), "testEnumDict")]
	public Modules module;

}
