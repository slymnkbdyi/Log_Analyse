using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Log_Analyse
{
    public partial class Form2 : Form
    {
        private readonly List<string> rows = new List<string>();
        private readonly List<string> rows1 = new List<string>();


        public Form2()
        {
            InitializeComponent();
        }

        public string filepath { get; set; }

        private void checkBox1_2_CheckedChanged(object sender, EventArgs e)
        {
            listBox3.DoubleClick -= listBox3_DoubleClick;


            var frm1 = new Form1();
            if (checkBox1_2.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;

                label2_2.Text = "Please Wait Proccessing  ...";
                listBox3.Items.Clear();

                Application.DoEvents();
                frm1.Analyse(listBox3, listBox3, filepath, 0, 0, 0, 0, "");


                label2_2.Text = "";

                Application.DoEvents();
            }
            else
            {
                listBox3.Items.Clear();
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var click_mesg = listBox3.SelectedItem.ToString();

                var index = click_mesg.IndexOf(' ');
                index = click_mesg.IndexOf(' ', index + 1);
                var f_name = click_mesg.Substring(0, index);

                var index1 = click_mesg.IndexOf("Line") + 5;
                var index2 = click_mesg.IndexOf(' ', index1 + 1);
                var f_line = Convert.ToInt32(click_mesg.Substring(index1, index2 - index1));

                open_text_editor(filepath + "\\" + f_name, f_line);
            }
            catch (Exception)
            {
            }
        }

        private void open_text_editor(string filepath, int line)
        {
            try
            {
                var nppDir = (string) Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Notepad++", null, null);
                var nppExePath = Path.Combine(nppDir, "Notepad++.exe");
                var nppReadmePath = Path.Combine(nppDir, filepath);

                var sb = new StringBuilder();
                sb.AppendFormat("\"{0}\" -n{1}", nppReadmePath, line);
                Process.Start(nppExePath, sb.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Notepad++ not install");
            }
        }

        private void listBox4_DoubleClick(object sender, EventArgs e)
        {
            var click_mesg1 = listBox3.SelectedItem.ToString();

            var index5 = click_mesg1.IndexOf(' ');
            index5 = click_mesg1.IndexOf(' ', index5 + 1);
            var f_name = click_mesg1.Substring(0, index5);

            var index3 = click_mesg1.IndexOf("Line") + 5;
            var index4 = click_mesg1.IndexOf(' ', index3 + 1);
            var f_line1 = Convert.ToInt32(click_mesg1.Substring(index3, index4 - index3));

            open_text_editor(filepath + "\\" + f_name, f_line1);
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var frm1 = new Form1();

            listBox3.DoubleClick += listBox3_DoubleClick;
            if (checkBox1.Checked)
            {
                checkBox1_2.Checked = false;

                label2_2.Text = "Please Wait Proccessing  ...";
                listBox3.Items.Clear();

                Application.DoEvents();
                if (checkBox2.Checked)
                    frm1.Analyse(listBox3, listBox3, filepath, 2, 1, 1, 0, "");
                else
                    frm1.Analyse(listBox3, listBox3, filepath, 2, 1, 0, 0, "");

                label2_2.Text = "";

                Application.DoEvents();
            }
            else
            {
                label2_2.Text = "Please Wait Proccessing  ...";
                listBox3.Items.Clear();

                Application.DoEvents();

                if (checkBox2.Checked)
                    frm1.Analyse(listBox3, listBox3, filepath, 2, 0, 1, 0, "");
                label2_2.Text = "";

                Application.DoEvents();
            }

            var index_list1 = new List<string>();
            if (checkedListBox1.Items.Count != 0)
            {
                for (var j = 0; j < checkedListBox1.Items.Count; j++)
                    if (checkedListBox1.GetItemChecked(j) == false)
                        foreach (var lisitem in listBox3.Items)
                        {
                            var itm = checkedListBox1.Items[j].ToString();
                            var line = lisitem.ToString();
                            if (line.Contains(itm))
                                index_list1.Add(line);
                        }


                for (var i = 0; i < index_list1.Count; i++)
                    listBox3.Items.Remove(index_list1[i]);
            }

            Application.DoEvents();
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            var frm1 = new Form1();
            listBox3.DoubleClick += listBox3_DoubleClick;
            if (checkBox2.Checked)
            {
                checkBox1_2.Checked = false;

                label2_2.Text = "Please Wait Proccessing  ...";
                listBox3.Items.Clear();

                Application.DoEvents();
                if (checkBox1.Checked)
                    frm1.Analyse(listBox3, listBox3, filepath, 2, 1, 1, 0, "");
                else
                    frm1.Analyse(listBox3, listBox3, filepath, 2, 0, 1, 0, "");

                label2_2.Text = "";

                Application.DoEvents();
            }
            else
            {
                label2_2.Text = "Please Wait Proccessing  ...";
                listBox3.Items.Clear();

                Application.DoEvents();

                if (checkBox1.Checked)
                    frm1.Analyse(listBox3, listBox3, filepath, 2, 1, 0, 0, "");
                label2_2.Text = "";

                Application.DoEvents();
            }
            var index_list = new List<string>();
            if (checkedListBox1.Items.Count != 0)
            {
                for (var i = 0; i < checkedListBox1.Items.Count; i++)
                    if (checkedListBox1.GetItemChecked(i) == false)
                        foreach (var lisitem in listBox3.Items)
                        {
                            var itm = checkedListBox1.Items[i].ToString();
                            var line = lisitem.ToString();
                            if (line.Contains(itm))
                                index_list.Add(line);
                        }


                for (var i = 0; i < index_list.Count; i++)
                    listBox3.Items.Remove(index_list[i]);
            }

            Application.DoEvents();
        }

       

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            var firstCheck_state = checkBox1.Checked;
            var secCheck_state = checkBox2.Checked;
            if (textBox1.Text == "")
            {
                if (checkBox1.Checked & checkBox2.Checked)
                {
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox1.Checked = true;
                    checkBox2.Checked = true;
                }
                if (checkBox1.Checked & (checkBox2.Checked == false))
                {
                    checkBox1.Checked = false;
                    checkBox1.Checked = true;
                }
                if ((checkBox1.Checked == false) & checkBox2.Checked)
                {
                    checkBox2.Checked = false;
                    checkBox2.Checked = true;
                }
                if ((checkBox1.Checked == false) & (checkBox2.Checked == false))
                {
                }
            }

            if (textBox1.Text != "")
            {
                rows1.Clear();
                listBox3.SelectedItems.Clear();
                var searchArg = textBox1.Text;

                var index = listBox3.FindString(searchArg, -1);


                foreach (string item in listBox3.Items)
                    if (item.Contains(searchArg))
                        rows1.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.Visible == false)
            {
                checkedListBox1.Visible = true;


                if (checkedListBox1.Items.Count == 0)
                {
                    var first_state = checkBox1.Checked;
                    var sec_state = checkBox2.Checked;
                    checkBox1_2.Checked = true;
                    foreach (string item in listBox3.Items)
                        rows.Add(item);
                    foreach (var item in listBox3.Items)
                    {
                        var value = item.ToString();
                        if (value.Contains("ERR"))
                            if (checkBox3.Checked)
                                checkedListBox1.Items.Add(item);
                        if (value.Contains("WRN"))
                            if (checkBox4.Checked)
                                checkedListBox1.Items.Add(item);
                    }
                    for (var i = 0; i < checkedListBox1.Items.Count; i++)
                        checkedListBox1.SetItemChecked(i, true);

                    checkBox1.Checked = first_state;
                    checkBox2.Checked = sec_state;
                }
            }
            else
            {
                checkedListBox1.Visible = false;
            }
        }

       

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void checkedListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label2_2.Text = "Please Wait Proccessing  ...";
            if (checkBox1.Checked & checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox1.Checked = true;
                checkBox2.Checked = true;
            }
            if (checkBox1.Checked & (checkBox2.Checked == false))
            {
                checkBox1.Checked = false;
                checkBox1.Checked = true;
            }
            if ((checkBox1.Checked == false) & checkBox2.Checked)
            {
                checkBox2.Checked = false;
                checkBox2.Checked = true;
            }
            if ((checkBox1.Checked == false) & (checkBox2.Checked == false))
            {
            }

            label2_2.Text = "";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            bool first_checkbox1, sec_checkbox2;
            first_checkbox1 = checkBox1.Checked;
            sec_checkbox2 = checkBox2.Checked;


            checkedListBox1.Items.Clear();
            foreach (var item in rows)
            {
                var value = item;
                if (checkBox3.Checked)
                {
                    if (value.Contains("ERR"))
                    {
                        checkedListBox1.Items.Add(item);
                    }
                    else
                    {
                        if (checkBox4.Checked)
                            checkedListBox1.Items.Add(item);
                    }
                }

                else
                {
                    if (value.Contains("WRN"))
                        if (checkBox4.Checked)
                            checkedListBox1.Items.Add(item);
                }
            }


            for (var i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
            checkBox1.Checked = first_checkbox1;
            checkBox2.Checked = sec_checkbox2;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            bool first_checkbox1, sec_checkbox2;
            first_checkbox1 = checkBox1.Checked;
            sec_checkbox2 = checkBox2.Checked;


            checkedListBox1.Items.Clear();
            foreach (var item in rows)
            {
                var value = item;
                if (checkBox4.Checked)
                {
                    if (value.Contains("WRN"))
                    {
                        checkedListBox1.Items.Add(item);
                    }
                    else
                    {
                        if (checkBox3.Checked)
                            checkedListBox1.Items.Add(item);
                    }
                }

                else
                {
                    if (value.Contains("ERR"))
                        if (checkBox3.Checked)
                            checkedListBox1.Items.Add(item);
                }
            }


            for (var i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
            checkBox1.Checked = first_checkbox1;
            checkBox2.Checked = sec_checkbox2;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (listBox3.Items.Count == 0)
            {
                MessageBox.Show("List is Empty");
            }
            else
            {
                var firstCheck_state = checkBox1.Checked;
                var secCheck_state = checkBox2.Checked;

                listBox3.Items.Clear();
                foreach (var item in rows1)
                    listBox3.Items.Add(item);
                if (listBox3.Items.Count == 0)
                {
                    MessageBox.Show("Not Found");
                    checkBox1.Checked = firstCheck_state;
                    checkBox2.Checked = secCheck_state;
                    textBox1.Text = "";
                    if (checkBox1.Checked & checkBox2.Checked)
                    {
                        checkBox1.Checked = false;
                        checkBox2.Checked = false;
                        checkBox1.Checked = true;
                        checkBox2.Checked = true;
                    }
                    if (checkBox1.Checked & (checkBox2.Checked == false))
                    {
                        checkBox1.Checked = false;
                        checkBox1.Checked = true;
                    }
                    if ((checkBox1.Checked == false) & checkBox2.Checked)
                    {
                        checkBox2.Checked = false;
                        checkBox2.Checked = true;
                    }
                    if ((checkBox1.Checked == false) & (checkBox2.Checked == false))
                    {
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                string f_path;
                var path = filepath.Substring(0, filepath.Length - 1);

                listBox3.Items.Clear();

                label17.Text = "Waiting to Search";
                Refresh();
                var length = Directory.GetFiles(path).Length;

                for (var j = 0; j < length; j++)
                {
                    var frm1 = new Form1();
                    f_path = path + j;
                    frm1.Analyse(listBox3, listBox3, f_path, 3, 3, 3, 3, textBox2.Text);
                }
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                label17.Text = "Finish to Search";
                Refresh();
            }
            if (listBox3.Items.Count == 0)
                MessageBox.Show("Not Found");
        }
    }
}