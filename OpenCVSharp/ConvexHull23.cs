using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class ConvexHull23 : IDisposable
    {
        IplImage bin;
        IplImage convex;

        public IplImage Binary(IplImage src)
        {
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, bin, ColorConversion.RgbToGray);
            Cv.Threshold(bin, bin, 150, 255, ThresholdType.Binary);
            return bin;
        }

        public IplImage ConvexHull(IplImage src)
        {
            //검은 이미지인 convex과 Binary 이미지인 bin을 선언하고 적용
            //convex는 원본을 복사하지 않아 검은색 이미지
            convex = new IplImage(src.Size, BitDepth.U8, 3);
            bin = new IplImage(src.Size, BitDepth.U8, 1);
            bin = this.Binary(src);

            CvMemStorage Storage = new CvMemStorage();
            CvSeq<CvPoint> contours;
            Cv.FindContours(bin, Storage, out contours, CvContour.SizeOf, ContourRetrieval.List, ContourChain.ApproxNone);

            CvSeq<CvPoint> apcon_seq = Cv.ApproxPoly(contours, CvContour.SizeOf, Storage, ApproxPolyMethod.DP, 3, true);

            for (CvSeq<CvPoint> c = apcon_seq; c != null; c = c.HNext)
            {
                CvPoint[] ptseq = new CvPoint[c.Total];

                if (c.Total > 4)
                {
                    for (int i = 0; i < c.Total; i++)
                    {
                        CvPoint? p = Cv.GetSeqElem(c, i);
                        //ptseq[i] = new CvPoint를 이용하여 하나의 다각형 안에 있는 점의 좌표들을 배열로 저장
                        //이 정보로 최외곽점들을 알 수 있음
                        ptseq[i] = new CvPoint
                        {
                            X = p.Value.X,
                            Y = p.Value.Y
                        };
                    }

                    foreach (CvPoint pt in ptseq)
                    {
                        Cv.Circle(convex, pt, 4, CvColor.Red, -1);
                    }

                    // 블록 껍질 찾기
                    CvPoint[] hull;
                    //ConvexHull2(코너점들의 집합, 최외곽 점, 회전 방향)
                    Cv.ConvexHull2(ptseq, out hull, ConvexHullOrientation.Clockwise);

                    CvPoint pt0 = hull.Last();  // 최초 지점 설정
                    foreach(CvPoint pt in hull)
                    {
                        Cv.Line(convex, pt0, pt, CvColor.Green, 2);
                        pt0 = pt;
                    }
                }
            }
            return convex;
        }

        public void Dispose()
        {
            if (bin != null) Cv.ReleaseImage(bin);
            if (convex != null) Cv.ReleaseImage(convex);
        }
    }
}
