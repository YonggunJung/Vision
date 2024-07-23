using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharpEx1
{
    internal class _2D_Filter47 : IDisposable
    {
        IplImage filter;

        public IplImage Filter2D(IplImage src)
        {
            //결과에 사용할 filter를 생성
            filter = new IplImage(src.Size, BitDepth.U8, 3);

            //임의의 커널을 생성 = 가우시안 블러 커널과 동일
            float[] data = {   1,  4,  7,  4,  1,
                                4,  16, 26, 16, 4,
                                7,  26, 41, 26, 7,
                                4,  16, 26, 16, 4,
                                1,  4,  7,  4,  1   };

            //CvMat(n, m, 매트릭스 타입, 배열)
            //nxm 크기의 매트릭스로 변환하여 생성
            CvMat kernel = new CvMat(5, 5, MatrixType.F32C1, data);

            //Cv.Normalize(원본, 결과, 최댓값, 최솟값, 정규화기준)
            //최댓값      : 정규화된 값의 최댓값.
            //최솟값      : 정규화된 값의 최솟값.
            //정규화기준  : 정규화할때의 기준을 선택.
            //NormType.C  : 매트릭스의 최댓값을 기준으로 정규화.
            //NormType.L1 : 매트릭스의 합을 기준으로 정규화.
            //NormType.L2 : 매트릭스의 유클리드 노름을 기준으로 정규화
            Cv.Normalize(kernel, kernel, 1.0, 0, NormType.L1);
            //Cv.Filter2D(원본, 결과, 커널)
            Cv.Filter2D(src, filter, kernel);

            return filter;
        }

        public void Dispose()
        {
            if (filter != null) Cv.ReleaseImage(filter);
        }
    }
}
