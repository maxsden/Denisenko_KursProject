using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denisenko_KursProject
{
    internal class Lexem
    {
        private string Name;
        private ClassLexem Class;
        private int Row;
        internal enum ClassLexem { Ident, Const, Assign, UnarOp, BinarOp, Service, Keyword, BoolOp };


        public Lexem(string name, ClassLexem @class, int row) //Создаем экземпляр лексему в конструкторе
        {
            Name = name;
            Class = @class;
            Row = row;
        }


        public override string ToString() //Переопределяем метод ToString()
        {
            return String.Concat(Name, "\t\t|\t", Class.ToString(), "\t\t|\t", Row);
        }

        public static bool FindLexemByName(string name) //Поиск лексем по имени - реализован для поиска идентификаторов
        {
            foreach (Lexem lex in LexicalAnalisator.GetOrigLexemList())
            {
                if (lex.Name == name) return true;
            }


            return false;
        }


        public string GetName()
        {
            return Name;
        }

        public int GetRow()
        {
            return Row;
        }

        public ClassLexem GetClass()
        {
            return Class;
        }

        public static ClassLexem GetClassByName(string name)
        {
            //Ident, Const, Assign, UnarOp, BinarOp, Service, Keyword, BoolOp
            switch (name)
            {
                case "Ident": 
                    return ClassLexem.Ident;
                case "Const":
                    return ClassLexem.Const;
                case "Assign":
                    return ClassLexem.Assign;
                case "UnarOp":
                    return ClassLexem.UnarOp;
                case "BinarOp":
                    return ClassLexem.BinarOp;
                case "Service":
                    return ClassLexem.Service;
                case "Keyword":
                    return ClassLexem.Keyword;
                case "BoolOp":
                    return ClassLexem.BoolOp;
                default:
                    return ClassLexem.Ident;






            }

        }

    }
}
