using Bai3.Models;
using System.ComponentModel;

namespace Bai3.ViewModels
{
    public class HomeViewModel
    {
        public List<Menu> Menus { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Product> DaxayProds { get; set; }
        public List<Product> TraProds { get; set; }
        public List<Product> TrahopProds { get; set; }
        public List<Product> TragoiProds { get; set; }
        public List<Product> TralonProds { get; set; }
        public Catology TraCateProds { get; set; }
        public Catology DaxayCateProds { get;set; }
        public Catology TrahopCateProds { get; set; }
        public Catology TragoiCateProds { get; set; }
        public Catology TralonCateProds { get; set; }
    }
}
