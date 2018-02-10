// Generated from /Users/qiong/Documents/myProject/unity-framework/tools/build_codegen_configgen/confparser/src/Config.g4 by ANTLR 4.7

import java.util.*;

import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class ConfigParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.7", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, DICT=27, ENUM=28, NAMESPACE=29, STRUCT=30, DATA=31, 
		LBRACE=32, RBRACE=33, LBRACK=34, RBRACK=35, SEMICOLON=36, ID=37, INT=38, 
		FloatingPointLiteral=39, CharacterLiteral=40, StringLiteral=41, WS=42, 
		NEWLINE=43, LINE_COMMENT=44, COMMENT=45;
	public static final int
		RULE_config = 0, RULE_ns = 1, RULE_qualifiedName = 2, RULE_struct = 3, 
		RULE_enumdecl = 4, RULE_data = 5, RULE_enumBody = 6, RULE_classBody = 7, 
		RULE_declaration = 8, RULE_type = 9, RULE_definedType = 10, RULE_primitiveType = 11, 
		RULE_dictKeyType = 12, RULE_dictValueType = 13, RULE_attribute = 14, RULE_attributeName = 15, 
		RULE_literal = 16, RULE_booleanLiteral = 17;
	public static final String[] ruleNames = {
		"config", "ns", "qualifiedName", "struct", "enumdecl", "data", "enumBody", 
		"classBody", "declaration", "type", "definedType", "primitiveType", "dictKeyType", 
		"dictValueType", "attribute", "attributeName", "literal", "booleanLiteral"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'.'", "'='", "'<'", "','", "'>'", "'bool'", "'byte'", "'short'", 
		"'ushort'", "'int'", "'uint'", "'long'", "'ulong'", "'float'", "'double'", 
		"'string'", "'XlsxName'", "'RefID'", "'ID'", "'Require'", "'Locale'", 
		"'Min'", "'Max'", "'null'", "'true'", "'false'", "'dict'", "'enum'", "'namespace'", 
		"'struct'", "'data'", "'{'", "'}'", "'['", "']'", "';'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, "DICT", "ENUM", "NAMESPACE", "STRUCT", "DATA", "LBRACE", 
		"RBRACE", "LBRACK", "RBRACK", "SEMICOLON", "ID", "INT", "FloatingPointLiteral", 
		"CharacterLiteral", "StringLiteral", "WS", "NEWLINE", "LINE_COMMENT", 
		"COMMENT"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Config.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }


	Set<String> structNames = new HashSet<String>();

	public ConfigParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}
	public static class ConfigContext extends ParserRuleContext {
		public DataContext data() {
			return getRuleContext(DataContext.class,0);
		}
		public TerminalNode EOF() { return getToken(ConfigParser.EOF, 0); }
		public NsContext ns() {
			return getRuleContext(NsContext.class,0);
		}
		public List<StructContext> struct() {
			return getRuleContexts(StructContext.class);
		}
		public StructContext struct(int i) {
			return getRuleContext(StructContext.class,i);
		}
		public List<EnumdeclContext> enumdecl() {
			return getRuleContexts(EnumdeclContext.class);
		}
		public EnumdeclContext enumdecl(int i) {
			return getRuleContext(EnumdeclContext.class,i);
		}
		public ConfigContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_config; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterConfig(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitConfig(this);
		}
	}

	public final ConfigContext config() throws RecognitionException {
		ConfigContext _localctx = new ConfigContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_config);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(37);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==NAMESPACE) {
				{
				setState(36);
				ns();
				}
			}

			setState(43);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==ENUM || _la==STRUCT) {
				{
				setState(41);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case STRUCT:
					{
					setState(39);
					struct();
					}
					break;
				case ENUM:
					{
					setState(40);
					enumdecl();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(45);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(46);
			data();
			setState(47);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class NsContext extends ParserRuleContext {
		public TerminalNode NAMESPACE() { return getToken(ConfigParser.NAMESPACE, 0); }
		public QualifiedNameContext qualifiedName() {
			return getRuleContext(QualifiedNameContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(ConfigParser.SEMICOLON, 0); }
		public NsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ns; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterNs(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitNs(this);
		}
	}

	public final NsContext ns() throws RecognitionException {
		NsContext _localctx = new NsContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_ns);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(49);
			match(NAMESPACE);
			setState(50);
			qualifiedName();
			setState(51);
			match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class QualifiedNameContext extends ParserRuleContext {
		public List<TerminalNode> ID() { return getTokens(ConfigParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(ConfigParser.ID, i);
		}
		public QualifiedNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_qualifiedName; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterQualifiedName(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitQualifiedName(this);
		}
	}

	public final QualifiedNameContext qualifiedName() throws RecognitionException {
		QualifiedNameContext _localctx = new QualifiedNameContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_qualifiedName);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(53);
			match(ID);
			setState(58);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__0) {
				{
				{
				setState(54);
				match(T__0);
				setState(55);
				match(ID);
				}
				}
				setState(60);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StructContext extends ParserRuleContext {
		public Token ID;
		public TerminalNode STRUCT() { return getToken(ConfigParser.STRUCT, 0); }
		public TerminalNode ID() { return getToken(ConfigParser.ID, 0); }
		public TerminalNode LBRACE() { return getToken(ConfigParser.LBRACE, 0); }
		public ClassBodyContext classBody() {
			return getRuleContext(ClassBodyContext.class,0);
		}
		public TerminalNode RBRACE() { return getToken(ConfigParser.RBRACE, 0); }
		public StructContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_struct; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterStruct(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitStruct(this);
		}
	}

	public final StructContext struct() throws RecognitionException {
		StructContext _localctx = new StructContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_struct);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(61);
			match(STRUCT);
			setState(62);
			((StructContext)_localctx).ID = match(ID);

			            if (!structNames.contains((((StructContext)_localctx).ID!=null?((StructContext)_localctx).ID.getText():null))) {
			                structNames.add((((StructContext)_localctx).ID!=null?((StructContext)_localctx).ID.getText():null));
			            }
			        
			setState(64);
			match(LBRACE);
			setState(65);
			classBody();
			setState(66);
			match(RBRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class EnumdeclContext extends ParserRuleContext {
		public Token ID;
		public TerminalNode ENUM() { return getToken(ConfigParser.ENUM, 0); }
		public TerminalNode ID() { return getToken(ConfigParser.ID, 0); }
		public TerminalNode LBRACE() { return getToken(ConfigParser.LBRACE, 0); }
		public EnumBodyContext enumBody() {
			return getRuleContext(EnumBodyContext.class,0);
		}
		public TerminalNode RBRACE() { return getToken(ConfigParser.RBRACE, 0); }
		public EnumdeclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_enumdecl; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterEnumdecl(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitEnumdecl(this);
		}
	}

	public final EnumdeclContext enumdecl() throws RecognitionException {
		EnumdeclContext _localctx = new EnumdeclContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_enumdecl);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(68);
			match(ENUM);
			setState(69);
			((EnumdeclContext)_localctx).ID = match(ID);

			            if (!structNames.contains((((EnumdeclContext)_localctx).ID!=null?((EnumdeclContext)_localctx).ID.getText():null))) {
			                structNames.add((((EnumdeclContext)_localctx).ID!=null?((EnumdeclContext)_localctx).ID.getText():null));
			            }
			        
			setState(71);
			match(LBRACE);
			setState(72);
			enumBody();
			setState(73);
			match(RBRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DataContext extends ParserRuleContext {
		public Token ID;
		public TerminalNode DATA() { return getToken(ConfigParser.DATA, 0); }
		public TerminalNode ID() { return getToken(ConfigParser.ID, 0); }
		public TerminalNode LBRACE() { return getToken(ConfigParser.LBRACE, 0); }
		public ClassBodyContext classBody() {
			return getRuleContext(ClassBodyContext.class,0);
		}
		public TerminalNode RBRACE() { return getToken(ConfigParser.RBRACE, 0); }
		public DataContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_data; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterData(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitData(this);
		}
	}

	public final DataContext data() throws RecognitionException {
		DataContext _localctx = new DataContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_data);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(75);
			match(DATA);
			setState(76);
			((DataContext)_localctx).ID = match(ID);

			            if (!structNames.contains((((DataContext)_localctx).ID!=null?((DataContext)_localctx).ID.getText():null))) {
			                structNames.add((((DataContext)_localctx).ID!=null?((DataContext)_localctx).ID.getText():null));
			            }
			        
			setState(78);
			match(LBRACE);
			setState(79);
			classBody();
			setState(80);
			match(RBRACE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class EnumBodyContext extends ParserRuleContext {
		public List<TerminalNode> ID() { return getTokens(ConfigParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(ConfigParser.ID, i);
		}
		public List<TerminalNode> INT() { return getTokens(ConfigParser.INT); }
		public TerminalNode INT(int i) {
			return getToken(ConfigParser.INT, i);
		}
		public List<TerminalNode> SEMICOLON() { return getTokens(ConfigParser.SEMICOLON); }
		public TerminalNode SEMICOLON(int i) {
			return getToken(ConfigParser.SEMICOLON, i);
		}
		public EnumBodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_enumBody; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterEnumBody(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitEnumBody(this);
		}
	}

	public final EnumBodyContext enumBody() throws RecognitionException {
		EnumBodyContext _localctx = new EnumBodyContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_enumBody);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(86); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(82);
				match(ID);
				setState(83);
				match(T__1);
				setState(84);
				match(INT);
				setState(85);
				match(SEMICOLON);
				}
				}
				setState(88); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==ID );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ClassBodyContext extends ParserRuleContext {
		public List<DeclarationContext> declaration() {
			return getRuleContexts(DeclarationContext.class);
		}
		public DeclarationContext declaration(int i) {
			return getRuleContext(DeclarationContext.class,i);
		}
		public List<TerminalNode> SEMICOLON() { return getTokens(ConfigParser.SEMICOLON); }
		public TerminalNode SEMICOLON(int i) {
			return getToken(ConfigParser.SEMICOLON, i);
		}
		public List<AttributeContext> attribute() {
			return getRuleContexts(AttributeContext.class);
		}
		public AttributeContext attribute(int i) {
			return getRuleContext(AttributeContext.class,i);
		}
		public ClassBodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_classBody; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterClassBody(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitClassBody(this);
		}
	}

	public final ClassBodyContext classBody() throws RecognitionException {
		ClassBodyContext _localctx = new ClassBodyContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_classBody);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(99); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(90);
				declaration();
				setState(94);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==LBRACK) {
					{
					{
					setState(91);
					attribute();
					}
					}
					setState(96);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(97);
				match(SEMICOLON);
				}
				}
				setState(101); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__11) | (1L << T__12) | (1L << T__13) | (1L << T__14) | (1L << T__15) | (1L << DICT) | (1L << ID))) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclarationContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode ID() { return getToken(ConfigParser.ID, 0); }
		public DeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declaration; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterDeclaration(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitDeclaration(this);
		}
	}

	public final DeclarationContext declaration() throws RecognitionException {
		DeclarationContext _localctx = new DeclarationContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_declaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(103);
			type();
			setState(104);
			match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeContext extends ParserRuleContext {
		public DefinedTypeContext definedType() {
			return getRuleContext(DefinedTypeContext.class,0);
		}
		public TerminalNode LBRACK() { return getToken(ConfigParser.LBRACK, 0); }
		public TerminalNode RBRACK() { return getToken(ConfigParser.RBRACK, 0); }
		public PrimitiveTypeContext primitiveType() {
			return getRuleContext(PrimitiveTypeContext.class,0);
		}
		public TerminalNode DICT() { return getToken(ConfigParser.DICT, 0); }
		public DictKeyTypeContext dictKeyType() {
			return getRuleContext(DictKeyTypeContext.class,0);
		}
		public DictValueTypeContext dictValueType() {
			return getRuleContext(DictValueTypeContext.class,0);
		}
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitType(this);
		}
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_type);
		int _la;
		try {
			setState(123);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ID:
				enterOuterAlt(_localctx, 1);
				{
				setState(106);
				definedType();
				setState(109);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LBRACK) {
					{
					setState(107);
					match(LBRACK);
					setState(108);
					match(RBRACK);
					}
				}

				}
				break;
			case T__5:
			case T__6:
			case T__7:
			case T__8:
			case T__9:
			case T__10:
			case T__11:
			case T__12:
			case T__13:
			case T__14:
			case T__15:
				enterOuterAlt(_localctx, 2);
				{
				setState(111);
				primitiveType();
				setState(114);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LBRACK) {
					{
					setState(112);
					match(LBRACK);
					setState(113);
					match(RBRACK);
					}
				}

				}
				break;
			case DICT:
				enterOuterAlt(_localctx, 3);
				{
				setState(116);
				match(DICT);
				setState(117);
				match(T__2);
				setState(118);
				dictKeyType();
				setState(119);
				match(T__3);
				setState(120);
				dictValueType();
				setState(121);
				match(T__4);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DefinedTypeContext extends ParserRuleContext {
		public Token ID;
		public TerminalNode ID() { return getToken(ConfigParser.ID, 0); }
		public DefinedTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_definedType; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterDefinedType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitDefinedType(this);
		}
	}

	public final DefinedTypeContext definedType() throws RecognitionException {
		DefinedTypeContext _localctx = new DefinedTypeContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_definedType);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(125);
			((DefinedTypeContext)_localctx).ID = match(ID);

			            if (!structNames.contains((((DefinedTypeContext)_localctx).ID!=null?((DefinedTypeContext)_localctx).ID.getText():null))) {
			                notifyErrorListeners("undefined type: "+(((DefinedTypeContext)_localctx).ID!=null?((DefinedTypeContext)_localctx).ID.getText():null));
			            }
			        
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PrimitiveTypeContext extends ParserRuleContext {
		public PrimitiveTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_primitiveType; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterPrimitiveType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitPrimitiveType(this);
		}
	}

	public final PrimitiveTypeContext primitiveType() throws RecognitionException {
		PrimitiveTypeContext _localctx = new PrimitiveTypeContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_primitiveType);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(128);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__11) | (1L << T__12) | (1L << T__13) | (1L << T__14) | (1L << T__15))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DictKeyTypeContext extends ParserRuleContext {
		public DictKeyTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_dictKeyType; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterDictKeyType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitDictKeyType(this);
		}
	}

	public final DictKeyTypeContext dictKeyType() throws RecognitionException {
		DictKeyTypeContext _localctx = new DictKeyTypeContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_dictKeyType);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(130);
			_la = _input.LA(1);
			if ( !(_la==T__9 || _la==T__15) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DictValueTypeContext extends ParserRuleContext {
		public DefinedTypeContext definedType() {
			return getRuleContext(DefinedTypeContext.class,0);
		}
		public PrimitiveTypeContext primitiveType() {
			return getRuleContext(PrimitiveTypeContext.class,0);
		}
		public DictValueTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_dictValueType; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterDictValueType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitDictValueType(this);
		}
	}

	public final DictValueTypeContext dictValueType() throws RecognitionException {
		DictValueTypeContext _localctx = new DictValueTypeContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_dictValueType);
		try {
			setState(134);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ID:
				enterOuterAlt(_localctx, 1);
				{
				setState(132);
				definedType();
				}
				break;
			case T__5:
			case T__6:
			case T__7:
			case T__8:
			case T__9:
			case T__10:
			case T__11:
			case T__12:
			case T__13:
			case T__14:
			case T__15:
				enterOuterAlt(_localctx, 2);
				{
				setState(133);
				primitiveType();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AttributeContext extends ParserRuleContext {
		public TerminalNode LBRACK() { return getToken(ConfigParser.LBRACK, 0); }
		public AttributeNameContext attributeName() {
			return getRuleContext(AttributeNameContext.class,0);
		}
		public TerminalNode RBRACK() { return getToken(ConfigParser.RBRACK, 0); }
		public List<LiteralContext> literal() {
			return getRuleContexts(LiteralContext.class);
		}
		public LiteralContext literal(int i) {
			return getRuleContext(LiteralContext.class,i);
		}
		public AttributeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_attribute; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterAttribute(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitAttribute(this);
		}
	}

	public final AttributeContext attribute() throws RecognitionException {
		AttributeContext _localctx = new AttributeContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_attribute);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(136);
			match(LBRACK);
			setState(137);
			attributeName();
			setState(142);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__3) {
				{
				{
				setState(138);
				match(T__3);
				setState(139);
				literal();
				}
				}
				setState(144);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(145);
			match(RBRACK);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AttributeNameContext extends ParserRuleContext {
		public AttributeNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_attributeName; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterAttributeName(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitAttributeName(this);
		}
	}

	public final AttributeNameContext attributeName() throws RecognitionException {
		AttributeNameContext _localctx = new AttributeNameContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_attributeName);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(147);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__16) | (1L << T__17) | (1L << T__18) | (1L << T__19) | (1L << T__20) | (1L << T__21) | (1L << T__22))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LiteralContext extends ParserRuleContext {
		public TerminalNode INT() { return getToken(ConfigParser.INT, 0); }
		public TerminalNode FloatingPointLiteral() { return getToken(ConfigParser.FloatingPointLiteral, 0); }
		public TerminalNode CharacterLiteral() { return getToken(ConfigParser.CharacterLiteral, 0); }
		public TerminalNode StringLiteral() { return getToken(ConfigParser.StringLiteral, 0); }
		public BooleanLiteralContext booleanLiteral() {
			return getRuleContext(BooleanLiteralContext.class,0);
		}
		public LiteralContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_literal; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterLiteral(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitLiteral(this);
		}
	}

	public final LiteralContext literal() throws RecognitionException {
		LiteralContext _localctx = new LiteralContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_literal);
		try {
			setState(155);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case INT:
				enterOuterAlt(_localctx, 1);
				{
				setState(149);
				match(INT);
				}
				break;
			case FloatingPointLiteral:
				enterOuterAlt(_localctx, 2);
				{
				setState(150);
				match(FloatingPointLiteral);
				}
				break;
			case CharacterLiteral:
				enterOuterAlt(_localctx, 3);
				{
				setState(151);
				match(CharacterLiteral);
				}
				break;
			case StringLiteral:
				enterOuterAlt(_localctx, 4);
				{
				setState(152);
				match(StringLiteral);
				}
				break;
			case T__24:
			case T__25:
				enterOuterAlt(_localctx, 5);
				{
				setState(153);
				booleanLiteral();
				}
				break;
			case T__23:
				enterOuterAlt(_localctx, 6);
				{
				setState(154);
				match(T__23);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BooleanLiteralContext extends ParserRuleContext {
		public BooleanLiteralContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_booleanLiteral; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).enterBooleanLiteral(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof ConfigListener ) ((ConfigListener)listener).exitBooleanLiteral(this);
		}
	}

	public final BooleanLiteralContext booleanLiteral() throws RecognitionException {
		BooleanLiteralContext _localctx = new BooleanLiteralContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_booleanLiteral);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(157);
			_la = _input.LA(1);
			if ( !(_la==T__24 || _la==T__25) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3/\u00a2\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\3\2\5\2(\n\2\3\2\3\2\7\2,\n\2\f\2\16\2/\13\2\3\2\3\2\3\2\3"+
		"\3\3\3\3\3\3\3\3\4\3\4\3\4\7\4;\n\4\f\4\16\4>\13\4\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\6\3\6\3\6\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\b\3"+
		"\b\3\b\3\b\6\bY\n\b\r\b\16\bZ\3\t\3\t\7\t_\n\t\f\t\16\tb\13\t\3\t\3\t"+
		"\6\tf\n\t\r\t\16\tg\3\n\3\n\3\n\3\13\3\13\3\13\5\13p\n\13\3\13\3\13\3"+
		"\13\5\13u\n\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\5\13~\n\13\3\f\3\f\3"+
		"\f\3\r\3\r\3\16\3\16\3\17\3\17\5\17\u0089\n\17\3\20\3\20\3\20\3\20\7\20"+
		"\u008f\n\20\f\20\16\20\u0092\13\20\3\20\3\20\3\21\3\21\3\22\3\22\3\22"+
		"\3\22\3\22\3\22\5\22\u009e\n\22\3\23\3\23\3\23\2\2\24\2\4\6\b\n\f\16\20"+
		"\22\24\26\30\32\34\36 \"$\2\6\3\2\b\22\4\2\f\f\22\22\3\2\23\31\3\2\33"+
		"\34\2\u00a1\2\'\3\2\2\2\4\63\3\2\2\2\6\67\3\2\2\2\b?\3\2\2\2\nF\3\2\2"+
		"\2\fM\3\2\2\2\16X\3\2\2\2\20e\3\2\2\2\22i\3\2\2\2\24}\3\2\2\2\26\177\3"+
		"\2\2\2\30\u0082\3\2\2\2\32\u0084\3\2\2\2\34\u0088\3\2\2\2\36\u008a\3\2"+
		"\2\2 \u0095\3\2\2\2\"\u009d\3\2\2\2$\u009f\3\2\2\2&(\5\4\3\2\'&\3\2\2"+
		"\2\'(\3\2\2\2(-\3\2\2\2),\5\b\5\2*,\5\n\6\2+)\3\2\2\2+*\3\2\2\2,/\3\2"+
		"\2\2-+\3\2\2\2-.\3\2\2\2.\60\3\2\2\2/-\3\2\2\2\60\61\5\f\7\2\61\62\7\2"+
		"\2\3\62\3\3\2\2\2\63\64\7\37\2\2\64\65\5\6\4\2\65\66\7&\2\2\66\5\3\2\2"+
		"\2\67<\7\'\2\289\7\3\2\29;\7\'\2\2:8\3\2\2\2;>\3\2\2\2<:\3\2\2\2<=\3\2"+
		"\2\2=\7\3\2\2\2><\3\2\2\2?@\7 \2\2@A\7\'\2\2AB\b\5\1\2BC\7\"\2\2CD\5\20"+
		"\t\2DE\7#\2\2E\t\3\2\2\2FG\7\36\2\2GH\7\'\2\2HI\b\6\1\2IJ\7\"\2\2JK\5"+
		"\16\b\2KL\7#\2\2L\13\3\2\2\2MN\7!\2\2NO\7\'\2\2OP\b\7\1\2PQ\7\"\2\2QR"+
		"\5\20\t\2RS\7#\2\2S\r\3\2\2\2TU\7\'\2\2UV\7\4\2\2VW\7(\2\2WY\7&\2\2XT"+
		"\3\2\2\2YZ\3\2\2\2ZX\3\2\2\2Z[\3\2\2\2[\17\3\2\2\2\\`\5\22\n\2]_\5\36"+
		"\20\2^]\3\2\2\2_b\3\2\2\2`^\3\2\2\2`a\3\2\2\2ac\3\2\2\2b`\3\2\2\2cd\7"+
		"&\2\2df\3\2\2\2e\\\3\2\2\2fg\3\2\2\2ge\3\2\2\2gh\3\2\2\2h\21\3\2\2\2i"+
		"j\5\24\13\2jk\7\'\2\2k\23\3\2\2\2lo\5\26\f\2mn\7$\2\2np\7%\2\2om\3\2\2"+
		"\2op\3\2\2\2p~\3\2\2\2qt\5\30\r\2rs\7$\2\2su\7%\2\2tr\3\2\2\2tu\3\2\2"+
		"\2u~\3\2\2\2vw\7\35\2\2wx\7\5\2\2xy\5\32\16\2yz\7\6\2\2z{\5\34\17\2{|"+
		"\7\7\2\2|~\3\2\2\2}l\3\2\2\2}q\3\2\2\2}v\3\2\2\2~\25\3\2\2\2\177\u0080"+
		"\7\'\2\2\u0080\u0081\b\f\1\2\u0081\27\3\2\2\2\u0082\u0083\t\2\2\2\u0083"+
		"\31\3\2\2\2\u0084\u0085\t\3\2\2\u0085\33\3\2\2\2\u0086\u0089\5\26\f\2"+
		"\u0087\u0089\5\30\r\2\u0088\u0086\3\2\2\2\u0088\u0087\3\2\2\2\u0089\35"+
		"\3\2\2\2\u008a\u008b\7$\2\2\u008b\u0090\5 \21\2\u008c\u008d\7\6\2\2\u008d"+
		"\u008f\5\"\22\2\u008e\u008c\3\2\2\2\u008f\u0092\3\2\2\2\u0090\u008e\3"+
		"\2\2\2\u0090\u0091\3\2\2\2\u0091\u0093\3\2\2\2\u0092\u0090\3\2\2\2\u0093"+
		"\u0094\7%\2\2\u0094\37\3\2\2\2\u0095\u0096\t\4\2\2\u0096!\3\2\2\2\u0097"+
		"\u009e\7(\2\2\u0098\u009e\7)\2\2\u0099\u009e\7*\2\2\u009a\u009e\7+\2\2"+
		"\u009b\u009e\5$\23\2\u009c\u009e\7\32\2\2\u009d\u0097\3\2\2\2\u009d\u0098"+
		"\3\2\2\2\u009d\u0099\3\2\2\2\u009d\u009a\3\2\2\2\u009d\u009b\3\2\2\2\u009d"+
		"\u009c\3\2\2\2\u009e#\3\2\2\2\u009f\u00a0\t\5\2\2\u00a0%\3\2\2\2\17\'"+
		"+-<Z`got}\u0088\u0090\u009d";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}