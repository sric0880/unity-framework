public class GameFSM : FSM
{
	private static GameFSM instance;
	public static GameFSM Instance
	{
		get {
			return instance?? (instance = new GameFSM());
		}
	}

	public BoolTrigger bootOverTrigger = new BoolTrigger(false); //启动资源加载完毕，触发事件
	public BoolTrigger updateResOverTrigger = new BoolTrigger(false); //资源更新成功，触发事件
	public BoolTrigger loginSuccessTrigger = new BoolTrigger(false); //登录成功，触发事件
	public BoolTrigger reLoginTrigger = new BoolTrigger(false); //切换账号，重新登录触发事件

	public GameBootState boot;
	public GameCheckUpdateState checkUpdate;
	public GameLoginState login;
	public GameMainCityState maincity;
	public GameBattleState battle;

	public void Start()
	{
		boot = new GameBootState();
		checkUpdate = new GameCheckUpdateState();
		login = new GameLoginState();
		maincity = new GameMainCityState();
		battle = new GameBattleState();
		// 游戏启动的入口
		this.CurState = boot;
		this.AnyState = new GameAnyState();

		this.AddTransition(boot, checkUpdate, bootOverTrigger, null);

		// update app 属于强制更新，该状态没有出口

		this.AddTransition(checkUpdate, login, updateResOverTrigger, null);

		this.AddTransition(login, maincity, loginSuccessTrigger, null);

		//this.AddTransition(maincity, battle, , null);
		//this.AddTransition(battle, maincity, , null);
		// 切换账号，重新登录
		this.AddTransition(this.AnyState, login, reLoginTrigger, null);
	}
}
