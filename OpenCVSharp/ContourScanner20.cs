using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class ContourScanner20 : IDisposable
    {
        IplImage bin;
        IplImage con;

        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray);
            Cv.Threshold(bin, bin, 150, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage Contour(IplImage src)
        {
            con = new IplImage(src.Size, BitDepth.U8, 3);
            bin = new IplImage(src.Size, BitDepth.U8, 1);

            Cv.Copy(src, con);
            bin = this.Binary(src);

            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;

            //Cv.StartFindContours()는 하나의 윤곽선(컨투어)를 찾는데 사용
            //Cv.StartFindContours(이진화 이미지, 메모리 저장소, 객체 할당, 검색 방법, 근사화 방법)
            //CvContourScanner는 시퀀스의 정보를 가짐
            CvContourScanner scanner = Cv.StartFindContours(bin, Storage, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);

            // #1        
            while (true)
            {
                //contours = Cv.FindNextContour(scanner)는 순차적으로 검사, 없으면 null
                contours = Cv.FindNextContour(scanner);

                if (contours == null) break;
                else
                {
                    Cv.DrawContours(con, contours, CvColor.Yellow, CvColor.Red, 1, 4, LineType.AntiAlias);
                }
            }
            Cv.EndFindContours(scanner);

            // #2        
            //foreach (CvSeq<CvPoint> c in scanner)
            //{
            //    con.DrawContours(c, CvColor.Yellow, CvColor.Red, 1, 4, LineType.AntiAlias);
            //}
            //Cv.ClearSeq(contours);

            Cv.ReleaseMemStorage(Storage);

            return con;
        }

        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (con != null) Cv.ReleaseImage(con);
        }
    }
}
