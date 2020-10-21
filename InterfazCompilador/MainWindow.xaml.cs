using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Reflection;

namespace InterfazCompilador
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        class Elements
        {
            public string Element { get; set; }
            public string Value { get; set; }
            public string Type { get; set; }

        }
        Dictionary<string, int> RulesColumnSymbol = new Dictionary<string, int>();
        Dictionary<string, int> TerminalSymbol = new Dictionary<string, int>();

        List<List<int>> GR2lsrTable = new List<List<int>>();
        List<String> GR2lsrRules = new List<String>();

        private void BTN_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                TXT_SourceCode.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void TXT_SourceCode_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BTN_GO_Click(object sender, RoutedEventArgs e)
        {
            this.listView.Items.Clear();
            List<Elements> MyElements = new List<Elements>();
            bool bFound;
            String sText = TXT_SourceCode.Text;
            sText += "$";
            //------------------------------ ANALISIS LEXICO -----------------------------------
            char character;
            int Indx = 0;
            while (Indx<sText.Length)
            {
                String sElement = "";
                String sType = "";
                String sValue = "-2";
                bFound = false;
                character = sText[Indx];

                //------------------- TIPOS, ID, PALABRAS RESERVADAS -------------------------
                if (Char.IsLetter(character) || character=='_')
                {
                    sType = "tipo";
                    sValue = "0";
                    while (!bFound)
                    {
                        if(Char.IsLetterOrDigit(character) || character == '_')
                        {
                            sElement += character;
                            Indx++;
                            character = sText[Indx];
                            if (character == '$')
                            {
                                bFound = true;
                            }
                        }
                        else
                        {
                            bFound = true;
                        }
                    }

                }
                //------------------------------- CONSTANTES ----------------------------------
                else if(Char.IsNumber(character) || character=='.')
                {
                    bool bAlreadyHasPoint = false;
                    sType = "constante";
                    sValue = "r";
                    while (!bFound)
                    {
                        if (Char.IsDigit(character))
                        {
                            sElement += character;
                            Indx++;
                            character = sText[Indx];
                            if (character == '$')
                            {
                                bFound = true;
                            }
                        }
                        else if (character == '.')
                        {
                            sValue = "s";
                            if (bAlreadyHasPoint)
                            {
                                //Error
                            }
                            else
                            {
                                bAlreadyHasPoint = true;
                                sElement += character;
                                Indx++;
                                character = sText[Indx];
                                if (character == '$')
                                {
                                    bFound = true;
                                }
                            }
                        }
                        else 
                        {
                            bFound = true;
                        }
                    }
                }

                //Asignamos segun el tipo de cadena
                if (sValue == "0")
                {
                    if (sElement == "entero")
                    {
                        sType = "tipo";
                        sValue = "d";
                    }
                    else if (sElement == "real")
                    {
                        sType = "tipo";
                        sValue = "e";
                    }
                    else if (sElement == "if")
                    {
                        sType = "if";
                        sValue = "h";
                    }
                    else if (sElement == "while")
                    {
                        sType = "while";
                        sValue = "t";
                    }
                    else if (sElement == "else")
                    {
                        sType = "else";
                        sValue = "k";
                    }
                    else if (sElement == "begin")
                    {
                        sType = "begin";
                        sValue = "a";
                    }
                    else if (sElement == "end")
                    {
                        sType = "end";
                        sValue = "b";
                    }
                    else if (sElement == "endwhile")
                    {
                        sType = "endwhile";
                        sValue = "u";
                    }
                    else
                    {
                        sType = "identificador";
                        sValue = "f";
                    }
                }

                //Siguiente elemento y agregamos el que encontramos
                if (sValue != "-2")
                {       
                    MyElements.Add(new Elements { Element = sElement, Type = sType, Value = sValue });
                }
                if (Indx + 1 <= sText.Length)
                {
                    Indx++;
                }

                sElement = "";
                sType = "";
                sValue = "-2";
                if (Char.IsPunctuation(character) || Char.IsSymbol(character))
                {
                    if (character == ';')
                    {
                        sElement += character;
                        sType = ";";
                        sValue = "c";
                    }
                    else if (character == ',')
                    {
                        sElement += character;
                        sType = ",";
                        sValue = "g";
                    }
                    else if (character == '(')
                    {
                        sElement += character;
                        sType = "(";
                        sValue = "i";
                    }
                    else if (character == ')')
                    {
                        sElement += character;
                        sType = ")";
                        sValue = "j";
                    }
                    else if (character == '=')
                    {
                        sElement += character;
                        sType = "=";
                        sValue = "l";
                        
                    }
                    else if (character == '+')
                    {
                        sElement += character;
                        sType = "opsuma";
                        sValue = "w";
                    }
                    else if (character == '-')
                    {
                        sElement += character;
                        sType = "opresta";
                        sValue = "y";
                    }
                    else if(character=='*')
                    {
                        sType = "opmultiplicacion";
                        sElement += character;
                        sValue = "x";
                    }
                    else if (character == '/')
                    {
                        sType = "opdivision";
                        sElement += character;
                        sValue = "z";
                    }
                    else if(character=='<')
                    {
                        sElement += character;
                        if (sText[Indx] == '=')
                        {
                            character = sText[Indx];
                            sElement += character;
                            sValue = "m";
                        }
                        else if (sText[Indx] == '>')
                        {
                            character = sText[Indx];
                            sElement += character;
                            sValue = "o";
                        }
                        else
                        {
                            sValue = "p";
                        }
                        sType = "opRelacional";
                    }
                    else if (character == '>')
                    {
                        sElement += character;
                        if (sText[Indx] == '=')
                        {
                            character = sText[Indx];
                            sElement += character;
                            sValue = "n";
                        }
                        else
                        {
                            sValue = "q";
                        }
                        sType = "opRelacional";
                    }
                    else if (character == ':')
                    {
                        sElement += character;
                        if (sText[Indx] == '=')
                        {
                            character = sText[Indx];
                            sElement += character;
                            sValue = "v";
                        }
                        sType = "asignacion";
                    }
                }
                
                //Siguiente elemento y agregamos el que encontramos
                if (sValue != "-2")
                {
                    MyElements.Add(new Elements { Element = sElement, Type = sType, Value = sValue });
                    if (Indx + 1 <= sText.Length)
                    {
                        Indx++;
                    }
                }
                else
                {
                    if (!Char.IsWhiteSpace(sText[Indx-1])&& Indx <sText.Length)
                        Indx--;
                }
                
            }

            MyElements.Add(new Elements { Element = "$", Type = "$", Value = "$" });
            //Displays Our Elements into the Data Grid View
            DGV_Elements.ItemsSource = MyElements;

            //----------------------------- ANALISIS SINTACTICO --------------------------------
            int TableIndxColumn = 0, TableIndxRow = 0, ProgramQueueIndx = 0, RuleIndxRow = 0; 
            Stack<char> SintactStack = new Stack<char>();
            SintactStack.Push('$');
            SintactStack.Push('S');
            bool IsFinalized = false, IsSintactlyCorrect = true;

            char ColumnCode;

            while (!IsFinalized)
            {
                string step = "";
                foreach (char c in SintactStack)
                {
                    step = c+step;
                }
                this.listView.Items.Add(step);
                ColumnCode = SintactStack.Pop();
                if (Char.IsUpper(ColumnCode))
                {
                    TableIndxRow = RulesColumnSymbol[ColumnCode.ToString()];
                    TableIndxColumn = TerminalSymbol[MyElements[ProgramQueueIndx].Value]-1;
                    RuleIndxRow = GR2lsrTable[TableIndxRow][TableIndxColumn]-1;
                    if(RuleIndxRow == -1)
                    {
                        TableIndxColumn = TerminalSymbol["-"] - 1;
                        RuleIndxRow = GR2lsrTable[TableIndxRow][TableIndxColumn] - 1;
                    }
                    if(RuleIndxRow == -1)
                    {
                        IsFinalized = true;
                        IsSintactlyCorrect = false;
                    }
                    else
                    {
                        foreach (char c in GR2lsrRules[RuleIndxRow])
                        {
                            SintactStack.Push(c);
                        }
                    }
                }
                else if (ColumnCode == '$')
                {
                    IsFinalized = true;
                    IsSintactlyCorrect = true;
                }
                else
                {
                    ProgramQueueIndx++;
                }
            }

            if (IsSintactlyCorrect)
            {
                LblStatus.Foreground = Brushes.GreenYellow;
                LblStatus.Content = "Analisis Correcto";
            }
            else
            {
                LblStatus.Foreground = Brushes.Red;
                LblStatus.Content = "Analisis Incorrecto";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            //Load the table
            string executableLocation = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string FileLocation = System.IO.Path.Combine(executableLocation, "AnalisisSintactico.txt");
            string sTable = File.ReadAllText(FileLocation);

            string[] lines = sTable.Split( new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach( string line in lines)
            {
                List<int> LineList = new List<int>();
                string[] cells = line.Split(new[] { '\t' }, StringSplitOptions.None);
                foreach( string cell in cells)
                {
                    int value = Int32.Parse(cell);
                    LineList.Add(value);
                }
                GR2lsrTable.Add(LineList);
            }

            //Load the rules
            FileLocation = System.IO.Path.Combine(executableLocation, "rules.txt");
            string sRules = File.ReadAllText(FileLocation);
            lines = sRules.Split(new[] {Environment.NewLine }, StringSplitOptions.None);
            foreach(string line in lines)
            {
                GR2lsrRules.Add(line);
            }

            //Load the Codes of the row rules
            FileLocation = System.IO.Path.Combine(executableLocation, "RulesColumnSymbol.txt");
            sRules = File.ReadAllText(FileLocation);
            lines = sRules.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int nIndx = 0;
            foreach (string line in lines)
            {
                RulesColumnSymbol.Add(line, nIndx);
                nIndx++;
            }

            //Load the value of the terminal symbols
            FileLocation = System.IO.Path.Combine(executableLocation, "TerminalSymbol.txt");
            sRules = File.ReadAllText(FileLocation);
            lines = sRules.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            nIndx = 1;
            foreach (string line in lines)
            {
                TerminalSymbol.Add(line, nIndx);
                nIndx++;
            }
        }
    }
}
