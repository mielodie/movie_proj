using SixLabors.ImageSharp.PixelFormats;

namespace movie_project.ImageAndEmail.Image
{
    public class ImageSize
    {
        public static bool IsImage(IFormFile imageFile, int maxSize = (2 * 1024 * 768))
        {
            try
            {
                using (var image = SixLabors.ImageSharp.Image.Load<Rgba32>(imageFile.OpenReadStream()))
                {
                    if (image.Width > 0 && image.Height > 0)
                    {
                        if (imageFile.Length <= maxSize)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("Image file size is too large");
                        }
                    }
                }
            }
            catch
            {
                if (imageFile.Length > maxSize)
                {
                    throw new Exception("Image file size is too large");
                }
                else
                {
                    throw new Exception("Invalid image file format");
                }
            }
            return false;
        }
    }
}
