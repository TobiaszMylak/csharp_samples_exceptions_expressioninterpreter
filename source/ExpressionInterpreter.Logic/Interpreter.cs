using System;
using System.Text;

namespace ExpressionInterpreter.Logic
{
    public class Interpreter
    {
        private double _operandLeft;
        private double _operandRight;
        private char _op;  // Operator
        private bool IsbeforeComma;
        private bool IsNegativeNumber;

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
                erg = _operandLeft / _operandRight;
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
            int Textlenght = ExpressionText.Length;
            SkipBlanks(ref pos);
            _operandLeft = ScanNumber(ref pos);
            SkipBlanks(ref pos);

            while (pos < Textlenght)
            {
                if (ExpressionText[pos] == ' ')
                {

                }
            }
            
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
           
            if(ExpressionText[pos] == '-')
            {
               
                pos++;
            }
     
                ScanInteger(ref pos);
            
            if (ExpressionText[pos] == ',')
            {
                ScanInteger(ref pos);
            }
             


            throw new NotImplementedException();
        }

        /// <summary>
        /// Eine Ganzzahl muss mit einer Ziffer beginnen.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private int ScanInteger(ref int pos)
        {
            int intValue = 0;
            int startposition = pos; //
            int counter = 0; //zaehlt wie oft die ziffer gelesen wird.
            while (Char.IsDigit(ExpressionText[pos]))
            {
                intValue = 
                pos++;
                //-- scan integer
            }
    
            // if(ExpressionText[pos])

            throw new NotImplementedException();
        }

        /// <summary>
        /// Setzt die Position weiter, wenn Leerzeichen vorhanden sind
        /// </summary>
        /// <param name="pos"></param>
        private void SkipBlanks(ref int pos)
        {
            while (ExpressionText[pos] == ' ')
                pos++;
        }

        public void Operator(ref int pos)
        {
            if(ExpressionText[pos]  == '+' )
            {
                Op = '+';
            }
            else if(ExpressionText[pos]  == '-')
            {
                 Op = '-';
            }
            else if(ExpressionText[pos]  == '*')
            {
                 Op = '*';
            }
            else if(ExpressionText[pos] == '/')
            {
                 Op = '/';
            }
            else
            {
                throw new Exception();
            }
            pos++;
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
