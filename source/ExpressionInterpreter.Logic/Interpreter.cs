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
           
            SkipBlanks(ref pos);
            OperandLeft = ScanNumber(ref pos);
            SkipBlanks(ref pos);
            Op =  ScanOperator(ref pos);
            SkipBlanks(ref pos);
            OperandRight = ScanNumber(ref pos);
            SkipBlanks(ref pos);
            Calculate();

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
            number = ScanInteger(ref pos);
            
            if(ExpressionText[pos] == ',')
            {
                pos++;
                number = number + ScanDecimalNumber(ref pos);
            }
          

            if (_nrIsNegative)
            {
                number = number * (-1);
                _nrIsNegative = false;
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
            int number = 0, currentpos = pos;

            if (char.IsDigit(ExpressionText[pos]) && pos < ExpressionText.Length) 
            {
                while (char.IsDigit(ExpressionText[pos]) )
                {
                    if(pos == currentpos)
                    {
                        number = (ExpressionText[pos] - '0');
                    }
                    else
                    {
                        number = (number * 10) + (ExpressionText[pos] - '0');
                    }
                    if (pos == ExpressionText.Length - 1) // Quick and very very dirty
                    {
                        break;
                    }
                    pos++;
                }
            }
            return number;
        }

        /// <summary>
        /// Setzt die Position weiter, wenn Leerzeichen vorhanden sind
        /// </summary>
        /// <param name="pos"></param>
        private void SkipBlanks(ref int pos)
        {
            while (ExpressionText[pos] == ' ' && pos < ExpressionText.Length-1 )
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
            {
                return false;
            }
        }

        private double ScanDecimalNumber( ref int pos)
        {
            double nr = 0;
            int currentpos = pos, counter = 0, currentint = 0;
          
            while(char.IsDigit(ExpressionText[pos]) )
            {
                counter++;

                if (pos == currentpos)
                {
                    currentint = (ExpressionText[pos] - '0');
                }
                else
                {
                    currentint = (currentint * 10) + (ExpressionText[pos] - '0');
                }
                pos++;
            }
            nr = currentint;

            while(counter > 0)
            {
                nr = nr / 10;
                counter--;
            }
            return nr;
        }

        private char ScanOperator(ref int pos)
        {
            char currentoperator;
            if (ExpressionText[pos] == '+' || ExpressionText[pos] == '-' ||
               ExpressionText[pos] == '*' || ExpressionText[pos] == '/')
            {
                currentoperator = ExpressionText[pos];
                pos++;
                return currentoperator;
                
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
