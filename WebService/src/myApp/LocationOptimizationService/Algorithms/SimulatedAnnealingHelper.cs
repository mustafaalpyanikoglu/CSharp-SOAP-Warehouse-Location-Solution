using System.Collections.Generic;
using System;
using Application.Features.LocationSolvers.Dtos;
using MyService.Algorithmss;

namespace MyService.Algorithms
{

    public class SimulatedAnnealingHelper
    {
        public bool CheckCapacityConstraints(List<int> solution, List<WarehouseWLPDto> warehouses, List<CustomerWLPDto> customers)
        {
            int warehousesCount = warehouses.Count;
            int[] warehouseCapacities = new int[warehousesCount];
            foreach (var customer in customers)
            {
                int warehouseIndex = solution[customer.Id];
                warehouseCapacities[warehouseIndex] += customer.Demand;
            }
            for (int i = 0; i < warehouses.Count; i++)
            {
                if (warehouseCapacities[i] > warehouses[i].Capacity)
                {
                    return false;
                }
            }
            for (int i = 0; i < warehousesCount; i++)
            {
                if (warehouseCapacities[i] > warehouses[i].Capacity)
                    Console.WriteLine($"Kapasite={warehouses[i].Capacity} Dolan Kapasite= {warehouseCapacities[i]}");
            }
            return true;
        }

    }

}
