namespace Конвертер
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            console = new TextBox();
            label1 = new Label();
            convert = new Button();
            Postgres = new RichTextBox();
            MSQLS = new RichTextBox();
            SuspendLayout();
            // 
            // console
            // 
            console.Location = new Point(87, 422);
            console.Margin = new Padding(3, 2, 3, 2);
            console.Name = "console";
            console.Size = new Size(1371, 23);
            console.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(88, 400);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 3;
            label1.Text = "Консоль";
            // 
            // convert
            // 
            convert.Location = new Point(710, 68);
            convert.Margin = new Padding(3, 2, 3, 2);
            convert.Name = "convert";
            convert.Size = new Size(122, 22);
            convert.TabIndex = 4;
            convert.Text = "Конвертировать";
            convert.UseVisualStyleBackColor = true;
            convert.Click += convert_Click;
            // 
            // Postgres
            // 
            Postgres.Location = new Point(88, 68);
            Postgres.Margin = new Padding(3, 2, 3, 2);
            Postgres.Name = "Postgres";
            Postgres.Size = new Size(616, 319);
            Postgres.TabIndex = 6;
            Postgres.Text = "";
            Postgres.KeyPress += Postgres_KeyPress;
            // 
            // MSQLS
            // 
            MSQLS.Location = new Point(838, 68);
            MSQLS.Margin = new Padding(3, 2, 3, 2);
            MSQLS.Name = "MSQLS";
            MSQLS.Size = new Size(620, 319);
            MSQLS.TabIndex = 7;
            MSQLS.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1571, 490);
            Controls.Add(MSQLS);
            Controls.Add(Postgres);
            Controls.Add(convert);
            Controls.Add(label1);
            Controls.Add(console);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox console;
        private Label label1;
        private Button convert;
        private RichTextBox Postgres;
        private RichTextBox MSQLS;
    }
}
