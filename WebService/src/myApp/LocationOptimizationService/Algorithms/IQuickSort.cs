using System.Collections.Generic;
using System;
using MyService.Algorithmss;
using Application.Features.LocationSolvers.Dtos;

namespace MyService.Algorithms
{

    public interface IQuickSort
    {
        void SortBy<T>(List<T> list, int low, int high, Comparison<T> comparison);
        int CompareCustomersByDemand(CustomerWLPDto c1, CustomerWLPDto c2);
        int CompareWarehousesBySetupCost(WarehouseWLPDto w1, WarehouseWLPDto w2);
    }

}