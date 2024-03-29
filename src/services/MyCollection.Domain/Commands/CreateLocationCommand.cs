﻿using FluentValidation;
using FluentValidation.Results;
using MyCollection.Domain.Contracts;
using System.Text.Json.Serialization;

namespace MyCollection.Domain.Commands
{
    public class CreateLocationCommand : ICommand
    {
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }

        public CreateLocationCommand(string initials, string description, Guid? parentId)
        {
            Initials = initials;
            Description = description;
            ParentId = parentId;
        }

        public string Initials { get; private set; }
        public string Description { get; private set; }
        public Guid? ParentId { get; private set; }


        public bool IsValid()
        {
            ValidationResult = new CreateLocationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateLocationCommandValidation : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidation()
        {
            RuleFor(e => e.Initials)
               .NotEmpty().WithMessage("Por favor, verifique se a Sigla foi informado.")
               .Length(3, 10).WithMessage("A Sigla deve ter entre 3 e 10 caracteres.");

            RuleFor(e => e.Description)
               .Length(3, 100).WithMessage("Descrição deve ter entre 3 e 100 caracteres.");

            RuleFor(e => e.ParentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Informe uma localização pai corretamente");
        }
    }
}
