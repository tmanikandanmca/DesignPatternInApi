﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace AdventureWorks.Domain.Models;

/// <summary>
/// Lookup table containing the languages in which some AdventureWorks data is stored.
/// </summary>
public partial class Culture
{
    /// <summary>
    /// Primary key for Culture records.
    /// </summary>
    public string CultureID { get; set; } = null!;

    /// <summary>
    /// Culture description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; } = new List<ProductModelProductDescriptionCulture>();
}