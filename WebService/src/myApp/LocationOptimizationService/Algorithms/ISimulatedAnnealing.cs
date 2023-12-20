using Application.Features.LocationSolvers.Dtos;
using MyService.Utilities.Abstract;

namespace MyService.Algorithms
{

    public interface ISimulatedAnnealing
    {
        IDataResult<BestResult> SolveWarehouseLocationProblem(LocationOptimizationRequestDto locationOptimizationRequestDto);
    }

}