﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace AdventureWorks.Domain.Models;

/// <summary>
/// Lookup table of customer purchase reasons.
/// </summary>
public partial class SalesReason
{
    /// <summary>
    /// Primary key for SalesReason records.
    /// </summary>
    public int SalesReasonID { get; set; }

    /// <summary>
    /// Sales reason description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Category the sales reason belongs to.
    /// </summary>
    public string ReasonType { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReasons { get; set; } = new List<SalesOrderHeaderSalesReason>();
}