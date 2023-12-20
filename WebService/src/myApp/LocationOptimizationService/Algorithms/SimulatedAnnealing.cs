using System.Collections.Generic;
using System;
using static MyService.Utilities.Constants.Algorithms.SimulatedAnnealingConstants;
using MyService.Utilities.Abstract;
using System.Linq;
using MyService.Utilities.Concrete;
using MyService.Algorithmss;
using Application.Features.LocationSolvers.Dtos;

namespace MyService.Algorithms
{

    public class SimulatedAnnealing : ISimulatedAnnealing
    {
        private Random _random = new Random(); // Random değer atamak için
        private int _numWarehouses { get; set; }
        private int _numCustomers { get; set; }
        private List<CustomerWLPDto> _customers;  // Müşteri listesi
        private List<WarehouseWLPDto> _warehouses;  // Depo listesi
        private Dictionary<int, int> _solution; // _solution veri tipi Dictionary<int, int> olarak değiştirildi

        private readonly IQuickSort _quickSort;

        public Dictionary<int, int> Solution // Solution property'sinin veri tipi değiştirildi
        {
            get { return _solution; }
            private set { _solution = value; }
        }

        public SimulatedAnnealing(IQuickSort quickSort)
        {
            _quickSort = quickSort;
        }

        public IDataResult<BestResult> SolveWarehouseLocationProblem(LocationOptimizationRequestDto locationOptimizationRequestDto)
        {
            _customers = locationOptimizationRequestDto.CustomerWLPDtos;
            _warehouses = locationOptimizationRequestDto.WarehouseWLPDtos;
            _numWarehouses = locationOptimizationRequestDto.WarehouseWLPDtos.Count;
            _numCustomers = locationOptimizationRequestDto.CustomerWLPDtos.Count;

            Dictionary<int, int> currentSolution = GenerateRandomSolution(); // currentSolution veri tipi değiştirildi
            double currentCost = CalculateCost(currentSolution.Values.ToList());

            Dictionary<int, int> bestSolution = new Dictionary<int, int>(currentSolution); // bestSolution veri tipi değiştirildi
            double bestCost = currentCost;

            double temperature = INITAL_TEMPERATURE;
            int iteration = 0;

            while (temperature > 0 && iteration < MAX_ITERATIONS)
            {
                Dictionary<int, int> newSolution = GenerateNeighborSolution(currentSolution); // newSolution veri tipi değiştirildi
                double newCost = CalculateCost(newSolution.Values.ToList());

                if (shouldAcceptNewSolution(currentCost, newCost))
                {
                    currentSolution = new Dictionary<int, int>(newSolution);
                    currentCost = newCost;
                }
                if (isNewCostBetter(newCost, bestCost))
                {
                    bestSolution = new Dictionary<int, int>(newSolution);
                    bestCost = newCost;
                }
                temperature *= COOLINGRATE;
                iteration++;
            }

            return new SuccessDataResult<BestResult>(new BestResult(bestCost, bestSolution.Values.ToList()));
        }
        public double CalculateCost(List<int> solution)
        {
            double totalCost = 0;
            for (int i = 0; i < _numCustomers; i++)
            {
                totalCost += CalculateCostForSelectedWarehouse(solution[i], i);
            }

            return totalCost;
        }
        private Dictionary<int, int> GenerateRandomSolution()
        {
            List<CustomerWLPDto> customers = new List<CustomerWLPDto>(_customers);
            List<WarehouseWLPDto> warehouses = new List<WarehouseWLPDto>(_warehouses);
            Dictionary<int, int> solution = new Dictionary<int, int>();

            _quickSort.SortBy(customers, 0, customers.Count - 1, _quickSort.CompareCustomersByDemand);
            _quickSort.SortBy(warehouses, 0, warehouses.Count - 1, _quickSort.CompareWarehousesBySetupCost);

            foreach (var customer in customers)
            {
                WarehouseWLPDto selectedWarehouse = findSuitableWarehouse(customer, warehouses);
                if (selectedWarehouse != null)
                {
                    selectedWarehouse.Capacity -= customer.Demand;
                    solution[customer.Id] = selectedWarehouse.Id;
                }
                else
                {
                    // Uygun bir depo bulunamadı
                }
            }

            return solution;
        }

        private Dictionary<int, int> GenerateNeighborSolution(Dictionary<int, int> currentSolution)
        {
            Dictionary<int, int> newSolution = new Dictionary<int, int>(currentSolution);
            int randomCustomerId = _customers[_random.Next(_numCustomers)].Id;

            // Rastgele bir depo ID'si seç
            int randomWarehouseId = _warehouses[_random.Next(_numWarehouses)].Id;

            newSolution[randomCustomerId] = randomWarehouseId;

            return newSolution;
        }
        private double CalculateCostForSelectedWarehouse(int warehouseId, int customerId)
        {
            CustomerWarehouseCostWlpDto travel = _customers[customerId].CustomerWarehouseCostWlpDtos.Where(p => p.WarehouseID == warehouseId).FirstOrDefault();
            WarehouseWLPDto setup = _warehouses.Where(p => p.Id == warehouseId).FirstOrDefault();

            if (travel != null && setup != null)
            {
                double travelCost = travel.Cost;
                double setupCost = setup.SetupCost;
                return travelCost + setupCost;
            }
            else return 0;
        }
        private bool shouldAcceptNewSolution(double currentCost, double newCost)
        {
            if (isNewCostBetterThanCurrent(newCost, currentCost)) return true;

            double acceptanceProbability = calculateAcceptanceProbability(currentCost, newCost);
            double randomValue = _random.NextDouble();

            return randomValue < acceptanceProbability;
        }
        private WarehouseWLPDto findSuitableWarehouse(CustomerWLPDto customer, List<WarehouseWLPDto> warehouses) => warehouses.FirstOrDefault(item => isCapacitySufficient(item.Capacity, customer.Demand));
        private bool isNewCostBetter(double newCost, double bestCost) => newCost < bestCost;
        private bool isNewCostBetterThanCurrent(double newCost, double currentCost) => newCost < currentCost;
        private bool isCapacitySufficient(int capacity, int demand) => capacity >= demand;
        private double calculateAcceptanceProbability(double currentCost, double newCost) => Math.Exp((currentCost - newCost) / INITAL_TEMPERATURE);

    }

}