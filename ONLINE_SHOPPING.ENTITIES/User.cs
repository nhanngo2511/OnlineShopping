using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.ENTITIES
{
    public class User
    {
        public int ID { set; get; }
        [Required]
        public string UserName { set; get; }
         [Required]
        public string Password { set; get; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Nhập lại Password không khớp")]
         public string ConfirmPassword { set; get; }
        [Required]
         public string FullName { set; get; }
        [Required]
        [Range(0, 100)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Số tuổi phải lớn hơn 0")]
         public int Age { set; get; }
        [Required]
         public bool Gender { set; get; }
        [Required]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Số điện thoại chưa đúng định dạng")]
         public string PhoneNumber { set; get; }
         [Required]
        public string Address { set; get; }

        public Ward Ward { set; get; }
        [Required]
        [EmailAddress]
         public string Email { set; get; }

        public List<string> Roles { set; get; }
        
    }
}
