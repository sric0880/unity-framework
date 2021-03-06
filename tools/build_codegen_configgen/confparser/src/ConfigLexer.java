// Generated from /Users/qiong/Documents/myProject/unity-framework/tools/build_codegen_configgen/confparser/src/Config.g4 by ANTLR 4.7
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class ConfigLexer extends Lexer {
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
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "T__23", "T__24", 
		"T__25", "DICT", "ENUM", "NAMESPACE", "STRUCT", "DATA", "LBRACE", "RBRACE", 
		"LBRACK", "RBRACK", "SEMICOLON", "ID", "Letter", "Digit", "INT", "FloatingPointLiteral", 
		"Exponent", "FloatTypeSuffix", "CharacterLiteral", "StringLiteral", "EscapeSequence", 
		"OctalEscape", "HexDigit", "UnicodeEscape", "WS", "NEWLINE", "LINE_COMMENT", 
		"COMMENT"
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


	public ConfigLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Config.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2/\u01dc\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\3\2\3\2\3\3\3\3\3\4\3\4\3\5\3\5\3\6\3\6\3\7\3"+
		"\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\3\t\3\t\3\n\3\n\3\n"+
		"\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\13\3\f\3\f\3\f\3\f\3\f\3\r\3\r\3\r\3"+
		"\r\3\r\3\16\3\16\3\16\3\16\3\16\3\16\3\17\3\17\3\17\3\17\3\17\3\17\3\20"+
		"\3\20\3\20\3\20\3\20\3\20\3\20\3\21\3\21\3\21\3\21\3\21\3\21\3\21\3\22"+
		"\3\22\3\22\3\22\3\22\3\22\3\22\3\22\3\22\3\23\3\23\3\23\3\23\3\23\3\23"+
		"\3\24\3\24\3\24\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3\26\3\26\3\26"+
		"\3\26\3\26\3\26\3\26\3\27\3\27\3\27\3\27\3\30\3\30\3\30\3\30\3\31\3\31"+
		"\3\31\3\31\3\31\3\32\3\32\3\32\3\32\3\32\3\33\3\33\3\33\3\33\3\33\3\33"+
		"\3\34\3\34\3\34\3\34\3\34\3\35\3\35\3\35\3\35\3\35\3\36\3\36\3\36\3\36"+
		"\3\36\3\36\3\36\3\36\3\36\3\36\3\37\3\37\3\37\3\37\3\37\3\37\3\37\3 \3"+
		" \3 \3 \3 \3!\3!\3\"\3\"\3#\3#\3$\3$\3%\3%\3&\3&\3&\7&\u011d\n&\f&\16"+
		"&\u0120\13&\3\'\3\'\3(\3(\3)\6)\u0127\n)\r)\16)\u0128\3*\6*\u012c\n*\r"+
		"*\16*\u012d\3*\3*\7*\u0132\n*\f*\16*\u0135\13*\3*\5*\u0138\n*\3*\5*\u013b"+
		"\n*\3*\3*\6*\u013f\n*\r*\16*\u0140\3*\5*\u0144\n*\3*\5*\u0147\n*\3*\6"+
		"*\u014a\n*\r*\16*\u014b\3*\3*\5*\u0150\n*\3*\6*\u0153\n*\r*\16*\u0154"+
		"\3*\3*\3*\3*\3*\5*\u015c\n*\3*\7*\u015f\n*\f*\16*\u0162\13*\3*\3*\7*\u0166"+
		"\n*\f*\16*\u0169\13*\5*\u016b\n*\3*\3*\5*\u016f\n*\3*\6*\u0172\n*\r*\16"+
		"*\u0173\3*\5*\u0177\n*\5*\u0179\n*\3+\3+\5+\u017d\n+\3+\6+\u0180\n+\r"+
		"+\16+\u0181\3,\3,\3-\3-\3-\5-\u0189\n-\3-\3-\3.\3.\3.\7.\u0190\n.\f.\16"+
		".\u0193\13.\3.\3.\3/\3/\3/\3/\5/\u019b\n/\3\60\3\60\3\60\3\60\3\60\3\60"+
		"\3\60\3\60\3\60\5\60\u01a6\n\60\3\61\3\61\3\62\3\62\3\62\3\62\3\62\3\62"+
		"\3\62\3\63\6\63\u01b2\n\63\r\63\16\63\u01b3\3\63\3\63\3\64\5\64\u01b9"+
		"\n\64\3\64\3\64\3\64\3\64\3\65\3\65\3\65\3\65\7\65\u01c3\n\65\f\65\16"+
		"\65\u01c6\13\65\3\65\5\65\u01c9\n\65\3\65\3\65\3\65\3\65\3\66\3\66\3\66"+
		"\3\66\7\66\u01d3\n\66\f\66\16\66\u01d6\13\66\3\66\3\66\3\66\3\66\3\66"+
		"\4\u01c4\u01d4\2\67\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r"+
		"\31\16\33\17\35\20\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\61\32\63\33"+
		"\65\34\67\359\36;\37= ?!A\"C#E$G%I&K\'M\2O\2Q(S)U\2W\2Y*[+]\2_\2a\2c\2"+
		"e,g-i.k/\3\2\16\16\2&&C\\aac|\u00c2\u00d8\u00da\u00f8\u00fa\u2001\u3042"+
		"\u3191\u3302\u3381\u3402\u3d2f\u4e02\ua001\uf902\ufb01\21\2\62;\u0662"+
		"\u066b\u06f2\u06fb\u0968\u0971\u09e8\u09f1\u0a68\u0a71\u0ae8\u0af1\u0b68"+
		"\u0b71\u0be9\u0bf1\u0c68\u0c71\u0ce8\u0cf1\u0d68\u0d71\u0e52\u0e5b\u0ed2"+
		"\u0edb\u1042\u104b\3\2\62;\4\2RRrr\4\2--//\4\2GGgg\6\2FFHHffhh\4\2))^"+
		"^\4\2$$^^\n\2$$))^^ddhhppttvv\5\2\62;CHch\4\2\13\13\"\"\2\u01f9\2\3\3"+
		"\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2"+
		"\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3"+
		"\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2"+
		"%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2\2\2\61"+
		"\3\2\2\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2\2\2\2;\3\2\2\2"+
		"\2=\3\2\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2\2G\3\2\2\2\2I"+
		"\3\2\2\2\2K\3\2\2\2\2Q\3\2\2\2\2S\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\2e\3\2"+
		"\2\2\2g\3\2\2\2\2i\3\2\2\2\2k\3\2\2\2\3m\3\2\2\2\5o\3\2\2\2\7q\3\2\2\2"+
		"\ts\3\2\2\2\13u\3\2\2\2\rw\3\2\2\2\17|\3\2\2\2\21\u0081\3\2\2\2\23\u0087"+
		"\3\2\2\2\25\u008e\3\2\2\2\27\u0092\3\2\2\2\31\u0097\3\2\2\2\33\u009c\3"+
		"\2\2\2\35\u00a2\3\2\2\2\37\u00a8\3\2\2\2!\u00af\3\2\2\2#\u00b6\3\2\2\2"+
		"%\u00bf\3\2\2\2\'\u00c5\3\2\2\2)\u00c8\3\2\2\2+\u00d0\3\2\2\2-\u00d7\3"+
		"\2\2\2/\u00db\3\2\2\2\61\u00df\3\2\2\2\63\u00e4\3\2\2\2\65\u00e9\3\2\2"+
		"\2\67\u00ef\3\2\2\29\u00f4\3\2\2\2;\u00f9\3\2\2\2=\u0103\3\2\2\2?\u010a"+
		"\3\2\2\2A\u010f\3\2\2\2C\u0111\3\2\2\2E\u0113\3\2\2\2G\u0115\3\2\2\2I"+
		"\u0117\3\2\2\2K\u0119\3\2\2\2M\u0121\3\2\2\2O\u0123\3\2\2\2Q\u0126\3\2"+
		"\2\2S\u0178\3\2\2\2U\u017a\3\2\2\2W\u0183\3\2\2\2Y\u0185\3\2\2\2[\u018c"+
		"\3\2\2\2]\u019a\3\2\2\2_\u01a5\3\2\2\2a\u01a7\3\2\2\2c\u01a9\3\2\2\2e"+
		"\u01b1\3\2\2\2g\u01b8\3\2\2\2i\u01be\3\2\2\2k\u01ce\3\2\2\2mn\7\60\2\2"+
		"n\4\3\2\2\2op\7?\2\2p\6\3\2\2\2qr\7>\2\2r\b\3\2\2\2st\7.\2\2t\n\3\2\2"+
		"\2uv\7@\2\2v\f\3\2\2\2wx\7d\2\2xy\7q\2\2yz\7q\2\2z{\7n\2\2{\16\3\2\2\2"+
		"|}\7d\2\2}~\7{\2\2~\177\7v\2\2\177\u0080\7g\2\2\u0080\20\3\2\2\2\u0081"+
		"\u0082\7u\2\2\u0082\u0083\7j\2\2\u0083\u0084\7q\2\2\u0084\u0085\7t\2\2"+
		"\u0085\u0086\7v\2\2\u0086\22\3\2\2\2\u0087\u0088\7w\2\2\u0088\u0089\7"+
		"u\2\2\u0089\u008a\7j\2\2\u008a\u008b\7q\2\2\u008b\u008c\7t\2\2\u008c\u008d"+
		"\7v\2\2\u008d\24\3\2\2\2\u008e\u008f\7k\2\2\u008f\u0090\7p\2\2\u0090\u0091"+
		"\7v\2\2\u0091\26\3\2\2\2\u0092\u0093\7w\2\2\u0093\u0094\7k\2\2\u0094\u0095"+
		"\7p\2\2\u0095\u0096\7v\2\2\u0096\30\3\2\2\2\u0097\u0098\7n\2\2\u0098\u0099"+
		"\7q\2\2\u0099\u009a\7p\2\2\u009a\u009b\7i\2\2\u009b\32\3\2\2\2\u009c\u009d"+
		"\7w\2\2\u009d\u009e\7n\2\2\u009e\u009f\7q\2\2\u009f\u00a0\7p\2\2\u00a0"+
		"\u00a1\7i\2\2\u00a1\34\3\2\2\2\u00a2\u00a3\7h\2\2\u00a3\u00a4\7n\2\2\u00a4"+
		"\u00a5\7q\2\2\u00a5\u00a6\7c\2\2\u00a6\u00a7\7v\2\2\u00a7\36\3\2\2\2\u00a8"+
		"\u00a9\7f\2\2\u00a9\u00aa\7q\2\2\u00aa\u00ab\7w\2\2\u00ab\u00ac\7d\2\2"+
		"\u00ac\u00ad\7n\2\2\u00ad\u00ae\7g\2\2\u00ae \3\2\2\2\u00af\u00b0\7u\2"+
		"\2\u00b0\u00b1\7v\2\2\u00b1\u00b2\7t\2\2\u00b2\u00b3\7k\2\2\u00b3\u00b4"+
		"\7p\2\2\u00b4\u00b5\7i\2\2\u00b5\"\3\2\2\2\u00b6\u00b7\7Z\2\2\u00b7\u00b8"+
		"\7n\2\2\u00b8\u00b9\7u\2\2\u00b9\u00ba\7z\2\2\u00ba\u00bb\7P\2\2\u00bb"+
		"\u00bc\7c\2\2\u00bc\u00bd\7o\2\2\u00bd\u00be\7g\2\2\u00be$\3\2\2\2\u00bf"+
		"\u00c0\7T\2\2\u00c0\u00c1\7g\2\2\u00c1\u00c2\7h\2\2\u00c2\u00c3\7K\2\2"+
		"\u00c3\u00c4\7F\2\2\u00c4&\3\2\2\2\u00c5\u00c6\7K\2\2\u00c6\u00c7\7F\2"+
		"\2\u00c7(\3\2\2\2\u00c8\u00c9\7T\2\2\u00c9\u00ca\7g\2\2\u00ca\u00cb\7"+
		"s\2\2\u00cb\u00cc\7w\2\2\u00cc\u00cd\7k\2\2\u00cd\u00ce\7t\2\2\u00ce\u00cf"+
		"\7g\2\2\u00cf*\3\2\2\2\u00d0\u00d1\7N\2\2\u00d1\u00d2\7q\2\2\u00d2\u00d3"+
		"\7e\2\2\u00d3\u00d4\7c\2\2\u00d4\u00d5\7n\2\2\u00d5\u00d6\7g\2\2\u00d6"+
		",\3\2\2\2\u00d7\u00d8\7O\2\2\u00d8\u00d9\7k\2\2\u00d9\u00da\7p\2\2\u00da"+
		".\3\2\2\2\u00db\u00dc\7O\2\2\u00dc\u00dd\7c\2\2\u00dd\u00de\7z\2\2\u00de"+
		"\60\3\2\2\2\u00df\u00e0\7p\2\2\u00e0\u00e1\7w\2\2\u00e1\u00e2\7n\2\2\u00e2"+
		"\u00e3\7n\2\2\u00e3\62\3\2\2\2\u00e4\u00e5\7v\2\2\u00e5\u00e6\7t\2\2\u00e6"+
		"\u00e7\7w\2\2\u00e7\u00e8\7g\2\2\u00e8\64\3\2\2\2\u00e9\u00ea\7h\2\2\u00ea"+
		"\u00eb\7c\2\2\u00eb\u00ec\7n\2\2\u00ec\u00ed\7u\2\2\u00ed\u00ee\7g\2\2"+
		"\u00ee\66\3\2\2\2\u00ef\u00f0\7f\2\2\u00f0\u00f1\7k\2\2\u00f1\u00f2\7"+
		"e\2\2\u00f2\u00f3\7v\2\2\u00f38\3\2\2\2\u00f4\u00f5\7g\2\2\u00f5\u00f6"+
		"\7p\2\2\u00f6\u00f7\7w\2\2\u00f7\u00f8\7o\2\2\u00f8:\3\2\2\2\u00f9\u00fa"+
		"\7p\2\2\u00fa\u00fb\7c\2\2\u00fb\u00fc\7o\2\2\u00fc\u00fd\7g\2\2\u00fd"+
		"\u00fe\7u\2\2\u00fe\u00ff\7r\2\2\u00ff\u0100\7c\2\2\u0100\u0101\7e\2\2"+
		"\u0101\u0102\7g\2\2\u0102<\3\2\2\2\u0103\u0104\7u\2\2\u0104\u0105\7v\2"+
		"\2\u0105\u0106\7t\2\2\u0106\u0107\7w\2\2\u0107\u0108\7e\2\2\u0108\u0109"+
		"\7v\2\2\u0109>\3\2\2\2\u010a\u010b\7f\2\2\u010b\u010c\7c\2\2\u010c\u010d"+
		"\7v\2\2\u010d\u010e\7c\2\2\u010e@\3\2\2\2\u010f\u0110\7}\2\2\u0110B\3"+
		"\2\2\2\u0111\u0112\7\177\2\2\u0112D\3\2\2\2\u0113\u0114\7]\2\2\u0114F"+
		"\3\2\2\2\u0115\u0116\7_\2\2\u0116H\3\2\2\2\u0117\u0118\7=\2\2\u0118J\3"+
		"\2\2\2\u0119\u011e\5M\'\2\u011a\u011d\5M\'\2\u011b\u011d\5O(\2\u011c\u011a"+
		"\3\2\2\2\u011c\u011b\3\2\2\2\u011d\u0120\3\2\2\2\u011e\u011c\3\2\2\2\u011e"+
		"\u011f\3\2\2\2\u011fL\3\2\2\2\u0120\u011e\3\2\2\2\u0121\u0122\t\2\2\2"+
		"\u0122N\3\2\2\2\u0123\u0124\t\3\2\2\u0124P\3\2\2\2\u0125\u0127\t\4\2\2"+
		"\u0126\u0125\3\2\2\2\u0127\u0128\3\2\2\2\u0128\u0126\3\2\2\2\u0128\u0129"+
		"\3\2\2\2\u0129R\3\2\2\2\u012a\u012c\4\62;\2\u012b\u012a\3\2\2\2\u012c"+
		"\u012d\3\2\2\2\u012d\u012b\3\2\2\2\u012d\u012e\3\2\2\2\u012e\u012f\3\2"+
		"\2\2\u012f\u0133\7\60\2\2\u0130\u0132\4\62;\2\u0131\u0130\3\2\2\2\u0132"+
		"\u0135\3\2\2\2\u0133\u0131\3\2\2\2\u0133\u0134\3\2\2\2\u0134\u0137\3\2"+
		"\2\2\u0135\u0133\3\2\2\2\u0136\u0138\5U+\2\u0137\u0136\3\2\2\2\u0137\u0138"+
		"\3\2\2\2\u0138\u013a\3\2\2\2\u0139\u013b\5W,\2\u013a\u0139\3\2\2\2\u013a"+
		"\u013b\3\2\2\2\u013b\u0179\3\2\2\2\u013c\u013e\7\60\2\2\u013d\u013f\4"+
		"\62;\2\u013e\u013d\3\2\2\2\u013f\u0140\3\2\2\2\u0140\u013e\3\2\2\2\u0140"+
		"\u0141\3\2\2\2\u0141\u0143\3\2\2\2\u0142\u0144\5U+\2\u0143\u0142\3\2\2"+
		"\2\u0143\u0144\3\2\2\2\u0144\u0146\3\2\2\2\u0145\u0147\5W,\2\u0146\u0145"+
		"\3\2\2\2\u0146\u0147\3\2\2\2\u0147\u0179\3\2\2\2\u0148\u014a\4\62;\2\u0149"+
		"\u0148\3\2\2\2\u014a\u014b\3\2\2\2\u014b\u0149\3\2\2\2\u014b\u014c\3\2"+
		"\2\2\u014c\u014d\3\2\2\2\u014d\u014f\5U+\2\u014e\u0150\5W,\2\u014f\u014e"+
		"\3\2\2\2\u014f\u0150\3\2\2\2\u0150\u0179\3\2\2\2\u0151\u0153\4\62;\2\u0152"+
		"\u0151\3\2\2\2\u0153\u0154\3\2\2\2\u0154\u0152\3\2\2\2\u0154\u0155\3\2"+
		"\2\2\u0155\u0156\3\2\2\2\u0156\u0179\5W,\2\u0157\u0158\7\62\2\2\u0158"+
		"\u015c\7z\2\2\u0159\u015a\7\62\2\2\u015a\u015c\7Z\2\2\u015b\u0157\3\2"+
		"\2\2\u015b\u0159\3\2\2\2\u015c\u0160\3\2\2\2\u015d\u015f\5a\61\2\u015e"+
		"\u015d\3\2\2\2\u015f\u0162\3\2\2\2\u0160\u015e\3\2\2\2\u0160\u0161\3\2"+
		"\2\2\u0161\u016a\3\2\2\2\u0162\u0160\3\2\2\2\u0163\u0167\7\60\2\2\u0164"+
		"\u0166\5a\61\2\u0165\u0164\3\2\2\2\u0166\u0169\3\2\2\2\u0167\u0165\3\2"+
		"\2\2\u0167\u0168\3\2\2\2\u0168\u016b\3\2\2\2\u0169\u0167\3\2\2\2\u016a"+
		"\u0163\3\2\2\2\u016a\u016b\3\2\2\2\u016b\u016c\3\2\2\2\u016c\u016e\t\5"+
		"\2\2\u016d\u016f\t\6\2\2\u016e\u016d\3\2\2\2\u016e\u016f\3\2\2\2\u016f"+
		"\u0171\3\2\2\2\u0170\u0172\4\62;\2\u0171\u0170\3\2\2\2\u0172\u0173\3\2"+
		"\2\2\u0173\u0171\3\2\2\2\u0173\u0174\3\2\2\2\u0174\u0176\3\2\2\2\u0175"+
		"\u0177\5W,\2\u0176\u0175\3\2\2\2\u0176\u0177\3\2\2\2\u0177\u0179\3\2\2"+
		"\2\u0178\u012b\3\2\2\2\u0178\u013c\3\2\2\2\u0178\u0149\3\2\2\2\u0178\u0152"+
		"\3\2\2\2\u0178\u015b\3\2\2\2\u0179T\3\2\2\2\u017a\u017c\t\7\2\2\u017b"+
		"\u017d\t\6\2\2\u017c\u017b\3\2\2\2\u017c\u017d\3\2\2\2\u017d\u017f\3\2"+
		"\2\2\u017e\u0180\4\62;\2\u017f\u017e\3\2\2\2\u0180\u0181\3\2\2\2\u0181"+
		"\u017f\3\2\2\2\u0181\u0182\3\2\2\2\u0182V\3\2\2\2\u0183\u0184\t\b\2\2"+
		"\u0184X\3\2\2\2\u0185\u0188\7)\2\2\u0186\u0189\5]/\2\u0187\u0189\n\t\2"+
		"\2\u0188\u0186\3\2\2\2\u0188\u0187\3\2\2\2\u0189\u018a\3\2\2\2\u018a\u018b"+
		"\7)\2\2\u018bZ\3\2\2\2\u018c\u0191\7$\2\2\u018d\u0190\5]/\2\u018e\u0190"+
		"\n\n\2\2\u018f\u018d\3\2\2\2\u018f\u018e\3\2\2\2\u0190\u0193\3\2\2\2\u0191"+
		"\u018f\3\2\2\2\u0191\u0192\3\2\2\2\u0192\u0194\3\2\2\2\u0193\u0191\3\2"+
		"\2\2\u0194\u0195\7$\2\2\u0195\\\3\2\2\2\u0196\u0197\7^\2\2\u0197\u019b"+
		"\t\13\2\2\u0198\u019b\5c\62\2\u0199\u019b\5_\60\2\u019a\u0196\3\2\2\2"+
		"\u019a\u0198\3\2\2\2\u019a\u0199\3\2\2\2\u019b^\3\2\2\2\u019c\u019d\7"+
		"^\2\2\u019d\u019e\4\62\65\2\u019e\u019f\4\629\2\u019f\u01a6\4\629\2\u01a0"+
		"\u01a1\7^\2\2\u01a1\u01a2\4\629\2\u01a2\u01a6\4\629\2\u01a3\u01a4\7^\2"+
		"\2\u01a4\u01a6\4\629\2\u01a5\u019c\3\2\2\2\u01a5\u01a0\3\2\2\2\u01a5\u01a3"+
		"\3\2\2\2\u01a6`\3\2\2\2\u01a7\u01a8\t\f\2\2\u01a8b\3\2\2\2\u01a9\u01aa"+
		"\7^\2\2\u01aa\u01ab\7w\2\2\u01ab\u01ac\5a\61\2\u01ac\u01ad\5a\61\2\u01ad"+
		"\u01ae\5a\61\2\u01ae\u01af\5a\61\2\u01afd\3\2\2\2\u01b0\u01b2\t\r\2\2"+
		"\u01b1\u01b0\3\2\2\2\u01b2\u01b3\3\2\2\2\u01b3\u01b1\3\2\2\2\u01b3\u01b4"+
		"\3\2\2\2\u01b4\u01b5\3\2\2\2\u01b5\u01b6\b\63\2\2\u01b6f\3\2\2\2\u01b7"+
		"\u01b9\7\17\2\2\u01b8\u01b7\3\2\2\2\u01b8\u01b9\3\2\2\2\u01b9\u01ba\3"+
		"\2\2\2\u01ba\u01bb\7\f\2\2\u01bb\u01bc\3\2\2\2\u01bc\u01bd\b\64\2\2\u01bd"+
		"h\3\2\2\2\u01be\u01bf\7\61\2\2\u01bf\u01c0\7\61\2\2\u01c0\u01c4\3\2\2"+
		"\2\u01c1\u01c3\13\2\2\2\u01c2\u01c1\3\2\2\2\u01c3\u01c6\3\2\2\2\u01c4"+
		"\u01c5\3\2\2\2\u01c4\u01c2\3\2\2\2\u01c5\u01c8\3\2\2\2\u01c6\u01c4\3\2"+
		"\2\2\u01c7\u01c9\7\17\2\2\u01c8\u01c7\3\2\2\2\u01c8\u01c9\3\2\2\2\u01c9"+
		"\u01ca\3\2\2\2\u01ca\u01cb\7\f\2\2\u01cb\u01cc\3\2\2\2\u01cc\u01cd\b\65"+
		"\2\2\u01cdj\3\2\2\2\u01ce\u01cf\7\61\2\2\u01cf\u01d0\7,\2\2\u01d0\u01d4"+
		"\3\2\2\2\u01d1\u01d3\13\2\2\2\u01d2\u01d1\3\2\2\2\u01d3\u01d6\3\2\2\2"+
		"\u01d4\u01d5\3\2\2\2\u01d4\u01d2\3\2\2\2\u01d5\u01d7\3\2\2\2\u01d6\u01d4"+
		"\3\2\2\2\u01d7\u01d8\7,\2\2\u01d8\u01d9\7\61\2\2\u01d9\u01da\3\2\2\2\u01da"+
		"\u01db\b\66\2\2\u01dbl\3\2\2\2$\2\u011c\u011e\u0128\u012d\u0133\u0137"+
		"\u013a\u0140\u0143\u0146\u014b\u014f\u0154\u015b\u0160\u0167\u016a\u016e"+
		"\u0173\u0176\u0178\u017c\u0181\u0188\u018f\u0191\u019a\u01a5\u01b3\u01b8"+
		"\u01c4\u01c8\u01d4\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}