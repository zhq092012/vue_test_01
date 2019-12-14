using System;
using System.ComponentModel.DataAnnotations;

namespace dataprovider.Models
{
  public class TbBsPplb
  {
    public int Id { get; set; }
    [Required]
    [Display(Name = "品牌名称")]
    public string Name { get; set; }
    [Required]
    [Display(Name = "创建时间")]
    public DateTime Ctime { get; set; }
  }
}