using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denisenko_KursProject
{
    internal class SyntaxAnalisator
    {
        public List<Lexem> LexemList = new List<Lexem>();
        private int current_lexem = 0;
        public static string errorMessage = string.Empty;
        private bool flag = false;
        public List<Error> errors = new();

        public SyntaxAnalisator(string text1)
        {
            LexemList.Clear();
            string[] lexems = new string[200];
            lexems = text1.Split(new char[] { '\n' });
            foreach (string inp in lexems)
            {
                string[] vals = new string[3];
                vals = inp.Split(new char[] { '\t','|' },StringSplitOptions.RemoveEmptyEntries) ;
                if (vals.Length>0)
                    LexemList.Add(new Lexem(vals[0], Lexem.GetClassByName(vals[1]), Convert.ToInt32(vals[2])));
            }
            

            //LexemList = LexicalAnalisator.GetOrigLexemList(); //Получаем лексемы
            if (LexemList.Count == 0)
            {
                MessageBox.Show("Выберите файл лексем!", "Ошибка анализа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                try
                {
                    SyntaxAnalis(); // Запуск синтаксического анализа
                    MessageBox.Show("Успешно!", "Синтаксический анализ", MessageBoxButtons.OK, MessageBoxIcon.None);

                }
                catch (Exception e)
                {
                    errorMessage = "Ошибка синтаксического анализа: " + e.Message;
                    MessageBox.Show(errorMessage, "Ошибка анализа", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        private void SyntaxAnalis()  //Запускаем синтаксический анализ
        {
            ParseVariableDeclaration();
            ParseCalculationDescription();
        }


        private void MatchTerminal(string terminal) //Проверка соответствия лексемы необходимому терминалу
        {
            string lexemName = LexemList[current_lexem].GetName();
            if (lexemName.ToLower() != terminal.ToLower())
            {
                errors.Add(new Error("Синтаксическая ошибка", $"Ожидался терминал \"{terminal}\", получен \"{lexemName}\"", LexemList[current_lexem].GetRow()));
                //throw new Exception($"Ожидался терминал \"{terminal}\", получен \"{lexemName}\", лексема \"{current_lexem}\"");
            }
            //else
                current_lexem++;
        }

        private void ParseVariableDeclaration() //Обработка декларации (объявления) переменных
        {
            MatchTerminal("Var");
            ParseVariableList();
            MatchTerminal(":");
            MatchTerminal("Integer");
            MatchTerminal(";");
        }

        private void ParseCalculationDescription() //Обработка описания вычислений
        {
            MatchTerminal("begin");
            ParseAssignmentList();
            MatchTerminal("end");
        }

        private void ParseVariableList() //Обработка списка переменных
        {
            ParseIdentifier();

            while (LexemList[current_lexem].GetName() == ",")
            {
                current_lexem++;
                ParseVariableList();
            }
        }

        private void ParseAssignmentList() //Обработка списка присваиваний
        {
            ParseAssignment();

            while (LexemList[current_lexem].GetName().ToLower() != "end" && LexemList[current_lexem].GetName().ToLower() != "}")
            {
                ParseAssignmentList();
            }
        }

        private void ParseAssignment() //Обработка присваивания
        {
            if (LexemList[current_lexem].GetName().ToLower() == "if")
            {
                ParseIf();
            }
            else if(LexemList[current_lexem].GetName().ToLower() == "while")
            {
                ParseWhile();
            }
            else
            {
                ParseIdentifier();
                MatchTerminal(":=");
                ParseExpression();
                MatchTerminal(";");
            }
            
        }

        private void ParseIdentifier() //Обработка идентификаторов
        {
            if (LexemList[current_lexem].GetClass() != Lexem.ClassLexem.Ident)
            {
                errors.Add(new Error("Синтаксическая ошибка", $"Ожидался идентификатор, получен \"{LexemList[current_lexem].GetName()}\"", LexemList[current_lexem].GetRow()));

            }
            //throw new Exception($"Ожидался идентификатор, получен \"{LexemList[current_lexem].GetName()}\", лексема {current_lexem}");

            current_lexem++;
        }

        private bool ParseOperand() //Обработка операндов: идентификаторов и констант
        {
            if (LexemList[current_lexem].GetClass() != Lexem.ClassLexem.Const && LexemList[current_lexem].GetClass() != Lexem.ClassLexem.Ident)
            { 
                errors.Add(new Error("Синтаксическая ошибка", $"Ожидался операнд, получено \"{LexemList[current_lexem].GetName()}\"", LexemList[current_lexem].GetRow()));
                return false;

            }       //throw new Exception($"Ожидался операнд, получено \"{LexemList[current_lexem].GetName()}\", лексема {current_lexem}");
            else
            {
                current_lexem++;
                return true;
            }
        }

        private void ParseExpression() //Обработка выражения
        {
            if (LexemList[current_lexem].GetClass() == Lexem.ClassLexem.UnarOp)
            {
                current_lexem++;
            }

            ParseSubExpression();
            while (ParseBinarOperator())
            {
                ParseSubExpression();
            }

        }

        private bool ParseBinarOperator() //Обработка бинарного оператора
        {
            if ((LexemList[current_lexem].GetName() == "+") || (LexemList[current_lexem].GetName() == "-") || (LexemList[current_lexem].GetName() == "*") || (LexemList[current_lexem].GetName() == "/"))
            {
                current_lexem++;
                return true;
            }
            else return false;
        }

        private void ParseSubExpression() //Обработка подвыражения
        {
            if (LexemList[current_lexem].GetName() == "(")
            {
                current_lexem++;
                ParseExpression();
                MatchTerminal(")");
            }
            else
            {
                ParseOperand();
            }

        }

        private void ParseIf() //обработка условного выражения
        {
            MatchTerminal("if");
            ParseBoolExpression();
            MatchTerminal("{");
            ParseAssignmentList();
            MatchTerminal("}");
            if (LexemList[current_lexem].GetName().ToLower() == "else") 
            {
                MatchTerminal("else");
                MatchTerminal("{");
                ParseAssignmentList();
                MatchTerminal("}");
            }
        }

        private void ParseBoolExpression() //обработка логического выражения
        {
            MatchTerminal("(");
            ParseOperand();
            ParseBoolOperator();
            ParseOperand();
            MatchTerminal(")");

        }

        private bool ParseBoolOperator() //Обработка булева оператора
        {
            if ((LexemList[current_lexem].GetName() == ">") || (LexemList[current_lexem].GetName() == "<") || (LexemList[current_lexem].GetName() == "<=") || (LexemList[current_lexem].GetName() == ">=") || (LexemList[current_lexem].GetName() == "="))
            {
                current_lexem++;
                return true;
            }
            else return false;
        }

        private void ParseWhile()
        {
            MatchTerminal("while");
            ParseBoolExpression();
            MatchTerminal("{");
            ParseAssignmentList();
            MatchTerminal("}");
        }
    }
}
