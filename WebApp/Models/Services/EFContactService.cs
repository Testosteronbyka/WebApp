using Microsoft.EntityFrameworkCore;

namespace WebApp.Models.Services;

public class EFContactService: IContactService
{
    private readonly AppDbContext _context;

    public EFContactService(AppDbContext context)
    {
        _context = context;
    }

    public void Add(ContactModel model)
    {
        _context.Contacts.Add(ContactMapper.ToEntity(model));
        _context.SaveChanges();
    }

    public void Update(ContactModel model)
    {
        _context.Contacts.Update(ContactMapper.ToEntity(model));
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _context.Contacts.Remove(new ContactEntity() { Id = id });
        _context.SaveChanges();
    }

    public List<ContactModel> GetAll()
    {
        return _context.Contacts
            .Select(e => ContactMapper.FromEntity(e))
            .ToList();
    }

    public ContactModel? GetById(int id)
    {
        var entity = _context.Contacts.Find(id);
        return entity != null ? ContactMapper.FromEntity(entity) : null;
    }

    public List<OrganizationEntity> GetOrganizations()
    {
        return _context.Organizations.ToList();
    }
}