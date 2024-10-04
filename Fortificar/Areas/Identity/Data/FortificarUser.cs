using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;   
using Microsoft.AspNetCore.Identity;

namespace Fortificar.Areas.Identity.Data;

public class FortificarUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }
    [PersonalData]
    [Column(TypeName = "tinyint")]
    public int Tipo { get; set; }
}
