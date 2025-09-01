using Revenue_recognition_system.Application.DTOs;

namespace Revenue_recognition_system.Application.Validators;

public class ClientValidator
{
    public ValidationResult ValidateIndividual(CreateIndividualClientDto dto)
        {
            var result = new ValidationResult();

            if (dto == null)
            {
                result.Errors.Add("Payload is required.");
                return result;
            }

            ValidateFirstName(dto.FirstName, result);
            ValidateLastName(dto.LastName, result);
            ValidatePesel(dto.Pesel, result);

            return result;
        }

        public ValidationResult ValidateCompany(CreateCompanyClientDto dto)
        {
            var result = new ValidationResult();

            if (dto == null)
            {
                result.Errors.Add("Payload is required.");
                return result;
            }

            ValidateName(dto.Name, result);
            ValidateKrs(dto.Krs, result);

            return result;
        }

        private void ValidateFirstName(string firstName, ValidationResult r)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                r.Errors.Add("FirstName is required.");
            else if (firstName.Length > 100)
                r.Errors.Add("FirstName max length is 100.");
        }

        private void ValidateLastName(string lastName, ValidationResult r)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                r.Errors.Add("LastName is required.");
            else if (lastName.Length > 100)
                r.Errors.Add("LastName max length is 100.");
        }

        private void ValidatePesel(string pesel, ValidationResult r)
        {
            if (string.IsNullOrWhiteSpace(pesel))
                r.Errors.Add("PESEL is required.");
            else if (pesel.Length != 11 || !pesel.All(char.IsDigit))
                r.Errors.Add("PESEL must be 11 digits.");
        }

        private void ValidateName(string name, ValidationResult r)
        {
            if (string.IsNullOrWhiteSpace(name))
                r.Errors.Add("Name is required.");
            else if (name.Length > 100)
                r.Errors.Add("Name max length is 100.");
        }

        private void ValidateKrs(string krs, ValidationResult r)
        {
            if (string.IsNullOrWhiteSpace(krs))
                r.Errors.Add("KRS is required.");
            else if (krs.Length != 10 || !krs.All(char.IsDigit))
                r.Errors.Add("KRS must be 10 digits.");
        }
    }
