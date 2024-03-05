using Domain.Members;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _context;

    public MemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Member member)
    {
        _context.Members.Add(member);
    }
    public void Update(Member member)
    {
        _context.Members.Update(member);
    }

    public Task<Member?> GetByIdAsync(MemberId id, CancellationToken cancellationToken = default)
    {
        return _context.Members
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _context.Members
            .FirstOrDefaultAsync(m => m.Email == email, cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        return !await _context.Members.AnyAsync(m => m.Email == email, cancellationToken);
    }

}
