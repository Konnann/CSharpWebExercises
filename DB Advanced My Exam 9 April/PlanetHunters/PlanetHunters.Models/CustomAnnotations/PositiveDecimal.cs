using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetHunters.Models.CustomAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PositiveDecimal : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (decimal?)value > 0 || value == null;
        }
    }
}
