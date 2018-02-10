// Generated from /Users/qiong/Documents/myProject/unity-framework/tools/build_codegen_configgen/confparser/src/Config.g4 by ANTLR 4.7

import java.util.*;

import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link ConfigParser}.
 */
public interface ConfigListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link ConfigParser#config}.
	 * @param ctx the parse tree
	 */
	void enterConfig(ConfigParser.ConfigContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#config}.
	 * @param ctx the parse tree
	 */
	void exitConfig(ConfigParser.ConfigContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#ns}.
	 * @param ctx the parse tree
	 */
	void enterNs(ConfigParser.NsContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#ns}.
	 * @param ctx the parse tree
	 */
	void exitNs(ConfigParser.NsContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#qualifiedName}.
	 * @param ctx the parse tree
	 */
	void enterQualifiedName(ConfigParser.QualifiedNameContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#qualifiedName}.
	 * @param ctx the parse tree
	 */
	void exitQualifiedName(ConfigParser.QualifiedNameContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#struct}.
	 * @param ctx the parse tree
	 */
	void enterStruct(ConfigParser.StructContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#struct}.
	 * @param ctx the parse tree
	 */
	void exitStruct(ConfigParser.StructContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#enumdecl}.
	 * @param ctx the parse tree
	 */
	void enterEnumdecl(ConfigParser.EnumdeclContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#enumdecl}.
	 * @param ctx the parse tree
	 */
	void exitEnumdecl(ConfigParser.EnumdeclContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#data}.
	 * @param ctx the parse tree
	 */
	void enterData(ConfigParser.DataContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#data}.
	 * @param ctx the parse tree
	 */
	void exitData(ConfigParser.DataContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#enumBody}.
	 * @param ctx the parse tree
	 */
	void enterEnumBody(ConfigParser.EnumBodyContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#enumBody}.
	 * @param ctx the parse tree
	 */
	void exitEnumBody(ConfigParser.EnumBodyContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#classBody}.
	 * @param ctx the parse tree
	 */
	void enterClassBody(ConfigParser.ClassBodyContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#classBody}.
	 * @param ctx the parse tree
	 */
	void exitClassBody(ConfigParser.ClassBodyContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#declaration}.
	 * @param ctx the parse tree
	 */
	void enterDeclaration(ConfigParser.DeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#declaration}.
	 * @param ctx the parse tree
	 */
	void exitDeclaration(ConfigParser.DeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#type}.
	 * @param ctx the parse tree
	 */
	void enterType(ConfigParser.TypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#type}.
	 * @param ctx the parse tree
	 */
	void exitType(ConfigParser.TypeContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#definedType}.
	 * @param ctx the parse tree
	 */
	void enterDefinedType(ConfigParser.DefinedTypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#definedType}.
	 * @param ctx the parse tree
	 */
	void exitDefinedType(ConfigParser.DefinedTypeContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#primitiveType}.
	 * @param ctx the parse tree
	 */
	void enterPrimitiveType(ConfigParser.PrimitiveTypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#primitiveType}.
	 * @param ctx the parse tree
	 */
	void exitPrimitiveType(ConfigParser.PrimitiveTypeContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#dictKeyType}.
	 * @param ctx the parse tree
	 */
	void enterDictKeyType(ConfigParser.DictKeyTypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#dictKeyType}.
	 * @param ctx the parse tree
	 */
	void exitDictKeyType(ConfigParser.DictKeyTypeContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#dictValueType}.
	 * @param ctx the parse tree
	 */
	void enterDictValueType(ConfigParser.DictValueTypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#dictValueType}.
	 * @param ctx the parse tree
	 */
	void exitDictValueType(ConfigParser.DictValueTypeContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#attribute}.
	 * @param ctx the parse tree
	 */
	void enterAttribute(ConfigParser.AttributeContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#attribute}.
	 * @param ctx the parse tree
	 */
	void exitAttribute(ConfigParser.AttributeContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#attributeName}.
	 * @param ctx the parse tree
	 */
	void enterAttributeName(ConfigParser.AttributeNameContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#attributeName}.
	 * @param ctx the parse tree
	 */
	void exitAttributeName(ConfigParser.AttributeNameContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#literal}.
	 * @param ctx the parse tree
	 */
	void enterLiteral(ConfigParser.LiteralContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#literal}.
	 * @param ctx the parse tree
	 */
	void exitLiteral(ConfigParser.LiteralContext ctx);
	/**
	 * Enter a parse tree produced by {@link ConfigParser#booleanLiteral}.
	 * @param ctx the parse tree
	 */
	void enterBooleanLiteral(ConfigParser.BooleanLiteralContext ctx);
	/**
	 * Exit a parse tree produced by {@link ConfigParser#booleanLiteral}.
	 * @param ctx the parse tree
	 */
	void exitBooleanLiteral(ConfigParser.BooleanLiteralContext ctx);
}