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
                new PurchaseRequestRepository());

            purchaseService.RequestGoods(
                new SupplierId("SUP-AA-01"),
                new DepotId("DEP-AA-01"),
                new[] {new PurchaseItem { Product = "Tyres", Qty = 4, UnitPrice = 59.99 } });

            var goods = new[] {new PurchaseItem {Product = "Tyres", Qty = 4, UnitPrice = 59.99}};
            var requestForDepot1 = new DepotRequest {DepotId = new DepotId("DEP-AA-01"), Items = goods};
            var requestForDepot2 = new DepotRequest {DepotId = new DepotId("DEP-AA-02"), Items = goods};

            purchaseService.RequestGoods(
                new SupplierId("SUP-AA-01"),
                new[] {
                    requestForDepot1,
                    requestForDepot2
                });
        }
    }

    internal class DepotRequest
    {
        public DepotId DepotId { get; set; }
        public PurchaseItem[] Items { get; set; }
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
        private readonly PurchaseRequestRepository _purchaseRequestRepository;

        public PurchaseService(
            SupplierRepository supplierRepository, 
            DepotRepository depotRepository,
            PurchaseRequestRepository purchaseRequestRepository)
        {
            _supplierRepository = supplierRepository;
            _depotRepository = depotRepository;
            _purchaseRequestRepository = purchaseRequestRepository;
        }

        public void RequestGoods(SupplierId supplierRef, DepotId depotRef, PurchaseItem[] purchaseItems)
        {
            #region hide for convenience
            if (supplierRef == null)
                throw new ArgumentException("Missing", nameof(supplierRef));
            if (depotRef == null)
                throw new ArgumentException("Missing", nameof(depotRef));
            if (purchaseItems.Length == 0)
                throw new ArgumentException("Missing", nameof(purchaseItems));
            #endregion

            Supplier supplier = _supplierRepository.Get(supplierRef);
            Depot depot = _depotRepository.Get(depotRef);

            PurchaseRequest purchaseRequest = new PurchaseRequest(supplier, depot, purchaseItems);

            _purchaseRequestRepository.Save(purchaseRequest);
        }

        public void RequestGoods(SupplierId supplierRef, DepotId[] depotRefs, PurchaseItem[] purchaseItems)
        {
            #region hide for convenience
            if (supplierRef == null)
                throw new ArgumentException("Missing", nameof(supplierRef));
            if (depotRefs.Length == 0)
                throw new ArgumentException("Missing", nameof(depotRefs));
            if (purchaseItems.Length == 0)
                throw new ArgumentException("Missing", nameof(purchaseItems));
            #endregion

            Supplier supplier = _supplierRepository.Get(supplierRef);

            foreach (var depotRef in depotRefs)
            {
                Depot depot = _depotRepository.Get(depotRef);

                PurchaseRequest purchaseRequest = new PurchaseRequest(supplier, depot, purchaseItems);

                _purchaseRequestRepository.Save(purchaseRequest);
            }
        }

        public void RequestGoods(SupplierId supplierRef, DepotRequest[] depotRequest)
        {
            Supplier supplier = _supplierRepository.Get(supplierRef);

            foreach (var request in depotRequest)
            {
                Depot depot = _depotRepository.Get(request.DepotId);

                PurchaseRequest purchaseRequest = new PurchaseRequest(supplier, depot, request.Items);

                _purchaseRequestRepository.Save(purchaseRequest);
            }
        }
    }

    internal class PurchaseRequest
    {
        public PurchaseRequest(Supplier supplier, Depot depot, PurchaseItem[] purchaseItems)
        {
        }
    }

    internal class PurchaseRequestRepository
    {
        public void Save(PurchaseRequest request)
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
