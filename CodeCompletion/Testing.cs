﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PascalABCCompiler.SyntaxTree;
using PascalABCCompiler.Parsers;
using PascalABCCompiler.Errors;
using System.IO;
//using ICSharpCode.SharpDevelop.Dom;

namespace CodeCompletion
{
	//#if (DEBUG)
    public class CodeCompletionTester
    {
    	
    	public static void Test()
    	{
    		TestExpressionExtract();
    		//TestVBNETExpressionExtract();
    	}
    	
    	private static void assert(bool cond)
    	{
    		System.Diagnostics.Debug.Assert(cond);
    	}
    	
    	private static void TestVBNETExpressionExtract()
    	{
    		string s;
    		int off=0;
    		int line=0;
    		int col=0;
    		PascalABCCompiler.Parsers.KeywordKind keyw;
    		PascalABCCompiler.Parsers.IParser parser = CodeCompletionController.ParsersController.selectParser(".vb");
    		
    		string test_str = "System.Console";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "System(2+3,-Test34).Console";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "(a+b*c-e)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "If test";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim(' ','\n','\t')=="test");
    		
    		test_str = "test\r\n (abc)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim(' ','\n','\t')=="(abc)");
    	}
    	
    	private static void TestExpressionExtract()
    	{
    		string s;
    		int off=0;
    		int line=0;
    		int col=0;
    		PascalABCCompiler.Parsers.KeywordKind keyw;
            CodeCompletionController.ParsersController.Reload();
    		PascalABCCompiler.Parsers.IParser parser = CodeCompletionController.ParsersController.selectParser(".pas");
    		
    		string test_str = "System.Console";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "System(2+3,-Test34).Console";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "(a+b*c-e)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "a[2,3+b[2-3*b(2*Test-&var,nil)]][56,89,(44)]";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "begin test";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim(' ','\n','\t')=="test");
    		
    		test_str = "begin test[4]";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim(' ','\n','\t')=="test[4]");
    		
    		test_str = "begin sin(2)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim(' ','\n','\t')=="sin(2)");
    		
    		test_str = "repeat test";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim(' ','\n','\t')=="test");
    		
    		test_str = "while test";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim(' ','\n','\t')=="test");
    		
    		test_str = "Test(new Text2, new Text3(2,3),inherited Create).a[new Text3]";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "Test[new Text2, new Text3(2,3),inherited Create].a[new Text3]";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "Str(a:2)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "Str(a:2:5)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "&Begin.&Var.&else.&for";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "a[s<5]";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "a[s>5]";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "a(s<5)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "a(s>5)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "ab{2 hsdjsdh 'ddd'}";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "TClass&<integer>.abc";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "TClass&<integer>";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "Test[new {dsdsdsd} Text2, new Text3{sdsd}(2,{''''''} 3),inherited {zez snj }Create]{...}.a{^^}[new Text3]";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "begin \n \t\t \n bb     \t";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="bb");
    		
    		test_str = "Test\n[new {dsdsdsd\n} Text2, new \t Text3(2,{''''''} 3),inherited {zez snj }Create]{...}.a{^^}[new Text3]";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "test//abcd\ntest.abcd";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="test.abcd");
    		
    		test_str = "test(2,3) < ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "test(2,3) + ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "test(2,3) * ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "test(2,3) div ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "test(2,3) - ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "test(2,3) shl ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "(s as string)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s==test_str);
    		
    		test_str = "begin (s as string)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="(s as string)");
    		
    		test_str = "test(2,3) shr ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "test(2,3) > ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "test(2,3) or ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "test(2,3) and ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "test(2,3) xor ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "test(2,3) mod ppp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')=="ppp");
    		
    		test_str = "typeof(char)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')==test_str);
    		
    		test_str = "sizeof(char)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpression(off,test_str,line,col,out keyw);
    		assert(s.Trim('\n',' ','\t')==test_str);
    		
    		int num_param = 0;
    		//testirovanie nazhatija skobki
    		test_str = "writeln";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "Console.WriteLine";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "begin writeln";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s.Trim('\n',' ','\t') == "writeln");
    		assert(num_param==0);
    		
    		test_str = "Console{dsdsdsd}.WriteLine{''''''sds}";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "test(2,3).mmm";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "test[2<3].a";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "test[2>3].a";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "test[2>3] .a";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "23 < test";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s.Trim('\n',' ','\t') == "test");
    		assert(num_param==0);
    		
    		test_str = "23 > test";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s.Trim('\n',' ','\t') == "test");
    		assert(num_param==0);
    		
    		test_str = "(a<b) + test";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s.Trim('\n',' ','\t') == "test");
    		assert(num_param==0);
    		
    		test_str = "test2[sin(2,3),cos(2)]";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		
    		test_str = "sin{sdsd}(2,3).ttt";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "new System.Collections.Generic.List<integer>";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "TClass&<T>.bbb";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "test; \n  sin";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "sin");
    		assert(num_param==0);
    		
    		test_str = "new TClass";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "Proc1({}2+3,'aasd',Proc2(a[34,{}2],new TClass(f('ssd')))).zzz";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "'abc'.tostring('aa','bb{()').ggg";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "Test([],[1,2,3],{cdsds} \tsin";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "{cdsds} \tsin");
    		assert(num_param==0);
    		
    		test_str = "test(2+3,aa.bb";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == "aa.bb");
    		
    		test_str = "test(2<3).bb";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "sin(new TClass<integer>,TClass&<real>).pp";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		assert(num_param==0);
    		
    		test_str = "raise new TClass";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "new TClass");
    		assert(num_param==0);
    		
    		test_str = "begin test(2)";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "test(2)");
    		assert(num_param==0);
    		
    		test_str = "//aaaa\nConsole.WriteLine";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "Console.WriteLine");
    		assert(num_param==0);
    		
    		test_str = "&var.&uses.&procedure";
    		off = test_str.Length;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,'(',ref num_param);
    		assert(s == test_str);
    		
    		//testirovanie nazhatija zapjatoj
    		test_str = ";test(3,aa.bb";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s == "test");
    		assert(num_param == 3);
    		
    		test_str = ";test(a[2,3,4],sin(2+3),aa.bb";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s == "test");
    		assert(num_param == 4);
    		
    		test_str = "Console.WriteLine(a[2,3,4]{ds},[1,2+3,5,'aa)'],sin(2+3,4+f(3)),aa.bb,(23)";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s == "Console.WriteLine");
    		assert(num_param == 6);
    		
    		test_str = "Console.WriteLine(new TClass(2,3";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s == "new TClass");
    		assert(num_param == 3);
    		
    		test_str = "Console.WriteLine(new{} TClass(2+f(2,[]),3";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s == "new{} TClass");
    		assert(num_param == 3);
    		
    		test_str = "begin \n\t sin(";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "sin");
    		assert(num_param == 2);
    		
    		test_str = "sin(2)";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "");
    		
    		test_str = "begin f(2)+sin(2,3,4)";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "");
    		
    		test_str = ";\n [sin(f(3)),a[3]";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "");
    		
    		test_str = ";\n (sin)";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "");
    		
    		test_str = "max(2<3";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "max");
    		assert(num_param == 2);
    		
    		test_str = "max(2>3";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "max");
    		assert(num_param == 2);
    		
    		test_str = "max(Test&<integer>(2,3)";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "max");
    		assert(num_param == 2);
    		
    		test_str = "new TextABC(60,110,110,'Hello!',RGB(x";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "RGB");
    		assert(num_param == 2);
    		
    		test_str = "new TextABC(60,110,110,'Hello!',new RGB(x";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "new RGB");
    		assert(num_param == 2);
    		
    		test_str = "new TextABC(new RGB(x";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "new RGB");
    		assert(num_param == 2);
    		
    		test_str = "new TextABC(new RGB(x,(2)";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "new RGB");
    		assert(num_param == 3);
    		
    		test_str = "new TextABC(new RGB(x(2)";
    		off = test_str.Length;
    		num_param = 1;
    		s = parser.LanguageInformation.FindExpressionForMethod(off,test_str,line,col,',',ref num_param);
    		assert(s.Trim(' ','\n','\t') == "new RGB");
    		assert(num_param == 2);
    		
    		string str = null;
    		//mouse hover
    		test_str = "sin(2)";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')=="sin(2)");
    		
    		test_str = "sin(2,3,4)";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')=="sin(2,3,4)");
    		
    		test_str = "sin {sdsd'} (2,3,4)";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')==test_str);
    		
    		test_str = "sin (df,'3)+2','{')";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')==test_str);
    		
    		test_str = "cos()";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')==test_str);
    		
    		test_str = "cos{()}";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')=="cos");
    		
    		test_str = "cos//()";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')=="cos");
    		
    		test_str = "cos(//)";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')=="cos");
    		
    		test_str = "cos('//')";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')==test_str);
    		
    		test_str = "cos({//})";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')==test_str);
    		
    		test_str = "cos(Math.Cos(x)+1)";
    		off = 1;
    		s = parser.LanguageInformation.FindExpressionFromAnyPosition(off,test_str,line,col,out keyw,out str);
    		assert(s.Trim('\n',' ','\t')==test_str);

            //----
            Type[] types = typeof(int).Assembly.GetExportedTypes();
            int i = 0;
            int j = 0;
            StreamWriter sw = new StreamWriter("mscorlib.txt");
            foreach (Type t in types)
            {
                sw.WriteLine(parser.LanguageInformation.GetCompiledTypeRepresentation(t, t, ref i, ref j));
            }
            
            sw.Close();
            types = typeof(System.Diagnostics.Process).Assembly.GetExportedTypes();
            sw = new StreamWriter(typeof(System.Diagnostics.Process).Assembly.ManifestModule.ScopeName+".txt");
            foreach (Type t in types)
            {
                sw.WriteLine(parser.LanguageInformation.GetCompiledTypeRepresentation(t, t, ref i, ref j));
            }

            sw.Close();

            types = typeof(System.Data.Constraint).Assembly.GetExportedTypes();
            sw = new StreamWriter(typeof(System.Data.Constraint).Assembly.ManifestModule.ScopeName + ".txt");
            foreach (Type t in types)
            {
                sw.WriteLine(parser.LanguageInformation.GetCompiledTypeRepresentation(t, t, ref i, ref j));
            }

            sw.Close();

            types = typeof(System.Xml.XmlDocument).Assembly.GetExportedTypes();
            sw = new StreamWriter(typeof(System.Xml.XmlDocument).Assembly.ManifestModule.ScopeName + ".txt");
            foreach (Type t in types)
            {
                sw.WriteLine(parser.LanguageInformation.GetCompiledTypeRepresentation(t, t, ref i, ref j));
            }

            sw.Close();
    	}
    }

    public class FormatterTester
    {
        private static string GetTestSuiteDir()
        {
            var dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            var ind = dir.LastIndexOf("bin");
            return dir.Substring(0, ind) + "TestSuite";
        }

        public static void Test()
        {
            string test_dir = GetTestSuiteDir() + @"\formatter_tests";
            string[] files = Directory.GetFiles(test_dir+@"\input","*.pas");
            StreamWriter log = new StreamWriter(test_dir + @"\output\log.txt", false, Encoding.GetEncoding(1251));
            SyntaxTreeComparer stc = new SyntaxTreeComparer();
            foreach (string s in files)
            {
                string Text = new StreamReader(s,System.Text.Encoding.GetEncoding(1251)).ReadToEnd();
                List<Error> Errors = new List<Error>();
                compilation_unit cu = CodeCompletionController.ParsersController.GetCompilationUnitForFormatter(s, Text, Errors);
                if (Errors.Count == 0)
                {
                    CodeFormatters.CodeFormatter cf = new CodeFormatters.CodeFormatter(2);
                    Text = cf.FormatTree(Text, cu, 1, 1);
                    StreamWriter sw = new StreamWriter(Path.Combine(test_dir + @"\output", Path.GetFileName(s)), false, Encoding.GetEncoding(1251));
                    sw.Write(Text);
                    sw.Close();
                    Errors.Clear();
                    compilation_unit cu2 = CodeCompletionController.ParsersController.GetCompilationUnitForFormatter(Path.Combine(test_dir + @"\output", Path.GetFileName(s)), Text, Errors);
                    if (Errors.Count > 0)
                    {
                        for (int i = 0; i < Errors.Count; i++)
                            log.WriteLine(Errors[i].ToString());
                    }
                    else
                        try
                        {
                            stc.Compare(cu, cu2);
                        }
                        catch (Exception ex)
                        {
                            log.WriteLine("SyntaxTreeNotEquals " + s + Environment.NewLine);
                        }
                }
            }
            files = Directory.GetFiles(test_dir+@"\output", "*.pas");
            foreach (string s in files)
            {
                string Text = new StreamReader(s, System.Text.Encoding.GetEncoding(1251)).ReadToEnd();
                List<Error> Errors = new List<Error>();
                compilation_unit cu = CodeCompletionController.ParsersController.GetCompilationUnitForFormatter(s, Text, Errors);
                CodeFormatters.CodeFormatter cf = new CodeFormatters.CodeFormatter(2);
                string Text2 = cf.FormatTree(Text, cu, 1, 1);
                if (Text != Text2)
                    log.WriteLine("Inavlid formatting of File " + s);
            }
            log.Close();
        }
    }
   // #endif
}
