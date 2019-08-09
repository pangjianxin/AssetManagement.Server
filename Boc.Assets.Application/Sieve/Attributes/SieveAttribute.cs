﻿using Boc.Assets.Application.Sieve.Models;
using System;

namespace Boc.Assets.Application.Sieve.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SieveAttribute : Attribute, ISievePropertyMetadata
    {
        /// <summary>
        /// Override name used 
        /// </summary>
        public string Name { get; set; }

        public string FullName => Name;

        public bool CanSort { get; set; }
        public bool CanFilter { get; set; }
    }
}