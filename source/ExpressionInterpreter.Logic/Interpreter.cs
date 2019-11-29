using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionInterpreter.Logic
{
    public class Interpreter
    {
        private double _operandLeft;
        private double _operandRight;
        private char _op;  // Operator     
        List<Exception> exlist = new List<Exception>();


        bool _nrIsNegative;                //ist die Ganzzahl positiv oder Negativ
        bool _intIsDecimalPlace;          // ist die Ganzzahl hinter dem Komma 
        /// <summary>
        /// Eingelesener Text
        /// </summary>
        public string ExpressionText { get; private set; }

        public double OperandLeft
        {
            set { _operandLeft = value; }
            get { return _operandLeft; }
        }

        public double OperandRight
        {
            set { _operandRight = value; }
            get { return _operandRight; }
        }

        public char Op
        {
            set { _op = value; }
            get { return _op; }
        }


        public void Parse(string expressionText)
        {
            //To be done
            ExpressionText = expressionText;
            ParseExpressionStringToFields();
        }

        /// <summary>
        /// Wertet den Ausdruck aus und gibt das Ergebnis zurück.
        /// Fehlerhafte Operatoren und Division durch 0 werden über Exceptions zurückgemeldet
        /// </summary>
        public double Calculate()
        {
            double erg = 0.0;
            if (Op == '+')
            {
                erg = _operandLeft + _operandRight;
            }
            else if (Op == '-')
            {
                erg = _operandLeft - _operandRight;
            }
            else if (Op == '*')
            {
                erg = _operandLeft * _operandRight;
            }
            else if (Op == '/')
            {
                if (_operandRight == 0)
                {
                    throw new DivideByZeroException();
                }
                else
                {
                    erg = _operandLeft / _operandRight;
                }
            }
            else
            {
                throw new Exception($"Operator {Op} ist fehlerhaft!");
            }
            return erg;
        }

        /// <summary>
        /// Expressionstring in seine Bestandteile zerlegen und in die Felder speichern.
        /// 
        ///     { }[-]{ }D{D}[,D{D}]{ }(+|-|*|/){ }[-]{ }D{D}[,D{D}]{ }
        ///     
        /// Syntax  OP = +-*/
        ///         Vorzeichen -
        ///         Zahlen double/int
        ///         Trennzeichen Leerzeichen zwischen OP, Vorzeichen und Zahlen
        /// </summary>
        public void ParseExpressionStringToFields()
        {
            int pos = 0;
        
            int expressiontextl = ExpressionText.Length;
            SkipBlanks(ref pos);
            OperandLeft = ScanNumber(ref pos);
            SkipBlanks(ref pos);
            ScanOperator(ref pos);
            SkipBlanks(ref pos);
            OperandRight = ScanNumber(ref pos);
            SkipBlanks(ref pos);

            //while
            //throw new NotImplementedException();
        }


        /// <summary>
        /// Ein Double muss mit einer Ziffer beginnen. Gibt es Nachkommastellen,
        /// müssen auch diese mit einer Ziffer beginnen.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private double ScanNumber(ref int pos)
        {
            double number = 0;
            _nrIsNegative = CheckIsNegativeNumber(ref pos);
            ScanInteger(ref pos);


            if (_nrIsNegative)
            {
                number = number * (-1);
            }
            return number;
        }

        /// <summary>
        /// Eine Ganzzahl muss mit einer Ziffer beginnen.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private int ScanInteger(ref int pos)
        {
           
            int number = ExpressionText[pos] - '0';
            pos++;
            return number;
        }

        /// <summary>
        /// Setzt die Position weiter, wenn Leerzeichen vorhanden sind
        /// </summary>
        /// <param name="pos"></param>
        private void SkipBlanks(ref int pos)
        {
            while (ExpressionText[pos] == ' ')
            {
                pos++;
            }

        }
        private bool CheckIsNegativeNumber(ref int pos)
        {
            if (ExpressionText[pos] == '-')
            {
                pos++;
                return true;
            }
            else
                return false;

        }

        private void ScanOperator(ref int pos)
        {
            if (ExpressionText[pos] == '+' || ExpressionText[pos] == '-' ||
               ExpressionText[pos] == '*' || ExpressionText[pos] == '/')
            {
                Op = ExpressionText[pos];
                pos++;
            }
            else
                throw new InvalidOperationException();

        }

        /// <summary>
        /// Exceptionmessage samt Innerexception-Texten ausgeben
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExceptionTextWithInnerExceptions(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
