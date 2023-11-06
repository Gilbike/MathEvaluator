using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class BusinessLogic
    {

        /// <summary>
        /// Létrehoz egy megadott útvonalon eg új fájlt.<br/>
        /// Amennyiben már létezik a megadott útvonalon a fájl
        /// vagy nem abszolút az elérési út, hibát dob.
        /// </summary>
        /// <param name="path">Új fájl abszolút elérési útja</param>
        public void MakeFile(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                throw new ArgumentException("A megadott elérési út nem relatív");
            }

            if (File.Exists(path))
            {
                throw new IOException("A megadott elérési úton már létezik fájl");
            }

            File.WriteAllText(path, "");
        }

    }
}
