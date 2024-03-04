using Domain.Members;

namespace Domain.Repositories;
public interface IMemberRepository
{
    void Add(Member member);
    void Update(Member member);
    Task<Member?> GetByIdAsync(MemberId id, CancellationToken cancellationToken = default);
    Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
}
