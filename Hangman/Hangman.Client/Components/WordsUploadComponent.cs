using System.Text;
using System.Threading.Tasks;

namespace Hangman.Client.Components
{
    public class WordsUploadComponent : BaseHangmanComponent
    {
        public async Task UploadFile()
        {
            var data = await JsInterop.GetFileData("fileUpload");
            var bytes = Encoding.UTF8.GetBytes(data);
            var response = await ApiClient.UploadWordsFile(bytes);
            UriHelper.NavigateTo("/word/all");
        }
    }
}
