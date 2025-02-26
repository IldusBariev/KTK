using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIshka.Entities
{
    public class New
    {

        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

        public New()
        {
            
        }

        public New(
            string title,
            string description,
            string imageName
            )
        {
            Title = title;
            Description = description;
            ImageName = imageName;
        }

    }
}
