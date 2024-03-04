using Application.Interfaces;
using Domain.Members;
using Domain.Repositories;

namespace Application.Members;
public class MemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MemberService(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(string email, string name, CancellationToken cancellationToken = default)
    {
        var member = new Member
        {
            Id = new MemberId(Guid.NewGuid()),
            Email = email,
            Name = name
        };

        _memberRepository.Add(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(Member member, CancellationToken cancellationToken = default)
    {
        _memberRepository.Update(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
