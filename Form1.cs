using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Конвертер
{
    public partial class Form1 : Form
    {
        Parser pars;
        Result kod_builder;

        public Form1()
        {
            InitializeComponent();
            pars = new Parser(); // инициализация анализатора
            kod_builder = new Result();
        }

        private void convert_Click(object sender, EventArgs e)
        {
            pars = new Parser();
            console.Text = ""; // очищаем консоль
            MSQLS.Text = ""; // очищаем форму вывода результата

            StringBuilder resultBuilder = new StringBuilder(); // Используем StringBuilder для накопления результатов

            foreach (var item in Postgres.Lines) // обрабатываем каждую строку входного кода
            {
                string result = pars.Analizator(item.ToString());
                if (!string.IsNullOrEmpty(result) && !(result[0] == '<')) // Если первый символ не <, значит, анализатор нашёл ошибку
                {
                    console.Text = result; // Сообщаем об ошибке
                    break;
                }
                resultBuilder.Append(result); // Добавляем результат в StringBuilder
            }

            string allItems = resultBuilder.ToString(); // Получаем все результаты
            MSQLS.Text = kod_builder.Convert(allItems); // Обновляем выходной текст
        }

        private void Postgres_KeyPress(object sender, KeyPressEventArgs e)
        {
            pars.AutoChange(); // Вы можете оставить эту логику, если она действительно необходима
        }
    }
}
