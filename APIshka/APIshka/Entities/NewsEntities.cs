using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIshka.Entities
{
    public class NewsEntities
    {

        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }

        public NewsEntities()
        {
            
        }

        public NewsEntities(
            string title,
            string description,
            string imageName
            )
        {
            Title = title;
            Description = description;
            ImageName = imageName;
            CreateDate = DateTime.Now;
        }

    }
}
