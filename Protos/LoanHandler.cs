using AutoMapper;
using BE_LoansApp.DataAccess;
using BE_LoansApp.DTOs;
using BE_LoansApp.Entities;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace BE_LoansApp.Protos
{
    public class LoanHandler: LoanService.LoanServiceBase
    {
        private readonly ThingsContext _context;

        public LoanHandler(ThingsContext context)
        {
            _context = context;
        }
        public override Task<Mensaje> ReturnLoan(Loan request, ServerCallContext context)
        {
            var loan = _context.Loans.FirstOrDefault(x => x.Id == request.Id);

            if (loan == null)
            {
                return Task.FromResult(new Mensaje
                {
                    Respuesta = "No se encontro el prestamo Buscado, con el id Nro." + request.Id
                }); 
            }
            else {

                if (loan.Status == "devuelto")
                {
                    return Task.FromResult(new Mensaje
                    {
                        Respuesta = "El prestamo al que hace referencia ya se encuentra registrado como Devuelto"
                    });
                }
                else {
                    loan.ReturnDate = DateTime.Now;
                    loan.Status = "devuelto";
                    _context.SaveChanges();

                    return Task.FromResult(new Mensaje
                    {
                        Respuesta = "Prestamo Devuelto Exitosamente"
                    });
                }
              
            } 
                           
        }

    }
}
