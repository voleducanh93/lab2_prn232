﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class CosmeticCategory
{
    public string CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string UsagePurpose { get; set; }

    public string FormulationType { get; set; }

    public virtual ICollection<CosmeticInformation> CosmeticInformations { get; set; } = new List<CosmeticInformation>();
}