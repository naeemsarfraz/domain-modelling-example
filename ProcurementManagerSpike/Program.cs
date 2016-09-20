using System;
using System.Diagnostics.Contracts;

namespace ProcurementManagerSpike
{
    class Program
    {
        static void Main(string[] args)
        {
            PurchaseService purchaseService = new PurchaseService(
                new SupplierRepository(), 
                new DepotRepository(),
                new PurchaseOrderRepository());

            purchaseService.RequestGoods(
                new SupplierId("SUP-AA-01"),
                new DepotId("DEP-AA-01"),
                new[] {new PurchaseItem { Product = "Tyres", Qty = 4, UnitPrice = 59.99 } });
        }
    }

    internal class DepotId
    {
        public string DepotRef { get; set; }

        public DepotId(string depotRef)
        {
            DepotRef = depotRef;
        }
    }

    internal class SupplierId
    {
        public string SupplierRef { get; set; }

        public SupplierId(string supplierRef)
        {
            SupplierRef = supplierRef;
        }
    }

    internal class PurchaseItem
    {
        public string Product { get; set; }
        public int Qty { get; set; }
        public double UnitPrice { get; set; }
    }

    internal class Depot
    {
    }

    internal class Supplier
    {
    }

    internal class PurchaseService
    {
        private readonly SupplierRepository _supplierRepository;
        private readonly DepotRepository _depotRepository;
        private readonly PurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseService(
            SupplierRepository supplierRepository, 
            DepotRepository depotRepository,
            PurchaseOrderRepository purchaseOrderRepository)
        {
            _supplierRepository = supplierRepository;
            _depotRepository = depotRepository;
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public void RequestGoods(SupplierId supplierRef, DepotId depotRef, PurchaseItem[] purchaseItems)
        {
            if (supplierRef == null)
                throw new ArgumentException("Missing", nameof(supplierRef));
            if (depotRef == null)
                throw new ArgumentException("Missing", nameof(depotRef));
            if (purchaseItems.Length == 0)
                throw new ArgumentException("Missing", nameof(purchaseItems));

            Supplier supplier = _supplierRepository.Get(supplierRef);
            Depot depot = _depotRepository.Get(depotRef);

            PurchaseOrder newPurchaseOrder = new PurchaseOrder(supplier, depot, purchaseItems);

            _purchaseOrderRepository.Save(newPurchaseOrder);
        }
    }

    internal class PurchaseOrder
    {
        public PurchaseOrderStatus Status { get; set; }

        public PurchaseOrder(Supplier supplier, Depot depot, PurchaseItem[] purchaseItems)
        {
            Status = PurchaseOrderStatus.Requested;
        }
    }

    internal enum PurchaseOrderStatus
    {
        Requested
    }

    internal class PurchaseOrderRepository
    {
        public void Save(PurchaseOrder newPo)
        {
            throw new NotImplementedException();
        }
    }

    internal class SupplierRepository
    {
        public Supplier Get(SupplierId supplierRef)
        {
            throw new NotImplementedException();
        }
    }

    internal class DepotRepository
    {
        public Depot Get(DepotId depotRef)
        {
            throw new NotImplementedException();
        }
    }
}
