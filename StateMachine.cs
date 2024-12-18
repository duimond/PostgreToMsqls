using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Конвертер
{
    internal class StateMachine
    {
        public StateMachine()
        {
            enclosure = new Dictionary<string, string>();
            AddEnclosure("fr0","fr1");
            AddEnclosure("jo0", "jo1");
            AddEnclosure("wh4", "wh3");
            AddEnclosure("wh5", "wh3");
            AddEnclosure("wh6", "wh3");
            AddEnclosure("or0", "or1");
            this.CurrentState = "0"; // устанавливаем начальное состояние
            transitionTable = new Dictionary<string, string>(); // словарь состояний
            // состояния со знаком "-" не учётные состояния для которых уже не осталось номеров
            // SELECT
            AddTransition("0","<sel>","se0");
            AddTransition("se0", "<dis>", "se5");
            AddTransition("se0", "<sum>", "se9");
            AddTransition("se0", "<min>", "se9");
            AddTransition("se0", "<max>", "se9");
            AddTransition("se0", "<avg>", "se9");
            AddTransition("se0", "<con>", "se9");
            AddTransition("se0", "<alll>", "se4");
            AddTransition("se0", "<all>", "se2");
            AddTransition("se1", "<as>", "se3");
            AddTransition("se1", "<fro>", "fr0");
            AddTransition("se1", "<zpt>", "se4");
            AddTransition("se1", "<all>", "se2");
            AddTransition("se2", "<fro>", "fr0");
            AddTransition("se2", "<zpt>", "se4");
            AddTransition("se4", "<all>", "se2");
            AddTransition("se4", "<sum>", "se9");
            AddTransition("se4", "<min>", "se9");
            AddTransition("se4", "<max>", "se9");
            AddTransition("se4", "<avg>", "se9");
            AddTransition("se4", "<con>", "se9");
            AddTransition("se4", "<subst>", "se9");
            AddTransition("se5", "<all>", "se2");
            AddTransition("se5", "<on>", "se6");
            AddTransition("se6", "<lef>", "se7");
            AddTransition("se8", "<zpt>", "se7");
            AddTransition("se8", "<rig>", "se4");
            AddTransition("se9", "<lef>", "se10");
            AddTransition("se11", "<zpt>", "se10");
            AddTransition("se11", "<rig>", "se12");
            AddTransition("se12", "<zpt>", "se4");
            AddTransition("se12", "<as>", "se3");
            // FROM
            AddTransition("fr0", "<lef>", "0");
            AddTransition("fr1", "<zpt>", "fr0");
            AddTransition("fr1", "<rig>", "null");
            AddTransition("fr1", "<tzpt>", "fin");
            AddTransition("fr1", "<as>", "fr3");
            AddTransition("fr1", "<joi>", "jo0");
            AddTransition("fr1", "<ijo>", "jo0");
            AddTransition("fr1", "<ljo>", "jo0");
            AddTransition("fr1", "<rjo>", "jo0");
            AddTransition("fr1", "<fjo>", "jo0");
            AddTransition("fr1", "<whe>", "wh1");
            AddTransition("fr1", "<gro>", "gr0");
            AddTransition("fr1", "<ord>", "or0");
            AddTransition("fr1", "<lim>", "li0");

            AddTransition("fr2", "<joi>", "jo0");
            AddTransition("fr2", "<ijo>", "jo0");
            AddTransition("fr2", "<ljo>", "jo0");
            AddTransition("fr2", "<rjo>", "jo0");
            AddTransition("fr2", "<fjo>", "jo0");
            AddTransition("fr2", "<whe>", "wh1");
            AddTransition("fr2", "<gro>", "gr0");
            AddTransition("fr2", "<ord>", "or0");
            AddTransition("fr2", "<lim>", "li1");
            AddTransition("fr2", "<rig>", "null");
            AddTransition("fr2", "<zpt>", "fr0");
            AddTransition("fr2", "<tzpt>", "fin");
            // JOIN
            AddTransition("jo0", "<lef>", "0");
            AddTransition("jo1", "<on>", "jo4");
            AddTransition("jo1", "<as>", "jo2");
            AddTransition("jo3", "<on>", "jo4");
            AddTransition("jo5", "<rav>", "jo6");
            AddTransition("jo7", "<joi>", "jo0");
            AddTransition("jo7", "<ijo>", "jo0");
            AddTransition("jo7", "<ljo>", "jo0");
            AddTransition("jo7", "<rjo>", "jo0");
            AddTransition("jo7", "<fjo>", "jo0");
            AddTransition("jo7", "<rig>", "null");
            AddTransition("jo7", "<tzpt>", "fin");
            AddTransition("jo7", "<whe>", "wh1");
            AddTransition("jo7", "<ord>", "or0");
            AddTransition("jo7", "<gro>", "gr0");
            AddTransition("jo7", "<lim>", "li0");
            // WHERE
            AddTransition("wh1", "<not>", "wh2");

            AddTransition("wh3", "<not>", "wh2");
            AddTransition("wh3", "<men>", "wh4");
            AddTransition("wh3", "<rav>", "wh5");
            AddTransition("wh3", "<bol>", "wh6");
            AddTransition("wh3", "<is>", "wh17");
            AddTransition("wh3", "<bwn>", "wh7");
            AddTransition("wh3", "<lik>", "wh10");
            AddTransition("wh3", "<ilike>", "wh10");
            AddTransition("wh3", "<in>", "wh11");
            AddTransition("wh3", "<and>", "wh1");
            AddTransition("wh3", "<or>", "wh1");
            AddTransition("wh3", "<vos>", "wh16");
            AddTransition("wh3", "<til>", "wh14");
            AddTransition("wh3", "<rig>", "null");
            AddTransition("wh3", "<gro>", "gr0");
            AddTransition("wh3", "<ord>", "or0");
            AddTransition("wh3", "<lim>", "li0");

            AddTransition("wh19", "<not>", "wh2");
            AddTransition("wh19", "<men>", "wh4");
            AddTransition("wh19", "<rav>", "wh5");
            AddTransition("wh19", "<bol>", "wh6");
            AddTransition("wh19", "<is>", "wh17");
            AddTransition("wh19", "<bwn>", "wh7");
            AddTransition("wh19", "<lik>", "wh10");
            AddTransition("wh19", "<ilike>", "wh10");
            AddTransition("wh19", "<in>", "wh11");
            AddTransition("wh19", "<and>", "wh1");
            AddTransition("wh19", "<or>", "wh1");
            AddTransition("wh19", "<vos>", "wh16");
            AddTransition("wh19", "<til>", "wh14");
            AddTransition("wh19", "<rig>", "null");
            AddTransition("wh19", "<gro>", "gr0");
            AddTransition("wh19", "<ord>", "or0");
            AddTransition("wh19", "<lim>", "li0");
            AddTransition("wh19", "<tzpt>", "li0");

            AddTransition("wh4", "<bol>", "wh6");
            AddTransition("wh4", "<rav>", "wh5");
            AddTransition("wh4", "<lef>", "0");
            AddTransition("wh5", "<lef>", "0");
            AddTransition("wh6", "<lef>", "0");
            AddTransition("wh8", "<and>", "wh9");
            AddTransition("wh11", "<lef>", "wh12");
            AddTransition("wh13", "<zpt>", "wh12");
            AddTransition("wh13", "<rig>", "wh19");
            AddTransition("wh14", "<all>", "wh15");
            AddTransition("wh16", "<til>", "wh14");
            AddTransition("wh17", "<not>", "wh18");
            AddTransition("wh17", "<null>", "wh19");
            AddTransition("wh18", "<null>", "wh19");
            // GROUP BY
            AddTransition("gr0", "<lef>", "gr4");
            AddTransition("gr0", "<cub>", "gr3");
            AddTransition("gr0", "<rol>", "gr3");
            AddTransition("gr3", "<lef>", "gr4");
            AddTransition("gr5", "<zpt>", "gr4");
            AddTransition("gr5", "<rig>", "gr6");
            AddTransition("gr6", "<rig>", "null");
            AddTransition("gr6", "<tzpt>", "fin");
            AddTransition("gr6", "<lim>", "li0");
            AddTransition("gr6", "<ord>", "or0");
            //ORDER BY
            AddTransition("or0", "<lef>", "0");
            AddTransition("or1", "<rig>", "null");
            AddTransition("or1", "<asc>", "or4");
            AddTransition("or1", "<desc>", "or4");
            AddTransition("or1", "<tzpt>", "fin");
            AddTransition("or1", "<nulls>", "or2");
            AddTransition("or1", "<lim>", "li0");
            AddTransition("or1", "<zpt>", "or0");
            AddTransition("or2", "<firs>", "or3");
            AddTransition("or2", "<last>", "or3");
            AddTransition("or3", "<zpt>", "or0");
            AddTransition("or3", "<tzpt>", "fin");
            AddTransition("or3", "<lim>", "li0");
            AddTransition("or3", "<rig>", "null");
            AddTransition("or4", "<zpt>", "or0");
            AddTransition("or4", "<nulls>", "or2");
            AddTransition("or4", "<lim>", "li0");
            AddTransition("or4", "<tzpt>", "fin");
            AddTransition("or4", "<rig>", "null");
            // LIMIT
            AddTransition("li0", "<alll>", "li1");
            AddTransition("li1", "<rig>", "null");
            AddTransition("li1", "<tzpt>", "fin");

        }


        private void AddTransition(string сurrentState, string token, string NextState) // добавляет новые возможные переходы в словарь
        {
            string key = сurrentState + token; // совмещаем состояние и полученный токен
            transitionTable.Add(key, NextState); //добавляем в автомат новый переход
        }
        private void AddEnclosure(string сurrentState, string NextState) // добавляет новые возможные переходы в словарь
        {
            enclosure.Add(сurrentState, NextState); //добавляем в словарь вложенности новый переход
        }


        public bool ProcessInput(string token) //проверяет возможность перехода и записывает новое состояние автомата (Для обычных токенов, не для не определенных токенов)
        {
            string key = CurrentState + token; // совмещаем состояние и полученный токен
            if (transitionTable.ContainsKey(key)) // Если словарь содержит текущее состояние и входной токен, то
            {
                if (transitionTable[key] == "0") 
                { 
                    attached_requests.Add(enclosure[CurrentState]); 
                }
                if (transitionTable[key] == "fin" && attached_requests.Count() > 0) // если команда на конец запроса, но ещё есть вложенность, то ошибка
                {
                    return false;
                }
                if (transitionTable[key] == "null" && attached_requests.Count() == 0) // если закрываем подзапрос ,но вложенности нет то ошибка
                {
                    return false;
                }
                CurrentState = transitionTable[key]; // записываем новое состояние

                if (transitionTable[key] == "null" && attached_requests.Count() > 0) // если закрываем подзапрос и есть то что можно закрыть, то
                {
                    CurrentState = attached_requests[attached_requests.Count - 1]; // возвращаемся в место откуда начали
                    attached_requests.RemoveAt(attached_requests.Count - 1); // удаляем запись о вложенности

                }
                return true;
            }
            else // если состояния и/или токена нет, то возвращаем false
            {
                return false;
            }
        }
        public bool VariablePossibility() //проверяет возможность установки переменной
        {
            string[] allowedStates = {
                "se0", "se1", "se3", "se4", "se5", "se7", "se10", "se12",
                "fr0", "fr1", "fr3",
                "jo0", "jo1", "jo2", "jo4", "jo6",
                "wh1", "wh2", "wh4", "wh5", "wh6", "wh7", "wh9", "wh10", "wh12", "wh14", "wh15",
                "gr0", "gr4",
                "or0",
                "li0"
            };
            if (Array.Exists(allowedStates, state => state == CurrentState)) // записываем все состояния допускающие написание переменной, в будущем надо ещё осуществить переход на новое состояние в зависимости от логики переходов
            {
                switch(CurrentState)
                {
                    case ("se0"):CurrentState = "se1";break;
                    case ("se1"):CurrentState = "se2";break;
                    case ("se3"):CurrentState = "se2";break;
                    case ("se4"):CurrentState = "se1";break;
                    case ("se5"):CurrentState = "se1";break;
                    case ("se7"):CurrentState = "se8";break;
                    case ("se10"):CurrentState = "se11";break;
                    case ("se12"):CurrentState = "se2";break;
                    case ("fr0"):CurrentState = "fr1";break;
                    case ("fr1"):CurrentState = "fr2";break;
                    case ("fr3"):CurrentState = "fr2";break;
                    case ("jo0"):CurrentState = "jo1";break;
                    case ("jo1"): CurrentState = "jo3"; break;
                    case ("jo2"): CurrentState = "jo3"; break;
                    case ("jo4"): CurrentState = "jo5"; break;
                    case ("jo6"): CurrentState = "jo7"; break;
                    case ("wh1"): CurrentState = "wh19"; break;
                    case ("wh2"): CurrentState = "wh19"; break;
                    case ("wh4"): CurrentState = "wh19"; break;
                    case ("wh5"): CurrentState = "wh19"; break;
                    case ("wh6"): CurrentState = "wh19"; break;
                    case ("wh7"): CurrentState = "wh8"; break;
                    case ("wh9"): CurrentState = "wh19"; break;
                    case ("wh10"): CurrentState = "wh19"; break;
                    case ("wh12"): CurrentState = "wh13"; break;
                    case ("wh14"): CurrentState = "wh19"; break;
                    case ("wh15"): CurrentState = "wh19"; break;
                    case ("gr0"): CurrentState = "gr6"; break;
                    case ("gr4"): CurrentState = "gr5"; break;
                    case ("or0"): CurrentState = "or1"; break;
                    case ("li0"): CurrentState = "li1"; break;

                }
                return true;
            }
            else
            { 
                return false; 
            }
        }
        public string getstate()
        {
            return this.CurrentState;
        }
        private Dictionary<string, string> transitionTable; // словарь состояний
        private Dictionary<string, string> enclosure; // словарь вложенности
        private string CurrentState; // текущее состояние
        private List<string> attached_requests = new List<string>();
    }
}
