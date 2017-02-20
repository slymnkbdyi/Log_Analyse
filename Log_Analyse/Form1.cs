using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using Microsoft.Win32;

namespace Log_Analyse
{
    public partial class Form1 : Form
    {
        private readonly string destination = @"" + Path.GetTempPath() + "Log_Process_Fıle";
        private string Chosen_File, Chosen_File_tar_gz;


        private int error_count, error_without_repeat, wrn_count, wrn_without_repeat, line_count;
        public Form2 frm2;

        private string search_path;

        public Form1()
        {
            InitializeComponent();
            frm2 = new Form2();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var frm2 = new Form2();
            frm2.label2_2.Text = "Please select procedure you would like to perform   ...";
            frm2.Show();
            var mes_path = destination + "\\" + 3;

            Application.DoEvents();

            frm2.filepath = mes_path;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm2 = new Form2();
            frm2.label2_2.Text = "Please select procedure you would like to perform   ...";
            frm2.Show();
            var mes_path = destination + "\\" + 0;

            Application.DoEvents();

            frm2.filepath = mes_path;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var frm2 = new Form2();
            frm2.label2_2.Text = "Please select procedure you would like to perform  ...";
            frm2.Show();
            var mes_path = destination + "\\" + 1;

            frm2.filepath = mes_path;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var frm2 = new Form2();
            frm2.label2_2.Text = "Please select procedure you would like to perform  ...";
            frm2.Show();
            var mes_path = destination + "\\" + 2;

            frm2.filepath = mes_path;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm2 = new Form2();
            frm2.label2_2.Text = "Please select procedure you would like to perform  ...";
            frm2.Show();
            var mes_path = destination + "\\" + 5;

            frm2.filepath = mes_path;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var frm2 = new Form2();

            frm2.label2_2.Text = "Please select procedure you would like to perform  ...";
            frm2.Show();
            var mes_path = destination + "\\" + 6;

            frm2.filepath = mes_path;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Directory.Exists(destination))
                Directory.Delete(destination, true);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            var frm2 = new Form2();

            frm2.Show();
            var mes_path = destination + "\\" + 4;

            frm2.filepath = mes_path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(destination))
                Directory.Delete(destination, true);

            error_count = 0;
            wrn_count = 0;
            error_without_repeat = 0;
            wrn_without_repeat = 0;
            line_count = 0;
            /* İşlenecek Dosyanın secilmesini saglayan kısım */
            var file = new OpenFileDialog();
            file.FileName = "";


            if (file.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("Please Select File");
            }

            else
            {
                Chosen_File = file.FileName;
                file.InitialDirectory = Path.GetDirectoryName(Chosen_File);
                if (Chosen_File.Contains(".dat") || Chosen_File.Contains(".zip"))
                {
                    /* Listboxlarda  ve labellarda önceden kalan veri yada eleman varsa temziliyoruz */
                    label11.Text = "";
                    label1.Text = "";
                    label8.Text = "";
                    label2.Text = "";
                    label9.Text = "";
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    label1.Text = "";
                    label2.Text = "";
                    label5.Text = "Please Wait Proccessing  ...";
                    Application.DoEvents();
                    /* Secilen Dosyanın adını uzantısını degiştirdik */

                    Chosen_File_tar_gz = Path.ChangeExtension(Chosen_File, ".zip");


                    var index1 = Chosen_File_tar_gz.IndexOf(".") + 1;
                    var index2 = Chosen_File_tar_gz.IndexOf('.', index1 + 1);
                    var f_device = Convert.ToInt32(Chosen_File_tar_gz.Substring(index1, index2 - index1));
                    label14.Text = "Vehicle ID : " + f_device;


                    File.Move(Chosen_File, Chosen_File_tar_gz);

                    /* Tar.gz li dosyayı dısarı cıkarıyoruz */
                    Decompres(Chosen_File_tar_gz, destination);
                    Chosen_File = Path.ChangeExtension(Chosen_File_tar_gz, ".dat");

                    File.Move(Chosen_File_tar_gz, Chosen_File);
                    /* Bu Çıkan Dosyanın içindeki tar.gz li log dosyalarınıda Extract ediyoruz */
                    var fileNames = Directory.GetFiles(destination);
                    var i = 0;
                    foreach (var FileName in fileNames)
                    {
                        var msj = destination + "\\" + i;
                        Decompres(FileName, msj);
                        i++;
                    }


                    /* Sırayla ilk cihazın log dosyalarından itibaren analiz etmek için dosyayı okuyup işlemeye başlıyoruz */
                    var length = Directory.GetFiles(destination).Length;

                    for (var j = 0; j < length; j++)
                    {
                        var mes_path = destination + "\\" + j;
                        Analyse(listBox1, listBox2, mes_path, 0, 0, 0, 0, "");
                    }
                    label5.Text = "";

                    label1.Text = error_count.ToString();
                    label2.Text = wrn_count.ToString();
                    label8.Text = error_without_repeat.ToString();
                    label9.Text = wrn_without_repeat.ToString();
                    label11.Text = line_count.ToString();

                    frm2.textBox2.Visible = true;
                    frm2.button3.Visible = true;
                    frm2.label17.Visible = true;
                    groupBox2.Visible = true;
                    if (length == 4)
                        button7.Visible = false;
                    if (length == 3)
                    {
                        button7.Visible = false;
                        button6.Visible = false;
                    }
                    if (length == 2)
                    {
                        button7.Visible = false;
                        button6.Visible = false;
                        button5.Visible = false;
                    }
                    if (length == 1)
                    {
                        button7.Visible = false;
                        button6.Visible = false;
                        button5.Visible = false;
                        button4.Visible = false;
                    }

                    Application.DoEvents();
                }
                else
                {
                    MessageBox.Show("Please Select .dat or .tar.gz file ");
                }
            }
        }

        private void Decompres(string source, string destination)
        {
            /* tar.gz dosyasını klasöre cıkartıyoruz */
            var tarFileInfo = new FileInfo(source);
            var targetDirectory = new DirectoryInfo(destination);
            if (!targetDirectory.Exists)
                targetDirectory.Create();
            using (Stream sourceStream = new GZipInputStream(tarFileInfo.OpenRead()))
            {
                using (
                    var tarArchive = TarArchive.CreateInputTarArchive(sourceStream, TarBuffer.DefaultBlockFactor)
                )
                {
                    tarArchive.ExtractContents(targetDirectory.FullName);
                }
                sourceStream.Close();
            }
        }

        public void Analyse(ListBox listBox1, ListBox listBox2, string Mes_Path, int state, int error, int warn,
            int search, string find_string)
        {
            /* Cihazların içindeki "message" dosyalarının içini okuyup "ERROR" yada "WARNING" uyarısı bulunan satırları tespit edip analiz ettğimiz kısım */
            var rows = new List<string>();
            var Message_Path = Directory.GetFiles(Mes_Path);
            int link_down_count = 0, update_count = 0;
            foreach (var mes_file in Message_Path)
            {
                var all_line = 0;
                var filestream = new FileStream(mes_file,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite);
                var file = new StreamReader(filestream, Encoding.UTF8, true, 128);
                string s = null;
                int ov_ver_state = 0, os_ver_state = 0;
                var path_f_name = Path.GetFileName(filestream.Name);
                while ((s = file.ReadLine()) != null)
                {
                    line_count++;
                    all_line++;
                    var line_str = all_line.ToString();


                    if ((search == 3) & s.Contains(find_string))
                    {
                        var row = path_f_name + "  => Line  " + line_str + "  " + s.Trim('\0');
                        rows.Add(row);
                    }


                    if (s.Contains("Link is Down"))
                        link_down_count++;
                    if (s.Contains("Update success. Rebooting..."))
                        update_count++;


                    if ((ov_ver_state == 0) & s.Contains("Application version:") & s.Contains("qovmc"))
                    {
                        var index1 = s.IndexOf("Application version:") + 20;
                        var ver = s.Substring(index1);
                        label12.Text = "OVMC ver:" + ver;
                        ov_ver_state = 1;
                    }
                    if ((os_ver_state == 0) & s.Contains("software version:") & s.Contains("qovmc"))
                    {
                        var index2 = s.IndexOf("software version:") + 18;
                        var ver2 = s.Substring(index2);
                        label13.Text = " OSCVM ver:" + ver2;
                        os_ver_state = 1;
                    }


                    if (s.Contains("ERR"))
                    {
                        var index = s.IndexOf("ERR");
                        var add_err_list = s.Substring(index);

                        error_count++;
                        if ((state == 0) & !listBox1.Items.Contains(add_err_list))
                        {
                            listBox1.Items.Add(add_err_list);
                            error_without_repeat++;
                        }
                        if ((state == 1) | (error == 1))
                        {
                            var row = path_f_name + "  => Line  " + line_str + "  " + s.Trim('\0');
                            rows.Add(row);
                        }
                    }

                    if (s.Contains("err"))
                    {
                        var index = s.IndexOf("err");
                        var add_err_list = s.Substring(index);

                        error_count++;
                        if ((state == 0) & !listBox1.Items.Contains(add_err_list))
                        {
                            listBox1.Items.Add(add_err_list);
                            error_without_repeat++;
                        }
                        if ((state == 1) | (error == 1))
                        {
                            var row = path_f_name + "  => Line  " + line_str + "  " + s.Trim('\0');
                            rows.Add(row);
                        }
                    }


                    if (s.Contains("WRN"))
                    {
                        var index = s.IndexOf("WRN");
                        var add_wrn_list = s.Substring(index);

                        wrn_count++;
                        if ((state == 0) & !listBox2.Items.Contains(add_wrn_list))
                        {
                            listBox2.Items.Add(add_wrn_list);
                            wrn_without_repeat++;
                        }
                        if ((state == 1) | (warn == 1))
                        {
                            var row = path_f_name + "  => Line  " + line_str + "  " + s.Trim('\0');
                            rows.Add(row);
                        }
                    }
                }

                filestream.Close();
            }
            if (update_count > 0 || link_down_count > 0)
                label15.Text = link_down_count + "//" + update_count;
            listBox1.Items.AddRange(rows.ToArray());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

       
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var click_mesg = listBox1.SelectedItem.ToString();

                var index = click_mesg.IndexOf(' ');
                index = click_mesg.IndexOf(' ', index + 1);
                var f_name = click_mesg.Substring(0, index);

                var index1 = click_mesg.IndexOf("Line") + 5;
                var index2 = click_mesg.IndexOf(' ', index1 + 1);
                var f_line = Convert.ToInt32(click_mesg.Substring(index1, index2 - index1));

                open_text_editor(search_path + "\\" + f_name, f_line);
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
    }
}