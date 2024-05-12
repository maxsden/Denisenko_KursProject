using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denisenko_KursProject
{
    internal class CodeGenerator
    {
        private List<string> Idents = new List<string>();
        public List<string> result = new();
        private List<Lexem> listLexem = new();
        private int current_if, current_else, current_end, current_while, current_end_while, before_while;
        private bool if_flag, else_flag, end_flag, while_flag = false;

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
            for (int i = 0; i <= listLexem[listLexem.Count - 1].GetRow(); i++)
            {

                while ((currentLexem < listLexem.Count) && (listLexem[currentLexem].GetRow() == i))
                {
                    listLexemRow.Add(listLexem[currentLexem++]); // считываем лексемы из одной строки
                }



                for (int k = 0; k < listLexemRow.Count; k++) // проходим по каждой лексеме в цикле
                {
                    Lexem lex = listLexemRow[k];
                    switch (lex.GetClass()) // через switch выбираем подходящую ветку в зависимости от класса лексемы
                    {
                        case Lexem.ClassLexem.Ident:
                            {
                                if (i > 1 && k > 0)
                                    result.Add($"LOAD {Idents.IndexOf(lex.GetName())} "); // в случае идентификатора (переменной), если это не первая лексема в строке (присваивание) - загружаем переменную из памяти
                                else if (k > 0)
                                    Idents.Add(lex.GetName());
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
                                if (if_flag)
                                {
                                    if (lex.GetName() == "<")
                                    {
                                        result.Add("JGT if");
                                        current_if = result.Count - 1;
                                        result.Add("JMP else");
                                        current_else = result.Count - 1;
                                    }
                                    else if (lex.GetName() == ">")
                                    {
                                        result.Add("JLT if");
                                        current_if = result.Count - 1;
                                        result.Add("JMP else");
                                        current_else = result.Count - 1;
                                    }
                                    else if (lex.GetName() == "<=")
                                    {
                                        result.Add("JGE if");
                                        current_if = result.Count - 1;
                                        result.Add("JMP else");
                                        current_else = result.Count - 1;
                                    }
                                    else if (lex.GetName() == ">=")
                                    {
                                        result.Add("JLE if");
                                        current_if = result.Count - 1;
                                        result.Add("JMP else");
                                        current_else = result.Count - 1;
                                    }
                                    else if (lex.GetName() == "=")
                                    {
                                        result.Add("JEQ if");
                                        current_if = result.Count - 1;
                                        result.Add("JMP else");
                                        current_else = result.Count - 1;
                                    }
                                }
                                else if (while_flag)
                                {
                                    if (lex.GetName() == "<")
                                    {
                                        result.Add("JGT while");
                                        current_while = result.Count - 1;
                                        result.Add("JMP endwhile");
                                        current_end_while = result.Count - 1;
                                    }
                                    else if (lex.GetName() == ">")
                                    {
                                        result.Add("JLT while");
                                        current_while = result.Count - 1;
                                        result.Add("JMP endwhile");
                                        current_end_while = result.Count - 1;
                                    }
                                    else if (lex.GetName() == "<=")
                                    {
                                        result.Add("JGE while");
                                        current_while = result.Count - 1;
                                        result.Add("JMP endwhile");
                                        current_end_while = result.Count - 1;
                                    }
                                    else if (lex.GetName() == ">=")
                                    {
                                        result.Add("JLE while");
                                        current_while = result.Count - 1;
                                        result.Add("JMP endwhile");
                                        current_end_while = result.Count - 1;
                                    }
                                    else if (lex.GetName() == "=")
                                    {
                                        result.Add("JEQ while");
                                        current_while = result.Count - 1;
                                        result.Add("JMP endwhile");
                                        current_end_while = result.Count - 1;
                                    }
                                }
                                break;
                            }
                        case Lexem.ClassLexem.UnarOp:
                            {
                                result.Add("NOT "); // для унарных операторов - инвертируем значение в вершине стека
                                break;
                            }
                        case Lexem.ClassLexem.Assign:
                            {
                                result.Add($"STO {Idents.IndexOf(listLexemRow[0].GetName())} "); // присваиваем значение к переменной из начала строки кода
                                break;
                            }
                        case Lexem.ClassLexem.Keyword:
                            {
                                if (lex.GetName().ToLower() == "end")
                                {
                                    result.Add("NOP");
                                }
                                else if (lex.GetName().ToLower() == "if")
                                {
                                    if_flag = true;
                                }
                                else if (lex.GetName().ToLower() == "else")
                                {
                                    else_flag = true;
                                }
                                else if (lex.GetName().ToLower() == "endif")
                                {
                                    result[current_end] = result[current_end].Replace("endif", result.Count.ToString());
                                    if (!else_flag)
                                    {
                                        result[current_else] = result[current_else].Replace("else", result.Count.ToString());
                                    }
                                    else_flag = false;
                                    if_flag = false;
                                    end_flag = false;
                                }
                                else if (lex.GetName().ToLower() == "while")
                                {
                                    while_flag= true;
                                    before_while = result.Count;
                                }
                                else if (lex.GetName().ToLower() == "endwhile")
                                {
                                    result[current_end_while] = result[current_end_while].Replace("endwhile", result.Count.ToString());
                                    while_flag= false;
                                }
                                break;
                            }
                        case Lexem.ClassLexem.Service:
                            {
                                if (lex.GetName() == "{")
                                {
                                    if (if_flag)
                                    {
                                        if (current_if != 0)
                                        {
                                            result[current_if] = result[current_if].Replace("if", result.Count.ToString());
                                        }
                                    }
                                    else if (else_flag)
                                    {
                                        if (current_else != 0)
                                        {
                                            result[current_else] = result[current_else].Replace("else", result.Count.ToString());
                                        }
                                    }
                                    
                                    if (while_flag)
                                    {
                                        if (current_while != 0)
                                        {
                                            result[current_while] = result[current_while].Replace("while", result.Count.ToString());
                                        }
                                    }
                                }
                                else if (lex.GetName() == "}")
                                {
                                    if (if_flag)
                                    {
                                        if_flag = false;
                                        end_flag = true;
                                        current_end = result.Count;
                                        result.Add("JMP endif");
                                    }
                                    else if (else_flag)
                                    {
                                        //else_flag = false;
                                        end_flag = false;
                                    }

                                    if (while_flag)
                                    {
                                        result.Add($"JMP {before_while.ToString()}");
                                    }
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
