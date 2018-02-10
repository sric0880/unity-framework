public class TypeOfArray extends TypeInfo {
    TypeInfo valueType;
    public TypeOfArray() {
        super(Type.ARRAY, "Array");
    }

    @Override
    public String getSerializeName() {
        return typeName +"_"+ valueType.typeName;
    }
}
