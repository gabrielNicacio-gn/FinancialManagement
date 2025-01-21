using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Shared
{
    public class BaseResponseDto<T>
    {
        public bool IsSucess { get; private set; } = true;
        public List<string> Errors { get; private set; }
        public T? Data { get; private set; }

        public BaseResponseDto()
        => Errors = new List<string>();

        public BaseResponseDto(bool isSucess = true) : this()
        => IsSucess = isSucess;

        public BaseResponseDto(T data) : this()
        => Data = data;

        public void AddError(string error) =>
        Errors.Add(error);

        public void AddErrors(IEnumerable<string> errors) =>
        Errors.AddRange(errors);
    }

}