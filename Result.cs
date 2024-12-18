using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Конвертер
{
    internal class Result
    {
        public Result(RichTextBox Box)
        {

            this.query = new StringBuilder();
            this.limitValue = null;
            sqlToMSQLS = new Dictionary<string, string>();
            this.sqlToMSQLS.Add("<upd>", "UPDATE");
            this.sqlToMSQLS.Add("<del>", "DELETE");
            this.sqlToMSQLS.Add("<sel>", "SELECT ");
            this.sqlToMSQLS.Add("<fro>", "FROM ");
            this.sqlToMSQLS.Add("<whe>", "WHERE ");
            this.sqlToMSQLS.Add("<ord>", "ORDER BY ");
            this.sqlToMSQLS.Add("<gro>", "GROUP BY ");
            this.sqlToMSQLS.Add("<lim>", "TOP");
            this.sqlToMSQLS.Add("<dis>", "DISTINCT");
            this.sqlToMSQLS.Add("<all>", "*");
            this.sqlToMSQLS.Add("<alll>", "ALL");
            this.sqlToMSQLS.Add("<as>", "AS ");
            this.sqlToMSQLS.Add("<con>", "COUNT");
            this.sqlToMSQLS.Add("<zpt>", ", ");
            this.sqlToMSQLS.Add("<bol>", ">");
            this.sqlToMSQLS.Add("<men>", "<");
            this.sqlToMSQLS.Add("<tzpt>", ";");
            this.sqlToMSQLS.Add("<ins>", "INSERT INTO ");
            this.sqlToMSQLS.Add("<val>", "VALUES");
            this.sqlToMSQLS.Add("<kov>", "'");
            this.sqlToMSQLS.Add("<set>", "SET");
            this.sqlToMSQLS.Add("<rav>", "= ");
            this.sqlToMSQLS.Add("<lef>", "(");
            this.sqlToMSQLS.Add("<rig>", ") ");
            this.sqlToMSQLS.Add("<ijo>", "INNER JOIN ");
            this.sqlToMSQLS.Add("<ljo>", "LEFT JOIN ");
            this.sqlToMSQLS.Add("<rjo>", "RIGHT JOIN ");
            this.sqlToMSQLS.Add("<fjo>", "FULL OUTER JOIN ");
            this.sqlToMSQLS.Add("<joi>", "JOIN ");
            this.sqlToMSQLS.Add("<on>", "ON ");
            this.sqlToMSQLS.Add("<not>", "NOT ");
            this.sqlToMSQLS.Add("<and>", "AND ");
            this.sqlToMSQLS.Add("<or>", "OR ");
            this.sqlToMSQLS.Add("<is>", "IS ");
            this.sqlToMSQLS.Add("<bwn>", "BETWEEN ");
            this.sqlToMSQLS.Add("<lik>", "LIKE ");
            this.sqlToMSQLS.Add("<ilik>", "COLLATE ");
            this.sqlToMSQLS.Add("<in>", "IN");
            this.sqlToMSQLS.Add("<til>", "LIKE ");
            this.sqlToMSQLS.Add("<vos>", "NOT ");
            this.sqlToMSQLS.Add("<cub>", "CUBE");
            this.sqlToMSQLS.Add("<rol>", "ROLLUP");
            this.sqlToMSQLS.Add("<len>", "LEN");
            this.sqlToMSQLS.Add("<asc>", "ASC");
            this.sqlToMSQLS.Add("<desc>", "DESC");
            this.sqlToMSQLS.Add("<div>", "/");
            this.sqlToMSQLS.Add("<ili>", "+");
            this.sqlToMSQLS.Add("<plus>", "+");
            this.sqlToMSQLS.Add("<minus>", "-");
            this.sqlToMSQLS.Add("<nulls>", "NULLS");
            this.sqlToMSQLS.Add("<null>", "NULL");
            this.sqlToMSQLS.Add("<last>", "IS NULL THEN 1 ELSE 0 END, ");
            this.sqlToMSQLS.Add("<firs>", "IS NULL THEN 0 ELSE 1 END, ");
            this.sqlToMSQLS.Add("<onl>", "ONLY");
            this.sqlToMSQLS.Add("<sum>", "SUM");
            this.sqlToMSQLS.Add("<min>", "MIN");
            this.sqlToMSQLS.Add("<max>", "MAX");
            this.sqlToMSQLS.Add("<avg>", "AVG");
            this.sqlToMSQLS.Add("<subst>", "SUBSTRING");

        }
        public string Convert(string st)
        {
            string result = structuring(st);
            lan = 0;
            return result;
        }
        private string structuring(string st)
        {
            string result = ""; //строка с результатом
            List<List<string>> structure = new List<List<string>>(); // список в котором будут упорядочены токены
            int index = -1; // индекс текущего списка в structure
            string[] key_values = { "<sel>", "<fro>","<joi>", "<rjo>", "<ljo>", "<fjo>", "<ijo>", "<whe>", "<ord>", "<gro>", "<lim>" }; // ключевые токены для разбивки
            int currentIndex = 0; // текущий индекс токена
            string[] rawTokens = st.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // разделяю строку с токенами

            List<string> stroke = new List<string>(); // список который будет содержать все вложенные токены
            int num_lef = 0; // количество скобок, нужно для вложенности
            int num_rig = 0;
            bool flag = false; // флаг работы с вложенностью
            for (int i = 0; i < rawTokens.Count(); i++)
            {
                string token = rawTokens[i];
                if (token == "<lef>" && key_values.Contains(rawTokens[i + 1]) && !flag) // если текущий токен это ( и следующий токен является ключевым, значит мы наткунлись на вложенность
                // и мы в данный момент не работаем с вложенностью (внутри вложенности может быть ещё одна вложенность, поэтому её нужно игнорировать)
                {
                    structure[index].Add(token);
                    structure[index].Add(Environment.NewLine);
                    flag = true; // ставим флаг работы со вложенностью
                    num_lef ++; // увеличиваем количество (
                    lan++; // увеличиваем уровень вложенности
                    continue;
                }
                if (flag) // если работаем с вложенностью
                {
                    if (token == "<lef>") num_lef++; // если обнаружили ( то добавляем к количеству
                    if (token == "<rig>") num_rig++; // если обнаружили ) то добавляем к количеству
                    if (num_lef != num_rig) // если после выявления скобок их количество всё ещё не равно
                    {
                        stroke.Add(token); // добавляем токен в новую строку для обработки
                    }
                    else
                    {
                        num_lef = 0; // сбрасываем в исходное положение
                        num_rig = 0;
                        string lan_stroke = string.Join(" ", stroke); // формируем строку для вложеннсоти
                        structure[index].Add(structuring(lan_stroke)); // обрабатываем её также как и текущую
                        lan--; // убираем вложенность
                       // structure[index].Add(Environment.NewLine); // делаем перенос на следующую строку
                        structure[index].Add(token);// Добавляем токен в структуру
                        flag = false; // прекращаем работу с вложенностью                          
                    }
                }
                else // если работаем не с вложенностью
                {
                    if (key_values.Contains(token)) // если токен есть в списке ключевых токенов то формируем строку конкретно для него
                    {
                        index++;// Увеличиваем индекс, когда находим новый ключевой токен
                        structure.Add(new List<string>());// создаём новый список под следующий ключевой токен
                        structure[index].Add(token);// Добавляем токен в структуру

                    }
                    else // если токен не ключевой то просто добавляем его к текущему набору
                    {
                        structure[index].Add(token);// Добавляем токен в структуру
                    }
                }
            }
            result = transformation(structure);
            return result;
        }
        private string transformation(List<List<string>> structure) // преобразование токенов в код
        {
            string result = ""; // строка для результата
            foreach (List<string> stri in structure) // обрабатываем каждый блок ключевых токенов
            {
                switch (stri[0]) // определяем с каким ключевым оператором имеем дело
                {
                    case ("<sel>"):// если текущая конструкция это SELECT
                        result += String.Concat(Enumerable.Repeat(tab, lan));// добавляем табуляцию если есть вложеннсоть
                        result += (sqlToMSQLS[stri[0]]); // добавляем ключевой оператор в начало
                        if (structure[structure.Count() - 1][0] == "<lim>") // если в коде присутсвует LIMIT то его нужно вклинить в select
                        {
                            List<string> list = new List<string>(); //Вспомогательный список для преобразования токенов в операторы и идентификаторы
                            foreach (string str in structure[structure.Count() - 1]) //обрабатываем каждый токен у LIMIT
                            {
                                if (str == "<tzpt>") continue; // если точка с запятой то пропускаем его
                                if (sqlToMSQLS.ContainsKey(str)) // если токен присутсвует в словаре
                                {
                                    list.Add(sqlToMSQLS[str] ); // тогда преобразовываем его
                                }
                                else
                                {
                                    list.Add(str + " "); //иначе оставляем как есть
                                }
                            } // конец вставки LIMIT
                            result += String.Join(" ", list); //обьединяем полученную конвертацию в одну строку и добавляем к результату,
                            result += ", "; // ставим запятую после TOP и идём дальше, а именно добавляем остаток select
                        }
                        foreach (string str in structure[structure.IndexOf(stri)].Skip(1)) // проходимся по каждому токену в структуре ключевого токена кроме первого так как мы его уже записали
                        {
                            
                            if (sqlToMSQLS.ContainsKey(str)) // если токен присутсвует в словаре
                            {
                                result += sqlToMSQLS[str] ; // тогда преобразовываем его 
                            }
                            else
                            {
                                result += str + " "; //иначе оставляем как есть
                            }
                        }
                        result += Environment.NewLine; // переходим на новую строку после конвертации ключевого токена
                        break;
                    case "<fro>": // если текущая конструкция это FROM, From не имеет каких либо несостыковок поэтому передаём всё как есть
                    case "<joi>": // если текущая конструкция это JOIN, JOIN не имеет каких либо несостыковок поэтому передаём всё как есть
                    case "<ijo>":
                    case "<ljo>":
                    case "<rjo>":
                    case "<fjo>":
                    case "<gro>":

                        result += String.Concat(Enumerable.Repeat(tab, lan));// добавляем табуляцию если есть вложеннсоть
                        result += (sqlToMSQLS[stri[0]]); // добавляем ключевой оператор в начало
                        foreach (string str in structure[structure.IndexOf(stri)].Skip(1)) // проходимся по каждому токену в структуре ключевого токена кроме первого так как мы его уже записали
                        {
                            if (sqlToMSQLS.ContainsKey(str)) // если токен присутсвует в словаре
                            {
                                result += sqlToMSQLS[str]; // тогда преобразовываем его 
                            }
                            else
                            {
                                result += str + " "; //иначе оставляем как есть
                            }
                        }
                        result += Environment.NewLine; // переходим на новую строку после конвертации ключевого токена
                        break;
                    case "<whe>":
                        result += String.Concat(Enumerable.Repeat(tab, lan));// добавляем табуляцию если есть вложеннсоть
                        result += (sqlToMSQLS[stri[0]]); // добавляем ключевой оператор в начало
                        for (int i = 1; i < structure[structure.IndexOf(stri)].Count(); i++) // проходимся по каждому елементу
                        {
                            string str = structure[structure.IndexOf(stri)][i];

                            if (str == "<all>" && structure[structure.IndexOf(stri)][i-1] == "<til>") // если нашли оператор ~*, то пропускаем *
                            {
                                string help = structure[structure.IndexOf(stri)][i + 1]; // необходимо переписать следующий елемент
                                string helpfull = $"'{help}'".Replace(help, $"%{help.Trim('\'')}%"); // вставляем % 
                                structure[structure.IndexOf(stri)][i + 1] = helpfull;
                                continue; // Перепрыгиваем текущий элемент
                            }
                            else
                            {
                                if (sqlToMSQLS.ContainsKey(str)) // если токен присутсвует в словаре
                                {
                                    result += sqlToMSQLS[str]; // тогда преобразовываем его 
                                }
                                else
                                {
                                    result += str + " "; //иначе оставляем как есть
                                }
                            }

                            // Обработка str
                        }
                        result += Environment.NewLine; // переходим на новую строку после конвертации ключевого токена
                        break;
                    case "<ord>":
                        result += String.Concat(Enumerable.Repeat(tab, lan));// добавляем табуляцию если есть вложеннсоть
                        result += (sqlToMSQLS[stri[0]]); // добавляем ключевой оператор в начало
                        List<List<string>> save = new List<List<string>>(); // нужно разбить текущий набор символов на отдельные части разделённые запятой
                        save.Add(new List<string>()); // создаём место под первый набор
                        int index = 0; // индекс набора токенов
                        foreach (string token in structure[structure.IndexOf(stri)].Skip(1)) // обрабатываем каждый токен и создаём структурированный список токенов
                        {
                            if (token == "<zpt>") // если токен это запятая то выделяем новое место под набор, так как текущий закончен
                            {
                                save[index].Add(token); // записываем запятую и переходим на следующий набор
                                index++;// Увеличиваем индекс,
                                save.Add(new List<string>());// создаём новый список под следующий набор

                            }
                            else // если токен не запятая то добавляем как есть
                            {
                                save[index].Add(token);// Добавляем токен в структуру
                            }

                        }// на выходе получаем структурирированный набор токенов из ORDER BY. Далее ищем в наборе NULLS, если таковой имеется то меняем весь набор, если нет, то оставляем как есть
                        foreach(List<string> kit in save)
                        {
                            if (kit.Contains("<nulls>")) // если нашли NULLS то трансформируем его
                            {
                                result += "CASE WHEN " + kit[0] + " " ; // добавляем начало
                                if (kit.Contains("<last>")) // если конструкция last то находим его индекс и вставляем трансформацию. Последний елемент выбрать нельзя так как последним может быть токен запятой
                                {
                                    result += sqlToMSQLS[kit[kit.IndexOf("<last>")]] ;
                                }
                                else
                                {
                                    result += sqlToMSQLS[kit[kit.IndexOf("<firs>")]] ;
                                } //далее нужно добавить ASC либо DESC если таковые имеются
                                result += kit[0] + " "; // добавляем идентификатор для сортировки
                                if (kit.Contains("<ask>")) result += "ASK";
                                if (kit.Contains("<desc>")) result += "DESC";
                                if (kit.Contains("<zpt>")) result += ", ";
                            }
                            else // если NULLS нет значит записываем как есть
                            {
                                for (int i = 0; i < kit.Count();i++)
                                {
                                    if (sqlToMSQLS.ContainsKey(kit[i])) // если токен присутсвует в словаре
                                    {
                                        result += sqlToMSQLS[kit[i]]; // тогда преобразовываем его 
                                    }
                                    else
                                    {
                                        result += kit[i] + " "; //иначе оставляем как есть
                                    }
                                }
                            }
                        }
                        break;
                    case "<lim>": 
                        if (lan == 0) // если вложенности нет и встретился лимит, который уже преобразован в топ, нужно вставить точку с запятой, так как она была пропущена в селекте
                        {
                            result += ";";
                        }
                        break;


                }


            }
            return result;
        }
        private Dictionary<string, string> sqlToMSQLS;//словарь токенов и операторов MSQLS
        private StringBuilder query;
        private string limitValue; // значение для top
        private string tab = "      "; // табуляция для вложенности
        private int lan = 0; // уровень вложеннсоти

    }
}

