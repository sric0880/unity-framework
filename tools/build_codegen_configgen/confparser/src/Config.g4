grammar Config;
@parser::header {
import java.util.*;
}

@parser::members {
Set<String> structNames = new HashSet<String>();
}

config
    :   ns? (struct|enumdecl)* data
        EOF
    ;

ns
    :   NAMESPACE qualifiedName SEMICOLON
    ;

qualifiedName
    :   ID ('.' ID)*
    ;

struct
    :   STRUCT ID
        {
            if (!structNames.contains($ID.text)) {
                structNames.add($ID.text);
            }
        }
        LBRACE
        classBody
        RBRACE
    ;

enumdecl
    :   ENUM ID
        {
            if (!structNames.contains($ID.text)) {
                structNames.add($ID.text);
            }
        }
        LBRACE
        enumBody
        RBRACE
    ;

data
    :   DATA ID
        {
            if (!structNames.contains($ID.text)) {
                structNames.add($ID.text);
            }
        }
        LBRACE
        classBody
        RBRACE
    ;

enumBody
    :   (ID '=' INT SEMICOLON)+
    ;

classBody
    :   (declaration attribute* SEMICOLON)+
    ;

declaration
    :   type ID
    ;

type:   definedType (LBRACK RBRACK)?
    |   primitiveType (LBRACK RBRACK)?
    |   DICT '<' dictKeyType ',' dictValueType '>'
    ;

definedType
    :   ID
        {
            if (!structNames.contains($ID.text)) {
                notifyErrorListeners("undefined type: "+$ID.text);
            }
        }
    ;

primitiveType
    :   'bool'
    |   'byte'
    |   'short'
    |   'ushort'
    |   'int'
    |   'uint'
    |   'long'
    |   'ulong'
    |   'float'
    |   'double'
    |   'string'
    ;

dictKeyType
    :   'int'
    |   'string'
    ;

dictValueType
    :   definedType
    |   primitiveType
    ;

attribute
    :   LBRACK attributeName (',' literal)* RBRACK
    ;

attributeName
    :   'XlsxName'
    |   'RefID'
    |   'ID'
    |   'Require'
    |   'Locale'
    |   'Min'
    |   'Max'
    ;

literal
    :   INT
    |   FloatingPointLiteral
    |   CharacterLiteral
    |   StringLiteral
    |   booleanLiteral
    |   'null'
    ;

booleanLiteral
    :   'true'
    |   'false'
    ;

DICT    :   'dict'  ;
ENUM    :   'enum'  ;
NAMESPACE   :   'namespace' ;
STRUCT  :   'struct'    ;
DATA    :   'data'  ;
LBRACE  :	'{' ;
RBRACE       : '}'  ;
LBRACK:	'[' ;
RBRACK       : ']'  ;
SEMICOLON   :   ';' ;

ID
    :   Letter (Letter|Digit)*
    ;

fragment
Letter
    :  '\u0024' |
       '\u0041'..'\u005a' |
       '\u005f' |
       '\u0061'..'\u007a' |
       '\u00c0'..'\u00d6' |
       '\u00d8'..'\u00f6' |
       '\u00f8'..'\u00ff' |
       '\u0100'..'\u1fff' |
       '\u3040'..'\u318f' |
       '\u3300'..'\u337f' |
       '\u3400'..'\u3d2d' |
       '\u4e00'..'\u9fff' |
       '\uf900'..'\ufaff'
    ;

fragment
Digit
    :  '\u0030'..'\u0039' |
       '\u0660'..'\u0669' |
       '\u06f0'..'\u06f9' |
       '\u0966'..'\u096f' |
       '\u09e6'..'\u09ef' |
       '\u0a66'..'\u0a6f' |
       '\u0ae6'..'\u0aef' |
       '\u0b66'..'\u0b6f' |
       '\u0be7'..'\u0bef' |
       '\u0c66'..'\u0c6f' |
       '\u0ce6'..'\u0cef' |
       '\u0d66'..'\u0d6f' |
       '\u0e50'..'\u0e59' |
       '\u0ed0'..'\u0ed9' |
       '\u1040'..'\u1049'
   ;

INT :   [0-9]+ ;

FloatingPointLiteral
    :   ('0'..'9')+ '.' ('0'..'9')* Exponent? FloatTypeSuffix?
    |   '.' ('0'..'9')+ Exponent? FloatTypeSuffix?
    |   ('0'..'9')+ Exponent FloatTypeSuffix?
    |   ('0'..'9')+ FloatTypeSuffix
    |   ('0x' | '0X') (HexDigit )*
        ('.' (HexDigit)*)?
        ( 'p' | 'P' )
        ( '+' | '-' )?
        ( '0' .. '9' )+
        FloatTypeSuffix?
    ;
fragment
Exponent : ('e'|'E') ('+'|'-')? ('0'..'9')+ ;

fragment
FloatTypeSuffix : ('f'|'F'|'d'|'D') ;

CharacterLiteral
    :   '\'' ( EscapeSequence | ~('\''|'\\') ) '\''
    ;

StringLiteral
    :  '"' ( EscapeSequence | ~('\\'|'"') )* '"'
    ;

fragment
EscapeSequence
    :   '\\' ('b'|'t'|'n'|'f'|'r'|'"'|'\''|'\\')
    |   UnicodeEscape
    |   OctalEscape
    ;

fragment
OctalEscape
    :   '\\' ('0'..'3') ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7')
    ;
fragment
HexDigit : ('0'..'9'|'a'..'f'|'A'..'F') ;

fragment
UnicodeEscape
    :   '\\' 'u' HexDigit HexDigit HexDigit HexDigit
    ;

WS  :   [ \t]+ -> skip ;
NEWLINE :   '\r'? '\n'  -> skip ;
LINE_COMMENT : '//' .*? '\r'? '\n' -> skip ;
COMMENT      : '/*' .*? '*/' -> skip ;
