using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class EmpleadoValidator : AbstractValidator<EmpleadoDTO>
    {
        public EmpleadoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(x => x.Apellidos)
                .NotEmpty().WithMessage("Los apellidos son obligatorios")
                .MaximumLength(200).WithMessage("Los apellidos no pueden exceder los 200 caracteres");

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("El formato del correo electrónico no es válido")
                .When(x => !string.IsNullOrEmpty(x.Email)); 

            RuleFor(x => x.AreaId)
                .GreaterThan(0).WithMessage("Debe seleccionar un área válida");

            RuleFor(x => x.FechaContratacion)
                .NotEmpty().WithMessage("La fecha de contratación es obligatoria");
        }
    }
}
