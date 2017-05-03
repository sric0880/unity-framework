public class ConfTestDict
{
	[ID] public int id;
	public string name;
	public string desc;
	[RefID(typeof(Config), "testHeroes")]
	public int hero_id;
	public bool isMale;
	public string[] model_male;
	public string[] model_female;
	public string carrer_icon_path;
}
