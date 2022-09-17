using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>
    {
        public int Id { get; set; }

        public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;
            private readonly IMapper _mapper;

            public DeleteProgrammingLanguageCommandHandler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _mapper = mapper;
                _programmingLanguageRepository = programmingLanguageRepository;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }
            public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage programmingLanguage = await _programmingLanguageRepository.GetAsync(p => p.Id == request.Id);

                _programmingLanguageBusinessRules.ProgrammingLanguageShouldBeExistWhenRequested(programmingLanguage);

                ProgrammingLanguage deletedProgrammingLanguage = await _programmingLanguageRepository
                    .DeleteAsync(programmingLanguage);

                DeletedProgrammingLanguageDto deletedProgrammingLanguageDto = _mapper
                    .Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);

                return deletedProgrammingLanguageDto;

            }
        }
    }
}
