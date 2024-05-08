using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denisenko_KursProject
{
    internal class CodeGenerator
    {
        private string[] Idents = new string[20];
        public static List<string> result = new();
        private List<Lexem> listLexem = new();

        public CodeGenerator(string text)
        {
            result.Clear();

            listLexem.Clear();
            string[] lexems = new string[200];
            lexems = text.Split(new char[] { '\n' });
            foreach (string inp in lexems)
            {
                string[] vals = new string[3];
                vals = inp.Split(new char[] { '\t', '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (vals.Length > 0)
                    listLexem.Add(new Lexem(vals[0], Lexem.GetClassByName(vals[1]), Convert.ToInt32(vals[2])));
            }

            List<Lexem> listLexemRow = new();
            int currentLexem = 0;
            for (int i = 0; i < listLexem[listLexem.Count - 1].GetRow(); i++)
            {

                while (listLexem[currentLexem].GetRow() == i)
                {
                    listLexemRow.Add(listLexem[currentLexem++]); // считываем лексемы из одной строки
                }



                for (int k = 1; k < listLexemRow.Count; k++) // проходим по каждой лексеме в цикле
                {
                    Lexem lex = listLexemRow[k];
                    switch (lex.GetClass()) // через switch выбираем подходящую ветку в зависимости от класса лексемы
                    {
                        case Lexem.ClassLexem.Ident:
                            {
                                if (i > 1)
                                    result.Add($"LOAD {Array.IndexOf(Idents, lex.GetName())} "); // в случае идентификатора (переменной), если это не первая лексема в строке (присваивание) - загружаем переменную из памяти
                                else
                                    Idents.Append<string>(lex.GetName()).ToArray<string>();
                                break;
                            }
                        case Lexem.ClassLexem.Const:
                            {
                                result.Add($"LIT {lex.GetName()} "); // помещаем константу в вершину стека
                                break;
                            }
                        case Lexem.ClassLexem.BinarOp: // для бинарных операторов - в зависимости от знака добавляем значение
                            {
                                if (lex.GetName() == "+")
                                    result.Add("ADD ");
                                else if (lex.GetName() == "-")
                                    result.Add("SUB ");
                                else if (lex.GetName() == "*")
                                    result.Add("MUL ");
                                else if (lex.GetName() == "/")
                                    result.Add("DIV ");
                                break;

                            }
                        case Lexem.ClassLexem.BoolOp:
                            {
                                if (lex.GetName() == "<")
                                    result.Add("JGT k ");
                                else if (lex.GetName() == ">")
                                    result.Add("JLT k ");
                                else if (lex.GetName() == "<=")
                                    result.Add("JGE k ");
                                else if (lex.GetName() == ">=")
                                    result.Add("JLE k ");
                                else if (lex.GetName() == "=")
                                    result.Add("JEQ k ");
                                break;
                            }
                        case Lexem.ClassLexem.UnarOp:
                            {
                                result.Add("NOT "); // для унарных операторов - инвертируем значение в вершине стека
                                break;
                            }
                        case Lexem.ClassLexem.Assign:
                            {
                                result.Add($"STO {Array.IndexOf(Idents, listLexemRow[0].GetName())} "); // присваиваем значение к переменной из начала строки кода
                                break;
                            }
                        case Lexem.ClassLexem.Keyword:
                            {
                                if (lex.GetName() == "if")
                                {

                                }
                                break;
                            }
                    }
                }

                listLexemRow.Clear();
            }
        }
    }
}
