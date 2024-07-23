using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVSharpEx1
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new CameraExport());
            Application.Run(new ImageExport());
            //Application.Run(new OnMouseCallback37());
            //Application.Run(new Trackbar38());
            //Application.Run(new Mat39());
            //Application.Run(new CvWindow40());
            //Application.Run(new Window_Mat41());
            //Application.Run(new Bitwise_Mat42());
            //Application.Run(new Background_Remove_Subtractor43());
        }
    }
}
