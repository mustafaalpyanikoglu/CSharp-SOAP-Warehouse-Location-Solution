using Application.Features.LocationSolvers.Dtos;
using MyService.Algorithms;
using MyService.Utilities.Abstract;
using MyServiceAlgorithms;
using System.Web.Services;

namespace LocationOptimizationService
{
    /// <summary>
    /// Summary description for LocationSolverService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LocationSolverService : System.Web.Services.WebService
    {

        private readonly ISimulatedAnnealing _simulatedAnnealing;

        public LocationSolverService()
        {
            
        }

        [WebMethod]
        public BestResult LocationSolver(LocationOptimizationRequestDto LocationOptimizationRequestDto)
        {
            IDataResult<BestResult> result = new SimulatedAnnealing(new QuickSort()).SolveWarehouseLocationProblem(LocationOptimizationRequestDto);
            return result.Data;
        }
    }
}
