using System.ComponentModel.DataAnnotations;

namespace Domain.Enum
{
    public enum Status
    {
        [Display(Name ="Đã xác nhận")]
        confirm,
        [Display(Name = "Chưa xác nhận")]
        unconfimred,
        none
    }
}
