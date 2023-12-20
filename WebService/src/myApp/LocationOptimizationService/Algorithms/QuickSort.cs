using MyService.Algorithms;
using System.Collections.Generic;
using System;
using Application.Features.LocationSolvers.Dtos;
using MyService.Algorithmss;

namespace MyServiceAlgorithms
{
    public class QuickSort : IQuickSort
    {
        public void SortBy<T>(List<T> list, int low, int high, Comparison<T> comparison)
        {
            if (low < high)
            {
                int partitionIndex = Partition(list, low, high, comparison);
                SortBy(list, low, partitionIndex - 1, comparison);
                SortBy(list, partitionIndex + 1, high, comparison);
            }
        }
        public int CompareCustomersByDemand(CustomerWLPDto c1, CustomerWLPDto c2)
        {
            return c1.Demand.CompareTo(c2.Demand);
        }

        public int CompareWarehousesBySetupCost(WarehouseWLPDto w1, WarehouseWLPDto w2)
        {
            return w1.SetupCost.CompareTo(w2.SetupCost);
        }
        private int Partition<T>(List<T> list, int low, int high, Comparison<T> comparison)
        {
            T pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (comparison.Invoke(list[j], pivot) <= 0)
                {
                    i++;
                    Swap(list, i, j);
                }
            }

            Swap(list, i + 1, high);
            return i + 1;
        }

        private void Swap<T>(List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

}
