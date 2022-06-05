using System.Text;
using NetSparkleUpdater.Interfaces;

namespace NetSparkleUnitTests
{
    public class StringCastDataDownloader : IAppCastDataDownloader
    {
        private string _data = null;
        public StringCastDataDownloader(string data)
        {
            _data = data;
        }
        
        public string DownloadAndGetAppCastData(string url)
        {
            return _data;
        }

        public Encoding GetAppCastEncoding()
        {
                return Encoding.UTF8;
        }        
    }
}