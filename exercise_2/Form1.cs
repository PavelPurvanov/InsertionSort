using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace exercise_2
{
    public partial class Form1 : Form
    {

        string myString;
        List<string> myList;
      
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var streamReader = new StreamReader(@"C:\Users\boysa\Documents\Visual Studio 2012\Projects\exercise_2_17621736_SIT\stringFile.txt", Encoding.UTF8))
            {
                myString = streamReader.ReadToEnd();
            }

            label1.Text = myString;
        }

        public void separation() {

            Stopwatch MyTimer = new Stopwatch();

            MyTimer.Start();

            myList = myString.ToLower().Split(new[] { ' ', ',', '.', '?', '!', ':', '\t', '(', ')', '/', '-', '"', '“', '„' }).ToList();
            
            string[] wordToShow = { "и", "или", "пред", "преди", "от", "до", "зад", "на", "защо", "нали", "са", "е", "се", "може", "към", "при" };
            foreach (string var in myList)
            {
                for (int i = 0; i < wordToShow.Length; i++)
                {
                    if (var == wordToShow[i])
                    {
                        textBox8.Text += System.Environment.NewLine + var.ToString();
                    }
                }
            }

            myList.RemoveAll(x => x.Length <= 2);

            foreach (string var in myList)
            {
                textBox4.Text += System.Environment.NewLine + var.ToString();
            }

            MyTimer.Stop();
            textBox1.Text = MyTimer.Elapsed.ToString();
            

        }

        public void sorting()
        {

            int i, j;
            int counter = 0;
            Stopwatch MyTimer = new Stopwatch();

            MyTimer.Start();

            for (i = 1; i < myList.Count; i++)
            {
               var value = myList[i];
                j = i - 1;
                while ((j >= 0) && (myList[j].CompareTo(value) > 0))
                {
                    myList[j + 1] = myList[j];
                    j--;
                }
                myList[j + 1] = value;
            }

            
            MyTimer.Stop();
            textBox2.Text = MyTimer.Elapsed.ToString();

            foreach (string var in myList)
            {
                counter++;
                textBox5.Text += System.Environment.NewLine + var.ToString();
                
            }
            textBox6.Text = counter.ToString();
        }

        public void duplication() {

            int counter = 0;
            Stopwatch MyTimer = new Stopwatch();

            MyTimer.Start();

            var groups = myList.GroupBy(v => v);
            foreach (var group in groups)
            {
                counter++;
                var index = this.dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = group.Key;
                dataGridView1.Rows[index].Cells[1].Value = group.Count();
            }
            textBox3.Text = MyTimer.Elapsed.ToString();

            MyTimer.Stop();

            textBox7.Text = counter.ToString();
        
        
        }

        private void button1_scanner_Click(object sender, EventArgs e)
        {
            try
            {
                separation();
            }
            catch (Exception ex)
            {
                msgBox(ex);
            }
        }

        private void button2_sort_Click(object sender, EventArgs e)
        {
            try
            {
                sorting();
            }
            catch (Exception ex)
            {
                msgBox(ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
            duplication();
            }
            catch (Exception ex)
            {
                msgBox(ex);
            }
        }

        private void msgBox(Exception ex)
        {

            var dialogTypeName = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg";
            var dialogType = typeof(Form).Assembly.GetType(dialogTypeName);
            var dialog = (Form)Activator.CreateInstance(dialogType, new PropertyGrid());

            dialog.Text = "Error Box";
            dialogType.GetProperty("Details").SetValue(dialog, ex.ToString(), null);
            dialogType.GetProperty("Message").SetValue(dialog, "An exception has been thrown !", null);

            var result = dialog.ShowDialog();
        }

    }
}
