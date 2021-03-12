using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.BusinessLayer
{
  public class Car
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Brand Brand { get; set; }
    public int BrandId { get; set; }

    public bool ValidateCar(Car car)
    {
      var valid = true;
      if(string.IsNullOrWhiteSpace(Name)) valid = false;
      if(string.IsNullOrWhiteSpace(BrandId.ToString())) valid = false;
      return valid;
    }
  }
}
