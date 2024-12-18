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
            pars = new Parser(Postgres); // инициалицируем анализатор
            kod_builder = new Result(MSQLS);
        }


        private void convert_Click(object sender, EventArgs e)
        {
            pars = new Parser(Postgres); // Очищаем анализатор для новой итерации
            console.Text = ""; //очищаем консоль
            MSQLS.Text=""; // очищаем форму вывода результат
            foreach (var items in Postgres.Lines) // обрабатываем каждую строку входного кода
            {
                string result = pars.Analizator(items.ToString());
                if (result != "" && !(result[0] == '<')) // Если первый символ не < значит, анализатор нашёл ошибку и нужно прекратить выполнение 
                {
                    console.Text = result;
                    break;
                }
                MSQLS.Text += (result);

            }

            string allItems = string.Join(" ", MSQLS.Lines);

            MSQLS.Text = kod_builder.Convert(allItems);

            


        }
        private void Postgres_KeyPress(object sender, KeyPressEventArgs e)
        {
            pars.AutoChange();
        }
    }
}
