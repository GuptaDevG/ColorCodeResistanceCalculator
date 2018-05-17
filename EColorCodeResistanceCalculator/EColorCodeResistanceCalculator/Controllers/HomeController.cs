using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EColorCodeResistanceCalculator.Models;
using EColorCodeResistanceCalculator.Calculator;

namespace EColorCodeResistanceCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOhmValueCalculator _ohmValueCalculator;
        public HomeController(IOhmValueCalculator ohmValueCalculator)
        {
            _ohmValueCalculator = ohmValueCalculator;
        }

        public ActionResult Index()
        {
            try
            {
                BandColorCode selectOnLoad = new BandColorCode();
                return View(selectOnLoad);
            }
            catch (Exception ex)
            {
                return Json("Exception ocurred while loadig the bands color" + ex.Message);
            }
        }

        /// <summary>
        ///  Get Resistance method
        /// </summary>
        /// <param name="BandColors">4 bands color</param>
        /// <returns>Returns a JSON response contain the min and max resistance values </returns>
        [HttpPost]
        public ActionResult ResistanceValue([FromBody] FourBands bandcolors )
        {
            try
            {
                if (bandcolors == null)
                    throw new ArgumentNullException();
                CalculatorResult resultObject = _ohmValueCalculator.CalculateOhmValue(bandcolors.bandAColor,
                    bandcolors.bandBColor, bandcolors.bandCColor, bandcolors.bandDColor);
                return Json(new { min = resultObject.MinResistance, max = resultObject.MaxResistance });
            }
            catch (Exception ex)
            {
                return Json("Exception ocurred while calculating resistance value: " + ex.Message);
            }
        }
    }
}
