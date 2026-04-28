using System;

namespace RestoranRezervasyonSistemi.Models
{
    public class MenuItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string ImagePath { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedDate { get; set; }

        public MenuItemModel()
        {
        }

        public MenuItemModel(RestoranRezervasyonSistemi.Models.MenuItem menuItem)
        {
            Id = menuItem.Id;
            Name = menuItem.Name;
            Description = menuItem.Description;
            Price = menuItem.Price;
            Category = menuItem.Category;
            ImagePath = menuItem.ImagePath;
            IsAvailable = menuItem.IsAvailable;
            CreatedDate = menuItem.CreatedDate;
        }

        public RestoranRezervasyonSistemi.Models.MenuItem ToMenuItem()
        {
            return new RestoranRezervasyonSistemi.Models.MenuItem
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                Category = this.Category,
                ImagePath = this.ImagePath,
                IsAvailable = this.IsAvailable,
                CreatedDate = this.CreatedDate
            };
        }
    }
}
