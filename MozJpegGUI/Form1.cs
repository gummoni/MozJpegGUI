using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MozJpegGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateOutputFolder();
        }

        /// <summary>
        /// 出力フォルダ作成
        /// </summary>
        void CreateOutputFolder()
        {
            if (!Directory.Exists("output"))
            {
                Directory.CreateDirectory("output");
            }
        }

        /// <summary>
        /// 出力フォルダを開く
        /// </summary>
        void OpenOutputFolder()
        {
            var outPath = Path.Combine(Directory.GetCurrentDirectory(), "output");

            var info = new ProcessStartInfo()
            {
                FileName = outPath,
                UseShellExecute = true,
            };
            Process.Start(info);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var drags = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach(var drag in drags)
                {
                    if (Directory.Exists(drag))
                    {
                        var files = Directory.GetFiles(drag, "*.jpg", SearchOption.TopDirectoryOnly);
                        foreach(var file in files)
                        {
                            Execute(file);
                        }
                    }
                    else if (File.Exists(drag))
                    {
                        Execute(drag);
                    }
                }
            }
        }


        /// <summary>
        /// 変換開始
        /// </summary>
        /// <param name="fullname"></param>
        void Execute(string fullname)
        {
            //"%~dp0cjpeg.exe" -optimize -quality 85 -outfile "mozjpeg\%%A" "%%A"
            var filename = Path.GetFileName(fullname);
            var outFile = Path.Combine("output", filename);

            Process.Start(new ProcessStartInfo
            {
                FileName = "cjpeg.exe",
                Arguments = $"-optimize -quality 85 -outfile \"{outFile}\" \"{fullname}\""
            });
        }

        /// <summary>
        /// 出力フォルダを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            OpenOutputFolder();
        }
    }
}
