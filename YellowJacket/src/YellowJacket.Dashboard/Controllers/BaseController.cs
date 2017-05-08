using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YellowJacket.Dashboard.Controllers
{
    public class BaseController : Controller
    {
        internal void HandleError(Exception ex)
        {
            
        }
    }
}