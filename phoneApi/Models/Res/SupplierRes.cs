using phoneApi.Models.Domain;

namespace phoneApi.Models.Res
{
    public class SupplierRes: ISupplierRes
    {
        private readonly DatabaseContext context;

        public SupplierRes(DatabaseContext _context)
        {
            context = _context;
        }
        public List<Supplier> Getall()
        {
            return context.Suppliers.ToList();
        }
    }
}
