using System;
using System.ComponentModel.DataAnnotations;

namespace dataprovider.Models
{
  public class Student
  {
    [Key]
    public int Id { get; set; }
    [Display(Name = "姓名")]
    public string Name { get; set; }
    [Display(Name = "生日"), Required, DataType(DataType.Date)]
    public DateTime Birthday { get; set; }
    [Display(Name = "性别"), Required]
    public GenderEnum Gender { get; set; }
  }
}