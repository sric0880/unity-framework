public class LuaExportAttribute : ExportAttribute {

	public LuaExportAttribute(string luaTableName) : base("[lua]"+luaTableName) { }

	public LuaExportAttribute(string luaTableName, string key) : base("[lua]"+luaTableName, key) { }
}
