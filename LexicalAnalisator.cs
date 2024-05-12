using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Denisenko_KursProject
{
    internal class LexicalAnalisator
    {
        private static List<Lexem> LexemList = new List<Lexem>(); //Список лексем
        private int currentRow = 0; //Текущая строка
        private static int RowsCount = 0; //Общее количество строк
        private int currentSymbol = 0; //Текущий считываемый символ
        private string buffer; //Текущая строка
        private string[] input = new string[20]; //Текст анализируемой программы по строкам
        public static string[] Idents = new string[0];
        private string text;
        public List<Error> errorsList = new ();

        public LexicalAnalisator(string text1) //Конструктор
        {
            LexemList.Clear();
            errorsList.Clear();
            string word = "";
            text = text1;
            char symbol;
            GetText();

            while (currentRow != RowsCount)
            {
                symbol = GetNextSymbol(); //Получаем следующий символ
                if (symbol == ' ' || symbol == '\n' || symbol == '\r' || symbol == '\0' || symbol == '\t')
                {
                    if (word != "" && word != "-") LexemAdder(word); //При получении одного из специальных символов отправляем слово на добавление
                    word = "";
                }
                else if (symbol == ';' || symbol == ',' || symbol == '(' || symbol == ')')
                {
                    if (word != "" && word != "-") LexemAdder(word); //При получении знаком препинания отправляем уже имеющееся слово на добавлением
                    if (word == "-") LexemAdder("unar");
                    word = "";
                    LexemAdder(symbol.ToString()); //Добавляем знак препинания в таблицу лексем
                }
                else if (symbol == '-')
                {
                    if (word != "" && (Char.IsDigit(word[word.Length - 1])))//если перед знаком "-" шло число
                    {
                        LexemAdder(word); //добавляем число
                        LexemAdder(symbol.ToString()); //добавляем бинарный оператор
                        word = "";
                    }
                    else if (LexemList.Last<Lexem>().GetName() == ")")
                    {
                        LexemAdder(symbol.ToString());
                    }
                    else if (LexemList.Last<Lexem>().GetClass() == Lexem.ClassLexem.Assign)
                    {
                        LexemAdder("unar");
                    }

                    else
                    {
                        word += symbol; //если это унарный оператор, то продолжаем добавлять числовые символы
                    }
                }

                else if (symbol == '+' || symbol == '*' || symbol == '/') //Для бинарных операторов - добавляем предшествующее слово и оператор в таблицу лексем
                {
                    if (word != "") LexemAdder(word);
                    word = "";
                    LexemAdder(symbol.ToString());
                }
                else if (symbol == '>' || symbol == '<' || symbol == '=')
                {
                    if (word != "") LexemAdder(word);
                    word = "";
                    word += symbol;
                    symbol = GetNextSymbol();
                    while (symbol == ' ' || symbol == '\t')
                    {
                        symbol = GetNextSymbol();
                    }
                    if (symbol == '=') //Проверяем, является ли это оператором присваивания, если да - добавляем
                    {
                        word += symbol;
                        LexemAdder(word);
                        word = "";
                    }
                    else
                    {
                        LexemAdder(word);
                        word = symbol.ToString();
                    }
                }
                else if (symbol == ':')
                {
                    if (word != "") LexemAdder(word);
                    word = "";
                    word += symbol;
                    symbol = GetNextSymbol();
                    while (symbol == ' ' || symbol == '\t')
                    {
                        symbol = GetNextSymbol();
                    }
                    if (symbol == '=') //Проверяем, является ли это оператором присваивания, если да - добавляем
                    {
                        word += symbol;
                        LexemAdder(word);
                        word = "";
                    }
                    else
                    {
                        LexemAdder(word);
                        word = symbol.ToString();
                    }
                }
                else if (Char.IsPunctuation(symbol))
                {
                    LexemAdder(symbol.ToString()); //Если знак пунктуации - добавляем
                }
                else
                {
                    word += symbol; //Если не выполнено ни одно из предыдущих условий - добавляем символ к слову и продолжаем цикл
                }

            }



        }

        private char GetNextSymbol()
        {

            if (String.IsNullOrEmpty(buffer)) //Проверяем на null считываемую строку, если пустая - считываем следующую

            {
                buffer = input[currentRow++];
                return '\0';

            }
            else if (currentSymbol == buffer.Length) //Проверяем конец строки
            {
                buffer = input[currentRow++]; //Считываем следующую строку
                currentSymbol = 0;
                if (currentRow > RowsCount) { currentRow--; }
                return '\n';

            }


            return buffer[currentSymbol++];

        }



        private void GetText() //считываем весь текст программы
        {
            input = text.Split(new char[] { '\n' });
            RowsCount = input.Length;
        }

        private void LexemAdder(string inp) //Метод добавления лексем и ошибок в список
        {

            if (Char.IsDigit(inp[0])) //Добавляем число - константу
            {
                LexemList.Add(new Lexem(inp, Lexem.ClassLexem.Const, currentRow));
            }
            else if (inp == "unar")
            {
                LexemList.Add(new Lexem("-", Lexem.ClassLexem.UnarOp, currentRow));
            }
            else if (inp[0] == '-' && inp.Length > 1 && Char.IsDigit(inp[1])) //Добавляем унарный оператор и число
            {
                LexemList.Add(new Lexem("-", Lexem.ClassLexem.UnarOp, currentRow));
                LexemList.Add(new Lexem(inp.Substring(1), Lexem.ClassLexem.Const, currentRow));
            }
            else if (inp.ToLower() == "integer" || inp.ToLower() == "var" || inp.ToLower() == "begin" || inp.ToLower() == "end" || inp.ToLower() == "if" || inp.ToLower() == "else" || inp.ToLower() == "endif" || inp.ToLower() == "while" || inp.ToLower() == "endwhile") //Добавляем ключевые слова
            {
                if (inp.ToLower() == "begin")
                    LexemList.Add(new Lexem(inp, Lexem.ClassLexem.Keyword, currentRow - 1));
                else
                    LexemList.Add(new Lexem(inp, Lexem.ClassLexem.Keyword, currentRow));
            }
            else if ((Char.IsLetter(inp[0]) && currentRow == 1) || (Char.IsLetter(inp[0]) && Lexem.FindLexemByName(inp))) //Добавляем идентификаторы, если это первая строка (объявление переменных) либо если они уже объявлены ранее (в первой строке)
            {
                Lexem lex = new Lexem(inp, Lexem.ClassLexem.Ident, currentRow);
                LexemList.Add(lex);
                Idents = Idents.Append<string>(lex.GetName()).ToArray<string>();
            }
            else if (inp == ";" || inp == "," || inp == ")" || inp == "(" || inp == "}" || inp == "{" || inp == ":") //Добавляем служебные лексемы - знаки препинания
            {
                LexemList.Add(new Lexem(inp, Lexem.ClassLexem.Service, currentRow));
            }
            else if (inp == "+" || inp == "-" || inp == "*" || inp == "/") //Добавляем бинарные операторы
            {
                LexemList.Add(new Lexem(inp, Lexem.ClassLexem.BinarOp, currentRow));
            }
            else if (inp == ">" || inp == "<" || inp == ">=" || inp == "<=" || inp =="=") //Добавляем логические операторы
            {
                LexemList.Add(new Lexem(inp, Lexem.ClassLexem.BoolOp, currentRow));
            }
            else if (inp == ":=") //Добавляем оператор присваивания
            {
                LexemList.Add(new Lexem(inp, Lexem.ClassLexem.Assign, currentRow));
            }
            else
            {
                //MessageBox.Show(inp); //Если ни одна из групп лексем не подходит - ошибка
                Error error = new Error("Лексическая ошибка", $"Лексическая ошибка - встречены неизвестные символы: {inp}", currentRow);
                errorsList.Add(error);
            }


        }

        public static List<string> GetLexemList() //Метод получения списка лексем в формате String
        {
            List<string> list = new List<string>();
            foreach (Lexem lexem in LexemList)
            {
                list.Add(lexem.ToString());
            }
            return list;
        }




        public static List<Lexem> GetOrigLexemList() //Метод получения списка лексем в формате List
        {
            return LexemList;
        }

        public static void Clear() //Очистка списков при перезапуске анализатора
        {
            LexemList.Clear();
        }
        public List<string> GetNewLexemList() //Метод получения списка лексем в формате String
        {
            List<string> list = new List<string>();
            foreach (Lexem lexem in LexemList)
            {
                list.Add(lexem.ToString());
            }
            return list;
        }
    }
}
