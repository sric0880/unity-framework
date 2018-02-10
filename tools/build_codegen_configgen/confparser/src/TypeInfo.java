import org.stringtemplate.v4.ST;
import org.stringtemplate.v4.STGroup;

public class TypeInfo {
    public Type type;
    public String typeName;
    public String varName;

    public TypeInfo(Type type, String typeName) {
        this.type = type;
        this.typeName = typeName;
    }

    public String getSerializeName() {
        return typeName;
    }
}
