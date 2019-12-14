using System;
using System.ComponentModel.DataAnnotations;

namespace dataprovider.Models
{
  public class Student
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
  }
}