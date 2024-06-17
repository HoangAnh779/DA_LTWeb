using Bai3.Models;

namespace Bai3.ViewModels
{
    public class UserViewModel
    {
        public List<Menu> Menus { get; set; }
        public User Register { get; set; }
        public UserViewModel()
        {
            Register = new User();
        }
    }
}
