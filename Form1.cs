using System.IO;
using System.Windows.Forms;

namespace Denisenko_KursProject
{
    public partial class Form1 : Form
    {
        string text = "";
        public Form1()
        {
            InitializeComponent();
           // errorView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveLexemButton_Click(object sender, EventArgs e)
        {
            errorView.Items.Clear();
            
            LexicalAnalisator lexicalAnalisator = new LexicalAnalisator(richTextBox1.Text);

            List<Error> errors = new List<Error>();
            errors = lexicalAnalisator.errorsList;
            foreach (Error error in errors)
            {
                ListViewItem listViewItem = new ListViewItem(error.type.ToString());
                //listViewItem.SubItems.Add(error.type.ToString());
                listViewItem.SubItems.Add(error.row.ToString());
                listViewItem.SubItems.Add(error.message.ToString());
                errorView.Items.Add(listViewItem);


            }
            if (errorView.Items.Count == 0)
            {

                FolderBrowserDialog folderBrowserDialog = new();
                var result = folderBrowserDialog.ShowDialog();
                string path = "";
                if (result == DialogResult.OK)
                {
                    path = folderBrowserDialog.SelectedPath += "\\lexem.txt";
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        List<string> lexems = new();
                        lexems = lexicalAnalisator.GetNewLexemList();
                        foreach (string str in lexems)
                        {
                            sw.WriteLine(str);
                        }
                    }
                    MessageBox.Show($"Успешно проведён лексический анализ, результат записан в файл {path}");


                }

            }

            

            
        }

        private void loadCodeButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            var result = openFileDialog.ShowDialog();
            string text="";
            Stream stream = Stream.Null;
            if (result == DialogResult.OK)
            {
                stream = openFileDialog.OpenFile();
            }
            using (StreamReader sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    text+=sr.ReadLine();
                    text += '\n';
                }
            }
            richTextBox1.Text = text;
        }

        private void clearTextBoxButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            errorView.Items.Clear();
        }

        private void savePostfixButton_Click(object sender, EventArgs e)
        {
            List<Error> errors = new List<Error>();
            MessageBox.Show("Выберите файл лексем!");
            OpenFileDialog openFileDialog = new();
            var result = openFileDialog.ShowDialog();
            string text = "";
            Stream stream = Stream.Null;
            if (result == DialogResult.OK)
            {
                stream = openFileDialog.OpenFile();
            }
            
            using (StreamReader sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    text += sr.ReadLine();
                    text += '\n';
                }
            }

            SyntaxAnalisator syntaxAnalisator = new SyntaxAnalisator(text);
            errors = syntaxAnalisator.errors;
            foreach (Error error in errors)
            {
                ListViewItem listViewItem = new ListViewItem(error.type.ToString());
                //listViewItem.SubItems.Add(error.type.ToString());
                listViewItem.SubItems.Add(error.row.ToString());
                listViewItem.SubItems.Add(error.message.ToString());
                errorView.Items.Add(listViewItem);


            }

            if (errorView.Items.Count == 0)
            {
                PostfixGenerator postfixGenerator = new PostfixGenerator(syntaxAnalisator.LexemList);
                FolderBrowserDialog folderBrowserDialog = new();
                var result1 = folderBrowserDialog.ShowDialog();
                string path = "";
                if (result1 == DialogResult.OK)
                {
                    path = folderBrowserDialog.SelectedPath += "\\lexem_postfix.txt";
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        List<string> lexems = new();
                        lexems = postfixGenerator.GetPostfixLexemList();
                        foreach (string str in lexems)
                        {
                            sw.WriteLine(str);
                        }
                    }
                    MessageBox.Show($"Успешно проведена генерация постфиксного кода, результат записан в файл {path}");


                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
                
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            errorView.Columns[0].Width= (int)Math.Round(errorView.Width*0.2);
            errorView.Columns[1].Width = (int)Math.Round(errorView.Width * 0.1);
            errorView.Columns[2].Width = (int)Math.Round(errorView.Width * 0.7);



        }

        private void saveMnemocodeButton_Click(object sender, EventArgs e)
        {
            List<Error> errors = new List<Error>();
            MessageBox.Show("Выберите файл постфиксного кода!");
            OpenFileDialog openFileDialog = new();
            var result = openFileDialog.ShowDialog();
            string text = "";
            Stream stream = Stream.Null;
            if (result == DialogResult.OK)
            {
                stream = openFileDialog.OpenFile();
            }

            using (StreamReader sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    text += sr.ReadLine();
                    text += '\n';
                }
            }

            CodeGenerator codeGenerator = new CodeGenerator(text);
            
            
            

        }
    }
}