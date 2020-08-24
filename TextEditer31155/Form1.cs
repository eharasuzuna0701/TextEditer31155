using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31155
{
    public partial class Form1 : Form
    {
        //ファイル名の初期化
        private string filename = "";
        public Form1()
        {
            InitializeComponent();
        }
        //フォームのロード
        private void Form1_Load(object sender, EventArgs e)
        {
            FormName();
        }
        //ファイルの名前
        public void FormName()
        {
            if (filename != "")
            {
                this.Text = filename;
            }
            else
            {     
                this.Text = "無題";
            }
        }
        //終了ボタン
        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //名前を付けて保存
        private void SaveNameAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.GetEncoding("utf-8")))
                {
                    sw.WriteLine(rtTextArea.Text);
                }
                FormName();
            }
        }
        //開く
        private void OpenOToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding("utf-8"), false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    filename = openFileDialog.FileName;
                }
            }
            FormName();
        }
        //上書き保存
        private void SaveSToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            using (StreamWriter sw = new StreamWriter(openFileDialog.FileName, false, Encoding.GetEncoding("utf-8")))
            {
                if (filename == openFileDialog.FileName)
                {

                    sw.WriteLine(rtTextArea.Text);
                }
                else
                {
                    SaveNameAToolStripMenuItem_Click(sender, e);
                }
                FormName();
            }
        }

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.CanUndo) //元に戻すことが可能であれば
            {
                rtTextArea.Undo();
                rtTextArea.ClearUndo();
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();

            //クリップボードに保持されている情報がテキスト形式で取り出せるが確認
            if (iData.GetDataPresent(DataFormats.Text))
            {
                //文字列にキャストして取り出し
                rtTextArea.Text = (String)iData.GetData(DataFormats.Text);
            }
        }
    }
}

