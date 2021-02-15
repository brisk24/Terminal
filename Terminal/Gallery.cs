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
        private string PathToFolder;
        private PictureBox Pic;
        private string[] AllFiles;
        int id;

        public Gallery(string pathFolder, PictureBox pic)
        {
            id = 0;
            Pic = pic;
            PathToFolder = pathFolder;
            AllFiles = System.IO.Directory.GetFiles(pathFolder);
            
            Pic.Image = Image.FromFile(AllFiles[id]);
        }

        public int[] RandomPhotoShow()
        {
            Random rnd = new Random();
            IEnumerable<int> numbers = Enumerable.Range(0, AllFiles.Length).OrderBy(r => rnd.Next());
            return numbers.ToArray();
        }

        public void PhotoShowRight()
        {
            if (id < AllFiles.Length - 1)
                Pic.Image = Image.FromFile(AllFiles[++id]);
            else
                id = 0;
        }

        public void PhotoShowLeft()
        {
            if (id > 0)
                Pic.Image = Image.FromFile(AllFiles[--id]);
        }
    }
}
