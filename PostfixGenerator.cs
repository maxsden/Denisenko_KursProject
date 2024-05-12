using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denisenko_KursProject
{
    internal class PostfixGenerator
    {
        private static List<Lexem> output = new List<Lexem>(); //Основной список в постфиксной записи
        private Stack<Lexem> operatorStack = new Stack<Lexem>(); //Стэк для хранения операторов; использование стека обусловлено отличием принципов от списка - FIFO в List и LIFO в Stack
        private static List<Lexem> lexemList = new List<Lexem>(); //Исходные лексемы, затем итоговый список лексем с операторами в постфиксном виде
        private int assignPosition;


        public PostfixGenerator(List<Lexem> lexemList1)
        {
            lexemList = lexemList1.ToList<Lexem>();
            bool flag = false;
            bool flagIfWhileElse = false;
            //string output1 = "";
            int? currentLexem = null;
            Dictionary<string, int> operators = new Dictionary<string, int> //Создание словаря с указанием приоритетов знаков
            {
                { ":=",0 },
                { "+", 1 },
                { "-", 1 },
                { "*", 2 },
                { "/", 2 },
                { ">", 3 },
                { "<", 3 },
                { "=", 3 },
                { ">=",3 },
                { "<=",3 }
            };

            List<Lexem> lexemListRow = new List<Lexem>();

            for (int row = 0; row <= lexemList[lexemList.Count - 1].GetRow(); row++) //Ищем начало выражения в инфиксной форме
            {
                for (int i = 0; i < lexemList.Count; i++)
                {
                    if (lexemList[i].GetRow() == row)
                    {
                        lexemListRow.Add(lexemList[i]);
                        if (currentLexem == null && lexemList[i].GetClass() == Lexem.ClassLexem.Assign)
                            currentLexem = i;
                        else if (currentLexem == null && (lexemList[i].GetName() == "if" || lexemList[i].GetName() == "while" || lexemList[i].GetName() == "else"))
                        {
                            flagIfWhileElse = true;
                            currentLexem = i;
                        }
                    }
                    else if (lexemList[i].GetRow() > row)
                        break;
                }

                for (int i = 0; i < lexemListRow.Count; i++)
                {

                    Lexem c = lexemListRow[i];

                    if (c.GetClass() == Lexem.ClassLexem.Const || (c.GetClass() == Lexem.ClassLexem.Ident && flag))
                    {
                        output.Add(c);
                    }

                    else if (operators.ContainsKey(c.GetName())) //Проверяем, что это бинарный/унарный оператор
                    {
                        if (c.GetClass() == Lexem.ClassLexem.Assign)
                            flag = true;

                        while (operatorStack.Count > 0 && operatorStack.Peek().GetName() != "(" && operators[c.GetName()] <= operators[operatorStack.Peek().GetName()]) //Пока не скобка, добавляем в output
                        {
                            output.Add(operatorStack.Pop());
                        }
                        operatorStack.Push(c);
                    }
                    else if (c.GetName() == "(")// || c.GetName() == "{")
                    {
                        operatorStack.Push(c); //Если встречена скобка, добавляем ее в стек
                    }
                    else if (c.GetName() == ")")
                    {
                        while (operatorStack.Count > 0 && operatorStack.Peek().GetName() != "(")
                        {
                            output.Add(operatorStack.Pop()); //Если встречена закрывающая скобка, переносим всё из стека в список
                        }
                        if (operatorStack.Count > 0 && operatorStack.Peek().GetName() == "(")
                        {
                            operatorStack.Pop(); // Удаляем открывающую скобку из стека
                        }
                    }
                    //else if (c.GetName() == "}")
                    //{
                    //    while (operatorStack.Count > 0 && operatorStack.Peek().GetName() != "}")
                    //    {
                    //        output.Add(operatorStack.Pop()); //Если встречена закрывающая скобка, переносим всё из стека в список
                    //    }
                    //    if (operatorStack.Count > 0 && operatorStack.Peek().GetName() == "}")
                    //    {
                    //        operatorStack.Pop(); // Удаляем открывающую скобку из стека
                    //    }
                    //}
                    else if (c.GetClass() == Lexem.ClassLexem.Assign)
                    {
                        flag = true; //Проверяем, является ли идущее выражение правой частью присваивания
                        operatorStack.Push(c);
                    }
                    else if (c.GetName() == "if" || c.GetName() == "else" || c.GetName() == "while")
                    {
                        flag = true;
                        output.Add(c);
                    }
                    else if (c.GetName() == ";" || (flagIfWhileElse && i == lexemListRow.Count - 1))
                    {
                        flag = false; //Проверяем, дошли ли до конца строки - закончилась ли правая часть присваивания
                        //lexemListRow.Insert(i, lexemListRow[i]);
                        //lexemListRow.RemoveAt(assignPosition);
                        //assignPosition = 0;

                    }

                }

                while (operatorStack.Count > 0)
                {
                    output.Add(operatorStack.Pop()); //После всего выводим операторы из стека в список
                }

                for (int i = 0; i < output.Count; i++)
                {
                    if (lexemList[i].GetName() == ")" || lexemList[i].GetName() == "(")
                        lexemList.Remove(lexemList[i]);
                    if (currentLexem != null && output.Count > 0)
                        lexemList[currentLexem.Value + i] = output[i]; //Добавляем элементы в список лексем

                }
                while ((currentLexem != null && (lexemList[currentLexem.Value + output.Count].GetName() != ";" && lexemList[currentLexem.Value + output.Count].GetName() != "{")))
                {
                    lexemList.RemoveAt(currentLexem.Value + output.Count); //Удаляем лишние лексемы
                }
                flagIfWhileElse = false;
                flag = false;
                currentLexem = null;
                lexemListRow.Clear();

                output.Clear();
                operatorStack.Clear();


            }

        }

        public List<string> GetPostfixLexemList() //Метод для вывода нового списка лексем
        {
            List<string> list = new List<string>();
            foreach (Lexem lex in lexemList)
            {
                list.Add(lex.ToString());
            }


            return list;
        }

        public static List<Lexem> GetOrigPostfixLexemList()
        {
            return lexemList;
        }

    }
}
