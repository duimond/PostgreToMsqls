using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Конвертер
{
    internal class Parser
    {
        public Parser(RichTextBox box)
        {
            this.stroke = 1;
            this.box = box;
            this.patern = @"(\bSUBSTRING\b|\bINSERT\b|\bUPDATE\b|\bDELETE\b|\bSELECT\b|\bFROM\b|\bWHERE\b|\bORDER BY\b|\bGROUP BY\b|\bLIMIT\b|\bDISTINCT\b|\*|\bALL\b|\bAS\b|\bCOUNT\b|,|\(|\)|>|<|;|\bINSERT INTO\b|\bVALUES\b|\bSET\b|=|$|$|\bINNER JOIN\b|\bLEFT JOIN\b|\bRIGHT JOIN\b|\bFULL JOIN\b|\bJOIN\b|\bON\b|\bNOT\b|\bAND\b|\bOR\b|\bIS\b|\bBETWEEN\b|\bLIKE\b|\bILIKE\b|\bIN\b|~|!|\bCUBE\b|\bROLLUP\b|\bLENGTH\b|\bASC\b|\bDESC\b|/|\|\||\+|-|\bNULLS\b|\bNULL\b|\bLAST\b|\bFIRST\b|\bONLY\b|\bSUM\b|\bMIN\b|\bMAX\b|\bAVG\b|[\w\.\'\@]+)";// строка проверки
            machine = new StateMachine();
            this.analis = new Dictionary<string, string>();
            this.analis.Add("INSERT", "<ins>");
            this.analis.Add("UPDATE", "<upd>");
            this.analis.Add("DELETE", "<del>");
            this.analis.Add("SELECT", "<sel>");
            this.analis.Add("FROM", "<fro>");
            this.analis.Add("WHERE", "<whe>");
            this.analis.Add("ORDER BY", "<ord>");
            this.analis.Add("GROUP BY", "<gro>");
            this.analis.Add("LIMIT", "<lim>");
            this.analis.Add("DISTINCT", "<dis>");
            this.analis.Add("*", "<all>");
            this.analis.Add("ALL", "<alll>");
            this.analis.Add("AS", "<as>");
            this.analis.Add("COUNT", "<con>");
            this.analis.Add(",", "<zpt>");
            this.analis.Add(">", "<bol>");
            this.analis.Add("<", "<men>");
            this.analis.Add(";", "<tzpt>");
            this.analis.Add("INSERT INTO", "<ins>");
            this.analis.Add("VALUES", "<val>");
            this.analis.Add("'", "<kov>");
            this.analis.Add("SET", "<set>");
            this.analis.Add("=", "<rav>");
            this.analis.Add("(", "<lef>");
            this.analis.Add(")", "<rig>");
            this.analis.Add("INNER JOIN", "<ijo>");
            this.analis.Add("LEFT JOIN", "<ljo>");
            this.analis.Add("RIGHT JOIN", "<rjo>");
            this.analis.Add("FULL JOIN", "<fjo>");
            this.analis.Add("JOIN", "<joi>");
            this.analis.Add("ON", "<on>");
            this.analis.Add("NOT", "<not>");
            this.analis.Add("AND", "<and>");
            this.analis.Add("OR", "<or>");
            this.analis.Add("IS", "<is>");
            this.analis.Add("BETWEEN", "<bwn>");
            this.analis.Add("LIKE", "<lik>");
            this.analis.Add("ILIKE", "<ilik>");
            this.analis.Add("IN", "<in>");
            this.analis.Add("~", "<til>");
            this.analis.Add("!", "<vos>");
            this.analis.Add("CUBE", "<cub>");
            this.analis.Add("ROLLUP", "<rol>");
            this.analis.Add("LENGTH", "<len>");
            this.analis.Add("ASC", "<asc>");
            this.analis.Add("DESC", "<desc>");
            this.analis.Add("/", "<div>");
            this.analis.Add("||", "<ili>");
            this.analis.Add("+", "<plus>");
            this.analis.Add("-", "<minus>");
            this.analis.Add("NULLS", "<nulls>");
            this.analis.Add("NULL", "<null>");
            this.analis.Add("LAST", "<last>");
            this.analis.Add("FIRST", "<firs>");
            this.analis.Add("ONLY", "<onl>");
            this.analis.Add("SUM", "<sum>");
            this.analis.Add("MIN", "<min>");
            this.analis.Add("MAX", "<max>");
            this.analis.Add("AVG", "<avg>");
            this.analis.Add("SUBSTRING", "<subst>");

            this.box = box;
        }
        public string Analizator(string st)
        {
            string result = "";
            string proverka = patern; // строка проверки
            string[] tokens = Regex.Split(st, proverka, RegexOptions.IgnoreCase); // разделение входного кода по токенам, с игнорированием размера текста
            foreach (var token in tokens) // проходимся по каждому токену в текущей строке
            {
                string trimmedToken = token.Trim(); // Убираем пробелы
                var helpful = trimmedToken.ToUpper(); // Создаём клон токена

                if (helpful.Length > 0) // если токен не пуст
                {
                    if (analis.ContainsKey(helpful)) // Проверяем, есть ли токен в словаре, если есть, значит это како-то оператор и можно применить проверку ProcessInput
                    {

                        if (machine.ProcessInput(analis[helpful])) // если можно установить данный оператор, то устанавливаем его
                        {
                            result += analis[helpful] + " ";
                        }
                        else // если же оператор поставить нельзя, выдаём ошибку
                        {

                            result = "Ошибка в " + stroke + " строке " + helpful + machine.getstate();
                            break;
                        }
                    }
                    else // если токена нет в словаре, значит это какая-то переменная, поэтому применяем VariablePossibility
                    {
                        if (machine.VariablePossibility()) // если есть возможность написать переменную, то записываем как есть
                        {
                            result += "" + trimmedToken + " ";
                        }
                        else // если же переменную сейчас ставить нельзя, то выдаём ошибку
                        {
                            result = "Ошибка в " + stroke + " строке, неизвестный оператор '" + trimmedToken + "'" + machine.getstate();
                        }
                    }
                }
            }
            stroke++; // переход на новую строку
            return result; // Убираем лишние пробелы в начале и конце           
        }

        public void AutoChange() // переводим все операторы в верхний регистр и меняем текст
        {
            string proverka = patern; // строка проверки
            string[] tokens = Regex.Split(box.Text, proverka, RegexOptions.IgnoreCase); // разделение входного кода по токенам, с игнорированием размера текста
            
            int startIndex = 0;
            foreach (var token in tokens)
            {
                string trimmedToken = token.Trim(); // Убираем пробелы
                var helpful = trimmedToken.ToUpper(); // Создаём клон токена

                if (analis.ContainsKey(helpful)) // если токен присутствует
                {
                    // процедура изменения цвета
                }
            }
        }
        private Dictionary<string, string> analis; // словарь токенов
        
        private StateMachine machine; // конечный автомат
        private int stroke; // номер обрабатываемой строки
        private RichTextBox box; // блок textbox из главной формы
        private string patern; // строка проверки
    }
}
