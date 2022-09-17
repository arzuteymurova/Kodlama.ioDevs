using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageDto>
    {
        public string Name { get; set; }

        public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;
            private readonly IMapper _mapper;

            public CreateProgrammingLanguageCommandHandler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _mapper = mapper;
                _programmingLanguageRepository = programmingLanguageRepository;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageBusinessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenInsertedOrUpdated(request.Name);

                ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);

                ProgrammingLanguage createdProgrammingLanguage = await _programmingLanguageRepository
                    .AddAsync(mappedProgrammingLanguage);

                CreatedProgrammingLanguageDto createdProgrammingLanguageDto = _mapper
                    .Map<CreatedProgrammingLanguageDto>(createdProgrammingLanguage);

                return createdProgrammingLanguageDto;
            }
        }
    }
}
