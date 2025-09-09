using Grpc.Core;
using src.DTOs.Response;
using src.Interfaces.IRepositories;
using TuitionGrpc;

namespace src.Services
{
    public class TuitionGrpcServiceImpl : TuitionGrpcService.TuitionGrpcServiceBase
    {
        private readonly ITuitionRepository _tuitionRepository;
        public TuitionGrpcServiceImpl(ITuitionRepository tuitionRepository)
        {
            _tuitionRepository = tuitionRepository;
        }

        public override async Task<GetTuitionAsyncReply> GetTuition(GetTuitionAsyncRequest request, ServerCallContext context)
        {
            var tuitions = await _tuitionRepository.GetAllTuitionUnpaidByStudentId(request.StudentId);

            var reply = new GetTuitionAsyncReply();
            
            reply.Tuitions.AddRange(tuitions.Select(t => new TuitionItem
            {
                Amount = t.Amount.ToString(),
                DueDate = t.DueDate.ToString("yyyy-MM-dd"),
                Status = t.Status,
                Semester = t.Semester
            }));
 
            return reply;
        }
    }
}
