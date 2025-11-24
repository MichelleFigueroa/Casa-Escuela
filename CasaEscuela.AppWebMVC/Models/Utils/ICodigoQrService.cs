using Humanizer.Bytes;

namespace CasaEscuela.AppWebMVC.Utils
{
    public interface ICodigoQrService
    {
        Task<byte[]> GenerarQr(CodigoQr pCodigoString);
    }
}
