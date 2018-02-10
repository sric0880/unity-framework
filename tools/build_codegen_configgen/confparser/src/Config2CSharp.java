import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.ParseCancellationException;
import org.antlr.v4.runtime.tree.ParseTree;
import org.antlr.v4.runtime.tree.ParseTreeProperty;
import org.antlr.v4.runtime.tree.ParseTreeWalker;
import org.stringtemplate.v4.ST;
import org.stringtemplate.v4.STGroup;
import org.stringtemplate.v4.STGroupFile;

import java.io.FileWriter;
import java.io.IOException;
import java.util.*;

public class Config2CSharp extends ConfigBaseListener {
    ParseTreeProperty<ST> config = new ParseTreeProperty<>();
    ParseTreeProperty<TypeInfo> _types = new ParseTreeProperty<>();
    STGroup templates = new STGroupFile("CSharp.stg");
    ST namespaceST;
    List<TypeInfo> types = new ArrayList<>();
    HashMap<String, TypeInfo> serializeTypes = new HashMap<>();
    HashSet<String> enumTypes = new HashSet<>();
    String filename;
    String output;

    public Config2CSharp(String filename, String output) {
        this.filename = filename;
        this.output = output;
    }

    private void writeToFile(String fileName, String content) {
        FileWriter writer;
        try {
            if (this.output.isEmpty()) {
                writer = new FileWriter(fileName);
            }
            else {
                writer = new FileWriter(this.output + '/' + fileName);
            }
            writer.write(content);
            writer.flush();
            writer.close();
        }catch (IOException e) {
            System.err.println(e.getMessage());
        }
    }

    private ST getST(String name) {
        ST st = templates.getInstanceOf(name);
        if (st == null) {
            System.err.println("ConfParser error: string template 'class' not found.");
            return null;
        }
        return st;
    }

    private ST getReadExpr(TypeInfo typeInfo) {
        ST readExpr;
        switch (typeInfo.type) {
            case PRIMITIVE:
                readExpr = getST(typeInfo.typeName + "_read_expr");
                readExpr.add("var", typeInfo.varName);
                return readExpr;
            case DICT:
            case ARRAY:
                readExpr = getST("dict_or_array_read_expr");
                readExpr.add("var", typeInfo.varName);
                readExpr.add("serializerName", typeInfo.getSerializeName());
                return readExpr;
            case CLASS:
                readExpr = getST("class_read_expr");
                readExpr.add("var", typeInfo.varName);
                readExpr.add("serializerName", typeInfo.getSerializeName());
                return readExpr;
            case ENUM:
                readExpr = getST("enum_read_expr");
                readExpr.add("var", typeInfo.varName);
                readExpr.add("type", typeInfo.getSerializeName());
                return readExpr;
            default:
                return null;
        }
    }

    private ST getWriteExpr(TypeInfo typeInfo) {
        ST writeExpr;
        switch (typeInfo.type) {
            case PRIMITIVE:
                writeExpr = getST(typeInfo.typeName + "_write_expr");
                writeExpr.add("var", typeInfo.varName);
                return writeExpr;
            case DICT:
            case ARRAY:
                writeExpr = getST("dict_or_array_write_expr");
                writeExpr.add("var", typeInfo.varName);
                writeExpr.add("serializerName", typeInfo.getSerializeName());
                return writeExpr;
            case CLASS:
                writeExpr = getST("class_write_expr");
                writeExpr.add("var", typeInfo.varName);
                return writeExpr;
            case ENUM:
                writeExpr = getST("enum_write_expr");
                writeExpr.add("var", typeInfo.varName);
                return writeExpr;
            default:
                return null;
        }
    }

    private void writeCSharpFile(String className, List<ConfigParser.DeclarationContext> declarations, ST attrST) {
        ST classST = getST("class");
        if (namespaceST != null) {
            classST.add("namespace", namespaceST);
        }

        ST clsBody = getST("classBody");
        if (attrST != null) {
            clsBody.add("attrs", attrST);
        }
        clsBody.add("name", className);

        for (ConfigParser.DeclarationContext declaration: declarations) {
            clsBody.add("members", config.get(declaration));
        }

        for (TypeInfo typeInfo : types) {
            clsBody.add("readExprs", getReadExpr(typeInfo));
            clsBody.add("writeExprs", getWriteExpr(typeInfo));
        }
        types.clear();

        classST.add("classBody", clsBody);

        writeToFile(className + ".cs", classST.render());
    }

    private void writeDictOrArrayCSharpFile() {
        Iterator iter = serializeTypes.entrySet().iterator();
        while(iter.hasNext())
        {
            Map.Entry<String, TypeInfo> entry = (Map.Entry<String, TypeInfo>) iter.next();
            String serializeName = entry.getKey();
            TypeInfo typeInfo = entry.getValue();

            ST classST = getST("class");
            if (namespaceST != null) {
                classST.add("namespace", namespaceST);
            }

            if (typeInfo.type == Type.ARRAY){
                ST clsBody = getST("array_serializer");
                TypeOfArray typeOfArray = (TypeOfArray)typeInfo;
                typeOfArray.valueType.varName = "d[i]";
                clsBody.add("type", typeOfArray.valueType.typeName);
                clsBody.add("typeName", serializeName);
                clsBody.add("readExpr", getReadExpr(typeOfArray.valueType));
                clsBody.add("writeExpr", getWriteExpr(typeOfArray.valueType));
                classST.add("classBody", clsBody);
            } else if (typeInfo.type == Type.DICT) {
                ST clsBody = getST("dict_serializer");
                TypeOfDict typeOfDict = (TypeOfDict)typeInfo;
                clsBody.add("keyType", typeOfDict.keyType.typeName);
                clsBody.add("valueType", typeOfDict.valueType.typeName);
                clsBody.add("typeName", typeInfo.getSerializeName());
                typeOfDict.keyType.varName = "key";
                typeOfDict.valueType.varName = "value";
                clsBody.add("keyReadExpr", getReadExpr(typeOfDict.keyType));
                clsBody.add("valueReadExpr", getReadExpr(typeOfDict.valueType));
                typeOfDict.keyType.varName = "elem.Key";
                typeOfDict.valueType.varName = "elem.Value";
                clsBody.add("keyWriteExpr", getWriteExpr(typeOfDict.keyType));
                clsBody.add("valueWriteExpr", getWriteExpr(typeOfDict.valueType));
                classST.add("classBody", clsBody);
            }

            writeToFile(serializeName + ".cs", classST.render());
        }

        serializeTypes.clear();
    }

    @Override public void exitNs(ConfigParser.NsContext ctx) {
        namespaceST = getST("namespace");
        namespaceST.add("ns", ctx.qualifiedName().getText());
    }

    @Override public void exitStruct(ConfigParser.StructContext ctx) {
        String className = ctx.ID.getText();
        List<ConfigParser.DeclarationContext> declarations = ctx.classBody().declaration();
        writeCSharpFile(className, declarations, null);
    }

    @Override public void exitEnumdecl(ConfigParser.EnumdeclContext ctx) {
        String enumName = ctx.ID.getText();
        enumTypes.add(enumName);
        ST classST = getST("class");
        if (namespaceST != null) {
            classST.add("namespace", namespaceST);
        }

        ST enumBody = getST("enumBody");
        enumBody.add("name", enumName);

        int size = ctx.enumBody().ID().size();
        for (int i = 0; i < size; ++i) {
            String name = ctx.enumBody().ID(i).getText();
            String integer = ctx.enumBody().INT(i).getText();
            ST st = getST("enumexpr");
            st.add("name", name);
            st.add("integer", integer);
            enumBody.add("enums", st);
        }

        classST.add("classBody", enumBody);

        writeToFile(enumName + ".cs", classST.render());
    }

    @Override public void exitData(ConfigParser.DataContext ctx) {
        String className = ctx.ID.getText();
        List<ConfigParser.DeclarationContext> declarations = ctx.classBody().declaration();
        ST attrST = getST("attr");
        attrST.add("name", "Export");
        ST attrArgsST = getST("attrArgs");
        attrArgsST.add("arg", "\""+this.filename+"\"");
        attrST.add("args", attrArgsST);
        writeCSharpFile(className, declarations, attrST);
    }

    @Override public void exitType(ConfigParser.TypeContext ctx) {
        int startType = ctx.start.getType();

        ST st;
        if (startType == ConfigParser.DICT) {
            st = getST("dict");
            String keyTypeName = ctx.dictKeyType().getText();
            String valueTypeName = ctx.dictValueType().getText();
            st.add("keyType", keyTypeName);
            st.add("valueType", valueTypeName);

            TypeOfDict typeInfo = new TypeOfDict();
            typeInfo.keyType = new TypeInfo(Type.PRIMITIVE, keyTypeName);
            if (ctx.dictValueType().definedType() != null) {
                if (enumTypes.contains(valueTypeName)) {
                    typeInfo.valueType = new TypeInfo(Type.ENUM, valueTypeName);
                } else {
                    typeInfo.valueType = new TypeInfo(Type.CLASS, valueTypeName);
                }
            } else {
                typeInfo.valueType = new TypeInfo(Type.PRIMITIVE, valueTypeName);
            }
            types.add(typeInfo);
            _types.put(ctx, typeInfo);
            serializeTypes.put(typeInfo.getSerializeName(), typeInfo);

        } else {
            boolean isArray = false;
            if (ctx.LBRACK() != null) {
                st = getST("array");
                isArray = true;
            }
            else {
                st = getST("type");
            }
            String typeName;
            Type type;
            if (ctx.definedType() != null) {
                typeName = ctx.definedType().getText();
                type = Type.CLASS;
                if (enumTypes.contains(typeName)){
                    type = Type.ENUM;
                }
            } else {
                typeName = ctx.primitiveType().getText();
                type = Type.PRIMITIVE;
            }
            st.add("typeName", typeName);

            if (isArray) {
                TypeOfArray typeOfArray = new TypeOfArray();
                typeOfArray.valueType = new TypeInfo(type, typeName);
                types.add(typeOfArray);
                _types.put(ctx, typeOfArray);
                serializeTypes.put(typeOfArray.getSerializeName(), typeOfArray);
            } else {
                TypeInfo typeInfo = new TypeInfo(type, typeName);
                types.add(typeInfo);
                _types.put(ctx, typeInfo);
            }
        }
        config.put(ctx, st);
    }

    @Override public void exitDeclaration(ConfigParser.DeclarationContext ctx) {
        ST st = getST("member");
        st.add("type", config.get(ctx.type()));
        String varName = ctx.stop.getText();
        st.add("id", varName);
        _types.get(ctx.type()).varName = varName;
        config.put(ctx, st);
    }

    @Override public void exitAttribute(ConfigParser.AttributeContext ctx) {
        ST attrST = getST("attr");
        attrST.add("name", ctx.attributeName().getText());
        List<ConfigParser.LiteralContext> literals = ctx.literal();
        if (literals != null && literals.size() > 0) {
            ST attrArgsST = getST("attrArgs");
            for (ConfigParser.LiteralContext arg : literals) {
                attrArgsST.add("arg", arg.getText());
            }
            attrST.add("args", attrArgsST);
        }
        config.put(ctx, attrST);
    }

    @Override public void exitClassBody(ConfigParser.ClassBodyContext ctx) {
        int childCount = ctx.getChildCount();
        for(int i = 0; i < childCount;) {
            ST st = null;
            ParseTree o = ctx.getChild(i);
            if (ConfigParser.DeclarationContext.class.isInstance(o)) {
                st = config.get(ConfigParser.DeclarationContext.class.cast(o));
            }
            ++i;
            o = ctx.getChild(i);
            while(ConfigParser.AttributeContext.class.isInstance(o)) {
                ST attrST = config.get(ConfigParser.AttributeContext.class.cast(o));
                st.add("attrs", attrST);
                ++i;
                o = ctx.getChild(i);
            }
            ++i;
        }
    }

    @Override public void exitConfig(ConfigParser.ConfigContext ctx) {
        writeDictOrArrayCSharpFile();
        enumTypes.clear();
    }

    public static void main(String[] args) throws Exception {
        if (args.length == 0) {
            throw new IllegalArgumentException("Usage: java -jar confparser.jar [file] [out-dir]");
        }
        String path = args[0];
        int st = path.lastIndexOf('/');
        int end = path.lastIndexOf('.');
        String filename = args[0].substring(st+1, end);
        CharStream input = CharStreams.fromFileName(args[0]);
        ConfigLexer lexer = new ConfigLexer(input);
        CommonTokenStream tokens = new CommonTokenStream(lexer);
        ConfigParser parser = new ConfigParser(tokens);
        parser.setErrorHandler(new ConfigErrorStrategy());
        parser.setBuildParseTree(true);
        try {
            ConfigParser.ConfigContext tree = parser.config();
            Config2CSharp converter;
            if (args.length > 1) {
                converter = new Config2CSharp(filename, args[1]);
            } else {
                converter = new Config2CSharp(filename,"");
            }
            ParseTreeWalker.DEFAULT.walk(converter, tree);
        }
        catch (ParseCancellationException e)
        {
            System.err.println("In file " + args[0]);
            System.err.println("Caused by " + e.getCause().toString());
            throw e;
        }
    }
}
