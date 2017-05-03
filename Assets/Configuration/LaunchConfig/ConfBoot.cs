public class ConfBoot
{
	[Min(1), Max(6)]
	public int log_priority;
	public string locale;                               // 本地化设置
	public bool show_console;                           // 控制台开关
}