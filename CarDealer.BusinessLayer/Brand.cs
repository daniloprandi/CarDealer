using System;

namespace CarDealer.BusinessLayer
{
  public class Brand
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public bool ValidateBrand(Brand brand)
    {
      var valid = true;
      if(string.IsNullOrWhiteSpace(Name)) valid = false;
      return valid;
    }
  }
}
