using System;
using System.Collections;
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
        public Form1()
        {
            InitializeComponent();
        }
        //ファイル名の初期化
        private string filename = "";
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
            if (rtTextArea.Text != "" )
            {
                Not_saved(sender,e);
            }
            else
            {
                Application.Exit();
            }
            Application.Exit();

        }
        //名前を付けて保存
        private void SaveNameAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sFDSeveDeta.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sFDSeveDeta.FileName, false, Encoding.GetEncoding("utf-8")))
                {
                    sw.WriteLine(rtTextArea.Text);
                    filename = sFDSeveDeta.FileName;
                }
                
            }
            FormName();
        }
        //開く
        private void OpenOToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (rtTextArea.Text != "")
            {
                Not_saved(sender, e);
            }

            if (oFDOpenDeta.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(oFDOpenDeta.FileName, Encoding.GetEncoding("utf-8"), false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    filename = oFDOpenDeta.FileName;
                }
            }
                FormName();
            }
        
        //上書き保存
        private void SaveSToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            using (StreamWriter sw = new StreamWriter(oFDOpenDeta.FileName, false, Encoding.GetEncoding("utf-8")))
            {
                if (filename == oFDOpenDeta.FileName)
                {

                    sw.WriteLine(rtTextArea.Text);
                    filename = oFDOpenDeta.FileName;
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
                rtTextArea.Undo();
        }
        //やり直し
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
        }
        //コピー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();
        }
        //切り取り
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            rtTextArea.Cut();
        }
        //ペースト
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.SelectionStart = rtTextArea.SelectionStart + rtTextArea.SelectionLength;
            rtTextArea.Paste();
        }
        //デリート
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Text = "";
        }
        //マスク
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mask_menu();
        }
      
        private void Mask_menu()
        {
            if (rtTextArea.CanUndo)
            {
                UndoToolStripMenuItem.Enabled = true;
            }
            else
            {
                UndoToolStripMenuItem.Enabled = false;
            }
            if (rtTextArea.CanRedo)
            {
                RedoToolStripMenuItem.Enabled = true;
            }
            else
            {
                RedoToolStripMenuItem.Enabled = false;
            }
            if (rtTextArea.Text != "")
            {
                CutToolStripMenuItem.Enabled = true;
                CopyToolStripMenuItem.Enabled = true;
                DeleteToolStripMenuItem.Enabled = true;
            }
            else
            {
                CutToolStripMenuItem.Enabled = false;
                CopyToolStripMenuItem.Enabled = false;
                DeleteToolStripMenuItem.Enabled = false;
            }
            if (Clipboard.ContainsText() || Clipboard.ContainsImage())
            {
                PasteToolStripMenuItem.Enabled = true;
            }
            else
            {
                PasteToolStripMenuItem.Enabled = false;
            }
        }
        //色
        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cDColor.ShowDialog() == DialogResult.OK) { 
                rtTextArea.ForeColor = cDColor.Color;
            }
        }
        //フォント
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fdFont.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.SelectionFont = fdFont.Font;
            }
        }
        //新規制作
        private void NewNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(rtTextArea.Text != "")
            {
                Not_saved(sender, e);
            }
            rtTextArea.Text = "";
            this.filename = "";
        }
       //未保存
       private void Not_saved(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "ファイルを保存しますか？",
                "質問",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                //「はい」が選択された時
                if (filename == "")
                {
                    SaveNameAToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    SaveSToolStripMenuItem_Click(sender, e);
                }
                
            }
            else if (result == DialogResult.No)
            {
                //「いいえ」が選択された時
               
            }
            else if (result == DialogResult.Cancel)
            {
                //「キャンセル」が選択された時
                this.Show();
            }
        }
      
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtTextArea.Text != "")
            {
                Not_saved(sender, e);

            }

        }
    }
    
}

