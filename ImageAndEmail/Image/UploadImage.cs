using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace movie_project.ImageAndEmail.Image
{
    public class UploadImage
    {
        static string cloudName = "duaqq0ux7";
        static string apiKey = "426265263681176";
        static string apiSecret = "vQ88dv3ViIbaHSyN79PcsaiuRCE";
        static public Account account = new Account(cloudName, apiKey, apiSecret);
        static public Cloudinary _cloudinary = new Cloudinary(account);
        public static async Task<string> Upfile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("No files selected");
            }
            else
            {
                using (var stream = file.OpenReadStream())
                {
                    var upLoadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        PublicId = "ahihi" + "_" + DateTime.Now,
                        Transformation = new Transformation().Width(300).Height(400).Crop("fill")
                    };
                    var upLoadResult = await UploadImage._cloudinary.UploadAsync(upLoadParams);
                    if (upLoadResult.Error != null)
                    {
                        throw new Exception(upLoadResult.Error.Message);
                    }
                    string imageURL = upLoadResult.SecureUrl.ToString();
                    return imageURL;
                }
            }
        }
    }
}
