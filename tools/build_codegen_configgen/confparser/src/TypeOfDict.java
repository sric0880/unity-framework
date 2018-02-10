public class TypeOfDict extends TypeInfo {
    public TypeInfo keyType;
    public TypeInfo valueType;

    public TypeOfDict() {
        super(Type.DICT, "Dict");
    }

    @Override
    public String getSerializeName() {
        return typeName + "_" + keyType.typeName + "_" + valueType.typeName;
    }
}
