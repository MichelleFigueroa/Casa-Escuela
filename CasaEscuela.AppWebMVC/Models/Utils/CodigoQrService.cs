using Microsoft.VisualStudio.Web.CodeGeneration;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace CasaEscuela.AppWebMVC.Utils
{
    public class CodigoQrService : ICodigoQrService
    {
        public async Task<byte[]> GenerarQr(CodigoQr pCodigoString)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(pCodigoString.Texto, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qRCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            using (MemoryStream stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
