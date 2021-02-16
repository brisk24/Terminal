using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    class Gallery
    {
        public int X;
        public int OldX;
        private string PathToFolder;
        private PictureBox Pic;
        private string[] AllFiles;
        private string[] RandomFiles;
        int id;

        public Gallery(string pathFolder, PictureBox pic)
        {
            id = 0;
            Pic = pic;
            PathToFolder = pathFolder;
            AllFiles = System.IO.Directory.GetFiles(pathFolder);
            RandomFiles = new string[AllFiles.Length];
            RandomPhotoShow();
            Pic.Image = Image.FromFile(RandomFiles[id]);
        }



        public void ClickImage(int x)
        {
            X = x;
        }
        public void PushAndMove(int oldX)
        {
            OldX = oldX;
        }

        public void Show()
        {
            if (OldX < X - 20)
            {
                if (id < AllFiles.Length - 1)
                    Pic.Image = Image.FromFile(RandomFiles[++id]);
            }
            else if (OldX > X + 20)
            {
                if (id > 0)
                    Pic.Image = Image.FromFile(RandomFiles[--id]);
            }
           
        }

        public void RandomPhotoShow()
        {
            Random rnd = new Random();
            IEnumerable<int> numbers = Enumerable.Range(0, AllFiles.Length).OrderBy(r => rnd.Next());
            for (int i = 0; i < AllFiles.Length-1; i++)
            {
                int n = numbers.ToArray()[i];
                RandomFiles[i] = AllFiles[n];
            }           
        }

        public void PhotoShowRight()
        {
            if (id < RandomFiles.Length - 1)
                Pic.Image = Image.FromFile(RandomFiles[++id]);
            else
                id = 0;
        }

        public void PhotoShowLeft()
        {
            if (id > 0)
                Pic.Image = Image.FromFile(RandomFiles[--id]);
        }
    }
}
